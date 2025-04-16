using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFit.ViewModels
{
    public class SleepLog
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime BedTime { get; set; }
        public DateTime WakeUpTime { get; set; }
        public string Notes { get; set; }
    }
}
