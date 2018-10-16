using System;
using System.Configuration;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Internal;
using System.Text;
using DevExpress.Persistent.Base;
using XCRM.Module;

namespace XCRM.Win {
    public class Program {
        private static void winApplication_CustomizeFormattingCulture(object sender, CustomizeFormattingCultureEventArgs e) {
            e.FormattingCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        }

        [STAThread]
        public static void Main(string[] arguments) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if(Tracing.GetFileLocationFromSettings() == FileLocation.CurrentUserApplicationDataFolder) {
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            }
            Tracing.Initialize();
#if DEBUG
#else
            DevExpress.ExpressApp.Utils.ImageLoader.Instance.UseSvgImages = true;
#endif
            XCRMWinApplication winApplication = new XCRMWinApplication();
#if EasyTest
            try {
                DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
            }
            catch(Exception) { }
            DevExpress.XtraScheduler.Internal.Diagnostics.XtraSchedulerDebug.SkipInsertionCheck = true;
#endif
            winApplication.CustomizeFormattingCulture += new EventHandler<CustomizeFormattingCultureEventArgs>(winApplication_CustomizeFormattingCulture);
            SecurityAdapterHelper.Enable();
            try {
                ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["ConnectionString"];
                if(connectionStringSettings != null) {
                    winApplication.ConnectionString = connectionStringSettings.ConnectionString;
                }
                else {
                    connectionStringSettings = ConfigurationManager.ConnectionStrings["SqlExpressConnectionString"];
                    if(connectionStringSettings != null) {
                        winApplication.ConnectionString = DbEngineDetector.PatchConnectionString(connectionStringSettings.ConnectionString);
                    }
                }

                if(System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                    winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
                }
                winApplication.Setup();
                winApplication.Start();
            }
            catch(Exception e) {
                winApplication.HandleException(e);
            }
        }
    }

}
