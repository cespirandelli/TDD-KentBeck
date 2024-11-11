/*
    $5 USD + 10 CHF = $10 USD se a taxa � 2:1
    $5 * 2 = $10 USD
    Tornar "quantidade" privada.
    Efeitos colaterais em Dollar?
    Arredondamento de dinheiro?
    equals()
    hashCode()
    Igualdade de null
    Igualdade de objeto
    5 CHF * 2 = 10 CHF                               <- FOCO
    Duplica��o de D�lar/Franco
    Igualdade comum
    Multiplica��o comum
    */

//(C#)
namespace Capitulo_5
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