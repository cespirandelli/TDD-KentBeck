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

## Capítulo 3 - Igualdade para Todos

Quando você tem um número inteiro e soma 1 a ele, não espera que o valor original mude — você espera que o novo valor seja o resultado da soma. Objetos, no entanto, não se comportam dessa maneira.

Podemos usar objetos de forma semelhante a valores, como fizemos com nosso `Dollar`. O padrão para isso é o **Value Object**. Uma das suas restrições principais é que os valores das variáveis de instância do objeto **nunca** mudam depois de serem definidos no construtor.

### O que é um **Value Object** de forma bem simples?

Imagine que você tem uma **caixinha de valores**. Nessa caixinha, você coloca um dado que não precisa de um nome ou identificação, mas apenas de **dados dentro dela**. Quando duas dessas caixinhas contêm os **mesmos dados**, elas são **iguais**. E uma vez que você coloca algo dentro dessa caixinha, você **não pode mais mudar o que está dentro dela** — a caixinha é **imutável**.

### Um exemplo simples de **Value Object** em C#

Vamos criar um **Value Object** para representar uma **coisa simples**: uma **pessoa com idade**. O nome da pessoa não importa, só a **idade**. Se duas pessoas têm a mesma idade, elas são consideradas iguais, mesmo que sejam pessoas diferentes.

```
public class Idade
{
    public int Valor { get; }

    public Idade(int valor)
    {
        if (valor < 0)
            throw new ArgumentException("Idade não pode ser negativa.");
        Valor = valor;
    }

    // Compara apenas o valor (idade), não importa se são objetos diferentes.
    public override bool Equals(object obj)
    {
        if (obj is Idade outraIdade)
        {
            return this.Valor == outraIdade.Valor;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Valor.GetHashCode();  // Gerar um código único baseado no valor
    }
}
```

#### O que isso significa?

- **Imutabilidade**: Depois que a caixinha é criada com um valor (por exemplo, `10`), esse valor **não pode ser alterado**. Se a idade for `10`, ela será sempre `10`.
- **Igualdade**: Se você tem duas caixinhas, e ambas têm o valor `10`, elas são consideradas **iguais**, mesmo que sejam instâncias diferentes.
- **Sem Identidade**: Não importa quantas caixinhas com o valor `10` você tenha, todas elas serão iguais, porque **não têm identidade própria**, apenas o valor que carregam.

Agora, se você comparar duas instâncias de ```Idade``` com o valor `10`, elas serão **iguais**:

```
var idade1 = new Idade(10);

var idade2 = new Idade(10);

Console.WriteLine(idade1.Equals(idade2)); // Vai imprimir "True"`
```

Uma implicação do uso de **Value Object** é que todas as operações que modificam o valor devem retornar um **novo objeto**. Outra implicação é que o **Value Object deve implementar o método `Equals()`** para garantir que a comparação entre instâncias seja feita corretamente.

Se você utilizar o **`Dollar`** como chave em uma tabela hash, é essencial que você também implemente o método **`GetHashCode()`**, caso tenha implementado **`Equals()`**. Isso ocorre porque, em coleções como dicionários e tabelas hash, a consistência entre esses dois métodos é fundamental para o correto funcionamento da estrutura de dados.

Vamos criar um teste para checar igualdade (antes de implementar o método, o teste irá falhar):

```
public void TestEquality(){
    Assert.True(new Dollar(5).Equals(new Dollar(5)));
}
```

A implementação que devemos realizar para que retorne True mais simples seria:

```
public bool Equals(Object obj){
    return true;
}
```

#### Triangulação:

Para afirmarmos que "5 == 5", ou "quantidade == 5", ou "quantidade = Dollar.Amount". Temos que criar a comparação deste com outros número, por exemplo "5 != 6".

```
public void TestEquality()
{
    Assert.False(new Dollar(5).Equals(new Dollar(6)));
}
```

Logo, devemos implementar em ```Dollar```: 

```
public override bool Equals(object obj)
{
    if (obj is Dollar dollar) // Verifica se o objeto é do tipo Dollar
    {
        return Amount == dollar.Amount; // Compara os valores (Amount)
    }
    return false; // Retorna false caso o objeto não seja do tipo Dollar
}

