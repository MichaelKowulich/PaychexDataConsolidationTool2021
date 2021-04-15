using PaychexDataConsolidationTool.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Contracts
{
    public interface ICPTManager
    {
        Task<List<CPT>> getDates(string startDate, string endDate);
        Task<int> Count(string startDate, string endDate);
        Task<List<CPTType>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC", string search = "");
        Task<List<PaychexDataConsolidationTool.Entities.Type>> getTypes();
        Task<List<CPTType>> getTypeReportData(string startDate, string endDate, string typeName);
        public Task<int> CountAfterSearch(string startDate, string endDate);
        public Task<List<CPT>> getMostRecentDate();
        public Task<List<CPTType>> getMostRecentStatusCounts(string date);
    }
}
