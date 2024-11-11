using Xunit;

namespace Capitulo_3
{
    public class DollarTest
    {
        [Fact]
        public void TestMultiplication()
        {
            Dollar five = new Dollar(5);

            Dollar product = five.Times(2);
            Assert.Equal(10, product.Amount);


            product = five.Times(3);
            Assert.Equal(15, product.Amount);
        }

        [Fact]
        public void TestEquality()
        {
            // A igualdade de objetos do tipo Dollar é determinada pela comparação dos valores de "Amount".
            // Se os valores (Amount) forem iguais, os objetos são considerados iguais.
            Assert.True(new Dollar(5).Equals(new Dollar(5)));   // Deve ser 'True'
            Assert.True(new Dollar(6).Equals(new Dollar(6)));   // Deve ser 'True'
            Assert.False(new Dollar(5).Equals(new Dollar(6)));  // Deve ser 'False'
        }
    }
}