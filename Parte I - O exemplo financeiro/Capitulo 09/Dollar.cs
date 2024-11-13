//(C#)
namespace Capitulo_9
{
    public class Dollar : Money
    {
        public Dollar(int amount) : base(amount, "USD") { }

        public override Money Times(int multiplier)
        {
            return new Dollar(Amount * multiplier);
        }

    }
}