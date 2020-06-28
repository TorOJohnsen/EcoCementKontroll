using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using ServiceStack;

namespace CementControl.DataAccess
{
    public class DataFileHandle
    {
        private readonly ILogger _logger = Log.ForContext<DataFileHandle>();
        private readonly string _dataLogFile;
        private readonly StringBuilder _sb;
        private readonly int _bufferLenght = 200;

        public DataFileHandle(string dataLogFile)
        {
            _dataLogFile = dataLogFile;
            _sb = new StringBuilder();
        }


        public void FlushLogfile()
        {
            File.AppendAllText(_dataLogFile, _sb.ToString());
            _sb.Clear();
        }


        public void WriteData(string data)
        {
            _sb.Append(data);
            FlushWhenBufferLarge();

        }


        private void FlushWhenBufferLarge()
        {
            if (_sb == null) return;
            if (_sb.Length > _bufferLenght)
            {
                FlushLogfile();
            }
        }


        public void Close()
        {
            FlushLogfile();
        }








    }
}
