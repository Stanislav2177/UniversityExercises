using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uprajnenie_2___CsharpDisc
{
    public class Tag
    {
        public string Id { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public Tag Connection { get; set; } // Points to another Tag if there's a connection

        public Tag()
        {
            Attributes = new Dictionary<string, string>();
        }
    }
}
