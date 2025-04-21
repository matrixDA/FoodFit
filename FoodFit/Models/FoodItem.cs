using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodFit.Models
{
    public class FoodItem
    {
        public int FdcId { get; set; }
        public string Name { get; set; }
        public float Calories { get; set; }
        public float Carbs { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public override string ToString() => Name;
    }

}
