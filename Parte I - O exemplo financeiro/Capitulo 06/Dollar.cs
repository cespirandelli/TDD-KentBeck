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
    5 CHF * 2 = 10 CHF
    Duplica��o de D�lar/Franco
    Igualdade comum                                  <- FOCO
    Multiplica��o comum
    */

//(C#)
namespace Capitulo_6
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