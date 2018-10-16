using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    [VisibleInReports]
    [ImageName("BO_Lead")]
    [NavigationItem(true, GroupName = "Sales")]
    [System.ComponentModel.DisplayName("Lead")]
    public class CRMLead : Lead, ISaleStageHistory, IGenericEmail, IAddressable, IPhones, IXafEntityObject, IObjectSpaceLink {
        public CRMLead() : base() {
            Notes = new List<Note>();
            LeadHistoryRecords = new List<LeadHistoryRecord>();
        }

        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual IList<Note> Notes { get; set; }

        #region Sale Stage History
        [DevExpress.ExpressApp.DC.Aggregated, VisibleInDetailView(false)]
        public virtual IList<LeadHistoryRecord> LeadHistoryRecords { get; set; }
        public LeadHistoryRecord FindLeadHistoryRecord(SaleStage saleStage) {
            List<LeadHistoryRecord> historyRecords = new List<LeadHistoryRecord>(LeadHistoryRecords);
            LeadHistoryRecord result = null;
            foreach (LeadHistoryRecord record in historyRecords) {
                if (record.SaleStage == saleStage) {
                    if (result != null) {
                        throw new InvalidOperationException("Lead history item is duplicated.");
                    }
                    result = record;
                }
            }
            return result;
        }
        #endregion

        #region IGenericEmail
        [VisibleInListView(false)]
        public string Email { get; set; }

        public void Copy(IGenericEmail target) {
            if (target != null) {
                target.Email = Email;
            }
        }
        #endregion

        #region IAddressable
        [DevExpress.ExpressApp.DC.Aggregated]
        [VisibleInListView(false)]
        public virtual Address PrimaryAddress { get; set; }

        public void CopyTo(IAddressable target) {
            IAddressableLogic.Copy(this, target);
        }
        #endregion

        #region IPhones
        public string OtherPhone { get; set; }
        public string MobilePhone { get; set; }
        public string OfficePhone { get; set; }
        public string HomePhone { get; set; }
        public string Fax { get; set; }

        public void CopyTo(IPhones targetPhones) {
            IPhonesLogic.Copy(this, targetPhones);
        }
        #endregion

        #region IObjectSpaceLink
        [NotMapped]
        [Browsable(false)]
        public IObjectSpace ObjectSpace { get; set; }
        #endregion

        #region IXafEntityObject
        public void OnLoaded() { }
        public void OnSaving() { }
        public void OnCreated() {
            if (PrimaryAddress == null) {
                PrimaryAddress = ObjectSpace.CreateObject<Address>();
            }

            LeadHistoryRecord historyRecord = ObjectSpace.CreateObject<LeadHistoryRecord>();
            historyRecord.SaleStage = SaleStage.Lead;
            LeadHistoryRecords.Add(historyRecord);
        }
        #endregion
    }
}
