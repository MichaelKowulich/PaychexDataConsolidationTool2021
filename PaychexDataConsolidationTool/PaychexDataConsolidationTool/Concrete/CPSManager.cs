using Dapper;
using PaychexDataConsolidationTool.Contracts;
using PaychexDataConsolidationTool.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Concrete
{
    public class CPSManager : ICPSManager
    {
        private readonly IDapperManager _dapperManager;

        public CPSManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }

        public Task<int> Create(CPS cps)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("DateOfReport", cps.DateOfReport, DbType.String);
            dbPara.Add("StatusId", cps.StatusId, DbType.String);
            dbPara.Add("StatusCountAsOfDate", cps.StatusCountAsOfDate, DbType.Int64);
            var cpsId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_CPS]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return cpsId;
        }

        public Task<CPS> GetById(int id)
        {
            var cps = Task.FromResult(_dapperManager.Get<CPS>($"select * from [ClientStatus] where ClientStatusId = {id}", null,
                    commandType: CommandType.Text));
            return cps;
        }

        public Task<int> Delete(int id)
        {
            var deleteCPS = Task.FromResult(_dapperManager.Execute($"Delete [ClientStatus] where ClientStatusId = {id}", null,
                    commandType: CommandType.Text));
            return deleteCPS;
        }
        public Task<List<CPS>> SearchDates(string orderBy, string startDate, string endDate, string direction = "DESC")
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"Select [dbo].[ClientsPerStatus].DateOfReport, [dbo].[Status].StatusName, [dbo].[ClientsPerStatus].StatusCountAsOfDate " +
                $"from[dbo].[ClientsPerStatus], [dbo].[Status] " +
                $"WHERE[dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"AND[dbo].[ClientsPerStatus].DateOfReport >= '{startDate}' AND[dbo].[ClientsPerStatus].DateOfReport <= '{endDate}' " +
                $"ORDER BY[dbo].[ClientsPerStatus].DateOfReport, [dbo].[Status].StatusId ", null, commandType: CommandType.Text));
            return cpss;
        }
        public Task<int> CountAfterSearch(string startDate, string endDate)
        {
            var totCPS = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}'", null,
                    commandType: CommandType.Text));
            //Console.WriteLine($"select COUNT(*) from [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}'");
            return totCPS;
        }

        public Task<List<CPS>> getDates(string startDate, string endDate)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerStatus] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerStatus] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC");
            return cpss;
        }

        public Task<List<Status>> getStatuses()
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<Status>
                ($"SELECT StatusName FROM [dbo].[Status] ORDER BY StatusId ASC", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT StatusName FROM [dbo].[Status] ORDER BY StatusId ASC");
            return cpss;
        }

        public Task<List<CPSStatus>> getStatusReportData(string startDate, string endDate, string statusName)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPSStatus>
                ($"Select FORMAT ([dbo].[ClientsPerStatus].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Status].StatusName, [dbo].[ClientsPerStatus].StatusCountAsOfDate " +
                $"from[dbo].[ClientsPerStatus], [dbo].[Status] " +
                $"WHERE[dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"AND[dbo].[ClientsPerStatus].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerStatus].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[Status].StatusName = '{statusName}' " +
                $"ORDER BY[dbo].[ClientsPerStatus].DateOfReport", null, commandType: CommandType.Text));
            Console.WriteLine($"Select [dbo].[ClientsPerStatus].DateOfReport, [dbo].[Status].StatusName, [dbo].[ClientsPerStatus].StatusCountAsOfDate " +
                $"from[dbo].[ClientsPerStatus], [dbo].[Status] " +
                $"WHERE[dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"AND[dbo].[ClientsPerStatus].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerStatus].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[Status].StatusName = '{statusName}' " +
                $"ORDER BY[dbo].[ClientsPerStatus].DateOfReport");
            return cpss;
        }
        public Task<int> Update(CPS cps)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("ClientStatusId", cps.ClientStatusId);
            dbPara.Add("DateOfReport", cps.DateOfReport);
            dbPara.Add("StatusId", cps.StatusId, DbType.String);
            dbPara.Add("StatusCountAsOfDate", cps.StatusCountAsOfDate, DbType.Int64);

            var updateCPS = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_CPS]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateCPS;
        }
    }
}
