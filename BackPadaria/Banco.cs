using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackPadaria;

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

    // Declaracão do metodo: publico e estático, projetado especificamente para executar comandos
    // (geralmente INSERTs seguidos de SELECT LAST_INSERT_ID()) e retornar o ID gerado pelo banco.

    public static int executarComandoComRetornoId(MySqlCommand command)
    {
        using (var conexao = new MySqlConnection(stringDeConexao))
        {
            command.Connection = conexao;

            try
            {
                conexao.Open();
                /* O ExecuteScalar executa o comando e retorna APENAS a primeira coluna da primeira
                 * linha do resultado. É perfeito para pegar um único valor (como um ID recém-gerado).
                 * Ele retorna um tipo 'object' poruqe não sabe de antemão qual o tipo do dado no banco. */

                object objRetorno = command.ExecuteScalar();

                int retorno = 0;

                /* Verificação de segurança: Checa se o banco realmente retornou algo. Verifica tanto
                 * se o objeto C# não é nulo (null) quanto se não é um nulo do próprio banco de dados DBNull.Value)*/

                if (objRetorno != null && objRetorno != DBNull.Value)
                {
                    /* Se passou na verificação, converte o objeto (que veio como texto/objeto) para
                     * um número inteiro (int) */

                    retorno = int.Parse(objRetorno.ToString());

                    return retorno;
                }

                
            }

            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro de Banco de Dados {ex.Message}");
                return -1;
            }

        }
    }

    public static MySqlDataReader executarComRetornoDeDados(MySqlCommand command)
    {
        var conexao = new MySqlConnection(stringDeConexao);

        command.Connection = conexao;

        try
        {
            conexao.Open();

            return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);


            /* O CommandBehavior.CloseConnection é a peça-chave deste método: ele avisa ao leitor
             * que assim que ele (o leitor) for fechado ou descartado lá na tela/classe que o chamou,
             * a conexão com o banco deve ser fechada automaticamente junto com ele. */
        }

        catch (MySqlException ex)
        {
            conexao.Dispose();

            // O Dispose() já executa o Close() e libera a memória.

            Console.WriteLine($"Erro na leitura do Banco de Dados: {ex.Message}");

            // ao invés de retorna null, relança o erro (throw) para que o método que chamou
            // ExecuteReader saiba que a pesquisa falhou e possa tratar a tela do usuário.

            throw;
        }
    }

}
 
