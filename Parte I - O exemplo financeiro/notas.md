# Introdução

### Método, Motivo e Oportunidade.

É uma maneira de lembrar a razão pela qual você está tomando certas decisões durante o ciclo de desenvolvimento, garantindo que o código seja bem estruturado e que a abordagem de testes seja adequada.

1. **Método**:
- O **método** refere-se à **abordagem técnica** e ao **processo** que você escolhe para implementar algo no seu código.

- No caso do TDD, o método pode envolver o ciclo de "Vermelho, Verde, Refatorar", que é o procedimento sistemático a ser seguido para escrever testes antes do código, e, em seguida, refatorar o código para melhorar sua qualidade.

- Isso também envolve a escolha de como escrever os testes (como usá-los para cobrir os casos de borda (edge cases), testar falhas e garantir o comportamento esperado do sistema).
2. **Motivo**:
- O **motivo** está relacionado à **razão** pela qual você está escrevendo aquele teste ou fazendo determinada mudança no código.

- O motivo por trás de escrever o teste antes do código é **garantir que você está escrevendo apenas o código necessário** para passar no teste, evitando a tentação de "over-engineering" (ou "excesso de engenharia") ou de escrever código desnecessário.

- O motivo também é garantir que os testes cubram o comportamento desejado e assegurem que você não quebre funcionalidades existentes ao implementar novas.
3. **Oportunidade**:
- A **oportunidade** se refere à **chance de melhorar** o código ou o sistema. Uma oportunidade é quando você refatora o código para melhorar sua legibilidade, eficiência ou design sem alterar seu comportamento.

- Durante o ciclo de TDD, você ganha oportunidades para melhorar a estrutura do código cada vez que refatora, ou seja, quando você elimina duplicação, melhora a legibilidade ou reorganiza a lógica para torná-la mais clara e sustentável.

# Parte I - O exemplo financeiro

1. Adicionar um teste rapidamente.

2. Rodar todos os testes e ver o mais novo falhando.

3. Fazer uma pequena mudança.

4. Rodar todos os testes e ver todos funcionando.

5. Refatorar para remover duplicações.

**As prováveis surpresas incluem:**

- Como cada teste pode cobrir um pequeno aumento de funcionalidade.

- Quão pequenas e "feias" as mudanças podem ser para fazer os novos testes rodarem.

- Com que frequência os testes são executados.

- De quantos pequenos passos as refatorações são compostas.

## Capítulo 1 - Dinheiro Multi-Moeda

### Relatório inicial (em uma única moeda - USD):

| Instrumento | Ações | Preço     | Total          |
| ----------- | ----- | --------- | -------------- |
| IBM         | 1.000 | 25 USD    | 25.000 USD     |
| GE          | 400   | 100 USD   | 40.000 USD     |
|             |       | **Total** | **65.000 USD** |

### Relatório com múltiplas moedas:

Para um relatório multi-moeda, os preços precisam incluir as respectivas moedas:

| Instrumento | Ações | Preço     | Total          |
| ----------- | ----- | --------- | -------------- |
| IBM         | 1.000 | 25 USD    | 25.000 USD     |
| Novartis    | 400   | 150 CHF   | 60.000 CHF     |
|             |       | **Total** | **65.000 USD** |

### Taxa de câmbio:

| De  | Para | Taxa |
| --- | ---- | ---- |
| CHF | USD  | 1,5  |

#### Como a conversão funciona:

Para o relatório final, precisamos garantir que todas as quantias sejam convertidas para uma moeda comum, o USD.

Exemplo de cálculo:
`5 USD + 10 CHF = 10 USD` se a taxa de câmbio for 2:1.

### Comportamento necessário para calcular o relatório revisado:

1. **Somar valores em diferentes moedas**:
- Precisamos ser capazes de somar valores em diferentes moedas. Para isso, ao somarmos os totais, cada valor precisa ser convertido para uma moeda comum usando as taxas de câmbio fornecidas.

Exemplo:

- Se tivermos `60.000 CHF` e quisermos somá-los com `25.000 USD`, precisamos primeiro converter os CHF para USD, multiplicando o valor por 1,5 (usando a taxa de câmbio fornecida). Após a conversão, somamos os dois valores na moeda comum.
2. **Multiplicar valor por quantidade**:
- Para cada linha da tabela, precisamos multiplicar o número de ações pelo preço de cada ação.

