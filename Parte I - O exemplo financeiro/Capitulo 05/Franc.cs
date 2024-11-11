/*
    5 CHF * 2 = 10 CHF                               <- FOCO
    Duplicação de Dólar/Franco
    Igualdade comum
    Multiplicação comum
 */

namespace Capitulo_05
{
    public class Franc
    {
        private int Amount { get; }

        public Franc(int amount)
        {
            Amount = amount;
        }

        public Franc Times(int multiplier)
        {
            return new Franc(Amount * multiplier);
        }

        public override bool Equals(object obj)
        {
            if (obj is Franc franc)
            {
                return Amount == franc.Amount;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
    }
}
