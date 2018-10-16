using DevExpress.ExpressApp;
using System;
using System.Data.Entity;
using XCRM.Module.Data;

namespace XCRM.Module
{
    public class CustomDatabaseInitializer : DropCreateDatabaseIfModelChanges<XCRMDbContext>
    {
        private XafApplication application = null;

        private void Application_CustomCheckCompatibility(object sender, CustomCheckCompatibilityEventArgs e)
        {
            Application.CustomCheckCompatibility -= Application_CustomCheckCompatibility;

            e.Handled = true;
            IObjectSpace objectSpace = e.ObjectSpaceProvider.CreateObjectSpace();
            if (objectSpace != null)
            {
                Updater updater = new Updater(objectSpace, new Version());
                updater.UpdateDatabaseAfterUpdateSchema();
            }
        }
        public override void InitializeDatabase(XCRMDbContext context)
        {
            Boolean isDatabaseCreated = context.Database.Exists();
            Boolean isDatabaseSchemaValid = false;
            Boolean isDatabaseMetadataExists = true;

            try
            {
                isDatabaseSchemaValid = context.Database.CompatibleWithModel(true);
            }
            catch
            {
                isDatabaseMetadataExists = false;
            }

            base.InitializeDatabase(context);

            if (isDatabaseCreated && !isDatabaseMetadataExists && Application != null)
            {
                // Empty database
                Application.CustomCheckCompatibility += Application_CustomCheckCompatibility;
            }
        }
        public XafApplication Application
        {
            get { return application; }
            set { application = value; }
        }
    }
}
