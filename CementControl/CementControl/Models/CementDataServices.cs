using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CementControl.DataAccess;

namespace CementControl.Models
{
    public interface ICementDataServices
    {
        void SaveCementData(CementData cementData);
    }

    public class CementDataServices : ICementDataServices
    {
        private readonly CementContext _cementContext;

        public CementDataServices(CementContext cementContext)
        {
            _cementContext = cementContext;

        }

        public void SaveCementData(CementData cementData)
        {
            _cementContext.CementDatas.Add(cementData);
            _cementContext.SaveChanges();
        }

    }
}
