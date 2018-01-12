using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace WFNetLib.Forms
{
    public partial class ScreenKeyboard : Form{

        private static readonly IDictionary<string, string> keyPairs;
        private static readonly IDictionary<string, string> numKeyPairs;

        private static IDictionary<short, IList<KeyboardButton>> spacialVKButtonsMap;
        private static IDictionary<short, IList<KeyboardButton>> combinationVKButtonsMap;

        #region Key Pairs

        static ScreenKeyboard() {
            keyPairs = new Dictionary<string, string>();
            keyPairs.Add("1", "!");
            keyPairs.Add("2", "@");
            keyPairs.Add("3", "#");
            keyPairs.Add("4", "$");
            keyPairs.Add("5", "%");
            keyPairs.Add("6", "^");
            keyPairs.Add("7", "&&");
            keyPairs.Add("8", "*");
            keyPairs.Add("9", "(");
            keyPairs.Add("0", ")");
            keyPairs.Add("`", "~");
            keyPairs.Add("-", "_");
            keyPairs.Add("=", "+");
            keyPairs.Add("\\", "|");
            keyPairs.Add("[", "{");
            keyPairs.Add("]", "}");
            keyPairs.Add(";", ":");
            keyPairs.Add("'", "\"");
            keyPairs.Add(",", "<");
            keyPairs.Add(".", ">");
            keyPairs.Add("/", "?");

            numKeyPairs = new Dictionary<string, string>();
            numKeyPairs.Add("1", "end");
            numKeyPairs.Add("2", "¡ý");
            numKeyPairs.Add("3", "pdn");
            numKeyPairs.Add("4", "¡û");
            numKeyPairs.Add("5", string.Empty);
            numKeyPairs.Add("6", "¡ú");
            numKeyPairs.Add("7", "hm");
            numKeyPairs.Add("8", "¡ü");
            numKeyPairs.Add("9", "pup");
            numKeyPairs.Add("0", "ins");
        }

        #endregion

        public ScreenKeyboard() {
            InitializeComponent();
            base.TopMost = true;
            base.StartPosition = FormStartPosition.CenterScreen;
            

            #region Initialize VK Button maps

            spacialVKButtonsMap = new Dictionary<short, IList<KeyboardButton>>();
            combinationVKButtonsMap = new Dictionary<short, IList<KeyboardButton>>();

            IList<KeyboardButton> buttonList = new List<KeyboardButton>();
            buttonList.Add(this.btnLCTRL);
            buttonList.Add(this.btnRCTRL);
            combinationVKButtonsMap.Add(KeyboardConstaint.VK_CONTROL, buttonList);

            buttonList = new List<KeyboardButton>();
            buttonList.Add(this.btnLSHFT);
            buttonList.Add(this.btnRSHFT);
            combinationVKButtonsMap.Add(KeyboardConstaint.VK_SHIFT, buttonList);

            buttonList = new List<KeyboardButton>();
            buttonList.Add(this.btnLALT);
            buttonList.Add(this.btnRALT);
            combinationVKButtonsMap.Add(KeyboardConstaint.VK_MENU, buttonList);

            buttonList = new List<KeyboardButton>();
            buttonList.Add(this.btnLW);
            buttonList.Add(this.btnRW);
            combinationVKButtonsMap.Add(KeyboardConstaint.VK_LWIN, buttonList);

            buttonList = new List<KeyboardButton>();
            buttonList.Add(this.btnLOCK);
            spacialVKButtonsMap.Add(KeyboardConstaint.VK_CAPITAL, buttonList);

           

            

            #endregion

            foreach (Control ctrl in this.Controls) {
                KeyboardButton button = ctrl as KeyboardButton;
                if (button == null) {
                    continue;
                }

                #region Set virtual keycode

                short key = 0;
                bool isSpacialKey = true;
                switch (button.Name) {
                case "btnLCTRL":
                case "btnRCTRL":
                    key = KeyboardConstaint.VK_CONTROL;
                    break;

                case "btnLSHFT":
                case "btnRSHFT":
                    key = KeyboardConstaint.VK_SHIFT;
                    break;

                case "btnLALT":
                case "btnRALT":
                    key = KeyboardConstaint.VK_MENU;
                    break;

                case "btnLW":
                case "btnRW":
                    key = KeyboardConstaint.VK_LWIN;
                    break;

                case "btnESC":
                    key = KeyboardConstaint.VK_ESCAPE;
                    break;

                case "btnTAB":
                    key = KeyboardConstaint.VK_TAB;
                    break;

                case "btnF1":
                    key = KeyboardConstaint.VK_F1;
                    break;

                case "btnF2":
                    key = KeyboardConstaint.VK_F2;
                    break;

                case "btnF3":
                    key = KeyboardConstaint.VK_F3;
                    break;

                case "btnF4":
                    key = KeyboardConstaint.VK_F4;
                    break;

                case "btnF5":
                    key = KeyboardConstaint.VK_F5;
                    break;

                case "btnF6":
                    key = KeyboardConstaint.VK_F6;
                    break;

                case "btnF7":
                    key = KeyboardConstaint.VK_F7;
                    break;

                case "btnF8":
                    key = KeyboardConstaint.VK_F8;
                    break;

                case "btnF9":
                    key = KeyboardConstaint.VK_F9;
                    break;

                case "btnF10":
                    key = KeyboardConstaint.VK_F10;
                    break;

                case "btnF11":
                    key = KeyboardConstaint.VK_F11;
                    break;

                case "btnF12":
                    key = KeyboardConstaint.VK_F12;
                    break;

                case "btnENT":
                case "btnNUMENT":
                    key = KeyboardConstaint.VK_RETURN;
                    break;

                case "btnWave":
                    key = KeyboardConstaint.VK_OEM_3;
                    break;

                case "btnSem":
                    key = KeyboardConstaint.VK_OEM_1;
                    break;

                case "btnQute":
                    key = KeyboardConstaint.VK_OEM_7;
                    break;

                case "btnSpace":
                    key = KeyboardConstaint.VK_SPACE;
                    break;

                case "btnBKSP":
                    key = KeyboardConstaint.VK_BACK;
                    break;

                case "btnComma":
                    key = KeyboardConstaint.VK_OEM_COMMA;
                    break;

                case "btnFullStop":
                    key = KeyboardConstaint.VK_OEM_PERIOD;
                    break;

                case "btnLOCK":
                    key = KeyboardConstaint.VK_CAPITAL;
                    break;

                case "btnMinus":
                    key = KeyboardConstaint.VK_OEM_MINUS;
                    break;

                case "btnEqual":
                    key = KeyboardConstaint.VK_OEM_PLUS;
                    break;

                case "btnLBracket":
                    key = KeyboardConstaint.VK_OEM_4;
                    break;

                case "btnRBracket":
                    key = KeyboardConstaint.VK_OEM_6;
                    break;

                case "btnPath":
                    key = KeyboardConstaint.VK_OEM_5;
                    break;

                case "btnDivide":
                    key = KeyboardConstaint.VK_OEM_2;
                    break;

                case "btnMU":
                    key = KeyboardConstaint.VK_APPS;
                    break;

                case "btnPSC":
                    key = KeyboardConstaint.VK_SNAPSHOT;
                    break;

                case "btnSLK":
                    key = KeyboardConstaint.VK_SCROLL;
                    break;

                case "btnBRK":
                    key = KeyboardConstaint.VK_PAUSE;
                    break;

                case "btnINS":
                    key = KeyboardConstaint.VK_INSERT;
                    break;

                case "btnHM":
                    key = KeyboardConstaint.VK_HOME;
                    break;

                case "btnPUP":
                    key = KeyboardConstaint.VK_PRIOR;
                    break;

                case "btnDEL":
                    key = KeyboardConstaint.VK_DELETE;
                    break;

                case "btnEND":
                    key = KeyboardConstaint.VK_END;
                    break;

                case "btnPDN":
                    key = KeyboardConstaint.VK_NEXT;
                    break;

                case "btnUP":
                    key = KeyboardConstaint.VK_UP;
                    break;

                case "btnDN":
                    key = KeyboardConstaint.VK_DOWN;
                    break;

                case "btnLA":
                    key = KeyboardConstaint.VK_LEFT;
                    break;

                case "btnRA":
                    key = KeyboardConstaint.VK_RIGHT;
                    break;

                case "btnNUM0":
                    key = KeyboardConstaint.VK_NUMPAD0;
                    break;

                case "btnNUM1":
                    key = KeyboardConstaint.VK_NUMPAD1;
                    break;

                case "btnNUM2":
                    key = KeyboardConstaint.VK_NUMPAD2;
                    break;

                case "btnNUM3":
                    key = KeyboardConstaint.VK_NUMPAD3;
                    break;

                case "btnNUM4":
                    key = KeyboardConstaint.VK_NUMPAD4;
                    break;

                case "btnNUM5":
                    key = KeyboardConstaint.VK_NUMPAD5;
                    break;

                case "btnNUM6":
                    key = KeyboardConstaint.VK_NUMPAD6;
                    break;

                case "btnNUM7":
                    key = KeyboardConstaint.VK_NUMPAD7;
                    break;

                case "btnNUM8":
                    key = KeyboardConstaint.VK_NUMPAD8;
                    break;

                case "btnNUM9":
                    key = KeyboardConstaint.VK_NUMPAD9;
                    break;

                case "btnNLK":
                    key = KeyboardConstaint.VK_NUMLOCK;
                    break;

                case "btnNUMDivide":
                    key = KeyboardConstaint.VK_DIVIDE;
                    break;

                case "btnMultiply":
                    key = KeyboardConstaint.VK_MULTIPLY;
                    break;

                case "btnNUMPlus":
                    key = KeyboardConstaint.VK_ADD;
                    break;

                case "btnNUMMinus":
                    key = KeyboardConstaint.VK_SUBTRACT;
                    break;

                case "btnNUMDot":
                    key = KeyboardConstaint.VK_DECIMAL;
                    break;

                default:
                    isSpacialKey = false;
                    break;
                }

                if (!isSpacialKey) {
                    key = (short)button.Name[3];
                }

                button.VKCode = key;

                #endregion

                button.Click += ButtonOnClick;
            }
        }


        protected override void OnLoad(EventArgs e) 
        {
            
            base.OnLoad(e);
        } 
        private void ButtonOnClick(object sender, EventArgs e) {
            KeyboardButton btnKey = sender as KeyboardButton;
            if (btnKey == null) {
                return;
            }
//             if (true)//ÅÐ¶ÏÌØÊâ¼ü
//             {
//                 
//             }
//             else
//             {
//                 SendKeys.Send(btnKey.Text);
//             }
            
        } 
        private void SetButtonStatus() {
//             foreach (KeyValuePair<short, IList<KeyboardButton>> entry in spacialVKButtonsMap) {
//                 bool isOn = IsSPactialKeyOn(entry.Key);
//                 foreach (KeyboardButton button in entry.Value) {
//                     button.Checked = isOn;
//                 }
//             }
// 
//             foreach (KeyValuePair<short, IList<KeyboardButton>> entry in combinationVKButtonsMap) {
//                 bool isDown = IsCommonKeyOn(entry.Key);
//                 foreach (KeyboardButton button in entry.Value) {
//                     button.Checked = isDown;
//                 }
//             }

            SetButtonText();
        }

        private void SetButtonText() {
            bool isShifOn = this.btnLSHFT.Checked;
            bool isCapsLockOn = this.btnLOCK.Checked;

            foreach (Control ctrl in this.Controls) {
                Button button = ctrl as Button;
                if (button == null)
                    continue;

                string buttonName = button.Name;
                if (buttonName.Length == 4) {
                    if (char.IsDigit(buttonName[3])) {
                        // such as btn2
                        button.Text = isShifOn ? keyPairs[buttonName[3].ToString()] : buttonName[3].ToString();
                    } else {
                        // such as btnA
                        bool shouldShowUpper = isShifOn ^ isCapsLockOn;
                        button.Text = shouldShowUpper ? buttonName[3].ToString() : buttonName[3].ToString().ToLower();
                    }
                } else if (buttonName.IndexOf("btnNUM") >= 0 && buttonName.Length > 6 && char.IsDigit(buttonName[6])) {
                    // such as btnNUM2 
                   
                } else if (string.Equals(buttonName, "btnNUMDot")) {
                    
                } else {
                    IDictionary<string, string> buttonNameTextMaps = new Dictionary<string, string>();
                    buttonNameTextMaps.Add("btnWave", "`");
                    buttonNameTextMaps.Add("btnMinus", "-");
                    buttonNameTextMaps.Add("btnLBracket", "[");
                    buttonNameTextMaps.Add("btnRBracket", "]");
                    buttonNameTextMaps.Add("btnPath", "\\");
                    buttonNameTextMaps.Add("btnSem", ";");
                    buttonNameTextMaps.Add("btnQute", "'");
                    buttonNameTextMaps.Add("btnComma", ",");
                    buttonNameTextMaps.Add("btnFullStop", ".");
                    buttonNameTextMaps.Add("btnDivide", "/");

                    if (buttonNameTextMaps.ContainsKey(buttonName)) {
                        button.Text = isShifOn ? keyPairs[buttonNameTextMaps[buttonName]] : buttonNameTextMaps[buttonName];
                    }
                }
            }
        }
        
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
                cp.Parent = IntPtr.Zero; // Keep this line only if you used UserControl
                return cp;

                //return base.CreateParams;
            }
        } 
    }
    public class FormBase : Form
    {
        private PaintColor paintColor;

        public FormBase()
        {
            this.paintColor = new PaintColor();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (base.ClientRectangle.IsEmpty)
            {
                return;
            }

            using (Brush brush = new LinearGradientBrush(base.ClientRectangle,
                this.paintColor.startColor, this.paintColor.endColor, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, base.ClientRectangle);
            }

            base.OnPaint(e);
        }

        private class PaintColor
        {
            internal Color startColor;
            internal Color endColor;

            public PaintColor()
            {
                this.startColor = Color.FromArgb(88, 134, 213);
                this.endColor = Color.FromArgb(4, 57, 149);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormBase
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "FormBase";
            this.Load += new System.EventHandler(this.FormBase_Load);
            this.ResumeLayout(false);

        }
        private void FormBase_Load(object sender, EventArgs e)
        {

        }
    }
}
