using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    [VisibleInReports]
    [ImageName("BO_Order")]
    [NavigationItem(true, GroupName = "Sales")]
    [DisplayName("Order")]
    public class CRMOrder : Order, ILeadTarget, IGenericAddressableSale, ISaleStageHistoryTarget, IXafEntityObject, IObjectSpaceLink {
        public CRMOrder() : base() {
            Notes = new List<Note>();
        }
        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual IList<Note> Notes { get; set; }

        #region ILeadTarget
        [VisibleInListView(false)]
        public virtual Lead SourceLead { get; set; }
        #endregion

        #region IGenericAddressableSale
        [Aggregated]
        [VisibleInListView(false)]
        public virtual Address BillToAddress { get; set; }

        [Aggregated]
        [VisibleInListView(false)]
        public virtual Address ShipToAddress { get; set; }

        [VisibleInListView(false)]
        public Nullable<DateTime> DeliveryDate { get; set; }

        [VisibleInListView(false)]
        public ShippingMethod? ShippingMethod { get; set; }

        public void Copy(IGenericAddressableSale target) {
            IGenericAddressableSaleLogic.Copy(this, target);
        }
        #endregion

        #region ISaleStageHistoryTarget
        [System.ComponentModel.Browsable(false)]
        public SaleStage SaleStage {
            get { return SaleStage.Order; }
        }

        [System.ComponentModel.Browsable(false)]
        public ISaleStageHistory History {
            get { return ISaleStageHistoryTargetLogic.GetHistory(this); }
        }
        #endregion

        #region IXafEntityObject
        public override void OnLoaded() {
            base.OnLoaded();
        }
        public override void OnSaving() {
            base.OnSaving();
            #region ISaleStageHistoryTarget
            ISaleStageHistoryTargetLogic.UpdateHistory(this, ObjectSpace);
            #endregion
        }
        public override void OnCreated() {
            base.OnCreated();
            #region IGenericAddressableSale
            if (BillToAddress == null) {
                BillToAddress = ObjectSpace.CreateObject<Address>();
            }
            if (ShipToAddress == null) {
                ShipToAddress = ObjectSpace.CreateObject<Address>();
            }
            #endregion
        }
        #endregion
    }
}
