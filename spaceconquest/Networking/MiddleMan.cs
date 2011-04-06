using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace spaceconquest
{
    interface MiddleMan
    {
        void AddCommand(Command c);
        void EndTurn();
        bool DriverReady();
        void DriverReset();
        void Close();
        void AttendClose();
        //void cb(Socket s);
    }
}
