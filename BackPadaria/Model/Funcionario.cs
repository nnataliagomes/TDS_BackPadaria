using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace BackPadaria.Model
{
    internal class Funcionario
    {
        private int id;
        private string nome;
        private string cpf;
        private string cargo;


        public Funcionario(int id, string nome, string cpf, string cargo)
        {
            this.Id = id;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Cargo = cargo;
        }

        public Funcionario(string? nome, string? cpf, string? cargo)
        {
            Nome = nome;
            Cpf = cpf;
            Cargo = cargo;
        }

        public Funcionario(int id)
        {
            this.Id = id;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Cargo { get => cargo; set => cargo = value; }
      
        public static int inserir(Funcionario funcionario)
        {



            // Instrução SQL: Define o comando de inserção (INSERT).
            // 0 uso de marcações com '@' (ex: @Nome) é uma pratica de segurança crucial chamada de "Parametrização".
            // Ela impede ataques de SQL Injection, pois o banco entende o que e comando e o que e apenas texto do usuario.

            string sql = "insert into `Funcionario` (`Nome`, `CPF`, `Cargo`)" +
            "VALUES (@Nome, @CPF, @Cargo)";


            // Gerenciamento de recursos: Cria o objeto de comando já passando a instrução SQL.
            // O 'using' garante que os recursos de memoria ocupados pelo comando sejam liberados ao final do bloco.

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Nome",  funcionario.Nome);
                cmd.Parameters.AddWithValue("@CPF",   funcionario.Cpf);
                cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);
               


                // Execucao: Como o comando de INSERT nao retorna tabelas (apenas executa uma ação),
                // ele envia o comando configurado para o método genérico 'executarComando' da classe 'Banco'.
                // 0 método retorna 1 (se o usuário for inserido com sucesso) ou -1 (se houver erro no banco).

                return Banco.executarComando(cmd);
            }
        }

        public static int Deletar(Funcionario funcionario)
        {
            string sql = "DELETE FROM `Funcionario` WHERE `id` = @id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", funcionario.id);

                return Banco.executarComando(cmd);
            }
        }

        public static List<Funcionario> ListarTodos()
        {
            string sql = "SELECT * FROM `Funcionario`";

            List<Funcionario> listaFuncionario = new();

            using (var cmd = new MySqlCommand(sql))
            {


                // Executa a leitura. 0 bloco 'using' aqui é FUNDAMENTAL para garantir que o
                // reader seja fechado no final, o que por sua vez fecha a conexão com o banco.
                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {
                    // while vai rodar uma vez para CADA linha que existir na tabela
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("Id");
                        string nome = validarCampo(reader["Nome"]);
                        string cpf = validarCampo(reader["CPF"]);
                        string cargo = validarCampo(reader["Cargo"]);


                        Funcionario funcionario = new Funcionario(id, nome, cpf, cargo);

                        listaFuncionario.Add(funcionario);
                    }
                }
                // retorna a lista completa (se a tabela estiver vazia, retorna uma lista com 0 itens)
                return listaFuncionario;
            }
        }

        public static Funcionario? ListarPorId(int id)
        {
            string sql = "SELECT * FROM `Funcionario` WHERE `id` = @id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);
                Funcionario funcionario = null;
                while (reader.Read())
                {
                    // Extrair os dados da linha atual
                    int idBanco = reader.GetInt32("Id");

                    string nome = validarCampo(reader["Nome"]);
                    string cpf = validarCampo(reader["CPF"]);
                    string cargo = validarCampo(reader["Cargo"]);
                    
                    // Cria o objeto Cliente com os dados extraídos
                    funcionario = new Funcionario(idBanco, nome, cpf, cargo);
                }
                return funcionario;
            }
        }

        public static int Atualizar(Funcionario funcionario)
        {
            string sql = "UPDATE `Funcionario` SET `Nome` = @Nome, `Cpf` = @CPF,  " +
                "WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@CPF", funcionario.Cpf);
                cmd.Parameters.AddWithValue("@Telefone", funcionario.Cargo);
                


                // a chave de busca: Quem será atualizado?
                cmd.Parameters.AddWithValue("@Id", funcionario.id);

                return Banco.executarComando(cmd);
            }
        }

        private static string validarCampo(object campo)
        {
            if (campo == DBNull.Value || campo == null)
            {
                return "";
            }
            else
            {
                return campo.ToString();
            }
        }



    } // internal class
} // namespace    
