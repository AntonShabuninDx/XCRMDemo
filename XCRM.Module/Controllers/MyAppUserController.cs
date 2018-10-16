using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using XCRM.Module.Data;

namespace XCRM.Module.Controllers {
    public class MyAppUserController : ObjectViewController<DetailView, MyAppUser> {
        private List<string> nonPersistentProperties = new List<string>() { "LastName", "FirstName", "BirthDate", "JobTitle", "Gender", "MaritalStatus", "SpouseName", "Anniversary" };
        private void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e) {
            MyAppUser user = e.Object as MyAppUser;
            if((user != null) && nonPersistentProperties.Contains(e.PropertyName)) {
                ObjectSpace.SetModified(ViewCurrentObject.Person);
            }
        }
        protected override void OnActivated() {
            base.OnActivated();
            View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
            View.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
        }
    }
}
