using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    public enum OrderStatus {
        None = 0,
        [XafDisplayName("No Money")]
        NoMoney = 1,
        [XafDisplayName("Cusomer Canceled")]
        CustomerCanceled = 2
    }

    public interface IOrderCustomer {
        IList<Order> Orders { get; set; }
    }

    public interface IOrderOpportunity {
        IList<Order> Orders { get; set; }
    }

    public interface IOrderTarget {
        Order SourceOrder { get; set; }
    }

    [VisibleInReports]
    [ImageName("BO_Order")]
    [Appearance("Disable IOrder by StatusReason", TargetItems = "*", Criteria = "Status != ##Enum#XCRM.Module.Data.OrderStatus,None#", Enabled = false)]
    [ListViewFilter("Closed Orders", "[Status] != ##Enum#XCRM.Module.Data.OrderStatus,None#", "Closed Orders", Index = 0)]
    [ListViewFilter("My Open Orders", "[Owner.ID] = custom('CurrentUserId') AND [Status] = ##Enum#XCRM.Module.Data.OrderStatus,None#", "My Open Orders", true, Index = 1)]
    [ListViewFilter("Open Orders", "[Status] == ##Enum#XCRM.Module.Data.OrderStatus,None#", "Open Orders", Index = 2)]
    [Table("Orders")]
    [DisplayName("Order")]
    public class Order : SaleBase, IOpportunityTarget, IObjectSpaceLink {
        public Order()
            : base() {
                Invoices = new List<CRMInvoice>();
        }
        private Opportunity opportunity;
        private Customer backRefCustomer;

        public OrderStatus Status { get; set; }

        [VisibleInListView(false)]
        public Nullable<DateTime> CancelDate { get; set; }

        [VisibleInListView(false)]
        public virtual Quote SourceQuote { get; set; }

        [System.ComponentModel.Browsable(false)]
        [VisibleInListView(false)]
        public virtual Customer BackRefCustomer {
            get { return backRefCustomer; }
            set { backRefCustomer = value; PotentialCustomer = BackRefCustomer; }
        }

        [System.ComponentModel.Browsable(false)]
        [VisibleInListView(false)]
        public virtual Opportunity Opportunity {
            get { return opportunity; }
            set { opportunity = value; SourceOpportunity = Opportunity; }
        }

        [InverseProperty("SourceOrder")]
        public virtual IList<CRMInvoice> Invoices { get; set; }

        #region IOpportunityTarget
        private Opportunity sourceOpportunity;
        [VisibleInListView(false)]
        [NotMapped]
        public virtual Opportunity SourceOpportunity {
            get { return sourceOpportunity; }
            set {
                sourceOpportunity = value;
                SourceOpportunityUpdated();
            }
        }
        protected virtual void SourceOpportunityUpdated() {
            if (Opportunity != SourceOpportunity) {
                Opportunity = SourceOpportunity;
            }
        }
        #endregion

        [Action(PredefinedCategory.View, Caption = "Cancel Order...", AutoCommit = true,
            TargetObjectsCriteria = "Status = ##Enum#XCRM.Module.Data.OrderStatus,None#",
            SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void Cancel(CancelOrderParameters parameters) {
            Status = parameters.Status;
            CancelDate = parameters.CancelDate;
            ObjectSpace.SetModified(this);
        }


        [Action(PredefinedCategory.View, Caption = "Create Invoice...", AutoCommit = true,
          SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void CreateInvoiceFromOrder() {
            //TODO: Move to container
            //IOrderTarget invoice = ObjectSpace.CreateObject<IOrderTarget>();
            //Copy(invoice);
            //Invoices.Add(invoice);
        }
        protected override void PotentialCustomerUpdated() {
            if (BackRefCustomer != PotentialCustomer) {
                BackRefCustomer = PotentialCustomer;
            }
        }
    }

    [DomainComponent]
    public class CancelOrderParameters {
        public CancelOrderParameters(Order order) {
            this.CancelDate = DateTime.Now;
        }
        public OrderStatus Status { get; set; }
        public Nullable<DateTime> CancelDate { get; set; }
    }
}
