using Ecommerce.Models;
using Ecommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.UI
{
    public class PedidoUI
    {
        private readonly GerenciamentoDePedidos _gerenciador;

        public PedidoUI(GerenciamentoDePedidos gerenciamentoDePedidos)
        {
            this._gerenciador = gerenciamentoDePedidos;
        }

        //public void MostrarMenu()
        //{
        //    while (true)
        //    {
        //        Console.WriteLine("Selecione uma opção:");
        //        Console.WriteLine("1. Criar um novo pedido");
        //        Console.WriteLine("2. Adicionar itens a um pedido");
        //        Console.WriteLine("3. Atualizar o status de um pedido");
        //        Console.WriteLine("4. Remover um pedido");
        //        Console.WriteLine("5. Listar pedidos por cliente, status ou data");
        //        Console.WriteLine("6. Calcular o valor total de um pedido");
        //        Console.WriteLine("0. Sair");

        //        var opcao = Console.ReadLine();

        //        switch (opcao)
        //        {
        //            case "1":
        //                CriarPedido();
        //                break;
        //            case "2":
        //                AdicionarItensAoPedido();
        //                break;
        //            case "3":
        //                AtualizarStatusPedido();
        //                break;
        //            case "4":
        //                RemoverPedido();
        //                break;
        //            case "5":
        //                Console.WriteLine("Selecione uma opção:");
        //                Console.WriteLine("1. Listar por cliente");
        //                Console.WriteLine("2. Listar por status");
        //                Console.WriteLine("3. Listar por data");
        //                var opcaoListagem = Console.ReadLine();
        //                switch (opcaoListagem)
        //                {
        //                    case "1":
        //                        ListarPedidosPorCliente();
        //                        break;
        //                    case "2":
        //                        ListarPedidosPorStatus();
        //                        break;
        //                    case "3":
        //                        ListarPedidosPorData();
        //                        break;
        //                    default:
        //                        Console.WriteLine("Opção inválida. Tente novamente.");
        //                        break;
        //                }
        //                break;
        //            case "6":
        //                CalcularValorTotalPedido();
        //                break;
        //            case "0":
        //                return;
        //            default:
        //                Console.WriteLine("Opção inválida. Tente novamente.");
        //                break;
        //        }

        //        Console.WriteLine();
        //    }
        //}

        public void ChamarCriarPedido()
        {
            CriarPedido();
        }
        public void ChamarAtualizarStatusPedido()
        {
            AtualizarStatusPedido();
        }
        public void ChamarRemoverPedido()
        {
            RemoverPedido();
        }
        public void ChamarListarPedidosPorCliente()
        {
            ListarPedidosPorCliente();
        }
        public void ChamarRemoverPedido()
        {
            RemoverPedido();
        }
        public void ChamarRemoverPedido()
        {
            RemoverPedido();
        }

        private void CriarPedido()
        {
            Console.WriteLine("Digite o nome do cliente:");
            var nomeCliente = Console.ReadLine();
            var novoPedido = new Pedido
            {
                Cliente = nomeCliente,
                Status = "Em aberto",
                DataPedido = DateTime.Now,
                Itens = new List<ItemPedido>()
            };
            _gerenciador.CriarPedido(novoPedido);
            Console.WriteLine("Pedido criado com sucesso.");
        }


        private void AtualizarStatusPedido()
        {
            Console.WriteLine("Digite o ID do pedido:");
            var pedidoId = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o novo status:");
            var novoStatus = Console.ReadLine();
            _gerenciador.AtualizarStatusPedido(pedidoId, novoStatus);
            Console.WriteLine("Status do pedido atualizado com sucesso.");
        }
        private void RemoverPedido()
        {
            Console.WriteLine("Digite o ID do pedido:");
            int id = int.Parse(Console.ReadLine());

            _gerenciador.RemoverPedido(id);
            Console.WriteLine("Pedido removido com sucesso!");
        }

        private void ListarPedidosPorCliente()
        {
            Console.WriteLine("Digite o nome do cliente:");
            string nomeCliente = Console.ReadLine();

            var pedidos = _gerenciador.ListarPedidosPorCliente(nomeCliente);
            if (pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido encontrado para o cliente informado.");
                return;
            }

            Console.WriteLine($"Pedidos do cliente {nomeCliente}:");
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"ID: {pedido.Id} | Data do pedido: {pedido.DataPedido} | Status: {pedido.Status}");
            }
        }

        private void ListarPedidosPorStatus()
        {
            Console.WriteLine("Digite o status:");
            string status = Console.ReadLine();

            var pedidos = _gerenciador.ListarPedidosPorStatus(status);
            if (pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido encontrado para o status informado.");
                return;
            }

            Console.WriteLine($"Pedidos com status {status}:");
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"ID: {pedido.Id} | Data do pedido: {pedido.DataPedido} | Cliente: {pedido.Cliente}");
            }
        }

        private void ListarPedidosPorData()
        {
            Console.WriteLine("Digite a data inicial (dd/MM/yyyy):");
            DateTime dataInicial = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Digite a data final (dd/MM/yyyy):");
            DateTime dataFinal = DateTime.Parse(Console.ReadLine());

            var pedidos = _gerenciador.ListarPedidosPorData(dataInicial, dataFinal);
            if (pedidos.Count == 0)
            {
                Console.WriteLine("Nenhum pedido encontrado para o período informado.");
                return;
            }

            Console.WriteLine($"Pedidos no período de {dataInicial:dd/MM/yyyy} a {dataFinal:dd/MM/yyyy}:");
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"ID: {pedido.Id} | Cliente: {pedido.Cliente} | Status: {pedido.Status}");
            }
        }

        private void CalcularValorTotalPedido()
        {
            Console.WriteLine("Digite o ID do pedido:");
            int id = int.Parse(Console.ReadLine());

            decimal valorTotal = _gerenciador.CalcularValorTotalPedido(id);
            Console.WriteLine($"Valor total do pedido: R$ {valorTotal}");
        }
        public void mostrarClientes(List<Pedido> lista)
        {
            foreach (var item in lista)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Cliente);
            }
        }

        //private void AdicionarItensAoPedido()
        //{
        //    Console.WriteLine("Digite o ID do pedido:");
        //    var pedidoId = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Digite o ID do produto:");
        //    var produtoId = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Digite a quantidade:");
        //    var quantidade = int.Parse(Console.ReadLine());
        //    var pedido = _gerenciamentoDePedidos.BuscarPorID(pedidoId);
        //    var produto = _gerenciamentoDePedidos.ObterProdutoPorId(produtoId);
        //    var itemPedido = new ItemPedido
        //    {
        //        Produto = produtoId,
        //        Quantidade = quantidade,
        //        PrecoUnitario = 55 // aqui você pode alterar para o preço real do produto, se tiver essa informação disponível
        //    };
        //    _gerenciador.AdicionarItemAoPedido(pedidoId, itemPedido);
        //    Console.WriteLine("Item adicionado ao pedido com sucesso.");
        //}
    }
}
