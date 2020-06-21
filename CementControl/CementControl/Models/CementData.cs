using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CementControl.Models
{
    public class CementData
    {
        [Key]
        public int Id { get; set; }
        
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss:fff }")]
        public DateTime Date { get; set; }
        
        public string Description { get; set; }
        
        public double Weight { get; set; }
        
        public int State { get; set; }


        public CementData() { }

        public CementData(DateTime date, string description, double weight, int state)
        {
            Date = date;
            Description = description;
            Weight = weight;
            State = state;

        }

        



    }
}
