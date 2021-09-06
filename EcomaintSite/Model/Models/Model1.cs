namespace Model.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Interception;
    using System.Web;
    public partial class Model1 : IdentityDbContext<ApplicationUser>
    {
        public Model1()
            : base("name=Model1")
        {
            try
            {
                string databaseName;
                try
                {
                     databaseName = HttpContext.Current.Request.Cookies["DatabaseName"].Value;

                }
                catch
                {
                    databaseName = "CMMS_BARIA";

                }
                string connectionString = this.Database.Connection.ConnectionString;
                if (databaseName != this.Database.Connection.Database)
                {
                    this.Database.Connection.ConnectionString = connectionString.Replace(connectionString.Substring(connectionString.IndexOf("Database"), connectionString.IndexOf("User") - connectionString.IndexOf("Database")), "Database=" + databaseName + ";");
                }
            }
            catch (Exception ex) { }
            this.Database.CommandTimeout = 999999;
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<WorkSite> WorkSite { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRequest> UserRequest { get; set; }
        public DbSet<UserRequestDetail> UserRequestDetail { get; set; }
        public DbSet<UserRequestComponent> UserRequestComponent { get; set; }
        public DbSet<UserRequestDocument> UserRequestDocument { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<Formation> Formation { get; set; }
        public DbSet<WebMenu> WebMenu { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Component> Component { get; set; }
        public DbSet<ComponentInternalMonitoringParams> ComponentInternalMonitoringParams { get; set; }
        public DbSet<Monitoring> Monitoring { get; set; }
        public DbSet<MonitoringDocument> MonitoringDocument { get; set; }
        public DbSet<MonitoringOfQuantitative> MonitoringOfQuantitative { get; set; }
        public DbSet<MonitoringOfQualitative> MonitoringOfQualitative { get; set; }
        public DbSet<OrgUnit> OrgUnit { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<MasterPlan> MasterPlan { get; set; }
        public DbSet<MasterPlansOfWork> MasterPlansOfWork { get; set; }
        public DbSet<MasterPlansOfSparePart> MasterPlansOfSparePart { get; set; }
        public DbSet<TypeOfMaintenance> TypeOfMaintenance { get; set; }
        public DbSet<ReasonForRepair> ReasonForRepair { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<Languages> LANGUAGES { get; set; }
        public DbSet<MaintenanceForm> MaintenanceForm { get; set; }
        public DbSet<WebUserLogin> WebUserLogin { get; set; }
        public DbSet<ComponentOfWork> ComponentOfWork { get; set; }
        public DbSet<ComponentOfSparePart> ComponentOfSparePart { get; set; }
        public DbSet<PreventiveMaintenance> PreventiveMaintenance { get; set; }
        public DbSet<PreventiveMaintenanceOfWork> PreventiveMaintenanceOfWork { get; set; }
        public DbSet<PreventiveMaintenanceCycle> PreventiveMaintenanceCycle { get; set; }
        public DbSet<PreventiveMaintenanceOfSparePart> PreventiveMaintenanceOfSparePart { get; set; }
        public DbSet<MeasureDeviceCalibration> MeasureDeviceCalibration { get; set; }
        public DbSet<UnitTime> UnitTime { get; set; }
        public DbSet<UnitOfRuntime> UnitOfRuntime { get; set; }
        public DbSet<CalibrationCycle> CalibrationCycle { get; set; }
        public DbSet<General> General { get; set; }
        public DbSet<WorkOrder> WorkOrder { get; set; }
        public DbSet<WorkOrderOfWork> WorkOrderOfWork { get; set; }
        public DbSet<WorkOrderOfWorkIncludeStaff> WorkOrderOfWorkIncludeStaff { get; set; }
        public DbSet<WorkOrderOfWorkIncludeDetailStaff> WorkOrderOfWorkIncludeDetailStaff { get; set; }
        public DbSet<WorkOrderOfWorkIncludeSuppotingStaff> WorkOrderOfWorkIncludeSuppotingStaff { get; set; }
        public DbSet<WorkOrderOfWorkIncludeSparePart> WorkOrderOfWorkIncludeSparePart { get; set; }
        public DbSet<WorkOrderOfWorkIncludeDetailSparePart> WorkOrderOfWorkIncludeDetailSparePart { get; set; }
        public DbSet<WorkOrderOfWorkIncludeDetailSparePartAtWarehouse> WorkOrderOfWorkIncludeDetailSparePartAtWarehouse { get; set; }
        public DbSet<WorkOrderOfSupportingWork> WorkOrderOfSupportingWork { get; set; }
        public DbSet<WorkOrderOfWorkIncludeSupportingSparePart> WorkOrderOfWorkIncludeSupportingSparePart { get; set; }
        public DbSet<WorkOrderByTheStatus> WorkOrderByTheStatus { get; set; }
        public DbSet<WorkOrderDetailsByTheState> WorkOrderDetailsByTheState { get; set; }
        public DbSet<TheStatusOfWorkOrder> TheStatusOfWorkOrder { get; set; }
        public DbSet<TheStatusDetailsOfWorkOrder> TheStatusDetailsOfWorkOrder { get; set; }
        public DbSet<TypeOfMaterial> TypeOfMaterial { get; set; }
        public DbSet<Sparepart> Sparepart { get; set; }
        public DbSet<SparepartAndTypeOfDevice> SparepartAndTypeOfDevice { get; set; }
        public DbSet<SparepartAndTypeOfSparepart> SparepartAndTypeOfSparepart { get; set; }
        public DbSet<SparePartWarehouse> SparePartWarehouse { get; set; }

        public  DbSet<ST_HazardReport> ST_HazardReport { get; set; }
        public  DbSet<ST_HazardReport1> ST_HazardReport1 { get; set; }
        public  DbSet<ST_StopCard> ST_StopCard { get; set; }
        public  DbSet<ST_StopCard1> ST_StopCard1 { get; set; }
        public  DbSet<ST_StopCard2> ST_StopCard2 { get; set; }
        public  DbSet<Leadership> Leaderships { get; set; }
        public  DbSet<LeadershipDetail> LeadershipDetails { get; set; }
        public  DbSet<ST_APPROVAL_USER> ST_APPROVAL_USER { get; set; }
        public  DbSet<ST_Safety> ST_Safety { get; set; }


        public DbSet<THOI_GIAN_CHAY_MAY> THOI_GIAN_CHAY_MAY { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leadership>()
           .HasMany(e => e.LeadershipDetails)
           .WithRequired(e => e.Leadership)
           .HasForeignKey(e => e.IDLeadership)
           .WillCascadeOnDelete(false);

            modelBuilder.Entity<ST_APPROVAL_USER>()
                .Property(e => e.FORM_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Device>()
                .HasMany(e => e.UserRequestDetail)
                .WithRequired(e => e.Device)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Device>()
                .Property(e => e.CustomerID)
                .IsUnicode(false);

            modelBuilder.Entity<Device>()
                .HasMany(e => e.Component)
                .WithRequired(e => e.Device)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Device>()
                .HasMany(e => e.CalibrationCycle)
                .WithRequired(e => e.Device)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Device>()
                .HasMany(e => e.MasterPlan)
                .WithRequired(e => e.Device)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
              .HasMany(e => e.WorkOrder)
              .WithOptional(e => e.Staff)
              .HasForeignKey(e => e.MonitoredBy);

            modelBuilder.Entity<Sparepart>()
               .Property(e => e.CustomerID)
               .IsUnicode(false);
            
            modelBuilder.Entity<Sparepart>()
                .HasMany(e => e.WorkOrderOfWorkIncludeDetailSparePartAtWarehouse)
                .WithRequired(e => e.Sparepart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sparepart>()
                .HasMany(e => e.WorkOrderOfWorkIncludeSupportingSparePart)
                .WithRequired(e => e.Sparepart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrder>()
              .HasMany(e => e.WorkOrderOfSupportingWork)
              .WithRequired(e => e.WorkOrder)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrder>()
                .HasMany(e => e.WorkOrderOfWork)
                .WithRequired(e => e.WorkOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrder>()
                .HasMany(e => e.WorkOrderDetailsByTheState)
                .WithRequired(e => e.WorkOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrder>()
                .HasMany(e => e.WorkOrderByTheStatus)
                .WithRequired(e => e.WorkOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrderOfWork>()
                .Property(e => e.CostPerHour)
                .IsUnicode(false);

            modelBuilder.Entity<WorkOrderOfWork>()
                .Property(e => e.Currency)
                .IsUnicode(false);

            modelBuilder.Entity<WorkOrderOfWork>()
                .HasMany(e => e.WorkOrderOfWorkIncludeStaff)
                .WithRequired(e => e.WorkOrderOfWork)
                .HasForeignKey(e => new { e.WorkOrderID, e.WorkID, e.ComponentID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrderOfWork>()
                .HasMany(e => e.WorkOrderOfWorkIncludeSparePart)
                .WithRequired(e => e.WorkOrderOfWork)
                .HasForeignKey(e => new { e.WorkOrderID, e.WorkID, e.ComponentID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrderOfWorkIncludeStaff>()
                .HasMany(e => e.WorkOrderOfWorkIncludeDetailStaff)
                .WithRequired(e => e.WorkOrderOfWorkIncludeStaff)
                .HasForeignKey(e => new { e.WorkOrderID, e.WorkID, e.ComponentID, e.StaffID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrderOfWorkIncludeSparePart>()
                .HasMany(e => e.WorkOrderOfWorkIncludeDetailSparePart)
                .WithRequired(e => e.WorkOrderOfWorkIncludeSparePart)
                .HasForeignKey(e => new { e.WorkOrderID, e.WorkID, e.ComponentID, e.SparePartID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrderOfSupportingWork>()
                .HasMany(e => e.WorkOrderOfWorkIncludeSuppotingStaff)
                .WithRequired(e => e.WorkOrderOfSupportingWork)
                .HasForeignKey(e => new { e.WorkOrderID, e.ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrderOfSupportingWork>()
                .HasMany(e => e.WorkOrderOfWorkIncludeSupportingSparePart)
                .WithRequired(e => e.WorkOrderOfSupportingWork)
                .HasForeignKey(e => new { e.WorkOrderID, e.SupportingWorkID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TheStatusOfWorkOrder>()
                .HasMany(e => e.WorkOrder)
                .WithOptional(e => e.TheStatusOfWorkOrder1)
                .HasForeignKey(e => e.TheStatusOfWorkOrderID);

            modelBuilder.Entity<Device>()
                .HasMany(e => e.UserRequestDetail)
                .WithRequired(e => e.Device)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkSite>()
                .HasMany(e => e.WorkSite1)
                .WithOptional(e => e.WorkSite2);
            // .HasForeignKey(e => e.MS_CHA);

            modelBuilder.Entity<Formation>()
                .HasMany(e => e.Formation1);

            modelBuilder.Entity<UserRequest>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<UserRequest>()
                .HasMany(e => e.UserRequestDetail)
                .WithRequired(e => e.UserRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRequestDetail>()
                .HasMany(e => e.UserRequestComponent)
                .WithRequired(e => e.UserRequestDetail)
                .HasForeignKey(e => new { e.UserRequestID, e.DeviceID, e.ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRequestDetail>()
                .HasMany(e => e.UserRequestDocument)
                .WithRequired(e => e.UserRequestDetail)
                .HasForeignKey(e => new { e.UserRequestID, e.DeviceID, e.ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ComponentInternalMonitoringParams>()
                .HasMany(e => e.MonitoringOfQuantitative)
                .WithRequired(e => e.ComponentInternalMonitoringParams)
                .HasForeignKey(e => new { e.DeviceID, e.MonitoringParamsID, e.ComponentID, e.ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Component>()
                .HasMany(e => e.ComponentOfSparePart)
                .WithRequired(e => e.Component)
                .HasForeignKey(e => new { e.DeviceID, e.ComponentID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Component>()
                .HasMany(e => e.ComponentOfWork)
                .WithRequired(e => e.Component)
                .HasForeignKey(e => new { e.DeviceID, e.ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Work>()
                .HasMany(e => e.ComponentOfWork)
                .WithRequired(e => e.Work)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CalibrationCycle>()
                .HasMany(e => e.MeasureDeviceCalibration)
                .WithRequired(e => e.CalibrationCycle)
                .HasForeignKey(e => new { e.DeviceID, e.SparePartID, e.LocationID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PreventiveMaintenance>()
              .HasMany(e => e.PreventiveMaintenanceCycle)
              .WithRequired(e => e.PreventiveMaintenance)
              .HasForeignKey(e => new { e.DeviceID, e.TypeOfMaintenanceID })
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<PreventiveMaintenance>()
                .HasMany(e => e.PreventiveMaintenanceOfWork)
                .WithRequired(e => e.PreventiveMaintenance)
                .HasForeignKey(e => new { e.DeviceID, e.TypeOfMaintenanceID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PreventiveMaintenanceOfWork>()
                .HasMany(e => e.PreventiveMaintenanceOfSparePart)
                .WithRequired(e => e.PreventiveMaintenanceOfWork)
                .HasForeignKey(e => new { e.DeviceID, e.TypeOfMaintenanceID, e.WorkID, e.ComponentID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeOfMaintenance>()
              .HasMany(e => e.PreventiveMaintenance)
              .WithRequired(e => e.TypeOfMaintenance)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<Monitoring>()
                .HasMany(e => e.MonitoringOfQuantitative)
                .WithRequired(e => e.Monitoring)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonitoringOfQuantitative>()
                .HasMany(e => e.MonitoringDocument)
                .WithRequired(e => e.MonitoringOfQuantitative)
                .HasForeignKey(e => new { e.MonitoringID, e.DeviceID, e.MonitoringParamsID, e.ComponentID, e.ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonitoringOfQuantitative>()
                .HasMany(e => e.MonitoringOfQualitative)
                .WithRequired(e => e.MonitoringOfQuantitative)
                .HasForeignKey(e => new { e.MonitoringID, e.DeviceID, e.MonitoringParamsID, e.ComponentID, e.ID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaintenanceForm>()
                .HasMany(e => e.TypeOfMaintenance)
                .WithRequired(e => e.MaintenanceForm)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<MasterPlansOfWork>()
                          .HasMany(e => e.MasterPlansOfSparePart)
                          .WithRequired(e => e.MasterPlansOfWork)
                          .HasForeignKey(e => new { e.DeviceID, e.PlanID, e.WorkID, e.ComponentID })
                          .WillCascadeOnDelete(false);
   
            modelBuilder.Entity<MasterPlan>()
                        .HasMany(e => e.MasterPlansOfWork)
                        .WithRequired(e => e.MasterPlan)
                        .HasForeignKey(e => e.PlanID)
                        .WillCascadeOnDelete(false);


            modelBuilder.Entity<Work>()
                .HasMany(e => e.MasterPlansOfWork)
                .WithRequired(e => e.Work)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReasonForRepair>()
              .HasMany(e => e.MasterPlan)
              .WithOptional(e => e.ReasonForRepair)
              .HasForeignKey(e => e.ReasonForRepairID);

            modelBuilder.Entity<User>();
            modelBuilder.Entity<Priority>();
            modelBuilder.Entity<WebMenu>();

            modelBuilder.Entity<Budget>();
            modelBuilder.Entity<Staff>();

            modelBuilder.Entity<Team>()
               .HasMany(e => e.Staff)
               .WithRequired(e => e.Team)
               .HasForeignKey(e => e.DepartmentID)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Team)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrgUnit>()
                  .HasMany(e => e.Budget)
                  .WithRequired(e => e.OrgUnit)
                  .HasForeignKey(e => e.OrgUnitID)
                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrgUnit>()
                .HasMany(e => e.CostCenters)
                .WithRequired(e => e.OrgUnit)
                .HasForeignKey(e => e.OrgUnitID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrgUnit>()
                .HasMany(e => e.Department)
                .WithRequired(e => e.OrgUnit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Budget>()
             .HasMany(e => e.CostCenters)
             .WithMany(e => e.Budget)
             .Map(m => m.ToTable("DISTRIBUTION_RULE").MapLeftKey("MS_BP_CHIU_PHI").MapRightKey("MS_COSTCENTER"));

           // modelBuilder.Entity<THOI_GIAN_CHAY_MAY>()
           //.Property(f => f.NGAY)
           //.HasColumnType("datetime");
            base.OnModelCreating(modelBuilder);
            //DbInterception.Add(new DateInterceptor());

        }

        public System.Data.Entity.DbSet<Model.Data.GetMasterPlansObj> GetMasterPlansObjs { get; set; }

        public System.Data.Entity.DbSet<Model.Data.HazardReportViewModel> HazardReportViewModels { get; set; }
    }
}
