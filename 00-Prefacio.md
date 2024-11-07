# Prefácio

## Regras para conduzirmos o desenvolvimento com testes automatizados

- **Escrevemos código novo apenas quando um teste automatizado falha.**
- **Eliminamos duplicação.**

Isso implica que:

- Devemos projetar organicamente o código, executando-o e fornecendo feedback constantemente entre as decisões.
- Devemos escrever nossos próprios testes.
- Nosso ambiente de desenvolvimento deve fornecer respostas rápidas a pequenas mudanças.
- Nosso projeto deve consistir em muitos componentes altamente coesos e fracamente acoplados, tornando os testes mais fáceis.

Essas duas regras implicam em uma ordem para as tarefas de programação:

1. **Vermelho** – Escrever um pequeno teste que inicialmente não funcione e que talvez nem mesmo compile.
2. **Verde** – Fazer o teste funcionar rapidamente, mesmo cometendo alguns "pecados" necessários no processo.
3. **Refatorar** – Eliminar todas as duplicações criadas apenas para que o teste funcione.

### Uma analogia para testes no desenvolvimento:

> "Imagine que programar é como girar uma manivela para puxar um balde com água de um poço. Quando o balde é pequeno, uma manivela sem catraca é ótima. 
> 
> Quando o balde é grande e está cheio de água, você ficará cansado antes de puxá-lo completamente. Você precisará de um mecanismo de catracas para descansar entre as séries de maniveladas.
> 
> Quanto mais pesado for o balde, mais próximos os dentes da catraca precisam estar."

Uma vez que tenhamos um teste funcionando, sabemos que ele está funcionando agora e para sempre. Ao final deste livro, você será capaz de:

- Começar de forma simples.
- Escrever testes automatizados.
- Refatorar para adicionar uma decisão de projeto por vez.

## Este livro é organizado em três partes:

1. **Parte I: O Exemplo Financeiro** – permitirá que você aprenda a escrever testes antes de código e a desenvolver um projeto organicamente.
2. **Parte II: O Exemplo xUnit** – ensinará como trabalhar em passos ainda menores do que no primeiro exemplo.
3. **Parte III: Padrões para Desenvolvimento Guiado por Testes** – abordará padrões que auxiliam na decisão de qual teste escrever utilizando o xUnit.




