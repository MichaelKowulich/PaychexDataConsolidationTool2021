using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Models
{
    public class CPSStatusModel
    {
        public int ClientStatusId { get; set; }

        public string DateOfReport { get; set; }

        public int StatusId { get; set; }

        public int StatusCountAsOfDate { get; set; }

        public int StatusName { get; set; }
    }
}