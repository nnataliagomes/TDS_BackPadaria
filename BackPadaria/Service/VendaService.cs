using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackPadaria.Service
{
    public static class VendaService
    {
        public static void MenuVendas()
        {
            bool sair = false;

            while (!sair)
            {
                Console.Clear();
                Console.WriteLine("========== PDV - Padaria Pãos & Pãos ==========");
                Console.WriteLine("0 - Voltar ao menu principal");
                Console.WriteLine("1 - Iniciar nova venda");
                Console.WriteLine("===============================================");
                Console.WriteLine("Digite uma opção: ");

                string opcao = Console.ReadLine();
                Console.Clear();


                try
                {
                    switch (opcao)
                    {
                        case "1":
                            iniciarNovaVenda();
                            break;
                        case "0":
                            sair = true;
                            break;
                        default:
                            Console.WriteLine("Opção inválida");
                            break;

                    }

                }catch (Exception ex)
                {
                    Console.WriteLine($"\nErro na venda {ex.Message}");
                    Console.WriteLine("\nPressione uma tecla para continuar.");
                    Console.ReadKey();
                }
            }
        }

        public static void iniciarNovaVenda()
        {
            Console.WriteLine("------- NOVA VENDA ------");



            Console.ReadKey();
        }



    }//classe
}//namespace
