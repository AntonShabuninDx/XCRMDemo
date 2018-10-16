using System;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards.Win;
using DevExpress.Persistent.Base;
using XCRM.Module.Dashboards;

namespace Demo.Module.Win {
    public class DashboardCustomizeController : ViewController<DetailView> {
        WinShowDashboardDesignerController desingerController;
        WinDashboardViewerViewItem dashboardViewerViewItem;
        public DashboardCustomizeController() {
            TargetObjectType = typeof(IDashboardData);
        }
        protected override void OnActivated() {
            base.OnActivated();
            dashboardViewerViewItem = View.FindItem("DashboardViewer") as WinDashboardViewerViewItem;
            if(dashboardViewerViewItem != null) {
                dashboardViewerViewItem.ControlCreated += DashboardViewerViewItem_ControlCreated;
            }
            desingerController = Frame.GetController<WinShowDashboardDesignerController>();
            if(desingerController != null) {
                desingerController.DashboardDesignerManager.DashboardDesignerCreated += DashboardDesignerManager_DashboardDesignerCreated;
            }
        }
        protected override void OnDeactivated() {
            if(dashboardViewerViewItem != null) {
                dashboardViewerViewItem.ControlCreated -= DashboardViewerViewItem_ControlCreated;
            }
            if(desingerController != null) {
                desingerController.DashboardDesignerManager.DashboardDesignerCreated -= DashboardDesignerManager_DashboardDesignerCreated;
            }
            base.OnDeactivated();
        }
        private void DashboardDesignerManager_DashboardDesignerCreated(object sender, DashboardDesignerShownEventArgs e) {
            e.DashboardDesigner.ConfigureDataConnection += DashboardDesigner_ConfigureDataConnection;
        }
        private void DashboardDesigner_ConfigureDataConnection(object sender, DashboardConfigureDataConnectionEventArgs e) {
            ConfigureDataConnection(e);
        }
        private void DashboardViewerViewItem_ControlCreated(object sender, EventArgs e) {
            WinDashboardViewerViewItem dashboardViewerViewItem = View.FindItem("DashboardViewer") as WinDashboardViewerViewItem;
            if(dashboardViewerViewItem != null) {
                dashboardViewerViewItem.Viewer.ConfigureDataConnection += Viewer_ConfigureDataConnection;
            }
        }
        private void Viewer_ConfigureDataConnection(object sender, DashboardConfigureDataConnectionEventArgs e) {
            ConfigureDataConnection(e);
        }
        private void ConfigureDataConnection(DashboardConfigureDataConnectionEventArgs e) {
            if(e.ConnectionName == "GlobalSales_XCRM") {
                e.ConnectionParameters = new MsSqlConnectionParameters();
                SqlDashboardHelper.SetupSqlParameters(ObjectSpace, (MsSqlConnectionParameters)e.ConnectionParameters);
            }
        }
    }
}
