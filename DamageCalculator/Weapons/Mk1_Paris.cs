using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamageCalculator.Weapons
{
    public class Mk1_Paris : Weapon
    {
        public Mk1_Paris()
        {
            damages = new List<DamageType>(3);
            damages.Add(new ImpactDamage(6.0));
            damages.Add(new PunctureDamage(96.0));
            damages.Add(new SlashDamage(18.0));
            crit_chance = 0.3;
            crit_damage = 2.0;
            base_damage = TotalDamage();
        }

        public override void report()
        {
            Console.WriteLine();
            foreach (DamageType item in damages)
            {
                item.report();
            }
            Console.WriteLine();
            Console.WriteLine("Base damage: " + base_damage);
            Console.WriteLine("Critical chance: " + CritChance());
            Console.WriteLine("Critical damage multiplier: " + CritDamage());
            Console.WriteLine();
            Console.WriteLine("Damage without crit: " + TotalDamage());
            Console.WriteLine("Damage when crit: " + DamageWhenCrit());
            Console.WriteLine("Average damage: " + AverageDamage());
        }
    }
}
