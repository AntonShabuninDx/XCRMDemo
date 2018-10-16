using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace XCRM.Module.Data {
    public enum SaleStage {
        Unknown = 0,
        Lead = 1,
        Opportunity = 2,
        Quote = 3,
        Order = 4,
        Invoice = 5,
        Sale = 6
    }

    public interface ISaleStageHistory {
        IList<LeadHistoryRecord> LeadHistoryRecords { get; set; }
        LeadHistoryRecord FindLeadHistoryRecord(SaleStage saleStage);
    }

    public interface ISaleStageHistoryTarget {
        SaleStage SaleStage { get; }
        ISaleStageHistory History { get; }
    }

    public static class ISaleStageHistoryTargetLogic {
        public static ISaleStageHistory GetHistory(ISaleStageHistoryTarget saleStageHistoryTarget) {
            ISaleStageHistory result = null;
            if (GetLeadFrom(saleStageHistoryTarget as ILeadTarget, out result)) {
                return result;
            }
            if (GetLeadFrom(saleStageHistoryTarget as IOpportunityTarget, out result)) {
                return result;
            }
            return null;
        }
        public static void UpdateHistory(ISaleStageHistoryTarget saleStageHistoryTarget, IObjectSpace os) {
            if (saleStageHistoryTarget.History != null) {
                bool addHistoryRecord = true;
                foreach (LeadHistoryRecord historyRecord in saleStageHistoryTarget.History.LeadHistoryRecords) {
                    if (historyRecord.SaleStage == saleStageHistoryTarget.SaleStage)
                        addHistoryRecord = false;
                }
                if (addHistoryRecord) {
                    LeadHistoryRecord historyRecord = os.CreateObject<LeadHistoryRecord>();
                    historyRecord.SaleStage = saleStageHistoryTarget.SaleStage;
                    saleStageHistoryTarget.History.LeadHistoryRecords.Add(historyRecord);
                }
            }
        }
        private static bool GetLeadFrom(ILeadTarget leadTarget, out ISaleStageHistory result) {
            result = null;
            if (leadTarget != null) {
                result = leadTarget.SourceLead as ISaleStageHistory;
                return true;
            }
            return false;
        }
        private static bool GetLeadFrom(IOpportunityTarget opportunityTarget, out ISaleStageHistory result) {
            result = null;
            if (opportunityTarget != null) {
                return GetLeadFrom(opportunityTarget.SourceOpportunity as ILeadTarget, out result);
            }
            return false;
        }
    }

    public class LeadHistoryRecord {
        [Key]
        public int LeadHistoryRecordID { get; protected set; }
        public SaleStage SaleStage { get; set; }
        public int StageOrder {
            get { return (int)SaleStage; }
        }
    }
}
