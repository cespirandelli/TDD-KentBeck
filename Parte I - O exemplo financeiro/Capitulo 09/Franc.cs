namespace Capitulo_9
{
    public class Franc : Money
    {

        public Franc(int amount, string currency) : base(amount)
        {
            _currency = currency;
        }

        public override Money Times(int multiplier)
        {
            return Money.Franc(Amount * multiplier);
        }

    }
}
