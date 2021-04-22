using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class CPB
    {
        [Key]
        public int ClientsPerBrandId { get; set; }

        public string DateOfReport { get; set; }

        public int ClientBrandId { get; set; }
       
        public int ClientsPerBrandCountTypeId { get; set; }

        public int CountAsOfDate { get; set; }
    }
}
