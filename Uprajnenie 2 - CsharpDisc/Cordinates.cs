using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uprajnenie_2___CsharpDisc
{
    public class Coordinate
    {
        public float lat { get; set; }
        public float lng { get; set; }

        public Coordinate(float latitude, float longitude)
        {
            lat = latitude;
            lng = longitude;
        }
    }
}
