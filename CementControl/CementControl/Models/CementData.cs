using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CementControl.Execution;

namespace CementControl.Models
{
    public class CementData
    {
        public DateTime Date { get; set; }
        
        public string Description { get; set; }
        
        public double CurrentWeight { get; set; }

        public double CurrentVoltage { get; set; }

        public string State { get; set; }

        public double CementLoadGoal { get; set; }

        public double CementLoaded { get; set; }
        public double StartingWeight { get; set; }



        public CementData(string description, double currentWeight, ExecutionState state, double currentVoltage, double cementLoadGoal, double cementLoaded, double startingWeight)
        {
            Date = DateTime.Now;
            Description = description;
            CurrentWeight = currentWeight;
            CurrentVoltage = currentVoltage;
            State = state.ToString();
            CementLoadGoal = cementLoadGoal;
            CementLoaded = cementLoaded;
            StartingWeight = startingWeight;
        }
    }
}
