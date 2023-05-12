using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public interface IItemPedidoRepository
    {
        void Adicionar(ItemPedido itemPedido);
        void Atualizar(ItemPedido itemPedido);
        void Remover(int id);
        ItemPedido ObterPorId(int id);
        List<ItemPedido> ObterTodos();
    }
}
