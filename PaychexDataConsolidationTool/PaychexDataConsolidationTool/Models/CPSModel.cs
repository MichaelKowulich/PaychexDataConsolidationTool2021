using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Models
{
    public class CPSModel
    {
        public int ID { get; set; }

        public string Date { get; set; }

        public string Status { get; set; }

        public int Total { get; set; }
    }
}
