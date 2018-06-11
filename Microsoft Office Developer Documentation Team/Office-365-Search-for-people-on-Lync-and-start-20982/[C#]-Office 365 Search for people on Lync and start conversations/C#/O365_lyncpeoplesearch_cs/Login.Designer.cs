namespace O365_LyncPeopleSearch
{
    partial class Login
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
            this.SingInToLync = new System.Windows.Forms.GroupBox();
            this.SignIn = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.Username = new System.Windows.Forms.TextBox();
            this.State_Label = new System.Windows.Forms.Label();
            this.SingInToLync.SuspendLayout();
            this.SuspendLayout();
            // 
            // SingInToLync
            // 
            this.SingInToLync.Controls.Add(this.State_Label);
            this.SingInToLync.Controls.Add(this.SignIn);
            this.SingInToLync.Controls.Add(this.lblPassword);
            this.SingInToLync.Controls.Add(this.lblUsername);
            this.SingInToLync.Controls.Add(this.Password);
            this.SingInToLync.Controls.Add(this.Username);
            this.SingInToLync.Location = new System.Drawing.Point(29, 28);
            this.SingInToLync.Name = "SingInToLync";
            this.SingInToLync.Size = new System.Drawing.Size(397, 185);
            this.SingInToLync.TabIndex = 5;
            this.SingInToLync.TabStop = false;
            this.SingInToLync.Text = "Sign in to Lync";
            // 
            // SignIn
            // 
            this.SignIn.Location = new System.Drawing.Point(143, 127);
            this.SignIn.Name = "SignIn";
            this.SignIn.Size = new System.Drawing.Size(87, 23);
            this.SignIn.TabIndex = 11;
            this.SignIn.Text = "Sign In";
            this.SignIn.UseVisualStyleBackColor = true;
            this.SignIn.Click += new System.EventHandler(this.SignIn_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(6, 86);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(6, 29);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 9;
            this.lblUsername.Text = "Username";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(95, 83);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(286, 20);
            this.Password.TabIndex = 8;
            this.Password.UseSystemPasswordChar = true;
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(95, 26);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(286, 20);
            this.Username.TabIndex = 7;
            // 
            // State_Label
            // 
            this.State_Label.AutoSize = true;
            this.State_Label.Location = new System.Drawing.Point(6, 160);
            this.State_Label.Name = "State_Label";
            this.State_Label.Size = new System.Drawing.Size(0, 13);
            this.State_Label.TabIndex = 12;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(450, 232);
            this.Controls.Add(this.SingInToLync);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.SingInToLync.ResumeLayout(false);
            this.SingInToLync.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SingInToLync;
        private System.Windows.Forms.Button SignIn;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label State_Label;
    }
}