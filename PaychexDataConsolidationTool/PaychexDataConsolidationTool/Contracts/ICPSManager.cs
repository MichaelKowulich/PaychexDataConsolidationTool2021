using PaychexDataConsolidationTool.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Contracts
{
    public interface ICPSManager
    {
        Task<int> Create(CPS cps);
        Task<int> Delete(int Id);
        Task<int> Count(string search);
        Task<int> Update(CPS cps);
        Task<CPS> GetById(int Id);
        Task<List<CPS>> ListAll(string orderBy, string direction, string search);
        Task<List<CPS>> SearchDates(string orderBy, string direction, string startDate, string endDate);
        Task<List<CPS>> getDates(string startDate, string endDate);

        Task<List<CPS>> getInactives(string startDate, string endDate);
        Task<List<CPS>> getActives(string startDate, string endDate);
        Task<List<CPS>> getDemos(string startDate, string endDate);
        Task<List<CPS>> getMasters(string startDate, string endDate);
        Task<List<CPS>> getSuspendeds(string startDate, string endDate);
        Task<List<CPS>> getDeleteds(string startDate, string endDate);
        Task<List<CPS>> getImplementations(string startDate, string endDate);

        public Task<int> CountAfterSearch(string startDate, string endDate);
    }
}
