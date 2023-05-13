using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.IniciarDataBase
{
    public class IniciarDataBase
    {
        private readonly string _connectionString;
        public IniciarDataBase(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void CriarDb()
        {
            AdicionarProduto("Produto 1", 10.99m);
            AdicionarProduto("x2", 10.99m);
            AdicionarProduto("s3", 10.99m);
            AdicionarProduto("Produto 4", 10.99m);
            AdicionarProduto("Produto 5", 10.99m);
            AdicionarProduto("Produto 6", 10.99m);
            AdicionarProduto("Produto 7", 10.99m);
            AdicionarProduto("Produto 8", 10.99m);
            AdicionarProduto("Produto 9", 10.99m);
        }
        private void AdicionarProduto(string nome, decimal preco)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                // Verifica se o produto já existe na tabela
                var command = new MySqlCommand("SELECT COUNT(*) FROM tb_produto WHERE Nome = @Nome", connection);
                command.Parameters.AddWithValue("@Nome", nome);
                long count = (long)command.ExecuteScalar();

                if (count == 0)
                {
                    // Adiciona o produto na tabela
                    command = new MySqlCommand("INSERT INTO tb_produto (Nome, Preco) VALUES (@Nome, @Preco)", connection);
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@Preco", preco);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
