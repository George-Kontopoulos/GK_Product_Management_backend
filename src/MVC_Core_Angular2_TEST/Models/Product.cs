using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public DateTime available { get; set; }
        public Decimal price { get; set; }
        public float rating { get; set; }

    }
}
