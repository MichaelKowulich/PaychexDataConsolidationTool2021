using Dapper;
using PaychexDataConsolidationTool.Contracts;
using PaychexDataConsolidationTool.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Concrete
{
    public class CPTManager : ICPTManager
    {
        private readonly IDapperManager _dapperManager;

        public CPTManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }
        public Task<int> Count(string startDate, string endDate)
        {
            var totCPTS = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND [dbo].[ClientsPerType].DateOfReport <= '{endDate}';", null,
                    commandType: CommandType.Text));
            return totCPTS;
        }

        public Task<List<CPTType>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC", string search = "")
        {
            var cptt = Task.FromResult(_dapperManager.GetAll<CPTType>
                ($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientType].ClientTypeName as ClientTypeName, [dbo].[ClientsPerType].TypeCountAsOfDate as TypeCountAsOfDate " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND [dbo].[ClientsPerType].DateOfReport <= '{endDate}' " +
                $"ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY;", null, commandType: CommandType.Text));
            return cptt;
        }

        public Task<List<CPT>> getDates(string startDate, string endDate)
        {
            var cpts = Task.FromResult(_dapperManager.GetAll<CPT>
                ($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerType] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerType] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC");
            return cpts;
        }

        public Task<List<PaychexDataConsolidationTool.Entities.ClientType>> getTypes()
        {
            var cpts = Task.FromResult(_dapperManager.GetAll<PaychexDataConsolidationTool.Entities.ClientType>
                ($"SELECT ClientTypeName FROM [dbo].[ClientType] ORDER BY ClientTypeId ASC", null, commandType: CommandType.Text));
            return cpts;
        }

        public Task<List<CPTType>> getTypeReportData(string startDate, string endDate, string typeName)
        {
            var cptt = Task.FromResult(_dapperManager.GetAll<CPTType>
                ($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientType].ClientTypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE  " +
                $"[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerType].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[ClientType].ClientTypeName = '{typeName}' " +
                $"ORDER BY[dbo].[ClientsPerType].DateOfReport", null, commandType: CommandType.Text));
            return cptt;
        }

        public Task<List<CPT>> getMostRecentDate()
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPT>
                ($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[ClientsPerType]", null, commandType: CommandType.Text));
            return cpss;
        }

        public Task<List<CPTType>> getMostRecentTypeCounts(string date)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPTType>
                ($"SELECT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientType].ClientTypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"FROM [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE DateOfReport = '{date}' " +
                $"ORDER BY [dbo].[ClientType].ClientTypeId", null, commandType: CommandType.Text));
            return cpss;
        }
    }
}
