//(C#)
namespace Capitulo_10
{
    public class Dollar : Money
    {

        public Dollar(int amount, string currency) : base(amount, currency)
        {
            _currency = currency;
        }
    }
}