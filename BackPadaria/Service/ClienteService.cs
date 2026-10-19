using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackPadaria.Service
{
    internal class ClienteService
    {
        public static void MenuCliente()
        {
            bool sair = false;

            while (sair)
            {
                Console.WriteLine("====== GE~STÃO DE CLIENTES ======");
                Console.WriteLine("1 - Cadastrar cliente");
                Console.WriteLine("2 - Listar todos os clientes");
                Console.WriteLine("3 - Buscar cliente por ID");
                Console.WriteLine("4 - Atualizar cliente");
                Console.WriteLine("5 - Excluir cliente");
                Console.WriteLine("6 - Visualizar pontos do cliente");
                Console.WriteLine("0 - SAIR");
                Console.WriteLine("==================================");
                Console.WriteLine("Digite uma opção: ");

                string opcao = Console.ReadLine();
                Console.Clear();
            }

        }




    } // internalclass
} // namespace
