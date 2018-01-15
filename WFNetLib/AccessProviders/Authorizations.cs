using System;
using System.Collections.Generic;

using System.Text;
using System.Data.OleDb;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System.Data;

namespace WFNetLib.AccessProviders
{
    public static class Authorizations
    {
        public static string ApplicationName { get; set; }
        public static string _DatabaseFileName;
        public static string _AppName;
        public static int _ApplicationId = 0;
        public static DateTime _ApplicationIDCacheDate;
        public static bool IsRoleInAuthorization(string rolename, string authorizationName)
        {
            SecUtility.CheckParameter(ref rolename, true, false, true, 255, "rolename");
            if (rolename.Length < 1)
                return false;
            SecUtility.CheckParameter(ref authorizationName, true, true, true, 255, "authorizationName");

            AccessConnectionHolder holder = AccessConnectionHelper.GetConnection(_DatabaseFileName, true);
            OleDbConnection connection = holder.Connection;
            try
            {
                try
                {
                    int appId = GetApplicationId(holder);
                    int roleId = GetRoleId(connection, appId, rolename);
                    int authorizationId = GetAuthorizationId(connection, appId, authorizationName);

                    OleDbCommand command;

                    if (roleId == 0)
                    {
                        return false;
                    }

                    if (authorizationId == 0)
                    {
                        return false;
                    }

                    command = new OleDbCommand(@"SELECT UserId FROM aspnet_UsersInRoles WHERE RoleId = @RoleId AND AuthorizationId = @AuthorizationId", connection);
                    command.Parameters.Add(new OleDbParameter("@RoleId", roleId));
                    command.Parameters.Add(new OleDbParameter("@AuthorizationId", authorizationId));

                    object result = command.ExecuteScalar();

                    if (result == null || !(result is int) || ((int)result) != roleId)
                        return false;
                    return true;
                }
                catch (Exception e)
                {
                    throw AccessConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
        }
        public static string[] GetRolesForAuthorization(string authorizationname)
        {
            SecUtility.CheckParameter(ref authorizationname, true, false, true, 255, "authorizationname");
            if (authorizationname.Length < 1)
                return new string[0];

            AccessConnectionHolder holder = AccessConnectionHelper.GetConnection(_DatabaseFileName, true);
            OleDbConnection connection = holder.Connection;
            OleDbDataReader reader = null;

            try
            {
                try
                {
                    int appId = GetApplicationId(holder);
                    int authorizationId = GetAuthorizationId(connection, appId, authorizationname);
                    if (authorizationId == 0)
                    {
                        return new string[0];
                    }
                    OleDbCommand command;
                    StringCollection sc = new StringCollection();
                    String[] strReturn;


                    command = new OleDbCommand(@"SELECT RoleName FROM aspnet_AuthorizationsInRoles ar, aspnet_Roles r " +
                                                @"WHERE ar.AuthorizationId = @AuthorizationId AND ar.RoleId = r.RoleId " +
                                                @"ORDER BY RoleName",
                                               connection);
                    command.Parameters.Add(new OleDbParameter("@AuthorizationId", authorizationId));
                    reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (reader.Read())
                        sc.Add(reader.GetString(0));
                    strReturn = new String[sc.Count];
                    sc.CopyTo(strReturn, 0);
                    return strReturn;
                }
                catch (Exception e)
                {
                    throw AccessConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
        }
        public static void CreateRole(string authorizationName)
        {
            SecUtility.CheckParameter(ref authorizationName, true, true, true, 255, "authorizationName");

            AccessConnectionHolder holder = AccessConnectionHelper.GetConnection(_DatabaseFileName, true);
            OleDbConnection connection = holder.Connection;
            bool fBeginTransCalled = false;
            try
            {
                try
                {
                    int appId = GetApplicationId(holder);
                    OleDbCommand command;
                    int authorizationId = GetAuthorizationId(connection, appId, authorizationName);

                    if (authorizationId != 0)
                    {
                        throw new ProviderException("Provider role already exists: " + authorizationName);
                    }

                    command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    fBeginTransCalled = true;
                    command = new OleDbCommand(@"INSERT INTO aspnet_Authorizations (ApplicationId, AuthorizationName) VALUES (@AppId, @AName)", connection);
                    command.Parameters.Add(new OleDbParameter("@AppId", appId));
                    command.Parameters.Add(new OleDbParameter("@AName", authorizationName));
                    int returnValue = command.ExecuteNonQuery();
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    fBeginTransCalled = false;

                    if (returnValue == 1)
                        return;
                    throw new ProviderException("Unknown provider failure");
                }
                catch (Exception e)
                {
                    if (fBeginTransCalled)
                    {
                        try
                        {
                            OleDbCommand command = new OleDbCommand("ROLLBACK TRANSACTION", connection);
                            command.ExecuteNonQuery();
                        }
                        catch { }
                    }
                    throw AccessConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////

        public static bool DeleteRole(string authorizationName, bool throwOnPopulatedAuthorization)
        {
            SecUtility.CheckParameter(ref authorizationName, true, true, true, 255, "authorizationName");
            AccessConnectionHolder holder = AccessConnectionHelper.GetConnection(_DatabaseFileName, true);
            OleDbConnection connection = holder.Connection;
            bool fBeginTransCalled = false;
            try
            {
                try
                {
                    int appId = GetApplicationId(holder);
                    OleDbCommand command;
                    int authorizationId = GetAuthorizationId(connection, appId, authorizationName);

                    if (authorizationId == 0)
                    {
                        return false;
                    }

                    if (throwOnPopulatedAuthorization)
                    {
                        command = new OleDbCommand(@"SELECT COUNT(*) " +
                                               @"FROM aspnet_AuthorizationsInRoles ar, aspnet_Authorizations a " +
                                               @"WHERE ar.AuthorizationId = @AuthorizationId AND ar.AuthorizationId = a.AuthorizationId", connection);

                        command.Parameters.Add(new OleDbParameter("@AuthorizationId", authorizationId));
                        object num = command.ExecuteScalar();
                        if (!(num is int) || ((int)num) != 0)
                            throw new ProviderException("Authorization is not empty");
                    }
                    else
                    {
                        command = new OleDbCommand("BEGIN TRANSACTION", connection);
                        command.ExecuteNonQuery();
                        fBeginTransCalled = true;
                        command = new OleDbCommand(@"DELETE FROM aspnet_AuthorizationsInRoles WHERE AuthorizationId = @AuthorizationId", connection);
                        command.Parameters.Add(new OleDbParameter("@AuthorizationId", authorizationId));
                        /*int returnValue =*/ command.ExecuteNonQuery();
                        command = new OleDbCommand("COMMIT TRANSACTION", connection);
                        command.ExecuteNonQuery();
                        fBeginTransCalled = false;
                    }

                    command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    fBeginTransCalled = true;
                    command = new OleDbCommand(@"DELETE FROM aspnet_Authorizations WHERE AuthorizationId = @AuthorizationId", connection);
                    command.Parameters.Add(new OleDbParameter("@AuthorizationId", authorizationId));
                    int returnValue = command.ExecuteNonQuery();
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    fBeginTransCalled = false;

                    return (returnValue == 1);
                }
                catch (Exception e)
                {
                    if (fBeginTransCalled)
                    {
                        try
                        {
                            OleDbCommand command = new OleDbCommand("ROLLBACK TRANSACTION", connection);
                            command.ExecuteNonQuery();
                        }
                        catch { }
                    }
                    throw AccessConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////

        public static bool RoleExists(string authorizationName)
        {
            try
            {
                SecUtility.CheckParameter(ref authorizationName, true, true, true, 255, "authorizationName");
            }
            catch
            {
                return false;
            }
            AccessConnectionHolder holder = AccessConnectionHelper.GetConnection(_DatabaseFileName, true);
            OleDbConnection connection = holder.Connection;
            try
            {
                try
                {
                    int appId = GetApplicationId(holder);
                    int authorizationId = GetAuthorizationId(connection, appId, authorizationName);

                    return (authorizationId != 0);
                }
                catch (Exception e)
                {
                    throw AccessConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////

        public static void AddAuthorizationsToRoles(string[] authorizationnames, string[] roleNames)
        {
            SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 255, "roleNames");
            SecUtility.CheckArrayParameter(ref authorizationnames, true, true, true, 255, "authorizationnames");

            AccessConnectionHolder holder = AccessConnectionHelper.GetConnection(_DatabaseFileName, true);
            OleDbConnection connection = holder.Connection;
            bool fBeginTransCalled = false;
            try
            {
                try
                {
                    int appId = GetApplicationId(holder);
                    int[] authorizationIds = new int[authorizationnames.Length];
                    int[] roleIds = new int[roleNames.Length];

                    OleDbCommand command;

                    for (int iterR = 0; iterR < roleNames.Length; iterR++)
                    {
                        roleIds[iterR] = GetRoleId(connection, appId, roleNames[iterR]);
                        if (roleIds[iterR] == 0)
                        {
                            throw new ProviderException("Provider role not found: " + roleNames[iterR]);
                        }
                    }
                    for (int iterA = 0; iterA < authorizationnames.Length; iterA++)
                    {
                        authorizationIds[iterA] = GetAuthorizationId(connection, appId, authorizationnames[iterA]);
                    }
                    command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    fBeginTransCalled = true;

                    for (int iterA = 0; iterA < authorizationnames.Length; iterA++)
                    {
                        if (authorizationIds[iterA] == 0)
                            continue;
                        for (int iterR = 0; iterR < roleNames.Length; iterR++)
                        {
                            command = new OleDbCommand(@"SELECT AuthorizationId FROM aspnet_AuthorizationsInRoles WHERE AuthorizationId = @AuthorizationId AND RoleId = @RoleId", connection);
                            command.Parameters.Add(new OleDbParameter("@AuthorizationId", authorizationIds[iterA]));
                            command.Parameters.Add(new OleDbParameter("@RoleId", roleIds[iterR]));

                            object result = command.ExecuteScalar();
                            if (result != null && (result is int) && ((int)result) == authorizationIds[iterA])
                            { // Exists!

                                throw new ProviderException("The Authorization " + authorizationnames[iterA] + " is already in role " + roleNames[iterR]);
                            }
                        }
                    }

                    for (int iterA = 0; iterA < authorizationnames.Length; iterA++)
                    {
                        if (authorizationIds[iterA] == 0)
                        {
                            authorizationIds[iterA] = GetAuthorizationId(connection, appId, authorizationnames[iterA]);
                        }
                        if (authorizationIds[iterA] == 0)
                        {
                            throw new ProviderException("Authorization not found: " + authorizationnames[iterA]);
                        }
                    }
                    for (int iterA = 0; iterA < authorizationnames.Length; iterA++)
                    {
                        for (int iterR = 0; iterR < roleNames.Length; iterR++)
                        {
                            command = new OleDbCommand(@"INSERT INTO aspnet_AuthorizationsInRoles (AuthorizationId, RoleId) VALUES(@AuthorizationId, @RoleId)", connection);
                            command.Parameters.Add(new OleDbParameter("@AuthorizationId", authorizationIds[iterA]));
                            command.Parameters.Add(new OleDbParameter("@RoleId", roleIds[iterR]));
                            if (command.ExecuteNonQuery() != 1)
                            {
                                throw new ProviderException("Unknown provider failure");
                            }
                        }
                    }
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    try
                    {
                        if (fBeginTransCalled)
                        {
                            OleDbCommand command = new OleDbCommand("ROLLBACK TRANSACTION", connection);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch { }
                    throw AccessConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////

        public static void RemoveAuthorizationsFromRoles(string[] authorizationnames, string[] roleNames)
        {
            SecUtility.CheckArrayParameter(ref roleNames, true, true, true, 255, "roleNames");
            SecUtility.CheckArrayParameter(ref authorizationnames, true, true, true, 255, "authorizationnames");

            AccessConnectionHolder holder = AccessConnectionHelper.GetConnection(_DatabaseFileName, true);
            OleDbConnection connection = holder.Connection;
            bool fBeginTransCalled = false;
            try
            {
                try
                {
                    int appId = GetApplicationId(holder);
                    int[] authorizationIds = new int[authorizationnames.Length];
                    int[] roleIds = new int[roleNames.Length];

                    OleDbCommand command;
                    command = new OleDbCommand("BEGIN TRANSACTION", connection);
                    command.ExecuteNonQuery();
                    fBeginTransCalled = true;

                    for (int iterA = 0; iterA < authorizationnames.Length; iterA++)
                    {
                        authorizationIds[iterA] = GetAuthorizationId(connection, appId, authorizationnames[iterA]);
                        if (authorizationIds[iterA] == 0)
                        {
                            throw new ProviderException("Authorization not found: " + authorizationnames[iterA]);
                        }
                    }
                    for (int iterR = 0; iterR < roleNames.Length; iterR++)
                    {
                        roleIds[iterR] = GetRoleId(connection, appId, roleNames[iterR]);
                        if (roleIds[iterR] == 0)
                        {
                            throw new ProviderException("Role not found: " + roleNames[iterR]);
                        }
                    }
                    for (int iterA = 0; iterA < authorizationnames.Length; iterA++)
                    {
                        for (int iterR = 0; iterR < roleNames.Length; iterR++)
                        {
                            command = new OleDbCommand(@"SELECT AuthorizationId FROM aspnet_AuthorizationsInRoles WHERE AuthorizationId = @AuthorizationId AND RoleId = @RoleId", connection);
                            command.Parameters.Add(new OleDbParameter("@AuthorizationId", authorizationIds[iterA]));
                            command.Parameters.Add(new OleDbParameter("@RoleId", roleIds[iterR]));

                            object result = command.ExecuteScalar();
                            if (result == null || !(result is int) || ((int)result) != authorizationIds[iterA])
                            { // doesn't exist!
                                throw new ProviderException("The Authorization " + authorizationnames[iterA] + " is already not in role " + roleNames[iterR]);
                            }
                        }
                    }

                    for (int iterA = 0; iterA < authorizationnames.Length; iterA++)
                    {
                        for (int iterR = 0; iterR < roleNames.Length; iterR++)
                        {
                            command = new OleDbCommand(@"DELETE FROM aspnet_AuthorizationsInRoles WHERE AuthorizationId = @AuthorizationId AND RoleId = @RoleId", connection);
                            command.Parameters.Add(new OleDbParameter("@AuthorizationId", authorizationIds[iterA]));
                            command.Parameters.Add(new OleDbParameter("@RoleId", roleIds[iterR]));
                            if (command.ExecuteNonQuery() != 1)
                            {
                                throw new ProviderException("Unknown failure");
                            }
                        }
                    }
                    command = new OleDbCommand("COMMIT TRANSACTION", connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    try
                    {
                        if (fBeginTransCalled)
                        {
                            OleDbCommand command = new OleDbCommand("ROLLBACK TRANSACTION", connection);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch { }
                    throw AccessConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
        }


        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////

        public static string[] GetAuthorizationsInRole(string roleName)
        {
            SecUtility.CheckParameter(ref roleName, true, true, true, 255, "roleName");
            StringCollection sc = new StringCollection();
            String[] strReturn;
            AccessConnectionHolder holder = AccessConnectionHelper.GetConnection(_DatabaseFileName, true);
            OleDbDataReader reader = null;
            OleDbConnection connection = holder.Connection;
            try
            {
                try
                {
                    int appId = GetApplicationId(holder);
                    int roleId = GetRoleId(connection, appId, roleName);
                    OleDbCommand command;
                    if (roleId == 0)
                    {
                        throw new ProviderException("Role not found: " + roleName);
                    }
                    command = new OleDbCommand(@"SELECT AuthorizationName " +
                                               @"FROM aspnet_AuthorizationsInRoles ar, aspnet_Authorizations a " +
                                               @"WHERE ar.RoleId = @RoleId AND ar.AuthorizationId = a.AuthorizationId " +
                                               @"ORDER BY UserName", connection);

                    command.Parameters.Add(new OleDbParameter("@RoleId", roleId));
                    reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (reader.Read())
                        sc.Add(reader.GetString(0));
                }
                catch (Exception e)
                {
                    throw AccessConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
            strReturn = new String[sc.Count];
            sc.CopyTo(strReturn, 0);
            return strReturn;
        }

        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        public static string[] FindAuthorizationsInRole(string roleName, string authorizationnameToMatch)
        {
            SecUtility.CheckParameter(ref roleName, true, true, true, 255, "roleName");
            SecUtility.CheckParameter(ref authorizationnameToMatch, true, true, false, 255, "authorizationnameToMatch");

            StringCollection sc = new StringCollection();

            AccessConnectionHolder holder = AccessConnectionHelper.GetConnection(_DatabaseFileName, true);
            OleDbDataReader reader = null;
            OleDbConnection connection = holder.Connection;
            try
            {
                try
                {
                    int appId = GetApplicationId(holder);
                    int roleId = GetRoleId(connection, appId, roleName);

                    OleDbCommand command;

                    if (roleId == 0)
                    {
                        throw new ProviderException("Role not found " + roleName);
                    }

                    command = new OleDbCommand(@"SELECT AuthorizationName " +
                                               @"FROM aspnet_AuthorizationsInRoles ar, aspnet_Authorizations a " +
                                               @"WHERE ar.RoleId = @RoleId AND ar.AuthorizationId = a.AuthorizationId AND A.AuthorizationName LIKE '%'+@AuthorizationNameToMatch+'%' " +
                                               @"ORDER BY UserName", connection);
                    command.Parameters.Add(new OleDbParameter("@RoleId", roleId));
                    command.Parameters.Add(new OleDbParameter("@AuthorizationNameToMatch", authorizationnameToMatch));
                    reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (reader.Read())
                        sc.Add((string)reader.GetString(0));
                }
                catch (Exception e)
                {
                    throw AccessConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
            string[] allUsers = new string[sc.Count];
            sc.CopyTo(allUsers, 0);
            return allUsers;
        }

        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////

        public static string[] GetAllAuthorizations()
        {
            AccessConnectionHolder holder = AccessConnectionHelper.GetConnection(_DatabaseFileName, true);
            OleDbConnection connection = holder.Connection;
            OleDbDataReader reader = null;
            try
            {
                try
                {
                    int appId = GetApplicationId(holder);
                    OleDbCommand command;
                    StringCollection sc = new StringCollection();
                    String[] strReturn = null;

                    command = new OleDbCommand(@"SELECT AuthorizationName FROM aspnet_Authorizations WHERE ApplicationId = @AppId ORDER BY RoleName", connection);
                    command.Parameters.Add(new OleDbParameter("@AppId", appId));
                    reader = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    while (reader.Read())
                        sc.Add(reader.GetString(0));
                    strReturn = new String[sc.Count];
                    sc.CopyTo(strReturn, 0);
                    return strReturn;
                }
                catch (Exception e)
                {
                    throw AccessConnectionHelper.GetBetterException(e, holder);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                    holder.Close();
                }
            }
            catch
            {
                throw;
            }
        }
        private static int GetRoleId(OleDbConnection connection, int applicationID, string roleName)
        {
            if (applicationID == 0 || roleName == null || roleName.Length < 1) // Application doesn't exist or user doesn't exist
                return 0;

            if (connection == null)
                return 0; // Wrong params!

            OleDbCommand command = new OleDbCommand(@"SELECT RoleId FROM aspnet_Roles WHERE ApplicationId = @AppId AND RoleName = @RoleName", connection);
            command.Parameters.Add(new OleDbParameter("@AppId", applicationID));
            command.Parameters.Add(new OleDbParameter("@RoleName", roleName));

            object result = command.ExecuteScalar();
            if (result == null || !(result is int) || ((int)result) == 0)
            {
                return 0;
            }
            else
            {
                return (int)result;
            }
        }
        private static int GetAuthorizationId(OleDbConnection connection, int appId, string authorizationName)
        {
            object result;
            OleDbCommand command;

            command = new OleDbCommand(@"SELECT AuthorizationId FROM aspnet_Authorizations WHERE ApplicationId = @AppId AND AuthorizationName = @AuthorizationName", connection);
            command.Parameters.Add(new OleDbParameter("@AppId", appId));
            command.Parameters.Add(new OleDbParameter("@AuthorizationName", authorizationName));
            result = command.ExecuteScalar();
            if (result == null || !(result is int) || ((int)result) == 0)
            {
                return 0;
            }
            else
            {
                return (int)result;
            }
        }
        private static int GetApplicationId(AccessConnectionHolder holder)
        {
            if (_ApplicationId != 0 && holder.CreateDate < _ApplicationIDCacheDate) // Already cached?
                return _ApplicationId;
            string appName = _AppName;
            if (appName.Length > 255)
                appName = appName.Substring(0, 255);
            _ApplicationId = AccessConnectionHelper.GetApplicationID(holder.Connection, appName, true);
            _ApplicationIDCacheDate = DateTime.Now;
            if (_ApplicationId != 0)
                return _ApplicationId;
            throw new ProviderException("Provider Error");
        }
    }
}
