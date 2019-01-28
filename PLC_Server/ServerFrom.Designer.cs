namespace PLC_Server
{
    partial class ServerFrom
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerFrom));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.axActEasyIF1 = new AxACTMULTILib.AxActEasyIF();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.axActEasyIF1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "启动服务";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(179, 47);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 37);
            this.button2.TabIndex = 1;
            this.button2.Text = "停止服务";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // axActEasyIF1
            // 
            this.axActEasyIF1.Enabled = true;
            this.axActEasyIF1.Location = new System.Drawing.Point(0, 0);
            this.axActEasyIF1.Name = "axActEasyIF1";
            this.axActEasyIF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axActEasyIF1.OcxState")));
            this.axActEasyIF1.Size = new System.Drawing.Size(0, 0);
            this.axActEasyIF1.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBox1.Location = new System.Drawing.Point(188, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "iLogicalStationNumber";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ServerFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 105);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.axActEasyIF1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ServerFrom";
            this.Text = "DES服务";
            ((System.ComponentModel.ISupportInitialize)(this.axActEasyIF1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private AxACTMULTILib.AxActEasyIF axActEasyIF1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}

