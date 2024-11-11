namespace Capitulo_4
{
    public class DollarTest
    {
        [Fact]
        public void TestMultiplication()
        {
            Dollar five = new Dollar(5);
            Assert.Equal(new Dollar(10), five.Times(2));
            Assert.Equal(new Dollar(15), five.Times(3));
        }

        [Fact]
        public void TestEquality()
        {
            Assert.True(new Dollar(5).Equals(new Dollar(5)));
            Assert.True(new Dollar(6).Equals(new Dollar(6)));
            Assert.False(new Dollar(5).Equals(new Dollar(6)));
        }
    }
}