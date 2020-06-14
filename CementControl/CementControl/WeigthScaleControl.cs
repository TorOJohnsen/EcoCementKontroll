using Serilog;
using System;
using System.Configuration;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace CementControl
{
    public class WeigthScaleControl
    {

        private readonly ISerialConnection _serialConnection;
        private readonly double _voltageOn;
        private readonly double _voltageOff;

        private readonly ILogger _logger = Log.ForContext<PowerSupplyControl>();

        public event EventHandler<string> OnDataRead;


        public WeigthScaleControl(ISerialConnection serialConnection)
        {
            _serialConnection = serialConnection;
        }

        public void OpenConnection(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, string newLine)
        {
            _serialConnection.Open(serialPortNumber, baudRate, parity, stopBits, dataBits, handshake, newLine);
            _serialConnection.PortDataRead += DataReceived;
        }

        public void ReadWeight()
        {
            string cmd = "READ";
            _serialConnection.SendCommand(cmd);
        }

        private void DataReceived(object sender, string data)
        {
            Log.Information($"Weight scale raw data {data}");

            string analyzedData = AnalyzeReceivedScaleData(data);
            if (analyzedData != String.Empty)
            {
                OnDataRead?.Invoke(this, analyzedData);
            }
        }

        public void ClosePort()
        {
            _serialConnection.ClosePort();
        }


        private string AnalyzeReceivedScaleData(string inData)
        {
            //T,GS,   665.5,kg
            string searchString = ", *([0-9]+.[0-9]),kg";
            string result;


            Match match = Regex.Match(inData, searchString, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                result = match.Groups[1].Value;
            }
            else
            {
                result = String.Empty;
            }

            return result;
        }

    }
}
