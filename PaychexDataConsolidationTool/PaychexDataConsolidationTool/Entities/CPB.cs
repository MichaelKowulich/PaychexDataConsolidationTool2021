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
        public int ID { get; set; }

        public string Brand { get; set; }

        public int ActiveClientCount { get; set; }
       
        public int ActiveEECount { get; set; }

        public int Count3 { get; set; }

        public int Count4 { get; set; }
    }
}
