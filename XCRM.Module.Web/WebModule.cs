using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;

namespace XCRM.Module.Web {
    [ToolboxItemFilter("Xaf.Platform.Web")]
    public sealed partial class XCRMAspNetModule : ModuleBase {
        public XCRMAspNetModule() {
            InitializeComponent();
            SiteMode = false;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            return ModuleUpdater.EmptyModuleUpdaters;
        }
        public bool SiteMode { get; set; }
    }
}
