using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace BackPadaria.Model
{
    internal class Produto
        {
           private int id;
           private string nome;
           private decimal Preco;
           private string categoria;
           private bool fabricado;
           private int marcaId;


        public Produto (int id, string nome, decimal preco, string categoria, bool fabricado, int marcaId)
        {
            this.id = id;
            this.nome = nome;
            this.Preco = preco;
            this.categoria = categoria;
            this.fabricado = fabricado;
            this.marcaId = marcaId;
        }

        public Produto(string nome, decimal preco, string categoria, bool fabricado, int marcaId)
        {
            this.nome = nome;
            this.Preco = preco;
            this.categoria = categoria;
            this.fabricado = fabricado;
            this.marcaId = marcaId;
        }

            public Produto(int id)
            {
                this.Id = id;
            }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public decimal Preco1 { get => Preco; set => Preco = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public bool Fabricado { get => fabricado; set => fabricado = value; }
        public int MarcaId { get => marcaId; set => marcaId = value; }



        public static int Inserir (Produto produto)
        {
            string sql = "insert into `produto` (`Nome`, `Preco`, `Categoria`, `Fabricado`, `MarcaId`)" +
            "VALUES (@Nome, @CPF, @Preco, @Fabricado, @MarcaId)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@CPF", produto.Preco);
                cmd.Parameters.AddWithValue("@Telefone", produto.Categoria);
                cmd.Parameters.AddWithValue("@Endereco", produto.Fabricado);
                cmd.Parameters.AddWithValue("@Pontos", produto.MarcaId);

                return Banco.executarComando(cmd);
            }
        
        }

        public static int Deletar(Produto produto)
        {
            string sql = "DELETE FROM `Produto` WHERE `id` = @id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", produto.id);

                return Banco.executarComando(cmd);
            }
        }

        public static List<Produto> ListarTodos()
        {
            string sql = "SELECT * FROM `Produto`";

            List<Produto> listaProduto = new();

            using (var cmd = new MySqlCommand(sql))
            {
                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {
                    while (reader.Read())
                    {
                        int Id = reader.GetInt32("Id");
                        string nome = validarCampo(reader["Nome"]);
                        decimal preco = reader.GetDecimal("Preço");
                        string categoria = validarCampo(reader["Categoria"]);
                        bool fabricado = reader.GetBoolean("Fabricado");
                        int marcaId = reader.GetInt32("MarcaId");


                        Produto produto = new Produto(Id, nome, preco, categoria, fabricado, marcaId);

                        listaProduto.Add(produto);
                    }
                }
                return listaProduto;
            }
        } // fim inserir

        public static Produto? ListarPorId(int id)
        {
            string sql = "SELECT * FROM `Produto` WHERE `id` = @id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);
                Funcionario funcionario = null;
                while (reader.Read())
                {
                    int Id = reader.GetInt32("Id");
                    string nome = validarCampo(reader["Nome"]);
                    decimal preco = reader.GetDecimal("Preço");
                    string categoria = validarCampo(reader["Categoria"]);
                    bool fabricado = reader.GetBoolean("Fabricado");
                    int marcaId = reader.GetInt32("MarcaId");

                    Produto produto = new Produto(id, nome, preco, categoria, fabricado, marcaId);
                }
                return Produto.produto;
            }
        }


        public static int Atualizar(Produto produto)
        {
            string sql = "UPDATE `Produto` SET `Nome` = @Nome, `Preco` = @Preco,  `Categoria` = @Categoria, `Fabricado` = @Fabricado, `MarcaId` = @MarcaId" +
                "WHERE `Id` = @Id";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                cmd.Parameters.AddWithValue("@Categoria", produto.Categoria);
                cmd.Parameters.AddWithValue("@Fabricado", produto.Fabricado);
                cmd.Parameters.AddWithValue("@MarcaId", produto.MarcaId);

                // a chave de busca: Quem será atualizado?
                cmd.Parameters.AddWithValue("@Id", produto.id);

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


    } // internal
} // namespace
