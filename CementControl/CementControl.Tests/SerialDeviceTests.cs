using CementControl;
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
        private string _port = "COM3";
        private static AutoResetEvent event_1 = new AutoResetEvent(false);


        [TestMethod]
        public void SerialPortTestCommand()
        {

            event_1.Reset();

            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.WriteLine($"SerialPortTestCommand");
            var serial = new SerialConnection();
            serial.Open(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, NewLine.SlashN, ReadMode.ReadLine);
            serial.PortDataRead += serial_dataReceived;
            serial.SendCommand("*IDN?");
            event_1.WaitOne(timeout: TimeSpan.FromSeconds(3));
            serial.ClosePort();

        }


        [TestMethod]
        public void SerialPortSetVoltage()
        {
            event_1.Reset();
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.WriteLine($"SerialPortSetVoltage");
            var serial = new SerialConnection();
            serial.Open(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, NewLine.SlashN, ReadMode.ReadChunksTillNoneMore);
            serial.PortDataRead += serial_dataReceived;
            serial.SendCommand("VSET1:12.00");
            serial.SendCommand("VOUT1?");
            serial.PortDataRead += serial_dataReceived;
            event_1.WaitOne(timeout: TimeSpan.FromSeconds(3));
            serial.ClosePort();

        }
        [TestMethod]
        public void SerialPortReadVoltage()
        {
            event_1.Reset();
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.WriteLine($"SerialPortReadVoltage");
            var serial = new SerialConnection();
            serial.Open(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, NewLine.SlashN, ReadMode.ReadChunksTillNoneMore);
            serial.SendCommand("VOUT1?");
            event_1.WaitOne(timeout: TimeSpan.FromSeconds(3));
            serial.ClosePort();


        }

        [TestMethod]
        public void SerialPortSetVoltageOff()
        {
            event_1.Reset();
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.WriteLine($"SerialPortSetVoltageOff");
            var serial = new SerialConnection();
            serial.Open(_port, 9600, Parity.None, StopBits.One, 8, Handshake.None, NewLine.SlashN, ReadMode.ReadChunksTillNoneMore);
            serial.PortDataRead += serial_dataReceived;
            serial.SendCommand("VSET1:0.00");
            Thread.Sleep(200);
            serial.SendCommand("VOUT1?");
            event_1.WaitOne(timeout: TimeSpan.FromSeconds(3));
            serial.ClosePort();

        }


        private void serial_dataReceived(object sender, string data)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            string local = data;
            Trace.WriteLine($"Serial port read message: {local}");
            event_1.Set();
        }
       
    }
}
