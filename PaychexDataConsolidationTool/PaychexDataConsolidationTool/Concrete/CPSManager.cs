using Dapper;
using PaychexDataConsolidationTool.Contracts;
using PaychexDataConsolidationTool.Entities;
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

        public Task<List<CPS>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            var cpss = Task.FromResult(_dapperManager.GetAll<CPS>
                ($"SELECT * FROM [CPS] WHERE Status like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text));
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
