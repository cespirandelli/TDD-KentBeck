namespace Capitulo_6
{
    public class Money
    {
        protected int Amount;
        // Optei por usar um campo (field) em vez de uma propriedade (property)
        // para simplificar o código, já que as propriedades em C# adicionam
        // uma camada extra de complexidade. Embora as propriedades ofereçam
        // mais conveniência e controle (como getters e setters), neste caso,
        // um campo direto é suficiente e mais alinhado com a abordagem mais
        // simples, como visto no exemplo Java do livro.

        public Money(int amount)
        {
            Amount = amount;
        }

        public override bool Equals(object obj)
        {
            if (obj is Money money)
            {
                return Amount == money.Amount;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
    }
}
