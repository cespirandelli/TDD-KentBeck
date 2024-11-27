namespace Capitulo_10
{
    public class Franc : Money
    {

        public Franc(int amount, string currency) : base(amount, currency)
        {
            _currency = currency;
        }
    }
}
