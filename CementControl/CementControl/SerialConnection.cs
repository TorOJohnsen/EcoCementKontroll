using System;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.IO.Ports;
using Serilog;

namespace CementControl
{
    public interface ISerialConnection
    {
        event EventHandler<string> PortDataRead;

        void Open(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake,string newLine);
        void SendCommand(string cmd);
        void ClosePort();
    }

    
    
    public class SerialConnection : ISerialConnection
    {


        private int timeout_ms = 200;
        private SerialPort _mySerialPort;
        public event EventHandler<string> PortDataRead;
        private readonly ILogger _logger = Log.ForContext<SerialConnection>();

        public SerialConnection()
        {

        }


        public void Open(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, string newLine)
        {

            _mySerialPort = new SerialPort(serialPortNumber);

            _mySerialPort.BaudRate = baudRate;
            _mySerialPort.Parity = parity;
            _mySerialPort.StopBits = stopBits;
            _mySerialPort.DataBits = dataBits;
            _mySerialPort.Handshake = handshake;
            _mySerialPort.NewLine = newLine;

            _mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            _mySerialPort.Open();
            _logger.Debug($"Opened Serial port {serialPortNumber}");
        }


        public void SetTimeout(int setTimeout_ms)
        {
            timeout_ms = setTimeout_ms;
        }

        
        public void SendCommand(string cmd)
        {
            _mySerialPort.Write(cmd);
        }

        public void ClosePort()
        {
            _mySerialPort.Close();
        }


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            _logger.Debug("Data Received:");
            
            string readChunk = sp.ReadExisting();
            string inndata = readChunk.TrimEnd();

            while (!string.IsNullOrWhiteSpace(readChunk) && stopwatch.ElapsedMilliseconds < timeout_ms)
            {
                readChunk = sp.ReadExisting();
                if (string.IsNullOrWhiteSpace(readChunk)) continue;
                inndata += readChunk.TrimEnd();
                Debug.Print(".");
            }

            _logger.Debug($">>>{inndata}<<<");

            PortDataRead?.Invoke(this,inndata);
        }
    }
}




