using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class ClientSPerBrandCountType
    {
        [Key]
        public int ClientsPerBrandCountTypeId { get; set; }

        public string ClientPerBrandCountTypeName { get; set; }

    }
}