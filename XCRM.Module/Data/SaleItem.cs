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
    public class SaleItemValidationRules {
        public const string ProductIsRequired = "ProductIsRequired";
        public const string QuantityIsGreaterThanOrEqualZero = "QuantityIsGreaterThanOrEqualZero";
        public const string DiscountIsGreaterThanOrEqualZero = "DiscountIsGreaterThanOrEqualZero";
    }

    [NavigationItem(false)]
    [DisplayName("Sale Item")]
    [VisibleInDashboards]
    public class SaleItem : DevExpress.ExpressApp.IXafEntityObject {
        private bool isLoaded = false;
        private bool isCreated = false;
        private Product product;
        private int quantity;
        private decimal discount;

        [NotMapped]
        protected bool IsLoaded { get { return isLoaded; } }
        
        [NotMapped]
        protected bool IsCreated { get { return isCreated; } }

        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int ID { get; protected set; }

        [VisibleInListView(false)]
        [InverseProperty("SaleItems")]
        public virtual SaleBase SaleBase { get; set; }

        [RuleRequiredField(SaleItemValidationRules.ProductIsRequired, DefaultContexts.Save)]
        [ImmediatePostData]
        public virtual Product Product {
            get { return product; }
            set { product = value; UpdateAmount(); }
        }

        [RuleValueComparison(SaleItemValidationRules.QuantityIsGreaterThanOrEqualZero, DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 0)]
        [ImmediatePostData]
        public int Quantity {
            get { return quantity; }
            set { quantity = value; UpdateAmount(); }
        }

        [RuleValueComparison(SaleItemValidationRules.DiscountIsGreaterThanOrEqualZero, DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 0)]
        [ImmediatePostData]
        public decimal Discount {
            get { return discount; }
            set { discount = value; UpdateAmount(); }
        }

        public decimal Amount { get; set; }

        [NotMapped]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public CRMInvoice Invoice {
            get {
                return SaleBase as CRMInvoice;
            }
        }

        public virtual decimal GetProductPrice() {
            // supporting method to implement price lists. Dan.
            return Product.Price;
        }
        public virtual void UpdateAmount() {
            if (!IsLoaded && !IsCreated) { 
                return; 
            }

            if (Product != null) {
                Amount = (GetProductPrice() * Quantity) - Discount;
            } else {
                Amount = 0;
            }
            if (SaleBase != null) {
                SaleBase.UpdateAmount();
            }
        }

        #region IXafEntityObject
        public void OnLoaded() {
            isLoaded = true;
        }
        public void OnCreated() {
            isCreated = true;
        }
        public void OnSaving() { }
        #endregion
    }
}
