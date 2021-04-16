using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PaychexDataConsolidationTool.Entities
{
    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; }

        public string UserTypeName { get; set; }

    }
}