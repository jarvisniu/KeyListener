namespace WinFormDemo
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelSettingState = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonStartSetting = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Try: F1, F5, Ctrl+R";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 16F);
            this.label2.Location = new System.Drawing.Point(14, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Waiting for keys";
            // 
            // labelSettingState
            // 
            this.labelSettingState.AutoSize = true;
            this.labelSettingState.Location = new System.Drawing.Point(16, 283);
            this.labelSettingState.Name = "labelSettingState";
            this.labelSettingState.Size = new System.Drawing.Size(71, 12);
            this.labelSettingState.TabIndex = 2;
            this.labelSettingState.Text = "not setting";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 250);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(189, 21);
            this.textBox1.TabIndex = 3;
            // 
            // buttonStartSetting
            // 
            this.buttonStartSetting.Location = new System.Drawing.Point(211, 248);
            this.buttonStartSetting.Name = "buttonStartSetting";
            this.buttonStartSetting.Size = new System.Drawing.Size(118, 23);
            this.buttonStartSetting.TabIndex = 4;
            this.buttonStartSetting.Text = "Start Setting";
            this.buttonStartSetting.UseVisualStyleBackColor = true;
            this.buttonStartSetting.Click += new System.EventHandler(this.buttonStartSetting_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Set shortcut:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Bind shortcut：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 344);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonStartSetting);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelSettingState);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "KeyListener Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSettingState;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonStartSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

