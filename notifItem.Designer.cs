﻿namespace WashablesSystem
{
    partial class notifItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.activityText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // activityText
            // 
            this.activityText.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.activityText.Location = new System.Drawing.Point(19, 8);
            this.activityText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.activityText.MaximumSize = new System.Drawing.Size(517, 30);
            this.activityText.Name = "activityText";
            this.activityText.Size = new System.Drawing.Size(493, 30);
            this.activityText.TabIndex = 0;
            this.activityText.Text = "activity";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(122)))), ((int)(((byte)(69)))));
            this.panel1.Location = new System.Drawing.Point(-4, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(15, 35);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(513, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "________________________________________________________";
            // 
            // notifItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.activityText);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(517, 50);
            this.MinimumSize = new System.Drawing.Size(517, 50);
            this.Name = "notifItem";
            this.Size = new System.Drawing.Size(517, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label activityText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}