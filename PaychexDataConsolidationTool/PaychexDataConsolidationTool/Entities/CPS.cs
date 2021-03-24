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
        public int ID { get; set; }

        public string Date { get; set; }

        public string Status { get; set; }

        public int Total { get; set; }
    }
}
