using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaychexDataConsolidationTool.Entities
{
    public class GraphData
    {
        public string EntityName { get; set; }
        public int[,] EntityCounts { get; set; }
        public string[] Dates { get; set; }
        public string[] Entitys { get; set; }
        public GraphData(string theEntityName, string[] theDates, string[] theEntities, int i, int j)
        {
            this.EntityName = theEntityName;
            this.Entitys = theEntities;
            this.EntityCounts = new int[i, j];
            this.Dates = theDates;
        }
    }
}
