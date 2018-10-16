using DevExpress.ExpressApp;
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
    public enum QuoteStatus {
        None = 0,
        Lost = 1,
        Canceled = 2,
        Won = 3
    }

    public interface IQuoteCustomer {
        IList<Quote> Quotes { get; set; }
    }

    public interface IQuoteOpportunity {
        IList<Quote> Quotes { get; set; }
    }

    [VisibleInReports]
    [ImageName("BO_Quote")]
    [ListViewFilter("Closed Quotes", "[Status] != ##Enum#XCRM.Module.Data.QuoteStatus,None#", "Closed Quotes", Index = 0)]
    [ListViewFilter("My Open Quotes", "[Owner.ID] = custom('CurrentUserId') AND [Status] = ##Enum#XCRM.Module.Data.QuoteStatus,None#", "My Open Quotes", true, Index = 1)]
    [ListViewFilter("Open Quotes", "[Status] = ##Enum#XCRM.Module.Data.QuoteStatus,None#", "Open Quotes", Index = 2)]
    [DisplayName("Quote")]
    [Table("Quotes")]
    public class Quote : SaleBase, IOpportunityTarget, IObjectSpaceLink {
        public Quote()
            : base() {
                Orders = new List<Order>();
        }
        private Customer backRefCustomer;
        private Opportunity backRefOpportunity;

        public QuoteStatus Status { get; set; }

        [VisibleInListView(false)]
        public Nullable<DateTime> EffectiveTo { get; set; }

        [VisibleInListView(false)]
        public Nullable<DateTime> EffectiveFrom { get; set; }

        [VisibleInListView(false)]
        public Nullable<DateTime> DueBy { get; set; }

        [VisibleInListView(false)]
        [System.ComponentModel.Browsable(false)]
        public Nullable<DateTime> ClosedOn { get; set; }

        [InverseProperty("SourceQuote")]
        public virtual IList<Order> Orders { get; set; }

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
            if (BackRefOpportunity != SourceOpportunity) {
                BackRefOpportunity = SourceOpportunity;
            }
        }
        #endregion

        [Action(PredefinedCategory.View, Caption = "Close Quote...", AutoCommit = true,
          SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void Close(CloseQuoteParameters parameters) {
            Status = parameters.Status;
            ClosedOn = parameters.CloseDate;
        }

        [Action(PredefinedCategory.View, Caption = "Create Order...", AutoCommit = true,
          SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void CreateOrderFromQuote(CreateOrderFromQuoteParameters parameters) {
            Order order = ObjectSpace.CreateObject<Order>();
            Copy(order);
            Orders.Add(order);
            ClosedOn = parameters.DateWon;
            Status = QuoteStatus.Won;
            if (parameters.CloseOpportunity && order.SourceOpportunity != null) {
                order.SourceOpportunity.Status = OpportunityStatus.Won;
            }
        }

        [Browsable(false)]
        public virtual Customer BackRefCustomer {
            get { return backRefCustomer; }
			set {
				backRefCustomer = value;
				if (PotentialCustomer != backRefCustomer) {
					PotentialCustomer = value;
				}
			}
        }

        [System.ComponentModel.Browsable(false)]
        public virtual Opportunity BackRefOpportunity {
            get { return backRefOpportunity; }
            set { backRefOpportunity = value; SourceOpportunity = BackRefOpportunity; }
        }

        protected override void PotentialCustomerUpdated() {
            if (BackRefCustomer != PotentialCustomer) {
                BackRefCustomer = PotentialCustomer;
            }
        }
    }

    [DomainComponent]
    public class CloseQuoteParameters {
        public CloseQuoteParameters(Quote quote) {
            this.CloseDate = DateTime.Now;
            this.Status = QuoteStatus.Canceled;
        }
        public Nullable<DateTime> CloseDate { get; set; }
        public QuoteStatus Status { get; set; }
    }

    [DomainComponent]
    public class CreateOrderFromQuoteParameters {
        public CreateOrderFromQuoteParameters(Quote quote) {
            this.DateWon = DateTime.Now;
            this.CloseOpportunity = true;
        }
        public Nullable<DateTime> DateWon { get; set; }
        public bool CloseOpportunity { get; set; }
    }
}
