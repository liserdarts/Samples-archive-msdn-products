﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAPBridgeMaster.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:8000/sap/bc/soap/rfc")]
        public string SAPBridgeMaster_BAPI_FLIGHT_CHECKAVAILIBILITY_BAPI_FLIGHT_CHECKAVAILIBILITYService {
            get {
                return ((string)(this["SAPBridgeMaster_BAPI_FLIGHT_CHECKAVAILIBILITY_BAPI_FLIGHT_CHECKAVAILIBILITYServic" +
                    "e"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:8000/sap/bc/soap/rfc")]
        public string SAPBridgeMaster_BAPI_FLCUST_GETLIST_BAPI_FLCUST_GETLISTService {
            get {
                return ((string)(this["SAPBridgeMaster_BAPI_FLCUST_GETLIST_BAPI_FLCUST_GETLISTService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://localhost:8000/sap/bc/soap/rfc")]
        public string SAPBridgeMaster_BAPI_FLIGHT_GETLIST_BAPI_FLIGHT_GETLISTService {
            get {
                return ((string)(this["SAPBridgeMaster_BAPI_FLIGHT_GETLIST_BAPI_FLIGHT_GETLISTService"]));
            }
        }
    }
}
