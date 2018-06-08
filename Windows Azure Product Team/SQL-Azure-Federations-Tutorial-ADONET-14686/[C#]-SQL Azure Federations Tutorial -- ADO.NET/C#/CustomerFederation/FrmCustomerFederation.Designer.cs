namespace CustomerFederation
{
    partial class frmCustomerFederation
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRoot = new System.Windows.Forms.Button();
            this.bindingSourceCustomer = new System.Windows.Forms.BindingSource(this.components);
            this.btnCustomerFanOut = new System.Windows.Forms.Button();
            this.btnCustomerQueryMember = new System.Windows.Forms.Button();
            this.dataGridViewCustomerAddress = new System.Windows.Forms.DataGridView();
            this.dataGridViewDBs = new System.Windows.Forms.DataGridView();
            this.bindingSourceFedMemberColumns = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceCustomerAddress = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceDBs = new System.Windows.Forms.BindingSource(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.numericUpDownFederatedKey = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnListDbs = new System.Windows.Forms.Button();
            this.btnDropDBs = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnGetRowCounts = new System.Windows.Forms.Button();
            this.btnDropDB = new System.Windows.Forms.Button();
            this.btnSplit = new System.Windows.Forms.Button();
            this.btnMember = new System.Windows.Forms.Button();
            this.dataGridViewFedMemberColumns = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridViewCustomer = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbFilterOn = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomerAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDBs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFedMemberColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCustomerAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDBs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFederatedKey)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFedMemberColumns)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomer)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRoot
            // 
            this.btnRoot.Location = new System.Drawing.Point(7, 8);
            this.btnRoot.Name = "btnRoot";
            this.btnRoot.Size = new System.Drawing.Size(83, 23);
            this.btnRoot.TabIndex = 0;
            this.btnRoot.Text = "&Root";
            this.btnRoot.UseVisualStyleBackColor = true;
            this.btnRoot.Click += new System.EventHandler(this.btnRoot_Click);
            // 
            // btnCustomerFanOut
            // 
            this.btnCustomerFanOut.Location = new System.Drawing.Point(87, 6);
            this.btnCustomerFanOut.Name = "btnCustomerFanOut";
            this.btnCustomerFanOut.Size = new System.Drawing.Size(75, 23);
            this.btnCustomerFanOut.TabIndex = 1;
            this.btnCustomerFanOut.Text = "&Fan Out";
            this.btnCustomerFanOut.UseVisualStyleBackColor = true;
            this.btnCustomerFanOut.Click += new System.EventHandler(this.btnCustomerFanOut_Click);
            // 
            // btnCustomerQueryMember
            // 
            this.btnCustomerQueryMember.Location = new System.Drawing.Point(6, 6);
            this.btnCustomerQueryMember.Name = "btnCustomerQueryMember";
            this.btnCustomerQueryMember.Size = new System.Drawing.Size(75, 23);
            this.btnCustomerQueryMember.TabIndex = 0;
            this.btnCustomerQueryMember.Text = "&Query Member";
            this.btnCustomerQueryMember.UseVisualStyleBackColor = true;
            this.btnCustomerQueryMember.Click += new System.EventHandler(this.btnCustomerQueryMember_Click);
            // 
            // dataGridViewCustomerAddress
            // 
            this.dataGridViewCustomerAddress.AllowUserToAddRows = false;
            this.dataGridViewCustomerAddress.AllowUserToDeleteRows = false;
            this.dataGridViewCustomerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCustomerAddress.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCustomerAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCustomerAddress.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewCustomerAddress.Location = new System.Drawing.Point(6, 191);
            this.dataGridViewCustomerAddress.Name = "dataGridViewCustomerAddress";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCustomerAddress.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewCustomerAddress.Size = new System.Drawing.Size(647, 150);
            this.dataGridViewCustomerAddress.TabIndex = 3;
            // 
            // dataGridViewDBs
            // 
            this.dataGridViewDBs.AllowUserToAddRows = false;
            this.dataGridViewDBs.AllowUserToDeleteRows = false;
            this.dataGridViewDBs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDBs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewDBs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDBs.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewDBs.Location = new System.Drawing.Point(6, 37);
            this.dataGridViewDBs.Name = "dataGridViewDBs";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDBs.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewDBs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDBs.Size = new System.Drawing.Size(647, 282);
            this.dataGridViewDBs.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(595, 13);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.Location = new System.Drawing.Point(13, 424);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(663, 144);
            this.txtResults.TabIndex = 10;
            // 
            // numericUpDownFederatedKey
            // 
            this.numericUpDownFederatedKey.Location = new System.Drawing.Point(167, 11);
            this.numericUpDownFederatedKey.Name = "numericUpDownFederatedKey";
            this.numericUpDownFederatedKey.Size = new System.Drawing.Size(105, 20);
            this.numericUpDownFederatedKey.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Federated Key (CustomerID):";
            // 
            // btnListDbs
            // 
            this.btnListDbs.Location = new System.Drawing.Point(6, 8);
            this.btnListDbs.Name = "btnListDbs";
            this.btnListDbs.Size = new System.Drawing.Size(137, 23);
            this.btnListDbs.TabIndex = 4;
            this.btnListDbs.Text = "&List Federated DBs";
            this.btnListDbs.UseVisualStyleBackColor = true;
            this.btnListDbs.Click += new System.EventHandler(this.btnListDbs_Click);
            // 
            // btnDropDBs
            // 
            this.btnDropDBs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDropDBs.Location = new System.Drawing.Point(536, 325);
            this.btnDropDBs.Name = "btnDropDBs";
            this.btnDropDBs.Size = new System.Drawing.Size(117, 23);
            this.btnDropDBs.TabIndex = 4;
            this.btnDropDBs.Text = "&Remove DB(s)";
            this.btnDropDBs.UseVisualStyleBackColor = true;
            this.btnDropDBs.Click += new System.EventHandler(this.btnDropDBs_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnGetRowCounts);
            this.tabPage2.Controls.Add(this.btnDropDB);
            this.tabPage2.Controls.Add(this.btnSplit);
            this.tabPage2.Controls.Add(this.btnRoot);
            this.tabPage2.Controls.Add(this.btnMember);
            this.tabPage2.Controls.Add(this.dataGridViewFedMemberColumns);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(659, 354);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Metadata";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnGetRowCounts
            // 
            this.btnGetRowCounts.Location = new System.Drawing.Point(203, 8);
            this.btnGetRowCounts.Name = "btnGetRowCounts";
            this.btnGetRowCounts.Size = new System.Drawing.Size(106, 23);
            this.btnGetRowCounts.TabIndex = 5;
            this.btnGetRowCounts.Text = "&Get Row Counts";
            this.btnGetRowCounts.UseVisualStyleBackColor = true;
            this.btnGetRowCounts.Click += new System.EventHandler(this.btnGetRowCounts_Click);
            // 
            // btnDropDB
            // 
            this.btnDropDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDropDB.Location = new System.Drawing.Point(550, 319);
            this.btnDropDB.Name = "btnDropDB";
            this.btnDropDB.Size = new System.Drawing.Size(103, 23);
            this.btnDropDB.TabIndex = 4;
            this.btnDropDB.Text = "&Drop Federation";
            this.btnDropDB.UseVisualStyleBackColor = true;
            this.btnDropDB.Click += new System.EventHandler(this.btnDropDB_Click);
            // 
            // btnSplit
            // 
            this.btnSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSplit.Location = new System.Drawing.Point(578, 8);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(75, 23);
            this.btnSplit.TabIndex = 2;
            this.btnSplit.Text = "&Split";
            this.btnSplit.UseVisualStyleBackColor = true;
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // btnMember
            // 
            this.btnMember.Location = new System.Drawing.Point(96, 8);
            this.btnMember.Name = "btnMember";
            this.btnMember.Size = new System.Drawing.Size(83, 23);
            this.btnMember.TabIndex = 1;
            this.btnMember.Text = "&Member";
            this.btnMember.UseVisualStyleBackColor = true;
            this.btnMember.Click += new System.EventHandler(this.btnMember_Click);
            // 
            // dataGridViewFedMemberColumns
            // 
            this.dataGridViewFedMemberColumns.AllowUserToAddRows = false;
            this.dataGridViewFedMemberColumns.AllowUserToDeleteRows = false;
            this.dataGridViewFedMemberColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFedMemberColumns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewFedMemberColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewFedMemberColumns.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewFedMemberColumns.Location = new System.Drawing.Point(7, 36);
            this.dataGridViewFedMemberColumns.Name = "dataGridViewFedMemberColumns";
            this.dataGridViewFedMemberColumns.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFedMemberColumns.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewFedMemberColumns.Size = new System.Drawing.Size(646, 277);
            this.dataGridViewFedMemberColumns.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(13, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(667, 380);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCustomerFanOut);
            this.tabPage1.Controls.Add(this.btnCustomerQueryMember);
            this.tabPage1.Controls.Add(this.dataGridViewCustomerAddress);
            this.tabPage1.Controls.Add(this.dataGridViewCustomer);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(659, 354);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Customers";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridViewCustomer
            // 
            this.dataGridViewCustomer.AllowUserToAddRows = false;
            this.dataGridViewCustomer.AllowUserToDeleteRows = false;
            this.dataGridViewCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCustomer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCustomer.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewCustomer.Location = new System.Drawing.Point(6, 35);
            this.dataGridViewCustomer.Name = "dataGridViewCustomer";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCustomer.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewCustomer.Size = new System.Drawing.Size(647, 150);
            this.dataGridViewCustomer.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnDropDBs);
            this.tabPage3.Controls.Add(this.btnListDbs);
            this.tabPage3.Controls.Add(this.dataGridViewDBs);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(659, 354);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Clean-up";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cbFilterOn
            // 
            this.cbFilterOn.AutoSize = true;
            this.cbFilterOn.Location = new System.Drawing.Point(295, 12);
            this.cbFilterOn.Name = "cbFilterOn";
            this.cbFilterOn.Size = new System.Drawing.Size(79, 17);
            this.cbFilterOn.TabIndex = 7;
            this.cbFilterOn.Text = "&Filtering On";
            this.cbFilterOn.UseVisualStyleBackColor = true;
            this.cbFilterOn.CheckedChanged += new System.EventHandler(this.cbFilterOn_CheckedChanged);
            // 
            // frmCustomerFederation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 579);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.numericUpDownFederatedKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cbFilterOn);
            this.Name = "frmCustomerFederation";
            this.Text = "SQL Azure Federations Tutorial - ADO.NET";
            this.Load += new System.EventHandler(this.frmCustomerFederation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomerAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDBs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFedMemberColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCustomerAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDBs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFederatedKey)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFedMemberColumns)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomer)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRoot;
        private System.Windows.Forms.BindingSource bindingSourceCustomer;
        private System.Windows.Forms.Button btnCustomerFanOut;
        private System.Windows.Forms.Button btnCustomerQueryMember;
        private System.Windows.Forms.DataGridView dataGridViewCustomerAddress;
        private System.Windows.Forms.DataGridView dataGridViewDBs;
        private System.Windows.Forms.BindingSource bindingSourceFedMemberColumns;
        private System.Windows.Forms.BindingSource bindingSourceCustomerAddress;
        private System.Windows.Forms.BindingSource bindingSourceDBs;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.NumericUpDown numericUpDownFederatedKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnListDbs;
        private System.Windows.Forms.Button btnDropDBs;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnGetRowCounts;
        private System.Windows.Forms.Button btnDropDB;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.Button btnMember;
        private System.Windows.Forms.DataGridView dataGridViewFedMemberColumns;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridViewCustomer;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox cbFilterOn;


    }
}

