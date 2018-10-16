using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    [DomainComponent]
    public class QualifyLeadParameters {
        public const string LeadQualifyContextId = "LeadQualify";
        public static Type QualifyLeadParametersType = typeof(QualifyLeadParameters);
        public static Type IContactObjectType = typeof(Contact);
        public static Type IAccountObjectType = typeof(Account);
        public static QualifyLeadParameters CreateQualifyLeadParameters() {
            return (QualifyLeadParameters)ReflectionHelper.CreateObject(QualifyLeadParametersType);
        }

        private static void CopyToCustomer(Lead lead, Customer customer) {
            customer.Owner = lead.Owner;
            customer.PreferredContactMethod = lead.PreferredContactMethod;
            if (lead is IPhones) {
                ((IPhones)lead).CopyTo(customer as IPhones);
            }
            lead.CopyTo(customer as ILeadTarget);
            if (lead is IAddressable) {
                ((IAddressable)lead).CopyTo(customer as IAddressable);
            }
        }
        protected virtual void QualifyCore(Lead lead, IObjectSpace os) {
            lead.Status = LeadStatus.Qualified;

            Contact contact = null;
            if (Contact) {
                contact = (Contact)os.CreateObject(IContactObjectType);
                CreatedCustomer = contact;

                contact.FirstName = lead.FirstName;
                contact.LastName = lead.LastName;
                CopyToCustomer(lead, contact);
            }
            if (Account) {
                Account account = (Account)os.CreateObject(IAccountObjectType);
                CreatedCustomer = account;

                account.AccountName = lead.CompanyName;
                account.SICCode = lead.SICCode;
                account.NumberOfEmployees = lead.NumberOfEmployees;
                account.WebSite = lead.WebSite;
                if (lead is IGenericEmail) {
                    ((IGenericEmail)lead).Copy(account as IGenericEmail);
                }

                if (Contact) {
                    account.Contacts.Add(contact);
                    os.CommitChanges();
                    account.PrimaryContact = contact;
                }
                CopyToCustomer(lead, account);
            }
        }
        protected virtual void QualifyCompleted(Lead lead, IObjectSpace os) {
            CreatedCustomer = (Customer)os.GetObject(CreatedCustomer);
        }

        public QualifyLeadParameters() {
        }

        public void Qualify(Lead lead, IObjectSpace os, IObjectSpace nos) {
            Guard.CheckObjectFromObjectSpace(os, lead);
            Validator.RuleSet.Validate(os, this, LeadQualifyContextId);
            lead = nos.GetObject<Lead>(lead);
            QualifyCore(lead, nos);
            nos.CommitChanges();
            QualifyCompleted(lead, os);
        }

        public bool Account { get; set; }
        public bool Contact { get; set; }

        [RuleFromBoolProperty("IsAccountOrContactSelected", LeadQualifyContextId, "You should select Contact or/and Account",
            UsedProperties = "Account,Contact")]
        [System.ComponentModel.Browsable(false)]
        [NotMapped]
        public bool IsAccountOrContactSelected {
            get {
                return this.Account || this.Contact;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public Customer CreatedCustomer { get; private set; }
    }

    [DomainComponent]
    public class QualifyLeadParametersWithOpportunity : QualifyLeadParameters {
        public static Type IOpportunityObjectType = typeof(Opportunity);
        public bool Opportunity { get; set; }
        protected override void QualifyCore(Lead lead, IObjectSpace os) {
            base.QualifyCore(lead, os);
            if (Opportunity) {
                Opportunity opportunity = (Opportunity)os.CreateObject(IOpportunityObjectType);
                opportunity.PotentialCustomer = CreatedCustomer;
                opportunity.Name = lead.Topic;
                lead.CopyTo(opportunity as ILeadTarget);
            }
        }
    }
}
