using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CementControl
{
    public class SerialPortConfig
    {

        //(_powerSupplyComPort, 9600, Parity.None, StopBits.One, 8, Handshake.None, "\n");
        // <add key = "app:pwrSypplySerialConfig" value="9600,None,One,8,None,\n" />
        private readonly int _baudRate;
        private readonly Parity _parity;
        private readonly StopBits _stopBits;
        private readonly int _dataBits;
        private readonly Handshake _handshake;
        private readonly string _newLine;


        private readonly string _connectionSting;

        public SerialPortConfig(string connectionString)
        {
            _connectionSting = connectionString;


            List<string> connString = SplitList();
            _baudRate = Convert.ToInt32(connString[0]);
            Enum.TryParse(connString[1], out _parity);
            Enum.TryParse(connString[2], out _stopBits);
            _dataBits = Convert.ToInt32(connString[3]);
            Enum.TryParse(connString[4], out _handshake);
            _newLine = connString[5];



        }


        private List<string> SplitList()
        {
            return _connectionSting.Split(',').ToList();
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

    }


}
