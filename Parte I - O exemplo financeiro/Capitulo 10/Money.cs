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

namespace Capitulo_10
{
    public class Money
    {
        protected int _amount;
        protected string _currency;

        public Money(int amount, string currency)
        {
            _amount = amount;
            _currency = currency;
        }

        public override bool Equals(object obj)
        {
            if (obj is Money money)
            {
                return _amount == money._amount
                    && Currency().Equals(money.Currency());
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _amount.GetHashCode();
        }

        public virtual Money Times(int multiplier)
        {
            return new Money(_amount*multiplier, _currency);
        }

        public override string ToString()
        {
            // escolhi manter o dialeto escolhido pelo autor abaixo
            return _amount + "" + _currency;
            // com interpolação de string seria
            //return $"{Amount} {_currency}";
        }

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
