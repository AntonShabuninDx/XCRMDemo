using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    [VisibleInReports]
    [ImageName("BO_Product")]
    [NavigationItem(true, GroupName = "Sales")]
    [System.ComponentModel.DisplayName("Product")]
    public class CRMProduct : Product {
        public CRMProduct() : base() {
            Notes = new List<Note>();
        }
        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual IList<Note> Notes { get; set; }
    }    
}
