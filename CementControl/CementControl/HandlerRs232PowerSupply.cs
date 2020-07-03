using Serilog;
using System;
using System.Configuration;
using System.Globalization;
using System.IO.Ports;

namespace CementControl
{
    public class HandlerRs232PowerSupply
    {

        private readonly ISerialConnection _serialConnection;
        private readonly double _voltageOn;
        private readonly double _voltageOff;

        private readonly ILogger _logger = Log.ForContext<HandlerRs232PowerSupply>();

        public event EventHandler<string> OnDataRead;


        public HandlerRs232PowerSupply(ISerialConnection serialConnection)
        {
            _serialConnection = serialConnection;

            _voltageOff = Convert.ToDouble(ConfigurationManager.AppSettings["app:voltageTurnOff"], CultureInfo.InvariantCulture);

            _voltageOn = Convert.ToDouble(ConfigurationManager.AppSettings["app:voltageTurnOn"], CultureInfo.InvariantCulture);
        }

        public void OpenConnection(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, NewLine newLine, ReadMode readMode)
        {
            _serialConnection.Open(serialPortNumber, baudRate, parity, stopBits, dataBits, handshake, newLine, readMode);
            _serialConnection.PortDataRead += DataReceived;
        }

        public void SetVoltage(double voltage)
        {
            string cmd = $"VSET1:{voltage:F2}";
            _serialConnection.SendCommand(cmd);
        }

        public void GetVoltage()
        {
            string cmd = "VOUT1?";
            _serialConnection.SendCommand(cmd);
        }


        public void TurnOn()
        {
            SetVoltage(_voltageOn);
        }

        public void TurnOff()
        {
            SetVoltage(_voltageOff);
        }

        private void DataReceived(object sender, string data)
        {
            Log.Information($"Power supply state read to {data} volts");
            OnDataRead?.Invoke(this, data);
        }

        public void ClosePort()
        {
            _serialConnection.ClosePort();
        }

    }
}
