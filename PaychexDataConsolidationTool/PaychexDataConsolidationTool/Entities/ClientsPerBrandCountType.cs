using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class ClientsPerBrandCountType
    {
        [Key]
        public int ClientsPerBrandCountTypeId { get; set; }

        public string ClientsPerBrandCountTypeName { get; set; }

    }
}