using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Models
{
    public class CPTTypeModel
    {
        public int ClientPerTypeId { get; set; }

        public string DateOfReport { get; set; }

        public int TypeId { get; set; }

        public int TypeCountAsOfDate { get; set; }

        public int TypeName { get; set; }
    }
}