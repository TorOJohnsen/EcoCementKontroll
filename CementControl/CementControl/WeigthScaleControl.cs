using Serilog;
using System;
using System.Configuration;
using System.Globalization;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CementControl
{
    public class WeigthScaleControl
    {

        private readonly ISerialConnection _serialConnection;
        private readonly ILogger _logger = Log.ForContext<PowerSupplyControl>();

        public event EventHandler<double> OnDataRead;

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

            double analyzedData = AnalyzeReceivedScaleData(data);
            if (!double.IsNaN(analyzedData))
            {
                OnDataRead?.Invoke(this, analyzedData);
            }
        }

        public void ClosePort()
        {
            _serialConnection.ClosePort();
        }



        private double AnalyzeReceivedScaleData(string inData)
        {
            //T,GS,   665.5,kg
            string searchString = ", *([0-9]+.[0-9]),kg";
            double result = Double.NaN;

            Match match = Regex.Match(inData, searchString, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string matchValue = match.Groups[1].Value;
                matchValue = matchValue.ToString(CultureInfo.InvariantCulture).Trim();

                try
                {
                    result = Convert.ToDouble(matchValue);
                }
                catch (Exception e)
                {
                    _logger.Error($"Error converting weight reading to decimal number. Read from scale: {inData}. Exception: {e.Message}");
                    result = Double.NaN;
                }

            }

            return result;
        }

    }
}
