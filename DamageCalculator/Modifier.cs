namespace DamageCalculator
{
    //public delegate double Effect(double value);

    public class Modifier
    {
        public string name;
        public double effect;

        public Modifier(string name,double effect)
        {
            this.name = name;
            this.effect = effect;
        }
    }
}