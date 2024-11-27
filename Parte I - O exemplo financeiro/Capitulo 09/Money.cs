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
    5 CHF * 2 = 10 CHF
    Duplicação de Dólar/Franco
    Igualdade comum
    Multiplicação comum
    Comparar Francos com Dólares
    Moeda?
    Deletar TestFrancMultiplication()?
    */

namespace Capitulo_9
{
    public abstract class Money
    {
        protected int Amount;
        protected string _currency;

        public Money(int amount)
        {
            Amount = amount;
        }

        public override bool Equals(object obj)
        {
            if (obj is Money money)
            {
                return Amount    == money.Amount
                    && GetType() == money.GetType();
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }

        public abstract Money Times(int multiplier);

        public string Currency()
        {
            return _currency;
        }

        public static Money Dollar(int amount)
        {
            return new Dollar(amount, "USD");
        }

        public static Money Franc(int amount)
        {
            return new Franc(amount, "CHF");
        }
    }
}
