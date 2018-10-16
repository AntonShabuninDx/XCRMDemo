using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    public enum ProductStatus {
        Inactive = 0,
        Active = 1
    }

    public class ProductValidationRules {
        public const string NameIsRequired = "ProductNameIsRequired";
        public const string PriceIsGreaterThanOrEqualZero = "PriceIsGreaterThanOrEqualZero";
    }

    [DefaultProperty("Name")]
    [VisibleInReports]
    [ImageName("BO_Product")]
    [ListViewFilter("Product_ActiveProducts", "[Status] = ##Enum#XCRM.Module.Data.ProductStatus,Active#", "Active Products", true, Index = 0)]
    [ListViewFilter("Product_AllProducts", "", "All Products", Index = 1)]
    [ListViewFilter("Product_InactiveProducts", "[Status] = ##Enum#XCRM.Module.Data.ProductStatus,Inactive#", "Inactive Products", Index = 2)]
    [DisplayName("Product")]
    public class Product {
        public Product() {
            Status = ProductStatus.Active;
            SaleItems = new List<SaleItem>();
        }

        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int ID { get; protected set; }

        [RuleRequiredField(ProductValidationRules.NameIsRequired, DefaultContexts.Save)]
        public string Name { get; set; }

        [RuleValueComparison(ProductValidationRules.PriceIsGreaterThanOrEqualZero, DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 0)]
        public decimal Price { get; set; }

        public ProductStatus Status { get; set; }

        public virtual Category Category { get; set; }

        [Browsable(false)]
        public virtual IList<SaleItem> SaleItems { get; set; }

        //TODO: Move to controllers
        [Action(PredefinedCategory.Edit, Caption = "Activate Product...", AutoCommit = true,
            ConfirmationMessage = "This operation will set the selected Product as Active.", TargetObjectsCriteria = "Status != ##Enum#XCRM.Module.Data.ProductStatus,Active#",
            SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects, ImageName = "Action_Workflow_Activate")]
        public void Activate() {
            Status = ProductStatus.Active;
        }

        [Action(PredefinedCategory.Edit, Caption = "Deactivate Product...", AutoCommit = true,
            ConfirmationMessage = DeactivateConfirmationMessage, TargetObjectsCriteria = "Status = ##Enum#XCRM.Module.Data.ProductStatus,Active#",
            SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects, ImageName = "Action_Workflow_Deactivate")]
        public void Deactivate() {
            Status = ProductStatus.Inactive;
        }
        public const string DeactivateConfirmationMessage =
@"This action will set the Product as inactive. There may be records in the system that continue to reference these inactive records.

Do you want to proceed?";
    }
}
