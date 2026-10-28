using backPadaria.Model;
using backPadaria.Service;
using BackPadaria.Model;
using BackPadaria.Service;

namespace BackPadaria
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine();

            Console.WriteLine("Digite o CPF do cliente: ");
            string cpfCliente = Console.ReadLine();

            Console.WriteLine("Digite o telefone do cliente: ");
            string telefoneCliente = Console.ReadLine();

            Console.WriteLine("Digite o endereço do cliente: ");
            string enderecoCliente = Console.ReadLine();

            Console.WriteLine("Digite os Pontos do cliente: ");
            int pontosCliente = int.Parse(Console.ReadLine());

            Cliente cliente1 = new Cliente
                (
                    nome: nomeCliente,
                    cpf: cpfCliente,
                    telefone: telefoneCliente,
                    endereco: enderecoCliente,
                    pontos: pontosCliente
                );
            Cliente.inserir(cliente1);
        }

        


            List<Cliente> funcionario = Cliente.ListarTodos();

            foreach (Cliente cliente in funcionario)
            {

                Console.WriteLine("-------------------------------");

                Console.WriteLine($"ID {cliente.Id}");
                Console.WriteLine($"Nome {cliente.Nome}");
                Console.WriteLine($"CPF {cliente.Cpf}");
                Console.WriteLine($"Telefone {cliente.Telefone}");
                Console.WriteLine($"Endereço {cliente.Endereco}");
                Console.WriteLine($"Pontos {cliente.Pontos}");

                Console.WriteLine("-------------------------------");
            }
            

            Cliente? cli01 = Cliente.ListarPorId(5);

            if (cli01 != null)
            {
                Console.WriteLine($"ID: {cli01.Id}");
                Console.WriteLine($"Nome:  {cli01.Nome}");
                Console.WriteLine($"CPF:  {cli01.Cpf}");
                Console.WriteLine($"Telefone:  {cli01.Telefone}");
                Console.WriteLine($"Endereço:  {cli01.Endereco}");
                Console.WriteLine($"Pontos:  {cli01.Pontos}");
            }
            else
            {
                Console.WriteLine("O cliente não foi encontrado!");
            }
            


            Cliente? meuCliente = Cliente.ListarPorId(2);

            if (meuCliente != null)
            {
                meuCliente.Endereco = "Rua Barão de Alagoas, 171";
                meuCliente.Telefone = "11978945612";

                int resultado = Cliente.Atualizar(meuCliente);
                if(resultado > 0 )
                {
                    Console.WriteLine("Cliente atualizado com sucesso");
                }
               

            }  
            */

            // A partir daqui é teste funcionario

            /*
            Console.WriteLine("Digite o nome do funcionário: ");
            string nomeFuncionario = Console.ReadLine();

            Console.WriteLine("Digite o CPF do funcionário: ");
            string cpfFuncionario = Console.ReadLine();

            Console.WriteLine("Digite o Cargo do funcionário: ");
            string cargoFuncionario = Console.ReadLine();


            Funcionario func1 = new Funcionario(nome: nomeFuncionario, cpf: cpfFuncionario, cargo: cargoFuncionario);

            Funcionario.inserir(func1);
        }
        */


            /*
            Funcionario funcionarioParaDeletar = new Funcionario(4);

            if (Funcionario.Deletar(funcionarioParaDeletar) >= 1)
            {
                Console.WriteLine("Funcionário excluído com sucesso.");
            }
            else
            {
                Console.WriteLine("Não foi possível excluir.");
            }
            */



            /*
            Funcionario? funcionario = Funcionario.ListarPorId(1);
                if (funcionario != null)
                {
                    Console.WriteLine($"ID: {funcionario.Id}");
                    Console.WriteLine($"Nome:  {funcionario.Nome}");
                    Console.WriteLine($"CPF:  {funcionario.Cpf}");
                    Console.WriteLine($"CPF:  {funcionario.Cargo}");
                }
                else
                {
                    Console.WriteLine("O funcionário não foi encontrado!");
                }
            
            */


            /*
            List<Funcionario> funcionarios = Funcionario.ListarTodos();

            foreach (Funcionario funcionario in funcionarios)
            {

                Console.WriteLine("-------------------------------");

                Console.WriteLine($"ID {funcionario.Id}");
                Console.WriteLine($"Nome {funcionario.Nome}");
                Console.WriteLine($"CPF {funcionario.Cpf}");
                Console.WriteLine($"Cargo {funcionario.Cargo}");

                Console.WriteLine("-------------------------------");
            }
            */


            // ClienteService.MenuCliente();
            //FuncionarioService.MenuFuncionario();
            //VendaService.MenuVendas();
            LoginFuncionario login = new LoginFuncionario(2, "jp_padoca@padaria.com.br", "doumaooito", "Diretor");
            LoginFuncionario.Inserir(login);

            LoginService.FazerLoginMenu();




        } //static void
    } // internalclass
} // namespace    
    
