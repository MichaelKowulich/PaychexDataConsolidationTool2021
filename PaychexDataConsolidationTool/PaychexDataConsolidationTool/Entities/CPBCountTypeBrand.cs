using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class CPBCountTypeBrand
    {
        [Key]
        public int ClientsPerBrandId { get; set; }

        public string DateOfReport { get; set; }

        public int ClientBrandId { get; set; }

        public string ClientBrandName { get; set; }
        
        public int ClientsPerBrandCountTypeId { get; set; }

        public string ClientsPerBrandCountTypeName { get; set; }

        public int CountAsOfDate { get; set; }

    }
}