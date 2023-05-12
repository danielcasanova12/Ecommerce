//using Ecommerce.Repositories;
//using Ecommerce.Services;
//using System.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace Ecommerce
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            // Obtém a string de conexão do arquivo de configuração
//            var connectionString = ConfigurationManager.ConnectionStrings["NomeDaConnectionString"].ConnectionString;

//            // Configura o ServiceCollection
//            var services = new ServiceCollection();

//            services.AddTransient<IPedidoRepository>(provider => new PedidoRepository(connectionString));
//            services.AddTransient<IItemPedidoRepository>(provider => new ItemPedidoRepository(connectionString));
//            services.AddTransient<GerenciamentoDePedidos>();

//            var serviceProvider = services.BuildServiceProvider();

//            var gerenciamentoDePedidos = serviceProvider.GetService<GerenciamentoDePedidos>();
//        }
//    }
//}
using Ecommerce.Models;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ecommerce
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configura o ServiceCollection e obtém uma instância do GerenciamentoDePedidos
            var gerenciamentoDePedidos = ConfigurarGerenciamentoDePedidos();

            // Loop principal do menu
            while (true)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Criar um novo pedido");
                Console.WriteLine("2. Adicionar itens a um pedido");
                Console.WriteLine("3. Atualizar o status de um pedido");
                Console.WriteLine("4. Remover um pedido");
                Console.WriteLine("5. Listar pedidos por cliente, status ou data");
                Console.WriteLine("6. Calcular o valor total de um pedido");
                Console.WriteLine("0. Sair");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine("Digite o nome do cliente:");
                        var nomeCliente = Console.ReadLine();
                        var novoPedido = new Pedido(nomeCliente);
                        gerenciamentoDePedidos.CriarPedido(novoPedido);
                        break;
                    case "2":
                        Console.WriteLine("Digite o ID do pedido:");
                        var pedidoId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o ID do produto:");
                        Console.WriteLine("Digite o ID do produto:");
                        var produtoId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite a quantidade:");
                        var quantidade = int.Parse(Console.ReadLine());
                        gerenciamentoDePedidos.AdicionarItemAoPedido(pedidoId, produtoId, quantidade);
                        break;
                    case "3":
                        Console.WriteLine("Digite o ID do pedido:");
                        pedidoId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o novo status:");
                        var novoStatus = Console.ReadLine();
                        gerenciamentoDePedidos.AtualizarStatusDoPedido(pedidoId, novoStatus);
                        break;
                    case "4":
                        Console.WriteLine("Digite o ID do pedido:");
                        pedidoId = int.Parse(Console.ReadLine());
                        gerenciamentoDePedidos.RemoverPedido(pedidoId);
                        break;
                    case "5":
                        Console.WriteLine("Selecione uma opção:");
                        Console.WriteLine("1. Listar por cliente");
                        Console.WriteLine("2. Listar por status");
                        Console.WriteLine("3. Listar por data");
                        var opcaoListagem = Console.ReadLine();
                        switch (opcaoListagem)
                        {
                            case "1":
                                Console.WriteLine("Digite o nome do cliente:");
                                var cliente = Console.ReadLine();
                                gerenciamentoDePedidos.ListarPedidosPorCliente(cliente);
                                break;
                            case "2":
                                Console.WriteLine("Digite o status:");
                                var status = Console.ReadLine();
                                gerenciamentoDePedidos.ListarPedidosPorStatus(status);
                                break;
                            case "3":
                                Console.WriteLine("Digite a data inicial (yyyy-MM-dd):");
                                var dataInicial = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Digite a data final (yyyy-MM-dd):");
                                var dataFinal = DateTime.Parse(Console.ReadLine());
                                gerenciamentoDePedidos.ListarPedidosPorData(dataInicial, dataFinal);
                                break;
                            default:
                                Console.WriteLine("Opção inválida. Tente novamente.");
                                break;
                        }
                        break;
                    case "6":
                        Console.WriteLine("Digite o ID do pedido:");
                        pedidoId = int.Parse(Console.ReadLine());
                        gerenciamentoDePedidos.CalcularValorTotalDoPedido(pedidoId);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static GerenciamentoDePedidos ConfigurarGerenciamentoDePedidos()
        {
            // Obtém a string de conexão do arquivo de configuração
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Configura o ServiceCollection
            var services = new ServiceCollection();

            services.AddTransient<IPedidoRepository>(provider => new PedidoRepository(connectionString));
            services.AddTransient<IItemPedidoRepository>(provider => new ItemPedidoRepository(connectionString));
            services.AddTransient<GerenciamentoDePedidos>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<GerenciamentoDePedidos>();
        }
    }
}
