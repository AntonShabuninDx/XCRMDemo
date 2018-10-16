using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;

namespace XCRM.Module.Data {
	public class ContactValidationRules {
		public const string LastNameIsRequired = "ContactLastNameIsRequired";
	}

	[VisibleInReports]
	[ImageName("BO_Contact")]
	[DefaultProperty("FullName")]
	[DisplayName("Contact")]
	public abstract class Contact : Customer, IXafEntityObject, IObjectSpaceLink {
		private Person person;

		public Contact() {
		}

		[InverseProperty("Contacts")]
		public virtual Account Account { get; set; }

		#region Person
		[VisibleInListView(false), VisibleInDetailView(false)]
		public virtual Person Person {
			get { return person; }
			set {
				person = value;
				if(person != null) {
					Name = person.FullName;
				}
				else {
					Name = string.Empty;
				}
			}
		}
		[NotMapped]
		[RuleRequiredField(ContactValidationRules.LastNameIsRequired, DefaultContexts.Save)]
		public string LastName {
			get { return Person.LastName; }
			set {
				Person.LastName = value;
				Name = person.FullName;
			}
		}
		[NotMapped]
		public string FirstName {
			get { return Person.FirstName; }
			set {
				Person.FirstName = value;
				Name = person.FullName;
			}
		}
		[NotMapped]
		public Nullable<DateTime> BirthDate {
			get { return Person.BirthDate; }
			set { Person.BirthDate = value; }
		}
		[NotMapped]
		public string JobTitle {
			get { return Person.JobTitle; }
			set { Person.JobTitle = value; }
		}
		[NotMapped]
		public Gender Gender {
			get { return Person.Gender; }
			set { Person.Gender = value; }
		}
		[NotMapped]
		public MaritalStatus MaritalStatus {
			get { return Person.MaritalStatus; }
			set { Person.MaritalStatus = value; }
		}
		[NotMapped]
		public string SpouseName {
			get { return Person.SpouseName; }
			set { Person.SpouseName = value; }
		}
		[NotMapped]
		public Nullable<DateTime> Anniversary {
			get { return Person.Anniversary; }
			set { Person.Anniversary = value; }
		}
		[NotMapped]
		[Calculated("Person.FirstName + ' ' + Person.LastName")]
		public string FullName { get { return Name; } }
		#endregion


        public virtual MediaDataObject Photo {
            get;
            set;
        }

		#region IObjectSpaceLink
		[NotMapped]
		[Browsable(false)]
		public IObjectSpace ObjectSpace { get; set; }
		#endregion

		#region IXafEntityObject
		public virtual void OnCreated() {
			Person = ObjectSpace.CreateObject<Person>();
            Photo = ObjectSpace.CreateObject<MediaDataObject>();
		}
		public virtual void OnLoaded() { }
		public virtual void OnSaving() { }
		#endregion
	}
}
