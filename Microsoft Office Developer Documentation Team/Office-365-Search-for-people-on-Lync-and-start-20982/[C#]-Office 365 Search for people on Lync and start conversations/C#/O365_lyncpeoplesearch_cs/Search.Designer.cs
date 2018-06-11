namespace O365_LyncPeopleSearch
{
    partial class Search
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
            this.Contact = new System.Windows.Forms.TextBox();
            this.lblPerson = new System.Windows.Forms.Label();
            this.SearchContact = new System.Windows.Forms.Button();
            this.PeopleSearch = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.TextBox();
            this.SearchResults = new System.Windows.Forms.DataGridView();
            this.StartIM = new System.Windows.Forms.Button();
            this.SignIn = new System.Windows.Forms.Button();
            this.ExpertSearch = new System.Windows.Forms.CheckBox();
            this.PeopleSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchResults)).BeginInit();
            this.SuspendLayout();
            // 
            // Contact
            // 
            this.Contact.Location = new System.Drawing.Point(125, 23);
            this.Contact.Name = "Contact";
            this.Contact.Size = new System.Drawing.Size(268, 20);
            this.Contact.TabIndex = 0;
            // 
            // lblPerson
            // 
            this.lblPerson.AutoSize = true;
            this.lblPerson.Location = new System.Drawing.Point(6, 26);
            this.lblPerson.Name = "lblPerson";
            this.lblPerson.Size = new System.Drawing.Size(102, 13);
            this.lblPerson.TabIndex = 1;
            this.lblPerson.Text = "Person(Uri or Name)";
            // 
            // SearchContact
            // 
            this.SearchContact.Location = new System.Drawing.Point(494, 19);
            this.SearchContact.Name = "SearchContact";
            this.SearchContact.Size = new System.Drawing.Size(128, 27);
            this.SearchContact.TabIndex = 3;
            this.SearchContact.Text = "Search";
            this.SearchContact.UseVisualStyleBackColor = true;
            this.SearchContact.Click += new System.EventHandler(this.SearchContact_Click);
            // 
            // PeopleSearch
            // 
            this.PeopleSearch.Controls.Add(this.ExpertSearch);
            this.PeopleSearch.Controls.Add(this.lblMessage);
            this.PeopleSearch.Controls.Add(this.Message);
            this.PeopleSearch.Controls.Add(this.SearchResults);
            this.PeopleSearch.Controls.Add(this.StartIM);
            this.PeopleSearch.Controls.Add(this.Contact);
            this.PeopleSearch.Controls.Add(this.SearchContact);
            this.PeopleSearch.Controls.Add(this.lblPerson);
            this.PeopleSearch.Location = new System.Drawing.Point(15, 68);
            this.PeopleSearch.Name = "PeopleSearch";
            this.PeopleSearch.Size = new System.Drawing.Size(641, 429);
            this.PeopleSearch.TabIndex = 4;
            this.PeopleSearch.TabStop = false;
            this.PeopleSearch.Text = "People Search";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(6, 391);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(50, 13);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.Text = "Message";
            // 
            // Message
            // 
            this.Message.Location = new System.Drawing.Point(108, 388);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(360, 20);
            this.Message.TabIndex = 6;
            // 
            // SearchResults
            // 
            this.SearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SearchResults.Location = new System.Drawing.Point(20, 67);
            this.SearchResults.Name = "SearchResults";
            this.SearchResults.Size = new System.Drawing.Size(602, 298);
            this.SearchResults.TabIndex = 5;
            // 
            // StartIM
            // 
            this.StartIM.Location = new System.Drawing.Point(494, 384);
            this.StartIM.Name = "StartIM";
            this.StartIM.Size = new System.Drawing.Size(128, 27);
            this.StartIM.TabIndex = 4;
            this.StartIM.Text = "Start Conversation";
            this.StartIM.UseVisualStyleBackColor = true;
            this.StartIM.Click += new System.EventHandler(this.StartIM_Click);
            // 
            // SignIn
            // 
            this.SignIn.Location = new System.Drawing.Point(223, 12);
            this.SignIn.Name = "SignIn";
            this.SignIn.Size = new System.Drawing.Size(185, 39);
            this.SignIn.TabIndex = 12;
            this.SignIn.Text = "Sign In to Lync";
            this.SignIn.UseVisualStyleBackColor = true;
            this.SignIn.Click += new System.EventHandler(this.SignIn_Click);
            // 
            // ExpertSearch
            // 
            this.ExpertSearch.AutoSize = true;
            this.ExpertSearch.Enabled = false;
            this.ExpertSearch.Location = new System.Drawing.Point(399, 26);
            this.ExpertSearch.Name = "ExpertSearch";
            this.ExpertSearch.Size = new System.Drawing.Size(93, 17);
            this.ExpertSearch.TabIndex = 8;
            this.ExpertSearch.Text = "Expert Search";
            this.ExpertSearch.UseVisualStyleBackColor = true;
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(677, 521);
            this.Controls.Add(this.SignIn);
            this.Controls.Add(this.PeopleSearch);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lync People Search";
            this.PeopleSearch.ResumeLayout(false);
            this.PeopleSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox Contact;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.Button SearchContact;
        private System.Windows.Forms.GroupBox PeopleSearch;
        private System.Windows.Forms.Button StartIM;
        private System.Windows.Forms.DataGridView SearchResults;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.Button SignIn;
        private System.Windows.Forms.CheckBox ExpertSearch;
    }
}

