namespace Microsoft.SAPSK.ContosoTours
{
    partial class ErrorForm
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
            label1 = new System.Windows.Forms.Label();
            labelMessage = new System.Windows.Forms.Label();
            textBoxErrorMessage = new System.Windows.Forms.TextBox();
            textBoxError = new System.Windows.Forms.TextBox();
            buttonClose = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(9, 57);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(62, 13);
            label1.TabIndex = 3;
            label1.Text = "Error Detail:";
            // 
            // labelMessage
            // 
            labelMessage.AutoSize = true;
            labelMessage.BackColor = System.Drawing.Color.Transparent;
            labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelMessage.Location = new System.Drawing.Point(9, 8);
            labelMessage.Name = "labelMessage";
            labelMessage.Size = new System.Drawing.Size(78, 13);
            labelMessage.TabIndex = 2;
            labelMessage.Text = "Error Message:";
            // 
            // textBoxErrorMessage
            // 
            textBoxErrorMessage.BackColor = System.Drawing.SystemColors.Window;
            textBoxErrorMessage.Location = new System.Drawing.Point(12, 25);
            textBoxErrorMessage.Name = "textBoxErrorMessage";
            textBoxErrorMessage.ReadOnly = true;
            textBoxErrorMessage.Size = new System.Drawing.Size(349, 20);
            textBoxErrorMessage.TabIndex = 2;
            // 
            // textBoxError
            // 
            textBoxError.BackColor = System.Drawing.SystemColors.Window;
            textBoxError.Location = new System.Drawing.Point(12, 74);
            textBoxError.Multiline = true;
            textBoxError.Name = "textBoxError";
            textBoxError.ReadOnly = true;
            textBoxError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBoxError.Size = new System.Drawing.Size(349, 138);
            textBoxError.TabIndex = 3;
            // 
            // buttonClose
            // 
            buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonClose.Location = new System.Drawing.Point(286, 218);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new System.Drawing.Size(75, 23);
            buttonClose.TabIndex = 1;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += new System.EventHandler(buttonClose_Click);
            // 
            // ErrorForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonClose;
            ClientSize = new System.Drawing.Size(373, 249);
            ControlBox = false;
            Controls.Add(label1);
            Controls.Add(buttonClose);
            Controls.Add(labelMessage);
            Controls.Add(textBoxErrorMessage);
            Controls.Add(textBoxError);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "ErrorForm";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Error Message";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TextBox textBoxErrorMessage;
    }
}