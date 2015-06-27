namespace Popup_Dictionairy
{
    partial class PreferencesForm
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
            this.pfLblInterval = new System.Windows.Forms.Label();
            this.pfTxtInterval = new System.Windows.Forms.TextBox();
            this.pfButtonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pfLblInterval
            // 
            this.pfLblInterval.AutoSize = true;
            this.pfLblInterval.Location = new System.Drawing.Point(13, 13);
            this.pfLblInterval.Name = "pfLblInterval";
            this.pfLblInterval.Size = new System.Drawing.Size(145, 17);
            this.pfLblInterval.TabIndex = 0;
            this.pfLblInterval.Text = "Question interval (seconds):";
            this.pfLblInterval.UseCompatibleTextRendering = true;
            // 
            // pfTxtInterval
            // 
            this.pfTxtInterval.Location = new System.Drawing.Point(187, 10);
            this.pfTxtInterval.Name = "pfTxtInterval";
            this.pfTxtInterval.Size = new System.Drawing.Size(100, 20);
            this.pfTxtInterval.TabIndex = 1;
            // 
            // pfButtonOK
            // 
            this.pfButtonOK.Location = new System.Drawing.Point(187, 216);
            this.pfButtonOK.Name = "pfButtonOK";
            this.pfButtonOK.Size = new System.Drawing.Size(75, 23);
            this.pfButtonOK.TabIndex = 2;
            this.pfButtonOK.Text = "OK";
            this.pfButtonOK.UseVisualStyleBackColor = true;
            this.pfButtonOK.Click += new System.EventHandler(this.pfButtonOK_Click);
            // 
            // PreferencesForm
            // 
            this.AcceptButton = this.pfButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 261);
            this.Controls.Add(this.pfButtonOK);
            this.Controls.Add(this.pfTxtInterval);
            this.Controls.Add(this.pfLblInterval);
            this.Name = "PreferencesForm";
            this.Text = "Preferences";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pfLblInterval;
        private System.Windows.Forms.TextBox pfTxtInterval;
        private System.Windows.Forms.Button pfButtonOK;
    }
}