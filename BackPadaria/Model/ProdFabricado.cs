using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace BackPadaria.Model
{
    internal class ProdFabricado
    {
        int produtoID;
        string descricaoProducao;

        public ProdFabricado(int produtoID, string descricaoProducao)
        {
            this.ProdutoID = produtoID;
            this.DescricaoProducao = descricaoProducao;
        }

        public int ProdutoID { get => produtoID; set => produtoID = value; }
        public string DescricaoProducao { get => descricaoProducao; set => descricaoProducao = value; }

        public static int Inserir(ProdFabricado prodFabricado)
        {
            string sql = "INSERT INTO `ProdFabricado` (produtoID, descricaoProducao) VALUES (@produtoID, @descricaoProducao)";

            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@produtoID", prodFabricado.produtoID);
                cmd.Parameters.AddWithValue("@descricaoProducao", prodFabricado.descricaoProducao);

                return Banco.executarComando(cmd);
            }

        }

        public static int Deletar(ProdFabricado prodFabricado)
        {
            string sql = "DELETE FROM `ProdutoFabricado` WHERE `Id` = @id";
            using (var cmd = new MySqlCommand(sql))

            {
                cmd.Parameters.AddWithValue("@id", prodFabricado.id);
                return Banco.executarComando(cmd);
            }

        }

        public static List<ProdFabricado> ListarTodos()
        {
            string sql = "SELECT * FROM `ProdutoFabricado`";
            List<ProdFabricado> listaProdFabricado = new();
            using (var cmd = new MySqlCommand(sql))
            {
                using (MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd))
                {
                    while (reader.Read())

                    {

                        int produtoID = reader.GetInt32("produtoID");
                        string descricaoProducao = validarCampo(reader["descricaoProducao"]);


                        ProdFabricado prodFabricado = new ProdFabricado(produtoID, descricaoProducao);
                        listaProdFabricado.Add(prodFabricado);
                    }

                }
                return listaProdFabricado;
            }

        }

        public static ProdFabricado? ListarPorId(int id)
        {
            string sql = "SELECT * FROM `ProdutoFabricado` WHERE `id` = @Id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = Banco.executarComRetornoDeDados(cmd);
                ProdFabricado? prodFabricado = null;
                while (reader.Read())
                {

                    int produtoID = reader.GetInt32("Id");
                    string descricaoProducao = validarCampo(reader["Nome"]);

                    prodFabricado = new ProdFabricado(produtoID, descricaoProducao);
                }

                return prodFabricado;
            }
        }

        public static int Atualizar(ProdFabricado prodFabricado)
        {
            string sql = @"UPDATE ProdutoFabricado SET produtoID = @produtoID, descricaoProducao = @descricaoProducao, WHERE Id = @Id";
            using (var cmd = new MySqlCommand(sql))
            {
                cmd.Parameters.AddWithValue("@produtoID", prodFabricado.produtoID);
                cmd.Parameters.AddWithValue("@descricaoProducao", prodFabricado.descricaoProducao);

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
} //namespace  



