using DevExpress.Persistent.Base.Security;
using XCRM.Module;

namespace XCRM.Win {
	partial class XCRMWinApplication {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void DisposeCore() {
			if((components != null)) {
				components.Dispose();
			}
			base.DisposeCore();
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.systemModule1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
			this.winSystemModule1 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
            this.viewVariantsModule1 = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
			this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.XCRMFullAppModule1 = new XCRMModule();
			this.validationWinModule1 = new DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule();
            this.securityStrategyComplex = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.securityStrategyComplex.SupportNavigationPermissionsForTypes = false;
			this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            this.fileAttachmentsWindowsFormsModule1 = new DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule();
            this.reportsModule1 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
            this.reportsWinModule1 = new DevExpress.ExpressApp.ReportsV2.Win.ReportsWindowsFormsModuleV2();
            this.kpiModule = new DevExpress.ExpressApp.Kpi.KpiModule();
            this.chartModule = new DevExpress.ExpressApp.Chart.ChartModule();
            this.chartWindowsFormsModule = new DevExpress.ExpressApp.Chart.Win.ChartWindowsFormsModule();
            this.schedulerWindowsFormsModule1 = new DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule();
            this.schedulerModuleBase1 = new DevExpress.ExpressApp.Scheduler.SchedulerModuleBase();
            this.treeListEditorsWindowsFormsModule1 = new DevExpress.ExpressApp.TreeListEditors.Win.TreeListEditorsWindowsFormsModule();
            this.objectsModule1 = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
            this.pivotGridModule = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
            this.pivotGridWindowsFormsModule = new DevExpress.ExpressApp.PivotGrid.Win.PivotGridWindowsFormsModule();
            this.dashboardsModule = new DevExpress.ExpressApp.Dashboards.DashboardsModule();
            this.dashboardsWindowsFormsModule = new DevExpress.ExpressApp.Dashboards.Win.DashboardsWindowsFormsModule();
            this.officeWindowsFormsModule = new DevExpress.ExpressApp.Office.Win.OfficeWindowsFormsModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();

            this.reportsModule1.EnableInplaceReports = true;
            this.reportsModule1.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.EF.ReportDataV2);
            this.reportsModule1.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
            // 
            // authenticationStandard1
            this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            this.authenticationStandard1.UserType = typeof(XCRM.Module.Data.MyAppUser);
            // 
            // dashboardsModule1
            // 
            this.dashboardsModule.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.EF.DashboardData);
            this.dashboardsModule.GenerateNavigationItem = false;
            // 
            // securityComplex
            // 
            this.securityStrategyComplex.UserType = typeof(XCRM.Module.Data.MyAppUser);
            this.securityStrategyComplex.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex.RoleType = typeof(XCRM.Module.Data.PersistentRole);
            this.Security = securityStrategyComplex; 
			// 
			// XCRMWinApplication
			// 
			this.ApplicationName = "XCRM";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
			this.Modules.Add(this.systemModule1);
			this.Modules.Add(this.winSystemModule1);
            this.Modules.Add(this.viewVariantsModule1);
			this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.XCRMFullAppModule1);
			this.Modules.Add(this.validationWinModule1);
            this.Modules.Add(this.fileAttachmentsWindowsFormsModule1);
            this.Modules.Add(this.reportsModule1);
            this.Modules.Add(this.reportsWinModule1);
            this.Modules.Add(this.kpiModule);
            this.Modules.Add(this.dashboardsModule);
            this.Modules.Add(this.chartModule);
            this.Modules.Add(this.chartWindowsFormsModule);
            this.Modules.Add(this.schedulerWindowsFormsModule1);
            this.Modules.Add(this.schedulerModuleBase1);
            this.Modules.Add(this.treeListEditorsWindowsFormsModule1);
            this.Modules.Add(this.objectsModule1);
            this.Modules.Add(this.pivotGridModule);
            this.Modules.Add(this.pivotGridWindowsFormsModule);
            this.Modules.Add(this.dashboardsWindowsFormsModule);
            this.Modules.Add(this.officeWindowsFormsModule);
            this.UseOldTemplates = false;
            this.LastLogonParametersRead += new System.EventHandler<DevExpress.ExpressApp.LastLogonParametersReadEventArgs>(this.XCRMWinApplication_LastLogonParametersRead);
			this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.XCRMWinApplication_DatabaseVersionMismatch);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

		#endregion

		private DevExpress.ExpressApp.SystemModule.SystemModule systemModule1;
		private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule winSystemModule1;
		private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private XCRMModule XCRMFullAppModule1;
		private DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule validationWinModule1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex;
		private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
        private DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule viewVariantsModule1;
        private DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule fileAttachmentsWindowsFormsModule1;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModule1;
        private DevExpress.ExpressApp.ReportsV2.Win.ReportsWindowsFormsModuleV2 reportsWinModule1;
        private DevExpress.ExpressApp.Kpi.KpiModule kpiModule;
        private DevExpress.ExpressApp.Chart.ChartModule chartModule;
        private DevExpress.ExpressApp.Chart.Win.ChartWindowsFormsModule chartWindowsFormsModule;
        private DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule schedulerWindowsFormsModule1;
        private DevExpress.ExpressApp.Scheduler.SchedulerModuleBase schedulerModuleBase1;
        private DevExpress.ExpressApp.TreeListEditors.Win.TreeListEditorsWindowsFormsModule treeListEditorsWindowsFormsModule1;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule1;
        private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule;
        private DevExpress.ExpressApp.PivotGrid.Win.PivotGridWindowsFormsModule pivotGridWindowsFormsModule;
        private DevExpress.ExpressApp.Dashboards.DashboardsModule dashboardsModule;
        private DevExpress.ExpressApp.Dashboards.Win.DashboardsWindowsFormsModule dashboardsWindowsFormsModule;
        private DevExpress.ExpressApp.Office.Win.OfficeWindowsFormsModule officeWindowsFormsModule;
    }
}
