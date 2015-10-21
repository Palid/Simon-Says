using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimonSays
{
    interface IKeyView

    {
        string key { get; set; }
        void showKey();
    }
}