public override int GetHashCode()
{
    return Amount.GetHashCode(); // Retorna o código de hash de Amount
}
```

##### Por que devemos implementar `Equals()` e `GetHashCode()` no `Dollar`?

Quando trabalhamos com objetos em C# e queremos compará-los, especialmente em coleções como listas, dicionários ou tabelas hash, é fundamental garantir que a comparação entre os objetos seja feita de maneira correta e consistente. Para isso, precisamos implementar os métodos `Equals()` e `GetHashCode()`. Vamos entender o motivo de sua implementação no contexto do seu código com a classe `Dollar` e o teste de igualdade.

1. **A importância do método `Equals()`**

O método `Equals()` é utilizado para comparar dois objetos e determinar se eles são **iguais em valor**. Se não implementarmos o método `Equals()`, o C# vai usar a implementação padrão da classe `object`, que compara se os objetos são **referências iguais**, ou seja, se são o **mesmo objeto na memória**, e não se possuem valores iguais.

**No caso do `Dollar`, a lógica de igualdade que queremos é baseada no valor do campo `Amount`**, não nas referências dos objetos. Ou seja, dois objetos `Dollar` com o mesmo valor (por exemplo, `Dollar(5)`) devem ser considerados **iguais**, independentemente de serem instâncias diferentes, localizadas em locais diferentes da memória.

2. **A importância do método `GetHashCode()`**

Quando implementamos `Equals()`, é fundamental implementar também o método `GetHashCode()`. Esse método é usado em **estruturas de dados baseadas em hash**, como o **Dictionary**, **HashSet**, e outras coleções que usam tabelas hash. 

O `GetHashCode()` é responsável por gerar um código único para cada objeto, baseado em seu valor. Esse código é utilizado para buscar o objeto de forma eficiente nas coleções que utilizam hashing.

Se dois objetos são considerados iguais pelo método `Equals()`, seus códigos de hash **também devem ser iguais**. Caso contrário, podemos ter problemas, como a duplicação de objetos em coleções baseadas em hash.

## Capítulo 4 - Privacidade

Agora que definimos a igualdade entre objetos, podemos usá-la para tornar nossos testes mais expressivos.

```
[Fact]
public void TestMultiplication()
{
    Dollar five = new Dollar(5);

    Dollar product = five.Times(2);
    Assert.Equal(10, product.Amount);


    product = five.Times(3);
    Assert.Equal(15, product.Amount);
}
```

Podemos reescrever o Assert para comparar Dollar com Dollar. Em vez de comparar diretamente os valores internos (`Amount`), podemos comparar objetos :

```
[Fact]
public void TestMultiplication()
{
    Dollar five = new Dollar(5);

    Dollar product = five.Times(2);
    Assert.Equal(new Dollar(10), product);


    product = five.Times(3);
    Assert.Equal(new Dollar(15), product);
}
```

Pode-se observar também que agora a variável product não é mais tão útil, podemos otimizá-la, tornando o teste mais direto e conciso:

```
[Fact]
public void TestMultiplication()
{
    Dollar five = new Dollar(5);

    Assert.Equal(new Dollar(10), five.Times(2));

    Assert.Equal(new Dollar(15), five.Times(3));
}
```

Com essa mudança, o teste se torna mais expressivo. Agora ele "fala" mais diretamente o que esperamos: "O valor de `five` multiplicado por 2 deve resultar em `Dollar(10)`", e assim por diante.

Com estas mudanças para o teste, Dollar é a única classe usando sua variável de instância `amount`, então podemos torná-la privada: ```private int Amount { get; }``` 

### **Por que tornar a variável `Amount` privada?**

Agora que o teste não acessa diretamente o valor de `Amount` (mas sim compara objetos `Dollar`), temos uma boa oportunidade de tornar o campo `Amount` privado. Isso oferece várias vantagens:

- **Encapsulamento**: O valor de `Amount` não é acessível diretamente de fora da classe. Ele é protegido e só pode ser manipulado dentro da classe.
- **Segurança**: A classe `Dollar` garante que seu valor não seja alterado inadvertidamente, uma vez que a variável `Amount` é **imutável** e privada.
- **Desacoplamento**: Os testes agora trabalham com o objeto completo, sem depender da estrutura interna de dados da classe. Isso promove uma maior flexibilidade e facilita alterações futuras na implementação sem impactar os testes.

## Capítulo 5 - Falando Franca-mente

Criamos um arquivo análogo ao de `Dollar`, agora para representar o **Franco Suíço** (Franc), seguindo os mesmos critérios utilizados na classe `Dollar`.

No entanto, **agora temos uma duplicação de código excessiva**. Para evitar a repetição e facilitar a manutenção, precisamos eliminar essa duplicação antes de prosseguir com a criação do próximo teste.

## Capítulo 6 - Igualdade para Todos, Restaurada

No capítulo anterior, acabamos duplicando muito código. Agora, vamos refatorar para eliminar essas duplicações. Para isso, ao invés de fazer uma das classes herdar a outra, vamos criar uma superclasse comum para ambas.

Nossa estratégia será criar uma classe `Money`, que centralize o código comum relacionado à comparação de valores, como a igualdade entre os objetos.

1. Criamos a superclasse `Money` apenas com seu "esqueleto" e a herdamos em `Dollar` e `Franc`.
   
   ```
   namespace Capitulo_6
   {
       public class Money
       {
   
       }
   }
   ```
   
   ```
   public class Dollar : Money
   {...}
   ```
   
   public class Franc : Money
   {...}

```
2. Rodamos todos os testes e verificamos que eles continuam funcionando normalmente.

