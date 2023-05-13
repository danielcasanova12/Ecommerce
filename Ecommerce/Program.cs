﻿//using System;
//using Ecommerce.Models;
//using Ecommerce.Repositories;
//using System.Configuration;
//using System.Data.SqlClient;
//using Ecommerce.Services;
//using MySql.Data.MySqlClient;

//class Program
//{
//    static void Main(string[] args)
//    {

//        var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
//        string query = "SELECT * FROM tb_usuario";
//        var gerenciamentoDePedidos = new GerenciamentoDePedidos(new PedidoRepository(connectionString), new ItemPedidoRepository(connectionString));
//        using (MySqlConnection connection = new MySqlConnection(connectionString))
//        {
//            MySqlCommand command = new MySqlCommand(query, connection);
//            connection.Open();

//            MySqlDataReader reader = command.ExecuteReader();

//            while (reader.Read())
//            {
//                Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
//            }

//            reader.Close();

//        }
//        var novoPedido = new Pedido
//        {
//            Id = 1,
//            Cliente = "SAD",
//            Status = "SADASD",
//            DataPedido = DateTime.Now,
//            Itens = new List<ItemPedido>()
//        };
//        gerenciamentoDePedidos.CriarPedido(novoPedido);
//    }
//}


using Ecommerce.Models;
using Ecommerce.Repositories;
using Ecommerce.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace Ecommerce
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Configura o ServiceCollection
            var services = new ServiceCollection();

            services.AddTransient<IPedidoRepository>(provider => new PedidoRepository(connectionString));
            services.AddTransient<IItemPedidoRepository>(provider => new ItemPedidoRepository(connectionString));
            services.AddTransient<GerenciamentoDePedidos>();

            var serviceProvider = services.BuildServiceProvider();
            var gerenciamentoDePedidos =  serviceProvider.GetService<GerenciamentoDePedidos>();
            // Configura o ServiceCollection e obtém uma instância do GerenciamentoDePedidos
           

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
                        var novoPedido = new Pedido
                        {
                            Cliente = "SAD",
                            Status = "SADASD",
                            DataPedido = DateTime.Now,
                            Itens = new List<ItemPedido>()
                        };
                        gerenciamentoDePedidos.CriarPedido(novoPedido);
                        break;
                    case "2":
                        Console.WriteLine("Digite o ID do pedido:");
                        var pedidoId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o ID do produto:");
                        var produtoId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite a quantidade:");
                        var quantidade = int.Parse(Console.ReadLine());

                    //    var itemPedido = new ItemPedido
                      //  {
                           // Produto = pedidoId,
                       //     Quantidade = quantidade,    
                          //  PrecoUnitario = 55
                       // }
                        //gerenciamentoDePedidos.AdicionarItemAoPedido(itemPedido);
                        break;
                    case "3":
                        Console.WriteLine("Digite o ID do pedido:");
                        pedidoId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o novo status:");
                        var novoStatus = Console.ReadLine();
                        gerenciamentoDePedidos.AtualizarStatusPedido(pedidoId, novoStatus);
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
                                var lista = gerenciamentoDePedidos.ListarPedidosPorCliente(cliente);
                                foreach (var item in lista)
                                {
                                    Console.WriteLine(item.Id);
                                    Console.WriteLine(item.Cliente);
                                }
                                break;
                            case "2":
                                Console.WriteLine("Digite o status:");
                                var status = Console.ReadLine();
                                 lista = gerenciamentoDePedidos.ListarPedidosPorStatus(status);
                                foreach (var item in lista)
                                {
                                    Console.WriteLine(item.Id);
                                    Console.WriteLine(item.Cliente);
                                }
                                break;
                            case "3":
                                Console.WriteLine("Digite a data inicial (yyyy-MM-dd):");
                                var dataInicial = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Digite a data final (yyyy-MM-dd):");
                                var dataFinal = DateTime.Parse(Console.ReadLine());
                                 lista = gerenciamentoDePedidos.ListarPedidosPorData(dataInicial, dataFinal);
                                foreach (var item in lista)
                                {
                                    Console.WriteLine(item.Id);
                                    Console.WriteLine(item.Cliente);
                                }
                                break;
                            default:
                                Console.WriteLine("Opção inválida. Tente novamente.");
                                break;
                        }
                        break;
                    case "6":
                        Console.WriteLine("Digite o ID do pedido:");
                        pedidoId = int.Parse(Console.ReadLine());
                        gerenciamentoDePedidos.CalcularValorTotalPedido(pedidoId);
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


    }
}