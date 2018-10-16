using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;

namespace XCRM.Module.Data {
    public enum Gender {
        None = 0,
        Male = 1,
        Female = 2
    }

    public enum MaritalStatus {
        None = 0,
        Single = 1,
        Married = 2,
        Divorced = 3,
        Widowed = 4
    }

    public class PersonValidationRules {
        public const string LastNameIsRequired = "PersonLastNameIsRequired";
    }

    [DefaultProperty("FullName")]
    public class Person {

        public Person() {
            FirstName = "";
            LastName = "";
        }

        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int ID { get; protected set; }
		[NotMapped]
		[Calculated("FirstName + ' ' + LastName")]
		public String FullName {
            get { return FirstName + ' ' + LastName; }
        }
        [RuleRequiredField(PersonValidationRules.LastNameIsRequired, DefaultContexts.Save)]
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }
        public string JobTitle { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public Nullable<DateTime> Anniversary { get; set; }
    }
}
