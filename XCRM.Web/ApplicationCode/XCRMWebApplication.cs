using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Web;
using XCRM.Module;
using System.Data.Common;
using XCRM.Module.Data;
using DevExpress.ExpressApp.EF;
using DevExpress.Web;
using XCRM.Module.Web;

namespace XCRM.Web {
    public class XCRMWebApplication : WebApplication {
		private DevExpress.ExpressApp.SystemModule.SystemModule systemModule1;
		private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule webSystemModule1;
		private SecurityModule securityModule1;
        private SecurityStrategyComplex securityStrategyComplex;
		private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule1;
		private DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule fileAttachmentsWebModule1;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModule1;
		private DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2 reportsWebModule1;
		private DevExpress.ExpressApp.Validation.ValidationModule validationModule1;
        private DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule validationAspNetModule1;
		private DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule viewVariantsModule1;
		private AuthenticationStandard authenticationStandard1;
		private DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule schedulerAspNetModule1;
		private DevExpress.ExpressApp.Scheduler.SchedulerModuleBase schedulerModuleBase1;
		private DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule htmlPropertyEditorAspNetModule1;
        private DevExpress.ExpressApp.Kpi.KpiModule kpiModule;
        private XCRMModule XCRMFullAppModule1;
        private XCRMAspNetModule xcrmAspNetModule;
        private DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule treeListEditorsAspNetModule;
        private DevExpress.ExpressApp.Chart.ChartModule chartModule;
        private DevExpress.ExpressApp.Chart.Web.ChartAspNetModule chartAspNetModule;
        private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule;
        private DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule pivotGridAspNetModule;
        private DevExpress.ExpressApp.Dashboards.DashboardsModule dashboardsModule;

