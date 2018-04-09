namespace FangyouBackup
{
    partial class FormSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonConnection = new System.Windows.Forms.Button();
            this.textBoxDBPwd = new System.Windows.Forms.TextBox();
            this.textBoxDbUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDatabase = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelSavePath = new System.Windows.Forms.Label();
            this.buttonChangePath = new System.Windows.Forms.Button();
            this.numericUpDownBackupTime = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownLocalKeepDay = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBackupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLocalKeepDay)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(454, 298);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(79, 25);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(369, 298);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(79, 25);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "2.保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonConnection);
            this.groupBox1.Controls.Add(this.textBoxDBPwd);
            this.groupBox1.Controls.Add(this.textBoxDbUser);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxDatabase);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 95);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库相关";
            // 
            // buttonConnection
            // 
            this.buttonConnection.Location = new System.Drawing.Point(340, 51);
            this.buttonConnection.Name = "buttonConnection";
            this.buttonConnection.Size = new System.Drawing.Size(175, 23);
            this.buttonConnection.TabIndex = 14;
            this.buttonConnection.Text = "1.联接数据库";
            this.buttonConnection.UseVisualStyleBackColor = true;
            this.buttonConnection.Click += new System.EventHandler(this.buttonConnection_Click);
            // 
            // textBoxDBPwd
            // 
            this.textBoxDBPwd.Location = new System.Drawing.Point(340, 20);
            this.textBoxDBPwd.Name = "textBoxDBPwd";
            this.textBoxDBPwd.PasswordChar = '*';
            this.textBoxDBPwd.Size = new System.Drawing.Size(175, 25);
            this.textBoxDBPwd.TabIndex = 13;
            // 
            // textBoxDbUser
            // 
            this.textBoxDbUser.Location = new System.Drawing.Point(109, 20);
            this.textBoxDbUser.Name = "textBoxDbUser";
            this.textBoxDbUser.Size = new System.Drawing.Size(162, 25);
            this.textBoxDbUser.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "用户名：";
            // 
            // comboBoxDatabase
            // 
            this.comboBoxDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatabase.FormattingEnabled = true;
            this.comboBoxDatabase.Location = new System.Drawing.Point(109, 52);
            this.comboBoxDatabase.Name = "comboBoxDatabase";
            this.comboBoxDatabase.Size = new System.Drawing.Size(162, 23);
            this.comboBoxDatabase.TabIndex = 9;
            this.comboBoxDatabase.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabase_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据库名称：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelSavePath);
            this.groupBox2.Controls.Add(this.buttonChangePath);
            this.groupBox2.Controls.Add(this.numericUpDownBackupTime);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.numericUpDownLocalKeepDay);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(13, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(520, 179);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数相关";
            // 
            // labelSavePath
            // 
            this.labelSavePath.AutoSize = true;
            this.labelSavePath.Location = new System.Drawing.Point(127, 31);
            this.labelSavePath.Name = "labelSavePath";
            this.labelSavePath.Size = new System.Drawing.Size(0, 15);
            this.labelSavePath.TabIndex = 10;
            // 
            // buttonChangePath
            // 
            this.buttonChangePath.Location = new System.Drawing.Point(427, 119);
            this.buttonChangePath.Name = "buttonChangePath";
            this.buttonChangePath.Size = new System.Drawing.Size(80, 23);
            this.buttonChangePath.TabIndex = 9;
            this.buttonChangePath.Text = "变更路径";
            this.buttonChangePath.UseVisualStyleBackColor = true;
            this.buttonChangePath.Click += new System.EventHandler(this.buttonChangePath_Click);
            // 
            // numericUpDownBackupTime
            // 
            this.numericUpDownBackupTime.Location = new System.Drawing.Point(434, 148);
            this.numericUpDownBackupTime.Name = "numericUpDownBackupTime";
            this.numericUpDownBackupTime.Size = new System.Drawing.Size(73, 25);
            this.numericUpDownBackupTime.TabIndex = 6;
            this.numericUpDownBackupTime.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(282, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "备份时间(每日)：";
            // 
            // numericUpDownLocalKeepDay
            // 
            this.numericUpDownLocalKeepDay.Location = new System.Drawing.Point(133, 146);
            this.numericUpDownLocalKeepDay.Name = "numericUpDownLocalKeepDay";
            this.numericUpDownLocalKeepDay.Size = new System.Drawing.Size(73, 25);
            this.numericUpDownLocalKeepDay.TabIndex = 2;
            this.numericUpDownLocalKeepDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "本地保留(天)：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "保存路径：";
            // 
            // FormSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 335);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetup";
            this.Text = "配置";
            this.Load += new System.EventHandler(this.FormSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBackupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLocalKeepDay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxDBPwd;
        private System.Windows.Forms.TextBox textBoxDbUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownLocalKeepDay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonConnection;
        private System.Windows.Forms.NumericUpDown numericUpDownBackupTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonChangePath;
        private System.Windows.Forms.Label labelSavePath;
    }
}