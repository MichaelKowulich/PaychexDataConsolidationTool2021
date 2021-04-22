using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class CPBBrand
    {
        [Key]
        public int ClientPerBrandId { get; set; }

        public string DateOfReport { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }
        
        public int ClientsPerBrandCountTypeId { get; set; }

        public string ClientsPerBrandCountTypeName { get; set; }

        public int CountAsOfDate { get; set; }

    }
}