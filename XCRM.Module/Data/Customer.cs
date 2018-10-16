using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using System.ComponentModel.DataAnnotations;

namespace XCRM.Module.Data {
    public enum CustomerStatus {
        Inactive = 0,
        Active = 1
    }

    public enum PreferredContactMethod {
        Any = 0,
        Email = 1,
        Phone = 2,
        Fax = 3,
        Mail = 4
    }

    [DefaultProperty("Name")]
    [DisplayName("Customer")]
    public abstract class Customer : IOpportunityCustomer, INotifyPropertyChanged {
        public Customer() : base() {
            Opportunities = new List<Opportunity>();
            Invoices = new List<Invoice>();
            Quotes = new List<Quote>();
            Orders = new List<Order>();
            Status = CustomerStatus.Active;
        }

        [Key]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int CustomerId { get; protected set; }

        [NotMapped]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        public string DisplayName {
            get { return ReflectionHelper.GetObjectDisplayText(this); }
        }

        [InverseProperty("Customer")]
        public virtual IList<Opportunity> Opportunities { get; set; }

		[VisibleInListView(false), VisibleInDetailView(false)]
        public String Name { get; set; }

        public virtual DCUser Owner { get; set; }

        private CustomerStatus status;
        public CustomerStatus Status {
            get { return status; }
            set {
                if(status != value) {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public PreferredContactMethod PreferredContactMethod { get; set; }

        [VisibleInListView(false)]
        public decimal CreditLimit { get; set; }

        [VisibleInListView(false)]
        public bool CreditHold { get; set; }

        [NotMapped]
        public decimal SaleAmount {
            get {
                decimal amount = 0;
                foreach (Invoice invoice in Invoices) {
                    if (invoice.Status == InvoiceStatus.Completed) {
                        amount += invoice.Amount;
                    }
                }
                return amount;
            }
        }

        [InverseProperty("Customer")]
        public virtual IList<Invoice> Invoices { get; set; }

        [InverseProperty("BackRefCustomer")]
        public virtual IList<Quote> Quotes { get; set; }

        [InverseProperty("BackRefCustomer")]
        public virtual IList<Order> Orders { get; set; }

        //TODO: Move to controller
        [Action(PredefinedCategory.View, Caption = "Activate...", AutoCommit = true,
            ConfirmationMessage = "This operation will set the selected object as Active.", TargetObjectsCriteria = "Status != ##Enum#XCRM.Module.Data.CustomerStatus,Active#",
            SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects)]
        public void Activate() {
            Status = CustomerStatus.Active;
        }

        [Action(PredefinedCategory.View, Caption = "Deactivate...", AutoCommit = true,
            ConfirmationMessage = DeactivateConfirmationMessage, TargetObjectsCriteria = "Status = ##Enum#XCRM.Module.Data.CustomerStatus,Active#",
            SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects)]
        public void Deactivate() {
            Status = CustomerStatus.Inactive;
        }
        public const string DeactivateConfirmationMessage =
@"This action will set the object as inactive. There may be records in the system that continue to reference these inactive records.

Do you want to proceed?";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) {
            PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
            if(PropertyChanged != null) {
                PropertyChanged(this, args);
            }
        }
    }
}
