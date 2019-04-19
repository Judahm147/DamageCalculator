using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamageCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Weapon w = new Mk1_Paris();
            w.report();
            w.InstallMod(new Serration(10), 1);
            w.report();
            w.InstallMod(new PointStrike(5), 0);
            w.report();
        }
    }
}
