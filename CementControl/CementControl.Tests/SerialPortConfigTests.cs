using Microsoft.VisualStudio.TestTools.UnitTesting;
using CementControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CementControl.Tests
{
    [TestClass()]
    public class SerialPortConfigTests
    {


        private readonly string _connString1 = "9600,None,One,8,None,\n,*IDN?,RND 320-KD3005P";
        private readonly string _connString2 = "9600,None,One,8,None,\r,READ,GS";



        [TestMethod()]
        public void DiscoveryTest()
        {
            List<SerialPortConfigParameters> serialPortConfigParameterses = new List<SerialPortConfigParameters>();
            var cfg1 = new SerialPortConfig(_connString1);
            var cfg2 = new SerialPortConfig(_connString2);

            serialPortConfigParameterses.Add(cfg1.GetConnectionObject());
            serialPortConfigParameterses.Add(cfg2.GetConnectionObject());



        }



        [TestMethod()]
        public void GetBaudRateTest()
        {
            var cfg = new SerialPortConfig(_connString1);
            cfg.GetBaudRate().Should().Be(9600);
        }

        [TestMethod()]
        public void GetParityTest()
        {
            var cfg = new SerialPortConfig(_connString2);
            cfg.GetParity().Should().Be(Parity.None);
        }

        [TestMethod()]
        public void GetStopBitsTest()
        {
            var cfg = new SerialPortConfig(_connString2);
            cfg.GetStopBits().Should().Be(StopBits.One);
        }

        [TestMethod()]
        public void GetDataBitsTest()
        {
            var cfg = new SerialPortConfig(_connString2);
            cfg.GetDataBits().Should().Be(8);
        }

        [TestMethod()]
        public void GetHandshakeTest()
        {
            var cfg = new SerialPortConfig(_connString2);
            cfg.GetHandshake().Should().Be(Handshake.None);
        }

        [TestMethod()]
        public void GetNewLineTest()
        {
            var cfg = new SerialPortConfig(_connString1);
            cfg.GetNewLine().Should().Be("\n");
        }

        [TestMethod()]
        public void GetDiscoveryReadMatchTest()
        {
            var cfg = new SerialPortConfig(_connString1);
            cfg.GetDiscoveryReadMatch().Should().Be("RND 320-KD3005P");

        }

        [TestMethod()]
        public void GetDiscoverySendCommandTest()
        {
            var cfg = new SerialPortConfig(_connString1);
            cfg.GetDiscoverySendCommand().Should().Be("*IDN?");

        }
    }
}