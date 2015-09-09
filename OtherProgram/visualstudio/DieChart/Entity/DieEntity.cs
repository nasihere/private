using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DieChart.Entity
{
    public class DieEntity
    {
            public int id { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public string Id1 { get; set; }
            public string Id2 { get; set; }
            public string Length { get; set; }
            public Nullable<int> Price_val { get; set; }
            public string Glass_Size { get; set; }
            public string ID1_ID2 { get; set; }
        }
    
}