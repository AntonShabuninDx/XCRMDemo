using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace XCRM.Module.Data {
    public class AccountValidationRules {
        public const string AccountNameIsRequired = "AccountNameIsRequired";
    }

    [VisibleInReports]
    [ImageName("BO_Customer")]
	[DefaultProperty("AccountName")]
	[DisplayName("Account")]
	public class Account : Customer {
		private String accountName;
		
		public Account()
			: base() {
            Contacts = new List<Contact>();
        }
		
		[RuleRequiredField(AccountValidationRules.AccountNameIsRequired, DefaultContexts.Save)]
		public String AccountName {
			get { return accountName; }
			set {
				accountName = value;
				Name = accountName;
			}
		}

        public virtual Contact PrimaryContact { get; set; }

        [VisibleInListView(false)]
        public string WebSite { get; set; }

        [VisibleInListView(false)]
        public string NumberOfEmployees { get; set; }

        [VisibleInListView(false)]
        public string SICCode { get; set; }

        [VisibleInListView(false)]
        public decimal AnnualRevenue { get; set; }

        [InverseProperty("Account")]
        public virtual IList<Contact> Contacts { get; set; }
    }
}
