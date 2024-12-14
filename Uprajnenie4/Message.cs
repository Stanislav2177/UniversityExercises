using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uprajnenie4
{

    public struct Message
    {
        public Contact sender { get; set; }
        public string message { get; set; }
        public DateTime timeSent { get; set; }
        
    }
}
