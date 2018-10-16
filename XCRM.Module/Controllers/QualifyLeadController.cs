using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using XCRM.Module.Data;

namespace XCRM.Module.Controllers {
    public class QualifyLeadController : ViewController {
        private PopupWindowShowAction qualifyLead;
		private PopupWindowShowAction disqualifyLead;
		private void qualifyLead_CustomizePopupWindowParams(Object sender, CustomizePopupWindowParamsEventArgs e) {
			e.View = Application.CreateDetailView(new NonPersistentObjectSpace(Application.TypesInfo), QualifyLeadParameters.CreateQualifyLeadParameters());
			((DetailView)e.View).ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
		}
		private void qualifyLead_Execute(Object sender, PopupWindowShowActionExecuteEventArgs e) {
			Lead lead = null;
			if(View.CurrentObject is XafDataViewRecord) {
				lead = (Lead)ObjectSpace.GetObject(View.CurrentObject);
			}
			else {
				lead = (Lead)View.CurrentObject;
			}
			((QualifyLeadParameters)e.PopupWindow.View.CurrentObject).Qualify(lead, View.ObjectSpace, Application.CreateObjectSpace(typeof(Lead)));
			View.ObjectSpace.CommitChanges();
			if(View is ListView) {
				View.ObjectSpace.Refresh();
			}
		}
		private void disqualifyLead_CustomizePopupWindowParams(Object sender, CustomizePopupWindowParamsEventArgs e) {
			e.View = Application.CreateDetailView(new NonPersistentObjectSpace(Application.TypesInfo), new DisqualifyLeadParameters(null));
			((DetailView)e.View).ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
		}
		private void disqualifyLead_Execute(Object sender, PopupWindowShowActionExecuteEventArgs e) {
			Lead lead = null;
			if(View.CurrentObject is XafDataViewRecord) {
				lead = (Lead)ObjectSpace.GetObject(View.CurrentObject);
			}
			else {
				lead = (Lead)View.CurrentObject;
			}
			lead.Status = ((DisqualifyLeadParameters)e.PopupWindow.View.CurrentObject).Status;
			View.ObjectSpace.CommitChanges();
			if(View is ListView) {
				View.ObjectSpace.Refresh();
			}
		}
		public QualifyLeadController() {
            TargetObjectType = typeof(Lead);
            qualifyLead = new PopupWindowShowAction(this, "QualifyLead", PredefinedCategory.View);
            qualifyLead.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            qualifyLead.Caption = "Qualify Lead...";
            qualifyLead.TargetObjectsCriteria = "Status = ##Enum#XCRM.Module.Data.LeadStatus,None#";
			qualifyLead.CustomizePopupWindowParams += qualifyLead_CustomizePopupWindowParams;
			qualifyLead.Execute += qualifyLead_Execute;

			disqualifyLead = new PopupWindowShowAction(this, "DisqualifyLead", PredefinedCategory.View);
			disqualifyLead.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
			disqualifyLead.Caption = "Disqualify Lead...";
			disqualifyLead.TargetObjectsCriteria = "Status = ##Enum#XCRM.Module.Data.LeadStatus,None#";
			disqualifyLead.CustomizePopupWindowParams += disqualifyLead_CustomizePopupWindowParams;
			disqualifyLead.Execute += disqualifyLead_Execute;
		}
    }
}
