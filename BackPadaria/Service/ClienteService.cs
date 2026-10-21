using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackPadaria.Model;

namespace BackPadaria.Service
{
    public static class ClienteService
    {

        public static void MenuCliente()
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("===== GESTÃO DE CLIENTES =====");

                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar Todos os Funcionários");
                Console.WriteLine("3 - ListarPorId");
                Console.WriteLine("4 - Atualizar Cliente");
                Console.WriteLine("5 - Excluir Cliente");
                Console.WriteLine("6 - Visualizar pontuação");
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
                            cadastrarClienteMenu();
                            break;

                        case "2": // Listar
                            listarClientesMenu();
                            break;

                        case "3": // Buscar
                            buscarClientesPorId();
                            break;

                        case "4": // Buscar
                            atualizarClienteMenu();
                            break;

                        case "5": // deletar
                            deletarClienteMenu();
                            break;

                        case "6": // deletar
                            visualizarPontosMenu();
                            break;

                        case "0": // Sair
                            sair = true;
                            Console.WriteLine("Tchau");
                            break;

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO DE VALIDAÇÃO]: {ex.Message}");
                }



            }




        } // FIM MENU


        
        private static void cadastrarClienteMenu()
        {
            Console.WriteLine("------ CADASTRAR CLIENTE ----- ");

            Console.WriteLine("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o endereço do cliente: ");
            string enderecoCliente = Console.ReadLine();

            Console.WriteLine("Digite o cpf do cliente: ");
            string cpfCliente = Console.ReadLine();

            Console.WriteLine("Digite o telefone do cliente: ");
            string telefoneCliente = Console.ReadLine();

            Console.WriteLine("Digite a quantidade de pontos");
            int pontosCliente = int.Parse(Console.ReadLine());


            Cliente NovoCliente = new Cliente(
                nome: nomeCliente,
                cpf: cpfCliente,
                telefone: telefoneCliente,
                endereco: enderecoCliente,
                pontos: pontosCliente
            );

            int resultadoInsert = Cliente.inserir(NovoCliente);

            Console.Clear();

            if (resultadoInsert > 0) Console.WriteLine("Cliente cadastrado com sucesso!");

        } // fim cadastro cliente

        private static void listarClientesMenu()
        {
            Console.WriteLine("------ LISTA DE CLIENTES ----- ");

            List<Cliente> lista = Cliente.ListarTodos();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado no sistema");

            }
            else
            {
                foreach (var cliente in lista)
                {
                    Console.WriteLine($"ID: {cliente.Id} | Nome {cliente.Nome} | CPF: {cliente.Cpf}| Pontos: {cliente.Pontos} ");
                }
            }

            Console.WriteLine("\nPressione uma tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        } // fim lista clientes


        
        private static void buscarClientesPorId()
        {
            Console.WriteLine("------ BUSCAR CLIENTE POR ID ----- ");
            Console.WriteLine("Digite o ID do cliente que quer buscar: ");

            int id = int.Parse(Console.ReadLine());

            Cliente cliente = Cliente.ListarPorId(id);

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado");
            }
            else
            {
                Console.WriteLine($"ID: {cliente.Id} | Nome {cliente.Nome} | CPF: {cliente.Cpf}");
            }

            Console.WriteLine("Pressione alguma tecla para continuar");
            Console.ReadKey();
            Console.Clear();
        } // fim busca cliente



        private static void atualizarClienteMenu()
        {
            Console.WriteLine("----- ATUALIZAR CLIENTE POR ID -----");
            Console.WriteLine("Digite o ID do cliente que deseja atualizar: ");

            int idBusca = 0;
            int.TryParse(Console.ReadLine(), out idBusca);
            Console.WriteLine($"-------> {idBusca}");

            Cliente clienteParaAtualizar = Cliente.ListarPorId(idBusca);

            if (clienteParaAtualizar != null)
            {
                Console.WriteLine($"Atualizar dados de {clienteParaAtualizar.Nome}");
                Console.WriteLine("Pressione ENTER sem digitar nada para manter os dados atual ");
                Console.WriteLine($"Novo Endereço: Atual ({clienteParaAtualizar.Endereco} )");

                string novoEndereco = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoEndereco)) clienteParaAtualizar.Endereco = novoEndereco;

                Console.WriteLine($"Novo Telefone Atual: ({clienteParaAtualizar.Telefone}) ");
                string? novoTelefone = Console.ReadLine();
                if (!string.IsNullOrEmpty(novoTelefone)) clienteParaAtualizar.Telefone = novoTelefone;

                int resultadoAtualizacao = Cliente.Atualizar(clienteParaAtualizar);
                if (resultadoAtualizacao > 0) Console.WriteLine("Cliente atualizado com sucesso! ");

            }
            else
            {
                Console.WriteLine("Cliente não encontrado");
            }

            Console.ReadKey();
        } // fim atualizar cliente

        private static void deletarClienteMenu()
        {
            Console.WriteLine("----- EXCLUIR CLIENTE POR ID -----");
            Console.WriteLine("Digite o ID do cliente que deseja excluir: ");
            int id = int.Parse (Console.ReadLine());


            Cliente clienteParaDeletar = Cliente.ListarPorId(id);

            if (clienteParaDeletar != null)
            {
                Console.WriteLine($"Tem certeza que deseja excluir o {clienteParaDeletar.Nome} (s/n)?");
                string opcao = Console.ReadLine();

                if (opcao.ToUpper() == "S")
                {
                    int clienteDeletado = Cliente.Deletar(clienteParaDeletar);
                    Console.WriteLine("Cliente excluído com sucesso.");

                }
                else if (opcao.ToUpper() == "N")
                {

                    Console.WriteLine("Cliente não foi excluído.");
                }

            }
        } // fim DELETE


        private static void visualizarPontosMenu()
        {
            Console.WriteLine("----- VISUALIZAR PONTOS DO CLIENTE -----");

            Console.WriteLine("Digite o ID do cliente que deseja visualizar: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido");
                return;
                Console.Clear();
            }

            Cliente cliente = Cliente.ListarPorId (id);

            if (cliente != null)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine($"ID do cliente: {cliente.Id}");
                Console.WriteLine($"ID do cliente: {cliente.Nome}");
                Console.WriteLine($"ID do cliente: {cliente.Pontos}");
                Console.WriteLine("---------------------");

                Console.WriteLine("Pressione alguma tecla para continuar.");
                Console.ReadKey();
                Console.Clear();

            }
            else
            {
                Console.WriteLine("Cliente não encontrado");
            }
        } // fim visualizar pontos


      

        }// static class
}// namespace
