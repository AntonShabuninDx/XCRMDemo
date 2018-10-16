using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    [DomainComponent]
    public class DisqualifyLeadParameters {
        public DisqualifyLeadParameters(Lead lead) {
        }
        //need S92424
        public LeadStatus Status { get; set; }
    }
}
