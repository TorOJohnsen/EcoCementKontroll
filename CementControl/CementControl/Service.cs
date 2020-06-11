using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace CementControl
{
    public class Service
    {
        private readonly ISerialDevice _serialDevice;

        public Service(ISerialDevice serialDevice)
        {
            _serialDevice = serialDevice;
        }

    }
}
