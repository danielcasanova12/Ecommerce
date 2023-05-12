using Ecommerce.Models;
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

        public void Adicionar(ItemPedido itemPedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "INSERT INTO ItemPedido (ProdutoId, Quantidade, PrecoUnitario, PedidoId) VALUES (@ProdutoId, @Quantidade, @PrecoUnitario, @PedidoId); SELECT CAST(SCOPE_IDENTITY() as int)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProdutoId", itemPedido.Produto.Id);
                command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
                command.Parameters.AddWithValue("@PrecoUnitario", itemPedido.PrecoUnitario);
                command.Parameters.AddWithValue("@PedidoId", itemPedido.Pedido.Id);

                var id = (int)command.ExecuteScalar();
                itemPedido.Id = id;
            }
        }

        public void Atualizar(ItemPedido itemPedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE ItemPedido SET ProdutoId = @ProdutoId, Quantidade = @Quantidade, PrecoUnitario = @PrecoUnitario, PedidoId = @PedidoId WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProdutoId", itemPedido.Produto.Id);
                command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
                command.Parameters.AddWithValue("@PrecoUnitario", itemPedido.PrecoUnitario);
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
                        Produto = new Produto { Id = (int)reader["ProdutoId"] },
                        Quantidade = (int)reader["Quantidade"],
                        PrecoUnitario = (decimal)reader["PrecoUnitario"],
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
                        Produto = new Produto { Id = (int)reader["ProdutoId"] },
                        Quantidade = (int)reader["Quantidade"],
                        PrecoUnitario = (decimal)reader["PrecoUnitario"],
                        Pedido = new Pedido { Id = (int)reader["PedidoId"] }
                    };

                    itens.Add(itemPedido);
                }

                return itens;
            }
        }
    }
}
