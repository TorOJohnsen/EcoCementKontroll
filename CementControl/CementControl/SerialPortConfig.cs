using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public string NewLine { get; set; }
        public string CheckCommand { get; set; }
        public string CheckRead { get; set; }
        public SerialPortState SerialPortState { get; set; }
        public ReadMode ReadMode { get; set; }

        public SerialPortConfigParameters()
        {

        }

        public SerialPortConfigParameters(SerialPortState deviceType, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, string newLine, ReadMode readMode)
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
        private readonly string _newLine;
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
            _newLine = connString[6];
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

        public string GetNewLine()
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
        private List<SerialPortConfigParameters> _ports;
        private List<string> _comms;
        //private ISerialConnection _iSerialConnection;


        public DiscoverSerialConnections(List<SerialPortConfigParameters> ports)
        {
            _ports = ports;
            _comms = SerialPort.GetPortNames().ToList();
            //_iSerialConnection = serialConnection;
        }


        public void Run()
        {
            foreach (var comport in _comms)
            {
                foreach (var device in _ports)
                {
                    if (device.SerialPortState == SerialPortState.Undiscovered)
                    {

                        // Connect
                        var serial = new SerialConnection();
                        string readData = serial.CheckConnection(comport, device.BaudRate, device.Parity, device.StopBits, device.DataBits, device.Handshake, device.NewLine, device.CheckCommand);

                        // Evaluate the reply
                        if (readData.Contains(device.CheckRead))
                        {
                            device.SerialPortState = device.DeviceType;
                        }
                        else
                        {
                            device.SerialPortState = SerialPortState.NotDefined;
                        }
                    }
                }
            }
        }


    }



}