- Agora podemos mover `private int Amount;` para a classe `Money`.

- Precisamos alterar de `private` para `protected`, para que as subclasses consigam acessá-la. Ao usar **`protected`**, a propriedade fica acessível dentro da classe base **`Money`** e também nas classes derivadas (como `Dollar` e `Franc`), mas não diretamente fora dessas classes.

- ```
  public class Money
  {
      protected int Amount { get; }
  }
```

3- Precisamos adicionar a declaração de `Amount` em `Money` para que ela possa ser acessada pelas subclasses em C#. Em seguida, vamos lidar com o método `Equals`. Os testes continuam funcionando normalmente.

```
public class Money
{
    protected int Amount;

    public Money(int amount)
    {
        Amount = amount;
    }

}
```

    Agora podemos alterar as subclasses:

```
public Dollar(int amount) : base(amount) { }

public Franc(int amount) : base(amount) { }
```

4. Agora vamos transformar o método `Equals` para movê-lo para a superclasse `Money`, e em seguida remover a redundância nas suas subclasses. Os testes continuam funcionando normalmente.
   
   ```
   public class Money
   {
       protected int Amount;
   
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
   ```

```

```

   public class Dollar : Money
   {
       public Dollar(int amount) : base(amount) { }

       public Dollar Times(int multiplier)
       {
           return new Dollar(Amount * multiplier);
       }

   }

   {...}

   public class Franc : Money
   {
       public Franc(int amount) : base(amount) { }

       public Franc Times(int multiplier)
       {
           return new Franc(Amount * multiplier);
    
       }

   }

