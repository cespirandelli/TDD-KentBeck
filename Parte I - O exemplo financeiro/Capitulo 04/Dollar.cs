/*
    $5 USD + 10 CHF = $10 USD se a taxa � 2:1
    $5 * 2 = $10 USD
    Tornar "quantidade" privada.                     <- FOCO
    Efeitos colaterais em Dollar?
    Arredondamento de dinheiro?
    equals()
    hashCode()
    Igualdade de null
    Igualdade de objeto
    */

//(C#)
namespace Capitulo_4
{
    public class Dollar
    {
        private int Amount { get; }

        public Dollar(int amount)
        {
            Amount = amount;
        }

        public Dollar Times(int multiplier)
        {
            return new Dollar(Amount * multiplier);
        }

        public override bool Equals(object obj)
        {
            if (obj is Dollar dollar)
            {
                return Amount == dollar.Amount;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
    }
}