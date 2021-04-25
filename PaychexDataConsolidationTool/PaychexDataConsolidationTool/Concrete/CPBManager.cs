﻿using Dapper;
using PaychexDataConsolidationTool.Contracts;
using PaychexDataConsolidationTool.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Concrete
{
    public class CPBManager : ICPBManager
    {
        private readonly IDapperManager _dapperManager;

        public CPBManager(IDapperManager dapperManager)
        {
            this._dapperManager = dapperManager;
        }

        public Task<int> Count(string startDate, string endDate)
        {
            var totCPBB = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) " +
                $"from [dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"WHERE " +
                $"[dbo].[ClientsPerBrand].DateOfReport >= '{startDate}' " +
                $"AND [dbo].[ClientsPerBrand].DateOfReport <= '{endDate}';", null,
                    commandType: CommandType.Text));
            return totCPBB;
        }

        public Task<List<CPBCountTypeBrand>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string countTypeName, string direction = "DESC", string search = "")
        {
            var cpbb = Task.FromResult(_dapperManager.GetAll<CPBCountTypeBrand>
                ($"Select FORMAT ([dbo].[ClientsPerBrand].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientBrand].ClientBrandName, " +
                $"[dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName, [dbo].[ClientsPerBrand].CountAsOfDate as CountAsOfDate " +
                $"from[dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"INNER JOIN [dbo].[ClientsPerBrandCountType] ON [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeId = [dbo].[ClientsPerBrand].ClientsPerBrandCountTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerBrand].DateOfReport >= '{startDate}' " +
                $"AND [dbo].[ClientsPerBrand].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName = '{countTypeName}' " +
                $"ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY;", null, commandType: CommandType.Text));
            return cpbb;
        }

        public Task<List<CPB>> getDates(string startDate, string endDate)
        {
            var cpbb = Task.FromResult(_dapperManager.GetAll<CPB>
                ($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerBrand] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerBrand] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC");
            return cpbb;
        }

        public Task<List<ClientBrand>> getBrands()
        {
            var cpbb = Task.FromResult(_dapperManager.GetAll<ClientBrand>
                ($"SELECT ClientBrandName FROM [dbo].[ClientBrand] ORDER BY ClientBrandId ASC", null, commandType: CommandType.Text));
            return cpbb;
        }
        public Task<List<ClientsPerBrandCountType>> getCountTypes()
        {
            var cpbb = Task.FromResult(_dapperManager.GetAll<ClientsPerBrandCountType>
                ($"SELECT ClientsPerBrandCountTypeName FROM [dbo].[ClientsPerBrandCountType] ORDER BY ClientsPerBrandCountTypeId ASC", null, commandType: CommandType.Text));
            return cpbb;
        }
        public Task<List<CPBCountTypeBrand>> getBrandReportData(string startDate, string endDate, string brandName, string countTypeName)
        {
            var cpbb = Task.FromResult(_dapperManager.GetAll<CPBCountTypeBrand>
                ($"Select FORMAT ([dbo].[ClientsPerBrand].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientBrand].ClientBrandName, " + 
                $"[dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName, [dbo].[ClientsPerBrand].CountAsOfDate " +
                $"FROM [dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"INNER JOIN [dbo].[ClientsPerBrandCountType] ON [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeId = [dbo].[ClientsPerBrand].ClientsPerBrandCountTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerBrand].DateOfReport >= '{startDate}' " +
                $"AND [dbo].[ClientsPerBrand].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[ClientBrand].ClientBrandName = '{brandName}' " +
                $"AND [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName = '{countTypeName}' " +
                $"ORDER BY[dbo].[ClientsPerBrand].DateOfReport", null, commandType: CommandType.Text));
            return cpbb;
        }
        public Task<List<CPB>> getMostRecentDate()
        {
            var cpbb = Task.FromResult(_dapperManager.GetAll<CPB>
                ($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[ClientsPerBrand]", null, commandType: CommandType.Text));
            return cpbb;
        }

        public Task<List<CPBCountTypeBrand>> getMostRecentBrandCountsOfType(string date)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPBCountTypeBrand>
                ($"Select FORMAT ([dbo].[ClientsPerBrand].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientBrand].ClientBrandName, " + "" +
                $"[dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName, [dbo].[ClientsPerBrand].CountAsOfDate " +
                $"FROM [dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"INNER JOIN [dbo].[ClientsPerBrandCountType] ON [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeId = [dbo].[ClientsPerBrand].ClientsPerBrandCountTypeId " +
                $"WHERE DateOfReport = '{date}' " +
                $"ORDER BY [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName,[dbo].[ClientBrand].ClientBrandId ASC", null, commandType: CommandType.Text));
            return cpss;
        }


    }
}
