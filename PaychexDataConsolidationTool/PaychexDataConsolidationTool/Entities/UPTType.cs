using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class UPTType
    {
        [Key]
        public int UserPerTypeId { get; set; }

        public string DateOfReport { get; set; }

        public string UserTypeId { get; set; }

        public int UserTypeCountAsOfDate { get; set; }

        public string UserTypeName { get; set; }
    }
}