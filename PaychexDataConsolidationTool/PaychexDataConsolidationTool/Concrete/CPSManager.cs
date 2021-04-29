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

        /// <summary>
        /// Count - Integer count of all records retrieved
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns> Integer count of all records retrieved </returns>
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

        /// <summary>
        /// ListAll - List of All CPS joined with Status to put into tabular view
        /// </summary>
        /// <param name="skip"> What Offset you are on </param>
        /// <param name="take"> How many rows you grab </param>
        /// <param name="orderBy"> What column to order by </param>
        /// <param name="startDate"> Start Date</param>
        /// <param name="endDate"> End Date</param>
        /// <param name="direction"> Direction to sort by </param>
        /// <returns>List of All CPS joined with Status to put into tabular view</returns>
        public Task<List<CPSStatus>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC")
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

        /// <summary>
        /// getDates - List of Dates in database between range
        /// </summary>
        /// <param name="startDate"> Start Date</param>
        /// <param name="endDate"> End Date </param>
        /// <returns></returns>
        public Task<List<CPS>> getDates(string startDate, string endDate)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[ClientsPerStatus] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC", null, commandType: CommandType.Text));
            return cpss;
        }

        /// <summary>
        /// getStatuses - gets all statuses currently in database
        /// </summary>
        /// <returns>List of all statuses currently in database </returns>
        public Task<List<Status>> getStatuses()
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<Status>
                ($"SELECT StatusName FROM [dbo].[Status] ORDER BY StatusId ASC", null, commandType: CommandType.Text));
            return cpss;
        }

        /// <summary>
        /// getStatusReportData - Data used for the line graph in Clients Per Status
        /// </summary>
        /// <param name="startDate"> Start Date </param>
        /// <param name="endDate"> End Date </param>
        /// <param name="statusName"> Status Name </param>
        /// <returns>List of CPS joined with Status</returns>
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
            return cpss;
        }

        /// <summary>
        /// getMostRecentDate - Gets most recent date in database
        /// </summary>
        /// <returns>List of all objects matching the most recent date</returns>
        public Task<List<CPS>> getMostRecentDate()
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[ClientsPerStatus]", null, commandType: CommandType.Text));
            return cpss;
        }

        /// <summary>
        /// getMostRecentStatusCounts- Gets data to populate the pi chart on the dashboard for Clients Per Status
        /// </summary>
        /// <param name="date"> Date </param>
        /// <returns>List of CPS joined with Status objects</returns>
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
