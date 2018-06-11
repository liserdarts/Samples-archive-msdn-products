﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1378
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.1378.
// 
#pragma warning disable 1591

namespace Microsoft.SAPSK.ContosoTours.SAPServices.SAP_FLIGHTCUSTOMERLIST {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1378")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="BAPI_FLCUST_GETLISTBinding", Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class BAPI_FLCUST_GETLISTService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback BAPI_FLCUST_GETLISTOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public BAPI_FLCUST_GETLISTService() {
            this.Url = global::Microsoft.SAPSK.ContosoTours.SAPServices.Properties.Settings.Default.ContosoTours_SAPServices_SAP_FLIGHTCUSTOMERLIST_BAPI_FLCUST_GETLISTService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event BAPI_FLCUST_GETLISTCompletedEventHandler BAPI_FLCUST_GETLISTCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.sap.com/BAPI_FLCUST_GETLIST", RequestNamespace="urn:sap-com:document:sap:rfc:functions", ResponseElementName="BAPI_FLCUST_GETLIST.Response", ResponseNamespace="urn:sap-com:document:sap:rfc:functions", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void BAPI_FLCUST_GETLIST([System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] ref BAPISCUDAT[] CUSTOMER_LIST, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string CUSTOMER_NAME, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] ref BAPISCUCRA[] CUSTOMER_RANGE, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] ref BAPIPAREX[] EXTENSION_IN, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] ref BAPIPAREX[] EXTENSION_OUT, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int MAX_ROWS, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlIgnoreAttribute()] bool MAX_ROWSSpecified, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)] ref BAPIRET2[] RETURN, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string WEB_USER) {
            object[] results = this.Invoke("BAPI_FLCUST_GETLIST", new object[] {
                        CUSTOMER_LIST,
                        CUSTOMER_NAME,
                        CUSTOMER_RANGE,
                        EXTENSION_IN,
                        EXTENSION_OUT,
                        MAX_ROWS,
                        MAX_ROWSSpecified,
                        RETURN,
                        WEB_USER});
            CUSTOMER_LIST = ((BAPISCUDAT[])(results[0]));
            CUSTOMER_RANGE = ((BAPISCUCRA[])(results[1]));
            EXTENSION_IN = ((BAPIPAREX[])(results[2]));
            EXTENSION_OUT = ((BAPIPAREX[])(results[3]));
            RETURN = ((BAPIRET2[])(results[4]));
        }
        
        /// <remarks/>
        public void BAPI_FLCUST_GETLISTAsync(BAPISCUDAT[] CUSTOMER_LIST, string CUSTOMER_NAME, BAPISCUCRA[] CUSTOMER_RANGE, BAPIPAREX[] EXTENSION_IN, BAPIPAREX[] EXTENSION_OUT, int MAX_ROWS, bool MAX_ROWSSpecified, BAPIRET2[] RETURN, string WEB_USER) {
            this.BAPI_FLCUST_GETLISTAsync(CUSTOMER_LIST, CUSTOMER_NAME, CUSTOMER_RANGE, EXTENSION_IN, EXTENSION_OUT, MAX_ROWS, MAX_ROWSSpecified, RETURN, WEB_USER, null);
        }
        
        /// <remarks/>
        public void BAPI_FLCUST_GETLISTAsync(BAPISCUDAT[] CUSTOMER_LIST, string CUSTOMER_NAME, BAPISCUCRA[] CUSTOMER_RANGE, BAPIPAREX[] EXTENSION_IN, BAPIPAREX[] EXTENSION_OUT, int MAX_ROWS, bool MAX_ROWSSpecified, BAPIRET2[] RETURN, string WEB_USER, object userState) {
            if ((this.BAPI_FLCUST_GETLISTOperationCompleted == null)) {
                this.BAPI_FLCUST_GETLISTOperationCompleted = new System.Threading.SendOrPostCallback(this.OnBAPI_FLCUST_GETLISTOperationCompleted);
            }
            this.InvokeAsync("BAPI_FLCUST_GETLIST", new object[] {
                        CUSTOMER_LIST,
                        CUSTOMER_NAME,
                        CUSTOMER_RANGE,
                        EXTENSION_IN,
                        EXTENSION_OUT,
                        MAX_ROWS,
                        MAX_ROWSSpecified,
                        RETURN,
                        WEB_USER}, this.BAPI_FLCUST_GETLISTOperationCompleted, userState);
        }
        
        private void OnBAPI_FLCUST_GETLISTOperationCompleted(object arg) {
            if ((this.BAPI_FLCUST_GETLISTCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.BAPI_FLCUST_GETLISTCompleted(this, new BAPI_FLCUST_GETLISTCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.1378")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class BAPISCUDAT {
        
        private string cUSTOMERIDField;
        
        private string cUSTNAMEField;
        
        private string fORMField;
        
        private string sTREETField;
        
        private string pOBOXField;
        
        private string pOSTCODEField;
        
        private string cITYField;
        
        private string cOUNTRField;
        
        private string cOUNTR_ISOField;
        
        private string rEGIONField;
        
        private string pHONEField;
        
        private string eMAILField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CUSTOMERID {
            get {
                return this.cUSTOMERIDField;
            }
            set {
                this.cUSTOMERIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CUSTNAME {
            get {
                return this.cUSTNAMEField;
            }
            set {
                this.cUSTNAMEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FORM {
            get {
                return this.fORMField;
            }
            set {
                this.fORMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STREET {
            get {
                return this.sTREETField;
            }
            set {
                this.sTREETField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string POBOX {
            get {
                return this.pOBOXField;
            }
            set {
                this.pOBOXField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string POSTCODE {
            get {
                return this.pOSTCODEField;
            }
            set {
                this.pOSTCODEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CITY {
            get {
                return this.cITYField;
            }
            set {
                this.cITYField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string COUNTR {
            get {
                return this.cOUNTRField;
            }
            set {
                this.cOUNTRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string COUNTR_ISO {
            get {
                return this.cOUNTR_ISOField;
            }
            set {
                this.cOUNTR_ISOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string REGION {
            get {
                return this.rEGIONField;
            }
            set {
                this.rEGIONField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PHONE {
            get {
                return this.pHONEField;
            }
            set {
                this.pHONEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EMAIL {
            get {
                return this.eMAILField;
            }
            set {
                this.eMAILField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.1378")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class BAPIRET2 {
        
        private string tYPEField;
        
        private string idField;
        
        private string nUMBERField;
        
        private string mESSAGEField;
        
        private string lOG_NOField;
        
        private string lOG_MSG_NOField;
        
        private string mESSAGE_V1Field;
        
        private string mESSAGE_V2Field;
        
        private string mESSAGE_V3Field;
        
        private string mESSAGE_V4Field;
        
        private string pARAMETERField;
        
        private int rOWField;
        
        private bool rOWFieldSpecified;
        
        private string fIELDField;
        
        private string sYSTEMField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TYPE {
            get {
                return this.tYPEField;
            }
            set {
                this.tYPEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NUMBER {
            get {
                return this.nUMBERField;
            }
            set {
                this.nUMBERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE {
            get {
                return this.mESSAGEField;
            }
            set {
                this.mESSAGEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LOG_NO {
            get {
                return this.lOG_NOField;
            }
            set {
                this.lOG_NOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LOG_MSG_NO {
            get {
                return this.lOG_MSG_NOField;
            }
            set {
                this.lOG_MSG_NOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V1 {
            get {
                return this.mESSAGE_V1Field;
            }
            set {
                this.mESSAGE_V1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V2 {
            get {
                return this.mESSAGE_V2Field;
            }
            set {
                this.mESSAGE_V2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V3 {
            get {
                return this.mESSAGE_V3Field;
            }
            set {
                this.mESSAGE_V3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V4 {
            get {
                return this.mESSAGE_V4Field;
            }
            set {
                this.mESSAGE_V4Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PARAMETER {
            get {
                return this.pARAMETERField;
            }
            set {
                this.pARAMETERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int ROW {
            get {
                return this.rOWField;
            }
            set {
                this.rOWField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ROWSpecified {
            get {
                return this.rOWFieldSpecified;
            }
            set {
                this.rOWFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FIELD {
            get {
                return this.fIELDField;
            }
            set {
                this.fIELDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SYSTEM {
            get {
                return this.sYSTEMField;
            }
            set {
                this.sYSTEMField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.1378")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class BAPIPAREX {
        
        private string sTRUCTUREField;
        
        private string vALUEPART1Field;
        
        private string vALUEPART2Field;
        
        private string vALUEPART3Field;
        
        private string vALUEPART4Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string STRUCTURE {
            get {
                return this.sTRUCTUREField;
            }
            set {
                this.sTRUCTUREField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VALUEPART1 {
            get {
                return this.vALUEPART1Field;
            }
            set {
                this.vALUEPART1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VALUEPART2 {
            get {
                return this.vALUEPART2Field;
            }
            set {
                this.vALUEPART2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VALUEPART3 {
            get {
                return this.vALUEPART3Field;
            }
            set {
                this.vALUEPART3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VALUEPART4 {
            get {
                return this.vALUEPART4Field;
            }
            set {
                this.vALUEPART4Field = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.1378")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class BAPISCUCRA {
        
        private string sIGNField;
        
        private string oPTIONField;
        
        private string lOWField;
        
        private string hIGHField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SIGN {
            get {
                return this.sIGNField;
            }
            set {
                this.sIGNField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OPTION {
            get {
                return this.oPTIONField;
            }
            set {
                this.oPTIONField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LOW {
            get {
                return this.lOWField;
            }
            set {
                this.lOWField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string HIGH {
            get {
                return this.hIGHField;
            }
            set {
                this.hIGHField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1378")]
    public delegate void BAPI_FLCUST_GETLISTCompletedEventHandler(object sender, BAPI_FLCUST_GETLISTCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1378")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class BAPI_FLCUST_GETLISTCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal BAPI_FLCUST_GETLISTCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public BAPISCUDAT[] CUSTOMER_LIST {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BAPISCUDAT[])(this.results[0]));
            }
        }
        
        /// <remarks/>
        public BAPISCUCRA[] CUSTOMER_RANGE {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BAPISCUCRA[])(this.results[1]));
            }
        }
        
        /// <remarks/>
        public BAPIPAREX[] EXTENSION_IN {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BAPIPAREX[])(this.results[2]));
            }
        }
        
        /// <remarks/>
        public BAPIPAREX[] EXTENSION_OUT {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BAPIPAREX[])(this.results[3]));
            }
        }
        
        /// <remarks/>
        public BAPIRET2[] RETURN {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BAPIRET2[])(this.results[4]));
            }
        }
    }
}

#pragma warning restore 1591