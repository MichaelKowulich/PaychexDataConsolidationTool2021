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
        public int ID { get; set; }

        public string Type { get; set; }

        public int Number { get; set; }
    }
}
