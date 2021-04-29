using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class ClientType
    {
        [Key]
        public int ClientTypeId { get; set; }

        public string ClientTypeName { get; set; }

    }
}