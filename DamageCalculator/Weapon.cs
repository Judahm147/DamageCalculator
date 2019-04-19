using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamageCalculator
{
    abstract public class Weapon
    {
        protected List<DamageType> damages;
        protected List<Mod> mods = new List<Mod> {null,null,null,null,null,null,null,null};
        public double crit_chance { get; protected set; }
        protected Modifier cc_mult;
        public double crit_damage { get; protected set; }

        public abstract void report();
        public double BaseDamage()
        {
            double pom = 0.0;
            foreach (var item in damages)
            {
                pom += item.damage;
            }
            return pom;
        }

        public double TotalDamage()
        {
            double pom = 0.0;
            foreach (var item in damages)
            {
                pom += item.GetTotalDamage();
            }
            return pom;
        }

        public double DamageWhenCrit()
        {
            return TotalDamage() * crit_damage;
        }

        public double AverageDamage()
        {
            double cc = GetCritChance();
            return TotalDamage() * (1 - cc) + DamageWhenCrit() * cc;
        }

        public void InstallMod(Mod m, int position)
        {
            if (mods[position] != null)
                UnInstallMod(position);
            mods[position] = m;
            m.InstallEffect(this);
        }

        private void UnInstallMod(int position)
        {
            throw new NotImplementedException();
        }

        public void InstallEffect(string field, Modifier m)
        {
            switch (field)
            {
                case "damage":
                    DamageEffect(m);
                    break;
                case "critchance":
                    CCEffect(m);
                    break;
            }
            
        }

        private void CCEffect(Modifier m)
        {
            if (cc_mult == null)
                cc_mult = new Modifier(m.name, 1 + m.effect);
        }

        private void DamageEffect(Modifier m)
        {
            bool added = false;
            foreach (DamageType item in damages)
            {
                foreach (Modifier modifier in item.multipliers)
                {
                    if (m.name == modifier.name)
                    {
                        modifier.effect += m.effect;
                        added = true;
                        break;
                    }
                }
                if (!added)
                {
                    Modifier mod = new Modifier(m.name, m.effect + 1);
                    item.multipliers.Add(mod);
                }
            }
        }

        public double GetCritChance()
        {
            if (cc_mult == null)
                return crit_chance;
            else
                return crit_chance * cc_mult.effect;
        }
    }

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
        }

        public override void report()
        {
            Console.WriteLine();
            foreach (DamageType item in damages)
            {
                item.report();
            }
            Console.WriteLine();
            Console.WriteLine("Base damage: " + BaseDamage());
            Console.WriteLine("Critical chance: " + CritChance());
            Console.WriteLine("Critical damage multiplier: " + CritDamage());
            Console.WriteLine();
            Console.WriteLine("Damage without crit: " + TotalDamage());
            Console.WriteLine("Damage when crit: " + DamageWhenCrit());
            Console.WriteLine("Average damage: " + AverageDamage());
        }

        private string CritDamage()
        {
            return crit_damage + "x";
        }

        private string CritChance()
        {
            return GetCritChance() * 100 + "%";
        }
    }
}
