using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel.DataAnnotations;

namespace XCRM.Module.Data {
    public enum LeadStatus {
        None = 0,
        Lost = 1,
        [XafDisplayName("Cannot Contact")]
        CannotContact = 2,
        [XafDisplayName("No Longer Interested")]
        NoLongerInterested = 3,
        Canceled = 4,
        Qualified = 5
    }

    public enum LeadSource {
        ColdCall,
        ExistingCustomer,
        SelfGenerated,
        EmployeeReferral,
        Partner,
        PublicRelations,
        Seminar,
        TradeShow,
        Web,
        WordOfMouth,
        Advertisement,
        Other
    }

    public class LeadValidationRules {
        public const string TopicIsRequired = "TopicIsRequired";
        public const string CompanyNameIsRequired = "CompanyNameIsRequired";
        public const string LastNameIsRequired = "LeadLastNameIsRequired";
    }

    public interface ILeadTarget {
        Lead SourceLead { get; set; }
    }

    [VisibleInReports]
    [ImageName("BO_Lead")]
    [Appearance("Disable ILead by Status", TargetItems = "*", Criteria = "Status != ##Enum#XCRM.Module.Data.LeadStatus,None#", Enabled = false)]
    [DefaultProperty("Name")]
    [DisplayName("Lead")]
    [ListViewFilter("Lead_OpenLeads", "[Status] = ##Enum#XCRM.Module.Data.LeadStatus,None#", "Open Leads", true, Index = 0)]
    [ListViewFilter("Lead_ClosedLeads", "[Status] != ##Enum#XCRM.Module.Data.LeadStatus,None#", "Closed Leads", Index = 2)]
    public class Lead {
        public Lead() : base() {
            CreatedOn = DateTime.Now;
        }

        [Key]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int LeadId { get; protected set; }

        [NotMapped]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        public string DisplayName {
            get { return ReflectionHelper.GetObjectDisplayText(this); }
        }

        [NotMapped]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
		[Calculated("FirstName + ' ' + LastName")]
        public string Name {
            get {
                return FirstName + " " + LastName;
            }
        }

        [RuleRequiredField(LeadValidationRules.TopicIsRequired, DefaultContexts.Save)]
        public string Topic { get; set; }

        public LeadStatus Status { get; set; }

        public Nullable<DateTime> CreatedOn { get; set; }

        [RuleRequiredField(LeadValidationRules.CompanyNameIsRequired, DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string CompanyName { get; set; }

        [RuleRequiredField(LeadValidationRules.LastNameIsRequired, DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string LastName { get; set; }

        [VisibleInListView(false)]
        public string FirstName { get; set; }

        [VisibleInListView(false)]
        public LeadSource LeadSource { get; set; }

        [VisibleInListView(false)]
        public virtual DCUser Owner { get; set; }

        [VisibleInListView(false)]
        public string JobTitle { get; set; }

        [VisibleInListView(false)]
        public string WebSite { get; set; }

        [VisibleInListView(false)]
        public decimal AnnualRevenue { get; set; }

        [VisibleInListView(false)]
        public string NumberOfEmployees { get; set; }

        [VisibleInListView(false)]
        public string SICCode { get; set; }

        [VisibleInListView(false)]
        public PreferredContactMethod PreferredContactMethod { get; set; }

        [Action(PredefinedCategory.View, Caption = "Reactivate Lead...", AutoCommit = true,
            TargetObjectsCriteria = "Status != ##Enum#XCRM.Module.Data.LeadStatus,None#",
            SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void Reactivate() {
            Status = LeadStatus.None;
        }

        public void CopyTo(ILeadTarget leadTarget) {
            if (leadTarget != null) {
                leadTarget.SourceLead = this;
            }
        }
    }
    
    public interface IGenericEmail {
        string Email { get; set; }
        void Copy(IGenericEmail target);
    }
}
