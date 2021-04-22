using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class ClientBrand
    {
        [Key]
        public int ClientBrandId { get; set; }

        public string ClientBrandName { get; set; }

    }
}