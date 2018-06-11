/*++

Copyright © Microsoft Corporation

Module Name:

    SIPSnoop.cs

Abstract:

    This module implements GUI pieces for the SIPSnoop application.
        
Notes:

    The SIPSnoop application has two parts: the SessionManager
    and the GUI manager. The GUI manager uses SessionManager
    exported callbacks to get state changes, and updates the GUI.
    
--*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Microsoft.Rtc.Sip.SDK.Samples.SIPSnoop;
using Microsoft.Rtc.Sip.SDK.Samples.Utils;

namespace Microsoft.Rtc.Sip.SDK.Samples.SIPSnoop
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        #region GUIComponents

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label requests;
        private System.Windows.Forms.Label register;
        private System.Windows.Forms.Label notify;
        private System.Windows.Forms.Label invite;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label twoxx;
        private System.Windows.Forms.Label otherrequests;
        private System.Windows.Forms.Label fourxx;
        private System.Windows.Forms.Label fivexx;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Status;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.IContainer components;
        
        #endregion GUIComponents
        
        #region SIPControl

        protected SessionManager sessionManager;
        private System.Windows.Forms.CheckBox invitestatus;
        private System.Windows.Forms.CheckBox messagestatus;
        private System.Windows.Forms.CheckBox notifystatus;
        private System.Windows.Forms.CheckBox registerstatus;
        private System.Windows.Forms.CheckBox otherstatus;
        private System.Windows.Forms.CheckBox _2xxstatus;
        private System.Windows.Forms.CheckBox _4xxstatus;
        private System.Windows.Forms.CheckBox _5xxstatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox inviteval;
        private System.Windows.Forms.TextBox messageval;
        private System.Windows.Forms.TextBox notifyval;
        private System.Windows.Forms.TextBox registerval;
        private System.Windows.Forms.TextBox otherval;
        private System.Windows.Forms.TextBox _2xxval;
        private System.Windows.Forms.TextBox _4xxval;
        private System.Windows.Forms.TextBox _5xxval;
        private System.Windows.Forms.TextBox users;
        private System.Windows.Forms.TextBox totalsessions;
        private System.Windows.Forms.TextBox activesessions;
        private System.Windows.Forms.RichTextBox display;
        private System.Windows.Forms.Label _6xxresponses;
        private System.Windows.Forms.TextBox _6xxval;
        private System.Windows.Forms.CheckBox _6xxstatus;
        private System.Windows.Forms.Label threexx;
        private System.Windows.Forms.CheckBox _3xxstatus;
        private System.Windows.Forms.Label _1xx;
        private System.Windows.Forms.CheckBox _1xxstatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox infostatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox byeval;
        private System.Windows.Forms.CheckBox byestatus;
        private System.Windows.Forms.TextBox _3xxval;
        private System.Windows.Forms.TextBox _1xxval;
        private System.Windows.Forms.TextBox infoval;
        private System.Windows.Forms.CheckBox showauth;
        private System.Windows.Forms.Label label7;
        
        /// <summary>
        /// are we connected to server ?
        /// </summary>
        protected bool connected; 

        /// <summary>
        /// helper synchronization object for writing to text box
        /// </summary>
        protected object displayLock;

        /// name of the file containing application manifest.
        /// this file must be present in the working directory of the
        ///  SIPSnoop
        /// </summary>
        protected const string manifestFile = "sipsnoop.am";
        protected const string asyncCallManifestFile = "sipsnoop2.am";

        /// <summary>
        /// SIPsnoop's GUID
        /// </summary>
        protected string sipSnoopGuid =  "{cd4382aa-7cf1-4727-b23f-1c7fec952443}";

        /// <summary>
        /// friendly name
        /// </summary>
        protected const string sipSnoop = "SIPSnoop";
        
        #endregion SIPControl

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            sessionManager = SessionManager.GetSessionManager();

            ///establish listeners
            sessionManager.DisconnectListeners += 
                new SessionManager.DisconnectListener(this.DisconnectListener);

            sessionManager.StateChangeListeners +=
                new SessionManager.StateChangeListener(this.StateChangeListener);

            displayLock = new Object();

            ///establish state
            connected = false;
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }

                if (sessionManager != null)
                {
                    sessionManager.Dispose();
                    sessionManager = null;
                }
            }
            base.Dispose( disposing );
        }


        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.requests = new System.Windows.Forms.Label();
            this.register = new System.Windows.Forms.Label();
            this.notify = new System.Windows.Forms.Label();
            this.invite = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.twoxx = new System.Windows.Forms.Label();
            this.otherrequests = new System.Windows.Forms.Label();
            this.fourxx = new System.Windows.Forms.Label();
            this.fivexx = new System.Windows.Forms.Label();
            this._6xxresponses = new System.Windows.Forms.Label();
            this.inviteval = new System.Windows.Forms.TextBox();
            this.messageval = new System.Windows.Forms.TextBox();
            this.notifyval = new System.Windows.Forms.TextBox();
            this.registerval = new System.Windows.Forms.TextBox();
            this.otherval = new System.Windows.Forms.TextBox();
            this._2xxval = new System.Windows.Forms.TextBox();
            this._4xxval = new System.Windows.Forms.TextBox();
            this._5xxval = new System.Windows.Forms.TextBox();
            this._6xxval = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Status = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.invitestatus = new System.Windows.Forms.CheckBox();
            this.messagestatus = new System.Windows.Forms.CheckBox();
            this.notifystatus = new System.Windows.Forms.CheckBox();
            this.registerstatus = new System.Windows.Forms.CheckBox();
            this.otherstatus = new System.Windows.Forms.CheckBox();
            this._2xxstatus = new System.Windows.Forms.CheckBox();
            this._4xxstatus = new System.Windows.Forms.CheckBox();
            this._5xxstatus = new System.Windows.Forms.CheckBox();
            this._6xxstatus = new System.Windows.Forms.CheckBox();
            this.display = new System.Windows.Forms.RichTextBox();
            this._3xxstatus = new System.Windows.Forms.CheckBox();
            this._1xxstatus = new System.Windows.Forms.CheckBox();
            this.infostatus = new System.Windows.Forms.CheckBox();
            this.byestatus = new System.Windows.Forms.CheckBox();
            this.showauth = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.users = new System.Windows.Forms.TextBox();
            this.totalsessions = new System.Windows.Forms.TextBox();
            this.activesessions = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.threexx = new System.Windows.Forms.Label();
            this._3xxval = new System.Windows.Forms.TextBox();
            this._1xx = new System.Windows.Forms.Label();
            this._1xxval = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.infoval = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.byeval = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(184, 638);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // requests
            // 
            this.requests.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.requests.Location = new System.Drawing.Point(8, 168);
            this.requests.Name = "requests";
            this.requests.Size = new System.Drawing.Size(144, 16);
            this.requests.TabIndex = 1;
            this.requests.Text = "Request statistics";
            this.toolTip1.SetToolTip(this.requests, "Requests received and proxied ");
            // 
            // register
            // 
            this.register.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.register.Location = new System.Drawing.Point(8, 312);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(100, 20);
            this.register.TabIndex = 3;
            this.register.Text = "REGISTER";
            // 
            // notify
            // 
            this.notify.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.notify.Location = new System.Drawing.Point(8, 288);
            this.notify.Name = "notify";
            this.notify.Size = new System.Drawing.Size(100, 20);
            this.notify.TabIndex = 5;
            this.notify.Text = "NOTIFY";
            // 
            // invite
            // 
            this.invite.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.invite.Location = new System.Drawing.Point(8, 192);
            this.invite.Name = "invite";
            this.invite.Size = new System.Drawing.Size(100, 20);
            this.invite.TabIndex = 8;
            this.invite.Text = "INVITE";
            // 
            // message
            // 
            this.message.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.message.Location = new System.Drawing.Point(8, 240);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(100, 20);
            this.message.TabIndex = 9;
            this.message.Text = "MESSAGE";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 360);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(136, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Response statistics";
            this.toolTip1.SetToolTip(this.label10, "Responses received and proxied");
            // 
            // twoxx
            // 
            this.twoxx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.twoxx.Location = new System.Drawing.Point(8, 408);
            this.twoxx.Name = "twoxx";
            this.twoxx.Size = new System.Drawing.Size(100, 20);
            this.twoxx.TabIndex = 13;
            this.twoxx.Text = "2xx";
            // 
            // otherrequests
            // 
            this.otherrequests.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.otherrequests.Location = new System.Drawing.Point(8, 336);
            this.otherrequests.Name = "otherrequests";
            this.otherrequests.Size = new System.Drawing.Size(100, 20);
            this.otherrequests.TabIndex = 14;
            this.otherrequests.Text = "OTHER";
            // 
            // fourxx
            // 
            this.fourxx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.fourxx.Location = new System.Drawing.Point(8, 456);
            this.fourxx.Name = "fourxx";
            this.fourxx.Size = new System.Drawing.Size(100, 20);
            this.fourxx.TabIndex = 15;
            this.fourxx.Text = "4xx";
            // 
            // fivexx
            // 
            this.fivexx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.fivexx.Location = new System.Drawing.Point(8, 480);
            this.fivexx.Name = "fivexx";
            this.fivexx.Size = new System.Drawing.Size(100, 20);
            this.fivexx.TabIndex = 16;
            this.fivexx.Text = "5xx";
            // 
            // _6xxresponses
            // 
            this._6xxresponses.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this._6xxresponses.Location = new System.Drawing.Point(8, 512);
            this._6xxresponses.Name = "_6xxresponses";
            this._6xxresponses.Size = new System.Drawing.Size(100, 20);
            this._6xxresponses.TabIndex = 17;
            this._6xxresponses.Text = "6xx";
            // 
            // inviteval
            // 
            this.inviteval.Location = new System.Drawing.Point(96, 192);
            this.inviteval.MaxLength = 7;
            this.inviteval.Name = "inviteval";
            this.inviteval.ReadOnly = true;
            this.inviteval.Size = new System.Drawing.Size(50, 20);
            this.inviteval.TabIndex = 21;
            this.inviteval.TabStop = false;
            this.inviteval.Text = "0";
            this.inviteval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // messageval
            // 
            this.messageval.Location = new System.Drawing.Point(96, 240);
            this.messageval.MaxLength = 7;
            this.messageval.Name = "messageval";
            this.messageval.ReadOnly = true;
            this.messageval.Size = new System.Drawing.Size(50, 20);
            this.messageval.TabIndex = 22;
            this.messageval.TabStop = false;
            this.messageval.Text = "0";
            this.messageval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // notifyval
            // 
            this.notifyval.Location = new System.Drawing.Point(96, 288);
            this.notifyval.MaxLength = 7;
            this.notifyval.Name = "notifyval";
            this.notifyval.ReadOnly = true;
            this.notifyval.Size = new System.Drawing.Size(50, 20);
            this.notifyval.TabIndex = 23;
            this.notifyval.TabStop = false;
            this.notifyval.Text = "0";
            this.notifyval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // registerval
            // 
            this.registerval.Location = new System.Drawing.Point(96, 312);
            this.registerval.MaxLength = 7;
            this.registerval.Name = "registerval";
            this.registerval.ReadOnly = true;
            this.registerval.Size = new System.Drawing.Size(50, 20);
            this.registerval.TabIndex = 24;
            this.registerval.TabStop = false;
            this.registerval.Text = "0";
            this.registerval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // otherval
            // 
            this.otherval.Location = new System.Drawing.Point(96, 336);
            this.otherval.MaxLength = 7;
            this.otherval.Name = "otherval";
            this.otherval.ReadOnly = true;
            this.otherval.Size = new System.Drawing.Size(50, 20);
            this.otherval.TabIndex = 27;
            this.otherval.TabStop = false;
            this.otherval.Text = "0";
            this.otherval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _2xxval
            // 
            this._2xxval.Location = new System.Drawing.Point(96, 408);
            this._2xxval.MaxLength = 7;
            this._2xxval.Name = "_2xxval";
            this._2xxval.ReadOnly = true;
            this._2xxval.Size = new System.Drawing.Size(50, 20);
            this._2xxval.TabIndex = 28;
            this._2xxval.TabStop = false;
            this._2xxval.Text = "0";
            this._2xxval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _4xxval
            // 
            this._4xxval.Location = new System.Drawing.Point(96, 456);
            this._4xxval.MaxLength = 7;
            this._4xxval.Name = "_4xxval";
            this._4xxval.ReadOnly = true;
            this._4xxval.Size = new System.Drawing.Size(50, 20);
            this._4xxval.TabIndex = 29;
            this._4xxval.TabStop = false;
            this._4xxval.Text = "0";
            this._4xxval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _5xxval
            // 
            this._5xxval.Location = new System.Drawing.Point(96, 480);
            this._5xxval.MaxLength = 7;
            this._5xxval.Name = "_5xxval";
            this._5xxval.ReadOnly = true;
            this._5xxval.Size = new System.Drawing.Size(50, 20);
            this._5xxval.TabIndex = 30;
            this._5xxval.TabStop = false;
            this._5xxval.Text = "0";
            this._5xxval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _6xxval
            // 
            this._6xxval.Location = new System.Drawing.Point(96, 512);
            this._6xxval.MaxLength = 7;
            this._6xxval.Name = "_6xxval";
            this._6xxval.ReadOnly = true;
            this._6xxval.Size = new System.Drawing.Size(50, 20);
            this._6xxval.TabIndex = 31;
            this._6xxval.TabStop = false;
            this._6xxval.Text = "0";
            this._6xxval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Status
            // 
            this.Status.Location = new System.Drawing.Point(32, 8);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(152, 23);
            this.Status.TabIndex = 0;
            this.Status.Text = "Connect to server";
            this.toolTip1.SetToolTip(this.Status, "Application status");
            this.Status.Click += new System.EventHandler(this.Status_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(208, 8);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(120, 23);
            this.exit.TabIndex = 1;
            this.exit.Text = "Quit";
            this.toolTip1.SetToolTip(this.exit, "Exit ");
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 32);
            this.label2.TabIndex = 35;
            this.label2.Text = "Total Sessions ";
            this.toolTip1.SetToolTip(this.label2, "Total IM/Audio-Video sessions estimated to be established");
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 36;
            this.label3.Text = "Users ";
            this.toolTip1.SetToolTip(this.label3, "Users seen");
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 37;
            this.label4.Text = "Active Sessions";
            this.toolTip1.SetToolTip(this.label4, "Total IM/Audio-Video sessions estimated to be active");
            // 
            // invitestatus
            // 
            this.invitestatus.Location = new System.Drawing.Point(152, 192);
            this.invitestatus.Name = "invitestatus";
            this.invitestatus.Size = new System.Drawing.Size(16, 16);
            this.invitestatus.TabIndex = 41;
            this.toolTip1.SetToolTip(this.invitestatus, "check the box if you do not want to see INVITE messages on the display");
                        this.invitestatus.Checked = true;           
                        // 
            // messagestatus
            // 
            this.messagestatus.Location = new System.Drawing.Point(152, 240);
            this.messagestatus.Name = "messagestatus";
            this.messagestatus.Size = new System.Drawing.Size(16, 16);
            this.messagestatus.TabIndex = 42;
                        this.messagestatus.Checked = true;          
            this.toolTip1.SetToolTip(this.messagestatus, "check the box if you do not want to see MESSAGE requests/responses on the display" +
                "");
            // 
            // notifystatus
            // 
            this.notifystatus.Location = new System.Drawing.Point(152, 288);
            this.notifystatus.Name = "notifystatus";
            this.notifystatus.Size = new System.Drawing.Size(16, 16);
            this.notifystatus.TabIndex = 43;
            this.toolTip1.SetToolTip(this.notifystatus, "check the box if you do not want to see NOTIFY requests/responses on the display");
                        this.notifystatus.Checked = true;           
            // 
            // registerstatus
            // 
            this.registerstatus.Location = new System.Drawing.Point(152, 312);
            this.registerstatus.Name = "registerstatus";
            this.registerstatus.Size = new System.Drawing.Size(16, 16);
            this.registerstatus.TabIndex = 44;
                        this.registerstatus.Checked = true;         
            this.toolTip1.SetToolTip(this.registerstatus, "check the box if you do not want to see REGISTER requests/responses  on the displ" +
                "ay");
            // 
            // otherstatus
            // 
            this.otherstatus.Location = new System.Drawing.Point(152, 336);
            this.otherstatus.Name = "otherstatus";
            this.otherstatus.Size = new System.Drawing.Size(16, 16);
            this.otherstatus.TabIndex = 45;
                        this.otherstatus.Checked = true;            
            this.toolTip1.SetToolTip(this.otherstatus, "check the box if you do not want to see OTHER requests/responses on the display");
            // 
            // _2xxstatus
            // 
            this._2xxstatus.Location = new System.Drawing.Point(152, 408);
            this._2xxstatus.Name = "_2xxstatus";
            this._2xxstatus.Size = new System.Drawing.Size(16, 16);
            this._2xxstatus.TabIndex = 46;
                        this._2xxstatus.Checked = true;
            this.toolTip1.SetToolTip(this._2xxstatus, "check the box if you do not want to see 2xx responses on the display");
            // 
            // _4xxstatus
            // 
            this._4xxstatus.Location = new System.Drawing.Point(152, 456);
            this._4xxstatus.Name = "_4xxstatus";
            this._4xxstatus.Size = new System.Drawing.Size(16, 16);
            this._4xxstatus.TabIndex = 47;
                        this._4xxstatus.Checked = true;
            this.toolTip1.SetToolTip(this._4xxstatus, "check the box if you do not want to see 4xx responses on the display");
            // 
            // _5xxstatus
            // 
            this._5xxstatus.Location = new System.Drawing.Point(152, 480);
            this._5xxstatus.Name = "_5xxstatus";
            this._5xxstatus.Size = new System.Drawing.Size(16, 16);
            this._5xxstatus.TabIndex = 48;
                        this._5xxstatus.Checked = true;
            this.toolTip1.SetToolTip(this._5xxstatus, "check the box if you do not want to see 5xx responses on the display");
            // 
            // _6xxstatus
            // 
            this._6xxstatus.Location = new System.Drawing.Point(152, 512);
            this._6xxstatus.Name = "_6xxstatus";
            this._6xxstatus.Size = new System.Drawing.Size(16, 16);
            this._6xxstatus.TabIndex = 49;
                        this._6xxstatus.Checked = true;
            this.toolTip1.SetToolTip(this._6xxstatus, "check the box if you do not want to see other responses on the display");
            // 
            // display
            // 
            this.display.DetectUrls = false;
            this.display.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.display.MaxLength = 65535;
            this.display.Name = "display";
            this.display.ReadOnly = true;
            this.display.Size = new System.Drawing.Size(560, 576);
            this.display.TabIndex = 1;
            this.display.Text = "";
            this.toolTip1.SetToolTip(this.display, "Requests are blue color, Responses are green color");
            // 
            // _3xxstatus
            // 
            this._3xxstatus.Location = new System.Drawing.Point(152, 432);
            this._3xxstatus.Name = "_3xxstatus";
            this._3xxstatus.Size = new System.Drawing.Size(16, 16);
            this._3xxstatus.TabIndex = 52;
                        this._3xxstatus.Checked = true;
            this.toolTip1.SetToolTip(this._3xxstatus, "check the box if you do not want to see 2xx responses on the display");
            // 
            // _1xxstatus
            // 
            this._1xxstatus.Location = new System.Drawing.Point(152, 384);
            this._1xxstatus.Name = "_1xxstatus";
            this._1xxstatus.Size = new System.Drawing.Size(16, 16);
            this._1xxstatus.TabIndex = 55;
                        this._1xxstatus.Checked = true;
            this.toolTip1.SetToolTip(this._1xxstatus, "check the box if you do not want to see 2xx responses on the display");
            // 
            // infostatus
            // 
            this.infostatus.Location = new System.Drawing.Point(152, 264);
            this.infostatus.Name = "infostatus";
            this.infostatus.Size = new System.Drawing.Size(16, 16);
            this.infostatus.TabIndex = 58;
                        this.infostatus.Checked = true;
            this.toolTip1.SetToolTip(this.infostatus, "check the box if you do not want to see MESSAGE requests/responses on the display" +
                "");
            // 
            // byestatus
            // 
            this.byestatus.Location = new System.Drawing.Point(152, 216);
            this.byestatus.Name = "byestatus";
            this.byestatus.Size = new System.Drawing.Size(16, 16);
            this.byestatus.TabIndex = 64;
                        this.byestatus.Checked = true;
            this.toolTip1.SetToolTip(this.byestatus, "check the box if you do not want to see INVITE messages on the display");
            // 
            // showauth
            // 
            this.showauth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.showauth.Location = new System.Drawing.Point(8, 568);
            this.showauth.Name = "showauth";
            this.showauth.Size = new System.Drawing.Size(136, 24);
            this.showauth.TabIndex = 65;
            this.showauth.Text = "Show auth headers ";
            this.toolTip1.SetToolTip(this.showauth, "If this is checked, then SIP authentication headers will also be displayed");
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 24);
            this.label1.TabIndex = 34;
            this.label1.Text = "Session statistics";
            // 
            // panel1
            // 
            this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.display});
            this.panel1.Location = new System.Drawing.Point(176, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(568, 584);
            this.panel1.TabIndex = 32;
            // 
            // panel2
            // 
            this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.exit,
                                                                                 this.Status});
            this.panel2.Location = new System.Drawing.Point(240, 592);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 40);
            this.panel2.TabIndex = 33;
            // 
            // users
            // 
            this.users.Location = new System.Drawing.Point(104, 72);
            this.users.MaxLength = 7;
            this.users.Name = "users";
            this.users.ReadOnly = true;
            this.users.Size = new System.Drawing.Size(50, 20);
            this.users.TabIndex = 38;
            this.users.TabStop = false;
            this.users.Text = "0";
            this.users.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // totalsessions
            // 
            this.totalsessions.Location = new System.Drawing.Point(104, 104);
            this.totalsessions.MaxLength = 7;
            this.totalsessions.Name = "totalsessions";
            this.totalsessions.ReadOnly = true;
            this.totalsessions.Size = new System.Drawing.Size(50, 20);
            this.totalsessions.TabIndex = 39;
            this.totalsessions.TabStop = false;
            this.totalsessions.Text = "0";
            this.totalsessions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // activesessions
            // 
            this.activesessions.Location = new System.Drawing.Point(104, 136);
            this.activesessions.MaxLength = 7;
            this.activesessions.Name = "activesessions";
            this.activesessions.ReadOnly = true;
            this.activesessions.Size = new System.Drawing.Size(50, 20);
            this.activesessions.TabIndex = 40;
            this.activesessions.TabStop = false;
            this.activesessions.Text = "0";
            this.activesessions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // threexx
            // 
            this.threexx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.threexx.Location = new System.Drawing.Point(8, 432);
            this.threexx.Name = "threexx";
            this.threexx.Size = new System.Drawing.Size(100, 20);
            this.threexx.TabIndex = 50;
            this.threexx.Text = "3xx";
            // 
            // _3xxval
            // 
            this._3xxval.Location = new System.Drawing.Point(96, 432);
            this._3xxval.MaxLength = 7;
            this._3xxval.Name = "_3xxval";
            this._3xxval.ReadOnly = true;
            this._3xxval.Size = new System.Drawing.Size(50, 20);
            this._3xxval.TabIndex = 51;
            this._3xxval.TabStop = false;
            this._3xxval.Text = "0";
            this._3xxval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _1xx
            // 
            this._1xx.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this._1xx.Location = new System.Drawing.Point(8, 384);
            this._1xx.Name = "_1xx";
            this._1xx.Size = new System.Drawing.Size(100, 20);
            this._1xx.TabIndex = 53;
            this._1xx.Text = "1xx";
            // 
            // _1xxval
            // 
            this._1xxval.Location = new System.Drawing.Point(96, 384);
            this._1xxval.MaxLength = 7;
            this._1xxval.Name = "_1xxval";
            this._1xxval.ReadOnly = true;
            this._1xxval.Size = new System.Drawing.Size(50, 20);
            this._1xxval.TabIndex = 54;
            this._1xxval.TabStop = false;
            this._1xxval.Text = "0";
            this._1xxval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 56;
            this.label5.Text = "INFO";
            // 
            // infoval
            // 
            this.infoval.Location = new System.Drawing.Point(96, 264);
            this.infoval.MaxLength = 7;
            this.infoval.Name = "infoval";
            this.infoval.ReadOnly = true;
            this.infoval.Size = new System.Drawing.Size(50, 20);
            this.infoval.TabIndex = 57;
            this.infoval.TabStop = false;
            this.infoval.Text = "0";
            this.infoval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.TabIndex = 59;
            this.label6.Text = "BYE";
            // 
            // byeval
            // 
            this.byeval.Location = new System.Drawing.Point(96, 216);
            this.byeval.MaxLength = 7;
            this.byeval.Name = "byeval";
            this.byeval.ReadOnly = true;
            this.byeval.Size = new System.Drawing.Size(50, 20);
            this.byeval.TabIndex = 61;
            this.byeval.TabStop = false;
            this.byeval.Text = "0";
            this.byeval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(128, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 66;
            this.label7.Text = "Noshow";
            this.toolTip1.SetToolTip(this.label7, "Check the boxes below if you dont want to a particular message type to be shown o" +
                "n display");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5, 13);
            this.ClientSize = new System.Drawing.Size(760, 638);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                          this.label7,
                                                                          this.showauth,
                                                                          this.byestatus,
                                                                          this.byeval,
                                                                          this.label6,
                                                                          this.infostatus,
                                                                          this.infoval,
                                                                          this.label5,
                                                                          this._1xxstatus,
                                                                          this._1xxval,
                                                                          this._1xx,
                                                                          this._3xxstatus,
                                                                          this._3xxval,
                                                                          this.threexx,
                                                                          this._6xxstatus,
                                                                          this._5xxstatus,
                                                                          this._4xxstatus,
                                                                          this._2xxstatus,
                                                                          this.otherstatus,
                                                                          this.registerstatus,
                                                                          this.notifystatus,
                                                                          this.messagestatus,
                                                                          this.invitestatus,
                                                                          this.activesessions,
                                                                          this.totalsessions,
                                                                          this.users,
                                                                          this.label4,
                                                                          this.label3,
                                                                          this.label2,
                                                                          this.label1,
                                                                          this.panel2,
                                                                          this.panel1,
                                                                          this._6xxval,
                                                                          this._5xxval,
                                                                          this._4xxval,
                                                                          this._2xxval,
                                                                          this.otherval,
                                                                          this.registerval,
                                                                          this.notifyval,
                                                                          this.messageval,
                                                                          this.inviteval,
                                                                          this._6xxresponses,
                                                                          this.fivexx,
                                                                          this.fourxx,
                                                                          this.otherrequests,
                                                                          this.twoxx,
                                                                          this.label10,
                                                                          this.message,
                                                                          this.invite,
                                                                          this.notify,
                                                                          this.register,
                                                                          this.requests,
                                                                          this.splitter1});
            this.Name = "Form1";
            this.Text = "SIPSnoop";
            this.toolTip1.SetToolTip(this, "SIPSnoop is a Live Communications Server application that prints all packets that pass through the server");
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        public static bool connectTest = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) 
        {
            if (args.Length > 0) {
                 for (int i = 0; i < args.Length; i++) {
                      if (args[i].ToLower() == "/addheader") {
                          SessionManager.AddHeader = true;
                      }
                      else if (args[i].ToLower() == "/modifytoheader") {
                           SessionManager.ModifyToHeader = true;
                      }
                      else if (args[i].ToLower() == "/simpleproxy") {
                           SessionManager.SimpleProxy = true;
                      }
                      else if (args[i].ToLower() == "/connecttest") {
                           connectTest = true;
                      }
                      else if (args[i].ToLower() == "/asynccall") {
                           SessionManager.AsyncCall = true;
                      }
                      else { 
                            Usage();
                            return;
                      }
                 }
            }

            Application.Run(new Form1());
        }

               
        static void Usage() 
        {
            MessageBox.Show("Usage: SipSnoop.exe [/AddHeader] [/ModifyToHeader] [/simpleproxy] [/ConnectTest] [/asynccall].\r\n");
        }

        #region SessionManagerCallbacks

        protected virtual void DisconnectListener(string reason)
        {
            lock (this)
            {
                if (connected == false) return; //spurious

                ///we are disconnected, so update GUI state
                InternalConnectToDisconnect(reason);
            }
        }


        protected virtual void StateChangeListener(Object args)
        {
            lock (this)
            {
                if (connected == false) return;  //ignore spurious event

                ///we got a request/response, update GUI state
                ///for requests we check the request selection states
                ///for responses we check both the response and request selection states
                RequestReceivedEventArgs reqArgs = args as RequestReceivedEventArgs;
                if (reqArgs != null)
                {
                    Request request = reqArgs.Request;
                    if (request != null && CanShow(request.StandardMethod))
                    {
                        DisplayRequest(request, reqArgs.Parameters);        
                    }
    
                    return;
                }
                else 
                {
                    ResponseReceivedEventArgs respArgs = args as ResponseReceivedEventArgs;
                    if (respArgs != null) 
                    {
  
                        Response response = respArgs.Response;
                        Request request = respArgs.ClientTransaction.ServerTransaction.Request;

                        Debug.Assert (response != null && request != null);

                        if (CanShow(response.StatusClass) && CanShow(request.StandardMethod))
                        {
                            DisplayResponse(response, respArgs.Parameters);
                        }
   
                        return;
                    }

                    // Else Ignore notifications.
                }
            }
        }


        #endregion SessionManagerCallbacks


        #region GUIStateManagers

        /// <summary>
        /// This routine is invoked when the GUI Connect/Disconnect
        /// button is clicked
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="args">unused</param>
        private void Status_Click(object sender, System.EventArgs args)
        {
            ToggleConnection("Disconnect by user request");
        }

        private void ToggleConnection(string toggleReason)
        {
            ///User clicked on the connect button. toggle our
            ///connect state
            lock (this)
            {
                this.Status.Enabled = false;
                
                if (connected)
                {
                    sessionManager.Disconnect();
                    
                    ///we are disconnected, change state
                    InternalConnectToDisconnect(toggleReason);
                }
                else
                {
                    try
                    {
                        string manifestFileToUse = manifestFile;

                        if (SessionManager.AsyncCall) {
                            manifestFileToUse = asyncCallManifestFile;
                        }
 
                        sessionManager.ConnectToServer(manifestFileToUse, sipSnoop, ref sipSnoopGuid);
                        
                        ///we are connected, change state
                        InternalDisconnectToConnect();
                    } 
                    catch (Exception e)
                    {
                        ///we are unable to connect, print
                        ///the exception in our UI, restore
                        ///the button state
                        
                        DisplayException(e);

                        this.Status.Enabled = true;
                        this.exit.Enabled = true;
                    }
                }
            } ///lock(this)
        }
        
        /// <summary>
        /// changes UI to show the connect to disconnect
        /// transition. callers should hold the this lock
        /// </summary>
        protected void InternalConnectToDisconnect(string reason)
        {
            StringBuilder sb = new StringBuilder(256);
            sb.AppendFormat("\n----- Disconnect at {0} due to {1} -----\n", 
                 DateTime.Now.ToLongTimeString(), reason);
            DisplayText(sb.ToString(), Color.Coral);

            connected = false;
            this.Status.Text = "Connect to Server";
            this.Status.Enabled = true;
            this.exit.Enabled = true;
        }

        /// <summary>
        /// changes GUI to show the disconnect to connect
        /// transition. callers should hold the this lock
        /// </summary>
        protected void InternalDisconnectToConnect()
        {
            StringBuilder sb = new StringBuilder(256);
            sb.AppendFormat("\n----- Connect success at {0} -----\n", 
                DateTime.Now.ToLongTimeString());
            sb.AppendFormat("\n----- Server Role is {0} ----\n", sessionManager.Role.ToString());
            DisplayText(sb.ToString(), Color.Green);

            connected = true;
            this.Status.Text = "Disconnect from Server";
            this.Status.Enabled = true;
            this.exit.Enabled = false;
        }
        
    
        /// <summary>
        /// This handler is invoked when user clicks on Quit
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void exit_Click(object sender, System.EventArgs e)
        {
            lock (this)
            {
                this.exit.Enabled = false;
                this.Status.Enabled = false;

                if (connected == true)
                    sessionManager.Disconnect();
            }
    
            System.Environment.Exit(0);
        }

        #endregion GUIStateManagers

        #region Helpers

        /// <summary>
        /// Converts an exception to a string, and puts it
        /// it on our display. Callers must hold the this lock
        /// </summary>
        /// <param name="e"></param>
        protected void DisplayException(Exception e)
        {
            StringBuilder sb = new StringBuilder(1024, 2048); ///1K max
            sb.AppendFormat("-- EXCEPTION ({0}) --\n", DateTime.Now.ToLongTimeString());
            do
            {
                if (e.Message.Length + sb.Length + 2 < sb.MaxCapacity)
                {
                    sb.Append(e.Message);
                    sb.Append("\n");
                }
                else
                    break;

                e = e.InnerException;
            } while (e != null);
            
            DisplayText(sb.ToString(), Color.Red);

            return;
        }


        /// <summary>
        /// helper to format a SIP request and display it on UI
        /// </summary>
        /// <param name="r"></param>
        protected void DisplayRequest(Request r, object[] parameters)
        {
            ///we dont support messages that are super long
            StringBuilder sb = new StringBuilder(4096);

            sb.AppendFormat("\n----- Request ({0}) -----\n", DateTime.Now.ToLongTimeString());
            sb.AppendFormat("{0} {1} SIP/2.0\n", r.Method, r.RequestUri);

            try
            { ///try block for StringBuilder max capacity exception
            
                foreach (Header h in r.AllHeaders)
                {
                    if (CanShowHeader(h))
                    {
                        sb.AppendFormat("{0}: {1}\n", h.Type, h.Value);
                    }
                }

                sb.AppendFormat("\n{0}", r.Content);

                if (parameters != null) {
                    sb.AppendFormat("\nParameters ");
                    foreach (object o in parameters) {
                        sb.AppendFormat("\n{0}", o.ToString());
                    }
                }
            } 
            catch (Exception e)
            {
                ///ignore StringBuilder exceptions
                Debug.Write(e.Message);
            }   
            
            DisplayText(sb.ToString(), Color.Blue);

            return;
        }


        /// <summary>
        /// helper function for formating a SIP response as a
        /// string
        /// </summary>
        /// <param name="r">the sip response</param>
        protected void DisplayResponse(Response r, object[] parameters)
        {
            ///we dont support messages that are super long
            StringBuilder sb = new StringBuilder(4096);
            sb.AppendFormat("\n----- Response ({0}) -----\n", DateTime.Now.ToLongTimeString());
            sb.AppendFormat("SIP/2.0 {0} {1}\n", r.StatusCode, r.ReasonPhrase);

            try
            {
                foreach (Header h in r.AllHeaders)
                {
                    if (CanShowHeader(h))
                        sb.AppendFormat("{0}: {1}\n", h.Type, h.Value);
                }

                sb.AppendFormat("\n{0}", r.Content);

                if (parameters != null) {
                    sb.AppendFormat("\nParameters ");
                    foreach (object o in parameters) {
                        sb.AppendFormat("\n{0}", o.ToString());
                    }
                }
            } 
            catch (Exception e)
            {
                ///ignore StringBuilder exceptions
                Debug.Write(e.Message);
            }  
            
            DisplayText(sb.ToString(), Color.Green);
        
            return;
        }


        /// <summary>
        /// displays the text with a given color
        /// </summary>
        /// <param name="s">text to add to display</param>
        /// <param name="color">its color</param>
        protected void DisplayText(string s, Color color)
        {
            if (s.Length <= 0) return;

            lock (displayLock)
            {
                display.AppendText(s);
                int offset = display.Text.Length - s.Length + 1;
                if (offset < 0) offset = 0;
                display.Select(offset, s.Length);
                display.SelectionColor = color;
            }
        }


        /// <summary>
        /// checks whether the method given in sm can be displayed
        /// in the GUI based on the check state 
        /// </summary>
        /// <param name="sm">the SIP message's method</param>
        /// <returns>true if it can be displayed</returns>
        protected bool CanShow(Request.StandardMethodType sm)
        {
            ///check if user has turned off this request type.
            if (sm == Request.StandardMethodType.Invite)
                return !invitestatus.Checked;
            if (sm == Request.StandardMethodType.Bye)
                return !byestatus.Checked;
            if (sm == Request.StandardMethodType.Info)
                return !infostatus.Checked;
            if (sm == Request.StandardMethodType.Message)
                return !messagestatus.Checked;
            if (sm == Request.StandardMethodType.Register)
                return !registerstatus.Checked;
            if (sm == Request.StandardMethodType.Notify)
                return !notifystatus.Checked;
        
            return otherstatus.Checked == false;
        }


        /// <summary>
        /// Timer handler. Map the statistics object of the
        /// session manager to the GUI
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            ///Use a simple lock to ensure that two timers are not
            ///in this function at the same time
            int oldTimerValue = Interlocked.CompareExchange(ref timerBusy, 1, 0);
            if (oldTimerValue == 1) return; ///previous timer is still active 
        
            Statistics st = sessionManager.Statistics;

            if (connected == true)
            {

                ///Update responses
                _1xxval.Text = st.Count(100).ToString();
                _2xxval.Text = st.Count(200).ToString();
                _3xxval.Text = st.Count(300).ToString();
                _4xxval.Text = st.Count(400).ToString();
                _5xxval.Text = st.Count(500).ToString();
                _6xxval.Text = st.Count(600).ToString();
                
                ///Update requests
                infoval.Text = st.Count(Statistics.StandardMethod.Info).ToString();
                inviteval.Text = st.Count(Statistics.StandardMethod.Invite).ToString();
                byeval.Text = st.Count(Statistics.StandardMethod.Bye).ToString();
                otherval.Text = st.Count(Statistics.StandardMethod.Other).ToString();
                messageval.Text = st.Count(Statistics.StandardMethod.Message).ToString();
                notifyval.Text = st.Count(Statistics.StandardMethod.Notify).ToString();
                registerval.Text = st.Count(Statistics.StandardMethod.Register).ToString();

                ///Update session statistics
                users.Text = st.Users.ToString();
                totalsessions.Text = st.TotalSessions.ToString();
                activesessions.Text = st.ActiveSessions.ToString();
            }


            if (connectTest) {
                passesSinceLastToggle++;
                if (passesSinceLastToggle > autoToggleMinTogglePeriod) {
                    int prob = rand.Next(1, 100);
                    if (prob >= toggleProbability) {
                        ToggleConnection("Timer - Auto toggle");
                        passesSinceLastToggle = 0;
                    }
                 }
            }

            ///we are done, so reset the busy flag  
            timerBusy = 0;

            return;
        }

        /// <summary>
        /// timer synchronization variable
        /// </summary>
        private int timerBusy;
        
        /// <summary>
        /// auto-toggle feature variables
        /// </summary>
        private int passesSinceLastToggle;
        private const int autoToggleMinTogglePeriod = 120; // 2 minutes
        private const int toggleProbability = 10;
        private static readonly Random rand = new Random();

        /// <summary>
        /// Given status class such as 2xx, decide if user wants
        /// to see the response based on GUI state
        /// </summary>
        /// <param name="sc">status class of the SIP response</param>
        /// <returns>true if this can be shown on display</returns>
        protected bool CanShow(int sc)
        {
            switch (sc)
            {
                case 100:
                    return !_1xxstatus.Checked;
                case 200:
                    return !_2xxstatus.Checked;
                case 300:
                    return !_3xxstatus.Checked;
                case 400:
                    return !_4xxstatus.Checked;
                case 500:
                    return !_5xxstatus.Checked;
                default:
                    return !_6xxstatus.Checked; 
            }
        }


        /// <summary>
        /// Authentication related headers
        /// </summary>
        const string AuthenticationInfo      = "Authentication-Info";
        const string ProxyAuthenticationInfo = "Proxy-Authentication-Info";
        const string WWWAuthenticate         = "WWW-Authenticate";
        const string ProxyAuthenticate       = "Proxy-Authenticate";
        const string Authorization           = "Authorization";
        const string ProxyAuthorization      = "Proxy-Authorization";

        protected static string[] AuthHeaderNames;
        
        static Form1()
        {
            AuthHeaderNames = new string[6];
            AuthHeaderNames[0] = AuthenticationInfo;
            AuthHeaderNames[1] = ProxyAuthenticationInfo;
            AuthHeaderNames[2] = WWWAuthenticate;
            AuthHeaderNames[3] = ProxyAuthenticate;
            AuthHeaderNames[4] = Authorization;
            AuthHeaderNames[5] = ProxyAuthorization;
        }


        /// <summary>
        /// Given a header type determine if this header
        /// can be displayed in the UI as part of the SIP
        /// message. 
        /// </summary>
        /// <param name="h">The SIP header</param>
        /// <returns></returns>
        protected bool CanShowHeader(Header h)
        {
            ///If user wants all headers show them
            if (this.showauth.Checked) return true; 

            for (int i = 0; i < AuthHeaderNames.Length; i++)
            {
                if (h.Type.Length == AuthHeaderNames[i].Length &&
                    h.Value == AuthHeaderNames[i])
                    return false;
            }

            return true;
        }
        
        #endregion Helpers
    }
}
