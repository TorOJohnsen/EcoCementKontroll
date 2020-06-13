using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace CementControl
{
    public class PowerSupplyControl
    {

        private readonly ISerialConnection _serialConnection;
        private readonly double _voltageOn;
        private readonly double _voltageOff;

        private readonly ILogger _logger = Log.ForContext<PowerSupplyControl>();

        public event EventHandler<string> OnDataRead;




        public PowerSupplyControl(ISerialConnection serialConnection)
        {
            _serialConnection = serialConnection;

            _voltageOff = Convert.ToDouble(ConfigurationManager.AppSettings["app:voltageTurnOff"]);
                
            _voltageOn  = Convert.ToDouble(ConfigurationManager.AppSettings["app:voltageTurnOn"]);
        }

        public void OpenConnection(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, string newLine)
        {
            _serialConnection.Open(serialPortNumber, baudRate, parity, stopBits, dataBits, handshake, newLine);
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
