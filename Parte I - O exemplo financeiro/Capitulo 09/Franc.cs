namespace Capitulo_9
{
    public class Franc : Money
    {

        public Franc(int amount) : base(amount, "CHF") { }

        public override Money Times(int multiplier)
        {
            return new Franc(Amount * multiplier);
        }

    }
}
