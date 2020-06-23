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
        private string _port = "COM3";

        [TestMethod()]
        public void TurnOnTest()
        {
            //var serial = new SerialConnectionScaleTest();
            var serial = new SerialConnection();
            var ps = new HandlerRs232WeigthScale(serial);
            ps.OpenConnection(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, NewLine.SlashR, ReadMode.ReadTillSlashRSlashN);
            ps.OnDataRead += DataPortRead;

            ps.ReadWeight();
            Thread.Sleep(4000);
            ps.ReadWeight();
            Thread.Sleep(4000);
            ps.ReadWeight();
            Thread.Sleep(4000);
            ps.ReadWeight();
            Thread.Sleep(4000);
            ps.ClosePort();

        }

        private void DataPortRead(object sender, double data)
        {
            Debug.Write(data);
        }

    }
}
