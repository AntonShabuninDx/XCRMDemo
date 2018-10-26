using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.TestScripts;
using DevExpress.Internal;
using DevExpress.Persistent.Base;
using DevExpress.Web;
using System;
using System.Configuration;
using System.Web;
using DevExpress.Web.Demos;
using DevExpress.ExpressApp.Security;

namespace XCRM.Web {
    public class Global : System.Web.HttpApplication {
        private static void webApplication_CustomizeFormattingCulture(object sender, CustomizeFormattingCultureEventArgs e) {
            e.FormattingCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        }

        public Global() {
            InitializeComponent();
        }
        protected void Application_Start(object sender, EventArgs e) {
            ASPxWebControl.CallbackError += new EventHandler(Application_Error);
#if DEBUG
            TestScriptsManager.EasyTestEnabled = true;
#endif
        }
        protected void Session_Start(object sender, EventArgs e) {
            Tracing.Initialize();

            WebApplication.SetInstance(Session, new XCRM.Web.XCRMWebApplication());            
            WebApplication.Instance.SwitchToNewStyle();
            WebApplication.Instance.Settings.DefaultVerticalTemplateContentPath = "CustomDefaultVerticalTemplateContent.ascx";
            WebApplication.Instance.CustomizeFormattingCulture += new EventHandler<CustomizeFormattingCultureEventArgs>(webApplication_CustomizeFormattingCulture);
            SecurityAdapterHelper.Enable();
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["ConnectionString"];
            if (connectionStringSettings != null) {
                WebApplication.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            } 
            else if(string.IsNullOrEmpty(WebApplication.Instance.ConnectionString) && WebApplication.Instance.Connection == null) {
                connectionStringSettings = ConfigurationManager.ConnectionStrings["SqlExpressConnectionString"];
                if (connectionStringSettings != null) {
                    WebApplication.Instance.ConnectionString = DbEngineDetector.PatchConnectionString(connectionStringSettings.ConnectionString);
                }
            }
            if (ConfigurationManager.AppSettings["SiteMode"] != null && ConfigurationManager.AppSettings["SiteMode"].ToLower() == "true") {
                connectionStringSettings = ConfigurationManager.ConnectionStrings["XCRMSiteModeConnectionString"];
                if (connectionStringSettings != null) {
                    WebApplication.Instance.ConnectionString = connectionStringSettings.ConnectionString;
                    WebApplication.Instance.ObjectSpaceCreated += (s, args) => {
                        args.ObjectSpace.Committing += ObjectSpace_Committing;
                        args.ObjectSpace.Disposed += ObjectSpace_Disposed;
                    };
                }
                WebApplication.Instance.Modules.FindModule<Module.Web.XCRMAspNetModule>().SiteMode = true;
            }
            //if(System.Diagnostics.Debugger.IsAttached && WebApplication.Instance.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                WebApplication.Instance.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            //}

            WebApplication.Instance.Setup();
            WebApplication.Instance.Start();
        }
        private void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = true;
            if(CanThrowException((IObjectSpace)sender)) {
                throw new UserFriendlyException("Data modifications are not allowed in this online demo. You can test data editing functionality by installing XAF on your machine and running the demo locally.");
            }
        }
        private void ObjectSpace_Disposed(object sender, EventArgs e) {
            IObjectSpace os = (IObjectSpace)sender;
            os.Committing -= ObjectSpace_Committing;
            os.Disposed -= ObjectSpace_Disposed;
        }
        private bool CanThrowException(IObjectSpace os) {
            bool result = true;
            foreach(object obj in os.ModifiedObjects) {
                if(obj is DevExpress.ExpressApp.ReportsV2.IReportDataV2) {
                    result = false;
                    break;
                }
                if(obj is IDashboardData) {
                    result = false;
                    break;
                }
            }
            return result;
        }
        protected void Application_BeginRequest(object sender, EventArgs e) {
            ASPxWebControl.GlobalThemeBaseColor = Utils.CurrentBaseColor;
            ASPxWebControl.GlobalThemeFont = Utils.CurrentFont;
        }
        protected void Application_EndRequest(object sender, EventArgs e) {
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e) {
        }
        protected void Application_Error(object sender, EventArgs e) {
            ErrorHandling.Instance.ProcessApplicationError();
        }
        protected void Session_End(object sender, EventArgs e) {
            WebApplication.DisposeInstance(Session);
        }
        protected void Application_End(object sender, EventArgs e) {
        }

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}
