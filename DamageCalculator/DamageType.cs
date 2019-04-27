using System;
using System.Collections.Generic;

namespace DamageCalculator
{
    abstract public class DamageType
    {
        public DamageType() {}
        public double damage { get; set; }
        public string name { get; protected set; }
        public List<Modifier> multipliers = new List<Modifier>();
        public Dictionary<HealthClass, double> damageModifiers;

        public void report()
        {
            Console.WriteLine($"{name}: {GetTotalDamage()}");
        }

        public double GetTotalDamage()
        {
            double pom = damage;
            foreach (var item in multipliers)
            {
                pom *= item.effect;
            }
            return pom;
        }

        public double GetTotalDamage(HealthClass hc)
        {
            double pom = damage;
            foreach (var item in multipliers)
            {
                pom *= item.effect;
            }
            return pom * damageModifiers[hc];
        }

        public static DamageType GetDamageType(string name)
        {
            switch(name)
            {
                case "Impact": return new ImpactDamage(1.0);
                case "Puncture": return new PunctureDamage(1.0);
                case "Slash": return new SlashDamage(1.0);
                case "Heat": return new HeatDamage(1.0);
                case "Toxin": return new ToxinDamage(1.0);
                case "Cold": return new ColdDamage(1.0);
                case "Electricity": return new ElectricityDamage(1.0);
                case "Viral": return new ViralDamage(1.0);
                case "Radiation": return new RadiationDamage(1.0);
                case "Magnetic": return new MagneticDamage(1.0);
                case "Gas": return new GasDamage(1.0);
                case "Corrosive": return new CorrosiveDamage(1.0);
                case "Blast": return new BlastDamage(1.0);
                default:
                    throw new ArgumentException("Unsupported damage type");
            }
        }
    }

