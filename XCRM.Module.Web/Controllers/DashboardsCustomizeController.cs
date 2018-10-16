using DevExpress.DashboardWeb;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards.Web;
using DevExpress.Persistent.Base;
using System;
using DevExpress.DataAccess.ConnectionParameters;
using XCRM.Module.Dashboards;

namespace XCRM.Module.Web.Controllers {
    public class DashboardCustomizeController : ViewController<DetailView> {
        public DashboardCustomizeController() {
            TargetObjectType = typeof(IDashboardData);
        }
        protected override void OnActivated() {
            base.OnActivated();

            WebDashboardViewerViewItem dashboardViewerViewItem = View.FindItem("DashboardViewer") as WebDashboardViewerViewItem;
            if(dashboardViewerViewItem != null) {
                dashboardViewerViewItem.ControlCreated += DashboardViewerViewItem_ControlCreated;
            }
        }

        private void DashboardViewerViewItem_ControlCreated(object sender, EventArgs e) {
            WebDashboardViewerViewItem dashboardViewerViewItem = sender as WebDashboardViewerViewItem;
            dashboardViewerViewItem.DashboardControl.Height = 760;
            dashboardViewerViewItem.DashboardControl.ConfigureDataConnection += DashboardDesigner_ConfigureDataConnection;
            dashboardViewerViewItem.DashboardControl.CustomJSProperties += DashboardControl_CustomJSProperties;
            dashboardViewerViewItem.DashboardControl.ClientSideEvents.DashboardInitialized = "function(s,e) { s.GetDashboardControl().dashboard().title.visible(false); }";
        }

        private void DashboardControl_CustomJSProperties(object sender, DevExpress.Web.CustomJSPropertiesEventArgs e) {
            e.Properties["cpTitleVisible"] = false;
        }

        private void DashboardDesigner_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            if(e.ConnectionName == "GlobalSales_XCRM") {
                e.ConnectionParameters = new MsSqlConnectionParameters();
                SqlDashboardHelper.SetupSqlParameters(ObjectSpace, (MsSqlConnectionParameters)e.ConnectionParameters);
            }
        }

        protected override void OnDeactivated() {
            WebDashboardViewerViewItem dashboardViewerViewItem = View.FindItem("DashboardViewer") as WebDashboardViewerViewItem;
            if(dashboardViewerViewItem != null) {
                dashboardViewerViewItem.ControlCreated -= DashboardViewerViewItem_ControlCreated;
            }

            base.OnDeactivated();
        }
    }
}
