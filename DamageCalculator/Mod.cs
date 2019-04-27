using System;

namespace DamageCalculator
{
    abstract public class Mod
    {
        public int rank { get; set; } = 0;
        protected int max_rank;

        abstract public void InstallEffect(Weapon weapon);
    }

    public class Serration : Mod
    {
        public Serration()
        {
            max_rank = 10;
        }

        public Serration(int r)
        {
            max_rank = 10;
            rank = ((r <= max_rank) ? r : max_rank);
        }
        public override void InstallEffect(Weapon weapon)
        {
            weapon.InstallEffect("base_damage", new Modifier("damage", 0.15 * (1 + rank)));
        }
    }

    public class PointStrike : Mod
    {
        public PointStrike()
        {
            max_rank = 5;
        }

        public PointStrike(int r)
        {
            max_rank = 5;
            rank = ((r <= max_rank) ? r : max_rank);
        }
        public override void InstallEffect(Weapon weapon)
        {
            weapon.InstallEffect("critchance", new Modifier("cc", 0.25 * (1 + rank)));
        }
    }

    public class SplitChamber : Mod
    {
        public SplitChamber()
        {
            max_rank = 5;
        }

        public SplitChamber(int r)
        {
            max_rank = 5;
            rank = ((r <= max_rank) ? r : max_rank);
        }
        public override void InstallEffect(Weapon weapon)
        {
            weapon.InstallEffect("projectiles", new Modifier("multishot", 0.15 * (1 + rank)));
        }
    }

    public class Hellfire : Mod
    {
        public Hellfire()
        {
            max_rank = 5;
        }

        public Hellfire(int r)
        {
            max_rank = 5;
            rank = ((r <= max_rank) ? r : max_rank);
        }

        public override void InstallEffect(Weapon weapon)
        {
            weapon.InstallEffect("damage", new Modifier("Heat", 0.15 * (1 + rank)));
        }
    }

    public class Stormbringer : Mod
    {
        public Stormbringer()
        {
            max_rank = 5;
        }

        public Stormbringer(int r)
        {
            max_rank = 5;
            rank = ((r <= max_rank) ? r : max_rank);
        }

        public override void InstallEffect(Weapon weapon)
        {
            weapon.InstallEffect("damage", new Modifier("Electricity", 0.15 * (1 + rank)));
        }
    }
}