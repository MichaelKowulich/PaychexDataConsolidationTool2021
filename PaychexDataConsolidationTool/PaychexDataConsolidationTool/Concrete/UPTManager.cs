using Dapper;
using PaychexDataConsolidationTool.Contracts;
using PaychexDataConsolidationTool.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Concrete
{
    public class UPTManager : IUPTManager
    {
        private readonly IDapperManager _dapperManager;

        public UPTManager(IDapperManager dapperManager)
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
            var totUPTS = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) " +
                $"from [dbo].[UsersPerType] " +
                $"INNER JOIN [dbo].[UserType] ON [dbo].[UserType].UserTypeId = [dbo].[UsersPerType].UserTypeId " +
                $"WHERE " +
                $"[dbo].[UsersPerType].DateOfReport >= '{startDate}' " +
                $"AND [dbo].[UsersPerType].DateOfReport <= '{endDate}';", null,
                    commandType: CommandType.Text));
            return totUPTS;
        }

        /// <summary>
        /// ListAll - List of All UPT joined with UserType to put into tabular view
        /// </summary>
        /// <param name="skip"> What Offset you are on </param>
        /// <param name="take"> How many rows you grab </param>
        /// <param name="orderBy"> What column to order by </param>
        /// <param name="startDate"> Start Date</param>
        /// <param name="endDate"> End Date</param>
        /// <param name="direction"> Direction to sort by </param>
        /// <returns>List of All UPT joined with UserType to put into tabular view</returns>
        public Task<List<UPTType>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC")
        {
            var uptt = Task.FromResult(_dapperManager.GetAll<UPTType>
                ($"Select FORMAT ([dbo].[UsersPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[UserType].UserTypeName as UserTypeName, [dbo].[UsersPerType].UserTypeCountAsOfDate as UserTypeCountAsOfDate " +
                $"from [dbo].[UsersPerType] " +
                $"INNER JOIN [dbo].[UserType] ON [dbo].[UserType].UserTypeId = [dbo].[UsersPerType].UserTypeId " +
                $"WHERE " +
                $"[dbo].[UsersPerType].DateOfReport >= '{startDate}' " +
                $"AND [dbo].[UsersPerType].DateOfReport <= '{endDate}' " +
                $"ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY;", null, commandType: CommandType.Text));
            return uptt;
        }

        /// <summary>
        /// getDates - List of Dates in database between range
        /// </summary>
        /// <param name="startDate"> Start Date</param>
        /// <param name="endDate"> End Date </param>
        /// <returns></returns>
        public Task<List<UPT>> getDates(string startDate, string endDate)
        {
            var upts = Task.FromResult(_dapperManager.GetAll<UPT>
                ($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[UsersPerType] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT DISTINCT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport FROM [dbo].[UsersPerType] WHERE DateOfReport >= '{startDate}' AND DateOfReport <= '{endDate}' ORDER BY DateOfReport ASC");
            return upts;
        }

        /// <summary>
        /// getTypes - gets all User Types currently in database
        /// </summary>
        /// <returns>List of all User Types currently in database </returns>
        public Task<List<PaychexDataConsolidationTool.Entities.UserType>> getTypes()
        {
            var upts = Task.FromResult(_dapperManager.GetAll<PaychexDataConsolidationTool.Entities.UserType>
                ($"SELECT UserTypeName FROM [dbo].[UserType] ORDER BY UserTypeId ASC", null, commandType: CommandType.Text));
            return upts;
        }

        /// <summary>
        /// getTypeReportData - Data used for the line graph in Users Per Type
        /// </summary>
        /// <param name="startDate"> Start Date </param>
        /// <param name="endDate"> End Date </param>
        /// <param name="typeName"> Type Name </param>
        /// <returns>List of UPT joined with UserType </returns>
        public Task<List<UPTType>> getTypeReportData(string startDate, string endDate, string typeName)
        {
            var uptt = Task.FromResult(_dapperManager.GetAll<UPTType>
                ($"Select FORMAT ([dbo].[UsersPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[UserType].UserTypeName, [dbo].[UsersPerType].UserTypeCountAsOfDate " +
                $"from [dbo].[UsersPerType] " +
                $"INNER JOIN [dbo].[UserType] ON [dbo].[UserType].UserTypeId = [dbo].[UsersPerType].UserTypeId " +
                $"WHERE  " +
                $"[dbo].[UsersPerType].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[UsersPerType].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[UserType].UserTypeName = '{typeName}' " +
                $"ORDER BY[dbo].[UsersPerType].DateOfReport", null, commandType: CommandType.Text));
            return uptt;
        }

        /// <summary>
        /// getMostRecentDate - Gets most recent date in database
        /// </summary>
        /// <returns>List of all objects matching the most recent date</returns>
        public Task<List<UPT>> getMostRecentDate()
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<UPT>
                ($"SELECT MAX(FORMAT (DateOfReport, 'yyyy-MM-dd') ) as DateOfReport FROM [dbo].[UsersPerType]", null, commandType: CommandType.Text));
            return cpss;
        }

        /// <summary>
        /// getMostRecentTypeCounts- Gets data to populate the pi chart on the dashboard for Users Per Type
        /// </summary>
        /// <param name="date"> Date </param>
        /// <returns>List of UPT joined with UserType objects</returns>
        public Task<List<UPTType>> getMostRecentTypeCounts(string date)
        {
            var uptt = Task.FromResult(_dapperManager.GetAll<UPTType>
                ($"SELECT FORMAT (DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[UserType].UserTypeName, [dbo].[UsersPerType].UserTypeCountAsOfDate " +
                $"FROM [dbo].[UsersPerType] " +
                $"INNER JOIN [dbo].[UserType] ON [dbo].[UserType].UserTypeId = [dbo].[UsersPerType].UserTypeId " +
                $"WHERE DateOfReport = '{date}' " +
                $"ORDER BY [dbo].[UserType].UserTypeId", null, commandType: CommandType.Text));
            return uptt;
        }
    }
}
