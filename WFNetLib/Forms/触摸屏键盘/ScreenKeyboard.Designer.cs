using System.Windows.Forms;

namespace WFNetLib.Forms
{
    partial class ScreenKeyboard {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing) {
                foreach (Control ctrl in this.Controls) {
                    KeyboardButton button = ctrl as KeyboardButton;
                    if (button != null) {
                        button.Click -= ButtonOnClick;
                    }
                }

                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenKeyboard));
            this.btnRA = new WFNetLib.Forms.KeyboardButton();
            this.btnUP = new WFNetLib.Forms.KeyboardButton();
            this.btnDN = new WFNetLib.Forms.KeyboardButton();
            this.btnLA = new WFNetLib.Forms.KeyboardButton();
            this.btnF12 = new WFNetLib.Forms.KeyboardButton();
            this.btnF11 = new WFNetLib.Forms.KeyboardButton();
            this.btnF10 = new WFNetLib.Forms.KeyboardButton();
            this.btnF9 = new WFNetLib.Forms.KeyboardButton();
            this.btnF8 = new WFNetLib.Forms.KeyboardButton();
            this.btnF7 = new WFNetLib.Forms.KeyboardButton();
            this.btnF6 = new WFNetLib.Forms.KeyboardButton();
            this.btnF5 = new WFNetLib.Forms.KeyboardButton();
            this.btnF4 = new WFNetLib.Forms.KeyboardButton();
            this.btnF3 = new WFNetLib.Forms.KeyboardButton();
            this.btnF2 = new WFNetLib.Forms.KeyboardButton();
            this.btnF1 = new WFNetLib.Forms.KeyboardButton();
            this.btnPath = new WFNetLib.Forms.KeyboardButton();
            this.btnEqual = new WFNetLib.Forms.KeyboardButton();
            this.btnLCTRL = new WFNetLib.Forms.KeyboardButton();
            this.btnLSHFT = new WFNetLib.Forms.KeyboardButton();
            this.btnLOCK = new WFNetLib.Forms.KeyboardButton();
            this.btnTAB = new WFNetLib.Forms.KeyboardButton();
            this.btnBKSP = new WFNetLib.Forms.KeyboardButton();
            this.btnRSHFT = new WFNetLib.Forms.KeyboardButton();
            this.btnENT = new WFNetLib.Forms.KeyboardButton();
            this.btnRBracket = new WFNetLib.Forms.KeyboardButton();
            this.btnMinus = new WFNetLib.Forms.KeyboardButton();
            this.btnFullStop = new WFNetLib.Forms.KeyboardButton();
            this.btnL = new WFNetLib.Forms.KeyboardButton();
            this.btnO = new WFNetLib.Forms.KeyboardButton();
            this.btn8 = new WFNetLib.Forms.KeyboardButton();
            this.btnMU = new WFNetLib.Forms.KeyboardButton();
            this.btnN = new WFNetLib.Forms.KeyboardButton();
            this.btnH = new WFNetLib.Forms.KeyboardButton();
            this.btnY = new WFNetLib.Forms.KeyboardButton();
            this.btnSpace = new WFNetLib.Forms.KeyboardButton();
            this.btnC = new WFNetLib.Forms.KeyboardButton();
            this.btnD = new WFNetLib.Forms.KeyboardButton();
            this.btnE = new WFNetLib.Forms.KeyboardButton();
            this.btn5 = new WFNetLib.Forms.KeyboardButton();
            this.btnQute = new WFNetLib.Forms.KeyboardButton();
            this.btnLBracket = new WFNetLib.Forms.KeyboardButton();
            this.btn2 = new WFNetLib.Forms.KeyboardButton();
            this.btnDivide = new WFNetLib.Forms.KeyboardButton();
            this.btnSem = new WFNetLib.Forms.KeyboardButton();
            this.btnP = new WFNetLib.Forms.KeyboardButton();
            this.btn0 = new WFNetLib.Forms.KeyboardButton();
            this.btnComma = new WFNetLib.Forms.KeyboardButton();
            this.btnK = new WFNetLib.Forms.KeyboardButton();
            this.btnI = new WFNetLib.Forms.KeyboardButton();
            this.btn9 = new WFNetLib.Forms.KeyboardButton();
            this.btnRCTRL = new WFNetLib.Forms.KeyboardButton();
            this.btnM = new WFNetLib.Forms.KeyboardButton();
            this.btnJ = new WFNetLib.Forms.KeyboardButton();
            this.btnU = new WFNetLib.Forms.KeyboardButton();
            this.btn7 = new WFNetLib.Forms.KeyboardButton();
            this.btnRW = new WFNetLib.Forms.KeyboardButton();
            this.btnB = new WFNetLib.Forms.KeyboardButton();
            this.btnG = new WFNetLib.Forms.KeyboardButton();
            this.btnRALT = new WFNetLib.Forms.KeyboardButton();
            this.btnV = new WFNetLib.Forms.KeyboardButton();
            this.btnT = new WFNetLib.Forms.KeyboardButton();
            this.btnF = new WFNetLib.Forms.KeyboardButton();
            this.btnLALT = new WFNetLib.Forms.KeyboardButton();
            this.btn6 = new WFNetLib.Forms.KeyboardButton();
            this.btnX = new WFNetLib.Forms.KeyboardButton();
            this.btnR = new WFNetLib.Forms.KeyboardButton();
            this.btnS = new WFNetLib.Forms.KeyboardButton();
            this.btnLW = new WFNetLib.Forms.KeyboardButton();
            this.btn4 = new WFNetLib.Forms.KeyboardButton();
            this.btnZ = new WFNetLib.Forms.KeyboardButton();
            this.btnW = new WFNetLib.Forms.KeyboardButton();
            this.btnA = new WFNetLib.Forms.KeyboardButton();
            this.btn3 = new WFNetLib.Forms.KeyboardButton();
            this.btnQ = new WFNetLib.Forms.KeyboardButton();
            this.btn1 = new WFNetLib.Forms.KeyboardButton();
            this.btnWave = new WFNetLib.Forms.KeyboardButton();
            this.btnESC = new WFNetLib.Forms.KeyboardButton();
            this.keyboardButton1 = new WFNetLib.Forms.KeyboardButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRA
            // 
            this.btnRA.AntiAlias = true;
            this.btnRA.BackColor = System.Drawing.SystemColors.Control;
            this.btnRA.Checked = false;
            this.btnRA.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnRA.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnRA.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnRA.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRA.Location = new System.Drawing.Point(730, 187);
            this.btnRA.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnRA.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnRA.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnRA.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnRA.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnRA.Name = "btnRA";
            this.btnRA.ShowFocusRectangle = false;
            this.btnRA.Size = new System.Drawing.Size(51, 57);
            this.btnRA.TabIndex = 110;
            this.btnRA.TabStop = false;
            this.btnRA.Text = "→";
            this.btnRA.UseCompatibleTextRendering = true;
            this.btnRA.UseMnemonic = false;
            this.btnRA.UseVisualStyleBackColor = false;
            this.btnRA.VKCode = ((short)(0));
            // 
            // btnUP
            // 
            this.btnUP.AntiAlias = true;
            this.btnUP.BackColor = System.Drawing.SystemColors.Control;
            this.btnUP.Checked = false;
            this.btnUP.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnUP.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnUP.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnUP.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUP.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUP.Location = new System.Drawing.Point(679, 136);
            this.btnUP.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnUP.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnUP.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnUP.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnUP.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnUP.Name = "btnUP";
            this.btnUP.ShowFocusRectangle = false;
            this.btnUP.Size = new System.Drawing.Size(51, 51);
            this.btnUP.TabIndex = 95;
            this.btnUP.TabStop = false;
            this.btnUP.Text = "↑";
            this.btnUP.UseCompatibleTextRendering = true;
            this.btnUP.UseMnemonic = false;
            this.btnUP.UseVisualStyleBackColor = false;
            this.btnUP.VKCode = ((short)(0));
            // 
            // btnDN
            // 
            this.btnDN.AntiAlias = true;
            this.btnDN.BackColor = System.Drawing.SystemColors.Control;
            this.btnDN.Checked = false;
            this.btnDN.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnDN.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnDN.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnDN.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDN.Location = new System.Drawing.Point(679, 187);
            this.btnDN.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnDN.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnDN.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnDN.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnDN.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnDN.Name = "btnDN";
            this.btnDN.ShowFocusRectangle = false;
            this.btnDN.Size = new System.Drawing.Size(51, 57);
            this.btnDN.TabIndex = 109;
            this.btnDN.TabStop = false;
            this.btnDN.Text = "↓";
            this.btnDN.UseCompatibleTextRendering = true;
            this.btnDN.UseMnemonic = false;
            this.btnDN.UseVisualStyleBackColor = false;
            this.btnDN.VKCode = ((short)(0));
            // 
            // btnLA
            // 
            this.btnLA.AntiAlias = true;
            this.btnLA.BackColor = System.Drawing.SystemColors.Control;
            this.btnLA.Checked = false;
            this.btnLA.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnLA.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnLA.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnLA.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLA.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLA.Location = new System.Drawing.Point(626, 187);
            this.btnLA.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnLA.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnLA.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnLA.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnLA.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnLA.Name = "btnLA";
            this.btnLA.ShowFocusRectangle = false;
            this.btnLA.Size = new System.Drawing.Size(51, 57);
            this.btnLA.TabIndex = 108;
            this.btnLA.TabStop = false;
            this.btnLA.Text = "←";
            this.btnLA.UseCompatibleTextRendering = true;
            this.btnLA.UseMnemonic = false;
            this.btnLA.UseVisualStyleBackColor = false;
            this.btnLA.VKCode = ((short)(0));
            // 
            // btnF12
            // 
            this.btnF12.AntiAlias = true;
            this.btnF12.BackColor = System.Drawing.SystemColors.Control;
            this.btnF12.Checked = false;
            this.btnF12.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF12.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF12.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF12.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF12.Location = new System.Drawing.Point(613, 0);
            this.btnF12.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF12.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF12.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF12.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF12.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF12.Name = "btnF12";
            this.btnF12.ShowFocusRectangle = false;
            this.btnF12.Size = new System.Drawing.Size(51, 38);
            this.btnF12.TabIndex = 18;
            this.btnF12.TabStop = false;
            this.btnF12.Text = "F12";
            this.btnF12.UseCompatibleTextRendering = true;
            this.btnF12.UseMnemonic = false;
            this.btnF12.UseVisualStyleBackColor = false;
            this.btnF12.VKCode = ((short)(0));
            // 
            // btnF11
            // 
            this.btnF11.AntiAlias = true;
            this.btnF11.BackColor = System.Drawing.SystemColors.Control;
            this.btnF11.Checked = false;
            this.btnF11.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF11.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF11.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF11.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF11.Location = new System.Drawing.Point(562, 0);
            this.btnF11.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF11.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF11.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF11.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF11.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF11.Name = "btnF11";
            this.btnF11.ShowFocusRectangle = false;
            this.btnF11.Size = new System.Drawing.Size(51, 38);
            this.btnF11.TabIndex = 17;
            this.btnF11.TabStop = false;
            this.btnF11.Text = "F11";
            this.btnF11.UseCompatibleTextRendering = true;
            this.btnF11.UseMnemonic = false;
            this.btnF11.UseVisualStyleBackColor = false;
            this.btnF11.VKCode = ((short)(0));
            // 
            // btnF10
            // 
            this.btnF10.AntiAlias = true;
            this.btnF10.BackColor = System.Drawing.SystemColors.Control;
            this.btnF10.Checked = false;
            this.btnF10.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF10.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF10.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF10.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF10.Location = new System.Drawing.Point(511, 0);
            this.btnF10.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF10.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF10.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF10.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF10.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF10.Name = "btnF10";
            this.btnF10.ShowFocusRectangle = false;
            this.btnF10.Size = new System.Drawing.Size(51, 38);
            this.btnF10.TabIndex = 16;
            this.btnF10.TabStop = false;
            this.btnF10.Text = "F10";
            this.btnF10.UseCompatibleTextRendering = true;
            this.btnF10.UseMnemonic = false;
            this.btnF10.UseVisualStyleBackColor = false;
            this.btnF10.VKCode = ((short)(0));
            // 
            // btnF9
            // 
            this.btnF9.AntiAlias = true;
            this.btnF9.BackColor = System.Drawing.SystemColors.Control;
            this.btnF9.Checked = false;
            this.btnF9.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF9.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF9.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF9.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF9.Location = new System.Drawing.Point(460, 0);
            this.btnF9.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF9.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF9.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF9.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF9.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF9.Name = "btnF9";
            this.btnF9.ShowFocusRectangle = false;
            this.btnF9.Size = new System.Drawing.Size(51, 38);
            this.btnF9.TabIndex = 15;
            this.btnF9.TabStop = false;
            this.btnF9.Text = "F9";
            this.btnF9.UseCompatibleTextRendering = true;
            this.btnF9.UseMnemonic = false;
            this.btnF9.UseVisualStyleBackColor = false;
            this.btnF9.VKCode = ((short)(0));
            // 
            // btnF8
            // 
            this.btnF8.AntiAlias = true;
            this.btnF8.BackColor = System.Drawing.SystemColors.Control;
            this.btnF8.Checked = false;
            this.btnF8.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF8.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF8.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF8.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF8.Location = new System.Drawing.Point(409, 0);
            this.btnF8.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF8.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF8.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF8.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF8.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF8.Name = "btnF8";
            this.btnF8.ShowFocusRectangle = false;
            this.btnF8.Size = new System.Drawing.Size(51, 38);
            this.btnF8.TabIndex = 14;
            this.btnF8.TabStop = false;
            this.btnF8.Text = "F8";
            this.btnF8.UseCompatibleTextRendering = true;
            this.btnF8.UseMnemonic = false;
            this.btnF8.UseVisualStyleBackColor = false;
            this.btnF8.VKCode = ((short)(0));
            // 
            // btnF7
            // 
            this.btnF7.AntiAlias = true;
            this.btnF7.BackColor = System.Drawing.SystemColors.Control;
            this.btnF7.Checked = false;
            this.btnF7.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF7.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF7.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF7.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF7.Location = new System.Drawing.Point(358, 0);
            this.btnF7.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF7.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF7.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF7.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF7.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF7.Name = "btnF7";
            this.btnF7.ShowFocusRectangle = false;
            this.btnF7.Size = new System.Drawing.Size(51, 38);
            this.btnF7.TabIndex = 13;
            this.btnF7.TabStop = false;
            this.btnF7.Text = "F7";
            this.btnF7.UseCompatibleTextRendering = true;
            this.btnF7.UseMnemonic = false;
            this.btnF7.UseVisualStyleBackColor = false;
            this.btnF7.VKCode = ((short)(0));
            // 
            // btnF6
            // 
            this.btnF6.AntiAlias = true;
            this.btnF6.BackColor = System.Drawing.SystemColors.Control;
            this.btnF6.Checked = false;
            this.btnF6.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF6.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF6.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF6.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF6.Location = new System.Drawing.Point(307, 0);
            this.btnF6.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF6.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF6.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF6.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF6.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF6.Name = "btnF6";
            this.btnF6.ShowFocusRectangle = false;
            this.btnF6.Size = new System.Drawing.Size(51, 38);
            this.btnF6.TabIndex = 12;
            this.btnF6.TabStop = false;
            this.btnF6.Text = "F6";
            this.btnF6.UseCompatibleTextRendering = true;
            this.btnF6.UseMnemonic = false;
            this.btnF6.UseVisualStyleBackColor = false;
            this.btnF6.VKCode = ((short)(0));
            // 
            // btnF5
            // 
            this.btnF5.AntiAlias = true;
            this.btnF5.BackColor = System.Drawing.SystemColors.Control;
            this.btnF5.Checked = false;
            this.btnF5.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF5.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF5.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF5.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF5.Location = new System.Drawing.Point(256, 0);
            this.btnF5.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF5.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF5.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF5.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF5.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF5.Name = "btnF5";
            this.btnF5.ShowFocusRectangle = false;
            this.btnF5.Size = new System.Drawing.Size(51, 38);
            this.btnF5.TabIndex = 11;
            this.btnF5.TabStop = false;
            this.btnF5.Text = "F5";
            this.btnF5.UseCompatibleTextRendering = true;
            this.btnF5.UseMnemonic = false;
            this.btnF5.UseVisualStyleBackColor = false;
            this.btnF5.VKCode = ((short)(0));
            // 
            // btnF4
            // 
            this.btnF4.AntiAlias = true;
            this.btnF4.BackColor = System.Drawing.SystemColors.Control;
            this.btnF4.Checked = false;
            this.btnF4.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF4.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF4.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF4.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF4.Location = new System.Drawing.Point(205, 0);
            this.btnF4.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF4.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF4.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF4.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF4.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF4.Name = "btnF4";
            this.btnF4.ShowFocusRectangle = false;
            this.btnF4.Size = new System.Drawing.Size(51, 38);
            this.btnF4.TabIndex = 10;
            this.btnF4.TabStop = false;
            this.btnF4.Text = "F4";
            this.btnF4.UseCompatibleTextRendering = true;
            this.btnF4.UseMnemonic = false;
            this.btnF4.UseVisualStyleBackColor = false;
            this.btnF4.VKCode = ((short)(0));
            // 
            // btnF3
            // 
            this.btnF3.AntiAlias = true;
            this.btnF3.BackColor = System.Drawing.SystemColors.Control;
            this.btnF3.Checked = false;
            this.btnF3.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF3.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF3.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF3.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF3.Location = new System.Drawing.Point(154, 0);
            this.btnF3.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF3.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF3.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF3.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF3.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF3.Name = "btnF3";
            this.btnF3.ShowFocusRectangle = false;
            this.btnF3.Size = new System.Drawing.Size(51, 38);
            this.btnF3.TabIndex = 9;
            this.btnF3.TabStop = false;
            this.btnF3.Text = "F3";
            this.btnF3.UseCompatibleTextRendering = true;
            this.btnF3.UseMnemonic = false;
            this.btnF3.UseVisualStyleBackColor = false;
            this.btnF3.VKCode = ((short)(0));
            // 
            // btnF2
            // 
            this.btnF2.AntiAlias = true;
            this.btnF2.BackColor = System.Drawing.SystemColors.Control;
            this.btnF2.Checked = false;
            this.btnF2.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF2.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF2.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF2.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF2.Location = new System.Drawing.Point(103, 0);
            this.btnF2.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF2.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF2.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF2.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF2.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF2.Name = "btnF2";
            this.btnF2.ShowFocusRectangle = false;
            this.btnF2.Size = new System.Drawing.Size(51, 38);
            this.btnF2.TabIndex = 8;
            this.btnF2.TabStop = false;
            this.btnF2.Text = "F2";
            this.btnF2.UseCompatibleTextRendering = true;
            this.btnF2.UseMnemonic = false;
            this.btnF2.UseVisualStyleBackColor = false;
            this.btnF2.VKCode = ((short)(0));
            // 
            // btnF1
            // 
            this.btnF1.AntiAlias = true;
            this.btnF1.BackColor = System.Drawing.SystemColors.Control;
            this.btnF1.Checked = false;
            this.btnF1.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF1.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF1.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF1.Location = new System.Drawing.Point(52, 0);
            this.btnF1.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF1.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF1.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF1.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF1.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF1.Name = "btnF1";
            this.btnF1.ShowFocusRectangle = false;
            this.btnF1.Size = new System.Drawing.Size(51, 38);
            this.btnF1.TabIndex = 7;
            this.btnF1.TabStop = false;
            this.btnF1.Text = "F1";
            this.btnF1.UseCompatibleTextRendering = true;
            this.btnF1.UseMnemonic = false;
            this.btnF1.UseVisualStyleBackColor = false;
            this.btnF1.VKCode = ((short)(0));
            // 
            // btnPath
            // 
            this.btnPath.AntiAlias = true;
            this.btnPath.BackColor = System.Drawing.SystemColors.Control;
            this.btnPath.Checked = false;
            this.btnPath.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnPath.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnPath.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnPath.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPath.Location = new System.Drawing.Point(474, 97);
            this.btnPath.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnPath.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnPath.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnPath.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnPath.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnPath.Name = "btnPath";
            this.btnPath.ShowFocusRectangle = false;
            this.btnPath.Size = new System.Drawing.Size(52, 30);
            this.btnPath.TabIndex = 59;
            this.btnPath.TabStop = false;
            this.btnPath.Text = "\\";
            this.btnPath.UseCompatibleTextRendering = true;
            this.btnPath.UseMnemonic = false;
            this.btnPath.UseVisualStyleBackColor = false;
            this.btnPath.VKCode = ((short)(0));
            // 
            // btnEqual
            // 
            this.btnEqual.AntiAlias = true;
            this.btnEqual.BackColor = System.Drawing.SystemColors.Control;
            this.btnEqual.Checked = false;
            this.btnEqual.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnEqual.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnEqual.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnEqual.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEqual.Location = new System.Drawing.Point(613, 40);
            this.btnEqual.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnEqual.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnEqual.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnEqual.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnEqual.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.ShowFocusRectangle = false;
            this.btnEqual.Size = new System.Drawing.Size(51, 30);
            this.btnEqual.TabIndex = 37;
            this.btnEqual.TabStop = false;
            this.btnEqual.Text = "=";
            this.btnEqual.UseCompatibleTextRendering = true;
            this.btnEqual.UseMnemonic = false;
            this.btnEqual.UseVisualStyleBackColor = false;
            this.btnEqual.VKCode = ((short)(0));
            // 
            // btnLCTRL
            // 
            this.btnLCTRL.AntiAlias = true;
            this.btnLCTRL.BackColor = System.Drawing.SystemColors.Control;
            this.btnLCTRL.Checked = false;
            this.btnLCTRL.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnLCTRL.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnLCTRL.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnLCTRL.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLCTRL.Location = new System.Drawing.Point(2, 187);
            this.btnLCTRL.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnLCTRL.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnLCTRL.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnLCTRL.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnLCTRL.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnLCTRL.Name = "btnLCTRL";
            this.btnLCTRL.ShowFocusRectangle = false;
            this.btnLCTRL.Size = new System.Drawing.Size(50, 30);
            this.btnLCTRL.TabIndex = 100;
            this.btnLCTRL.TabStop = false;
            this.btnLCTRL.Text = "ctrl";
            this.btnLCTRL.UseCompatibleTextRendering = true;
            this.btnLCTRL.UseMnemonic = false;
            this.btnLCTRL.UseVisualStyleBackColor = false;
            this.btnLCTRL.VKCode = ((short)(0));
            // 
            // btnLSHFT
            // 
            this.btnLSHFT.AntiAlias = true;
            this.btnLSHFT.BackColor = System.Drawing.SystemColors.Control;
            this.btnLSHFT.Checked = false;
            this.btnLSHFT.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnLSHFT.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnLSHFT.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnLSHFT.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLSHFT.Location = new System.Drawing.Point(2, 157);
            this.btnLSHFT.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnLSHFT.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnLSHFT.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnLSHFT.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnLSHFT.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnLSHFT.Name = "btnLSHFT";
            this.btnLSHFT.ShowFocusRectangle = false;
            this.btnLSHFT.Size = new System.Drawing.Size(86, 30);
            this.btnLSHFT.TabIndex = 83;
            this.btnLSHFT.TabStop = false;
            this.btnLSHFT.Text = "shft";
            this.btnLSHFT.UseCompatibleTextRendering = true;
            this.btnLSHFT.UseMnemonic = false;
            this.btnLSHFT.UseVisualStyleBackColor = false;
            this.btnLSHFT.VKCode = ((short)(0));
            // 
            // btnLOCK
            // 
            this.btnLOCK.AntiAlias = true;
            this.btnLOCK.BackColor = System.Drawing.SystemColors.Control;
            this.btnLOCK.Checked = false;
            this.btnLOCK.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnLOCK.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnLOCK.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnLOCK.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLOCK.Location = new System.Drawing.Point(2, 127);
            this.btnLOCK.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnLOCK.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnLOCK.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnLOCK.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnLOCK.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnLOCK.Name = "btnLOCK";
            this.btnLOCK.ShowFocusRectangle = false;
            this.btnLOCK.Size = new System.Drawing.Size(69, 30);
            this.btnLOCK.TabIndex = 67;
            this.btnLOCK.TabStop = false;
            this.btnLOCK.Text = "lock";
            this.btnLOCK.UseCompatibleTextRendering = true;
            this.btnLOCK.UseMnemonic = false;
            this.btnLOCK.UseVisualStyleBackColor = false;
            this.btnLOCK.VKCode = ((short)(0));
            // 
            // btnTAB
            // 
            this.btnTAB.AntiAlias = true;
            this.btnTAB.BackColor = System.Drawing.SystemColors.Control;
            this.btnTAB.Checked = false;
            this.btnTAB.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnTAB.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnTAB.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnTAB.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTAB.Location = new System.Drawing.Point(2, 97);
            this.btnTAB.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnTAB.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnTAB.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnTAB.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnTAB.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnTAB.Name = "btnTAB";
            this.btnTAB.ShowFocusRectangle = false;
            this.btnTAB.Size = new System.Drawing.Size(52, 30);
            this.btnTAB.TabIndex = 46;
            this.btnTAB.TabStop = false;
            this.btnTAB.Text = "tab";
            this.btnTAB.UseCompatibleTextRendering = true;
            this.btnTAB.UseMnemonic = false;
            this.btnTAB.UseVisualStyleBackColor = false;
            this.btnTAB.VKCode = ((short)(0));
            // 
            // btnBKSP
            // 
            this.btnBKSP.AntiAlias = true;
            this.btnBKSP.BackColor = System.Drawing.SystemColors.Control;
            this.btnBKSP.Checked = false;
            this.btnBKSP.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnBKSP.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnBKSP.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnBKSP.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBKSP.Location = new System.Drawing.Point(664, 40);
            this.btnBKSP.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnBKSP.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnBKSP.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnBKSP.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnBKSP.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnBKSP.Name = "btnBKSP";
            this.btnBKSP.ShowFocusRectangle = false;
            this.btnBKSP.Size = new System.Drawing.Size(51, 30);
            this.btnBKSP.TabIndex = 38;
            this.btnBKSP.TabStop = false;
            this.btnBKSP.Text = "bksp";
            this.btnBKSP.UseCompatibleTextRendering = true;
            this.btnBKSP.UseMnemonic = false;
            this.btnBKSP.UseVisualStyleBackColor = false;
            this.btnBKSP.VKCode = ((short)(0));
            // 
            // btnRSHFT
            // 
            this.btnRSHFT.AntiAlias = true;
            this.btnRSHFT.BackColor = System.Drawing.SystemColors.Control;
            this.btnRSHFT.Checked = false;
            this.btnRSHFT.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnRSHFT.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnRSHFT.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnRSHFT.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRSHFT.Location = new System.Drawing.Point(440, 157);
            this.btnRSHFT.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnRSHFT.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnRSHFT.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnRSHFT.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnRSHFT.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnRSHFT.Name = "btnRSHFT";
            this.btnRSHFT.ShowFocusRectangle = false;
            this.btnRSHFT.Size = new System.Drawing.Size(86, 30);
            this.btnRSHFT.TabIndex = 94;
            this.btnRSHFT.TabStop = false;
            this.btnRSHFT.Text = "shft";
            this.btnRSHFT.UseCompatibleTextRendering = true;
            this.btnRSHFT.UseMnemonic = false;
            this.btnRSHFT.UseVisualStyleBackColor = false;
            this.btnRSHFT.VKCode = ((short)(0));
            // 
            // btnENT
            // 
            this.btnENT.AntiAlias = true;
            this.btnENT.BackColor = System.Drawing.SystemColors.Control;
            this.btnENT.Checked = false;
            this.btnENT.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnENT.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnENT.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnENT.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnENT.Location = new System.Drawing.Point(457, 127);
            this.btnENT.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnENT.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnENT.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnENT.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnENT.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnENT.Name = "btnENT";
            this.btnENT.ShowFocusRectangle = false;
            this.btnENT.Size = new System.Drawing.Size(69, 30);
            this.btnENT.TabIndex = 79;
            this.btnENT.TabStop = false;
            this.btnENT.Text = "ent";
            this.btnENT.UseCompatibleTextRendering = true;
            this.btnENT.UseMnemonic = false;
            this.btnENT.UseVisualStyleBackColor = false;
            this.btnENT.VKCode = ((short)(0));
            // 
            // btnRBracket
            // 
            this.btnRBracket.AntiAlias = true;
            this.btnRBracket.BackColor = System.Drawing.SystemColors.Control;
            this.btnRBracket.Checked = false;
            this.btnRBracket.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnRBracket.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnRBracket.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnRBracket.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRBracket.Location = new System.Drawing.Point(439, 97);
            this.btnRBracket.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnRBracket.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnRBracket.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnRBracket.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnRBracket.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnRBracket.Name = "btnRBracket";
            this.btnRBracket.ShowFocusRectangle = false;
            this.btnRBracket.Size = new System.Drawing.Size(35, 30);
            this.btnRBracket.TabIndex = 58;
            this.btnRBracket.TabStop = false;
            this.btnRBracket.Text = "]";
            this.btnRBracket.UseCompatibleTextRendering = true;
            this.btnRBracket.UseMnemonic = false;
            this.btnRBracket.UseVisualStyleBackColor = false;
            this.btnRBracket.VKCode = ((short)(0));
            // 
            // btnMinus
            // 
            this.btnMinus.AntiAlias = true;
            this.btnMinus.BackColor = System.Drawing.SystemColors.Control;
            this.btnMinus.Checked = false;
            this.btnMinus.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnMinus.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnMinus.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnMinus.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinus.Location = new System.Drawing.Point(562, 40);
            this.btnMinus.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnMinus.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnMinus.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnMinus.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnMinus.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.ShowFocusRectangle = false;
            this.btnMinus.Size = new System.Drawing.Size(51, 30);
            this.btnMinus.TabIndex = 36;
            this.btnMinus.TabStop = false;
            this.btnMinus.Text = "-";
            this.btnMinus.UseCompatibleTextRendering = true;
            this.btnMinus.UseMnemonic = false;
            this.btnMinus.UseVisualStyleBackColor = false;
            this.btnMinus.VKCode = ((short)(0));
            // 
            // btnFullStop
            // 
            this.btnFullStop.AntiAlias = true;
            this.btnFullStop.BackColor = System.Drawing.SystemColors.Control;
            this.btnFullStop.Checked = false;
            this.btnFullStop.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnFullStop.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnFullStop.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnFullStop.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFullStop.Location = new System.Drawing.Point(369, 157);
            this.btnFullStop.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnFullStop.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnFullStop.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnFullStop.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnFullStop.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnFullStop.Name = "btnFullStop";
            this.btnFullStop.ShowFocusRectangle = false;
            this.btnFullStop.Size = new System.Drawing.Size(35, 30);
            this.btnFullStop.TabIndex = 92;
            this.btnFullStop.TabStop = false;
            this.btnFullStop.Text = ".";
            this.btnFullStop.UseCompatibleTextRendering = true;
            this.btnFullStop.UseMnemonic = false;
            this.btnFullStop.UseVisualStyleBackColor = false;
            this.btnFullStop.VKCode = ((short)(0));
            // 
            // btnL
            // 
            this.btnL.AntiAlias = true;
            this.btnL.BackColor = System.Drawing.SystemColors.Control;
            this.btnL.Checked = false;
            this.btnL.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnL.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnL.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnL.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnL.Location = new System.Drawing.Point(351, 127);
            this.btnL.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnL.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnL.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnL.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnL.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnL.Name = "btnL";
            this.btnL.ShowFocusRectangle = false;
            this.btnL.Size = new System.Drawing.Size(35, 30);
            this.btnL.TabIndex = 76;
            this.btnL.TabStop = false;
            this.btnL.Text = "l";
            this.btnL.UseCompatibleTextRendering = true;
            this.btnL.UseMnemonic = false;
            this.btnL.UseVisualStyleBackColor = false;
            this.btnL.VKCode = ((short)(0));
            // 
            // btnO
            // 
            this.btnO.AntiAlias = true;
            this.btnO.BackColor = System.Drawing.SystemColors.Control;
            this.btnO.Checked = false;
            this.btnO.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnO.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnO.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnO.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnO.Location = new System.Drawing.Point(334, 97);
            this.btnO.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnO.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnO.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnO.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnO.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnO.Name = "btnO";
            this.btnO.ShowFocusRectangle = false;
            this.btnO.Size = new System.Drawing.Size(35, 30);
            this.btnO.TabIndex = 55;
            this.btnO.TabStop = false;
            this.btnO.Text = "o";
            this.btnO.UseCompatibleTextRendering = true;
            this.btnO.UseMnemonic = false;
            this.btnO.UseVisualStyleBackColor = false;
            this.btnO.VKCode = ((short)(0));
            // 
            // btn8
            // 
            this.btn8.AntiAlias = true;
            this.btn8.BackColor = System.Drawing.SystemColors.Control;
            this.btn8.Checked = false;
            this.btn8.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btn8.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btn8.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btn8.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn8.Location = new System.Drawing.Point(409, 40);
            this.btn8.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btn8.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btn8.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btn8.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btn8.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btn8.Name = "btn8";
            this.btn8.ShowFocusRectangle = false;
            this.btn8.Size = new System.Drawing.Size(51, 30);
            this.btn8.TabIndex = 33;
            this.btn8.TabStop = false;
            this.btn8.Text = "8";
            this.btn8.UseCompatibleTextRendering = true;
            this.btn8.UseMnemonic = false;
            this.btn8.UseVisualStyleBackColor = false;
            this.btn8.VKCode = ((short)(0));
            // 
            // btnMU
            // 
            this.btnMU.AntiAlias = true;
            this.btnMU.BackColor = System.Drawing.SystemColors.Control;
            this.btnMU.Checked = false;
            this.btnMU.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnMU.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnMU.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnMU.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMU.Location = new System.Drawing.Point(441, 187);
            this.btnMU.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnMU.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnMU.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnMU.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnMU.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnMU.Name = "btnMU";
            this.btnMU.ShowFocusRectangle = false;
            this.btnMU.Size = new System.Drawing.Size(35, 30);
            this.btnMU.TabIndex = 106;
            this.btnMU.TabStop = false;
            this.btnMU.Text = "mu";
            this.btnMU.UseCompatibleTextRendering = true;
            this.btnMU.UseMnemonic = false;
            this.btnMU.UseVisualStyleBackColor = false;
            this.btnMU.VKCode = ((short)(0));
            // 
            // btnN
            // 
            this.btnN.AntiAlias = true;
            this.btnN.BackColor = System.Drawing.SystemColors.Control;
            this.btnN.Checked = false;
            this.btnN.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnN.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnN.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnN.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnN.Location = new System.Drawing.Point(264, 157);
            this.btnN.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnN.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnN.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnN.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnN.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnN.Name = "btnN";
            this.btnN.ShowFocusRectangle = false;
            this.btnN.Size = new System.Drawing.Size(35, 30);
            this.btnN.TabIndex = 89;
            this.btnN.TabStop = false;
            this.btnN.Text = "n";
            this.btnN.UseCompatibleTextRendering = true;
            this.btnN.UseMnemonic = false;
            this.btnN.UseVisualStyleBackColor = false;
            this.btnN.VKCode = ((short)(0));
            // 
            // btnH
            // 
            this.btnH.AntiAlias = true;
            this.btnH.BackColor = System.Drawing.SystemColors.Control;
            this.btnH.Checked = false;
            this.btnH.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnH.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnH.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnH.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnH.Location = new System.Drawing.Point(246, 127);
            this.btnH.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnH.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnH.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnH.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnH.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnH.Name = "btnH";
            this.btnH.ShowFocusRectangle = false;
            this.btnH.Size = new System.Drawing.Size(35, 30);
            this.btnH.TabIndex = 73;
            this.btnH.TabStop = false;
            this.btnH.Text = "h";
            this.btnH.UseCompatibleTextRendering = true;
            this.btnH.UseMnemonic = false;
            this.btnH.UseVisualStyleBackColor = false;
            this.btnH.VKCode = ((short)(0));
            // 
            // btnY
            // 
            this.btnY.AntiAlias = true;
            this.btnY.BackColor = System.Drawing.SystemColors.Control;
            this.btnY.Checked = false;
            this.btnY.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnY.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnY.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnY.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnY.Location = new System.Drawing.Point(229, 97);
            this.btnY.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnY.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnY.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnY.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnY.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnY.Name = "btnY";
            this.btnY.ShowFocusRectangle = false;
            this.btnY.Size = new System.Drawing.Size(35, 30);
            this.btnY.TabIndex = 52;
            this.btnY.TabStop = false;
            this.btnY.Text = "y";
            this.btnY.UseCompatibleTextRendering = true;
            this.btnY.UseMnemonic = false;
            this.btnY.UseVisualStyleBackColor = false;
            this.btnY.VKCode = ((short)(0));
            // 
            // btnSpace
            // 
            this.btnSpace.AntiAlias = true;
            this.btnSpace.BackColor = System.Drawing.SystemColors.Control;
            this.btnSpace.Checked = false;
            this.btnSpace.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnSpace.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnSpace.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnSpace.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpace.Location = new System.Drawing.Point(137, 187);
            this.btnSpace.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnSpace.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnSpace.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnSpace.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnSpace.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnSpace.Name = "btnSpace";
            this.btnSpace.ShowFocusRectangle = false;
            this.btnSpace.Size = new System.Drawing.Size(219, 30);
            this.btnSpace.TabIndex = 103;
            this.btnSpace.TabStop = false;
            this.btnSpace.UseCompatibleTextRendering = true;
            this.btnSpace.UseMnemonic = false;
            this.btnSpace.UseVisualStyleBackColor = false;
            this.btnSpace.VKCode = ((short)(0));
            // 
            // btnC
            // 
            this.btnC.AntiAlias = true;
            this.btnC.BackColor = System.Drawing.SystemColors.Control;
            this.btnC.Checked = false;
            this.btnC.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnC.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnC.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnC.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC.Location = new System.Drawing.Point(159, 157);
            this.btnC.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnC.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnC.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnC.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnC.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnC.Name = "btnC";
            this.btnC.ShowFocusRectangle = false;
            this.btnC.Size = new System.Drawing.Size(35, 30);
            this.btnC.TabIndex = 86;
            this.btnC.TabStop = false;
            this.btnC.Text = "c";
            this.btnC.UseCompatibleTextRendering = true;
            this.btnC.UseMnemonic = false;
            this.btnC.UseVisualStyleBackColor = false;
            this.btnC.VKCode = ((short)(0));
            // 
            // btnD
            // 
            this.btnD.AntiAlias = true;
            this.btnD.BackColor = System.Drawing.SystemColors.Control;
            this.btnD.Checked = false;
            this.btnD.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnD.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnD.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnD.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnD.Location = new System.Drawing.Point(141, 127);
            this.btnD.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnD.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnD.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnD.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnD.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnD.Name = "btnD";
            this.btnD.ShowFocusRectangle = false;
            this.btnD.Size = new System.Drawing.Size(35, 30);
            this.btnD.TabIndex = 70;
            this.btnD.TabStop = false;
            this.btnD.Text = "d";
            this.btnD.UseCompatibleTextRendering = true;
            this.btnD.UseMnemonic = false;
            this.btnD.UseVisualStyleBackColor = false;
            this.btnD.VKCode = ((short)(0));
            // 
            // btnE
            // 
            this.btnE.AntiAlias = true;
            this.btnE.BackColor = System.Drawing.SystemColors.Control;
            this.btnE.Checked = false;
            this.btnE.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnE.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnE.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnE.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnE.Location = new System.Drawing.Point(124, 97);
            this.btnE.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnE.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnE.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnE.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnE.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnE.Name = "btnE";
            this.btnE.ShowFocusRectangle = false;
            this.btnE.Size = new System.Drawing.Size(35, 30);
            this.btnE.TabIndex = 49;
            this.btnE.TabStop = false;
            this.btnE.Text = "e";
            this.btnE.UseCompatibleTextRendering = true;
            this.btnE.UseMnemonic = false;
            this.btnE.UseVisualStyleBackColor = false;
            this.btnE.VKCode = ((short)(0));
            // 
            // btn5
            // 
            this.btn5.AntiAlias = true;
            this.btn5.BackColor = System.Drawing.SystemColors.Control;
            this.btn5.Checked = false;
            this.btn5.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btn5.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btn5.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btn5.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn5.Location = new System.Drawing.Point(256, 40);
            this.btn5.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btn5.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btn5.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btn5.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btn5.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btn5.Name = "btn5";
            this.btn5.ShowFocusRectangle = false;
            this.btn5.Size = new System.Drawing.Size(51, 30);
            this.btn5.TabIndex = 30;
            this.btn5.TabStop = false;
            this.btn5.Text = "5";
            this.btn5.UseCompatibleTextRendering = true;
            this.btn5.UseMnemonic = false;
            this.btn5.UseVisualStyleBackColor = false;
            this.btn5.VKCode = ((short)(0));
            // 
            // btnQute
            // 
            this.btnQute.AntiAlias = true;
            this.btnQute.BackColor = System.Drawing.SystemColors.Control;
            this.btnQute.Checked = false;
            this.btnQute.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnQute.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnQute.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnQute.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQute.Location = new System.Drawing.Point(421, 127);
            this.btnQute.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnQute.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnQute.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnQute.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnQute.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnQute.Name = "btnQute";
            this.btnQute.ShowFocusRectangle = false;
            this.btnQute.Size = new System.Drawing.Size(35, 30);
            this.btnQute.TabIndex = 78;
            this.btnQute.TabStop = false;
            this.btnQute.Text = "\'";
            this.btnQute.UseCompatibleTextRendering = true;
            this.btnQute.UseMnemonic = false;
            this.btnQute.UseVisualStyleBackColor = false;
            this.btnQute.VKCode = ((short)(0));
            // 
            // btnLBracket
            // 
            this.btnLBracket.AntiAlias = true;
            this.btnLBracket.BackColor = System.Drawing.SystemColors.Control;
            this.btnLBracket.Checked = false;
            this.btnLBracket.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnLBracket.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnLBracket.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnLBracket.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLBracket.Location = new System.Drawing.Point(404, 97);
            this.btnLBracket.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnLBracket.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnLBracket.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnLBracket.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnLBracket.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnLBracket.Name = "btnLBracket";
            this.btnLBracket.ShowFocusRectangle = false;
            this.btnLBracket.Size = new System.Drawing.Size(35, 30);
            this.btnLBracket.TabIndex = 57;
            this.btnLBracket.TabStop = false;
            this.btnLBracket.Text = "[";
            this.btnLBracket.UseCompatibleTextRendering = true;
            this.btnLBracket.UseMnemonic = false;
            this.btnLBracket.UseVisualStyleBackColor = false;
            this.btnLBracket.VKCode = ((short)(0));
            // 
            // btn2
            // 
            this.btn2.AntiAlias = true;
            this.btn2.BackColor = System.Drawing.SystemColors.Control;
            this.btn2.Checked = false;
            this.btn2.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btn2.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btn2.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btn2.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn2.Location = new System.Drawing.Point(103, 40);
            this.btn2.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btn2.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btn2.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btn2.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btn2.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btn2.Name = "btn2";
            this.btn2.ShowFocusRectangle = false;
            this.btn2.Size = new System.Drawing.Size(51, 30);
            this.btn2.TabIndex = 27;
            this.btn2.TabStop = false;
            this.btn2.Text = "2";
            this.btn2.UseCompatibleTextRendering = true;
            this.btn2.UseMnemonic = false;
            this.btn2.UseVisualStyleBackColor = false;
            this.btn2.VKCode = ((short)(0));
            // 
            // btnDivide
            // 
            this.btnDivide.AntiAlias = true;
            this.btnDivide.BackColor = System.Drawing.SystemColors.Control;
            this.btnDivide.Checked = false;
            this.btnDivide.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnDivide.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnDivide.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnDivide.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDivide.Location = new System.Drawing.Point(404, 157);
            this.btnDivide.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnDivide.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnDivide.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnDivide.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnDivide.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnDivide.Name = "btnDivide";
            this.btnDivide.ShowFocusRectangle = false;
            this.btnDivide.Size = new System.Drawing.Size(35, 30);
            this.btnDivide.TabIndex = 93;
            this.btnDivide.TabStop = false;
            this.btnDivide.Text = "/";
            this.btnDivide.UseCompatibleTextRendering = true;
            this.btnDivide.UseMnemonic = false;
            this.btnDivide.UseVisualStyleBackColor = false;
            this.btnDivide.VKCode = ((short)(0));
            // 
            // btnSem
            // 
            this.btnSem.AntiAlias = true;
            this.btnSem.BackColor = System.Drawing.SystemColors.Control;
            this.btnSem.Checked = false;
            this.btnSem.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnSem.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnSem.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnSem.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSem.Location = new System.Drawing.Point(386, 127);
            this.btnSem.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnSem.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnSem.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnSem.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnSem.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnSem.Name = "btnSem";
            this.btnSem.ShowFocusRectangle = false;
            this.btnSem.Size = new System.Drawing.Size(35, 30);
            this.btnSem.TabIndex = 77;
            this.btnSem.TabStop = false;
            this.btnSem.Text = ";";
            this.btnSem.UseCompatibleTextRendering = true;
            this.btnSem.UseMnemonic = false;
            this.btnSem.UseVisualStyleBackColor = false;
            this.btnSem.VKCode = ((short)(0));
            // 
            // btnP
            // 
            this.btnP.AntiAlias = true;
            this.btnP.BackColor = System.Drawing.SystemColors.Control;
            this.btnP.Checked = false;
            this.btnP.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnP.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnP.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnP.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnP.Location = new System.Drawing.Point(369, 97);
            this.btnP.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnP.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnP.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnP.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnP.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnP.Name = "btnP";
            this.btnP.ShowFocusRectangle = false;
            this.btnP.Size = new System.Drawing.Size(35, 30);
            this.btnP.TabIndex = 56;
            this.btnP.TabStop = false;
            this.btnP.Text = "p";
            this.btnP.UseCompatibleTextRendering = true;
            this.btnP.UseMnemonic = false;
            this.btnP.UseVisualStyleBackColor = false;
            this.btnP.VKCode = ((short)(0));
            // 
            // btn0
            // 
            this.btn0.AntiAlias = true;
            this.btn0.BackColor = System.Drawing.SystemColors.Control;
            this.btn0.Checked = false;
            this.btn0.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btn0.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btn0.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btn0.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn0.Location = new System.Drawing.Point(511, 40);
            this.btn0.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btn0.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btn0.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btn0.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btn0.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btn0.Name = "btn0";
            this.btn0.ShowFocusRectangle = false;
            this.btn0.Size = new System.Drawing.Size(51, 30);
            this.btn0.TabIndex = 35;
            this.btn0.TabStop = false;
            this.btn0.Text = "0";
            this.btn0.UseCompatibleTextRendering = true;
            this.btn0.UseMnemonic = false;
            this.btn0.UseVisualStyleBackColor = false;
            this.btn0.VKCode = ((short)(0));
            // 
            // btnComma
            // 
            this.btnComma.AntiAlias = true;
            this.btnComma.BackColor = System.Drawing.SystemColors.Control;
            this.btnComma.Checked = false;
            this.btnComma.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnComma.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnComma.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnComma.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComma.Location = new System.Drawing.Point(334, 157);
            this.btnComma.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnComma.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnComma.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnComma.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnComma.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnComma.Name = "btnComma";
            this.btnComma.ShowFocusRectangle = false;
            this.btnComma.Size = new System.Drawing.Size(35, 30);
            this.btnComma.TabIndex = 91;
            this.btnComma.TabStop = false;
            this.btnComma.Text = ",";
            this.btnComma.UseCompatibleTextRendering = true;
            this.btnComma.UseMnemonic = false;
            this.btnComma.UseVisualStyleBackColor = false;
            this.btnComma.VKCode = ((short)(0));
            // 
            // btnK
            // 
            this.btnK.AntiAlias = true;
            this.btnK.BackColor = System.Drawing.SystemColors.Control;
            this.btnK.Checked = false;
            this.btnK.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnK.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnK.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnK.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnK.Location = new System.Drawing.Point(316, 127);
            this.btnK.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnK.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnK.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnK.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnK.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnK.Name = "btnK";
            this.btnK.ShowFocusRectangle = false;
            this.btnK.Size = new System.Drawing.Size(35, 30);
            this.btnK.TabIndex = 75;
            this.btnK.TabStop = false;
            this.btnK.Text = "k";
            this.btnK.UseCompatibleTextRendering = true;
            this.btnK.UseMnemonic = false;
            this.btnK.UseVisualStyleBackColor = false;
            this.btnK.VKCode = ((short)(0));
            // 
            // btnI
            // 
            this.btnI.AntiAlias = true;
            this.btnI.BackColor = System.Drawing.SystemColors.Control;
            this.btnI.Checked = false;
            this.btnI.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnI.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnI.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnI.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnI.Location = new System.Drawing.Point(299, 97);
            this.btnI.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnI.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnI.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnI.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnI.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnI.Name = "btnI";
            this.btnI.ShowFocusRectangle = false;
            this.btnI.Size = new System.Drawing.Size(35, 30);
            this.btnI.TabIndex = 54;
            this.btnI.TabStop = false;
            this.btnI.Text = "i";
            this.btnI.UseCompatibleTextRendering = true;
            this.btnI.UseMnemonic = false;
            this.btnI.UseVisualStyleBackColor = false;
            this.btnI.VKCode = ((short)(0));
            // 
            // btn9
            // 
            this.btn9.AntiAlias = true;
            this.btn9.BackColor = System.Drawing.SystemColors.Control;
            this.btn9.Checked = false;
            this.btn9.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btn9.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btn9.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btn9.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn9.Location = new System.Drawing.Point(460, 40);
            this.btn9.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btn9.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btn9.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btn9.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btn9.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btn9.Name = "btn9";
            this.btn9.ShowFocusRectangle = false;
            this.btn9.Size = new System.Drawing.Size(51, 30);
            this.btn9.TabIndex = 34;
            this.btn9.TabStop = false;
            this.btn9.Text = "9";
            this.btn9.UseCompatibleTextRendering = true;
            this.btn9.UseMnemonic = false;
            this.btn9.UseVisualStyleBackColor = false;
            this.btn9.VKCode = ((short)(0));
            // 
            // btnRCTRL
            // 
            this.btnRCTRL.AntiAlias = true;
            this.btnRCTRL.BackColor = System.Drawing.SystemColors.Control;
            this.btnRCTRL.Checked = false;
            this.btnRCTRL.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnRCTRL.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnRCTRL.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnRCTRL.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRCTRL.Location = new System.Drawing.Point(476, 187);
            this.btnRCTRL.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnRCTRL.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnRCTRL.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnRCTRL.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnRCTRL.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnRCTRL.Name = "btnRCTRL";
            this.btnRCTRL.ShowFocusRectangle = false;
            this.btnRCTRL.Size = new System.Drawing.Size(50, 30);
            this.btnRCTRL.TabIndex = 107;
            this.btnRCTRL.TabStop = false;
            this.btnRCTRL.Text = "ctrl";
            this.btnRCTRL.UseCompatibleTextRendering = true;
            this.btnRCTRL.UseMnemonic = false;
            this.btnRCTRL.UseVisualStyleBackColor = false;
            this.btnRCTRL.VKCode = ((short)(0));
            // 
            // btnM
            // 
            this.btnM.AntiAlias = true;
            this.btnM.BackColor = System.Drawing.SystemColors.Control;
            this.btnM.Checked = false;
            this.btnM.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnM.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnM.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnM.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnM.Location = new System.Drawing.Point(299, 157);
            this.btnM.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnM.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnM.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnM.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnM.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnM.Name = "btnM";
            this.btnM.ShowFocusRectangle = false;
            this.btnM.Size = new System.Drawing.Size(35, 30);
            this.btnM.TabIndex = 90;
            this.btnM.TabStop = false;
            this.btnM.Text = "m";
            this.btnM.UseCompatibleTextRendering = true;
            this.btnM.UseMnemonic = false;
            this.btnM.UseVisualStyleBackColor = false;
            this.btnM.VKCode = ((short)(0));
            // 
            // btnJ
            // 
            this.btnJ.AntiAlias = true;
            this.btnJ.BackColor = System.Drawing.SystemColors.Control;
            this.btnJ.Checked = false;
            this.btnJ.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnJ.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnJ.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnJ.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJ.Location = new System.Drawing.Point(281, 127);
            this.btnJ.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnJ.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnJ.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnJ.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnJ.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnJ.Name = "btnJ";
            this.btnJ.ShowFocusRectangle = false;
            this.btnJ.Size = new System.Drawing.Size(35, 30);
            this.btnJ.TabIndex = 74;
            this.btnJ.TabStop = false;
            this.btnJ.Text = "j";
            this.btnJ.UseCompatibleTextRendering = true;
            this.btnJ.UseMnemonic = false;
            this.btnJ.UseVisualStyleBackColor = false;
            this.btnJ.VKCode = ((short)(0));
            // 
            // btnU
            // 
            this.btnU.AntiAlias = true;
            this.btnU.BackColor = System.Drawing.SystemColors.Control;
            this.btnU.Checked = false;
            this.btnU.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnU.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnU.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnU.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnU.Location = new System.Drawing.Point(264, 97);
            this.btnU.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnU.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnU.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnU.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnU.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnU.Name = "btnU";
            this.btnU.ShowFocusRectangle = false;
            this.btnU.Size = new System.Drawing.Size(35, 30);
            this.btnU.TabIndex = 53;
            this.btnU.TabStop = false;
            this.btnU.Text = "u";
            this.btnU.UseCompatibleTextRendering = true;
            this.btnU.UseMnemonic = false;
            this.btnU.UseVisualStyleBackColor = false;
            this.btnU.VKCode = ((short)(0));
            // 
            // btn7
            // 
            this.btn7.AntiAlias = true;
            this.btn7.BackColor = System.Drawing.SystemColors.Control;
            this.btn7.Checked = false;
            this.btn7.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btn7.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btn7.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btn7.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn7.Location = new System.Drawing.Point(358, 40);
            this.btn7.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btn7.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btn7.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btn7.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btn7.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btn7.Name = "btn7";
            this.btn7.ShowFocusRectangle = false;
            this.btn7.Size = new System.Drawing.Size(51, 30);
            this.btn7.TabIndex = 32;
            this.btn7.TabStop = false;
            this.btn7.Text = "7";
            this.btn7.UseCompatibleTextRendering = true;
            this.btn7.UseMnemonic = false;
            this.btn7.UseVisualStyleBackColor = false;
            this.btn7.VKCode = ((short)(0));
            // 
            // btnRW
            // 
            this.btnRW.AntiAlias = true;
            this.btnRW.BackColor = System.Drawing.SystemColors.Control;
            this.btnRW.Checked = false;
            this.btnRW.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnRW.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnRW.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnRW.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRW.Location = new System.Drawing.Point(406, 187);
            this.btnRW.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnRW.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnRW.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnRW.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnRW.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnRW.Name = "btnRW";
            this.btnRW.ShowFocusRectangle = false;
            this.btnRW.Size = new System.Drawing.Size(35, 30);
            this.btnRW.TabIndex = 105;
            this.btnRW.TabStop = false;
            this.btnRW.Text = "rw";
            this.btnRW.UseCompatibleTextRendering = true;
            this.btnRW.UseMnemonic = false;
            this.btnRW.UseVisualStyleBackColor = false;
            this.btnRW.VKCode = ((short)(0));
            // 
            // btnB
            // 
            this.btnB.AntiAlias = true;
            this.btnB.BackColor = System.Drawing.SystemColors.Control;
            this.btnB.Checked = false;
            this.btnB.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnB.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnB.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnB.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnB.Location = new System.Drawing.Point(229, 157);
            this.btnB.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnB.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnB.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnB.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnB.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnB.Name = "btnB";
            this.btnB.ShowFocusRectangle = false;
            this.btnB.Size = new System.Drawing.Size(35, 30);
            this.btnB.TabIndex = 88;
            this.btnB.TabStop = false;
            this.btnB.Text = "b";
            this.btnB.UseCompatibleTextRendering = true;
            this.btnB.UseMnemonic = false;
            this.btnB.UseVisualStyleBackColor = false;
            this.btnB.VKCode = ((short)(0));
            // 
            // btnG
            // 
            this.btnG.AntiAlias = true;
            this.btnG.BackColor = System.Drawing.SystemColors.Control;
            this.btnG.Checked = false;
            this.btnG.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnG.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnG.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnG.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnG.Location = new System.Drawing.Point(211, 127);
            this.btnG.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnG.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnG.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnG.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnG.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnG.Name = "btnG";
            this.btnG.ShowFocusRectangle = false;
            this.btnG.Size = new System.Drawing.Size(35, 30);
            this.btnG.TabIndex = 72;
            this.btnG.TabStop = false;
            this.btnG.Text = "g";
            this.btnG.UseCompatibleTextRendering = true;
            this.btnG.UseMnemonic = false;
            this.btnG.UseVisualStyleBackColor = false;
            this.btnG.VKCode = ((short)(0));
            // 
            // btnRALT
            // 
            this.btnRALT.AntiAlias = true;
            this.btnRALT.BackColor = System.Drawing.SystemColors.Control;
            this.btnRALT.Checked = false;
            this.btnRALT.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnRALT.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnRALT.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnRALT.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRALT.Location = new System.Drawing.Point(356, 187);
            this.btnRALT.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnRALT.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnRALT.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnRALT.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnRALT.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnRALT.Name = "btnRALT";
            this.btnRALT.ShowFocusRectangle = false;
            this.btnRALT.Size = new System.Drawing.Size(50, 30);
            this.btnRALT.TabIndex = 104;
            this.btnRALT.TabStop = false;
            this.btnRALT.Text = "alt";
            this.btnRALT.UseCompatibleTextRendering = true;
            this.btnRALT.UseMnemonic = false;
            this.btnRALT.UseVisualStyleBackColor = false;
            this.btnRALT.VKCode = ((short)(0));
            // 
            // btnV
            // 
            this.btnV.AntiAlias = true;
            this.btnV.BackColor = System.Drawing.SystemColors.Control;
            this.btnV.Checked = false;
            this.btnV.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnV.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnV.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnV.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnV.Location = new System.Drawing.Point(194, 157);
            this.btnV.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnV.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnV.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnV.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnV.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnV.Name = "btnV";
            this.btnV.ShowFocusRectangle = false;
            this.btnV.Size = new System.Drawing.Size(35, 30);
            this.btnV.TabIndex = 87;
            this.btnV.TabStop = false;
            this.btnV.Text = "v";
            this.btnV.UseCompatibleTextRendering = true;
            this.btnV.UseMnemonic = false;
            this.btnV.UseVisualStyleBackColor = false;
            this.btnV.VKCode = ((short)(0));
            // 
            // btnT
            // 
            this.btnT.AntiAlias = true;
            this.btnT.BackColor = System.Drawing.SystemColors.Control;
            this.btnT.Checked = false;
            this.btnT.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnT.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnT.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnT.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnT.Location = new System.Drawing.Point(194, 97);
            this.btnT.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnT.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnT.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnT.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnT.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnT.Name = "btnT";
            this.btnT.ShowFocusRectangle = false;
            this.btnT.Size = new System.Drawing.Size(35, 30);
            this.btnT.TabIndex = 51;
            this.btnT.TabStop = false;
            this.btnT.Text = "t";
            this.btnT.UseCompatibleTextRendering = true;
            this.btnT.UseMnemonic = false;
            this.btnT.UseVisualStyleBackColor = false;
            this.btnT.VKCode = ((short)(0));
            // 
            // btnF
            // 
            this.btnF.AntiAlias = true;
            this.btnF.BackColor = System.Drawing.SystemColors.Control;
            this.btnF.Checked = false;
            this.btnF.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnF.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnF.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnF.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF.Location = new System.Drawing.Point(176, 127);
            this.btnF.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnF.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnF.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnF.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnF.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnF.Name = "btnF";
            this.btnF.ShowFocusRectangle = false;
            this.btnF.Size = new System.Drawing.Size(35, 30);
            this.btnF.TabIndex = 71;
            this.btnF.TabStop = false;
            this.btnF.Text = "f";
            this.btnF.UseCompatibleTextRendering = true;
            this.btnF.UseMnemonic = false;
            this.btnF.UseVisualStyleBackColor = false;
            this.btnF.VKCode = ((short)(0));
            // 
            // btnLALT
            // 
            this.btnLALT.AntiAlias = true;
            this.btnLALT.BackColor = System.Drawing.SystemColors.Control;
            this.btnLALT.Checked = false;
            this.btnLALT.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnLALT.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnLALT.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnLALT.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLALT.Location = new System.Drawing.Point(87, 187);
            this.btnLALT.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnLALT.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnLALT.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnLALT.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnLALT.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnLALT.Name = "btnLALT";
            this.btnLALT.ShowFocusRectangle = false;
            this.btnLALT.Size = new System.Drawing.Size(50, 30);
            this.btnLALT.TabIndex = 102;
            this.btnLALT.TabStop = false;
            this.btnLALT.Text = "alt";
            this.btnLALT.UseCompatibleTextRendering = true;
            this.btnLALT.UseMnemonic = false;
            this.btnLALT.UseVisualStyleBackColor = false;
            this.btnLALT.VKCode = ((short)(0));
            // 
            // btn6
            // 
            this.btn6.AntiAlias = true;
            this.btn6.BackColor = System.Drawing.SystemColors.Control;
            this.btn6.Checked = false;
            this.btn6.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btn6.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btn6.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btn6.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn6.Location = new System.Drawing.Point(307, 40);
            this.btn6.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btn6.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btn6.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btn6.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btn6.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btn6.Name = "btn6";
            this.btn6.ShowFocusRectangle = false;
            this.btn6.Size = new System.Drawing.Size(51, 30);
            this.btn6.TabIndex = 31;
            this.btn6.TabStop = false;
            this.btn6.Text = "6";
            this.btn6.UseCompatibleTextRendering = true;
            this.btn6.UseMnemonic = false;
            this.btn6.UseVisualStyleBackColor = false;
            this.btn6.VKCode = ((short)(0));
            // 
            // btnX
            // 
            this.btnX.AntiAlias = true;
            this.btnX.BackColor = System.Drawing.SystemColors.Control;
            this.btnX.Checked = false;
            this.btnX.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnX.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnX.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnX.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnX.Location = new System.Drawing.Point(124, 157);
            this.btnX.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnX.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnX.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnX.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnX.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnX.Name = "btnX";
            this.btnX.ShowFocusRectangle = false;
            this.btnX.Size = new System.Drawing.Size(35, 30);
            this.btnX.TabIndex = 85;
            this.btnX.TabStop = false;
            this.btnX.Text = "x";
            this.btnX.UseCompatibleTextRendering = true;
            this.btnX.UseMnemonic = false;
            this.btnX.UseVisualStyleBackColor = false;
            this.btnX.VKCode = ((short)(0));
            // 
            // btnR
            // 
            this.btnR.AntiAlias = true;
            this.btnR.BackColor = System.Drawing.SystemColors.Control;
            this.btnR.Checked = false;
            this.btnR.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnR.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnR.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnR.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnR.Location = new System.Drawing.Point(159, 97);
            this.btnR.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnR.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnR.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnR.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnR.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnR.Name = "btnR";
            this.btnR.ShowFocusRectangle = false;
            this.btnR.Size = new System.Drawing.Size(35, 30);
            this.btnR.TabIndex = 50;
            this.btnR.TabStop = false;
            this.btnR.Text = "r";
            this.btnR.UseCompatibleTextRendering = true;
            this.btnR.UseMnemonic = false;
            this.btnR.UseVisualStyleBackColor = false;
            this.btnR.VKCode = ((short)(0));
            // 
            // btnS
            // 
            this.btnS.AntiAlias = true;
            this.btnS.BackColor = System.Drawing.SystemColors.Control;
            this.btnS.Checked = false;
            this.btnS.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnS.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnS.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnS.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnS.Location = new System.Drawing.Point(106, 127);
            this.btnS.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnS.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnS.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnS.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnS.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnS.Name = "btnS";
            this.btnS.ShowFocusRectangle = false;
            this.btnS.Size = new System.Drawing.Size(35, 30);
            this.btnS.TabIndex = 69;
            this.btnS.TabStop = false;
            this.btnS.Text = "s";
            this.btnS.UseCompatibleTextRendering = true;
            this.btnS.UseMnemonic = false;
            this.btnS.UseVisualStyleBackColor = false;
            this.btnS.VKCode = ((short)(0));
            // 
            // btnLW
            // 
            this.btnLW.AntiAlias = true;
            this.btnLW.BackColor = System.Drawing.SystemColors.Control;
            this.btnLW.Checked = false;
            this.btnLW.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnLW.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnLW.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnLW.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLW.Location = new System.Drawing.Point(52, 187);
            this.btnLW.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnLW.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnLW.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnLW.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnLW.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnLW.Name = "btnLW";
            this.btnLW.ShowFocusRectangle = false;
            this.btnLW.Size = new System.Drawing.Size(35, 30);
            this.btnLW.TabIndex = 101;
            this.btnLW.TabStop = false;
            this.btnLW.Text = "lw";
            this.btnLW.UseCompatibleTextRendering = true;
            this.btnLW.UseMnemonic = false;
            this.btnLW.UseVisualStyleBackColor = false;
            this.btnLW.VKCode = ((short)(0));
            // 
            // btn4
            // 
            this.btn4.AntiAlias = true;
            this.btn4.BackColor = System.Drawing.SystemColors.Control;
            this.btn4.Checked = false;
            this.btn4.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btn4.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btn4.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btn4.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn4.Location = new System.Drawing.Point(205, 40);
            this.btn4.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btn4.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btn4.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btn4.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btn4.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btn4.Name = "btn4";
            this.btn4.ShowFocusRectangle = false;
            this.btn4.Size = new System.Drawing.Size(51, 30);
            this.btn4.TabIndex = 29;
            this.btn4.TabStop = false;
            this.btn4.Text = "4";
            this.btn4.UseCompatibleTextRendering = true;
            this.btn4.UseMnemonic = false;
            this.btn4.UseVisualStyleBackColor = false;
            this.btn4.VKCode = ((short)(0));
            // 
            // btnZ
            // 
            this.btnZ.AntiAlias = true;
            this.btnZ.BackColor = System.Drawing.SystemColors.Control;
            this.btnZ.Checked = false;
            this.btnZ.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnZ.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnZ.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnZ.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZ.Location = new System.Drawing.Point(89, 157);
            this.btnZ.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnZ.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnZ.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnZ.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnZ.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnZ.Name = "btnZ";
            this.btnZ.ShowFocusRectangle = false;
            this.btnZ.Size = new System.Drawing.Size(35, 30);
            this.btnZ.TabIndex = 84;
            this.btnZ.TabStop = false;
            this.btnZ.Text = "z";
            this.btnZ.UseCompatibleTextRendering = true;
            this.btnZ.UseMnemonic = false;
            this.btnZ.UseVisualStyleBackColor = false;
            this.btnZ.VKCode = ((short)(0));
            // 
            // btnW
            // 
            this.btnW.AntiAlias = true;
            this.btnW.BackColor = System.Drawing.SystemColors.Control;
            this.btnW.Checked = false;
            this.btnW.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnW.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnW.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnW.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnW.Location = new System.Drawing.Point(89, 97);
            this.btnW.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnW.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnW.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnW.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnW.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnW.Name = "btnW";
            this.btnW.ShowFocusRectangle = false;
            this.btnW.Size = new System.Drawing.Size(35, 30);
            this.btnW.TabIndex = 48;
            this.btnW.TabStop = false;
            this.btnW.Text = "w";
            this.btnW.UseCompatibleTextRendering = true;
            this.btnW.UseMnemonic = false;
            this.btnW.UseVisualStyleBackColor = false;
            this.btnW.VKCode = ((short)(0));
            // 
            // btnA
            // 
            this.btnA.AntiAlias = true;
            this.btnA.BackColor = System.Drawing.SystemColors.Control;
            this.btnA.Checked = false;
            this.btnA.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnA.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnA.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnA.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnA.Location = new System.Drawing.Point(71, 127);
            this.btnA.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnA.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnA.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnA.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnA.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnA.Name = "btnA";
            this.btnA.ShowFocusRectangle = false;
            this.btnA.Size = new System.Drawing.Size(35, 30);
            this.btnA.TabIndex = 68;
            this.btnA.TabStop = false;
            this.btnA.Text = "a";
            this.btnA.UseCompatibleTextRendering = true;
            this.btnA.UseMnemonic = false;
            this.btnA.UseVisualStyleBackColor = false;
            this.btnA.VKCode = ((short)(0));
            // 
            // btn3
            // 
            this.btn3.AntiAlias = true;
            this.btn3.BackColor = System.Drawing.SystemColors.Control;
            this.btn3.Checked = false;
            this.btn3.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btn3.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btn3.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btn3.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn3.Location = new System.Drawing.Point(154, 40);
            this.btn3.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btn3.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btn3.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btn3.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btn3.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btn3.Name = "btn3";
            this.btn3.ShowFocusRectangle = false;
            this.btn3.Size = new System.Drawing.Size(51, 30);
            this.btn3.TabIndex = 28;
            this.btn3.TabStop = false;
            this.btn3.Text = "3";
            this.btn3.UseCompatibleTextRendering = true;
            this.btn3.UseMnemonic = false;
            this.btn3.UseVisualStyleBackColor = false;
            this.btn3.VKCode = ((short)(0));
            // 
            // btnQ
            // 
            this.btnQ.AntiAlias = true;
            this.btnQ.BackColor = System.Drawing.SystemColors.Control;
            this.btnQ.Checked = false;
            this.btnQ.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnQ.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnQ.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnQ.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQ.Location = new System.Drawing.Point(54, 97);
            this.btnQ.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnQ.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnQ.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnQ.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnQ.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnQ.Name = "btnQ";
            this.btnQ.ShowFocusRectangle = false;
            this.btnQ.Size = new System.Drawing.Size(35, 30);
            this.btnQ.TabIndex = 47;
            this.btnQ.TabStop = false;
            this.btnQ.Text = "q";
            this.btnQ.UseCompatibleTextRendering = true;
            this.btnQ.UseMnemonic = false;
            this.btnQ.UseVisualStyleBackColor = false;
            this.btnQ.VKCode = ((short)(0));
            // 
            // btn1
            // 
            this.btn1.AntiAlias = true;
            this.btn1.BackColor = System.Drawing.SystemColors.Control;
            this.btn1.Checked = false;
            this.btn1.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btn1.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btn1.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btn1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn1.Location = new System.Drawing.Point(52, 40);
            this.btn1.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btn1.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btn1.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btn1.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btn1.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btn1.Name = "btn1";
            this.btn1.ShowFocusRectangle = false;
            this.btn1.Size = new System.Drawing.Size(51, 30);
            this.btn1.TabIndex = 26;
            this.btn1.TabStop = false;
            this.btn1.Text = "1";
            this.btn1.UseCompatibleTextRendering = true;
            this.btn1.UseMnemonic = false;
            this.btn1.UseVisualStyleBackColor = false;
            this.btn1.VKCode = ((short)(0));
            // 
            // btnWave
            // 
            this.btnWave.AntiAlias = true;
            this.btnWave.BackColor = System.Drawing.SystemColors.Control;
            this.btnWave.Checked = false;
            this.btnWave.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnWave.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnWave.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnWave.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWave.Location = new System.Drawing.Point(1, 40);
            this.btnWave.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnWave.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnWave.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnWave.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnWave.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnWave.Name = "btnWave";
            this.btnWave.ShowFocusRectangle = false;
            this.btnWave.Size = new System.Drawing.Size(51, 30);
            this.btnWave.TabIndex = 25;
            this.btnWave.TabStop = false;
            this.btnWave.Text = "~";
            this.btnWave.UseCompatibleTextRendering = true;
            this.btnWave.UseMnemonic = false;
            this.btnWave.UseVisualStyleBackColor = false;
            this.btnWave.VKCode = ((short)(0));
            // 
            // btnESC
            // 
            this.btnESC.AntiAlias = true;
            this.btnESC.BackColor = System.Drawing.SystemColors.Control;
            this.btnESC.Checked = false;
            this.btnESC.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.btnESC.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.btnESC.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.btnESC.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnESC.Location = new System.Drawing.Point(1, 0);
            this.btnESC.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.btnESC.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.btnESC.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.btnESC.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.btnESC.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.btnESC.Name = "btnESC";
            this.btnESC.ShowFocusRectangle = false;
            this.btnESC.Size = new System.Drawing.Size(51, 38);
            this.btnESC.TabIndex = 6;
            this.btnESC.TabStop = false;
            this.btnESC.Text = "esc";
            this.btnESC.UseCompatibleTextRendering = true;
            this.btnESC.UseMnemonic = false;
            this.btnESC.UseVisualStyleBackColor = false;
            this.btnESC.VKCode = ((short)(0));
            // 
            // keyboardButton1
            // 
            this.keyboardButton1.AntiAlias = true;
            this.keyboardButton1.BackColor = System.Drawing.SystemColors.Control;
            this.keyboardButton1.Checked = false;
            this.keyboardButton1.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.keyboardButton1.DefaultStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(230)))), ((int)(((byte)(251)))));
            this.keyboardButton1.DefautEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(187)))), ((int)(((byte)(234)))));
            this.keyboardButton1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.keyboardButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.keyboardButton1.Location = new System.Drawing.Point(718, 3);
            this.keyboardButton1.MouseDownEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(131)))));
            this.keyboardButton1.MouseDownStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(151)))), ((int)(((byte)(84)))));
            this.keyboardButton1.MouseEnterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.keyboardButton1.MouseEnterEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(152)))));
            this.keyboardButton1.MouseEnterStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(197)))));
            this.keyboardButton1.Name = "keyboardButton1";
            this.keyboardButton1.ShowFocusRectangle = false;
            this.keyboardButton1.Size = new System.Drawing.Size(154, 127);
            this.keyboardButton1.TabIndex = 111;
            this.keyboardButton1.TabStop = false;
            this.keyboardButton1.Text = "关闭键盘";
            this.keyboardButton1.UseCompatibleTextRendering = true;
            this.keyboardButton1.UseMnemonic = false;
            this.keyboardButton1.UseVisualStyleBackColor = false;
            this.keyboardButton1.VKCode = ((short)(0));
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(783, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 112;
            this.label1.Text = "拖动键盘";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(784, -2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 263);
            this.label2.TabIndex = 113;
            // 
            // ScreenKeyboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(894, 256);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.keyboardButton1);
            this.Controls.Add(this.btnRA);
            this.Controls.Add(this.btnUP);
            this.Controls.Add(this.btnDN);
            this.Controls.Add(this.btnLA);
            this.Controls.Add(this.btnF12);
            this.Controls.Add(this.btnF11);
            this.Controls.Add(this.btnF10);
            this.Controls.Add(this.btnF9);
            this.Controls.Add(this.btnF8);
            this.Controls.Add(this.btnF7);
            this.Controls.Add(this.btnF6);
            this.Controls.Add(this.btnF5);
            this.Controls.Add(this.btnF4);
            this.Controls.Add(this.btnF3);
            this.Controls.Add(this.btnF2);
            this.Controls.Add(this.btnF1);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.btnEqual);
            this.Controls.Add(this.btnLCTRL);
            this.Controls.Add(this.btnLSHFT);
            this.Controls.Add(this.btnLOCK);
            this.Controls.Add(this.btnTAB);
            this.Controls.Add(this.btnBKSP);
            this.Controls.Add(this.btnRSHFT);
            this.Controls.Add(this.btnENT);
            this.Controls.Add(this.btnRBracket);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.btnFullStop);
            this.Controls.Add(this.btnL);
            this.Controls.Add(this.btnO);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btnMU);
            this.Controls.Add(this.btnN);
            this.Controls.Add(this.btnH);
            this.Controls.Add(this.btnY);
            this.Controls.Add(this.btnSpace);
            this.Controls.Add(this.btnC);
            this.Controls.Add(this.btnD);
            this.Controls.Add(this.btnE);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btnQute);
            this.Controls.Add(this.btnLBracket);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btnDivide);
            this.Controls.Add(this.btnSem);
            this.Controls.Add(this.btnP);
            this.Controls.Add(this.btn0);
            this.Controls.Add(this.btnComma);
            this.Controls.Add(this.btnK);
            this.Controls.Add(this.btnI);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btnRCTRL);
            this.Controls.Add(this.btnM);
            this.Controls.Add(this.btnJ);
            this.Controls.Add(this.btnU);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.btnRW);
            this.Controls.Add(this.btnB);
            this.Controls.Add(this.btnG);
            this.Controls.Add(this.btnRALT);
            this.Controls.Add(this.btnV);
            this.Controls.Add(this.btnT);
            this.Controls.Add(this.btnF);
            this.Controls.Add(this.btnLALT);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.btnX);
            this.Controls.Add(this.btnR);
            this.Controls.Add(this.btnS);
            this.Controls.Add(this.btnLW);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btnZ);
            this.Controls.Add(this.btnW);
            this.Controls.Add(this.btnA);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.btnQ);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btnWave);
            this.Controls.Add(this.btnESC);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ScreenKeyboard";
            this.Text = "On-Screen Keyboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KeyboardButton btnRA;
        private KeyboardButton btnUP;
        private KeyboardButton btnDN;
        private KeyboardButton btnLA;
        private KeyboardButton btnF12;
        private KeyboardButton btnF11;
        private KeyboardButton btnF10;
        private KeyboardButton btnF9;
        private KeyboardButton btnF8;
        private KeyboardButton btnF7;
        private KeyboardButton btnF6;
        private KeyboardButton btnF5;
        private KeyboardButton btnF4;
        private KeyboardButton btnF3;
        private KeyboardButton btnF2;
        private KeyboardButton btnF1;
        private KeyboardButton btnPath;
        private KeyboardButton btnEqual;
        private KeyboardButton btnLCTRL;
        private KeyboardButton btnLSHFT;
        private KeyboardButton btnLOCK;
        private KeyboardButton btnTAB;
        private KeyboardButton btnBKSP;
        private KeyboardButton btnRSHFT;
        private KeyboardButton btnENT;
        private KeyboardButton btnRBracket;
        private KeyboardButton btnMinus;
        private KeyboardButton btnFullStop;
        private KeyboardButton btnL;
        private KeyboardButton btnO;
        private KeyboardButton btn8;
        private KeyboardButton btnMU;
        private KeyboardButton btnN;
        private KeyboardButton btnH;
        private KeyboardButton btnY;
        private KeyboardButton btnSpace;
        private KeyboardButton btnC;
        private KeyboardButton btnD;
        private KeyboardButton btnE;
        private KeyboardButton btn5;
        private KeyboardButton btnQute;
        private KeyboardButton btnLBracket;
        private KeyboardButton btn2;
        private KeyboardButton btnDivide;
        private KeyboardButton btnSem;
        private KeyboardButton btnP;
        private KeyboardButton btn0;
        private KeyboardButton btnComma;
        private KeyboardButton btnK;
        private KeyboardButton btnI;
        private KeyboardButton btn9;
        private KeyboardButton btnRCTRL;
        private KeyboardButton btnM;
        private KeyboardButton btnJ;
        private KeyboardButton btnU;
        private KeyboardButton btn7;
        private KeyboardButton btnRW;
        private KeyboardButton btnB;
        private KeyboardButton btnG;
        private KeyboardButton btnRALT;
        private KeyboardButton btnV;
        private KeyboardButton btnT;
        private KeyboardButton btnF;
        private KeyboardButton btnLALT;
        private KeyboardButton btn6;
        private KeyboardButton btnX;
        private KeyboardButton btnR;
        private KeyboardButton btnS;
        private KeyboardButton btnLW;
        private KeyboardButton btn4;
        private KeyboardButton btnZ;
        private KeyboardButton btnW;
        private KeyboardButton btnA;
        private KeyboardButton btn3;
        private KeyboardButton btnQ;
        private KeyboardButton btn1;
        private KeyboardButton btnWave;
        private KeyboardButton btnESC;
        private KeyboardButton keyboardButton1;
        private Label label1;
        private Label label2;
    }
}

