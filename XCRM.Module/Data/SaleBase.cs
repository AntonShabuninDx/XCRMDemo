using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    public class SaleBaseValidationRules {
        public const string NameIsRequired = "SaleBaseNameIsRequired";
        public const string PotentialCustomerIsRequired = "PotentialCustomerIsRequired";
        public const string DiscountIsGreaterThanOrEqualZero = "SaleBaseDiscountIsGreaterThanOrEqualZero";
        public const string DiscountPercentIn0_100Range = "SaleBaseDiscountPercentIn0_100Range";
    }

    public enum ShippingMethod {
        None = 0,
        Airborne = 1,
        DHL = 2,
        FedEx = 3,
        UPS = 4,
        PostalMail = 5,
        FullLoad = 6,
        WillCall = 7
    }

    [Appearance("Disable Amount", TargetItems = "Amount", Criteria = "", Enabled = false)]
    [DisplayName("Sale Base")]
    public abstract class SaleBase : IObjectSpaceLink, IXafEntityObject {
        public SaleBase() : base() {
                SaleItems = new List<SaleItem>();
        }
        private bool isLoaded = false;
        private bool isCreated = false;
        private decimal discount;
        private decimal discountPercent;
        private Customer potentialCustomer;

        [Key]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int SaleBaseId { get; protected set; }

        [NotMapped]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        public string DisplayName {
            get { return ReflectionHelper.GetObjectDisplayText(this); }
        }

        [NotMapped]
        protected bool IsLoaded { get { return isLoaded; } }

        [NotMapped]
        protected bool IsCreated { get { return isCreated; } }

        [RuleRequiredField(SaleBaseValidationRules.NameIsRequired, DefaultContexts.Save)]
        public string Name { get; set; }

        [RuleRequiredField(SaleBaseValidationRules.PotentialCustomerIsRequired, DefaultContexts.Save)]
        public virtual Customer PotentialCustomer {
            get { return potentialCustomer; }
            set { potentialCustomer = value; PotentialCustomerUpdated(); }
        }

        public decimal Amount { get; set; }

        [DevExpress.ExpressApp.DC.Aggregated]
        [InverseProperty("SaleBase")]
        public virtual IList<SaleItem> SaleItems { get; set; }

        [VisibleInListView(false)]
        public virtual DCUser Owner { get; set; }

        [RuleValueComparison(SaleBaseValidationRules.DiscountIsGreaterThanOrEqualZero, DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 0)]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public decimal Discount {
            get { return discount; }
            set { discount = value; UpdateAmount(); } 
        }

        [RuleRange(SaleBaseValidationRules.DiscountPercentIn0_100Range, DefaultContexts.Save, 0, 100)]
        [ImmediatePostData]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [VisibleInListView(false)]
        public decimal DiscountPercent {
            get { return discountPercent; }
            set { discountPercent = value; UpdateAmount(); }
        }

        [NotMapped]
        [VisibleInListView(false)]
        public decimal DetailAmount {
            get {
                decimal amount = 0;
                foreach (SaleItem saleItem in SaleItems) {
                    amount += saleItem.Amount;
                }
                return amount;
            }
        }

        [VisibleInListView(false)]
        public string ID { get; set; }

        [VisibleInDetailView(false), VisibleInListView(false)]
        public Nullable<DateTime> CreatedOn { get; set; }

        public void Copy(SaleBase target) {
            //rewrite with Cloner - S35833
            target.Discount = Discount;
            target.DiscountPercent = DiscountPercent;
            target.Name = Name;
            target.Owner = Owner;
            target.PotentialCustomer = PotentialCustomer;

            foreach (SaleItem saleItem in SaleItems) {
                SaleItem saleItemNew = ObjectSpace.CreateObject<SaleItem>();
                saleItemNew.Discount = saleItem.Discount;
                saleItemNew.Product = saleItem.Product;
                saleItemNew.Quantity = saleItem.Quantity;
                saleItemNew.UpdateAmount();

                target.SaleItems.Add(saleItemNew);
            }
            target.UpdateAmount();

            if (target is IOpportunityTarget) {
                if (this is Opportunity) {
                    ((IOpportunityTarget)target).SourceOpportunity = (Opportunity)this;
                } else if (this is IOpportunityTarget) {
                    ((IOpportunityTarget)target).SourceOpportunity = ((IOpportunityTarget)this).SourceOpportunity;
                }
            }
            if (this is IGenericAddressableSale) {
                ((IGenericAddressableSale)this).Copy(target as IGenericAddressableSale);
            }
        }

        public virtual void UpdateAmount() {
            if (!IsLoaded && !IsCreated) { 
                return; 
            }

            Amount = DetailAmount - Discount - (DetailAmount - Discount) * DiscountPercent / 100;
        }

        protected abstract void PotentialCustomerUpdated();

        #region IObjectSpaceLink
        [NotMapped]
        [Browsable(false)]
        public IObjectSpace ObjectSpace { get; set; }
        #endregion

        #region IXafEntityObject
        public virtual void OnCreated() {
            CreatedOn = DateTime.Now;
            isCreated = true;
        }
        public virtual void OnLoaded() {
            isLoaded = true;
        }
        public virtual void OnSaving() { }
        #endregion
    }

    public interface IGenericAddressableSale {
        Address BillToAddress { get; set; }
        Address ShipToAddress { get; set; }
        Nullable<DateTime> DeliveryDate { get; set; }
        ShippingMethod? ShippingMethod { get; set; }
        void Copy(IGenericAddressableSale target);
    }

    public static class IGenericAddressableSaleLogic {
        public static void Copy(IGenericAddressableSale source, IGenericAddressableSale target) {
            if (source != null && target != null) {
                target.BillToAddress = source.BillToAddress;
                target.ShipToAddress = source.ShipToAddress;
                target.ShippingMethod = source.ShippingMethod;
                target.DeliveryDate = source.DeliveryDate;
            }
        }
    }
}
