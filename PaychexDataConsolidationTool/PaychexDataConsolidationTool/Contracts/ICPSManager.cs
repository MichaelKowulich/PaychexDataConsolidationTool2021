using PaychexDataConsolidationTool.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Contracts
{
    public interface ICPSManager
    {
        Task<List<CPS>> getDates(string startDate, string endDate);
        Task<int> Count(string startDate, string endDate);
        Task<List<CPSStatus>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC");
        Task<List<Status>> getStatuses();
        Task<List<CPSStatus>> getStatusReportData(string startDate, string endDate, string statusName);
        public Task<List<CPS>> getMostRecentDate();
        public Task<List<CPSStatus>> getMostRecentStatusCounts(string date);
    }
}
