using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using System.Collections.Generic;

namespace XCRM.Module.Data {
    [DefaultProperty("Name")]
    [ImageName("BO_Category")]
    [DisplayName("Category")]
    public class Category : HCategory, IXafEntityObject, IObjectSpaceLink {
        public Category()
            : base() {
                Products = new List<Product>();
        }
        public virtual MediaDataObject Image {
            get;
            set;
        }
        [MaxLength(4000)]
        public string Description { get; set; }

        [Browsable(false)]
        public virtual IList<Product> Products { get; set; }

        #region IXafEntityObject Members
        public void OnCreated() {
            Image = ObjectSpace.CreateObject<MediaDataObject>();
        }

        public void OnLoaded() {
        }

        public void OnSaving() {
        }

        #endregion

        #region IObjectSpaceLink Members
        [NotMapped]
        [Browsable(false)]
        public IObjectSpace ObjectSpace { get; set; }
        #endregion
    }
}
