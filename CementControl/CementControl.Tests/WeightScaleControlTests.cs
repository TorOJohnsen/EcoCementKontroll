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
    public class WeightScaleControlTests
    {
        private string _port = "COM10";

        [TestMethod()]
        public void TurnOnTest()
        {
            var serial = new SerialConnectionScaleTest();
            var ps = new WeigthScaleControl(serial);
            ps.OpenConnection(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\r");
            ps.OnDataRead += DataPortRead;

            ps.ReadWeight();
            Thread.Sleep(100);
            ps.ClosePort();
            Thread.Sleep(200);

        }

        private void DataPortRead(object sender, double data)
        {
            Debug.Write(data);
        }

    }
}
