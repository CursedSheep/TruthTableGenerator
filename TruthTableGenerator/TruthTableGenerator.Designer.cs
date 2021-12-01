namespace TruthTableGenerator
{
    partial class TruthTableGenerator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TruthTableListView = new System.Windows.Forms.ListView();
            this.formulaTxt = new System.Windows.Forms.TextBox();
            this.programStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TruthTableListView
            // 
            this.TruthTableListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TruthTableListView.BackgroundImageTiled = true;
            this.TruthTableListView.FullRowSelect = true;
            this.TruthTableListView.GridLines = true;
            this.TruthTableListView.Location = new System.Drawing.Point(12, 43);
            this.TruthTableListView.Name = "TruthTableListView";
            this.TruthTableListView.Size = new System.Drawing.Size(776, 312);
            this.TruthTableListView.TabIndex = 0;
            this.TruthTableListView.UseCompatibleStateImageBehavior = false;
            this.TruthTableListView.View = System.Windows.Forms.View.Details;
            this.TruthTableListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.TruthTableListView_ColumnWidthChanging);
            // 
            // formulaTxt
            // 
            this.formulaTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formulaTxt.Location = new System.Drawing.Point(12, 14);
            this.formulaTxt.Name = "formulaTxt";
            this.formulaTxt.PlaceholderText = "Enter formula";
            this.formulaTxt.Size = new System.Drawing.Size(776, 23);
            this.formulaTxt.TabIndex = 1;
            this.formulaTxt.TextChanged += new System.EventHandler(this.formulaTxt_TextChanged);
            // 
            // programStatus
            // 
            this.programStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.programStatus.Location = new System.Drawing.Point(0, 358);
            this.programStatus.Name = "programStatus";
            this.programStatus.Size = new System.Drawing.Size(800, 25);
            this.programStatus.TabIndex = 2;
            this.programStatus.Text = "Status: Running";
            // 
            // TruthTableGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 383);
            this.Controls.Add(this.programStatus);
            this.Controls.Add(this.formulaTxt);
            this.Controls.Add(this.TruthTableListView);
            this.Name = "TruthTableGenerator";
            this.Text = "Truth Table Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView TruthTableListView;
        private TextBox formulaTxt;
        private Label programStatus;
    }
}