Exemplo:

- Se a ação da IBM custa 25 USD e temos 1.000 ações, o total será `1.000 * 25 USD = 25.000 USD`. Para Novartis, se a ação custa 150 CHF e temos 400 ações, o total será `400 * 150 CHF = 60.000 CHF`. Após essa multiplicação, o valor precisa ser convertido para a moeda comum.

### Quais comportamentos precisamos garantir para gerar o relatório revisado corretamente?

1. **Conversão de moedas**:
   
   - Precisamos ser capazes de **somar** valores que estão em diferentes moedas. Isso exige a conversão dos valores usando a taxa de câmbio fornecida, antes de somá-los.

2. **Cálculo correto de totais**:
   
   - Precisamos ser capazes de **multiplicar** o valor do preço por ação pelo número de ações e calcular corretamente o total para cada instrumento. O valor total de cada instrumento deve ser calculado com base na moeda original e, se necessário, convertido para a moeda comum (USD).

3. **Testes necessários**:
   
   - **Testes de conversão de moedas**: Verificar se a soma dos valores em diferentes moedas está sendo realizada corretamente, usando as taxas de câmbio.
   - **Testes de multiplicação**: Verificar se o cálculo do total por instrumento está correto antes e depois de converter os valores para a moeda comum (USD).

Quando todos esses testes passarem, teremos confiança de que o código pode calcular corretamente o relatório revisado.

#### Passo 1: Criar um Teste (com erros de compilação)

##### Teste Inicial (C#):

using Xunit;

public class DollarTest
{
    [Fact]
    public void TestMultiplication()
    {
        Dollar five = new Dollar(5);           // Estamos tentando criar um objeto 'Dollar'
        five.Times(2);                                  // Chamando o método 'Times' no objeto
        Assert.Equal(10, five.Amount);    // Verificando se o valor final é 10
    }
}

**Problemas iniciais:**

- A classe `Dollar` não existe.

- Não existe um construtor `Dollar(int amount)`.

- Não existe o método `Times(int multiplier)`.

- Não existe o campo `Amount` (ou um getter público).

Agora, vamos corrigir esses problemas um a um.

#### Passo 2: Corrigir os Erros de Compilação (Adicionar Implementações Mínimas)

##### a) Criar a classe `Dollar`

Vamos começar com uma implementação básica da classe `Dollar`, apenas para que o teste compile. 

public class Dollar {

    public int Amount;  

    public Dollar(int amount) {
         Amount = amount;

    }

    public void Times(int multiplier) {
         Amount = Amount * multiplier;
    }

}

Agora o código **compilará** e, ao rodar o teste, veremos que o teste falha porque estamos tentando acessar `Amount`, mas o valor nunca foi modificado, então o teste falhará (deve retornar `0`, não `10`).

#### Passo 3: Fazer a Menor Mudança Possível Para Passar o Teste

Para corrigir isso de forma mínima, basta **atribuir o valor de `Amount` diretamente** para que o teste passe:

public class Dollar {
     public int Amount;
     

    public Dollar(int amount) {
         Amount = amount;

    }
     public void Times(int multiplier) {
         Amount = 10;

    }
}

Agora o teste vai passar, pois `Amount` será igual a `10`, como o teste espera. 

#### Passo 4: Rodar o Teste e Ver Todos Funcionando

Neste momento, o código está funcionando, mas sabemos que **não é uma solução geral**. O código apenas faz o teste passar de forma pontual, e não está **generalizado** para outros casos de multiplicação.

#### Passo 5: Refatoração para Remover Duplicações e Generalizar

Agora que o teste está funcionando, podemos **remover duplicações** e **generalizar o código** para torná-lo mais flexível.

#### Refatoração:

1. Vamos remover o valor fixo `10` e fazer com que a multiplicação realmente funcione com base no valor inicial.

public class Dollar {
     public int Amount;

    public Dollar(int amount) {
         Amount = amount;

    }
     public void Times(int multiplier) {
         Amount = Amount * multiplier; 

    }
}

2. Vamos simplificar ao máximo em seguida:

```
public class Dollar
 {
 public int Amount { get; private set; }

    public Dollar(int amount)
    {
        Amount = amount;
    }

    public void Times(int multiplier)
    {
        Amount *= multiplier;
        //Forma reduzida de "Amount = amount * multiplier"
    }
}
```