```
#### Comparação de `Franc(5)` e `Dollar(5)`

Adicionei um teste que compara um objeto `Franc(5)` com um objeto `Dollar(5)`, e o teste passou. No entanto, o motivo do teste passar é que a comparação está sendo feita **somente pelos valores** (`Amount`) e **não pelos tipos** dos objetos. 

#### Problema:

Embora os valores de `Amount` sejam iguais, isso é **tecnicamente errado**, pois `Dollar` e `Franc` são **tipos** diferentes, representando moedas distintas, e deveriam ser considerados **objetos distintos**,

#### O que será feito mais à frente:

Para corrigir esse comportamento, precisaremos adicionar uma comparação do **tipo dos objetos** utilizando o método `GetType()`. Dessa forma, vamos garantir que dois objetos de tipos diferentes (por exemplo, `Dollar` e `Franc`) não sejam considerados iguais, mesmo que tenham o mesmo valor.



## Capítulo 7 - Maças e Laranjas

 Este teste deveria passar, mas ele falha:

`Assert.False(new Dollar(5).Equals(new Franc(5)));`

Isso ocorre devido ao **tipo**, que, no caso, está sendo considerado igual na nossa comparação, o que é incorreto. **Dólares não são iguais a Francos**. Logo, precisamos alterar a lógica de comparação:
```

public override bool Equals(object obj)
{
    if (obj is Money money)
    {
        return Amount == money.Amount && GetType() == money.GetType();
    }
    return false;
}

```
> "Usar as classes dessa forma no código modelo é um pouco 'mau cheiroso'.
> 
> Gostaríamos de usar um critério que fizesse sentido no domínio das finanças, não no domínio de objetos Java. 
> 
> Mas, não temos atualmente nada como uma moeda, e isso não parece ser razão suficiente para introduzir uma, então isso terá que ficar assim por enquanto."



Até agora fizemos:

1. Pegamos uma dúvida que nos incomodava e a transformamos em um teste.

2. Fizemos o teste rodar de forma razoável, mas não perfeita, utilizando `GetType()`.

3. Decidimos não introduzir mais projetos até termos uma motivação mais clara para isso.



## Capítulo 8 - Fazendo Objetos

Agora, vamos alterar — e até remover — o método `Times` das classes `Dollar` e `Franc` para movê-lo para a classe `Money`, já que suas implementações são extremamente semelhantes.
```

public Money Times(int multiplier)
{
    return new Dollar(Amount * multiplier);
}

{...}

public Money Times(int multiplier)
{
    return new Franc(Amount * multiplier);
}

```
> "O próximo passo não é tão óbvio. Ambas as subclasses de Money não estáo fazendo o suficiente para justificar sua existência, logo, gostaríamos de elimiá-las. Mas, não podemos fazer isso em um grande passo, pois não seria uma demonstração muito eficaz de TDD." 



Para isso vamos alterar nosso teste e sua implementação:
```

public void TestMultiplication()
{
    Dollar five = Money.Dollar(5);
    Assert.Equal(new Dollar(10), five.Times(2));
    Assert.Equal(new Dollar(15), five.Times(3));
}

```
Implementação em Dollar e Franc:
```

static Dollar dollar(int amount)
{
    return new Dollar(amount);
}

{...}

static Franc franc(int amount)
{
    return new Franc(amount);
}

```
Ainda não removemos as referências à `Dollar`, por isso precisamos alterar a declaração no teste novamente:
```

public void TestMultiplication()
{
    Money five = Money.Dollar(5);
    Assert.Equal(new Dollar(10), five.Times(2));
    Assert.Equal(new Dollar(15), five.Times(3));
}

