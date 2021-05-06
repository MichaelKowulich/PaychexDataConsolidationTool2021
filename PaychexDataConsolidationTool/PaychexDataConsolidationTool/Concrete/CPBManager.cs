using Dapper;
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

        /// <summary>
        /// Count - Integer count of all records retrieved
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns> Integer count of all records retrieved </returns>
        public Task<int> Count(string startDate, string endDate)
        {
            var dbArgs = new DynamicParameters();
            dbArgs.Add("startDate", startDate);
            dbArgs.Add("endDate", endDate);
            var totCPBB = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) " +
                $"from [dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"WHERE " +
                $"[dbo].[ClientsPerBrand].DateOfReport >= @startDate " +
                $"AND [dbo].[ClientsPerBrand].DateOfReport <= @endDate ;", dbArgs,
                    commandType: CommandType.Text));
            return totCPBB;
        }

        /// <summary>
        /// ListAll - List of All CPB joined with CountType/Brand to put into tabular view
        /// </summary>
        /// <param name="skip"> What Offset you are on </param>
        /// <param name="take"> How many rows you grab </param>
        /// <param name="orderBy"> What column to order by </param>
        /// <param name="startDate"> Start Date</param>
        /// <param name="endDate"> End Date</param>
        /// <param name="countTypeName"> Name of the Count Type</param>
        /// <param name="direction"> Direction to sort by </param>
        /// <returns>List of All CPB joined with CountType/Brand to put into tabular view</returns>
        public Task<List<CPBCountTypeBrand>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string countTypeName, string direction = "DESC")
        {
            var dbArgs = new DynamicParameters();
            dbArgs.Add("startDate", startDate);
            dbArgs.Add("endDate", endDate);
            dbArgs.Add("orderBy", orderBy);
            dbArgs.Add("countTypeName", countTypeName);
            dbArgs.Add("direction", direction);
            dbArgs.Add("skip", skip);
            dbArgs.Add("take", take);
            var cpbb = Task.FromResult(_dapperManager.GetAll<CPBCountTypeBrand>
                ($"Select FORMAT ([dbo].[ClientsPerBrand].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientBrand].ClientBrandName, " +
                $"[dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName, [dbo].[ClientsPerBrand].CountAsOfDate as CountAsOfDate " +
                $"from[dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"INNER JOIN [dbo].[ClientsPerBrandCountType] ON [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeId = [dbo].[ClientsPerBrand].ClientsPerBrandCountTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerBrand].DateOfReport >= @startDate " +
                $"AND [dbo].[ClientsPerBrand].DateOfReport <= @endDate " +
                $"AND [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName = @countTypeName " +
                $"ORDER BY {orderBy} {direction} OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;", dbArgs, commandType: CommandType.Text));
            return cpbb;
        }

        /// <summary>
        /// getDates - List of Dates in database between range
        /// </summary>
        /// <param name="startDate"> Start Date</param>
        /// <param name="endDate"> End Date </param>
        /// <returns></returns>
        public Task<List<CPB>> getDates(string startDate, string endDate)
        {
            var dbArgs = new DynamicParameters();
            dbArgs.Add("startDate", startDate);
            dbArgs.Add("endDate", endDate);
            var cpbb = Task.FromResult(_dapperManager.GetAll<CPB>
                ($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerBrand] WHERE DateOfReport >= @startDate AND DateOfReport <= @endDate ORDER BY DateOfReport ASC", dbArgs, commandType: CommandType.Text));
            return cpbb;
        }

        /// <summary>
        /// getBrands - gets all brands currently in database
        /// </summary>
        /// <returns>List of all brands currently in database </returns>
        public Task<List<ClientBrand>> getBrands()
        {
            var cpbb = Task.FromResult(_dapperManager.GetAll<ClientBrand>
                ($"SELECT ClientBrandName FROM [dbo].[ClientBrand] ORDER BY ClientBrandId ASC", null, commandType: CommandType.Text));
            return cpbb;
        }

        /// <summary>
        /// getCountTypes - gets all CountTypes currently in database
        /// </summary>
        /// <returns> List of all Count Types currently in database </returns>
        public Task<List<ClientsPerBrandCountType>> getCountTypes()
        {
            var cpbb = Task.FromResult(_dapperManager.GetAll<ClientsPerBrandCountType>
                ($"SELECT ClientsPerBrandCountTypeName FROM [dbo].[ClientsPerBrandCountType] ORDER BY ClientsPerBrandCountTypeId ASC", null, commandType: CommandType.Text));
            return cpbb;
        }

        /// <summary>
        /// getBrandReportData - Data used for the line graph in Clients Per Brand
        /// </summary>
        /// <param name="startDate"> Start Date </param>
        /// <param name="endDate"> End Date </param>
        /// <param name="brandName"> Brand Name </param>
        /// <param name="countTypeName"> Count Type Name </param>
        /// <returns>List of CPB joined with CountType/Brand</returns>
        public Task<List<CPBCountTypeBrand>> getBrandReportData(string startDate, string endDate, string brandName, string countTypeName)
        {
            var dbArgs = new DynamicParameters();
            dbArgs.Add("startDate", startDate);
            dbArgs.Add("endDate", endDate);
            dbArgs.Add("brandName", brandName);
            dbArgs.Add("countTypeName", countTypeName);
            var cpbb = Task.FromResult(_dapperManager.GetAll<CPBCountTypeBrand>
                ($"Select FORMAT ([dbo].[ClientsPerBrand].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientBrand].ClientBrandName, " + 
                $"[dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName, [dbo].[ClientsPerBrand].CountAsOfDate " +
                $"FROM [dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"INNER JOIN [dbo].[ClientsPerBrandCountType] ON [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeId = [dbo].[ClientsPerBrand].ClientsPerBrandCountTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerBrand].DateOfReport >= @startDate " +
                $"AND [dbo].[ClientsPerBrand].DateOfReport <= @endDate " +
                $"AND [dbo].[ClientBrand].ClientBrandName = @brandName " +
                $"AND [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName = @countTypeName " +
                $"ORDER BY[dbo].[ClientsPerBrand].DateOfReport", dbArgs, commandType: CommandType.Text));
            return cpbb;
        }
        /// <summary>
        /// getMostRecentDate - Gets most recent date in database
        /// </summary>
        /// <returns>List of all objects matching the most recent date</returns>
        public Task<List<CPB>> getMostRecentDate()
        {
            var cpbb = Task.FromResult(_dapperManager.GetAll<CPB>
                ($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[ClientsPerBrand]", null, commandType: CommandType.Text));
            return cpbb;
        }

        /// <summary>
        /// getMostRecentBrandCountsOfType - Gets data to populate the pi chart on the dashboard per type
        /// </summary>
        /// <param name="date"> Date </param>
        /// <returns>List of CPB joined with CountType/Brand objects</returns>
        public Task<List<CPBCountTypeBrand>> getMostRecentBrandCountsOfType(string date)
        {
            var dbArgs = new DynamicParameters();
            dbArgs.Add("date", date);
            var cpss = Task.FromResult(_dapperManager.GetAll<CPBCountTypeBrand>
                ($"Select FORMAT ([dbo].[ClientsPerBrand].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientBrand].ClientBrandName, " + "" +
                $"[dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName, [dbo].[ClientsPerBrand].CountAsOfDate " +
                $"FROM [dbo].[ClientsPerBrand] " +
                $"INNER JOIN [dbo].[ClientBrand] ON [dbo].[ClientBrand].ClientBrandId = [dbo].[ClientsPerBrand].ClientBrandId " +
                $"INNER JOIN [dbo].[ClientsPerBrandCountType] ON [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeId = [dbo].[ClientsPerBrand].ClientsPerBrandCountTypeId " +
                $"WHERE DateOfReport = @date " +
                $"ORDER BY [dbo].[ClientsPerBrandCountType].ClientsPerBrandCountTypeName,[dbo].[ClientBrand].ClientBrandId ASC", dbArgs, commandType: CommandType.Text));
            return cpss;
        }


    }
}
