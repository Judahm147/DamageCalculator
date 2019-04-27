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
        protected List<Mod> mods = new List<Mod> { null, null, null, null, null, null, null, null };
        public double crit_chance { get; protected set; }
        protected Modifier cc_mult;
        public double crit_damage { get; protected set; }
        public double base_damage { get; protected set; }

        public abstract void report();

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
                case "base_damage":
                    DamageEffect(m);
                    break;
                case "critchance":
                    CCEffect(m);
                    break;
                case "projectiles":
                    Multishot(m);
                    break;
                case "damage":
                    InstallDamageType(m);
                    break;
            }   

        }

        private void Multishot(Modifier m)
        {
            throw new NotImplementedException();
        }

        private DamageType GetDamageType(DamageType type)
        {
            foreach (DamageType item in damages)
            {
                if (type.GetType() == item.GetType())
                    return item;
            }
            return null;
        }

        private void InstallDamageType(Modifier m)
        {
            DamageType dt = DamageType.GetDamageType(m.name);
            bool added = true;
            if (dt is PhysicalDamage)
            {
                dt = GetDamageType(dt);
                foreach (Modifier modifier in dt.multipliers)
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
                    dt.multipliers.Add(mod);
                }
                return;
            }
            else if (dt is ElementalDamage)
            {
                DamageType pom = dt;
                bool combine = false;
                foreach (DamageType item in damages)
                {
                    if (item.GetType() == dt.GetType())
                    {
                        dt = item;
                        break;
                    }
                    if (item.GetType().IsSubclassOf(typeof(BasicElementalDamage)) && (item.GetType() != dt.GetType()))
                    {
                        dt = CombinedElementalDamage.Combine(item, dt);
                        pom = item;
                        combine = true;
                        m.name = dt.name;
                        break;
                    }
                    else if (item.GetType().IsSubclassOf(typeof(CombinedElementalDamage)) && ((CombinedElementalDamage)item).IsCombinedFrom(dt))
                    {
                        dt = item;
                        m.name = item.name;
                        break;
                    }
                }
                if (combine)
                {
                    damages.Remove(pom);
                    damages.Add(dt);
                    foreach (Modifier modifier in dt.multipliers)
                    {
                        if (m.name == modifier.name)
                        {
                            modifier.effect += m.effect;
                            added = true;
                            break;
                        }
                    }
                    return;
                }
                if (dt == pom)
                {
                    dt.damage = this.base_damage;
                    dt.multipliers.Add(m);
                    this.damages.Add(dt);
                }
                else
                {
                    foreach (Modifier modifier in dt.multipliers)
                    {
                        if (m.name == modifier.name)
                        {
                            modifier.effect += m.effect;
                            added = true;
                            break;
                        }
                    }
                }
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

        public bool HasDamageType(DamageType dt)
        {
            foreach (DamageType item in damages)
            {
                if (item.GetType() == dt.GetType())
                    return true;
            }
            return false;
        }

        protected string CritDamage()
        {
            return crit_damage + "x";
        }

        protected string CritChance()
        {
            return GetCritChance() * 100 + "%";
        }
    }
}
