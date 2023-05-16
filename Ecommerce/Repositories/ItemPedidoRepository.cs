using Ecommerce.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public class ItemPedidoRepository : IItemPedidoRepository
    {
        private readonly string _connectionString;

        public ItemPedidoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AdicionarItem(ItemPedido itemPedido)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO tb_itempedido (ItemPedidoId, ProdutoId, Quantidade, preco_unitario, PedidoId) VALUES (@ItemPedidoId, @ProdutoId, @Quantidade, @PrecoUnitario, @PedidoId);";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ItemPedidoId", itemPedido.Id);
                command.Parameters.AddWithValue("@ProdutoId", itemPedido.Produto.ProdutoId);
                command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
                command.Parameters.AddWithValue("@PrecoUnitario", itemPedido.Produto.Preco);
                command.Parameters.AddWithValue("@PedidoId", itemPedido.Pedido.Id);
                command.ExecuteNonQuery();
            }
        }

        public void Atualizar(ItemPedido itemPedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE ItemPedido SET ProdutoId = @ProdutoId, Quantidade = @Quantidade, PrecoUnitario = @PrecoUnitario, PedidoId = @PedidoId WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProdutoId", itemPedido.Produto.ProdutoId);
                command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
                command.Parameters.AddWithValue("@PedidoId", itemPedido.Pedido.Id);
                command.Parameters.AddWithValue("@Id", itemPedido.Id);

                command.ExecuteNonQuery();
            }
        }


        public void Remover(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM ItemPedido WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }

        public ItemPedido ObterPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM ItemPedido WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var itemPedido = new ItemPedido
                    {
                        Id = (int)reader["Id"],
                        Produto = new Produto { ProdutoId = (int)reader["ProdutoId"] },
                        Quantidade = (int)reader["Quantidade"],
                        Pedido = new Pedido { Id = (int)reader["PedidoId"] }
                    };

                    return itemPedido;
                }

                return null;
            }
        }

        public List<ItemPedido> ObterTodos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM ItemPedido";
                var command = new SqlCommand(query, connection);

                var reader = command.ExecuteReader();

                var itens = new List<ItemPedido>();

                while (reader.Read())
                {
                    var itemPedido = new ItemPedido
                    {
                        Id = (int)reader["Id"],
                        Produto = new Produto { ProdutoId = (int)reader["ProdutoId"] },
                        Quantidade = (int)reader["Quantidade"],
                        Pedido = new Pedido { Id = (int)reader["PedidoId"] }
                    };

                    itens.Add(itemPedido);
                }

                return itens;
            }
        }
    }
}
