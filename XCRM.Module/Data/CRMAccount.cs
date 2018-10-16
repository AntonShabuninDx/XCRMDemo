using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
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
    [ImageName("BO_Customer")]
    [System.ComponentModel.DisplayName("Account")]
    [NavigationItem(true, GroupName = "Sales")]
    public class CRMAccount : Account, IQuoteCustomer, IOrderCustomer, IOpportunityCustomer, ILeadTarget, IGenericEmail, IAddressable, IPhones, IXafEntityObject, IObjectSpaceLink {
        public CRMAccount() : base() {
            Notes = new List<Note>();
        }
        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual IList<Note> Notes { get; set; }

        #region ILeadTarget
        [VisibleInListView(false)]
        public virtual Lead SourceLead { get; set; }
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
        }
        #endregion
    }
}
