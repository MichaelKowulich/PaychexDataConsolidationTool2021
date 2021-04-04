using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class CPT
    {
        [Key]
        public int ClientsPerTypeId { get; set; }

        public string DateOfReport { get; set; }

        public int TypeId { get; set; }

        public int TypeCountAsOfDate { get; set; }
    }
}
