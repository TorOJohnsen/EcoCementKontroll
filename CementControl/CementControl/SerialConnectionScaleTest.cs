using System;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.IO.Ports;
using System.Threading;
using Serilog;

namespace CementControl
{
    
    public class SerialConnectionScaleTest : ISerialConnection
    {
        private string readDataOne = "S";
        private string readDataTwo = "T,GS,   665.5,kg";

        private int timeout_ms = 67;

        public event EventHandler<string> PortDataRead;
        private readonly ILogger _logger = Log.ForContext<SerialConnectionScaleTest>();

        public SerialConnectionScaleTest()
        {

        }


        public void Open(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, string newLine)
        {

            _logger.Debug($"Opened Serial port {serialPortNumber}");
        }


        public void SetTimeout(int setTimeout_ms)
        {
            timeout_ms = setTimeout_ms;
        }

        
        public void SendCommand(string cmd)
        {
            _logger.Debug($"Send command: {cmd}");

            Thread.Sleep(timeout_ms);
            PortDataRead?.Invoke(this, readDataOne);
            Thread.Sleep(timeout_ms);
            PortDataRead?.Invoke(this, readDataTwo);

        }

        public void ClosePort()
        {
            _logger.Debug($"Close port");
        }
    }
}




