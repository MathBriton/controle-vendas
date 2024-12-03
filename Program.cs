using System;
using System.Collections.Generic;

namespace ControleDeVendas
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} | Nome: {Nome} | Preço: {Preco:C}";
        }
    }

    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public decimal Total => CalcularTotal();

        private decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var produto in Produtos)
            {
                total += produto.Preco;
            }
            return total;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Data: {Data:dd/MM/yyyy HH:mm} | Total: {Total:C} | Produtos: {string.Join(", ", Produtos.ConvertAll(p => p.Nome))}";
        }
    }

    public class SistemaVendas
    {
        private List<Produto> produtos = new List<Produto>();
        private List<Venda> vendas = new List<Venda>();
        private int proximoIdProduto = 1;
        private int proximoIdVenda = 1;

        public void AdicionarProduto(Produto produto)
        {
            produto.Id = proximoIdProduto++;
            produtos.Add(produto);
            Console.WriteLine("Produto adicionado com sucesso!");
        }

        public void ListarProdutos()
        {
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            foreach (var produto in produtos)
            {
                Console.WriteLine(produto);
            }
        }

        public void RegistrarVenda(List<int> idsProdutos)
        {
            var venda = new Venda
            {
                Id = proximoIdVenda++,
                Data = DateTime.Now
            };

            foreach (var id in idsProdutos)
            {
                var produto = produtos.Find(p => p.Id == id);
                if (produto != null)
                {
                    venda.Produtos.Add(produto);
                }
                else
                {
                    Console.WriteLine($"Produto com ID {id} não encontrado.");
                }
            }

            if (venda.Produtos.Count > 0)
            {
                vendas.Add(venda);
                Console.WriteLine("Venda registrada com sucesso!");
            }
            else
            {
                Console.WriteLine("Nenhum produto válido foi adicionado à venda.");
            }
        }

        public void ListarVendas()
        {
            if (vendas.Count == 0)
            {
                Console.WriteLine("Nenhuma venda registrada.");
                return;
            }

            foreach (var venda in vendas)
            {
                Console.WriteLine(venda);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SistemaVendas sistema = new SistemaVendas();

            while (true)
            {
                Console.WriteLine("\n===== Sistema de Controle de Vendas =====");
                Console.WriteLine("1. Adicionar Produto");
                Console.WriteLine("2. Listar Produtos");
                Console.WriteLine("3. Registrar Venda");
                Console.WriteLine("4. Listar Vendas");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.Write("Nome do Produto: ");
                        string nome = Console.ReadLine();
                        Console.Write("Preço do Produto: ");
                        decimal preco = decimal.Parse(Console.ReadLine());

                        var produto = new Produto
                        {
                            Nome = nome,
                            Preco = preco
                        };
                        sistema.AdicionarProduto(produto);
                        break;

                    case "2":
                        sistema.ListarProdutos();
                        break;

                    case "3":
                        sistema.ListarProdutos();
                        Console.WriteLine("Digite os IDs dos produtos vendidos separados por vírgula:");
                        string[] idsInput = Console.ReadLine().Split(',');
                        List<int> idsProdutos = new List<int>();

                        foreach (var idStr in idsInput)
                        {
                            if (int.TryParse(idStr.Trim(), out int id))
                            {
                                idsProdutos.Add(id);
                            }
                        }

                        sistema.RegistrarVenda(idsProdutos);
                        break;

                    case "4":
                        sistema.ListarVendas();
                        break;

                    case "5":
                        Console.WriteLine("Saindo do sistema...");
                        return;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
