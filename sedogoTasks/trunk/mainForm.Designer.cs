namespace sedogoTasks
{
    partial class mainForm
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
            this.sendAlertsButton = new System.Windows.Forms.Button();
            this.sendBroadcastEmailButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sendAlertsButton
            // 
            this.sendAlertsButton.Location = new System.Drawing.Point(12, 12);
            this.sendAlertsButton.Name = "sendAlertsButton";
            this.sendAlertsButton.Size = new System.Drawing.Size(164, 23);
            this.sendAlertsButton.TabIndex = 0;
            this.sendAlertsButton.Text = "Send alert emails";
            this.sendAlertsButton.UseVisualStyleBackColor = true;
            this.sendAlertsButton.Click += new System.EventHandler(this.sendAlertsButton_Click);
            // 
            // sendBroadcastEmailButton
            // 
            this.sendBroadcastEmailButton.Location = new System.Drawing.Point(12, 41);
            this.sendBroadcastEmailButton.Name = "sendBroadcastEmailButton";
            this.sendBroadcastEmailButton.Size = new System.Drawing.Size(164, 23);
            this.sendBroadcastEmailButton.TabIndex = 1;
            this.sendBroadcastEmailButton.Text = "Send broadcast email";
            this.sendBroadcastEmailButton.UseVisualStyleBackColor = true;
            this.sendBroadcastEmailButton.Click += new System.EventHandler(this.sendBroadcastEmailButton_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.sendBroadcastEmailButton);
            this.Controls.Add(this.sendAlertsButton);
            this.Name = "mainForm";
            this.Text = "Sedogo tasks";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button sendAlertsButton;
        private System.Windows.Forms.Button sendBroadcastEmailButton;
    }
}

