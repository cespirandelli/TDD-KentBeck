namespace Capitulo_8
{
    public class Franc : Money
    {
        public Franc(int amount) : base(amount) { }

        public override Money Times(int multiplier)
        // A palavra-chave override, indica que esses métodos
        // estão substituindo o método abstrat em Money
        {
            return new Franc(Amount * multiplier);
        }
    }
}
