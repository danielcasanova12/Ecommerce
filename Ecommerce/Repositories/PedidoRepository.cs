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
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _connectionString;

        public PedidoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Adicionar(Pedido pedido)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var query = "INSERT INTO tb_pedido (Data_pedido, Cliente, status_pedido) VALUES (@DataPedido, @Cliente, @Status); SELECT LAST_INSERT_ID()";
                    var command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
                    command.Parameters.AddWithValue("@Cliente", pedido.Cliente);
                    command.Parameters.AddWithValue("@Status", pedido.Status);

                    var id = (int)(ulong)command.ExecuteScalar();
                    pedido.Id = id;

                    foreach (var item in pedido.Itens)
                    {
                        item.Pedido = pedido;
                        AdicionarItem(item);
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate a exceção aqui, por exemplo, registrando-a em um arquivo de log ou exibindo uma mensagem de erro para o usuário.
                Console.WriteLine("Ocorreu um erro ao adicionar o pedido: " + ex.Message);
            }
        }



        public void Atualizar(Pedido pedido)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "UPDATE tb_pedido SET status_pedido = @Status WHERE PedidoId = @Id";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", pedido.Status);
                command.Parameters.AddWithValue("@Id", pedido.Id);
                command.ExecuteNonQuery();


            }
        }

        public void Remover(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM tb_pedido WHERE PedidoId  = @Id";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }

        public Pedido ObterPorId(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM tb_pedido WHERE PedidoId  = @Id";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var pedido = new Pedido
                    {
                        Id = (int)reader["PedidoId"],
                        DataPedido = (DateTime)reader["data_pedido"],
                        Cliente = (string)reader["Cliente"],
                        Status = (string)reader["status_pedido"]
                    };

                    return pedido;
                }

                return null;
            }
        }

        public List<Pedido> ObterTodos()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Pedido";
                var command = new MySqlCommand(query, connection);

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
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var query = "SELECT * FROM tb_pedido WHERE Cliente = @cliente";
                Console.WriteLine(query);
                var command = new MySqlCommand(query, connection);
                    Console.WriteLine(query);
                    command.Parameters.AddWithValue("@Cliente", cliente);

                    var reader = command.ExecuteReader();

                    var pedidos = new List<Pedido>();

                    while (reader.Read())
                    {
                        var pedido = new Pedido
                        {
                            Id = (int)reader["PedidoId"],
                            DataPedido = (DateTime)reader["data_pedido"],
                            Cliente = (string)reader["Cliente"],
                            Status = (string)reader["status_pedido"]
                        };

                        pedidos.Add(pedido);
                    }
                    
                    return pedidos;
                }
            }
            catch
            {
                Console.WriteLine("o cliente não foi encontrado");
                return null;
            }
        }

        public List<Pedido> ObterPorStatus(string status)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM tb_pedido WHERE status_pedido = @status";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@status", status);

                var reader = command.ExecuteReader();

                var pedidos = new List<Pedido>();

                while (reader.Read())
                {
                    var pedido = new Pedido
                    {
                        Id = (int)reader["PedidoId"],
                        DataPedido = (DateTime)reader["data_pedido"],
                        Cliente = (string)reader["Cliente"],
                        Status = (string)reader["status_pedido"]
                    };

                    pedidos.Add(pedido);
                }

                return pedidos;
            }
        }

        public List<Pedido> ObterPorData(DateTime data, DateTime data2)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Pedido WHERE data_pedido BETWEEN @DataInicio AND @DataFim";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@DataInicio", data);
                command.Parameters.AddWithValue("@DataFim", data2);

                var reader = command.ExecuteReader();

                var pedidos = new List<Pedido>();

                while (reader.Read())
                {
                    var pedido = new Pedido
                    {
                        Id = (int)reader["PedidoId"],
                        DataPedido = (DateTime)reader["data_pedido"],
                        Cliente = (string)reader["Cliente"],
                        Status = (string)reader["status_pedido"]
                    };

                    pedidos.Add(pedido);
                }

                return pedidos;
            }
        }

        private void AdicionarItem(ItemPedido itemPedido)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "INSERT INTO ItemPedido (ProdutoId, Quantidade, PrecoUnitario, PedidoId) VALUES (@ProdutoId, @Quantidade, @PrecoUnitario, @PedidoId)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProdutoId", itemPedido.Produto.Id);
                command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
                command.Parameters.AddWithValue("@PrecoUnitario", itemPedido.PrecoUnitario);
                command.Parameters.AddWithValue("@PedidoId", itemPedido.Pedido.Id);

                command.ExecuteNonQuery();
            }
        }

        //private void AtualizarItem(ItemPedido itemPedido)
        //{
        //    using (var connection = new MySqlConnection(_connectionString))
        //    {
        //        connection.Open();

        //        var query = "UPDATE ItemPedido SET ProdutoId = @ProdutoId, Quantidade = @Quantidade, PrecoUnitario = @PrecoUnitario WHERE Id = @Id";
        //        var command = new MySqlCommand(query, connection);
        //        command.Parameters.AddWithValue("@ProdutoId", itemPedido.Produto.Id);
        //        command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
        //        command.Parameters.AddWithValue("@PrecoUnitario", itemPedido.PrecoUnitario);
        //        command.Parameters.AddWithValue("@Id", itemPedido.Id);

        //        command.ExecuteNonQuery();
        //    }
        //}


        public Produto ObterProduto(int produtoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT nome, preco FROM produto WHERE id = @produtoId";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@produtoId", produtoId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Produto
                {
                    Id = produtoId,
                    Nome = reader.GetString("nome"),
                    Preco = reader.GetDecimal("preco")
                };
            }
            else
            {
                throw new Exception($"Produto com ID {produtoId} não encontrado.");
            }
        }
        private List<ItemPedido> ObterItensPorPedido(int pedidoId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM tb_itempedido WHERE ItemPedidoId = @PedidoId";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@PedidoId", pedidoId);

                var reader = command.ExecuteReader();

                var itens = new List<ItemPedido>();

                while (reader.Read())
                {
                    var item = new ItemPedido
                    {
                        Id = (int)reader["ItemPedidoId"],
                        Produto = new Produto { Id = (int)reader["ProdutoId"] },
                        Quantidade = (int)reader["quantidade"],
                        PrecoUnitario = (decimal)reader["preco_unitario"]
                    };

                    itens.Add(item);
                }

                return itens;
            }
        }
    }

}
