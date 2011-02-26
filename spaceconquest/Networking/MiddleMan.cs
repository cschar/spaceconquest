using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    interface MiddleMan
    {
        void AddCommand(Command c);
        void EndTurn();
        bool DriverReady();
        void DriverReset();
    }
}
