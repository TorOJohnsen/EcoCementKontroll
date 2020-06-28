using Microsoft.VisualStudio.TestTools.UnitTesting;
using CementControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CementControl.DataAccess;
using CementControl.Execution;

namespace CementControl.Models.Tests
{
    [TestClass()]
    public class CementDataServicesTests
    {

        [TestMethod()]
        public void SaveCementDataTest()
        {

            DataFileHandle dataFileHandle = new DataFileHandle(@"c:/temp/dataFile.csv");
            CementDataServices cementDataServices = new CementDataServices(dataFileHandle);

            CementData c1 = new CementData("Hole", 1.10, 1.10, ExecutionState.Loading,2.20, 3.30, 4.40, 5.50);
            CementData c2 = new CementData("Hole", 1.11, 1.10, ExecutionState.Loading,2.21, 3.31, 4.41, 5.51);
            CementData c3 = new CementData("Hole", 1.12, 1.10, ExecutionState.Loading,2.22, 3.32, 4.42, 5.52);

            cementDataServices.SaveCementData(c1);
            cementDataServices.SaveCementData(c2);
            cementDataServices.SaveCementData(c3);

            dataFileHandle.Close();

        }
    }
}