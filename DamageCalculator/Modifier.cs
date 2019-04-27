namespace DamageCalculator
{
    //public delegate double Effect(double value);

    public class Modifier
    {
        public string name { get; set; }
        public double effect { get; set; }

        public Modifier(string name,double effect)
        {
            this.name = name;
            this.effect = effect;
        }
    }
}