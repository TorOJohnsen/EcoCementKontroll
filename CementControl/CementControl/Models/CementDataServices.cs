using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CementControl.DataAccess;
using ServiceStack.Text;

namespace CementControl.Models
{
    public interface ICementDataServices
    {
        void SaveCementData(CementData cementData);
    }

    public class CementDataServices : ICementDataServices
    {
        private readonly DataFileHandle _dataFileHandle;
        private readonly string _sep = ",";


        public CementDataServices(DataFileHandle dataFileHandle)
        {
            _dataFileHandle = dataFileHandle;

            var header = $"Dato{_sep}Description{_sep}State{_sep}CurrentWeight{_sep}CurrentVoltage{_sep}CementLoadGoal{_sep}CementLoaded{_sep}StartingWeight{_sep}{Environment.NewLine}";
            SaveCementData(header);
        }

        public void SaveCementData(CementData cementData)
        {
            string data = ConvertToCsv(cementData);
            SaveCementData(data);
        }

        private void SaveCementData(string text)
        {
            _dataFileHandle.WriteData(text);
        }



        private string ConvertToCsv(CementData cementData)
        {

            string desc = cementData.Description.Replace(_sep, "-");
            
            
            string var =
                $"{DateTime.Now}{_sep}{desc}{_sep}{cementData.State}{_sep}{cementData.CurrentWeight}{_sep}{cementData.CurrentVoltage}{_sep}{cementData.CementLoadGoal}{_sep}{cementData.CementLoaded}{_sep}{cementData.StartingWeight}{_sep}{Environment.NewLine}";
            return var;
        }



    }
}
