using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackPadaria.Model;

namespace BackPadaria.Service
{
    public static class FuncionarioService
    {
        public static void MenuFuncionario()
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("===== GESTÃO DE CLIENTES =====");

                Console.WriteLine("1 - Cadastrar Funcionário");
                Console.WriteLine("2 - ListarTodos");
                Console.WriteLine("3 - Buscar Funcionário por ID");
                Console.WriteLine("4 - Atualizar");
                Console.WriteLine("5 - Excluir Funcionário");
                Console.WriteLine("0 - Sair do Programa");

                Console.WriteLine("======================");
                Console.WriteLine("Digite uma opção: ");

                string opcao = Console.ReadLine();

                Console.Clear();

                try
                {
                    switch (opcao)
                    {
                        case "1": // Cadastrar
                            cadastrarFuncionarioMenu();
                            break;

                        case "2": // Listar
                            listarFuncionarioMenu();
                            break;

                        case "3": // Buscar
                            buscarFuncionarioPorId();
                            break;

                        case "4": // Buscar
                            atualizarFuncionarioMenu();
                            break;

                        case "5": // deletar
                            deletarFuncionarioMenu();
                            break;

                        case "0": // Sair
                            sair = true;
                            Console.WriteLine("Bye-Bye!");
                            break;

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                }
            } // FIM MENU
        }


        private static void cadastrarFuncionarioMenu()
             {
            Console.WriteLine("------ CADASTRAR FUNCIONÁRIO ----- ");

            Console.WriteLine("Digite o nome do Funcionário: ");
            string nomeFunc = Console.ReadLine();

            Console.WriteLine("Digite o cpf do Funcionário: ");
            string cpfFunc = Console.ReadLine();

            Console.WriteLine("Digite o Cargo do Funcionário: ");
            string cargoFunc = Console.ReadLine();


            Funcionario NovoFunc = new Funcionario(
                nome: nomeFunc,
                cpf: cpfFunc,
                cargo: cargoFunc
            );

            int resultadoInsert = Funcionario.inserir(NovoFunc);

            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Funcionário cadastrado com sucesso!");

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();


        } // fim cadastro


        private static void listarFuncionarioMenu()
        {
            Console.WriteLine("------ LISTA DE FUNCIONÁRIOS ----- ");

            List<Funcionario> lista = Funcionario.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum funcionário cadastrado no sistema");

            }
            else
            {
                foreach (var func in lista)
                {
                    Console.WriteLine($"ID: {func.Id} | Nome {func.Nome} | CPF: {func.Cpf}| Cargo: {func.Cargo} ");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        
        } // fim lista clientes


        private static void buscarFuncionarioPorId()
        {
            Console.WriteLine("------ BUSCAR FUNCIONÁRIO POR ID ----- ");
            Console.WriteLine("Digite o ID do funcionário que quer buscar: ");

            int id = int.Parse(Console.ReadLine());

            Funcionario func = Funcionario.ListarPorId(id);

            if (func == null)
            {
                Console.WriteLine("Funcionário não encontrado");
            }
            else
            {
                Console.WriteLine($"ID: {func.Id} | Nome {func.Nome} | CPF: {func.Cpf}");
            }

            Console.WriteLine("Pressione alguma tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        
        } // fim busca Funcionário



        private static void atualizarFuncionarioMenu()
        {
            Console.WriteLine("----- ATUALIZAR FUNCIONÁRIO POR ID -----");
            Console.WriteLine("Digite o ID do Funcionário que deseja atualizar: ");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);
            Console.WriteLine($"ID selecionado: {idBusca}");

            Funcionario funcParaAtualizar = Funcionario.ListarPorId(idBusca);

            if (funcParaAtualizar != null)
            {
                Console.WriteLine($"Atualizar dados de {funcParaAtualizar.Nome}");
                Console.WriteLine("Pressione ENTER sem digitar nada para manter os dados atuais.");
                Console.WriteLine($"Novo Cargo: Atual ({funcParaAtualizar.Cargo} )");

                string novoCargo = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoCargo)) funcParaAtualizar.Cargo = novoCargo;

                int resultadoAtualizacao = Funcionario.Atualizar(funcParaAtualizar);
                if (resultadoAtualizacao > 0) Console.WriteLine("Cliente atualizado com sucesso! ");

            }
            else
            {
                Console.WriteLine("Funcionário não encontrado");
            }

            Console.ReadKey();
        } // fim atualizar Funcionário


        private static void deletarFuncionarioMenu()
        {
            Console.WriteLine("----- EXCLUIR FUNCIONÁRIO POR ID -----");
            Console.WriteLine("Digite o ID do Funcionário que deseja excluir: ");
            int id = int.Parse(Console.ReadLine());


            Funcionario funcParaDeletar = Funcionario.ListarPorId(id);

            if (funcParaDeletar != null)
            {
                Console.WriteLine($"Tem certeza que deseja excluir o {funcParaDeletar.Nome} (s/n)?");
                string opcao = Console.ReadLine();

                if (opcao.ToUpper() == "S")
                {
                    int clienteDeletado = Funcionario.Deletar(funcParaDeletar);
                    Console.WriteLine("Cliente excluído com sucesso.");

                }
                else if (opcao.ToUpper() == "N")
                {
                    Console.WriteLine("Cliente não foi excluído.");
                }

            }
        } // fim DELETE


    } //classe
} // namespace
