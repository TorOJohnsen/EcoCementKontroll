using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.IO.Ports;
using Serilog;

namespace CementControl
{
    public interface ISerialConnection
    {
        event EventHandler<string> PortDataRead;

        void Open(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, NewLine newLine, ReadMode readMode);
        void SendCommand(string cmd);
        void SendCommandLine(string cmd);
        void ClosePort();
    }


    public enum ReadMode
    {
        ReadChunksTillNoneMore,
        ReadTillSlashRSlashN,
        ReadLine
    }

    
    
    public class SerialConnection : ISerialConnection
    {


        private int _timeoutMs = 1500;
        private SerialPort _mySerialPort;
        public event EventHandler<string> PortDataRead;
        private readonly ILogger _logger = Log.ForContext<SerialConnection>();
        private ReadMode _readMode = ReadMode.ReadChunksTillNoneMore;


        public SerialConnection()
        {

        }


        public void Open(string serialPortNumber, int baudRate, Parity parity, StopBits stopBits, int dataBits, Handshake handshake, NewLine newLine, ReadMode readMode)
        {
            _logger.Information($"Opening serial port with these params:");
            _logger.Information($" -- serialPortNumber {serialPortNumber}");
            _logger.Information($" -- baudRate {baudRate}");
            _logger.Information($" -- parity {parity}");
            _logger.Information($" -- stopBits {stopBits}");
            _logger.Information($" -- dataBits {dataBits}");
            _logger.Information($" -- handshake {handshake}");
            _logger.Information($" -- newLine {newLine}");
            _logger.Information($" -- readMode {readMode}");



            try
            {
                _mySerialPort = new SerialPort(serialPortNumber);

                _mySerialPort.BaudRate = baudRate;
                _mySerialPort.Parity = parity;
                _mySerialPort.StopBits = stopBits;
                _mySerialPort.DataBits = dataBits;
                _mySerialPort.Handshake = handshake;
                _mySerialPort.NewLine = NewLineHelper.ToString(newLine);
                _readMode = readMode;




                switch (_readMode)
                {
                    case ReadMode.ReadChunksTillNoneMore:
                        _mySerialPort.DataReceived += DataReceivedHandlerReadLine;
                        break;
                    case ReadMode.ReadTillSlashRSlashN:
                        _mySerialPort.DataReceived += DataReceivedHandlerSlashRSlashN;
                        break;
                    case ReadMode.ReadLine:
                        _mySerialPort.DataReceived += DataReceivedHandlerReadLine;
                        break;
                }

                _mySerialPort.Open();
                _logger.Debug($"Opened Serial port {serialPortNumber}");

            }
            catch (Exception e)
            {
                _logger.Error($"Exception setting up COMM port {serialPortNumber}:  {e.Message}");
                _logger.Error($"{e}");
                throw;
            }

        }


        public void SetTimeout(int setTimeout_ms)
        {
            _timeoutMs = setTimeout_ms;
        }


        public void SendCommand(string cmd)
        {
            _mySerialPort.Write(cmd);
        }


        public void SendCommandLine(string cmd)
        {
            _mySerialPort.WriteLine(cmd);
        }

        public void ClosePort()
        {
            _mySerialPort.Close();
        }

        
        
        private void DataReceivedHandlerSlashRSlashN(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            _logger.Debug("Data Received:");
            string saveSpNewline = sp.NewLine;
            sp.NewLine = "\r\n";
            string inndata = sp.ReadLine();
            sp.NewLine = saveSpNewline;

            _logger.Debug($">>>{inndata}<<<");
            PortDataRead?.Invoke(this, inndata);
        }

        
        
        private void DataReceivedHandlerChunks(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            _logger.Debug("Data Received:");

            string readChunk = sp.ReadExisting();
            string inndata = readChunk.TrimEnd();

            while (!string.IsNullOrWhiteSpace(readChunk) && stopwatch.ElapsedMilliseconds < _timeoutMs)
            {
                readChunk = sp.ReadExisting();
                if (string.IsNullOrWhiteSpace(readChunk)) continue;
                inndata += readChunk.TrimEnd();
                _logger.Debug($".---{readChunk}---");
            }

            _logger.Debug($">>>{inndata}<<<");

            PortDataRead?.Invoke(this, inndata);
        }


        private void DataReceivedHandlerReadLine(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //sp.ReadTimeout = 500;
            //sp.NewLine = "\n";
            //sp.NewLine = "\r";
            //sp.NewLine = "\r\n";
            _logger.Debug("Data Received:");
            string inndata = String.Empty;
            //List<string> lstList = new List<string>();

            while (stopwatch.ElapsedMilliseconds < 500)
            {
                //string chunk = sp.ReadExisting();
                //inndata += chunk;
                inndata += sp.ReadExisting();
                //if (!string.IsNullOrEmpty(chunk))
                //{
                //    lstList.Add($"{stopwatch.ElapsedMilliseconds}-{chunk}");
                //}
            }
            _logger.Debug($">>>{inndata}<<<");

            PortDataRead?.Invoke(this, inndata);
        }

    }
}




