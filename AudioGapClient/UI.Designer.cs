namespace AudioGap.Client
{
    partial class UI
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
            this.ConnectButton = new System.Windows.Forms.Button();
            this.AudioDeviceList = new System.Windows.Forms.ComboBox();
            this.listName = new System.Windows.Forms.Label();
            this.serverIPlabel = new System.Windows.Forms.Label();
            this.ConStats = new System.Windows.Forms.Label();
            this.ServerIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CodecListBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(573, 90);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(101, 23);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Start Streaming";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // AudioDeviceList
            // 
            this.AudioDeviceList.FormattingEnabled = true;
            this.AudioDeviceList.Location = new System.Drawing.Point(89, 35);
            this.AudioDeviceList.Name = "AudioDeviceList";
            this.AudioDeviceList.Size = new System.Drawing.Size(585, 21);
            this.AudioDeviceList.TabIndex = 2;
            // 
            // listName
            // 
            this.listName.AutoSize = true;
            this.listName.Location = new System.Drawing.Point(9, 38);
            this.listName.Name = "listName";
            this.listName.Size = new System.Drawing.Size(74, 13);
            this.listName.TabIndex = 3;
            this.listName.Text = "Audio Device:";
            // 
            // serverIPlabel
            // 
            this.serverIPlabel.AutoSize = true;
            this.serverIPlabel.Location = new System.Drawing.Point(29, 15);
            this.serverIPlabel.Name = "serverIPlabel";
            this.serverIPlabel.Size = new System.Drawing.Size(54, 13);
            this.serverIPlabel.TabIndex = 4;
            this.serverIPlabel.Text = "Server IP:";
            // 
            // ConStats
            // 
            this.ConStats.AutoSize = true;
            this.ConStats.Location = new System.Drawing.Point(9, 95);
            this.ConStats.Name = "ConStats";
            this.ConStats.Size = new System.Drawing.Size(86, 13);
            this.ConStats.TabIndex = 5;
            this.ConStats.Text = "Connection stats";
            // 
            // ServerIP
            // 
            this.ServerIP.Location = new System.Drawing.Point(89, 12);
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.Size = new System.Drawing.Size(585, 20);
            this.ServerIP.TabIndex = 6;
            this.ServerIP.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Codec:";
            // 
            // CodecListBox
            // 
            this.CodecListBox.FormattingEnabled = true;
            this.CodecListBox.Location = new System.Drawing.Point(89, 62);
            this.CodecListBox.Name = "CodecListBox";
            this.CodecListBox.Size = new System.Drawing.Size(585, 21);
            this.CodecListBox.TabIndex = 7;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 118);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CodecListBox);
            this.Controls.Add(this.ServerIP);
            this.Controls.Add(this.ConStats);
            this.Controls.Add(this.serverIPlabel);
            this.Controls.Add(this.listName);
            this.Controls.Add(this.AudioDeviceList);
            this.Controls.Add(this.ConnectButton);
            this.Name = "UI";
            this.Text = "UI";
            this.Load += new System.EventHandler(this.UI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.ComboBox AudioDeviceList;
        private System.Windows.Forms.Label listName;
        private System.Windows.Forms.Label serverIPlabel;
        private System.Windows.Forms.Label ConStats;
        private System.Windows.Forms.TextBox ServerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CodecListBox;
    }
}