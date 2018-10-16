using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Persistent.BaseImpl.EF.Kpi;

namespace XCRM.Module.Web.Controllers {
    public class EnableAdaptivityKpiListViewController : ViewController<ListView> {
        public EnableAdaptivityKpiListViewController() {
            TargetObjectType = typeof(KpiDefinition);
        }
        protected override void OnActivated() {
            base.OnActivated();
            ASPxGridListEditor listEditor = View.Editor as ASPxGridListEditor;
            if(listEditor != null)
                listEditor.IsAdaptive = true;
        }
    }
}
