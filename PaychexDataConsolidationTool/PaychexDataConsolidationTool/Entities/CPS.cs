using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class CPS
    {
        [Key]
        public int ClientStatusId { get; set; }

        public string DateOfReport { get; set; }

        public int StatusId { get; set; }

        public int StatusCountAsOfDate { get; set; }
    }
}
