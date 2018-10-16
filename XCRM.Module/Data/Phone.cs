using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    public interface IPhones {
        string OtherPhone { get; set; }
        string MobilePhone { get; set; }
        string OfficePhone { get; set; }
        string HomePhone { get; set; }
        string Fax { get; set; }
        void CopyTo(IPhones targetPhones);
    }
    public static class IPhonesLogic {
        public static void Copy(IPhones source, IPhones target) {
            if (source != null && target != null) {
                target.OtherPhone = source.OtherPhone;
                target.MobilePhone = source.MobilePhone;
                target.OfficePhone = source.OfficePhone;
                target.HomePhone = source.HomePhone;
                target.Fax = source.Fax;
            }
        }
    }
}
