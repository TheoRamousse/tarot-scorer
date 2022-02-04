using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Pages
{
    public partial class ChartView
    {
        public uint[] MyData { get; set; } = new uint[4]
        {
            13000,
            14000,
            11000,
            5000
        };

        public String Title { get; set; } = "Mon super chart";
        public String Subitle { get; set; } = "Mon super chart subtitle";
        public String XAxisName { get; set; } = "Mon super chart subtitle";
        public String YAxisName { get; set; } = "Mon super chart subtitle";
        public int StartValue { get; set; } = 14;
    }
}
