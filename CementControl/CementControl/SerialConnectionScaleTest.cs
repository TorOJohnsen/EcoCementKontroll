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
        private string readDataTwo = "ST,GS,   682.0,kg";
        private double _measuredWeightTest = 665.5;

        public event EventHandler<string> PortDataRead;
        private readonly ILogger _logger = Log.ForContext<SerialConnectionScaleTest>();
        private int timeout_ms;


        public SerialConnectionScaleTest()
        {

        }


        public void Open(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, NewLine newLine, ReadMode readMode)
        {
            _logger.Debug($"Opened Serial port {serialPortNumber}");
        }

        public string CheckConnection(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits,
            Handshake handshake, string newLine, string command)
        {
            throw new NotImplementedException();
        }


        public void SetTimeout(int setTimeout_ms)
        {
            timeout_ms = setTimeout_ms;
        }

        



        public void SendCommand(string cmd)
        {
            _logger.Debug($"Send command: {cmd}");
            readDataTwo = $"T,GS,   {_measuredWeightTest:F1},kg";
            _measuredWeightTest -= 6.1;

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




