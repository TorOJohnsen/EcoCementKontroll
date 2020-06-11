using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace CementControl
{
    public class Discover
    {
        public static List<string> GetActiveSerialPorts()
        {
            var ports = SerialPort.GetPortNames();
            return ports.ToList();
        }
    }
}