using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class UPT
    {
        [Key]
        public int UserPerTypeId { get; set; }

        public string DateOfReport { get; set; }

        public string UserTypeId { get; set; }

        public int UserTypeCountAsOfDate { get; set; }
    }
}
