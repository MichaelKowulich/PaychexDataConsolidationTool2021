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

        public Task<int> Create(CPT cpt)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("DateOfReport", cpt.DateOfReport, DbType.String);
            dbPara.Add("TypeId", cpt.TypeId, DbType.String);
            dbPara.Add("TypeCountAsOfDate", cpt.TypeCountAsOfDate, DbType.Int64);
            var cptId = Task.FromResult(_dapperManager.Insert<int>("[dbo].[SP_Add_CPT]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return cptId;
        }

        public Task<CPT> GetById(int id)
        {
            var cpt = Task.FromResult(_dapperManager.Get<CPT>($"select * from [ClientType] where ClientsPerTypeId = {id}", null,
                    commandType: CommandType.Text));
            return cpt;
        }

        public Task<int> Delete(int id)
        {
            var deleteCPT = Task.FromResult(_dapperManager.Execute($"Delete [ClientType] where ClientsPerTypeId = {id}", null,
                    commandType: CommandType.Text));
            return deleteCPT;
        }

        public Task<int> Count(string startDate, string endDate)
        {
            var totCPTS = Task.FromResult(_dapperManager.Get<int>($"select COUNT(*) " +
                $"from[dbo].[ClientsPerType], [dbo].[Type] " +
                $"WHERE[dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
                $"AND[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerType].DateOfReport <= '{endDate}';", null,
                    commandType: CommandType.Text));
            return totCPTS;
        }

        public Task<List<CPTType>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC", string search = "")
        {
            var cptt = Task.FromResult(_dapperManager.GetAll<CPTType>
                ($"Select FORMAT ([dbo].[ClientsPerType].DateOfReport, 'yyyy-MM-dd') as DateOfReport, [dbo].[Type].TypeName as TypeName, [dbo].[ClientsPerType].TypeCountAsOfDate as TypeCountAsOfDate " +
                $"from[dbo].[ClientsPerType], [dbo].[Type] " +
                $"WHERE[dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
                $"AND[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerType].DateOfReport <= '{endDate}' " +
                $"ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY;", null, commandType: CommandType.Text));
            return cptt;
        }
        public Task<List<CPT>> SearchDates(string orderBy, string startDate, string endDate, string direction = "DESC")
        {
            var cptt = Task.FromResult(_dapperManager.GetAll<CPT>
                ($"Select [dbo].[ClientsPerType].DateOfReport, [dbo].[Type].TypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"from[dbo].[ClientsPerType], [dbo].[Type] " +
                $"WHERE[dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
                $"AND[dbo].[ClientsPerType].DateOfReport >= '{startDate}' AND[dbo].[ClientsPerType].DateOfReport <= '{endDate}' " +
                $"ORDER BY[dbo].[ClientsPerType].DateOfReport, [dbo].[Type].TypeId ", null, commandType: CommandType.Text));
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
                $"from[dbo].[ClientsPerType], [dbo].[Type] " +
                $"WHERE[dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
                $"AND[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerType].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[Type].TypeName = '{typeName}' " +
                $"ORDER BY[dbo].[ClientsPerType].DateOfReport", null, commandType: CommandType.Text));
            Console.WriteLine($"Select [dbo].[ClientsPerType].DateOfReport, [dbo].[Type].TypeName, [dbo].[ClientsPerType].TypeCountAsOfDate " +
                $"from[dbo].[ClientsPerType], [dbo].[Type] " +
                $"WHERE[dbo].[Type].TypeId = [dbo].[ClientsPerType].TypeId " +
                $"AND[dbo].[ClientsPerType].DateOfReport >= '{startDate}' " +
                $"AND[dbo].[ClientsPerType].DateOfReport <= '{endDate}' " +
                $"AND [dbo].[Type].TypeName = '{typeName}' " +
                $"ORDER BY[dbo].[ClientsPerType].DateOfReport");
            return cptt;
        }
        public Task<int> Update(CPT cpt)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("ClientTypeId", cpt.ClientsPerTypeId);
            dbPara.Add("DateOfReport", cpt.DateOfReport);
            dbPara.Add("TypeId", cpt.TypeId, DbType.String);
            dbPara.Add("TypeCountAsOfDate", cpt.TypeCountAsOfDate, DbType.Int64);

            var updateCPT = Task.FromResult(_dapperManager.Update<int>("[dbo].[SP_Update_CPT]",
                            dbPara,
                            commandType: CommandType.StoredProcedure));
            return updateCPT;
        }
    }
}
