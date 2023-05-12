using Ecommerce.Models;
using Ecommerce.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class GerenciamentoDePedidos
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemPedidoRepository _itemPedidoRepository;

        public GerenciamentoDePedidos(IPedidoRepository pedidoRepository, IItemPedidoRepository itemPedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _itemPedidoRepository = itemPedidoRepository;
        }

        public void CriarPedido(Pedido pedido)
        {
            _pedidoRepository.Adicionar(pedido);
        }

        public void AdicionarItemPedido(ItemPedido itemPedido)
        {
            _itemPedidoRepository.Adicionar(itemPedido);
        }

        public void AtualizarStatusPedido(int pedidoId, string status)
        {
            var pedido = _pedidoRepository.ObterPorId(pedidoId);

            if (pedido != null)
            {
                pedido.Status = status;
                _pedidoRepository.Atualizar(pedido);
            }
        }

        public void RemoverPedido(int pedidoId)
        {
            _pedidoRepository.Remover(pedidoId);
        }

        public List<Pedido> ListarPedidosPorCliente(string cliente)
        {
            return _pedidoRepository.ObterPorCliente(cliente);
        }

        public List<Pedido> ListarPedidosPorStatus(string status)
        {
            return _pedidoRepository.ObterPorStatus(status);
        }

        public List<Pedido> ListarPedidosPorData(DateTime data)
        {
            return _pedidoRepository.ObterPorData(data);
        }

        public decimal CalcularValorTotalPedido(int pedidoId)
        {
            var pedido = _pedidoRepository.ObterPorId(pedidoId);

            if (pedido != null)
            {
                return pedido.Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
            }

            return 0;
        }
    }

}
