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
                $"INNER JOIN [dbo].[Type] ON [dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND [dbo].[ClientsPerType].DateOfReport <= '{endDate}';", null,
                    commandType: CommandType.Text));
            return totCPTS;
        }

        public Task<List<CPTType>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC", string search = "")
        {
            var cptt = Task.FromResult(_dapperManager.GetAll<CPTType>
                ($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Type].TypeName as TypeName, [dbo].[ClientsPerType].TypeCountAsOfDate as TypeCountAsOfDate " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[Type] ON [dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND [dbo].[ClientsPerType].DateOfReport <= '{endDate}' " +
                $"ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY;", null, commandType: CommandType.Text));
            return cptt;
        }
        public Task<int> CountAfterSearch(string startDate, string endDate)
        {
            var totCPT = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [CPT] WHERE Date >= '{startDate}' AND Date <= '{endDate}'", null,
                    commandType: CommandType.Text));
            //Console.WriteLine($"select COUNT(*) from [CPT] WHERE Date >= '{startDate}' AND Date <= '{endDate}'");
            return totCPT;
        }

        public Task<List<CPT>> getDates(string startDate, string endDate)
        {
            var cpts = Task.FromResult(_dapperManager.GetAll<CPT>
                ($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerType] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerType] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC");
            return cpts;
        }

        public Task<List<PaychexDataConsolidationTool.Entities.Type>> getTypes()
        {
            var cpts = Task.FromResult(_dapperManager.GetAll<PaychexDataConsolidationTool.Entities.Type>
                ($"SELECT TypeName FROM [dbo].[Type] ORDER BY TypeId ASC", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT TypeName FROM [dbo].[Type] ORDER BY TypeId ASC");
            return cpts;
        }

        public Task<List<CPTType>> getTypeReportData(string startDate, string endDate, string typeName)
        {
            var cptt = Task.FromResult(_dapperManager.GetAll<CPTType>
                ($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Type].TypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[Type] ON [dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
                $"WHERE  " +
                $"[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerType].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[Type].TypeName = '{typeName}' " +
                $"ORDER BY[dbo].[ClientsPerType].DateOfReport", null, commandType: CommandType.Text));
            Console.WriteLine($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Type].TypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"from[dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[Type] ON [dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeID" +
                $"WHERE  " +
                $"[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerType].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[Type].TypeName = '{typeName}' " +
                $"ORDER BY[dbo].[ClientsPerType].DateOfReport");
            return cptt;
        }

        public Task<List<CPT>> getMostRecentDate()
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPT>
                ($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[ClientsPerType]", null, commandType: CommandType.Text));
            return cpss;
        }

        public Task<List<CPTType>> getMostRecentStatusCounts(string date)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPTType>
                ($"SELECT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Type].TypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"FROM [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[Type] ON [dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
                $"WHERE DateOfReport = '{date}' " +
                $"ORDER BY [dbo].[Type].TypeId", null, commandType: CommandType.Text));
            return cpss;
        }
    }
}
