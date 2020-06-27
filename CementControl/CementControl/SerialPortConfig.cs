using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;

namespace CementControl
{

    public class SerialPortConfigParameters
    {
        public SerialPortState DeviceType { get; set; }
        public string ComPort { get; set; }
        public int BaudRate { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public int DataBits { get; set; }
        public Handshake Handshake { get; set; }
        public NewLine NewLine { get; set; }
        public string CheckCommand { get; set; }
        public string CheckRead { get; set; }
        public SerialPortState SerialPortState { get; set; }
        public ReadMode ReadMode { get; set; }

        public SerialPortConfigParameters()
        {

        }

        public SerialPortConfigParameters(SerialPortState deviceType, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, NewLine newLine, ReadMode readMode)
        {
            DeviceType = deviceType;
            BaudRate = baudRate;
            Parity = parity;
            StopBits = stopBits;
            DataBits = dataBits;
            Handshake = handshake;
            NewLine = newLine;
            ReadMode = readMode;
            SerialPortState = SerialPortState.Undiscovered;
        }

        public void SetCommPort(string commPort)
        {
            ComPort = commPort;
        }

        public void SetCheckPortSettings(string checkCommand, string checkRead)
        {
            CheckCommand = checkCommand;
            CheckRead = checkRead;
        }

    }

    public enum SerialPortState
    {
        Undiscovered,
        NotDefined,
        PowerSupply,
        WeightScale
    }

    public enum NewLine
    {
        SlashN,
        SlashR
    }

    public static class NewLineHelper
    {
        public static string ToString(NewLine newLine)
        {
            string stringVers = "\n";
            if (newLine == NewLine.SlashN)
            {
                stringVers = "\n";
            } 
            else if (newLine == NewLine.SlashR)
            {
                stringVers = "\r";
            }
            return stringVers;
        }
    }



    public class SerialPortConfig
    {

        //(_powerSupplyComPort, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\n");
        // <add key = "app:pwrSypplySerialConfig" value="9600,None,One,8,None,\n" />
        private readonly SerialPortState _deviceType;
        private readonly int _baudRate;
        private readonly Parity _parity;
        private readonly StopBits _stopBits;
        private readonly int _dataBits;
        private readonly Handshake _handshake;
        private readonly NewLine _newLine;
        private readonly ReadMode _readMode;
        private readonly string _checkCommand;
        private readonly string _checkRead;


        private readonly string _connectionSting;

        public SerialPortConfig(string connectionString)
        {
            _connectionSting = connectionString;

            List<string> connString = SplitList();


            Enum.TryParse(connString[0], out _deviceType);
            _baudRate = Convert.ToInt32(connString[1]);
            Enum.TryParse(connString[2], out _parity);
            Enum.TryParse(connString[3], out _stopBits);
            _dataBits = Convert.ToInt32(connString[4]);
            Enum.TryParse(connString[5], out _handshake);
            Enum.TryParse(connString[6], out _newLine);
            Enum.TryParse(connString[7], out _readMode);
            _checkCommand = connString[8];
            _checkRead = connString[9];
        }

        public SerialPortConfigParameters GetConnectionObject()
        {
            SerialPortConfigParameters spcp = new SerialPortConfigParameters();
            spcp.DeviceType = _deviceType;
            spcp.BaudRate = _baudRate;
            spcp.Parity = _parity;
            spcp.StopBits = _stopBits;
            spcp.DataBits = _dataBits;
            spcp.Handshake = _handshake;
            spcp.NewLine = _newLine;
            spcp.CheckCommand = _checkCommand;
            spcp.CheckRead = _checkRead;
            spcp.ReadMode = _readMode;

            return spcp;
        }



        private List<string> SplitList()
        {
            return _connectionSting.Split(',').ToList();
        }


        public SerialPortState GetDeviceType()
        {
            return _deviceType;
        }


        public int GetBaudRate()
        {
            return _baudRate;
        }

        public Parity GetParity()
        {
            return _parity;
        }

        public StopBits GetStopBits()
        {
            return _stopBits;
        }

        public int GetDataBits()
        {
            return _dataBits;
        }

        public Handshake GetHandshake()
        {
            return _handshake;
        }

        public NewLine GetNewLine()
        {
            return _newLine;
        }

        public ReadMode GetReadMode()
        {
            return _readMode;
        }


        public string GetDiscoveryReadMatch()
        {
            return _checkRead;
        }
        public string GetDiscoverySendCommand()
        {
            return _checkCommand;
        }

    }








    public class DiscoverSerialConnections
    {
        SerialPortConfigParameters _port;
        private readonly string _windowMessage;
        private readonly string _windowTitle;
        private readonly ILogger _logger = Log.ForContext<DiscoverSerialConnections>();


        public DiscoverSerialConnections(SerialPortConfigParameters port, string windowTitle, string windowMessage)
        {
            _port = port;
            _windowMessage = windowMessage;
            _windowTitle = windowTitle;
        }


        public SerialPortConfigParameters Run()
        {

            _logger.Debug("Finding serial port being connected.");

            // Get port list
            List<string> comms = SerialPort.GetPortNames().ToList();
            List<string> added;

            MessageBox.Show(_windowMessage, _windowTitle);

            _logger.Debug($"Devices connected start: {comms.Count}");

            int devices = comms.Count;

            _logger.Debug("Plug device in");


            while (SerialPort.GetPortNames().ToList().Count == devices)
            {
                _logger.Debug("Venter ..");
                Thread.Sleep(50);
            }

            added = SerialPort.GetPortNames().ToList();
            var newDevs = added.Except(comms).ToList();
            _logger.Debug($"Devices connected end: {newDevs.Count}");

            if (newDevs.Count == 1)
            {
                _port.ComPort = newDevs[0];
                _port.SerialPortState = _port.DeviceType;
                _logger.Debug($"Found: {_port.ComPort}/{_port.DeviceType}");
                Thread.Sleep(2000); // Wait for the port to be configured in the system
            }

            return _port;
        }

    }



}
