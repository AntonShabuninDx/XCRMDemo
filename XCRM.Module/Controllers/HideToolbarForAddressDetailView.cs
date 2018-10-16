using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Templates;
using XCRM.Module.Data;

namespace XCRM.Module.Controllers {
    public class HideToolbarForAddressDetailView : ViewController {
        public HideToolbarForAddressDetailView() {
            TargetViewNesting = Nesting.Nested;
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(Address);
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            ISupportActionsToolbarVisibility visibilityManager = Frame.Template as ISupportActionsToolbarVisibility;
            if (Frame.Template is ISupportActionsToolbarVisibility) {
                ((ISupportActionsToolbarVisibility)Frame.Template).SetVisible(false);
            }
            if (Frame.Template is ISupportStoreSettings) {
                ((ISupportStoreSettings)Frame.Template).SettingsReloaded -= new EventHandler(HideToolbarForAddressDetailView_SettingsReloaded);
                ((ISupportStoreSettings)Frame.Template).SettingsReloaded += new EventHandler(HideToolbarForAddressDetailView_SettingsReloaded);
            }
        }
        protected override void OnDeactivated() {
            if (Frame.Template is ISupportStoreSettings) {
                ((ISupportStoreSettings)Frame.Template).SettingsReloaded -= new EventHandler(HideToolbarForAddressDetailView_SettingsReloaded);
            }
            base.OnDeactivated();
        }
        void HideToolbarForAddressDetailView_SettingsReloaded(object sender, EventArgs e) {
            if (sender is ISupportActionsToolbarVisibility) {
                ((ISupportActionsToolbarVisibility)Frame.Template).SetVisible(false);
            }
        }
    }
}
