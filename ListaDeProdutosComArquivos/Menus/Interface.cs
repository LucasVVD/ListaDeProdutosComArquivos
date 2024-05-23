using System;
using System.Globalization;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListaDeProdutosComArquivos.Entities;
using ListaDeProdutosComArquivos.Entities.Enums;

namespace ListaDeProdutosComArquivos.Menus
{
    internal class Interface
    {
        private static List<Products> products = new List<Products>();

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("\t\tBem-Vindo!\n");

            bool leave = false;
            while (!leave)
            {
                Console.WriteLine("==================================================\n");

                Console.WriteLine("Selecione algumas das opções abaixo:\n");
                Console.WriteLine("1) Criar pasta e arquivo\n2) Abrir arquivo existente\n");
                Console.Write("Opção: ");
                char option = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();

                if (option == '1')
                    CreateFolder();
                if (option == '2')
                {
                    Console.Write("\nDigite o caminho: ");
                    string filePath = Console.ReadLine();
                    OpenFile(filePath);
                }
                if (option == '6')
                    leave = true;
            }
        }

        private static void CreateFolder()
        {
            Console.Clear();
            try
            {
                Console.Write("Digite o nome da pasta: ");
                string folderName = Console.ReadLine();
                string directoryPath = $@"E:\Curso C# POO\ListaDeProdutosComArquivos\{folderName?.ToUpper()[0]}{folderName?.Substring(1)}\";

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                CreateFile(directoryPath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void CreateFile(string path)
        {
            Console.WriteLine();
            Console.WriteLine("==================================================\n");
            Console.WriteLine("Crie um arquivo e selecione o tipo da extensão em seguida:\n");

            bool selected = false;
            while (!selected)
            {
                Console.Write("Nome do arquivo: ");

                string fileName = $"{Console.ReadLine()}.";

                Console.WriteLine();
                Console.WriteLine("==================================================\n");

                Console.WriteLine("Selecione uma dessas extensões abaixo: ");
                Console.WriteLine("\n1) txt\n2) csv\n3) ini");
                Console.Write("\nExtenção: ");
                char option = Console.ReadKey().KeyChar;
                Console.WriteLine();

                string? extension = option switch
                {
                    '1' => "txt",
                    '2' => "csv",
                    '3' => "ini",
                    _ => null
                };

                if (extension == null)
                {
                    Console.WriteLine("Selecione uma opção válida");
                    continue;
                }

                string filePath = $"{path}{fileName.ToUpper()[0]}{fileName.Substring(1)}{extension}";
                if (!File.Exists(filePath))
                {
                    try
                    {
                        Console.WriteLine("\nArquivo criado com sucesso!");
                        using (FileStream fs = File.Create(filePath)) { }
                        selected = true;
                        Console.WriteLine();
                        Console.WriteLine("Deseja adicionar os produtos? (s/n)");
                        char addProdOption = char.ToLower(Console.ReadKey().KeyChar);
                        Console.WriteLine();

                        if (addProdOption == 's')
                        {
                            AddProduct(filePath);
                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Arquivo Existente! Escolha outro nome\n");
                }
            }
        }

        private static void OpenFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadToEnd();
                        Console.WriteLine(line);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void AddProduct(string path)
        {
            Console.Write("Quantidade de produtos à ser adicionados: ");
            int numQuantity = int.Parse(Console.ReadLine());
            int count = 1;
            for (int i = 0; i < numQuantity; i++)
            {
                Console.Clear();
                Console.WriteLine($"Produto #{count}:\n");
                Console.Write("Nome do produto: ");
                string prodName = Console.ReadLine();
                Console.Write("Preço: ");
                double prodPrice = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.Write("Quantidade: ");
                int prodQuantity = int.Parse(Console.ReadLine());
                Console.Write("Categoria: ");
                Console.WriteLine("\n1) Alimento\n2) Eletrodomestico\n3) Celular\n4) Movel");
                Category prodCategory = Enum.Parse<Category>(Console.ReadLine());
                count++;

                products.Add(new Products(prodName, prodPrice, prodQuantity, prodCategory));
                Console.WriteLine();
                using (StreamWriter sw = new(path))
                {
                    int countProd = 1;
                    if (path.Contains(".csv"))
                    {
                        sw.WriteLine("Nome;Preço;Quantidade;Categoria;Horário de registro;Preço total em estoque");
                        foreach (Products prod in products)
                        {
                            sw.WriteLine($"{prod.ProdName};R${prod.ProdPrice:F2};{prod.Quantity};{prod.ProdCategory};{prod.AddLog:yyyy/MM/dd HH:mm:ss};R${prod.TotalPrice():F2}");
                        }
                    }
                    else
                    {
                    foreach (Products prod in products)
                    {
                        sw.WriteLine($"Produto #{countProd}:\n");
                        sw.Write($"Nome: {prod.ProdName}\nPreço: R${prod.ProdPrice:F2}\nQuantidade: {prod.Quantity}\n" +
                            $"Categoria: {prod.ProdCategory}\nHora de registro: {prod.AddLog:yyyy/MM/dd HH:mm:ss}\nPreço total em estoque: R${prod.TotalPrice():F2}\n");
                        countProd++;
                    }
                }
            }
        }
    }
}
