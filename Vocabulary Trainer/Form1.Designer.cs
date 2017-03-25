namespace Vocabulary_Database_Processing
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
            this.rtbDisplay = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbFileIn = new System.Windows.Forms.TextBox();
            this.tbFileOut = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateRelation = new System.Windows.Forms.Button();
            this.btnBrowseIn = new System.Windows.Forms.Button();
            this.BrowseOut = new System.Windows.Forms.Button();
            this.btnAddWord = new System.Windows.Forms.Button();
            this.btnInfDer = new System.Windows.Forms.Button();
            this.chlbOption = new System.Windows.Forms.CheckedListBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbDisplay
            // 
            this.rtbDisplay.Location = new System.Drawing.Point(25, 167);
            this.rtbDisplay.Name = "rtbDisplay";
            this.rtbDisplay.Size = new System.Drawing.Size(860, 508);
            this.rtbDisplay.TabIndex = 0;
            this.rtbDisplay.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(892, 317);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "btnRun";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1048, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // tbFileIn
            // 
            this.tbFileIn.Location = new System.Drawing.Point(119, 44);
            this.tbFileIn.Name = "tbFileIn";
            this.tbFileIn.Size = new System.Drawing.Size(288, 20);
            this.tbFileIn.TabIndex = 3;
            // 
            // tbFileOut
            // 
            this.tbFileOut.Location = new System.Drawing.Point(119, 82);
            this.tbFileOut.Name = "tbFileOut";
            this.tbFileOut.Size = new System.Drawing.Size(288, 20);
            this.tbFileOut.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "File in ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "File out";
            // 
            // btnCreateRelation
            // 
            this.btnCreateRelation.Location = new System.Drawing.Point(891, 264);
            this.btnCreateRelation.Name = "btnCreateRelation";
            this.btnCreateRelation.Size = new System.Drawing.Size(144, 30);
            this.btnCreateRelation.TabIndex = 7;
            this.btnCreateRelation.Text = "Create relation";
            this.btnCreateRelation.UseVisualStyleBackColor = true;
            this.btnCreateRelation.Click += new System.EventHandler(this.btnCreateRelation_Click);
            // 
            // btnBrowseIn
            // 
            this.btnBrowseIn.Location = new System.Drawing.Point(413, 37);
            this.btnBrowseIn.Name = "btnBrowseIn";
            this.btnBrowseIn.Size = new System.Drawing.Size(90, 32);
            this.btnBrowseIn.TabIndex = 8;
            this.btnBrowseIn.Text = "Browse";
            this.btnBrowseIn.UseVisualStyleBackColor = true;
            this.btnBrowseIn.Click += new System.EventHandler(this.btnBrowseIn_Click);
            // 
            // BrowseOut
            // 
            this.BrowseOut.Location = new System.Drawing.Point(413, 76);
            this.BrowseOut.Name = "BrowseOut";
            this.BrowseOut.Size = new System.Drawing.Size(90, 30);
            this.BrowseOut.TabIndex = 9;
            this.BrowseOut.Text = "Browse";
            this.BrowseOut.UseVisualStyleBackColor = true;
            this.BrowseOut.Click += new System.EventHandler(this.BrowseOut_Click);
            // 
            // btnAddWord
            // 
            this.btnAddWord.Location = new System.Drawing.Point(892, 400);
            this.btnAddWord.Name = "btnAddWord";
            this.btnAddWord.Size = new System.Drawing.Size(144, 30);
            this.btnAddWord.TabIndex = 10;
            this.btnAddWord.Text = "Add New Word";
            this.btnAddWord.UseVisualStyleBackColor = true;
            // 
            // btnInfDer
            // 
            this.btnInfDer.Location = new System.Drawing.Point(891, 353);
            this.btnInfDer.Name = "btnInfDer";
            this.btnInfDer.Size = new System.Drawing.Size(144, 30);
            this.btnInfDer.TabIndex = 11;
            this.btnInfDer.Text = "Create Derivative ";
            this.btnInfDer.UseVisualStyleBackColor = true;
            this.btnInfDer.Click += new System.EventHandler(this.btnInfDer_Click);
            // 
            // chlbOption
            // 
            this.chlbOption.FormattingEnabled = true;
            this.chlbOption.Items.AddRange(new object[] {
            "Convert GRE/SAT to XML",
            "Create Infinitive Derivative XML ",
            "Convert WordNet to XML ",
            "Create Infinitive List",
            "Create Single Word Collection",
            "Create MacMillan XML",
            "Create Concise English XML",
            "Experiment"});
            this.chlbOption.Location = new System.Drawing.Point(509, 27);
            this.chlbOption.Name = "chlbOption";
            this.chlbOption.Size = new System.Drawing.Size(202, 124);
            this.chlbOption.TabIndex = 12;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(891, 161);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(144, 30);
            this.btnRun.TabIndex = 13;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 769);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.chlbOption);
            this.Controls.Add(this.btnInfDer);
            this.Controls.Add(this.btnAddWord);
            this.Controls.Add(this.BrowseOut);
            this.Controls.Add(this.btnBrowseIn);
            this.Controls.Add(this.btnCreateRelation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFileOut);
            this.Controls.Add(this.tbFileIn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtbDisplay);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbDisplay;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TextBox tbFileIn;
        private System.Windows.Forms.TextBox tbFileOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateRelation;
        private System.Windows.Forms.Button btnBrowseIn;
        private System.Windows.Forms.Button BrowseOut;
        private System.Windows.Forms.Button btnAddWord;
        private System.Windows.Forms.Button btnInfDer;
        private System.Windows.Forms.CheckedListBox chlbOption;
        private System.Windows.Forms.Button btnRun;
        
    }
}

