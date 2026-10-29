using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackPadaria.Model;

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
                Console.WriteLine("========== PDV - PADARIA PÃOS & PÃOS ==========\n");
                Console.WriteLine("0 - Voltar ao menu principal");
                Console.WriteLine("1 - Iniciar nova venda\n");
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

                } catch (Exception ex)
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

            //identificação do funcionário
            int funcionarioId = 2;

            //1 - identificação do cliente
            Console.WriteLine("Id do cliente (Presionar ENTER se não quiser identificar");
            int idCliente = 0;
            int.TryParse(Console.ReadLine(), out idCliente);

            Cliente? clienteVenda = Cliente.ListarPorId(idCliente);

            //Inicio objeto compra
            Compra compraAtiva = new Compra(DateTime.Now, funcionarioId, clienteVenda.Id);

            //loop carrinho de compras
            bool adicionarMais = true;

            while (adicionarMais)
            {
                Console.WriteLine("\n--- Adicionar Produto ---");
                Console.WriteLine("Id do produto ou '0' para finalizar/pagar");

                int.TryParse(Console.ReadLine(), out int idProduto);

                if (idProduto == 0)
                {
                    adicionarMais = false;
                    continue;
                }

                Produto? produto = Produto.ListarPorId(idProduto);

                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado. Tente novamente.");
                    continue;
                }

                Console.Write($"{produto.Nome} | Preço {produto.Preco1:C} | Digite a quantidade: ");
                int.TryParse(Console.ReadLine(), out int qtdProduto);

                if (qtdProduto >= 0)
                {
                    //item compra
                    compraAtiva.AdicionarItem(produto, qtdProduto);
                    Console.WriteLine($"Subtotal: {produto.Preco1 * qtdProduto}");
                    Console.ReadLine();

                }

            } //checkout
            if (compraAtiva.Itens.Count == 0)
            {
                Console.WriteLine("/n-------- Resumo da Venda -------");
                foreach (var item in compraAtiva.Itens)
                {
                    Console.WriteLine($"Id: {item.ProdutoId} | Quantidade: {item.Quantidade} | Preço Unit: {item.PrecoCobrado}" +
                        $"Subtotal: {item.CalcularSubtotal():C}");
                }
                decimal totalFinal = compraAtiva.CalcularTotal();

                Console.WriteLine("-------------------------------");
                Console.WriteLine($"TOTAL A PAGAR: {totalFinal:C}");
                Console.WriteLine("-------------------------------");
            }


        }
    }//classe
}//namespace
