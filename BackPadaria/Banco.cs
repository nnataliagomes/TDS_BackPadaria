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
                    Console.WriteLine("Tentando conectar ao banco de dados");

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

        
        //decalração do método: púclico e estático, reotrna um número inteiro (linhas afetadas)
        // e recebe um objeto MySqlCommand com a instrução SQL já conigurada em parâmetro.
        
        public static int executarComando(MySqlCommand command)
        {


            /* O bloco 'using' garante que a conexão será automaticamente fechada e liberada
            da memória ao final do bloco, mesmo que aocorram erros no meio do processo

            cria a conexão utilizando a varáivel de ambiente */

            using (var conexao = new MySqlConnection(stringDeConexao))
            {
                // vinculação: diz ao comando sql em qual conexão ele deve ser executado
                command.Connection = conexao;


                // tratamento de erros: Inicia um bloco de tentativa para proteger o programa de falhas abruptas.
                try
                {
                    // Abertura da conexão: Estabelece efetivamente a comunicação com o servidor Mysql
                    conexao.Open();



                    /* Execução: o ExecuteNonQuery dispara comandos de ação (INSERT, UPDATE, DELETE).
                     * Ele não retorna registros, mas sim o número inteiro de linhas que foram alteradas, e o 
                     * return devolve esse número para quem chamou o método. */
                    return command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Erro de Banco de Dados {ex.Message}");

                    return -1;
                }    
            }
        }
    }
}       
