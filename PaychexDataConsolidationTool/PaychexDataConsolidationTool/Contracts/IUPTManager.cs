using PaychexDataConsolidationTool.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Contracts
{
    public interface IUPTManager
    {
        Task<List<UPT>> getDates(string startDate, string endDate);
        Task<int> Count(string startDate, string endDate);
        Task<List<UPTType>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC");
        Task<List<PaychexDataConsolidationTool.Entities.UserType>> getTypes();
        Task<List<UPTType>> getTypeReportData(string startDate, string endDate, string typeName);
        public Task<List<UPT>> getMostRecentDate();
        public Task<List<UPTType>> getMostRecentTypeCounts(string date);
    }
}
