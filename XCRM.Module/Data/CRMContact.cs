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
    [ImageName("BO_Contact")]
    [NavigationItem(true, GroupName = "Sales")]
    [DisplayName("Contact")]
    public class CRMContact : Contact, IQuoteCustomer, IOrderCustomer, IOpportunityCustomer, ILeadTarget, IGenericEmail, IAddressable, IPhones, IXafEntityObject, IObjectSpaceLink {
        public CRMContact()
            : base() {
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
        [Aggregated]
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

        #region IXafEntityObject
        public override void OnCreated() {
            base.OnCreated();
            if (PrimaryAddress == null) {
                PrimaryAddress = ObjectSpace.CreateObject<Address>();
            }
        }
        #endregion
    }
}
