/*
    $5 USD + 10 CHF = $10 USD se a taxa é 2:1
    $5 * 2 = $10 USD
    Tornar "quantidade" privada.
    Efeitos colaterais em Dollar?
    Arredondamento de dinheiro?
    equals()
    hashCode()
    Igualdade de null
    Igualdade de objeto
    5 CHF * 2 = 10 CHF                               <- FOCO
    Duplicação de Dólar/Franco
    Igualdade comum
    Multiplicação comum
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