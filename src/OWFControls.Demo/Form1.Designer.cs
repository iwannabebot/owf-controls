
namespace OWFControls.Demo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.owfGrid1 = new OWFControls.DataGrid.OWFGrid();
            this.SuspendLayout();
            // 
            // owfGrid1
            // 
            this.owfGrid1.AutoScroll = true;
            this.owfGrid1.AutoScrollMinSize = new System.Drawing.Size(480, 130);
            this.owfGrid1.Cols = 5;
            this.owfGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.owfGrid1.Editor = null;
            this.owfGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.owfGrid1.FrozenRows = 1;
            this.owfGrid1.Location = new System.Drawing.Point(0, 0);
            this.owfGrid1.Name = "owfGrid1";
            this.owfGrid1.RowHeaderVisible = true;
            this.owfGrid1.Rows = 4;
            this.owfGrid1.Size = new System.Drawing.Size(847, 375);
            this.owfGrid1.TabIndex = 0;
            this.owfGrid1.Text = "owfGrid1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 375);
            this.Controls.Add(this.owfGrid1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DataGrid.OWFGrid owfGrid1;
    }
}