```
Note que o compilador está reclamando que o método `Times()` não está implementado em `Money`. Portanto, precisamos resolver isso rapidamente para que os testes rodem novamente, pois atualmente não conseguimos nem realizar o build.

### Vamos fazer algumas alterações em `Money`e suas subclasses:

Vamos tornar `Money` uma classe **abstrata**.

1. **Por que a classe `Money` precisa ser abstrata?**

   - Porque iremos implementar um método abstrato.

2. **Métodos abstratos não podem ser implementados diretamente** na classe base. Portanto, para ter o método `Times` como abstrato, **precisamos** marcar a classe base `Money` como `abstract`.

   - Isso é necessário para que o compilador entenda que o método `Times` é um "contrato" que deve ser implementado pelas classes filhas (como `Dollar` e `Franc`).

3. **Classe abstrata como base**: A classe `Money` deve ser abstrata porque ela não pode ser instanciada diretamente. Porém, queremos que as subclasses concretas (como `Dollar`, `Franc`, etc.) implementem o comportamento específico. A classe `Money` define a estrutura comum e o comportamento genérico (como `Equals`, `GetHashCode`), mas o comportamento de multiplicação (`Times`) deve ser implementado de forma diferente para cada tipo de moeda.

### Agora podemos mudar a declaração do método fábrica e testar:

`Money`

```Money
{...}

public static Money Dollar(int amount)
{
    return new Dollar(amount);
}

public static Money Franc(int amount)
{
    return new Franc(amount);
}
```

**Após definido como `public` para que fique visível para o restante de nosso sistema**, podemos prosseguir e fazer as alterações nos nossos testes para utilizar o método fábrica.

```
[Fact]
public void TestMultiplication()
{
    Money five = Money.Dollar(5);
    Assert.Equal(Money.Dollar(10), five.Times(2));
    Assert.Equal(Money.Dollar(15), five.Times(3));
}

[Fact]
public void TestFrancMultiplication()
{
    Money five = Money.Franc(5);
    Assert.Equal(Money.Franc(10), five.Times(2));
    Assert.Equal(Money.Franc(15), five.Times(3));
}

[Fact]
public void TestEquality()
{
    Assert.True(Money.Dollar(5).Equals(Money.Dollar(5)));
    Assert.False(Money.Dollar(5).Equals(Money.Dollar(6)));

    Assert.True(Money.Franc(5).Equals(Money.Franc(5)));
    Assert.False(Money.Franc(5).Equals(Money.Franc(6)));

    Assert.False(Money.Dollar(5).Equals(Money.Franc(5)));
}
```

**Todos os testes passaram!**

**Vamos revisar os pontos principais:**

1. **Eliminamos a duplicação** ao conciliar as assinaturas das duas variantes do método `Times()`.

2. **Movemos pelo menos uma declaração de método** para uma superclasse comum, centralizando a lógica compartilhada.

3. **Desacoplamos o código de teste** da existência de subclasses concretas por meio da introdução de métodos fábrica.

4. **Percebemos que, quando as subclasses desaparecerem**, alguns testes se tornarão redundantes. Contudo, decidimos não tomar providências imediatas sobre isso.

## Capítulo 9 - Tempos em que Estamos Vivendo

> "Como queremos testar moedas no momento?"

Vamos tentar reproduzir uma moeda apenas pelo uso do retorno de um string, como se o objeto nos avisasse qual é a moeda que estamos lidando. Primeiro vamos criar o teste:

```
[Fact]
public void TestCurrency()
{
    Assert.Equal("USD", Money.Dollar(1).Currency());
    Assert.Equal("CHF", Money.Franc(1).Currency());
}
```

Depois declaramos Currency em Money, só então implementamos nas subclasses.

```
Money
public abstract string Currency();
```

```
Dolar
public override string Currency()
{
    return "USD";
} 

{...}

Franc
public override string Currency()
{
    return "CHF";
} 
```

Como queremos que a mesma implementação seja suficiente em ambas as subclasses, podemos armazenar a moeda em uma variável de instância e retornar a variável.

```
Money
protected string _currency;
```

```
Dollar
public Dollar(int amount) : base(amount)
{
    _currency = "USD";
}

public string Currency()
{
    return _currency;
}

{...}

Franc
public Franc(int amount) : base(amount)
{
    _currency = "CHF";
}

