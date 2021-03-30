using PaychexDataConsolidationTool.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Contracts
{
    public interface ICPSManager
    {
        Task<int> Create(CPS cps);
        Task<int> Delete(int Id);
        Task<int> Update(CPS cps);
        Task<CPS> GetById(int Id);
        Task<List<CPS>> SearchDates(string orderBy, string direction, string startDate, string endDate);
        Task<List<CPS>> getDates(string startDate, string endDate);
        Task<int> Count(string startDate, string endDate);
        Task<List<CPSStatus>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string direction = "DESC", string search = "");
        Task<List<Status>> getStatuses();
        Task<List<CPSStatus>> getStatusReportData(string startDate, string endDate, string statusName);
        public Task<int> CountAfterSearch(string startDate, string endDate);
    }
}
