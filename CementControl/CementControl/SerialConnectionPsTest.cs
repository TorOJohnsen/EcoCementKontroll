using System;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.IO.Ports;
using System.Threading;
using Serilog;

namespace CementControl
{
    
    public class SerialConnectionPsTest : ISerialConnection
    {
        private string readDataTwo = "12.0";

        public event EventHandler<string> PortDataRead;
        private readonly ILogger _logger = Log.ForContext<SerialConnectionPsTest>();
        private int timeout_ms;


        public SerialConnectionPsTest()
        {

        }


        public void Open(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, NewLine newLine, ReadMode readMode)
        {
            _logger.Debug($"Opened Serial port {serialPortNumber}");
        }


        public void SetTimeout(int setTimeout_ms)
        {
            timeout_ms = setTimeout_ms;
        }

        



        public void SendCommand(string cmd)
        {
            _logger.Debug($"Send PS command: {cmd}");
            PortDataRead?.Invoke(this, readDataTwo);
        }

        public void SendCommandLine(string cmd)
        {
            SendCommand(cmd);
        }


        public void ClosePort()
        {
            _logger.Debug($"Close port");
        }
    }
}




