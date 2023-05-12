using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public string Cliente { get; set; }
        public string Status { get; set; }
        public List<ItemPedido> Itens { get; set; }
    }



}
