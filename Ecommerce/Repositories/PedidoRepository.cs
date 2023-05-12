using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _connectionString;

        public PedidoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Adicionar(Pedido pedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "INSERT INTO Pedido (DataPedido, Cliente, Status) VALUES (@DataPedido, @Cliente, @Status); SELECT CAST(SCOPE_IDENTITY() as int)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
                command.Parameters.AddWithValue("@Cliente", pedido.Cliente);
                command.Parameters.AddWithValue("@Status", pedido.Status);

                var id = (int)command.ExecuteScalar();
                pedido.Id = id;

                foreach (var item in pedido.Itens)
                {
                    item.Pedido = pedido;
                    AdicionarItem(item);
                }
            }
        }

        public void Atualizar(Pedido pedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE Pedido SET DataPedido = @DataPedido, Cliente = @Cliente, Status = @Status WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
                command.Parameters.AddWithValue("@Cliente", pedido.Cliente);
                command.Parameters.AddWithValue("@Status", pedido.Status);
                command.Parameters.AddWithValue("@Id", pedido.Id);

                command.ExecuteNonQuery();

                foreach (var item in pedido.Itens)
                {
                    item.Pedido = pedido;
                    AtualizarItem(item);
                }
            }
        }

        public void Remover(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM Pedido WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }

        public Pedido ObterPorId(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Pedido WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var pedido = new Pedido
                    {
                        Id = (int)reader["Id"],
                        DataPedido = (DateTime)reader["DataPedido"],
                        Cliente = (string)reader["Cliente"],
                        Status = (string)reader["Status"],
                        Itens = ObterItensPorPedido((int)reader["Id"])
                    };

                    return pedido;
                }

                return null;
            }
        }

        public List<Pedido> ObterTodos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Pedido";
                var command = new SqlCommand(query, connection);

                var reader = command.ExecuteReader();

                var pedidos = new List<Pedido>();

                while (reader.Read())
                {
                    var pedido = new Pedido
                    {
                        Id = (int)reader["Id"],
                        DataPedido = (DateTime)reader["DataPedido"],
                        Cliente = (string)reader["Cliente"],
                        Status = (string)reader["Status"],
                        Itens = ObterItensPorPedido((int)reader["Id"])
                    };

                    pedidos.Add(pedido);
                }

                return pedidos;
            }
        }

        public List<Pedido> ObterPorCliente(string cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Pedido WHERE Cliente = @Cliente";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Cliente", cliente);

                var reader = command.ExecuteReader();

                var pedidos = new List<Pedido>();

                while (reader.Read())
                {
                    var pedido = new Pedido
                    {
                        Id = (int)reader["Id"],
                        DataPedido = (DateTime)reader["DataPedido"],
                        Cliente = (string)reader["Cliente"],
                        Status = (string)reader["Status"],
                        Itens = ObterItensPorPedido((int)reader["Id"])
                    };

                    pedidos.Add(pedido);
                }

                return pedidos;
            }
        }

        public List<Pedido> ObterPorStatus(string status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Pedido WHERE Status = @Status";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", status);

                var reader = command.ExecuteReader();

                var pedidos = new List<Pedido>();

                while (reader.Read())
                {
                    var pedido = new Pedido
                    {
                        Id = (int)reader["Id"],
                        DataPedido = (DateTime)reader["DataPedido"],
                        Cliente = (string)reader["Cliente"],
                        Status = (string)reader["Status"],
                        Itens = ObterItensPorPedido((int)reader["Id"])
                    };

                    pedidos.Add(pedido);
                }

                return pedidos;
            }
        }

        public List<Pedido> ObterPorData(DateTime data)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Pedido WHERE DataPedido = @DataPedido";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DataPedido", data);

                var reader = command.ExecuteReader();

                var pedidos = new List<Pedido>();

                while (reader.Read())
                {
                    var pedido = new Pedido
                    {
                        Id = (int)reader["Id"],
                        DataPedido = (DateTime)reader["DataPedido"],
                        Cliente = (string)reader["Cliente"],
                        Status = (string)reader["Status"],
                        Itens = ObterItensPorPedido((int)reader["Id"])
                    };

                    pedidos.Add(pedido);
                }

                return pedidos;
            }
        }

        private void AdicionarItem(ItemPedido itemPedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "INSERT INTO ItemPedido (ProdutoId, Quantidade, PrecoUnitario, PedidoId) VALUES (@ProdutoId, @Quantidade, @PrecoUnitario, @PedidoId)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProdutoId", itemPedido.Produto.Id);
                command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
                command.Parameters.AddWithValue("@PrecoUnitario", itemPedido.PrecoUnitario);
                command.Parameters.AddWithValue("@PedidoId", itemPedido.Pedido.Id);

                command.ExecuteNonQuery();
            }
        }

        private void AtualizarItem(ItemPedido itemPedido)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE ItemPedido SET ProdutoId = @ProdutoId, Quantidade = @Quantidade, PrecoUnitario = @PrecoUnitario WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProdutoId", itemPedido.Produto.Id);
                command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
                command.Parameters.AddWithValue("@PrecoUnitario", itemPedido.PrecoUnitario);
                command.Parameters.AddWithValue("@Id", itemPedido.Id);

                command.ExecuteNonQuery();
            }
        }

        private List<ItemPedido> ObterItensPorPedido(int pedidoId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM ItemPedido WHERE PedidoId = @PedidoId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PedidoId", pedidoId);

                var reader = command.ExecuteReader();

                var itens = new List<ItemPedido>();

                while (reader.Read())
                {
                    var item = new ItemPedido
                    {
                        Id = (int)reader["Id"],
                        Produto = new Produto { Id = (int)reader["ProdutoId"] },
                        Quantidade = (int)reader["Quantidade"],
                        PrecoUnitario = (decimal)reader["PrecoUnitario"]
                    };

                    itens.Add(item);
                }

                return itens;
            }
        }
    }

}
