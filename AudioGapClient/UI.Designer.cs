namespace AudioGapClient
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
            this.button1 = new System.Windows.Forms.Button();
            this.DiscoveredList = new System.Windows.Forms.ListBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listName = new System.Windows.Forms.Label();
            this.discoveredListLabel = new System.Windows.Forms.Label();
            this.ConStats = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(573, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start Streaming";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // DiscoveredList
            // 
            this.DiscoveredList.FormattingEnabled = true;
            this.DiscoveredList.Location = new System.Drawing.Point(12, 25);
            this.DiscoveredList.Name = "DiscoveredList";
            this.DiscoveredList.Size = new System.Drawing.Size(662, 95);
            this.DiscoveredList.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(89, 126);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(585, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // listName
            // 
            this.listName.AutoSize = true;
            this.listName.Location = new System.Drawing.Point(9, 129);
            this.listName.Name = "listName";
            this.listName.Size = new System.Drawing.Size(74, 13);
            this.listName.TabIndex = 3;
            this.listName.Text = "Audio Device:";
            // 
            // discoveredListLabel
            // 
            this.discoveredListLabel.AutoSize = true;
            this.discoveredListLabel.Location = new System.Drawing.Point(9, 9);
            this.discoveredListLabel.Name = "discoveredListLabel";
            this.discoveredListLabel.Size = new System.Drawing.Size(101, 13);
            this.discoveredListLabel.TabIndex = 4;
            this.discoveredListLabel.Text = "Discovered servers:";
            // 
            // ConStats
            // 
            this.ConStats.AutoSize = true;
            this.ConStats.Location = new System.Drawing.Point(9, 163);
            this.ConStats.Name = "ConStats";
            this.ConStats.Size = new System.Drawing.Size(86, 13);
            this.ConStats.TabIndex = 5;
            this.ConStats.Text = "Connection stats";
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 180);
            this.Controls.Add(this.ConStats);
            this.Controls.Add(this.discoveredListLabel);
            this.Controls.Add(this.listName);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.DiscoveredList);
            this.Controls.Add(this.button1);
            this.Name = "UI";
            this.Text = "UI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox DiscoveredList;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label listName;
        private System.Windows.Forms.Label discoveredListLabel;
        private System.Windows.Forms.Label ConStats;
    }
}