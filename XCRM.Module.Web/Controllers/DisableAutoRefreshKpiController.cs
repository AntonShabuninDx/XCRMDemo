using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Kpi;

namespace XCRM.Module.Web.Controllers {
    public class DisableAutoRefreshKpiController : WindowController {
        protected override void OnFrameAssigned() {
            base.OnFrameAssigned();
            XCRMAspNetModule webModule = Application.Modules.FindModule<XCRMAspNetModule>();
            if(webModule != null && webModule.SiteMode) {
                AutoRefreshKpiController controller = Frame.GetController<AutoRefreshKpiController>();
                if(controller != null) {
                    controller.Active.SetItemValue("OnSite", false);
                }
            }
        }
    }
}
