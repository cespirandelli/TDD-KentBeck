using Xunit;

namespace Capitulo_2
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
    }
}