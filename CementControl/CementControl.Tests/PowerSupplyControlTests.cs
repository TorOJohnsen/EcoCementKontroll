using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CementControl.Tests
{
    [TestClass]
    public class PowerSupplyControlTests
    {
        private string _port = "COM10";

        [TestMethod()]
        public void TurnOnTest()
        {
            var serial = new SerialConnection();
            var ps = new PowerSupplyControl(serial);
            ps.OpenConnection(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\n");
            ps.OnDataRead += DataPortRead;

            ps.TurnOn();
            Thread.Sleep(10000);
            ps.GetVoltage();
            ps.TurnOff();
            Thread.Sleep(10000);
            ps.GetVoltage();
            Thread.Sleep(10000);
            ps.ClosePort();
        }

        private void DataPortRead(object sender, string data)
        {
            Debug.Write(data);
        }

    }
}