    abstract public class PhysicalDamage : DamageType { }

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
            name = "Impact";
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
            name = "Puncture";
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
            name = "Slash";
        }
    }

    abstract public class ElementalDamage : DamageType { }

    abstract public class BasicElementalDamage : ElementalDamage { }

    public class HeatDamage : BasicElementalDamage
    {
        static Dictionary<HealthClass, double> HeatDamageModifiers;
        static HeatDamage()
        {
            HeatDamageModifiers = new Dictionary<HealthClass, double>();
            HeatDamageModifiers.Add(HealthClass.Flesh, 0.0);
            HeatDamageModifiers.Add(HealthClass.Cloned_Flesh, +0.25);
            HeatDamageModifiers.Add(HealthClass.Fossilized, 0.0);
            HeatDamageModifiers.Add(HealthClass.Infested, +0.25);
            HeatDamageModifiers.Add(HealthClass.Infested_Flesh, +0.5);
            HeatDamageModifiers.Add(HealthClass.Infested_Sinew, 0.0);
            HeatDamageModifiers.Add(HealthClass.Machinery, 0.0);
            HeatDamageModifiers.Add(HealthClass.Robotic, 0.0);
            HeatDamageModifiers.Add(HealthClass.Object, 0.0);
            HeatDamageModifiers.Add(HealthClass.Shield, 0.0);
            HeatDamageModifiers.Add(HealthClass.Proto_Shield, 0.0);
            HeatDamageModifiers.Add(HealthClass.Ferrite_Armor, 0.0);
            HeatDamageModifiers.Add(HealthClass.Alloy_Armor, -0.5);
        }

        public HeatDamage(double value)
        {
            damageModifiers = HeatDamageModifiers;
            damage = value;
            name = "Heat";
        }
    }

    public class ToxinDamage : BasicElementalDamage
    {
        static Dictionary<HealthClass, double> ToxinDamageModifiers;
        static ToxinDamage()
        {
            ToxinDamageModifiers = new Dictionary<HealthClass, double>();
            ToxinDamageModifiers.Add(HealthClass.Flesh, +0.5);
            ToxinDamageModifiers.Add(HealthClass.Cloned_Flesh, 0.0);
            ToxinDamageModifiers.Add(HealthClass.Fossilized, -0.5);
            ToxinDamageModifiers.Add(HealthClass.Infested, 0.0);
            ToxinDamageModifiers.Add(HealthClass.Infested_Flesh, 0.0);
            ToxinDamageModifiers.Add(HealthClass.Infested_Sinew, 0.0);
            ToxinDamageModifiers.Add(HealthClass.Machinery, -0.25);
            ToxinDamageModifiers.Add(HealthClass.Robotic, -0.25);
            ToxinDamageModifiers.Add(HealthClass.Object, 0.0);
            ToxinDamageModifiers.Add(HealthClass.Shield, Double.NaN);
            ToxinDamageModifiers.Add(HealthClass.Proto_Shield, Double.NaN);
            ToxinDamageModifiers.Add(HealthClass.Ferrite_Armor, +0.25);
            ToxinDamageModifiers.Add(HealthClass.Alloy_Armor, 0.0);
        }

        public ToxinDamage(double value)
        {
            damageModifiers = ToxinDamageModifiers;
            damage = value;
            name = "Toxin";
        }
    }

    public class ColdDamage : BasicElementalDamage
    {
        static Dictionary<HealthClass, double> ColdDamageModifiers;
        static ColdDamage()
        {
            ColdDamageModifiers = new Dictionary<HealthClass, double>();
            ColdDamageModifiers.Add(HealthClass.Flesh, 0.0);
            ColdDamageModifiers.Add(HealthClass.Cloned_Flesh, 0.0);
            ColdDamageModifiers.Add(HealthClass.Fossilized, -0.25);
            ColdDamageModifiers.Add(HealthClass.Infested, 0.0);
            ColdDamageModifiers.Add(HealthClass.Infested_Flesh, -0.5);
            ColdDamageModifiers.Add(HealthClass.Infested_Sinew, +0.25);
            ColdDamageModifiers.Add(HealthClass.Machinery, 0.0);
            ColdDamageModifiers.Add(HealthClass.Robotic, 0.0);
            ColdDamageModifiers.Add(HealthClass.Object, 0.0);
            ColdDamageModifiers.Add(HealthClass.Shield, +0.5);
            ColdDamageModifiers.Add(HealthClass.Proto_Shield, 0.0);
            ColdDamageModifiers.Add(HealthClass.Ferrite_Armor, 0.0);
            ColdDamageModifiers.Add(HealthClass.Alloy_Armor, +0.25);
        }

        public ColdDamage(double value)
        {
            damageModifiers = ColdDamageModifiers;
            damage = value;
            name = "Cold";
        }
    }

    public class ElectricityDamage : BasicElementalDamage
    {
        static Dictionary<HealthClass, double> ElectricityDamageModifiers;
        static ElectricityDamage()
        {
            ElectricityDamageModifiers = new Dictionary<HealthClass, double>();
            ElectricityDamageModifiers.Add(HealthClass.Flesh, 0.0);
            ElectricityDamageModifiers.Add(HealthClass.Cloned_Flesh, 0.0);
            ElectricityDamageModifiers.Add(HealthClass.Fossilized, 0.0);
            ElectricityDamageModifiers.Add(HealthClass.Infested, 0.0);
            ElectricityDamageModifiers.Add(HealthClass.Infested_Flesh, 0.0);
            ElectricityDamageModifiers.Add(HealthClass.Infested_Sinew, 0.0);
            ElectricityDamageModifiers.Add(HealthClass.Machinery, +0.5);
            ElectricityDamageModifiers.Add(HealthClass.Robotic, +0.5);
            ElectricityDamageModifiers.Add(HealthClass.Object, 0.0);
            ElectricityDamageModifiers.Add(HealthClass.Shield, 0.0);
            ElectricityDamageModifiers.Add(HealthClass.Proto_Shield, 0.0);
            ElectricityDamageModifiers.Add(HealthClass.Ferrite_Armor, 0.0);
            ElectricityDamageModifiers.Add(HealthClass.Alloy_Armor, -0.5);
        }

        public ElectricityDamage(double value)
        {
            damageModifiers = ElectricityDamageModifiers;
            damage = value;
            name = "Electricity";
        }
    }

    abstract public class CombinedElementalDamage : ElementalDamage
    {
       abstract public bool IsCombinedFrom(DamageType dt);


        private static void UpdateModifier(DamageType dt, string orig_name, string new_name)
        {
            foreach (Modifier modifier in dt.multipliers)
            {
                if (orig_name == modifier.name)
                {
                    modifier.name = new_name;
                    return;
                }
            }
        }
        public static DamageType Combine(DamageType original, DamageType combineWith)
        {
            DamageType pom;
            if (original.GetType() == typeof(HeatDamage))
            {
                if (combineWith.GetType() == typeof(ColdDamage))
                {
                    pom = new BlastDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
                else if (combineWith.GetType() == typeof(ToxinDamage))
                {
                    pom = new GasDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
                else // Heat + Electricity
                {
                    pom = new RadiationDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
            }
            else if (original.GetType() == typeof(ColdDamage))
            {
                if (combineWith.GetType() == typeof(HeatDamage))
                {
                    pom = new BlastDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
                else if (combineWith.GetType() == typeof(ToxinDamage))
                {
                    pom = new ViralDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
                else // Cold + Electricity
                {
                    pom = new MagneticDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
            }
            else if (original.GetType() == typeof(ToxinDamage))
            {
                if (combineWith.GetType() == typeof(HeatDamage))
                {
                    pom = new GasDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
                else if (combineWith.GetType() == typeof(ColdDamage))
                {
                    pom = new ViralDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
                else // Toxin + Electricity
                {
                    pom = new CorrosiveDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
            }
            else // first = Electricity
            {
                if (combineWith.GetType() == typeof(HeatDamage))
                {
                    pom = new RadiationDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
                else if (combineWith.GetType() == typeof(ToxinDamage))
                {
                    pom = new CorrosiveDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
                else // Cold + Electricity
                {
                    pom = new MagneticDamage(original.damage);
                    pom.multipliers = original.multipliers;
                    UpdateModifier(pom, original.name, pom.name);
                    return pom;
                }
            }
        }
    }

    public class ViralDamage : CombinedElementalDamage
    {
        static Dictionary<HealthClass, double> ViralDamageModifiers;
        static ViralDamage()
        {
            ViralDamageModifiers = new Dictionary<HealthClass, double>();
            ViralDamageModifiers.Add(HealthClass.Flesh, +0.5);
            ViralDamageModifiers.Add(HealthClass.Cloned_Flesh, +0.75);
            ViralDamageModifiers.Add(HealthClass.Fossilized, 0.0);
            ViralDamageModifiers.Add(HealthClass.Infested, -0.5);
            ViralDamageModifiers.Add(HealthClass.Infested_Flesh, 0.0);
            ViralDamageModifiers.Add(HealthClass.Infested_Sinew, 0.0);
            ViralDamageModifiers.Add(HealthClass.Machinery, -0.25);
            ViralDamageModifiers.Add(HealthClass.Robotic, 0.0);
            ViralDamageModifiers.Add(HealthClass.Object, 0.0);
            ViralDamageModifiers.Add(HealthClass.Shield, 0.0);
            ViralDamageModifiers.Add(HealthClass.Proto_Shield, 0.0);
            ViralDamageModifiers.Add(HealthClass.Ferrite_Armor, 0.0);
            ViralDamageModifiers.Add(HealthClass.Alloy_Armor, -0.5);
        }

        public ViralDamage(double value)
        {
            damageModifiers = ViralDamageModifiers;
            damage = value;
            name = "Viral";
        }

        public override bool IsCombinedFrom(DamageType dt)
        {
            if (dt.GetType() == typeof(ColdDamage))
                return true;
            else if (dt.GetType() == typeof(ToxinDamage))
                return true;
            else
                return false;
        }
    }

    public class RadiationDamage : CombinedElementalDamage
    {
        static Dictionary<HealthClass, double> RadiationDamageModifiers;
        static RadiationDamage()
        {
            RadiationDamageModifiers = new Dictionary<HealthClass, double>();
            RadiationDamageModifiers.Add(HealthClass.Flesh, 0.0);
            RadiationDamageModifiers.Add(HealthClass.Cloned_Flesh, 0.0);
            RadiationDamageModifiers.Add(HealthClass.Fossilized, -0.75);
            RadiationDamageModifiers.Add(HealthClass.Infested, -0.5);
            RadiationDamageModifiers.Add(HealthClass.Infested_Flesh, 0.0);
            RadiationDamageModifiers.Add(HealthClass.Infested_Sinew, +0.5);
            RadiationDamageModifiers.Add(HealthClass.Machinery, 0.0);
            RadiationDamageModifiers.Add(HealthClass.Robotic, +0.25);
            RadiationDamageModifiers.Add(HealthClass.Object, 0.0);
            RadiationDamageModifiers.Add(HealthClass.Shield, -0.25);
            RadiationDamageModifiers.Add(HealthClass.Proto_Shield, 0.0);
            RadiationDamageModifiers.Add(HealthClass.Ferrite_Armor, 0.0);
            RadiationDamageModifiers.Add(HealthClass.Alloy_Armor, +0.75);
        }

        public RadiationDamage(double value)
        {
            damageModifiers = RadiationDamageModifiers;
            damage = value;
            name = "Radiation";
        }

        public override bool IsCombinedFrom(DamageType dt)
        {
            if (dt.GetType() == typeof(HeatDamage))
                return true;
            else if (dt.GetType() == typeof(ElectricityDamage))
                return true;
            else
                return false;
        }
    }

    public class MagneticDamage : CombinedElementalDamage
    {
        static Dictionary<HealthClass, double> MagneticDamageModifiers;
        static MagneticDamage()
        {
            MagneticDamageModifiers = new Dictionary<HealthClass, double>();
            MagneticDamageModifiers.Add(HealthClass.Flesh, 0.0);
            MagneticDamageModifiers.Add(HealthClass.Cloned_Flesh, 0.0);
            MagneticDamageModifiers.Add(HealthClass.Fossilized, 0.0);
            MagneticDamageModifiers.Add(HealthClass.Infested, 0.0);
            MagneticDamageModifiers.Add(HealthClass.Infested_Flesh, 0.0);
            MagneticDamageModifiers.Add(HealthClass.Infested_Sinew, 0.0);
            MagneticDamageModifiers.Add(HealthClass.Machinery, 0.0);
            MagneticDamageModifiers.Add(HealthClass.Robotic, 0.0);
            MagneticDamageModifiers.Add(HealthClass.Object, 0.0);
            MagneticDamageModifiers.Add(HealthClass.Shield, +0.75);
            MagneticDamageModifiers.Add(HealthClass.Proto_Shield, +0.75);
            MagneticDamageModifiers.Add(HealthClass.Ferrite_Armor, 0.0);
            MagneticDamageModifiers.Add(HealthClass.Alloy_Armor, 0.0);
        }

        public MagneticDamage(double value)
        {
            damageModifiers = MagneticDamageModifiers;
            damage = value;
            name = "Magnetic";
        }

        public override bool IsCombinedFrom(DamageType dt)
        {
            if (dt.GetType() == typeof(ColdDamage))
                return true;
            else if (dt.GetType() == typeof(ElectricityDamage))
                return true;
            else
                return false;
        }
    }

    public class GasDamage : CombinedElementalDamage
    {
        static Dictionary<HealthClass, double> GasDamageModifiers;
        static GasDamage()
        {
            GasDamageModifiers = new Dictionary<HealthClass, double>();
            GasDamageModifiers.Add(HealthClass.Flesh, -0.25);
            GasDamageModifiers.Add(HealthClass.Cloned_Flesh, -0.5);
            GasDamageModifiers.Add(HealthClass.Fossilized, 0.0);
            GasDamageModifiers.Add(HealthClass.Infested, +0.75);
            GasDamageModifiers.Add(HealthClass.Infested_Flesh, +0.5);
            GasDamageModifiers.Add(HealthClass.Infested_Sinew, 0.0);
            GasDamageModifiers.Add(HealthClass.Machinery, 0.0);
            GasDamageModifiers.Add(HealthClass.Robotic, 0.0);
            GasDamageModifiers.Add(HealthClass.Object, 0.0);
            GasDamageModifiers.Add(HealthClass.Shield, 0.0);
            GasDamageModifiers.Add(HealthClass.Proto_Shield, 0.0);
            GasDamageModifiers.Add(HealthClass.Ferrite_Armor, 0.0);
            GasDamageModifiers.Add(HealthClass.Alloy_Armor, 0.0);
        }

        public GasDamage(double value)
        {
            damageModifiers = GasDamageModifiers;
            damage = value;
            name = "Gas";
        }

        public override bool IsCombinedFrom(DamageType dt)
        {
            if (dt.GetType() == typeof(HeatDamage))
                return true;
            else if (dt.GetType() == typeof(ToxinDamage))
                return true;
            else
                return false;
        }
    }

    public class CorrosiveDamage : CombinedElementalDamage
    {
        static Dictionary<HealthClass, double> CorrosiveDamageModifiers;
        static CorrosiveDamage()
        {
            CorrosiveDamageModifiers = new Dictionary<HealthClass, double>();
            CorrosiveDamageModifiers.Add(HealthClass.Flesh, 0.0);
            CorrosiveDamageModifiers.Add(HealthClass.Cloned_Flesh, 0.0);
            CorrosiveDamageModifiers.Add(HealthClass.Fossilized, +0.75);
            CorrosiveDamageModifiers.Add(HealthClass.Infested, 0.0);
            CorrosiveDamageModifiers.Add(HealthClass.Infested_Flesh, 0.0);
            CorrosiveDamageModifiers.Add(HealthClass.Infested_Sinew, 0.0);
            CorrosiveDamageModifiers.Add(HealthClass.Machinery, 0.0);
            CorrosiveDamageModifiers.Add(HealthClass.Robotic, 0.0);
            CorrosiveDamageModifiers.Add(HealthClass.Object, 0.0);
            CorrosiveDamageModifiers.Add(HealthClass.Shield, 0.0);
            CorrosiveDamageModifiers.Add(HealthClass.Proto_Shield, -0.5);
            CorrosiveDamageModifiers.Add(HealthClass.Ferrite_Armor, +0.75);
            CorrosiveDamageModifiers.Add(HealthClass.Alloy_Armor, 0.0);
        }

        public CorrosiveDamage(double value)
        {
            damageModifiers = CorrosiveDamageModifiers;
            damage = value;
            name = "Corrosive";
        }

        public override bool IsCombinedFrom(DamageType dt)
        {
            if (dt.GetType() == typeof(ElectricityDamage))
                return true;
            else if (dt.GetType() == typeof(ToxinDamage))
                return true;
            else
                return false;
        }
    }

    public class BlastDamage : CombinedElementalDamage
    {
        static Dictionary<HealthClass, double> BlastDamageModifiers;
        static BlastDamage()
        {
            BlastDamageModifiers = new Dictionary<HealthClass, double>();
            BlastDamageModifiers.Add(HealthClass.Flesh, 0.0);
            BlastDamageModifiers.Add(HealthClass.Cloned_Flesh, 0.0);
            BlastDamageModifiers.Add(HealthClass.Fossilized, 0.0);
            BlastDamageModifiers.Add(HealthClass.Infested, 0.0);
            BlastDamageModifiers.Add(HealthClass.Infested_Flesh, 0.0);
            BlastDamageModifiers.Add(HealthClass.Infested_Sinew, 0.0);
            BlastDamageModifiers.Add(HealthClass.Machinery, 0.0);
            BlastDamageModifiers.Add(HealthClass.Robotic, 0.0);
            BlastDamageModifiers.Add(HealthClass.Object, 0.0);
            BlastDamageModifiers.Add(HealthClass.Shield, +0.75);
            BlastDamageModifiers.Add(HealthClass.Proto_Shield, +0.75);
            BlastDamageModifiers.Add(HealthClass.Ferrite_Armor, 0.0);
            BlastDamageModifiers.Add(HealthClass.Alloy_Armor, 0.0);
        }

        public BlastDamage(double value)
        {
            damageModifiers = BlastDamageModifiers;
            damage = value;
            name = "Blast";
        }

        public override bool IsCombinedFrom(DamageType dt)
        {
            if (dt.GetType() == typeof(ColdDamage))
                return true;
            else if (dt.GetType() == typeof(HeatDamage))
                return true;
            else
                return false;
        }
    }
}