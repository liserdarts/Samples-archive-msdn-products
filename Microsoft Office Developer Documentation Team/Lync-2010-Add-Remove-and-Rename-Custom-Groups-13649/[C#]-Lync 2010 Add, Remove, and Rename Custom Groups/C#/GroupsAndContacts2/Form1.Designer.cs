namespace GroupsAndContacts2
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtBoxAdd = new System.Windows.Forms.TextBox();
            this.txtBoxOldname = new System.Windows.Forms.TextBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.txtBoxRemove = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtBoxNewName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(22, 49);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(113, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add a Group";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtBoxAdd
            // 
            this.txtBoxAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxAdd.Location = new System.Drawing.Point(209, 52);
            this.txtBoxAdd.Name = "txtBoxAdd";
            this.txtBoxAdd.Size = new System.Drawing.Size(100, 20);
            this.txtBoxAdd.TabIndex = 1;
            this.txtBoxAdd.Text = "group name";
            // 
            // txtBoxOldname
            // 
            this.txtBoxOldname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxOldname.Location = new System.Drawing.Point(209, 81);
            this.txtBoxOldname.Name = "txtBoxOldname";
            this.txtBoxOldname.Size = new System.Drawing.Size(100, 20);
            this.txtBoxOldname.TabIndex = 3;
            this.txtBoxOldname.Text = "old name";
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(22, 78);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(113, 23);
            this.btnRename.TabIndex = 2;
            this.btnRename.Text = "Rename a Group";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // txtBoxRemove
            // 
            this.txtBoxRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxRemove.Location = new System.Drawing.Point(209, 110);
            this.txtBoxRemove.Name = "txtBoxRemove";
            this.txtBoxRemove.Size = new System.Drawing.Size(100, 20);
            this.txtBoxRemove.TabIndex = 5;
            this.txtBoxRemove.Text = "group name";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(22, 107);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(113, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove a Group";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(223, 169);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtBoxNewName
            // 
            this.txtBoxNewName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxNewName.Location = new System.Drawing.Point(355, 81);
            this.txtBoxNewName.Name = "txtBoxNewName";
            this.txtBoxNewName.Size = new System.Drawing.Size(100, 20);
            this.txtBoxNewName.TabIndex = 7;
            this.txtBoxNewName.Text = "new name";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 231);
            this.Controls.Add(this.txtBoxNewName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtBoxRemove);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.txtBoxOldname);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.txtBoxAdd);
            this.Controls.Add(this.btnAdd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtBoxAdd;
        private System.Windows.Forms.TextBox txtBoxOldname;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.TextBox txtBoxRemove;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtBoxNewName;
    }
}

