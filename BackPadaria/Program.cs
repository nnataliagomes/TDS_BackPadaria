using BackPadaria.Model;

namespace BackPadaria
{
    internal class Program
    {
        static void Main(string[] args)
        {

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


        
    }
}
