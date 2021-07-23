namespace createdb
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
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.status = new System.Windows.Forms.Label();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cboServers = new System.Windows.Forms.ComboBox();
            this.cboProjects = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboVersion = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(52, 291);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(131, 36);
            this.cmdUpdate.TabIndex = 0;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(36, 100);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(318, 95);
            this.listBox1.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(36, 201);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(318, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(40, 238);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(314, 28);
            this.status.TabIndex = 3;
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(212, 291);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(131, 36);
            this.cmdExit.TabIndex = 4;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cboServers
            // 
            this.cboServers.FormattingEnabled = true;
            this.cboServers.Location = new System.Drawing.Point(132, 10);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(121, 21);
            this.cboServers.TabIndex = 5;
            // 
            // cboProjects
            // 
            this.cboProjects.FormattingEnabled = true;
            this.cboProjects.Location = new System.Drawing.Point(132, 39);
            this.cboProjects.Name = "cboProjects";
            this.cboProjects.Size = new System.Drawing.Size(121, 21);
            this.cboProjects.TabIndex = 6;
            this.cboProjects.SelectedValueChanged += new System.EventHandler(this.dbChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Project";
            // 
            // cboVersion
            // 
            this.cboVersion.FormattingEnabled = true;
            this.cboVersion.Location = new System.Drawing.Point(132, 66);
            this.cboVersion.Name = "cboVersion";
            this.cboVersion.Size = new System.Drawing.Size(121, 21);
            this.cboVersion.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Schema";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(279, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 39);
            this.button3.TabIndex = 11;
            this.button3.Text = "Refresh data from Pro";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 339);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboProjects);
            this.Controls.Add(this.cboServers);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.status);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.cmdUpdate);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Database";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.ComboBox cboServers;
        private System.Windows.Forms.ComboBox cboProjects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
    }
}

