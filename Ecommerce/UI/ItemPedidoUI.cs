using Ecommerce.Models;
using Ecommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.UI
{
    public class ItemPedidoUI
    {
        private readonly GerenciamentoDePedidos _gerenciamentoDePedidos;

        public ItemPedidoUI(GerenciamentoDePedidos gerenciamentoDePedidos)
        {
            _gerenciamentoDePedidos = gerenciamentoDePedidos;
        }
        public void ChamarAdicionarItensPedido()
        {
            AdicionarItemAoPedido();
        }
        private void ListarProdutos()
        {
            _gerenciamentoDePedidos.ObterProdutos();
        }
        public void AdicionarItemAoPedido()
        {
            try
            {
                ListarProdutos();
                Console.WriteLine("Digite o ID do pedido:");
                var pedidoId = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite o ID do produto:");
                var produtoId = int.Parse(Console.ReadLine());
                Console.WriteLine("Digite a quantidade:");
                var quantidade = int.Parse(Console.ReadLine());

                var pedido = _gerenciamentoDePedidos.BuscarPorID(pedidoId);
                var produto = _gerenciamentoDePedidos.ObterProdutoPorId(produtoId);

                while(pedido == null || produto == null)
                {
                    Console.Clear();
                    Console.WriteLine("algo deu errador");
                    AdicionarItemAoPedido();
                }

                var novoItemPedido = new ItemPedido
                {
                    Pedido = pedido,
                    Produto = produto,
                    Quantidade = quantidade
                };
                Console.Clear();
                _gerenciamentoDePedidos.AdicionarItemPedido(novoItemPedido);
                Console.WriteLine("Pedido adicionado");
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("id não encontrado");
                AdicionarItemAoPedido();
                
            }
        }

        //public void AtualizarItemPedido()
        //{
        //    Console.WriteLine("Digite o ID do pedido:");
        //    var pedidoId = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Digite o ID do item:");
        //    var itemId = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Digite a nova quantidade:");
        //    var novaQuantidade = int.Parse(Console.ReadLine());
        //    Console.WriteLine("Digite o novo preço unitário:");
        //    var novoPrecoUnitario = decimal.Parse(Console.ReadLine());
        //    var pedido = _gerenciamentoDePedidos.BuscarPorID(pedidoId);
        //    var itemPedidoAtualizado = new ItemPedido
        //    {
        //        Id = itemId,
        //        Pedido = pedido,
        //        Quantidade = novaQuantidade,
        //        PrecoUnitario = novoPrecoUnitario
        //    };

        //    _gerenciamentoDePedidos.AtualizarItemPedido(itemPedidoAtualizado);
        //}
    }
}

