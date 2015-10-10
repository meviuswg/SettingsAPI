namespace SettingsManager
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.columnApplicationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnApplicationDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnApplicationVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnApplicationStore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnApplicationName,
            this.columnApplicationDescription,
            this.columnApplicationVersion,
            this.columnApplicationStore});
            this.dataGridView1.Location = new System.Drawing.Point(2, 52);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(535, 308);
            this.dataGridView1.TabIndex = 0;
            // 
            // columnApplicationName
            // 
            this.columnApplicationName.HeaderText = "Application";
            this.columnApplicationName.Name = "columnApplicationName";
            this.columnApplicationName.ReadOnly = true;
            // 
            // columnApplicationDescription
            // 
            this.columnApplicationDescription.HeaderText = "Description";
            this.columnApplicationDescription.Name = "columnApplicationDescription";
            this.columnApplicationDescription.ReadOnly = true;
            // 
            // columnApplicationVersion
            // 
            this.columnApplicationVersion.HeaderText = "Version";
            this.columnApplicationVersion.Name = "columnApplicationVersion";
            this.columnApplicationVersion.ReadOnly = true;
            // 
            // columnApplicationStore
            // 
            this.columnApplicationStore.HeaderText = "Store";
            this.columnApplicationStore.Name = "columnApplicationStore";
            this.columnApplicationStore.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(442, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 363);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnApplicationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnApplicationDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnApplicationVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnApplicationStore;
        private System.Windows.Forms.Button button1;
    }
}

