using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using MySqlConnector;

namespace BackPadaria.Model
{
    internal class Cliente
    {
        private int id;
        private string nome;
        private string cpf;
        private string telefone;
        private string endereco;
        private int pontos;

        public Cliente(string nome, string cpf, string telefone, string endereco, int pontos)
        {
            if (nome == "") throw new Exception("O nome do cliente não pode ser vazio");

            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Pontos = pontos;
        }

        public Cliente(int id, string nome, string cpf, string telefone, string endereco, int pontos)
        {
            this.Id = id;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Pontos = pontos;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public int Pontos { get => pontos; set => pontos = value; }

        public void AdicionarPontos(int pontos)
        {
            this.Pontos++;

        }

        public static int inserir(Cliente cliente)
        {



            // Instrução SQL: Define o comando de inserção (INSERT).
            // 0 uso de marcações com '@' (ex: @Nome) é uma pratica de segurança crucial chamada de "Parametrização".
            // Ela impede ataques de SQL Injection, pois o banco entende o que e comando e o que e apenas texto do usuario.

            string sql = "insert into `Cliente` (`Nome`, `CPF`, `Telefone`, `Endereco`, `Pontos`)" +
            "VALUES (@Nome, @CPF, @Telefone, @Endereco, @Pontos)";


            // Gerenciamento de recursos: Cria o objeto de comando já passando a instrução SQL.
            // O 'using' garante que os recursos de memoria ocupados pelo comando sejam liberados ao final do bloco.

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);
                cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                cmd.Parameters.AddWithValue("@Pontos", cliente.Pontos);


                // Execucao: Como o comando de INSERT nao retorna tabelas (apenas executa uma ação),
                // ele envia o comando configurado para o método genérico 'executarComando' da classe 'Banco'.
                // 0 método retorna 1 (se o usuário for inserido com sucesso) ou -1 (se houver erro no banco).

                return Banco.executarComando(cmd);
            }
        }

        public static int Deletar(Cliente cliente)
        {
            string sql = "DELETE FROM `Cliente` WHERE `id` = @id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", cliente.id);

                return Banco.executarComando(cmd);
            }
        }

        public static List<Cliente> ListarTodos()
        {
            string sql = "SELECT * FROM `Cliente`";

            List<Cliente> listaClientes = new();

            using (var cmd = new MySqlCommand(sql))
            {


                // Executa a leitura. 0 bloco 'using' aqui é FUNDAMENTAL para garantir que o
                // reader seja fechado no final, o que por sua vez fecha a conexão com o banco.
                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {
                    // while vai rodar uma vez para CADA linha que existir na tabela Cliente
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("Id");
                        string nome = validarCampo(reader["Nome"]);
                        string cpf = validarCampo(reader["CPF"]);
                        string telefone = validarCampo(reader["Telefone"]);
                        string endereco = validarCampo(reader["Endereco"]);
                        int pontos = reader.GetInt32("Pontos");

                        Cliente cliente = new Cliente(id, nome, cpf, telefone, endereco, pontos);

                        listaClientes.Add(cliente);
                    }
                }
                // retorna a lista completa (se a tabela estiver vazia, retorna uma lista com 0 itens)
                return listaClientes;
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
    }
}
    