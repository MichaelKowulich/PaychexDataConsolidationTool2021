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
            dbPara.Add("Date", cps.Date, DbType.String);
            dbPara.Add("Status", cps.Status, DbType.String);
            dbPara.Add("Total", cps.Total, DbType.Int64);
            var cpsId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_CPS]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return cpsId;
        }

        public Task<CPS> GetById(int id)
        {
            var cps = Task.FromResult(_dapperManager.Get<CPS>($"select * from [CPS] where ID = {id}", null,
                    commandType: CommandType.Text));
            return cps;
        }

        public Task<int> Delete(int id)
        {
            var deleteCPS = Task.FromResult(_dapperManager.Execute($"Delete [CPS] where ID = {id}", null,
                    commandType: CommandType.Text));
            return deleteCPS;
        }

        public Task<int> Count(string search)
        {
            var totCPS = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) from [CPS] WHERE Status like '%{search}%'", null,
                    commandType: CommandType.Text));
            return totCPS;
        }

        public Task<List<CPS>> ListAll(string orderBy, string direction = "DESC", string search = "")
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT FORMAT (Date, 'yyyy-MM-dd') as Date, Status, Total FROM [CPS] WHERE Date like '%{search}%' ORDER BY {orderBy} {direction};", null, commandType: CommandType.Text));
            return cpss;
        }
        public Task<List<CPS>> SearchDates(string orderBy, string startDate, string endDate, string direction = "DESC")
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT FORMAT (Date, 'yyyy-MM-dd') as Date, Status, Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' ORDER BY {orderBy} {direction}; ", null, commandType: CommandType.Text));
           // Console.WriteLine($"SELECT FORMAT (Date, 'yyyy-MM-dd') as Date, Status, Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY;");
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
                ($"SELECT DISTINCT FORMAT (Date, 'yyyy-MM-dd') as Date FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' ORDER BY Date ASC", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT DISTINCT FORMAT (Date, 'yyyy-MM-dd') as Date FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' ORDER BY Date ASC");
            return cpss;
        }

        public Task<List<CPS>> getInactives(string startDate, string endDate)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Inactive' ORDER BY Date ASC;", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Inactive' ORDER BY Date ASC");
            return cpss;
        }
        public Task<List<CPS>> getActives(string startDate, string endDate)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Active' ORDER BY Date ASC;", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Active' ORDER BY Date ASC");
            return cpss;
        }

        public Task<List<CPS>> getDemos(string startDate, string endDate)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Demo' ORDER BY Date ASC;", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Demo' ORDER BY Date ASC");
            return cpss;
        }

        public Task<List<CPS>> getMasters(string startDate, string endDate)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Master' ORDER BY Date ASC;", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Master' ORDER BY Date ASC");
            return cpss;
        }

        public Task<List<CPS>> getSuspendeds(string startDate, string endDate)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Suspended' ORDER BY Date ASC;", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Suspended' ORDER BY Date ASC");
            return cpss;
        }

        public Task<List<CPS>> getDeleteds(string startDate, string endDate)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Deleted' ORDER BY Date ASC;", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Deleted' ORDER BY Date ASC");
            return cpss;
        }
        public Task<List<CPS>> getImplementations(string startDate, string endDate)
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Implementation' ORDER BY Date ASC;", null, commandType: CommandType.Text));
            Console.WriteLine($"SELECT Total FROM [CPS] WHERE Date >= '{startDate}' AND Date <= '{endDate}' AND Status='Implementation' ORDER BY Date ASC");
            return cpss;
        }
        public Task<int> Update(CPS cps)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("Id", cps.ID);
            dbPara.Add("Date", cps.Date);
            dbPara.Add("Status", cps.Status, DbType.String);
            dbPara.Add("Total", cps.Total, DbType.Int64);

            var updateCPS = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_CPS]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateCPS;
        }
    }
}
