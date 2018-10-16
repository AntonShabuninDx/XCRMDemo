using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.EF;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCRM.Module.Controllers {
    public class CustomDeleteObjectsController : ViewController {
        protected override void OnActivated() {
            base.OnActivated();
            ObjectSpace.CustomDeleteObjects += ObjectSpace_CustomDeleteObjects;
        }
        protected override void OnDeactivated() {
            ObjectSpace.CustomDeleteObjects -= ObjectSpace_CustomDeleteObjects;
            base.OnDeactivated();
        }
        private void ForceLoadDependencies(object objectToDelete) {
            Type type = objectToDelete.GetType();
            ITypeInfo typeInfo = XafTypesInfo.Instance.FindTypeInfo(type);
            if(typeInfo != null) {
                foreach(IMemberInfo mi in typeInfo.Members) {
                    if(mi != null && mi.IsProperty) {
                        if(mi.IsList) {
                            IList list = mi.GetValue(objectToDelete) as IList;
                        }
                        else {
                            mi.GetValue(objectToDelete);
                        }
                    }
                }
            }
        }
        void ObjectSpace_CustomDeleteObjects(object sender, CustomDeleteObjectsEventArgs e) {
            if (e.Objects != null) {
                foreach (object objectToDelete in e.Objects) {
                    object deletedItem = objectToDelete;
                    if (deletedItem != null) {
                        if (deletedItem is EFDataViewRecord) {
                            deletedItem = ObjectSpace.GetObject(objectToDelete);
                        }
                        ForceLoadDependencies(deletedItem);
                    }
                }
            }
        }
    }
    public class DisableValidationOnDeleteController : WindowController {
        private void RuleSet_CustomNeedToValidateRule(object sender, DevExpress.Persistent.Validation.CustomNeedToValidateRuleEventArgs e) {
            e.Handled = true;
            e.NeedToValidateRule = false;
        }
        protected override void OnActivated() {
            base.OnActivated();

            DeleteObjectsViewController deleteObjectsViewController = Frame.GetController<DeleteObjectsViewController>();
            if(deleteObjectsViewController != null) {
                deleteObjectsViewController.DeleteAction.Executing += delegate(object sender, System.ComponentModel.CancelEventArgs e) {
                    DevExpress.Persistent.Validation.RuleSet.CustomNeedToValidateRule += RuleSet_CustomNeedToValidateRule;
                };
                deleteObjectsViewController.DeleteAction.Executed += delegate(object sender, DevExpress.ExpressApp.Actions.ActionBaseEventArgs e) {
                    DevExpress.Persistent.Validation.RuleSet.CustomNeedToValidateRule -= RuleSet_CustomNeedToValidateRule;
                };
            }
        }
    }
}
