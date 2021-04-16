using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class CPTType
    {
        [Key]
        public int ClientPerTypeId { get; set; }

        public string DateOfReport { get; set; }

        public int TypeId { get; set; }

        public int TypeCountAsOfDate { get; set; }

        public string TypeName { get; set; }
    }
}