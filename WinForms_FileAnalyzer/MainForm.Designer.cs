namespace WinForms_FileAnalyzer
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
            this.split = new System.Windows.Forms.SplitContainer();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.dgvWords = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvPunct = new System.Windows.Forms.DataGridView();
            this.lblFile = new System.Windows.Forms.Label();
            this.cboFileType = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.lblSummary = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWords)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPunct)).BeginInit();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 107);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.lblSummary);
            this.split.Panel1.Controls.Add(this.progressBar);
            this.split.Panel1.Controls.Add(this.dgvWords);
            this.split.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.statusStrip1);
            this.split.Panel2.Controls.Add(this.dgvPunct);
            this.split.Size = new System.Drawing.Size(1571, 548);
            this.split.SplitterDistance = 733;
            this.split.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 525);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(733, 23);
            this.progressBar.TabIndex = 1;
            // 
            // dgvWords
            // 
            this.dgvWords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWords.Location = new System.Drawing.Point(357, 148);
            this.dgvWords.Name = "dgvWords";
            this.dgvWords.Size = new System.Drawing.Size(240, 150);
            this.dgvWords.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 526);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(834, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(34, 17);
            this.statusLabel.Text = "Hazır";
            // 
            // dgvPunct
            // 
            this.dgvPunct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPunct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPunct.Location = new System.Drawing.Point(129, 148);
            this.dgvPunct.Name = "dgvPunct";
            this.dgvPunct.Size = new System.Drawing.Size(240, 150);
            this.dgvPunct.TabIndex = 0;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(33, 44);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(53, 13);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "File Type:";
            // 
            // cboFileType
            // 
            this.cboFileType.FormattingEnabled = true;
            this.cboFileType.Location = new System.Drawing.Point(92, 41);
            this.cboFileType.Name = "cboFileType";
            this.cboFileType.Size = new System.Drawing.Size(122, 21);
            this.cboFileType.TabIndex = 1;
            this.cboFileType.SelectedIndexChanged += new System.EventHandler(this.cboFileType_SelectedIndexChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Enabled = false;
            this.btnLoad.Location = new System.Drawing.Point(236, 39);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Yükle";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.btnLoad);
            this.topPanel.Controls.Add(this.cboFileType);
            this.topPanel.Controls.Add(this.lblFile);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1571, 107);
            this.topPanel.TabIndex = 0;
            this.topPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.topPanel_Paint);
            // 
            // lblSummary
            // 
            this.lblSummary.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(679, 91);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(0, 13);
            this.lblSummary.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1571, 655);
            this.Controls.Add(this.split);
            this.Controls.Add(this.topPanel);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel1.PerformLayout();
            this.split.Panel2.ResumeLayout(false);
            this.split.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWords)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPunct)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.ComboBox cboFileType;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.DataGridView dgvWords;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dgvPunct;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblSummary;
    }
}

