using System;
using System.Collections.Generic;

namespace DamageCalculator
{
    abstract public class DamageType
    {
        public DamageType() {}
        public double damage { get; set; }
        public List<Modifier> multipliers = new List<Modifier>();
        public Dictionary<HealthClass, double> damageModifiers;

        public abstract void report();

        public double GetTotalDamage()
        {
            double pom = damage;
            foreach (var item in multipliers)
            {
                pom *= item.effect;
            }
            return pom;
        }
    }

    abstract public class PhysicalDamage : DamageType
    { }

    public class ImpactDamage : PhysicalDamage
    {
        static Dictionary<HealthClass, double> ImpactDamageModifiers;
        static ImpactDamage()
        {
            ImpactDamageModifiers = new Dictionary<HealthClass, double>();
            ImpactDamageModifiers.Add(HealthClass.Flesh, -0.25);
            ImpactDamageModifiers.Add(HealthClass.Cloned_Flesh, -0.25);
            ImpactDamageModifiers.Add(HealthClass.Fossilized, 0.0);
            ImpactDamageModifiers.Add(HealthClass.Infested, 0.0);
            ImpactDamageModifiers.Add(HealthClass.Infested_Flesh, 0.0);
            ImpactDamageModifiers.Add(HealthClass.Infested_Sinew, 0.0);
            ImpactDamageModifiers.Add(HealthClass.Machinery, +0.25);
            ImpactDamageModifiers.Add(HealthClass.Robotic, 0.0);
            ImpactDamageModifiers.Add(HealthClass.Object, 0.0);
            ImpactDamageModifiers.Add(HealthClass.Shield, +0.5);
            ImpactDamageModifiers.Add(HealthClass.Proto_Shield, +0.25);
            ImpactDamageModifiers.Add(HealthClass.Ferrite_Armor, 0.0);
            ImpactDamageModifiers.Add(HealthClass.Alloy_Armor, 0.0);
        } 
        
        public ImpactDamage(double value)
        {
            damageModifiers = ImpactDamageModifiers;
            damage = value;
        }

        public override void report()
        {
            Console.WriteLine($"Impact: {GetTotalDamage()}");
        }
    }

    public class PunctureDamage : PhysicalDamage
    {
        static Dictionary<HealthClass, double> PunctureDamageModifiers;
        static PunctureDamage()
        {
            PunctureDamageModifiers = new Dictionary<HealthClass, double>();
            PunctureDamageModifiers.Add(HealthClass.Flesh, 0.0);
            PunctureDamageModifiers.Add(HealthClass.Cloned_Flesh, 0.0);
            PunctureDamageModifiers.Add(HealthClass.Fossilized, 0.0);
            PunctureDamageModifiers.Add(HealthClass.Infested, 0.0);
            PunctureDamageModifiers.Add(HealthClass.Infested_Flesh, 0.0);
            PunctureDamageModifiers.Add(HealthClass.Infested_Sinew, +0.25);
            PunctureDamageModifiers.Add(HealthClass.Machinery, 0.0);
            PunctureDamageModifiers.Add(HealthClass.Robotic, +0.25);
            PunctureDamageModifiers.Add(HealthClass.Object, 0.0);
            PunctureDamageModifiers.Add(HealthClass.Shield, -0.2);
            PunctureDamageModifiers.Add(HealthClass.Proto_Shield, -0.5);
            PunctureDamageModifiers.Add(HealthClass.Ferrite_Armor, +0.5);
            PunctureDamageModifiers.Add(HealthClass.Alloy_Armor, +0.15);
        }

        public PunctureDamage(double value)
        {
            damageModifiers = PunctureDamageModifiers;
            damage = value;
        }

        public override void report()
        {
            Console.WriteLine($"Puncture: {GetTotalDamage()}");
        }
    }

    public class SlashDamage : PhysicalDamage
    {
        static Dictionary<HealthClass, double> SlashDamageModifiers;
        static SlashDamage()
        {
            SlashDamageModifiers = new Dictionary<HealthClass, double>();
            SlashDamageModifiers.Add(HealthClass.Flesh, +0.25);
            SlashDamageModifiers.Add(HealthClass.Cloned_Flesh, +0.25);
            SlashDamageModifiers.Add(HealthClass.Fossilized, +0.15);
            SlashDamageModifiers.Add(HealthClass.Infested, +0.25);
            SlashDamageModifiers.Add(HealthClass.Infested_Flesh, +0.5);
            SlashDamageModifiers.Add(HealthClass.Infested_Sinew, 0.0);
            SlashDamageModifiers.Add(HealthClass.Machinery, 0.0);
            SlashDamageModifiers.Add(HealthClass.Robotic, -0.25);
            SlashDamageModifiers.Add(HealthClass.Object, 0.0);
            SlashDamageModifiers.Add(HealthClass.Shield, 0.0);
            SlashDamageModifiers.Add(HealthClass.Proto_Shield, 0.0);
            SlashDamageModifiers.Add(HealthClass.Ferrite_Armor, -0.15);
            SlashDamageModifiers.Add(HealthClass.Alloy_Armor, -0.5);
        }

        public SlashDamage(double value)
        {
            damageModifiers = SlashDamageModifiers;
            damage = value;
        }

        public override void report()
        {
            Console.WriteLine($"Slash: {GetTotalDamage()}");
        }
    }


}