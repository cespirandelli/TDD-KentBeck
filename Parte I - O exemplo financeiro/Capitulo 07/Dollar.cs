//(C#)
namespace Capitulo_7
{
    public class Dollar : Money
    {
        public Dollar(int amount) : base(amount) { }

        public Dollar Times(int multiplier)
        {
            return new Dollar(Amount * multiplier);
        }
    }
}