1. **Adicionar um teste rapidamente**: Criamos o teste simples `TestMultiplication`, que falhou inicialmente.
2. **Rodar todos os testes e ver o mais novo falhando**: O teste falhou porque a classe `Dollar` e os métodos necessários ainda não existiam.
3. **Fazer uma pequena mudança**: Implementamos a classe `Dollar`, o construtor e o método `Times`, atribuindo diretamente `Amount = 10` para fazer o teste passar.
4. **Rodar todos os testes e ver todos funcionando**: O teste passou, mas a solução não era genérica.
5. **Refatorar para remover duplicações**: Refatoramos o código para multiplicar corretamente o valor de `Amount` usando o método `Times(int multiplier)`.

**Nota:**

Este processo de desenvolvimento baseado em **Test-Driven Development (TDD)**, que envolve a criação de testes, execução, correção mínima e refatoração, foi mais bem minuciosa no **Capítulo 1**. Embora este ciclo tenha sido abordado de forma detalhada inicialmente, ele será resolvido de maneira implícita nos próximos arquivos, à medida que continuamos a implementar e refatorar o código, mantendo o foco na evolução dos exemplos dados no livro de maneira incremental e controlada.

## Capítulo 2 - Degenerar Objetos

Agora queremos realizar este teste:

```
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
```

Se testar neste momento, irá falhar, resultará em 30. Temos que alterar para continuar multiplicando corretamente. Devemos alterar a classe Dollar para poder viabilizar esta operação.

**Alteramos deste:**

```
publicvoiTimes(int multiplier)
{
 Amount *= multiplier;
}
```

**para:** 

```
public Dollar Times(int multiplier)
{
 return new Dollar(Amount * multiplier);
}
```

#### Como funciona:

- Este método cria **um novo objeto `Dollar`** e passa como argumento para o construtor o valor da multiplicação (`Amount * multiplier`).
- O operador `*` realiza a multiplicação e retorna o resultado dessa operação, sem alterar o valor de `Amount` no objeto original. O valor de `Amount` permanece o mesmo no objeto `five`.
- O novo objeto `Dollar` criado é retornado com o valor correto (o valor multiplicado de `Amount`), e o objeto original (`five`) não é modificado.

Esse comportamento é necessário para garantir que cada operação de multiplicação resulte em um novo objeto `Dollar`, sem alterar o valor da instância original. No exemplo acima, o primeiro teste de multiplicação resulta em `10 USD`, e o segundo em `15 USD`, mas o objeto original `five` permanece intacto, com seu valor inicial de `5 USD`.

### Duas Estratégias para Chegar ao "Verde" Rápidamente

- **Enganar o sistema**: Retorne uma constante e gradualmente substitua as constantes por variáveis até ter o código real.

- **Usar Implementação Óbvia**: Codifique a implementação real de forma direta, mas com a menor quantidade possível de código.

#### Para revisar:

1. **Traduzimos um obstáculo de design (efeitos colaterais) em um caso de teste que falhou devido a esse obstáculo**: No início, o problema estava em como o valor da variável `Amount` era alterado diretamente no método `Times(int multiplier)`, o que causava efeitos colaterais indesejados.

2. **Fizemos o teste funcionar codificando o que parecia ser o código correto**: Refatoramos o código para tratar da multiplicação corretamente, criando um novo objeto a cada chamada ao método `Times`.

3. **Obtivemos o código compilando rapidamente com uma implementação de stub**: Começamos com uma implementação mínima que fazia o teste passar, mas com um código muito específico e não generalizado.

4. **Refatoramos para remover duplicações e generalizar o código**: Finalmente, refatoramos a implementação para torná-la mais flexível, retornando um novo objeto `Dollar` a cada multiplicação e mantendo o comportamento esperado.



A tradução de um sentimento (como a aversão a efeitos colaterais) para um teste (como, por exemplo, garantir que multiplicar o mesmo objeto `Dollar` duas vezes produza o resultado esperado) é uma prática comum em TDD. 

Quanto mais eu praticar esse processo, mais apto estarei a traduzir meus julgamentos e intenções de design em testes que validem o comportamento desejado.

Após determinar qual é o comportamento correto, podemos então discutir a melhor forma de implementá-lo de maneira eficaz e eficiente.