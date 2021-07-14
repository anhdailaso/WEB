using Biz.Lib.Helpers;
using Model.Data;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Model.Repository
{
    public class WorkOrderRepository : IWorkOrderRepository, IDisposable
    {
        Model.Data.Model1 db;
        public WorkOrderRepository() => db = new Model.Data.Model1();

        public WorkOrderRepository(Model.Data.Model1 context) => db = context;

        public IEnumerable<WorkOrder> ListAll() => db.WorkOrder.ToList();

        public IEnumerable<GetWorkOrderObj> GetWorkOrder(string msnx, string msmay, string user, DateTime fromDate, DateTime toDate, int type, int lang)
        {
            List<GetWorkOrderObj>lst = new List<GetWorkOrderObj>();
            lst = db.Database.SqlQuery<GetWorkOrderObj>("GetWorkOrder @Username,@MS_N_XUONG,@MS_MAY, @fromDate, @toDate, @type, @ngonNgu", new object[]
            {
                new SqlParameter("@Username", user),
                new SqlParameter("@MS_N_XUONG", msnx),
                new SqlParameter("@MS_MAY", msmay),
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate),
                new SqlParameter("@type", type),
                new SqlParameter("@ngonNgu", lang)
            }).ToList();
            return lst;
        }

        public string GenerateWorkOrderID(string Username, int year, int month)
        {
            try
            {
                string workOrderID = GetWorkOrderByOrgUnit(Username);
                string temp = "";
                string param = year.ToString();
                if (month < 10)
                {
                    param += "0" + month.ToString();
                }
                else
                {
                    param += month.ToString();
                }
                temp = GetMaxWorkOrderID(workOrderID);
                if (temp.ToString().Length > 1)
                {
                    temp = workOrderID + "-" + temp;
                    string sTmp = null;
                    sTmp = temp.Replace(workOrderID + "-", "");
                    string temp1 = sTmp.Substring(4, 2);
                    int month1 = int.Parse(temp1);
                    if (month != month1)
                    {
                        workOrderID = GenerateWorkOrderID_New(Username, year, month);
                    }
                    else
                    {
                        if (temp.Length == 10 + workOrderID.Length)
                        {
                            temp1 = "000" + temp.Substring(7 + workOrderID.Length, 3);
                        }
                        else
                        {
                            if (temp.Length == 11 + workOrderID.Length)
                            {
                                temp1 = temp.Substring(7 + workOrderID.Length, 4);
                            }
                            else
                            {
                                temp1 = temp.Substring(7 + workOrderID.Length, 6);
                            }
                        }
                        workOrderID = temp.Substring(0, 7 + workOrderID.Length);
                        int SrNo = int.Parse(temp1) + 1;
                        for (int i = SrNo.ToString().Length; i <= 5; i++)
                        {
                            workOrderID = workOrderID + "0";
                        }

                        workOrderID = workOrderID + SrNo.ToString();
                    }
                }
                else
                {
                    workOrderID = GenerateWorkOrderID_New(Username, year, month);
                }
                return workOrderID;
            }
            catch { return ""; }
        }

        public string GetMaxWorkOrderID(string ms)
        {
            try
            {
                object[] parameter =
                    {
                    new SqlParameter("@MONTH", DateTime.Now.ToString("yyyyMM")),
                    new SqlParameter("@Ms",  ms)
                };
                return db.Database.SqlQuery<double>("getMAX_MS_PHIEU_BAO_TRI @MONTH, @Ms", parameter).FirstOrDefault().ToString();
            }
            catch { return ""; }
        }

        public string GenerateWorkOrderID_New(string Username, int year, int month)
        {
            string workOrderID = GetWorkOrderByOrgUnit(Username) + "-" + year.ToString();

            if (month > 9)
            {
                workOrderID += month.ToString();
            }
            else
            {
                workOrderID += "0" + month.ToString();
            }
            workOrderID += "000001";
            return workOrderID;
        }

        public string GetWorkOrderByOrgUnit(string Username)
        {
            if (db.General.Take(1).SingleOrDefault().DN_PBT.Value == 0) return "WO";
            try
            {
                var unit = db.OrgUnit.SingleOrDefault(x => x.Department.SingleOrDefault(y => y.User.Where(i => i.Username == Username).First().DepartmentID == y.ID).OrgUnitID == x.ID);

                if (unit == null || unit.CompactName == "")
                {
                    unit = db.OrgUnit.SingleOrDefault(x => x.Default == true);
                }

                System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.CultureInvariant | System.Text.RegularExpressions.RegexOptions.Compiled);
                return new System.Text.StringBuilder(r.Replace(unit.CompactName, String.Empty)).Replace(" ", "").ToString();
            }
            catch { return "WO"; }

        }

        public void AutoAddNewStaffIntoWorkOrder(string workOrderID) =>
            db.Database.ExecuteSqlCommand("AutoAddNewStaffIntoWorkOrder @workOrderID ", new SqlParameter("@workOrderID", workOrderID));

        public WorkOrder GetWorkOrderByID(string workOrderID) => db.WorkOrder.SingleOrDefault(x => x.ID == workOrderID);

        public void Add(WorkOrder workOrder) => db.WorkOrder.Add(workOrder);

        public void AddRange(IEnumerable<WorkOrder> lst)
        {
            db.WorkOrder.AddRange(lst);
        }

        public void SaveChanges() => db.SaveChanges();

        private bool disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Edit(WorkOrder obj)
        {
            try
            {
                var item = db.WorkOrder.Where(x => x.ID == obj.ID).SingleOrDefault();
                item.DateCreated = obj.DateCreated;
                item.TimeCreated = obj.TimeCreated;
                item.StartDatePlan = obj.StartDatePlan;
                item.EndDatePlan = obj.EndDatePlan;
                item.DueDate = obj.DueDate;
                item.Reason = obj.Reason;
                item.PriorityID = obj.PriorityID;
                item.TypeOfMaintenanceID = obj.TypeOfMaintenanceID;
                item.BreakdownStartTime = obj.BreakdownStartTime;
                item.BreakdownStartDate = obj.BreakdownStartDate;
                item.BreakdownEndTime = obj.BreakdownEndTime;
                item.BreakdownEndDate = obj.BreakdownEndDate;
                item.LastDateModified = obj.LastDateModified;
                item.MonitoredBy = obj.MonitoredBy;
                item.CreatedBy = obj.CreatedBy;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int XoaPhieuBaoTri(string workOrderID)
        {
            int ResponseCode = 0;
            try
            {
                List<SqlParameter> listParameter = new List<SqlParameter>();
                listParameter.Add(new SqlParameter("@workOrderID", workOrderID));
                ResponseCode = DBUtils.ExecuteSP<int>("SP_PHIEU_BAO_TRI_XOA", listParameter, AppName.BizSecurity);
                ResponseCode = 1;
            }
            catch (Exception)
            {
                ResponseCode = 0;
            }
            return ResponseCode;
        }

    }
}
