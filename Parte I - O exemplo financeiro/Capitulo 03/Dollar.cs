/*
Considere o seguinte teste:

(java)
public void testMultiplication(){
    Dollar five = new Dollar(5);
    five.times(2);
    assertEquals(10, five.amount);
}

    $5 USD + 10 CHF = $10 USD se a taxa é 2:1
    $5 * 2 = $10 USD
    Tornar "quantidade" privada.
    Efeitos colaterais em Dollar?                <- FOCO
    Arredondamento de dinheiro?
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
    }
}