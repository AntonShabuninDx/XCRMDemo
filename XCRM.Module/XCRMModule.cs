using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using System.Data.Entity;
using XCRM.Module.Data;
using DevExpress.ExpressApp.ReportsV2;
using XCRM.Module.Reports;
using DevExpress.ExpressApp.Dashboards;
using DevExpress.DashboardCommon;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Utils;

namespace XCRM.Module {
    public partial class XCRMModule : ModuleBase {
        private CustomDatabaseInitializer databaseInitializer = null;
        static XCRMModule() {
            DevExpress.ExpressApp.Kpi.KpiModule.UsedExportedTypes = DevExpress.Persistent.Base.UsedExportedTypes.Custom;
            DevExpress.ExpressApp.Security.SecurityModule.UsedExportedTypes = DevExpress.Persistent.Base.UsedExportedTypes.Custom;
        }

		public XCRMModule() {
			InitializeComponent();

            databaseInitializer = new CustomDatabaseInitializer();
            Database.SetInitializer<XCRMDbContext>(databaseInitializer);

            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.FileData));
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Resource));
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Role));
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.User));
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.ReportDataV2));

            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Kpi.KpiDefinition));
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance));
            AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Kpi.KpiScorecard));
            DashboardsModule.DataProvider = new XcrmDashboardDataProvider();
            DashboardsModule.DataProvider.TopReturnedRecordsInDesigner = 100;
        }

        public override void Setup(XafApplication application) {
            base.Setup(application);

			application.OptimizedControllersCreation = true;
			databaseInitializer.Application = application;

            QualifyLeadParameters.QualifyLeadParametersType = typeof(QualifyLeadParametersWithOpportunity);
            QualifyLeadParameters.IContactObjectType = typeof(CRMContact);
            QualifyLeadParameters.IAccountObjectType = typeof(CRMAccount);
            QualifyLeadParametersWithOpportunity.IOpportunityObjectType = typeof(CRMOpportunity);
        }

        public override void Setup(ApplicationModulesManager moduleManager) {
            base.Setup(moduleManager);
            ReportsModuleV2 reportModule = moduleManager.Modules.FindModule<ReportsModuleV2>();
            reportModule.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.EF.ReportDataV2);
            DashboardsModule dashboardsModule = moduleManager.Modules.FindModule<DashboardsModule>();
            dashboardsModule.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.EF.DashboardData);
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            Updater updater = new Updater(objectSpace, versionFromDB);
#if EasyTest
            UseTestDataGenerator = true;
#endif
            updater.CreateTestGenerator += delegate(object sender, System.ComponentModel.HandledEventArgs e){
                e.Handled = UseTestDataGenerator;
            };
            PredefinedReportsUpdater predefinedReportsUpdater =
                new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);
            predefinedReportsUpdater.AddPredefinedReport<ActiveInvoices>(
            "Active Invoices", typeof(CRMInvoice), true);
            predefinedReportsUpdater.AddPredefinedReport<ContactsReport>(
            "Contacts Report", typeof(CRMContact), true);
            predefinedReportsUpdater.AddPredefinedReport<SalesHistory>(
            "Sales History", typeof(CRMOpportunity), true);
            return new ModuleUpdater[] { updater, predefinedReportsUpdater };
        }
        public bool UseTestDataGenerator {
            get;
            set;
        }
        public override void AddGeneratorUpdaters(ModelNodesGeneratorUpdaters updaters) {
            base.AddGeneratorUpdaters(updaters);
            if(ImageLoader.Instance.UseSvgImages) {
                updaters.Add(new ImageSourceNodesGeneratorSvgUpdater());
            }
        }
    }
    public class XcrmDashboardDataProvider : DashboardDataProvider {
        protected override IObjectDataSourceCustomFillService CreateViewService(IDashboardData dashboardData) {
            if(dashboardData.Title == "Sales Overview") {
                return new DashboardViewDataSourceFillService();
            }
            return base.CreateViewService(dashboardData);
        }
    }
    public class ImageSourceNodesGeneratorSvgUpdater : ModelNodesGeneratorUpdater<ImageSourceNodesGenerator> {
        public override void UpdateNode(ModelNode node) {
            IModelImageSources modelImagesSources = ((IModelImageSources)node);
            IEnumerable<IModelAssemblyResourceImageSource> assemblyImageSources = modelImagesSources.GetNodes<IModelAssemblyResourceImageSource>();
            string assemblyName = typeof(XCRMModule).Assembly.GetName().Name;
            foreach(IModelAssemblyResourceImageSource assemblyResourceImageSource in assemblyImageSources) {
                if(assemblyResourceImageSource.AssemblyName == assemblyName) {
                    assemblyResourceImageSource.Folder = "SvgImages";
                }
            }
        }

    }
}
