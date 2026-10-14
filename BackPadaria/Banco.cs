using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackPadaria
{
    internal class Banco
    {
        private static string stringDeConexao = "Server=10.91.45.33;Port=3306;Database=padaria_bd;Uid=admin;Pwd=12345678";// string de conexao é o caminho para conectar com o banco

        public static void connectAndTest()
        {
            using (var connection = new MySqlConnection(stringDeConexao))// criando obj 
            {
                try
                {
                    Console.WriteLine("Tentando conecta ao banco de dados");

                    connection.Open();

                    Console.WriteLine("Conexão bem sucedida");

                    using (var command = new MySqlCommand("SELECT VERSION();", connection))
                    {
                        string? version = command.ExecuteScalar()?.ToString();
                        Console.WriteLine($"Versao do MariaDB {version}");

                        // teste
                    }


                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erro ao conectar no banco de dados");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

}
