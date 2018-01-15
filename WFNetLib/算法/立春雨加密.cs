using System;
using System.Collections.Generic;

using System.Text;

namespace WFNetLib
{
    public class lcyHexDecode
    {
        static readonly ushort[]  TCodeKey=new ushort[316]
        {
	        0x3139,0x2129,0x1119,0x0109,0x333B,0x232B,0x131B,0x030B,0x353D,0x252D,0x151D,0x050D,0x373F,0x272F,0x171F,
	        0x070F,0x3038,0x2028,0x1018,0x0008,0x323A,0x222A,0x121A,0x020A,0x343C,0x242C,0x141C,0x040C,0x363E,0x262E,0x161E,
	        0x060E,0x2707,0x2F0F,0x3717,0x3F1F,0x2606,0x2E0E,0x3616,0x3E1E,0x2505,0x2D0D,0x3515,0x3D1D,0x2404,0x2C0C,0x3414,
	        0x3C1C,0x2303,0x2B0B,0x3313,0x3B1B,0x2202,0x2A0A,0x3212,0x3A1A,0x2101,0x2909,0x3111,0x3919,0x2000,0x2808,0x3010,
	        0x3818,0x8080,0x041F,0x0100,0x0302,0x8080,0x0803,0x0504,0x0706,0x8080,0x0C07,0x0908,0x0B0A,0x8080,0x100B,0x0D0C,
	        0x0F0E,0x8080,0x140F,0x1110,0x1312,0x8080,0x1813,0x1514,0x1716,0x8080,0x1C17,0x1918,0x1B1A,0x8080,0x001B,0x1D1C,
	        0x1F1E,0x060F,0x1413,0x0B1C,0x101B,0x0E00,0x1916,0x1104,0x091E,0x0701,0x0D17,0x1A1F,0x0802,0x0C12,0x051D,0x0A15,
	        0x1803,0x3038,0x2028,0x1018,0x0008,0x3139,0x2129,0x1119,0x0109,0x323A,0x222A,0x121A,0x020A,0x333B,0x232B,0x8080,
	        0x8080,0x363E,0x262E,0x161E,0x060E,0x353D,0x252D,0x151D,0x050D,0x343C,0x242C,0x141C,0x040C,0x131B,0x030B,0x8080,
	        0x8080,0x8080,0x040D,0x0A10,0x0017,0x8080,0x0902,0x0E1B,0x1405,0x8080,0x0716,0x0B12,0x1903,0x8080,0x010F,0x1A06,
	        0x0C13,0x8080,0x3A2C,0x2237,0x3228,0x8080,0x3321,0x362B,0x2430,0x8080,0x382F,0x2A34,0x253B,0x8080,0x2331,0x352D,
	        0x2027,0x84D2,0xB16F,0x3EA9,0xC750,0xD81F,0x74A3,0x6BC5,0x920E,0x417B,0xE29C,0xAD06,0x58F3,0xE721,0x8D4A,0x90FC,
	        0x6B35,0x2E4B,0x8DF0,0x973C,0x615A,0xB7D0,0x1A49,0x5CE3,0x862F,0xBD14,0x7EC3,0x68AF,0x9205,0xD86B,0xA714,0x0F95,
	        0x3CE2,0xAFC1,0x6892,0x340D,0x5BE7,0x42AF,0x957C,0xDE61,0x380B,0xF59E,0xC328,0x4A70,0xB61D,0x2C43,0xFA95,0x17BE,
	        0x8D60,0x412C,0xB67A,0x3F85,0xE9D0,0x2CEB,0xD147,0xFA50,0x8639,0x1B42,0x78AD,0xC5F9,0x0E63,0xC7B8,0x2D1E,0x096F,
	        0x53A4,0xE37D,0x9A06,0x8512,0x4FBC,0xB5D8,0x036F,0x2C47,0xE91A,0x90A6,0x7DCB,0x3EF1,0x8452,0x063F,0xD8A1,0x5B94,
	        0x2EC7,0x9EA0,0xF563,0xC71D,0x28B4,0x09D7,0x6A34,0x5E28,0xF1CB,0x49D6,0x308F,0x2CB1,0xE75A,0xD01A,0x8769,0xE34F,
	        0x2CB5,0x8EF1,0x346B,0x2D97,0x5AC0,0x473D,0x8EF2,0x1AC0,0xB569,0x7B0E,0xD1A4,0xC658,0x2F93,0xA1D8,0x423F,0x7CB6,
	        0xE905,0xD1E4,0xB82F,0x6C3A,0x0759,0x740F,0xD1E2,0xCBA6,0x3895,0xE841,0x2BD6,0x97FC,0x503A,0x82FC,0x1749,0x3E5B,
	        0x6DA0,0x0201,0x0202,0x0202,0x0102,0x0202,0x0202,0x0202,0x0001,0x4080,0x1020,0x0408,0x0102
        };
        static byte[] RAMBUF=new byte[256];
        static byte R0XC0, R0XC1=0;
        static byte R0XBB;
        static byte R0XBC;
        static byte R0X52;
        static byte R0X53;
        static byte R0XBD;
        static byte R0XBE;
        static byte R0X40, R0X41;
        static byte R0XCB;            // bit6 读FLASH标识位		
        static int FlashAddr;
        static uint ulR0XAB;
        static uint ulR0XAF;
        static byte ReadFshData(byte c)
        {
	        int addr;            
	        addr = FlashAddr+c-0x1e14;
	        int i=addr/2;
            int j = addr % 2;
            if (j == 0)
            {
                return BytesOP.GetLowByte(TCodeKey[i]);
            }
            else
                return BytesOP.GetHighByte(TCodeKey[i]);
        }
        static void GetFshKey()
        {
	        byte t;
	        int p0,p1;

	        R0XBD=R0X52;
	        R0XBE=R0X53;

	        for (;;)
	        {
		        R0X40=R0XBE;

		        R0X40=(byte)((R0X40>>3)&0X1F);

		        t=(byte)(R0XBC+R0X40);
		        p0=t;

		        FlashAddr=0x1E14;
		        if (BytesOP.GetBit(R0XCB,6)) 
                    FlashAddr=0X1EF4;

                R0X41 = ReadFshData(R0XBD);

		        if ((R0X41&0x80)==0)
		        {
			        R0XC0=(byte)(R0X41&0X7);

			        FlashAddr=0X1E14;
			        if (BytesOP.GetBit(R0XCB,6)) 
                        FlashAddr=0X1EF4;
                    R0X40 = ReadFshData(R0XBD); 

			        R0X40=(byte)((R0X40>>3)&0x1F);            // 去掉了高三位，即去掉了循环右移影响

			        t=(byte)(R0XBB+R0X40);
			        p1=t;

			        FlashAddr=0X2084;
                    t = ReadFshData(R0XC0);//(A.R0XC0);
			        t=(byte)(t&(RAMBUF[p1]));

			        if (t!=0) 
                        t=0x80;
		        }
		        else
		        {
			        t=0x80;
		        }

		        RAMBUF[p0] = (byte)(((RAMBUF[p0])>>1)|t);

		        R0XBD--;
		        R0XBE--;
		        if ((R0XBE&0X80)!=0) 
                    break;
	        }

        }
        static void ShiftFshKey()
        {
	        FlashAddr=0X2074;
	        R0X41=ReadFshData(R0XC1);

	        if (R0X41!=0)
	        {
		        for (;;)
		        {
			        //			asm("BANKSEL _A.R0XAB");
			        //			asm("RRCF _A.R0XAB, F, B");
			        //			asm("RRCF _A.R0XAC, F, B");
			        //			asm("RRCF _A.R0XAD, F, B");
			        //			asm("RRCF _A.R0XAE, F, B ");
			        ulR0XAB>>=1;

			        BytesOP.ClrBit(ref ulR0XAB,7);
			        if (BytesOP.GetBit(ulR0XAB,27))
                        {BytesOP.SetBit(ref ulR0XAB,7);};

			        //           asm("BANKSEL _A.R0XAF");
			        //		   asm("RRCF _A.R0XAF, F, B");
			        //		   asm("RRCF _A.R0XB0, F, B");
			        //		   asm("RRCF _A.R0XB1, F, B");
			        //		   asm("RRCF _A.R0XB2, F, B");
			        ulR0XAF>>=1;

			        BytesOP.ClrBit(ref ulR0XAF,7);
			        if (BytesOP.GetBit(ulR0XAF,27))
                        {BytesOP.SetBit(ref ulR0XAF,7);};

			        R0X41--;
			        if (R0X41==0) 
                        break;
		        }
	        }

	        R0XBB=0XAB;
	        R0XBC=0XB3;
	        R0X52=0X7F;
	        R0X53=0X3F;

	        BytesOP.SetBit(ref R0XCB,6);
	        GetFshKey();

        }
        static void ShiftMixKey()
        {
            byte t;
	        int p0;

	        R0XBD=0XE0;

	        for (;;)
	        {
		        FlashAddr=0X1F74;
		        p0=R0XBB;
		        t=RAMBUF[p0];

		        t=(byte)((t>>1)|R0XBD);

		        R0X52=ReadFshData(t);

                t = RAMBUF[p0];

		        if ((t&1)==0)
		        {
			        R0X52=(byte)(R0X52>>4);
		        }

		        R0X52&=0XF;
		        p0=R0XBC;

		        if (!BytesOP.GetBit(R0XBD,5))
		        {
			        R0XBC++;

			        t=RAMBUF[p0];
			        t<<=4;

			        R0X52|=t;
		        }

		        RAMBUF[p0]=R0X52;

		        R0XBB++;

		        t=R0XBD;
		        R0XBD=(byte)(R0XBD-0X20);

		        //if (CARRY==0) break;
		        if (t<0x20) 
                    break;
	        }
        }
        static void MixFshKey()
        {
            int p0, p1, p2;

            R0XBB = 0XA7;
            R0XBC = 0X9B;
            R0X52 = 0XBF;
            R0X53 = 0X3F;

            BytesOP.ClrBit(ref R0XCB, 6);
            GetFshKey();
            ShiftFshKey();

            R0X53 = 8;
            p0 = 0x9b;
            p1 = 0xb3;

            for (; ; )
            {
                RAMBUF[p0] ^= (RAMBUF[p1]);
                p0++;
                p1++;

                R0X53--;
                if (R0X53 == 0)
                    break;
            }

            R0XBB = 0X9B;
            R0XBC = 0X9B;
            ShiftMixKey();

            R0XBB = 0X9B;
            R0XBC = 0XB3;
            R0X52 = 0XDF;
            R0X53 = 0X1F;

            BytesOP.ClrBit(ref R0XCB, 6);
            GetFshKey();

            R0X53 = 4;
            p0 = 0xa6;
            p1 = 0xaa;
            p2 = 0xb6;

            for (; ; )
            {
                RAMBUF[p2] ^= (RAMBUF[p0]);
                RAMBUF[p0] = RAMBUF[p1];
                RAMBUF[p1] = RAMBUF[p2];

                p0--;
                p1--;
                p2--;

                R0X53--;
                if (R0X53 == 0)
                    break;

            }

        }
        public static byte[] Decode(byte[] sin)
        {
            byte[] sout = new byte[8];

            for (int i = 0; i < 8; i++)
            {
                RAMBUF[i + 0x83] = sin[i];
                RAMBUF[i + 0x8b] = sin[i];
            }

            R0XBB = 0X8B;
            R0XBC = 0XA3;
            R0X52 = 0X3F;
            R0X53 = 0X3F;

            BytesOP.ClrBit(ref R0XCB, 6);

            GetFshKey();

            R0XBB = 0X93;
            R0XBC = 0XAB;
            R0X52 = 0X3F;
            R0X53 = 0X3F;

            BytesOP.SetBit(ref R0XCB, 6);

            GetFshKey();

            for (int i = 16; i != 0; i--)     // 16次循环
            {
                MixFshKey();
            }

            R0XBB = 0XA3;
            R0XBC = 0X8B;
            R0X52 = 0X7F;
            R0X53 = 0X3F;

            BytesOP.ClrBit(ref R0XCB, 6);

            GetFshKey();
            for (int i = 0; i < 8; i++)
                sout[i] = RAMBUF[0x8b+i];
            return sout;
        }
    }
    public class lcyHashCal
    {
        public byte[] HashCal(byte[] _HashIn,int n)
        {
            byte[] ret;
            ret=HashCal(_HashIn);
            for(int i=1;i<n;i++)
            {
                ret = HashCal(ret);
            }
            return ret;
        }
        public byte[] HashCal(byte[] _HashIn)
        {
            for (int k = 0; k < 8; k++)
                HashIn[k] = _HashIn[k];
            byte i;
	        byte[] Sa=new byte[0x08];
	        byte[] Sb=new byte[0x08];
	        //--
	        F_calsel=false;
	        sub_half();
	        for(i=0;i<8;i++)
	        {
		        Sa[i]=Ht[i];
	        }
	        //--
	        F_calsel=true;
	        sub_half();
	        for(i=0;i<8;i++)
	        {
		        Sb[i]=Ht[i+0x04];
	        }
	        //--
	        sub_add(Sa,Sb);
            byte[] ret = new byte[0x08];
            for (int j = 0; j < 8; j++)
                ret[j] = HashOut[j];
            return ret;
        }
        public void HashOnce()
        {
            HashCal(HashIn);
        }
        public byte[] HashIn=new byte[0x08];
        public byte[] HashOut=new byte[0x08];
        byte[] Ht=new byte[0x10];
        byte[] InTP=new byte[0x08];
        bool F_calsel=false;//false is fisrt,true is second;

