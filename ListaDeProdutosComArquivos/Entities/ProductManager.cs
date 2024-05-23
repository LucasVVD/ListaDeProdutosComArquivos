using ListaDeProdutosComArquivos.Entities.ClearConsoleCooldown;
using ListaDeProdutosComArquivos.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeProdutosComArquivos.Entities
{
    internal static class ProductManager
    {
        static List<Products> products = new List<Products>();

        public static void AddProduct(string path)
        {
            Console.Write("Quantidade de produtos à ser adicionados: ");
            int numQuantity = int.Parse(Console.ReadLine());
            for (int i = 0; i < numQuantity; i++)
            {
                Console.Clear();
                Console.WriteLine($"Produto #{i + 1}:\n");
                Console.Write("Nome do produto: ");

                string prodName = Console.ReadLine();
                Console.Write("Preço: ");
                double prodPrice = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                Console.Write("Quantidade: ");
                int prodQuantity = int.Parse(Console.ReadLine());

                Console.Write("Categoria: ");
                Console.WriteLine("\n1) Alimento\n2) Eletrodomestico\n3) Celular\n4) Movel");
                Category prodCategory = Enum.Parse<Category>(Console.ReadLine());

                products.Add(new Products(prodName, prodPrice, prodQuantity, prodCategory));
                Console.WriteLine();

                using (StreamWriter sw = new(path))
                {
                    int countProd = 1;
                    if (path.Contains(Path.GetExtension("csv")))
                    {
                        sw.WriteLine("Nome;Preço;Quantidade;Categoria;Horário de registro;Preço total em estoque;Id");
                        foreach (Products prod in products)
                        {
                            sw.WriteLine($"{prod.ProdName};R${prod.ProdPrice:F2};{prod.Quantity};{prod.ProdCategory};{prod.AddLog:yyyy/MM/dd HH:mm:ss};" +
                                $"R${prod.TotalPrice():F2};{prod.RandomId()}");
                        }
                    }
                    else
                    {
                        foreach (Products prod in products)
                        {
                            sw.WriteLine($"Produto #{countProd}:\n");
                            sw.WriteLine($"Nome: {prod.ProdName}\nPreço: R${prod.ProdPrice:F2}\nQuantidade: {prod.Quantity}\n" +
                                $"Categoria: {prod.ProdCategory}\nHora de registro: {prod.AddLog:yyyy/MM/dd HH:mm:ss}\nPreço total em estoque: R${prod.TotalPrice():F2}\nId: {prod.RandomId()}\n");
                            countProd++;
                        }
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Produtos registrados com sucesso!");
            ClearConsole.Cooldown();
        }
    }
}
