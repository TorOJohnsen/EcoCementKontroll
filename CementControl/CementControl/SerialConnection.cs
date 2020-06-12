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
        void SendCommand(string cmd);
        void ClosePort();
    }

    
    
    public class SerialConnection : ISerialConnection
    {
        
        
        private readonly SerialPort _mySerialPort;
        public event EventHandler<string> PortDataRead;
        private readonly ILogger _logger = Log.ForContext<SerialConnection>();




        public SerialConnection(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, string newLine)
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
            
            Debug.Print("Data Received:");
            
            string readChunk = sp.ReadExisting();
            string indata = readChunk.TrimEnd();

            while (!string.IsNullOrWhiteSpace(readChunk) && stopwatch.ElapsedMilliseconds < 200)
            {
                readChunk = sp.ReadExisting();
                if (string.IsNullOrWhiteSpace(readChunk)) continue;
                indata += readChunk.TrimEnd();
                Debug.Print(".");
            } 

            Debug.Print(indata);

            PortDataRead?.Invoke(this,indata);
        }
    }
}




