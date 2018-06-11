// <copyright file="NewAttachDialog.cs" company="Microsoft">
// Copyright Microsoft 2010
// </copyright>
namespace Outlook.CustomAttachment
{

    public partial class NewAttachDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private DiblyDeluxe diblyDeluxe;
        private System.Windows.Forms.CheckBox insertAsLink;
        private System.Windows.Forms.CheckBox insertAsCopy;

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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.insertAsLink = new System.Windows.Forms.CheckBox();
            this.insertAsCopy = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.diblyDeluxe = new DiblyDeluxe();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(545, 513);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(626, 513);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // insertAsLink
            // 
            this.insertAsLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.insertAsLink.AutoSize = true;
            this.insertAsLink.Checked = true;
            this.insertAsLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.insertAsLink.Location = new System.Drawing.Point(129, 517);
            this.insertAsLink.Name = "insertAsLink";
            this.insertAsLink.Size = new System.Drawing.Size(46, 17);
            this.insertAsLink.TabIndex = 4;
            this.insertAsLink.Text = "Link";
            this.insertAsLink.UseVisualStyleBackColor = true;
            // 
            // insertAsCopy
            // 
            this.insertAsCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.insertAsCopy.AutoSize = true;
            this.insertAsCopy.Checked = true;
            this.insertAsCopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.insertAsCopy.Location = new System.Drawing.Point(73, 517);
            this.insertAsCopy.Name = "insertAsCopy";
            this.insertAsCopy.Size = new System.Drawing.Size(50, 17);
            this.insertAsCopy.TabIndex = 3;
            this.insertAsCopy.Text = "Copy";
            this.insertAsCopy.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 518);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Insert as:";
            // 
            // diblyDeluxe
            // 
            this.diblyDeluxe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.diblyDeluxe.Location = new System.Drawing.Point(1, 1);
            this.diblyDeluxe.MultiselectEnabled = true;
            this.diblyDeluxe.Name = "diblyDeluxe";
            this.diblyDeluxe.Size = new System.Drawing.Size(713, 501);
            this.diblyDeluxe.TabIndex = 0;
            this.diblyDeluxe.VersionsEnabled = true;
            this.diblyDeluxe.FileDoubleClicked += new System.EventHandler(this.DiblyDeluxe_FileDoubleClicked);
            this.diblyDeluxe.SelectionChanged += new System.EventHandler(this.DiblyDeluxe_SelectionChanged);
            // 
            // NewAttachDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(713, 547);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.insertAsCopy);
            this.Controls.Add(this.insertAsLink);
            this.Controls.Add(this.diblyDeluxe);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "NewAttachDialog";
            this.Text = "Attach file from SharePoint";
            this.Load += new System.EventHandler(this.NewAttachDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
    }
}