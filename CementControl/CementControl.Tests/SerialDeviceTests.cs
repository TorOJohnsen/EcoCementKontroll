using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace CementControl.Tests
{
    [TestClass]
    public class SerialDeviceTests
    {
        private string _port = "COM12";

        [TestMethod]
        public void ReadActivePortsIntegrationTests()
        {
            var list = Discover.GetActiveSerialPorts();

        }


        [TestMethod]
        public void SerialPortTestCommand()
        {
            var serial = new SerialDevice(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\n");
            serial.PortDataRead += serial_dataReceived;
            serial.SendCommand("*IDN?");
            Thread.Sleep(2000);
            serial.ClosePort();

        }


        [TestMethod]
        public void SerialPortSetVoltage()
        {
            var serial = new SerialDevice(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\n");
            serial.PortDataRead += serial_dataReceived;
            serial.SendCommand("VSET1:12.00");
            Thread.Sleep(2000);
            serial.ClosePort();

        }
        [TestMethod]
        public void SerialPortReadVoltage()
        {
            var serial = new SerialDevice(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\n");
            serial.PortDataRead += serial_dataReceived;
            serial.SendCommand("VOUT1?");
            Thread.Sleep(2000);
            serial.ClosePort();


        }

        [TestMethod]
        public void SerialPortSetVoltageOff()
        {
            var serial = new SerialDevice(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\n");
            serial.PortDataRead += serial_dataReceived;
            serial.SendCommand("VSET1:0.00");
            Thread.Sleep(2000);
            serial.ClosePort();

        }


        private void serial_dataReceived(object sender, string data)
        {
            string local = data;
            Debug.Print($"Serial port read message: {local}");

        }

    }
}
