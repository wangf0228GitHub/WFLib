namespace SQLDBTool
{
    partial class frmGenerator
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpFilter = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtReaderFactory = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtAssembly = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cBoxTable = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFactoryAssembly = new System.Windows.Forms.TextBox();
            this.bWeb = new System.Windows.Forms.CheckBox();
            this.bDLL = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tpFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tpFilter);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(0, 106);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(883, 527);
            this.tabControl.TabIndex = 0;
            // 
            // tpFilter
            // 
            this.tpFilter.Controls.Add(this.panel2);
            this.tpFilter.Location = new System.Drawing.Point(4, 22);
            this.tpFilter.Name = "tpFilter";
            this.tpFilter.Padding = new System.Windows.Forms.Padding(3);
            this.tpFilter.Size = new System.Drawing.Size(875, 501);
            this.tpFilter.TabIndex = 0;
            this.tpFilter.Text = "Property";
            this.tpFilter.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtResult);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(869, 495);
            this.panel2.TabIndex = 6;
            // 
            // txtResult
            // 
            this.txtResult.AllowDrop = true;
            this.txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResult.Location = new System.Drawing.Point(0, 0);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(869, 495);
            this.txtResult.TabIndex = 0;
            this.txtResult.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResult_KeyDown);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtReaderFactory);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(875, 501);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "DataReaderFactory";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtReaderFactory
            // 
            this.txtReaderFactory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReaderFactory.Location = new System.Drawing.Point(3, 3);
            this.txtReaderFactory.Multiline = true;
            this.txtReaderFactory.Name = "txtReaderFactory";
            this.txtReaderFactory.Size = new System.Drawing.Size(869, 495);
            this.txtReaderFactory.TabIndex = 1;
            this.txtReaderFactory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReaderFactory_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(875, 501);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(869, 495);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(875, 501);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(875, 501);
            this.dataGridView2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "DomainAssembly:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(312, 77);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtAssembly
            // 
            this.txtAssembly.Location = new System.Drawing.Point(117, 46);
            this.txtAssembly.Name = "txtAssembly";
            this.txtAssembly.Size = new System.Drawing.Size(161, 21);
            this.txtAssembly.TabIndex = 1;
            this.txtAssembly.Text = "Common";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Table:";
            // 
            // cBoxTable
            // 
            this.cBoxTable.FormattingEnabled = true;
            this.cBoxTable.Location = new System.Drawing.Point(117, 13);
            this.cBoxTable.Name = "cBoxTable";
            this.cBoxTable.Size = new System.Drawing.Size(161, 20);
            this.cBoxTable.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "FactoryAssembly:";
            // 
            // txtFactoryAssembly
            // 
            this.txtFactoryAssembly.Location = new System.Drawing.Point(117, 77);
            this.txtFactoryAssembly.Name = "txtFactoryAssembly";
            this.txtFactoryAssembly.Size = new System.Drawing.Size(161, 21);
            this.txtFactoryAssembly.TabIndex = 6;
            this.txtFactoryAssembly.Text = "DataFactory";
            // 
            // bWeb
            // 
            this.bWeb.AutoSize = true;
            this.bWeb.Location = new System.Drawing.Point(475, 84);
            this.bWeb.Name = "bWeb";
            this.bWeb.Size = new System.Drawing.Size(42, 16);
            this.bWeb.TabIndex = 7;
            this.bWeb.Text = "Web";
            this.bWeb.UseVisualStyleBackColor = true;
            // 
            // bDLL
            // 
            this.bDLL.AutoSize = true;
            this.bDLL.Location = new System.Drawing.Point(544, 84);
            this.bDLL.Name = "bDLL";
            this.bDLL.Size = new System.Drawing.Size(42, 16);
            this.bDLL.TabIndex = 8;
            this.bDLL.Text = "DLL";
            this.bDLL.UseVisualStyleBackColor = true;
            // 
            // frmGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 633);
            this.Controls.Add(this.bDLL);
            this.Controls.Add(this.bWeb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFactoryAssembly);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAssembly);
            this.Controls.Add(this.cBoxTable);
            this.Name = "frmGenerator";
            this.ShowInTaskbar = false;
            this.Text = "Generator";
            this.Load += new System.EventHandler(this.frmGenerator_Load);
            this.tabControl.ResumeLayout(false);
            this.tpFilter.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpFilter;
        private System.Windows.Forms.TextBox txtAssembly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBoxTable;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtReaderFactory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFactoryAssembly;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.CheckBox bWeb;
        private System.Windows.Forms.CheckBox bDLL;
    }
}

