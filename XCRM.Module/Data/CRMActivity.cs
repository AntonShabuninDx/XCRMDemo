using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    [DisplayName("Event")]
    [ImageName("BO_Scheduler")]
    public class CRMActivity : Event {
        public CRMActivity()
            : base() {
                Notes = new List<Note>();
        }
        public virtual DCUser Owner { get; set; }

        [DevExpress.ExpressApp.DC.Aggregated]
        public virtual IList<Note> Notes { get; set; }
    }
}
