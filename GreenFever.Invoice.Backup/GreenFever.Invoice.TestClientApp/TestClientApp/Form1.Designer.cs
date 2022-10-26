namespace TestClientApp
{
    public partial class Form1
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.btnRunSingle = new System.Windows.Forms.Button();
            this.lblPercentInfo = new System.Windows.Forms.Label();
            this.pbInvoices = new System.Windows.Forms.ProgressBar();
            this.btnRunBatch = new System.Windows.Forms.Button();
            this.btnExecuteGrouping = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRunSingle
            // 
            this.btnRunSingle.Location = new System.Drawing.Point(173, 12);
            this.btnRunSingle.Name = "btnRunSingle";
            this.btnRunSingle.Size = new System.Drawing.Size(146, 36);
            this.btnRunSingle.TabIndex = 1;
            this.btnRunSingle.Text = "Start single processing";
            this.btnRunSingle.UseVisualStyleBackColor = true;
            this.btnRunSingle.Click += new System.EventHandler(this.btnRunSingle_Click);
            // 
            // lblPercentInfo
            // 
            this.lblPercentInfo.AutoSize = true;
            this.lblPercentInfo.Location = new System.Drawing.Point(21, 111);
            this.lblPercentInfo.Name = "lblPercentInfo";
            this.lblPercentInfo.Size = new System.Drawing.Size(111, 13);
            this.lblPercentInfo.TabIndex = 2;
            this.lblPercentInfo.Text = "0 points from 100 (0%)";
            this.lblPercentInfo.Visible = false;
            // 
            // pbInvoices
            // 
            this.pbInvoices.Location = new System.Drawing.Point(14, 63);
            this.pbInvoices.MarqueeAnimationSpeed = 50;
            this.pbInvoices.Name = "pbInvoices";
            this.pbInvoices.Size = new System.Drawing.Size(305, 36);
            this.pbInvoices.TabIndex = 1;
            this.pbInvoices.Visible = false;
            // 
            // btnRunBatch
            // 
            this.btnRunBatch.Location = new System.Drawing.Point(12, 12);
            this.btnRunBatch.Name = "btnRunBatch";
            this.btnRunBatch.Size = new System.Drawing.Size(146, 36);
            this.btnRunBatch.TabIndex = 0;
            this.btnRunBatch.Text = "Start batch processing";
            this.btnRunBatch.UseVisualStyleBackColor = true;
            this.btnRunBatch.Click += new System.EventHandler(this.btnRunBatch_Click);
            // 
            // btnExecuteGrouping
            // 
            this.btnExecuteGrouping.Location = new System.Drawing.Point(78, 139);
            this.btnExecuteGrouping.Name = "btnExecuteGrouping";
            this.btnExecuteGrouping.Size = new System.Drawing.Size(146, 36);
            this.btnExecuteGrouping.TabIndex = 4;
            this.btnExecuteGrouping.Text = "Execute grouping";
            this.btnExecuteGrouping.UseVisualStyleBackColor = true;
            this.btnExecuteGrouping.Click += new System.EventHandler(this.btnExecuteGrouping_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 186);
            this.Controls.Add(this.btnExecuteGrouping);
            this.Controls.Add(this.btnRunSingle);
            this.Controls.Add(this.lblPercentInfo);
            this.Controls.Add(this.btnRunBatch);
            this.Controls.Add(this.pbInvoices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRunBatch;
        private System.Windows.Forms.Label lblPercentInfo;
        private System.Windows.Forms.ProgressBar pbInvoices;
        private System.Windows.Forms.Button btnRunSingle;
        private System.Windows.Forms.Button btnExecuteGrouping;
    }
}