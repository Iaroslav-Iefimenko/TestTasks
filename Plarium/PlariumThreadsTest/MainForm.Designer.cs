namespace PlariumThreadsTest
{
    partial class MainForm
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
            this.lblDir = new System.Windows.Forms.Label();
            this.tbDir = new System.Windows.Forms.TextBox();
            this.btnDirSelect = new System.Windows.Forms.Button();
            this.btnResFileSave = new System.Windows.Forms.Button();
            this.tbResFile = new System.Windows.Forms.TextBox();
            this.lblResFile = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
            this.sfdResFile = new System.Windows.Forms.SaveFileDialog();
            this.pnlRes = new System.Windows.Forms.Panel();
            this.tvRes = new System.Windows.Forms.TreeView();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbFullName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDateOfCreate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDateOfModification = new System.Windows.Forms.TextBox();
            this.tbSizeInBytes = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDateOfLastAccess = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlRes.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Location = new System.Drawing.Point(13, 13);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(90, 13);
            this.lblDir.TabIndex = 0;
            this.lblDir.Text = "Directory for scan";
            // 
            // tbDir
            // 
            this.tbDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDir.Location = new System.Drawing.Point(16, 31);
            this.tbDir.Name = "tbDir";
            this.tbDir.ReadOnly = true;
            this.tbDir.Size = new System.Drawing.Size(564, 20);
            this.tbDir.TabIndex = 1;
            // 
            // btnDirSelect
            // 
            this.btnDirSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDirSelect.Location = new System.Drawing.Point(586, 30);
            this.btnDirSelect.Name = "btnDirSelect";
            this.btnDirSelect.Size = new System.Drawing.Size(75, 23);
            this.btnDirSelect.TabIndex = 2;
            this.btnDirSelect.Text = "Select...";
            this.btnDirSelect.UseVisualStyleBackColor = true;
            this.btnDirSelect.Click += new System.EventHandler(this.btnDirSelect_Click);
            // 
            // btnResFileSave
            // 
            this.btnResFileSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResFileSave.Location = new System.Drawing.Point(586, 71);
            this.btnResFileSave.Name = "btnResFileSave";
            this.btnResFileSave.Size = new System.Drawing.Size(75, 23);
            this.btnResFileSave.TabIndex = 5;
            this.btnResFileSave.Text = "Save...";
            this.btnResFileSave.UseVisualStyleBackColor = true;
            this.btnResFileSave.Click += new System.EventHandler(this.btnResFileSave_Click);
            // 
            // tbResFile
            // 
            this.tbResFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbResFile.Location = new System.Drawing.Point(15, 72);
            this.tbResFile.Name = "tbResFile";
            this.tbResFile.ReadOnly = true;
            this.tbResFile.Size = new System.Drawing.Size(564, 20);
            this.tbResFile.TabIndex = 4;
            // 
            // lblResFile
            // 
            this.lblResFile.AutoSize = true;
            this.lblResFile.Location = new System.Drawing.Point(12, 54);
            this.lblResFile.Name = "lblResFile";
            this.lblResFile.Size = new System.Drawing.Size(53, 13);
            this.lblResFile.TabIndex = 3;
            this.lblResFile.Text = "Result file";
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.Location = new System.Drawing.Point(136, 98);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(404, 34);
            this.btnScan.TabIndex = 6;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // sfdResFile
            // 
            this.sfdResFile.DefaultExt = "*.xml";
            this.sfdResFile.Filter = "XML files|*.xml";
            this.sfdResFile.Title = "Save scan results";
            // 
            // pnlRes
            // 
            this.pnlRes.Controls.Add(this.pnlDetail);
            this.pnlRes.Controls.Add(this.tvRes);
            this.pnlRes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRes.Location = new System.Drawing.Point(0, 138);
            this.pnlRes.Name = "pnlRes";
            this.pnlRes.Size = new System.Drawing.Size(685, 259);
            this.pnlRes.TabIndex = 8;
            // 
            // tvRes
            // 
            this.tvRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRes.HideSelection = false;
            this.tvRes.Location = new System.Drawing.Point(0, 0);
            this.tvRes.Name = "tvRes";
            this.tvRes.Size = new System.Drawing.Size(685, 259);
            this.tvRes.TabIndex = 8;
            this.tvRes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRes_AfterSelect);
            // 
            // pnlDetail
            // 
            this.pnlDetail.AutoScroll = true;
            this.pnlDetail.Controls.Add(this.tbSizeInBytes);
            this.pnlDetail.Controls.Add(this.label5);
            this.pnlDetail.Controls.Add(this.tbDateOfLastAccess);
            this.pnlDetail.Controls.Add(this.label6);
            this.pnlDetail.Controls.Add(this.tbDateOfModification);
            this.pnlDetail.Controls.Add(this.label4);
            this.pnlDetail.Controls.Add(this.tbDateOfCreate);
            this.pnlDetail.Controls.Add(this.label3);
            this.pnlDetail.Controls.Add(this.tbFullName);
            this.pnlDetail.Controls.Add(this.label2);
            this.pnlDetail.Controls.Add(this.tbName);
            this.pnlDetail.Controls.Add(this.label1);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDetail.Location = new System.Drawing.Point(485, 0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(200, 259);
            this.pnlDetail.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(13, 21);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(160, 20);
            this.tbName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Full name";
            // 
            // tbFullName
            // 
            this.tbFullName.Location = new System.Drawing.Point(13, 62);
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.ReadOnly = true;
            this.tbFullName.Size = new System.Drawing.Size(160, 20);
            this.tbFullName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Date of create";
            // 
            // tbDateOfCreate
            // 
            this.tbDateOfCreate.Location = new System.Drawing.Point(13, 103);
            this.tbDateOfCreate.Name = "tbDateOfCreate";
            this.tbDateOfCreate.ReadOnly = true;
            this.tbDateOfCreate.Size = new System.Drawing.Size(160, 20);
            this.tbDateOfCreate.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Date of modification";
            // 
            // tbDateOfModification
            // 
            this.tbDateOfModification.Location = new System.Drawing.Point(13, 144);
            this.tbDateOfModification.Name = "tbDateOfModification";
            this.tbDateOfModification.ReadOnly = true;
            this.tbDateOfModification.Size = new System.Drawing.Size(160, 20);
            this.tbDateOfModification.TabIndex = 7;
            // 
            // tbSizeInBytes
            // 
            this.tbSizeInBytes.Location = new System.Drawing.Point(13, 228);
            this.tbSizeInBytes.Name = "tbSizeInBytes";
            this.tbSizeInBytes.ReadOnly = true;
            this.tbSizeInBytes.Size = new System.Drawing.Size(160, 20);
            this.tbSizeInBytes.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Size in bytes";
            // 
            // tbDateOfLastAccess
            // 
            this.tbDateOfLastAccess.Location = new System.Drawing.Point(13, 187);
            this.tbDateOfLastAccess.Name = "tbDateOfLastAccess";
            this.tbDateOfLastAccess.ReadOnly = true;
            this.tbDateOfLastAccess.Size = new System.Drawing.Size(160, 20);
            this.tbDateOfLastAccess.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Date of last access";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 397);
            this.Controls.Add(this.pnlRes);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnResFileSave);
            this.Controls.Add(this.tbResFile);
            this.Controls.Add(this.lblResFile);
            this.Controls.Add(this.btnDirSelect);
            this.Controls.Add(this.tbDir);
            this.Controls.Add(this.lblDir);
            this.Name = "MainForm";
            this.Text = "Directory scaner";
            this.pnlRes.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDir;
        private System.Windows.Forms.TextBox tbDir;
        private System.Windows.Forms.Button btnDirSelect;
        private System.Windows.Forms.Button btnResFileSave;
        private System.Windows.Forms.TextBox tbResFile;
        private System.Windows.Forms.Label lblResFile;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.FolderBrowserDialog fbdDir;
        private System.Windows.Forms.SaveFileDialog sfdResFile;
        private System.Windows.Forms.Panel pnlRes;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.TreeView tvRes;
        private System.Windows.Forms.TextBox tbFullName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDateOfModification;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDateOfCreate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSizeInBytes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDateOfLastAccess;
        private System.Windows.Forms.Label label6;
    }
}