public string Currency()
{
    return _currency;
}
```

Agora podemos subir a declaração do método para `Money` pois ambas implementações são idênticas e removemos das subclasses.

```
Money
protected string _currency;

{...}
public string Currency()
{
    return _currency;
}
{...}
```

em seguida podemos simplificar:

```
Dollar
public class Dollar : Money
{

    public Dollar(int amount, string currency) : base(amount)
    {
        _currency = currency;
    }

    public override Money Times(int multiplier)
    {
        return Money.Dollar(Amount * multiplier);
    }
}

{}


public class Franc : Money
{

    public Franc(int amount, string currency) : base(amount)
    {
        _currency = currency;
    }

    public override Money Times(int multiplier)
    {
        return Money.Franc(Amount * multiplier);
    }

}
```

```
Money
{...}
public static Money Dollar(int amount)
{
    return new Dollar(amount, "USD");
}

public static Money Franc(int amount)
{
    return new Franc(amount, "CHF");
}
```

Assim, teremos a passagem da string toda vez que um novo objeto for instânciado.



### Revisão:

- Conciliamos dois coinstrutores, movendo a variação para o chamador (método fábrica) .

- Realizamos uma refatoralçai análoga (fazendo em Dollar o que já fizemos em Franc).

- Subimos dois construtores idênticos.



 

## Capítulo 10 - Tempos interessantes (Interesting Times)

Ao terminar este capítulo teremos apenas umas classe para representar `Money`. Detalhe, as duas implementações de `times()` são parecidas, mas não idênticas.

```
Franc
public override Money Times(int multiplier)
{
    return Money.Franc(Amount * multiplier);
}

{...}

Dollar
public override Money Times(int multiplier)
{
    return Money.Dollar(Amount * multiplier);
}
```

O que podemos fazer para otimizar nossos métodos fábrica?

```
Franc
public override Money Times(int multiplier)
{
    //return Money.Franc(Amount * multiplier);
    return new Franc(Amount * multiplier, _currency);
}

{...}

Dollar
public override Money Times(int multiplier)
{
    //return Money.Dollar(Amount * multiplier);
    return new Dollar(Amount * multiplier, _currency);
}
```

Em `Money` ficará então:

```
public static Money Dollar(int amount)
{
    return new Dollar(amount, "USD");
}

public static Money Franc(int amount)
{
    return new Franc(amount, "CHF");
}
```

 Logo iremos lidar com a implantação do método Equals, pois ela está comparando o tipo e não a moeda em si das classes. Mas antes criaremos o teste, para isso Money não poderá mais ser abstrata.

```
public Money(int amount, string currency)
{
    _amount = amount;
    _currency = currency;
}

public override bool Equals(object obj)
{
    if (obj is Money money)
    {
        return _amount == money._amount
            && Currency().Equals(money.Currency());
    }
    return false;
}
```

Desta forma conseguimos criar o seguinte teste, que irá passar:

```
Test
[Fact]
public void TestDifferentClassEquality()
{
    Assert.True(new Money(10, "CHF").Equals(new Franc(10, "CHF")));
}
```

Logo, o método `times()` agora pode ser alterado para construir Money, agora sim idênticos:

```
Franc e Dollar
public override Money Times(int multiplier)
{
    return new Money(_amount * multiplier, _currency);
}
```

Portanto podemos remover das subclasses e inserir em Money:

```
public virtual Money Times(int multiplier)
{
    return new Money(_amount*multiplier, _currency);
}
```

### Revisão:

- Conciliamos dois métodos - `times()` - pela otimização dos métodos que eles chamavam e pela substituição de constantes por variáveis.

- Escrevemos `ToString()` sem um teste apenas para nos ajudas a depurar.

- Tentamos uma mudança (Retornar `Money` ao invés de `Franc`) e deixamos os testes nos dizerem se funcionou.

- Retrocedemos um experimento e escrevemos outro teste. Fazer o teste funcionar fez o experimento funcionar.