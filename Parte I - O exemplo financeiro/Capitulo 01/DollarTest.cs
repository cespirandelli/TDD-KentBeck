namespace Capitulo_1
{
    public class DollarTest
    {
        [Fact] //Atributo que marca este método como um teste de unidade
        public void TestMultiplication()
        {
            // Arrange: Cria uma instância da classe Dollar
            Dollar five = new Dollar(5);

            // Act: Chama o método Times, multiplicando o valor por 2
            five.Times(2);

            // Assert: Verifica se o valor de Amount é o esperado (10)
            Assert.Equal(10, five.Amount);
        }
    }
}