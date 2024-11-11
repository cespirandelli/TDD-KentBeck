/*
    $5 USD + 10 CHF = $10 USD se a taxa é 2:1
    $5 * 2 = $10 USD
    Tornar "quantidade" privada.
    Efeitos colaterais em Dollar?
    Arredondamento de dinheiro?
    equals()                                         <- FOCO
    hashCode()
    Igualdade de null
    Igualdade de objeto
    */

//(C#)
namespace Capitulo_3
{
    public class Dollar
    {
        public int Amount { get; private set; }

        public Dollar(int amount)
        {
            Amount = amount;
        }

        public Dollar Times(int multiplier)
        {
            return new Dollar(Amount * multiplier);
        }

        // Abaixo tenho a menor modificação para que o teste
        // seja verdadeiro ("verde").
        //public bool Equals(object obj)
        //{
        //    return true;
        //}

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