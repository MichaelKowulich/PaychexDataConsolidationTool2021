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
            var totCPTS = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerType].DateOfReport >= @startDate " +
                $"AND [dbo].[ClientsPerType].DateOfReport <= @endDate ;", dbArgs,
                    commandType: CommandType.Text));
            return totCPTS;
        }

        /// <summary>
        /// ListAll - List of All CPS joined with Status to put into tabular view
        /// </summary>
        /// <param name="skip"> What Offset you are on </param>
        /// <param name="take"> How many rows you grab </param>
        /// <param name="orderBy"> What column to order by </param>
        /// <param name="startDate"> Start Date</param>
        /// <param name="endDate"> End Date</param>
        /// <param name="direction"> Direction to sort by </param>
        /// <returns>List of All CPT joined with ClientType to put into tabular view</returns>
        public Task<List<CPTType>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC")
        {
            var dbArgs = new DynamicParameters();
            dbArgs.Add("startDate", startDate);
            dbArgs.Add("endDate", endDate);
            dbArgs.Add("orderBy", orderBy);
            dbArgs.Add("direction", direction);
            dbArgs.Add("skip", skip);
            dbArgs.Add("take", take);

            var cptt = Task.FromResult(_dapperManager.GetAll<CPTType>
                ($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientType].ClientTypeName as ClientTypeName, [dbo].[ClientsPerType].TypeCountAsOfDate as TypeCountAsOfDate " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE " +
                $"[dbo].[ClientsPerType].DateOfReport >= @startDate " +
                $"AND [dbo].[ClientsPerType].DateOfReport <= @endDate " +
                $"ORDER BY {orderBy} {direction} OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;", dbArgs, commandType: CommandType.Text));
            return cptt;
        }

        /// <summary>
        /// getDates - List of Dates in database between range
        /// </summary>
        /// <param name="startDate"> Start Date</param>
        /// <param name="endDate"> End Date </param>
        /// <returns></returns>
        public Task<List<CPT>> getDates(string startDate, string endDate)
        {
            var dbArgs = new DynamicParameters();
            dbArgs.Add("startDate", startDate);
            dbArgs.Add("endDate", endDate);
            var cpts = Task.FromResult(_dapperManager.GetAll<CPT>
                ($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerType] WHERE DateOfReport >= @startDate AND DateOfReport <= @endDate ORDER BY DateOfReport ASC", dbArgs, commandType: CommandType.Text));
            return cpts;
        }

        /// <summary>
        /// getTypes - gets all Client Types currently in database
        /// </summary>
        /// <returns>List of all Client Types currently in database </returns>
        public Task<List<PaychexDataConsolidationTool.Entities.ClientType>> getTypes()
        {
            var cpts = Task.FromResult(_dapperManager.GetAll<PaychexDataConsolidationTool.Entities.ClientType>
                ($"SELECT ClientTypeName FROM [dbo].[ClientType] ORDER BY ClientTypeId ASC", null, commandType: CommandType.Text));
            return cpts;
        }

        /// <summary>
        /// getTypeReportData - Data used for the line graph in Clients Per Type
        /// </summary>
        /// <param name="startDate"> Start Date </param>
        /// <param name="endDate"> End Date </param>
        /// <param name="typeName"> Type Name </param>
        /// <returns>List of CPT joined with ClientType </returns>
        public Task<List<CPTType>> getTypeReportData(string startDate, string endDate, string typeName)
        {
            var dbArgs = new DynamicParameters();
            dbArgs.Add("startDate", startDate);
            dbArgs.Add("endDate", endDate);
            dbArgs.Add("typeName", typeName);
            var cptt = Task.FromResult(_dapperManager.GetAll<CPTType>
                ($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientType].ClientTypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"from [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE  " +
                $"[dbo].[ClientsPerType].DateOfReport >= @startDate " +
                $"AND[dbo].[ClientsPerType].DateOfReport <= @endDate " +
                $"AND [dbo].[ClientType].ClientTypeName = @typeName " +
                $"ORDER BY[dbo].[ClientsPerType].DateOfReport", dbArgs, commandType: CommandType.Text));
            return cptt;
        }

        /// <summary>
        /// getMostRecentDate - Gets most recent date in database
        /// </summary>
        /// <returns>List of all objects matching the most recent date</returns>
        public Task<List<CPT>> getMostRecentDate()
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPT>
                ($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[ClientsPerType]", null, commandType: CommandType.Text));
            return cpss;
        }

        /// <summary>
        /// getMostRecentTypeCounts- Gets data to populate the pi chart on the dashboard for Clients Per Type
        /// </summary>
        /// <param name="date"> Date </param>
        /// <returns>List of CPT joined with ClientType objects</returns>
        public Task<List<CPTType>> getMostRecentTypeCounts(string date)
        {
            var dbArgs = new DynamicParameters();
            dbArgs.Add("date", date);
            var cpss = Task.FromResult(_dapperManager.GetAll<CPTType>
                ($"SELECT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[ClientType].ClientTypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"FROM [dbo].[ClientsPerType] " +
                $"INNER JOIN [dbo].[ClientType] ON [dbo].[ClientType].ClientTypeId = [dbo].[ClientsPerType].ClientTypeId " +
                $"WHERE DateOfReport = @date " +
                $"ORDER BY [dbo].[ClientType].ClientTypeId", dbArgs, commandType: CommandType.Text));
            return cpss;
        }
    }
}
