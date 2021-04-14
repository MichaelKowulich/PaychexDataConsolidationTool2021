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

        public Task<int> Count(string startDate, string endDate)
        {
            var totCPSS = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) " +
                $"from [dbo].[ClientsPerStatus] " +
                $"INNER JOIN [dbo].[Status] ON [dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"WHERE " +
                $"[dbo].[ClientsPerStatus].DateOfReport >= '{startDate}' " +
                $"AND [dbo].[ClientsPerStatus].DateOfReport <= '{endDate}';", null,
                    commandType: CommandType.Text));
            return totCPSS;
        }

        public Task<List<CPSStatus>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC", string search = "")
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPSStatus>
                ($"Select FORMAT ([dbo].[ClientsPerStatus].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Status].StatusName as StatusName, [dbo].[ClientsPerStatus].StatusCountAsOfDate as StatusCountAsOfDate " +
                $"from[dbo].[ClientsPerStatus] " +
                $"INNER JOIN [dbo].[Status] ON [dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"WHERE " +
                $"[dbo].[ClientsPerStatus].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerStatus].DateOfReport <= '{endDate}' " +
                $"ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY;", null, commandType: CommandType.Text));
            return cpss;
        }

        public Task<int> CountAfterSearch(string startDate, string endDate)
        {
            var totCPS = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}'", null,
                    commandType: CommandType.Text));
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
                $"from[dbo].[ClientsPerStatus] " +
                $"INNER JOIN [dbo].[Status] ON [dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"WHERE " +
                $"[dbo].[ClientsPerStatus].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerStatus].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[Status].StatusName = '{statusName}' " +
                $"ORDER BY[dbo].[ClientsPerStatus].DateOfReport", null, commandType: CommandType.Text));
            Console.WriteLine($"Select [dbo].[ClientsPerStatus].DateOfReport, [dbo].[Status].StatusName, [dbo].[ClientsPerStatus].StatusCountAsOfDate " +
                $"from[dbo].[ClientsPerStatus] " +
                $"INNER JOIN [dbo].[Status] ON [dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"WHERE " +
                $"[dbo].[ClientsPerStatus].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerStatus].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[Status].StatusName = '{statusName}' " +
                $"ORDER BY[dbo].[ClientsPerStatus].DateOfReport");
            return cpss;
        }
        public Task<List<CPS>> getMostRecentDate()
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[ClientsPerStatus]", null, commandType: CommandType.Text));
            return cpss;
        }

        public Task<List<CPSStatus>> getMostRecentStatusCounts(string date)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPSStatus>
                ($"SELECT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Status].StatusName, [dbo].[ClientsPerStatus].StatusCountAsOfDate " +
                $"FROM [dbo].[ClientsPerStatus] " +
                $"INNER JOIN [dbo].[Status] ON [dbo].[Status].StatusId = [dbo].[ClientsPerStatus].StatusId " +
                $"WHERE DateOfReport = '{date}' " +
                $"ORDER BY [dbo].[Status].StatusId", null, commandType: CommandType.Text));
            return cpss;
        }


    }
}
