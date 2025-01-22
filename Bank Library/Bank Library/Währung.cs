using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Library
{
    public class Währung
    {
        public double Euro { get; set; }
        public double Dollar { get; set; }

        public void ToDollar()
        {
            //Kontostand * 1.05
        }
    }
}
