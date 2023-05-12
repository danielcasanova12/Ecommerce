using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public interface IPedidoRepository
    {
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);
        void Remover(int id);
        Pedido ObterPorId(int id);
        List<Pedido> ObterTodos();
        List<Pedido> ObterPorCliente(string cliente);
        List<Pedido> ObterPorStatus(string status);
        List<Pedido> ObterPorData(DateTime data);
    }
}