		#region Default XAF configuration options (https://www.devexpress.com/kb=T501418)
		static XCRMWebApplication() {
			EnableMultipleBrowserTabsSupport = true;
			DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.AllowFilterControlHierarchy = true;
			DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.MaxFilterControlHierarchyDepth = 3;
			DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.AllowFilterControlHierarchyDefault = true;
			DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.MaxHierarchyDepthDefault = 3;
			DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = true;
			DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = false;
		}
		private void InitializeDefaults() {
			LinkNewObjectToParentImmediately = false;
			OptimizedControllersCreation = true;
			EnableModelCache = true;
		}
		#endregion
		public XCRMWebApplication() {
			InitializeComponent();
			InitializeDefaults();
            CustomizeTemplate += new EventHandler<CustomizeTemplateEventArgs>(XCRMWebApplication_CustomizeTemplate);
		}
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            if (args.Connection != null) {
                args.ObjectSpaceProvider = new EFObjectSpaceProvider(typeof(XCRMDbContext), TypesInfo, null, (DbConnection)args.Connection);
            } else {
                args.ObjectSpaceProvider = new EFObjectSpaceProvider(typeof(XCRMDbContext), TypesInfo, null, args.ConnectionString);
            }
        }
        /*protected override IHttpRequestManager CreateHttpRequestManager() {
            //return new Full.XCRM.Web.MyHttpRequestManager();
        }*/
		private void XCRMWebApplication_CustomizeTemplate(object sender, CustomizeTemplateEventArgs e) {
			Page page = e.Template as Page;
			if (page != null) {
				ASPxPanel container = page.FindControl("NC") as ASPxPanel;
				if (container != null) {
					container.Width = Unit.Pixel(245);
				}
			}
		}
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XCRMWebApplication));
            this.systemModule1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.webSystemModule1 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.securityStrategyComplex = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.securityStrategyComplex.SupportNavigationPermissionsForTypes = false;
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            this.objectsModule1 = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
            this.fileAttachmentsWebModule1 = new DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule();
            this.reportsModule1 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
            this.reportsWebModule1 = new DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2();
            this.validationModule1 = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationAspNetModule1 = new DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule();
            this.viewVariantsModule1 = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
            this.schedulerAspNetModule1 = new DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule();
            this.schedulerModuleBase1 = new DevExpress.ExpressApp.Scheduler.SchedulerModuleBase();
            this.kpiModule = new DevExpress.ExpressApp.Kpi.KpiModule();
            this.htmlPropertyEditorAspNetModule1 = new DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule();
            this.treeListEditorsAspNetModule = new DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule();
            this.pivotGridModule = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
            this.pivotGridAspNetModule = new DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule();
            this.XCRMFullAppModule1 = new XCRMModule();
            this.xcrmAspNetModule = new XCRMAspNetModule();
            this.chartModule = new DevExpress.ExpressApp.Chart.ChartModule();
            this.chartAspNetModule = new DevExpress.ExpressApp.Chart.Web.ChartAspNetModule();
            this.dashboardsModule = new DevExpress.ExpressApp.Dashboards.DashboardsModule();

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // authenticationStandard1
            this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            this.authenticationStandard1.UserType = typeof(MyAppUser);
            // 
            // securityComplex
            // 
            this.securityStrategyComplex.UserType = typeof(MyAppUser);
            this.securityStrategyComplex.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex.RoleType = typeof(PersistentRole);
            this.Security = securityStrategyComplex; 
            // 
            // reportsModule1
            // 
            this.reportsModule1.EnableInplaceReports = true;
            this.reportsModule1.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.EF.ReportDataV2);
            this.reportsModule1.ShowAdditionalNavigation = false;
            this.reportsModule1.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
            this.reportsWebModule1.ReportViewerType = DevExpress.ExpressApp.ReportsV2.Web.ReportViewerTypes.HTML5;
            // 
            // dashboardsModule1
            // 
            this.dashboardsModule.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.EF.DashboardData);
            this.dashboardsModule.GenerateNavigationItem = false;
            this.dashboardsModule.HideDirectDataSourceConnections = true;
            // 
            // validationModule1
            // 
            this.validationModule1.AllowValidationDetailsAccess = true;
            // 
            // viewVariantsModule1
            // 
            this.viewVariantsModule1.ShowAdditionalNavigation = false;
            // 
            // MainDemoWebApplication
            // 
            this.ApplicationName = "XCRM";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.systemModule1);
            this.Modules.Add(this.webSystemModule1);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.objectsModule1);
            this.Modules.Add(this.fileAttachmentsWebModule1);
            this.Modules.Add(this.dashboardsModule);
            this.Modules.Add(this.reportsModule1);
            this.Modules.Add(this.reportsWebModule1);
            this.Modules.Add(this.validationModule1);
            this.Modules.Add(this.validationAspNetModule1);
            this.Modules.Add(this.viewVariantsModule1);
            this.Modules.Add(this.schedulerModuleBase1);
            this.Modules.Add(this.schedulerAspNetModule1);
            this.Modules.Add(this.kpiModule);
            this.Modules.Add(this.chartModule);
            this.Modules.Add(this.chartAspNetModule);
            this.Modules.Add(this.htmlPropertyEditorAspNetModule1);
            this.Modules.Add(this.treeListEditorsAspNetModule);
            this.Modules.Add(this.pivotGridModule);
            this.Modules.Add(this.pivotGridAspNetModule);
            this.Modules.Add(this.XCRMFullAppModule1);
            this.Modules.Add(this.xcrmAspNetModule);
            this.LastLogonParametersRead += new System.EventHandler<DevExpress.ExpressApp.LastLogonParametersReadEventArgs>(this.FullXCRMWebApplication_LastLogonParametersRead);
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.FullXCRMWebApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        private void FullXCRMWebApplication_LastLogonParametersRead(object sender, LastLogonParametersReadEventArgs e) {
            // Just to read demo user name for logon.
            AuthenticationStandardLogonParameters logonParameters = e.LogonObject as AuthenticationStandardLogonParameters;
            if(logonParameters != null) {
                if(String.IsNullOrEmpty(logonParameters.UserName)) {
					logonParameters.UserName = XCRM.Module.Updater.AdministratorUserName;
                }
            }
        }
        private void FullXCRMWebApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
            e.Updater.Update();
            e.Handled = true;
        }
	}
}
