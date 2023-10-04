using Microsoft.EntityFrameworkCore;
using SalesManagementApp.Data;
using SalesManagementApp.Models.ReportModels;
using SalesManagementApp.Services.Contracts;
using Syncfusion.Blazor.Grids;

namespace SalesManagementApp.Services
{
    public class SalesOrderReportService : ISalesOrderReportService
    {
        private readonly SalesManagementDbContext salesManagementDbContext;

        public SalesOrderReportService(SalesManagementDbContext salesManagementDbContext)
        {
            this.salesManagementDbContext = salesManagementDbContext;
        }

        //SR
        public async Task<List<GroupedFieldPriceModel>> GetEmployeePricePerMonthData()
        {
            try
            {
                var reportData = await (from s in this.salesManagementDbContext.SalesOrderReports



                                        where s.EmployeeId == 9
                                        group s by s.OrderDateTime.Month into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldPriceModel
                                        { 
                                         GroupedFieldKey=(
                                            GroupedData.Key == 1 ? "Jan":
                                            GroupedData.Key == 2 ? "Feb":
                                            GroupedData.Key == 3? "Mar":
                                            GroupedData.Key == 4 ? "Apr":
                                            GroupedData.Key == 5 ? "May":
                                            GroupedData.Key == 6 ? "Jun":
                                            GroupedData.Key == 7 ? "Jul":
                                            GroupedData.Key == 8 ? "Aug":
                                            GroupedData.Key == 9 ? "Sep":
                                            GroupedData.Key == 10 ? "Oct":
                                            GroupedData.Key == 11 ? "Nov":
                                            GroupedData.Key == 12 ? "Dec":
                                            ""
                                            ),
                                         Price = Math.Round(GroupedData.Sum(o=> o.OrderItemPrice), 2)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<List<GroupedFieldQtyModel>> GetQtyPerMonthData()
        {
            try
            {
                var reportData = await (from s in this.salesManagementDbContext.SalesOrderReports



                                        where s.EmployeeId == 9
                                        group s by s.OrderDateTime.Month into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldQtyModel
                                        {
                                            GroupedFieldKey = (
                                            GroupedData.Key == 1 ? "Jan" :
                                            GroupedData.Key == 2 ? "Feb" :
                                            GroupedData.Key == 3 ? "Mar" :
                                            GroupedData.Key == 4 ? "Apr" :
                                            GroupedData.Key == 5 ? "May" :
                                            GroupedData.Key == 6 ? "Jun" :
                                            GroupedData.Key == 7 ? "Jul" :
                                            GroupedData.Key == 8 ? "Aug" :
                                            GroupedData.Key == 9 ? "Sep" :
                                            GroupedData.Key == 10 ? "Oct" :
                                            GroupedData.Key == 11 ? "Nov" :
                                            GroupedData.Key == 12 ? "Dec" :
                                            ""
                                            ),
                                            Qty = (GroupedData.Sum(o => o.OrderItemQty))
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<GroupedFieldQtyModel>> GetQtyPerProductCategory()
        {
            try
            {
                var reportData = await (from s in this.salesManagementDbContext.SalesOrderReports
                                        group s by s.ProductCategoryName into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldQtyModel
                                        {
                                            GroupedFieldKey = GroupedData.Key,
                                            Qty = GroupedData.Sum(oi => oi.OrderItemQty)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //TL
        public async Task<List<GroupedFieldPriceModel>> GetGrossSalesPerTeamMemberData()
        {
            try
            {
                List<int> teamMemberIds = await GetTeamMemberIds(3);

                var reportData = await (from s in this.salesManagementDbContext.SalesOrderReports
                                        where teamMemberIds.Contains(s.EmployeeId)
                                        group s by s.EmployeeFirstName into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldPriceModel
                                        {
                                            GroupedFieldKey = GroupedData.Key,
                                            Price = Math.Round(GroupedData.Sum(oi=>oi.OrderItemPrice), 2)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {

                throw;
            }
        }
       

        public async Task<List<GroupedFieldQtyModel>> GetQtyPerTeamMemberData()
        {
            try
            {
                List<int> teamMemberIds = await GetTeamMemberIds(3);
                var reportData = await (from s in this.salesManagementDbContext.SalesOrderReports
                                        where teamMemberIds.Contains(s.EmployeeId)
                                        group s by s.EmployeeFirstName into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldQtyModel
                                        {
                                            GroupedFieldKey = GroupedData.Key,
                                            Qty = GroupedData.Sum(oi => oi.OrderItemQty)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<GroupedFieldQtyModel>> GetTeamQtyPerMonthData()
        {
            try
            {
                List<int> teamMemberIds = await GetTeamMemberIds(3);
                var reportData = await(from s in this.salesManagementDbContext.SalesOrderReports
                                       group s by s.OrderDateTime.Month into GroupedData
                                       orderby GroupedData.Key
                                       select new GroupedFieldQtyModel
                                       {
                                           GroupedFieldKey = (
                                           GroupedData.Key == 1 ? "Jan" :
                                           GroupedData.Key == 2 ? "Feb" :
                                           GroupedData.Key == 3 ? "Mar" :
                                           GroupedData.Key == 4 ? "Apr" :
                                           GroupedData.Key == 5 ? "May" :
                                           GroupedData.Key == 6 ? "Jun" :
                                           GroupedData.Key == 7 ? "Jul" :
                                           GroupedData.Key == 8 ? "Aug" :
                                           GroupedData.Key == 9 ? "Sep" :
                                           GroupedData.Key == 10 ? "Oct" :
                                           GroupedData.Key == 11 ? "Nov" :
                                           GroupedData.Key == 12 ? "Dec" :
                                           ""
                                           ),
                                           Qty = (GroupedData.Sum(o => o.OrderItemQty))
                                       }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task<List<int>> GetTeamMemberIds(int teamLeadId)
        {
            List<int> teamMembersIds = await this.salesManagementDbContext.Employees
                                       .Where(e => e.ReportToEmpId == teamLeadId)
                                       .Select(e => e.Id).ToListAsync();
            return teamMembersIds;
        }

        
    }
}
