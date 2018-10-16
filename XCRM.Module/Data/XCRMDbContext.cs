using System;
using System.Data.Common;
using System.Data.Entity;
using DevExpress.Persistent.BaseImpl.EF;

namespace XCRM.Module.Data {
    public class XCRMDbContext : DbContext {
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MyAppUser>()
                .HasRequired(u => u.Person)
                .WithOptional().Map(x => x.MapKey("AppUserId"))
                .WillCascadeOnDelete();

            modelBuilder.Entity<Role>()
                .HasMany(r => r.TypePermissions)
                .WithOptional(p => p.Role)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TypePermissionObject>()
                .HasMany(r => r.MemberPermissions)
                .WithOptional(p => p.Owner)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TypePermissionObject>()
                .HasMany(r => r.ObjectPermissions)
                .WithOptional(p => p.Owner)
                .WillCascadeOnDelete(true);
        }
		public XCRMDbContext(String connectionString)
			: base(connectionString) {
		}
        public XCRMDbContext(DbConnection connection)
			: base(connection, false) {
		}
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersistentRole> PersistentRoles { get; set; }
        public DbSet<CRMAccount> CRMAccounts { get; set; }
        public DbSet<CRMOpportunity> CRMOpportunities { get; set; }
        public DbSet<CRMLead> CRMLeads { get; set; }
        public DbSet<CRMOrder> CRMOrders { get; set; }
        public DbSet<CRMQuote> CRMQuotes { get; set; }
        public DbSet<CRMInvoice> CRMInvoices { get; set; }
        public DbSet<CRMContact> CRMContacts { get; set; }
        public DbSet<CRMProduct> CRMProducts { get; set; }
        public DbSet<CRMActivity> CRMActivities { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<DCUser> DCUsers { get; set; }
        public DbSet<MyAppUser> AppUsers { get; set; }
        public DbSet<LeadHistoryRecord> LeadHistoryRecords { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<TypePermissionObject> TypePermissionObjects { get; set; }

        public DbSet<DevExpress.Persistent.BaseImpl.EF.FileData> FileData { get; set; }
        public DbSet<DevExpress.Persistent.BaseImpl.EF.Resource> Resources { get; set; }
        public DbSet<DevExpress.Persistent.BaseImpl.EF.Role> Roles { get; set; }
        public DbSet<DevExpress.Persistent.BaseImpl.EF.User> Users { get; set; }
        public DbSet<DevExpress.Persistent.BaseImpl.EF.ReportDataV2> ReportData { get; set; }
        public DbSet<DevExpress.Persistent.BaseImpl.EF.DashboardData> DashboardData { get; set; }

        public DbSet<DevExpress.Persistent.BaseImpl.EF.Kpi.KpiDefinition> KpiDefinitions  { get; set; }
        public DbSet<DevExpress.Persistent.BaseImpl.EF.Kpi.KpiInstance> KpiInstances  { get; set; }
        public DbSet<DevExpress.Persistent.BaseImpl.EF.Kpi.KpiScorecard> KpiScorecards { get; set; }

        public DbSet<DevExpress.ExpressApp.EF.Updating.ModuleInfo> ModulesInfo { get; set; }

        public DbSet<MediaDataObject> MediaDataObjects { get; set; }
        public DbSet<MediaResourceObject> MediaResourceObjects { get; set; }
    }
}
