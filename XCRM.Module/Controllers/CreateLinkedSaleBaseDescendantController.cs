using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using XCRM.Module.Data;

namespace XCRM.Module.Controllers {
    public class CreateLinkedSaleBaseDescendantController : ViewController {
        public CreateLinkedSaleBaseDescendantController() {
            TargetViewNesting = Nesting.Nested;
            TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(SaleBase);
        }

        protected override void OnActivated() {
            base.OnActivated();
            NewObjectViewController standardController = Frame.GetController<NewObjectViewController>();
            standardController.ObjectCreated += new EventHandler<ObjectCreatedEventArgs>(CreateLinkedSaleBaseDescendantController_ObjectCreated);
        }

        void CreateLinkedSaleBaseDescendantController_ObjectCreated(object sender, ObjectCreatedEventArgs e) {
            if(Frame is NestedFrame) {
                SaleBase createdObject = e.CreatedObject as SaleBase;
                SaleBase parentObject = e.ObjectSpace.GetObject<SaleBase>((Frame as NestedFrame).ViewItem.CurrentObject as SaleBase);
                if(createdObject != null && parentObject != null) {
                    parentObject.Copy(createdObject);
                }
            }
        }
    }
}

