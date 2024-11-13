/*
    5 CHF * 2 = 10 CHF
    Duplicação de Dólar/Franco
    Igualdade comum                                  <- FOCO
    Multiplicação comum
 */

namespace Capitulo_7
{
    public class Franc : Money
    {
        public Franc(int amount) : base(amount) { }

        public Franc Times(int multiplier)
        {
            return new Franc(Amount * multiplier);
        }
    }
}
