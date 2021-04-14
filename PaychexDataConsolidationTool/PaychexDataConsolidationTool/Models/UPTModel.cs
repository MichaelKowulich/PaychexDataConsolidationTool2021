﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Models
{
    public class UPTModel
    {
        public int UserPerTypeId { get; set; }

        public string DateOfReport { get; set; }

        public int UserTypeId { get; set; }

        public int TypeCountAsOfDate { get; set; }
    }
}
