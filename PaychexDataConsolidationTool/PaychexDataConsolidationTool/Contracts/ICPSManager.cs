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
        Task<List<CPS>> ListAll(int skip, int take, string orderBy, string direction, string search);
        Task<List<CPS>> SearchDates(int skip, int take, string orderBy, string direction, string startDate, string endDate);    
        public Task<int> CountAfterSearch(string startDate, string endDate);
    }
}
