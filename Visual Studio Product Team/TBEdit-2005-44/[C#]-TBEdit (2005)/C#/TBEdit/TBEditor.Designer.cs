namespace Vsip.TBEdit
{
    partial class TBEditor
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.designPage = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.layoutPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.previewPage = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.designPage.SuspendLayout();
            this.layoutPage.SuspendLayout();
            this.previewPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.designPage);
            this.tabControl.Controls.Add(this.layoutPage);
            this.tabControl.Controls.Add(this.previewPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(431, 465);
            this.tabControl.TabIndex = 0;
            // 
            // designPage
            // 
            this.designPage.Controls.Add(this.button1);
            this.designPage.Location = new System.Drawing.Point(4, 4);
            this.designPage.Name = "designPage";
            this.designPage.Padding = new System.Windows.Forms.Padding(3);
            this.designPage.Size = new System.Drawing.Size(423, 439);
            this.designPage.TabIndex = 0;
            this.designPage.Text = "Design";
            this.designPage.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(101, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 96);
            this.button1.TabIndex = 0;
            this.button1.Text = "The Design Page";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // layoutPage
            // 
            this.layoutPage.Controls.Add(this.label1);
            this.layoutPage.Location = new System.Drawing.Point(4, 4);
            this.layoutPage.Name = "layoutPage";
            this.layoutPage.Padding = new System.Windows.Forms.Padding(3);
            this.layoutPage.Size = new System.Drawing.Size(423, 439);
            this.layoutPage.TabIndex = 1;
            this.layoutPage.Text = "Layout";
            this.layoutPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "The Layout Page";
            // 
            // previewPage
            // 
            this.previewPage.Controls.Add(this.label2);
            this.previewPage.Location = new System.Drawing.Point(4, 4);
            this.previewPage.Name = "previewPage";
            this.previewPage.Size = new System.Drawing.Size(423, 439);
            this.previewPage.TabIndex = 2;
            this.previewPage.Text = "Preview";
            this.previewPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 381);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "The Preview Page";
            // 
            // TBEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "TBEditor";
            this.Size = new System.Drawing.Size(431, 465);
            this.tabControl.ResumeLayout(false);
            this.designPage.ResumeLayout(false);
            this.layoutPage.ResumeLayout(false);
            this.layoutPage.PerformLayout();
            this.previewPage.ResumeLayout(false);
            this.previewPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage designPage;
        private System.Windows.Forms.TabPage layoutPage;
        private System.Windows.Forms.TabPage previewPage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
