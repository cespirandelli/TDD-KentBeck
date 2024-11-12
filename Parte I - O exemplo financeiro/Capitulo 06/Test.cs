namespace Capitulo_6
{
    public class Test
    {
        [Fact]
        public void TestMultiplication()
        {
            Dollar five = new Dollar(5);
            Assert.Equal(new Dollar(10), five.Times(2));
            Assert.Equal(new Dollar(15), five.Times(3));
        }

        [Fact]
        public void TestFrancMultiplication()
        {
            Franc five = new Franc(5);
            Assert.Equal(new Franc(10), five.Times(2));
            Assert.Equal(new Franc(15), five.Times(3));
        }

        [Fact]
        public void TestEquality()
        {
            Assert.True(new Dollar(5).Equals(new Dollar(5)));
            Assert.False(new Dollar(5).Equals(new Dollar(6)));

            Assert.True(new Franc(5).Equals(new Franc(5)));
            Assert.False(new Franc(5).Equals(new Franc(6)));

            Assert.True(new Dollar(5).Equals(new Franc(5)));
            // Sem a comparação do tipo, a igualdade é determinada apenas pelo
            // valor de Amount, o que faz com que uma instância de Dollar e uma
            // de Franc com o mesmo valor sejam consideradas iguais.
            // Ao adicionar GetType(), será possível garantir que objetos de tipos
            // não sejam considerados iguais, mesmo que o valor de Amount seja o mesmo.
        }
    }
}