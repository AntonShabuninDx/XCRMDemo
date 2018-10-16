using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Text;

namespace XCRM.Module.Data {
	[DisplayProperty("DisplayName")]
	[VisibleInReports(false)]
    [ImageName("BO_User")]
    public class DCUser : DevExpress.Persistent.BaseImpl.EF.User {
		public DCUser() : base() {
		}

        [SearchMemberOptions(SearchMemberMode.Exclude)]
		public String DisplayName { get; set; }

        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual IList<Lead> Leads { get; set; }

        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual IList<CRMActivity> Events { get; set; }

        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual IList<Customer> Customers { get; set; }

        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual IList<SaleBase> Sales { get; set; }
    }

    [VisibleInReports(false)]
    [ImageName("BO_Role")]
    public class PersistentRole : DevExpress.Persistent.BaseImpl.EF.Role {
        public PersistentRole() : base() {
        }
    }
}
