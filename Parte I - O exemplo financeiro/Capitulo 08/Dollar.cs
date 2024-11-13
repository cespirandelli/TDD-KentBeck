//(C#)
namespace Capitulo_8
{
    public class Dollar : Money
    {
        public Dollar(int amount) : base(amount) { }

        public override Money Times(int multiplier)
        // A palavra-chave override, indica que esses m�todos
        // est�o substituindo o m�todo abstrat em Money
        {
            return new Dollar(Amount * multiplier);
        }
    }
}