        void loadTab()
        {
	        byte[] TAB0=new byte[0x10]{
		        0x45,0x03,0x11,0x4F,0x85,0x4C,0x7A,0x31,
		        0xF6,0x7C,0xCB,0xB8,0x15,0xA4,0xD0,0x42};
	        byte[] TAB1=new byte[0x10]{
		        0x51,0x9B,0x49,0x14,0xB1,0x2E,0x8F,0xB5,
		        0x95,0xDE,0x62,0x7A,0xD8,0x0D,0x44,0x1C};

	        int i;
	        for(i=0;i<0x10;i++)
	        {
		        if(false==F_calsel)
		        {
			        Ht[i]=TAB0[i];
		        }
		        else
		        {
			        Ht[i]=TAB1[i];
		        }
	        }
        }
        void sub_half()
        {
	        byte n,m;
	        loadTab();
	        sub_cal0();//0AFE
	        n=0x00;
	        sub_cal1(n);//0CDF
	        m=1;
	        sub_cal_L_A(m);//0A9C
	        sub_cal0();//0AFE
	        n=0x04;
	        sub_cal1(n);//0CDF
	        sub_Mixed_A();
        //00
	        sub_cal2();//0B4B
	        m=1;
	        sub_cal_R_A(m);//0A95
        //01
	        sub_cal2();//0B4B
	        m=2;
	        sub_cal_R_A(m);//
        //02
	        sub_cal2();//0B4B
	        sub_Mixed_A();
        //03
	        sub_cal2();//0B4B
	        m=3;
	        sub_cal_R_B(m);
        //04
	        sub_cal2();//0B4B
	        m=3;
	        sub_cal_L_A(m);
        //05
	        sub_cal2();//0B4B
	        m=1;
	        sub_cal_L_A(m);
        //06
	        sub_cal2();//0B4B
	        n=0x04;
	        sub_cal1(n);//0CDF
	        m=1;
	        sub_cal_R_A(m);//
        //07
	        sub_cal2();//0B4B
	        m=1;
	        sub_cal_R_B(m);
        //08
	        sub_cal2();//0B4B
	        m=1;
	        sub_cal_R_A(m);//
        //09
	        sub_cal2();//0B4B
	        m=4;
	        sub_cal_L_A(m);
        //0A
	        sub_cal2();//0B4B
	        m=1;
	        sub_cal_R_B(m);
        //0B
	        sub_cal2();//0B4B
	        m=1;
	        sub_cal_L_A(m);
        //0C
	        sub_cal2();//0B4B
	        n=0x00;
	        sub_cal1(n);//0CDF
	        m=1;
	        sub_cal_R_A(m);//
        //0D
	        sub_cal2();//0B4B
	        m=3;
	        sub_cal_L_A(m);
        //0E
	        sub_cal2();//0B4B
	        m=3;
	        sub_cal_R_B(m);//
        //0F
	        sub_cal2();//0B4B
	        m=4;
	        sub_cal_L_A(m);
        //10
	        sub_cal3();//0BD9
	        m=3;
	        sub_cal_L_A(m);
        //11
	        sub_cal3();//0BD9
	        m=3;
	        sub_cal_R_B(m);
        //12
	        sub_cal3();//0BD9
	        m=2;
	        sub_cal_R_B(m);
        //13
	        sub_cal3();//0BD9
	        m=1;
	        sub_cal_R_A(m);
        //14
	        sub_cal3();//0BD9
	        m=2;
	        sub_cal_R_B(m);
        //15
	        sub_cal3();//0BD9
	        n=0x04;
	        sub_cal1(n);//0CDF
	        m=1;
	        sub_cal_L_A(m);
        //16
	        sub_cal3();//0BD9
	        m=3;
	        sub_cal_R_B(m);
        //17
	        sub_cal3();//0BD9
	        m=1;
	        sub_cal_R_B(m);
        //18
	        sub_cal3();//0BD9
	        n=0x00;
	        sub_cal1(n);//0CDF
	        m=2;
	        sub_cal_R_A(m);
        //19
	        sub_cal3();//0BD9
	        sub_Mixed_A();
        //1A
	        sub_cal3();//0BD9
	        m=3;
	        sub_cal_R_B(m);
        //1B
	        sub_cal3();//0BD9
	        m=2;
	        sub_cal_R_A(m);
        //1C
	        sub_cal3();//0BD9
	        m=4;
	        sub_cal_L_A(m);
        //1D
	        sub_cal3();//0BD9
	        m=3;
	        sub_cal_R_A(m);
        //1E
	        sub_cal3();//0BD9
	        m=1;
	        sub_cal_R_A(m);
        //1F
	        sub_cal3();//0BD9
	        m=3;
	        sub_cal_R_A(m);
        }       
        void sub_add_4B(byte[] p)
        {
	        byte i,C;
	        uint Sum;
	        C=0;
	        for(i=0;i<0x04;i++)
	        {
		        Sum=(uint)(Ht[0x0B-i]+p[0x03-i]+C);
		        Ht[0x0B-i]=(byte)(Ht[0x0B-i]+p[0x03-i]+C);
		        if(Sum>0xFF)
		        {
			        C=0x01;
		        }
		        else
		        {
			        C=0x00;
		        }
	        }
        }
        /*---------------------------------------------------------------------*/
        void sub_Shift_L(byte N)//N is cnt
        {
	        byte C,Ct,i,j;
	        for(i=0;i<N;i++)
	        {
		        C=(byte)(Ht[0x08]&0x80);
		        for(j=0;j<0x04;j++)
		        {
			        Ct=(byte)(Ht[0x0B-j]&0x80);
			        Ht[0x0B-j]=(byte)(Ht[0x0B-j]<<1);
			        if(0x00==C)
			        {
				        Ht[0x0B-j]=(byte)(Ht[0x0B-j]&0xFE);
			        }
			        else
			        {
				        Ht[0x0B-j]=(byte)(Ht[0x0B-j]|0x01);
			        }
			        C=Ct;
		        }
	        }
        }
        /*---------------------------------------------------------------------*/
        void sub_Shift_R(byte N)//N is cnt
        {
	        byte C,Ct,i,j;
	        for(i=0;i<N;i++)
	        {
		        C=(byte)(Ht[0x0B]&0x01);
		        for(j=0;j<0x04;j++)
		        {
			        Ct=(byte)(Ht[0x08+j]&0x01);
			        Ht[0x08+j]=(byte)(Ht[0x08+j]>>1);
			        if(0x00==C)
			        {
				        Ht[0x08+j]=(byte)(Ht[0x08+j]&0x7F);
			        }
			        else
			        {
				        Ht[0x08+j]=(byte)(Ht[0x08+j]|0x80);
			        }
			        C=Ct;
		        }
	        }
        }
        /*---------------------------------------------------------------------*/
        void sub_Mixed_A()//
        {
	        //              +0   +1   +2   +3   +4   +5   +6   +7   +8   +9   +A   +B   +C   +D   +E   +F
	        byte[] Ta=new byte[0x10]{0x04,0x05,0x06,0x07,0x08,0x09,0x0A,0x0B,0x0F,0x0C,0x0D,0x0E,0x00,0x01,0x02,0x03};
	        byte[] tmp=new byte[0x10];
	        byte i,j;
	        for(i=0;i<0x10;i++)
	        {
		        j=Ta[i];
		        tmp[j]=Ht[i];
	        }
	        //
	        for(i=0;i<0x10;i++)
	        {
		        Ht[i]=tmp[i];
	        }
        }
        /*---------------------------------------------------------------------*/
        void sub_Mixed_B()//
        {
	        //                      +0   +1   +2   +3   +4   +5   +6   +7   +8   +9   +A   +B   +C   +D   +E   +F
	        byte[] Tb=new byte[0x10]{0x04,0x05,0x06,0x07,0x08,0x09,0x0A,0x0B,0x0E,0x0F,0x0C,0x0D,0x00,0x01,0x02,0x03};
	        byte[] tmp=new byte[0x10];
	        byte i,j;

	        for(i=0;i<0x10;i++)
	        {
		        j=Tb[i];
		        tmp[j]=Ht[i];
	        }
	        //
	        for(i=0;i<0x10;i++)
	        {
		        Ht[i]=tmp[i];
	        }
        }
        /*---------------------------------------------------------------------*/
        void sub_cal0()//0AFE
        {
	        byte i;
	        byte[] Biao_a=new byte[0x04]{0x50,0xA2,0x8B,0xE6};
	        byte[] TP=new byte[0x04];
	        for(i=0;i<0x04;i++)
	        {
		        TP[i]=(byte)(((Ht[0x00+i]^Ht[0x04+i])&Ht[0x0C+i])^Ht[0x04+i]);
	        }
	        //--
	        sub_add_4B(TP);
	        if(true==F_calsel)
	        {
		        sub_add_4B(Biao_a);
	        }
        }
        /*---------------------------------------------------------------------*/
        void sub_cal1(byte s)//0CDF
        {
	        byte i;
	        byte[] T=new byte[0x04];
	        s=(byte)(s&0x04);
	        for(i=0;i<0x04;i++)
	        {
		        T[0x03-i]=HashIn[i+s];
	        }
	        sub_add_4B(T);
        }
        /*---------------------------------------------------------------------*/
        void sub_cal2()//0B4B
        {
	        byte i;
	        byte[] Biao_b=new byte[0x04]{0x5A,0x82,0x79,0x99};
	        byte[] TP=new byte[0x04];
	        byte m,n;
	        for(i=0;i<0x04;i++)
	        {
		        m=(byte)(Ht[0x00+i]&Ht[0x04+i]);
		        n=(byte)((Ht[0x00+i]|Ht[0x04+i])&Ht[0x0C+i]);
		        TP[i]=(byte)(m|n);
	        }
	        //--
	        sub_add_4B(TP);
	        if(false==F_calsel)
	        {
		        sub_add_4B(Biao_b);
	        }
        }
        /*---------------------------------------------------------------------*/
        void sub_cal3()//0BD9
        {
	        byte i;
	        byte[] Biao_c=new byte[0x04]{0x6E,0xD9,0xEB,0xA1};
	        byte[] Biao_d=new byte[0x04]{0x5C,0x4D,0xD1,0x24};
	        byte[] TP=new byte[0x04];

	        for(i=0;i<0x04;i++)
	        {
		        TP[i]=(byte)(Ht[0x00+i]^Ht[0x04+i]^Ht[0x0C+i]);
	        }
	        //--
	        sub_add_4B(TP);
	        if(false==F_calsel)
	        {
		        sub_add_4B(Biao_c);
	        }
	        else
	        {
		        sub_add_4B(Biao_d);
	        }
        }
        /*---------------------------------------------------------------------*/
        void sub_cal_L_A(byte x)//
        {
	        sub_Shift_L(x);
	        sub_Mixed_A();
        }
        /*---------------------------------------------------------------------*/
        void sub_cal_L_B(byte x)//
        {
	        sub_Shift_L(x);
	        sub_Mixed_B();
        }
        /*---------------------------------------------------------------------*/
        void sub_cal_R_A(byte x)//
        {
	        sub_Shift_R(x);
	        sub_Mixed_A();
        }
        /*---------------------------------------------------------------------*/
        void sub_cal_R_B(byte x)//
        {
	        sub_Shift_R(x);
	        sub_Mixed_B();
        }
        void sub_add(byte[] p,byte[] s)
        {
	        byte i,C;
	        uint Sum;
	        byte[] B=new byte[0x08]{0x89,0xAB,0xCD,0xEF,0xFE,0xDC,0xBA,0x98};
	        //--
	        C=0;
	        for(i=0;i<0x04;i++)
	        {
		        Sum=(uint)(p[0x03-i]+s[0x03-i]+C);
		        HashOut[0x00+i]=(byte)(p[0x03-i]+s[0x03-i]+C);
		        if(Sum>0xFF)
		        {
			        C=0x01;
		        }
		        else
		        {
			        C=0x00;
		        }
	        }
	        //
	        C=0;
	        for(i=0;i<0x04;i++)
	        {
		        Sum=(uint)(HashOut[0x00+i]+B[i]+C);
		        HashOut[0x00+i]=(byte)(HashOut[0x00+i]+B[i]+C);
		        if(Sum>0xFF)
		        {
			        C=0x01;
		        }
		        else
		        {
			        C=0x00;
		        }
	        }
	        //--
	        C=0;
	        for(i=0;i<0x04;i++)
	        {
		        Sum=(uint)(p[0x07-i]+s[0x07-i]+C);
		        HashOut[0x04+i]=(byte)(p[0x07-i]+s[0x07-i]+C);
		        if(Sum>0xFF)
		        {
			        C=0x01;
		        }
		        else
		        {
			        C=0x00;
		        }
	        }
	        //
	        C=0;
	        for(i=0;i<0x04;i++)
	        {
		        Sum=(uint)(HashOut[0x04+i]+B[0x04+i]+C);
		        HashOut[0x04+i]=(byte)(HashOut[0x04+i]+B[0x04+i]+C);
		        if(Sum>0xFF)
		        {
			        C=0x01;
		        }
		        else
		        {
			        C=0x00;
		        }
	        }
	        //--
        }
    }
}
