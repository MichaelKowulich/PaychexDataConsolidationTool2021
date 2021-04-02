using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class Type
    {
        [Key]
        public int TypeId { get; set; }

        public string TypeName { get; set; }

    }
}