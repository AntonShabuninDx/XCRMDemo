using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace XCRM.Module.Data {
    public interface IAddressable {
        Address PrimaryAddress { get; set; }
        void CopyTo(IAddressable target);
    }
    public static class IAddressableLogic {
        public static void Copy(IAddressable source, IAddressable target) {
            if (source != null && source.PrimaryAddress != null && target != null && target.PrimaryAddress != null) {
                source.PrimaryAddress.CopyTo(target.PrimaryAddress);
            }
        }
    }

    [DefaultProperty("DisplayName")]
    public class Address {
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public int ID { get; protected set; }

        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        [NotMapped]
		[Calculated(
			"Iif([Street2] != null, [Street2] + ' ', '') " +
			"+ Iif([Street1] != null, [Street1] + ' ', '') " +
			"+ Iif([City] != null, [City] + ' ', '') " +
			"+ Iif([State] != null, [State] + ' ', '') " +
			"+ Iif([Zip] != null, [Zip] + ' ', '') " + 
			"+ Iif([Country] != null, [Country], '')")]
		public string DisplayName {
            get {
                return String.Format("{0} {1} {2} {3} {4} {5}", Street2, Street1, City, State, Zip, Country);
            }
        }

        public void CopyTo(Address target) {
            if (target != null) {
                target.Street1 = Street1;
                target.Street2 = Street2;
                target.City = City;
                target.State = State;
                target.Zip = Zip;
                target.Country = Country;
            }
        }
    }
}
