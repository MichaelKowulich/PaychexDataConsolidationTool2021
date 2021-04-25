using PaychexDataConsolidationTool.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Contracts
{
    public interface ICPBManager
    {
        Task<List<CPB>> getDates(string startDate, string endDate);
        Task<int> Count(string startDate, string endDate);
        Task<List<CPBCountTypeBrand>> ListAll(int skip, int take, string orderBy, string startDate, string endDate, string countTypeName, string direction = "DESC", string search = "");
        Task<List<PaychexDataConsolidationTool.Entities.ClientBrand>> getBrands();
        Task<List<PaychexDataConsolidationTool.Entities.ClientsPerBrandCountType>> getCountTypes();
        Task<List<CPBCountTypeBrand>> getBrandReportData(string startDate, string endDate, string brandName, string countTypeName);
        public Task<List<CPB>> getMostRecentDate();
        public Task<List<CPBCountTypeBrand>> getMostRecentBrandCountsOfType(string date);
    }
}
