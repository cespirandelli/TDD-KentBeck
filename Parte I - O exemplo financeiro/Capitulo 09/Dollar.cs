//(C#)
namespace Capitulo_9
{
    public class Dollar : Money
    {

        public Dollar(int amount, string currency) : base(amount)
        {
            _currency = currency;
        }

        public override Money Times(int multiplier)
        {
            return Money.Dollar(Amount * multiplier);
        }
    }
}