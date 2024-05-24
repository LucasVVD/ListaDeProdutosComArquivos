using ListaDeProdutosComArquivos.Entities.ClearConsoleCooldown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaDeProdutosComArquivos.Entities.Files
{
    internal static class FileManager
    {
        static readonly string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static void CreateFolder()
        {
            Console.Clear();
            try
            {
                Console.Write("Digite o nome da pasta: ");
                string folderName = Console.ReadLine();
                string directoryPath = Path.Combine(myDocumentsFolder, char.ToUpper(folderName[0]) + folderName.Substring(1));

                if (VerifyDirectory(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Console.WriteLine($"Pasta criada com sucesso no diretório: {directoryPath}\n");
                    ClearConsole.Cooldown();
                }
                CreateFile(directoryPath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ListFiles()
        {
            Console.Clear();
            Console.Write("Nome da pasta: ");
            try
            {
                string folderName = Console.ReadLine();
                if (VerifyEmptyStrings(folderName))
                {
                    Console.WriteLine("O campo não pode estar vazio.");
                    Console.WriteLine("Redirecionando para o Menu inicial.\n");
                    ClearConsole.Cooldown();
                    return;
                }

                string folderPath = Path.Combine(myDocumentsFolder, char.ToUpper(folderName[0]) + folderName.Substring(1));

                string[] files = Directory.GetFiles(folderPath);

                if (files.Length == 0)
                {
                    Console.WriteLine($"Pasta '{folderName}' está vazia ou não existe");
                    return;
                }

                Console.WriteLine("Arquivos:\n");
                Console.WriteLine($"{files.Length} arquivos encontrados:\n");

                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
                }
                Console.WriteLine();

                if (int.TryParse(Console.ReadLine(), out int fileIndex) && fileIndex > 0 && fileIndex <= files.Length)
                {
                    string selectedFile = files[fileIndex - 1];
                    OpenFile(selectedFile);
                }
                else
                {
                    Console.WriteLine("Arquivo inválido ou fora da seleção");
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Nenhum caminho foi encontrado");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Acesso negado");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Pasta não encontrada");
            }
        }

        static void ListFolder()
        {
            string[] listFolders = Directory.GetDirectories(myDocumentsFolder);

            for (int i = 0; i < listFolders.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {listFolders[i]}");
            }
            Console.WriteLine();
        }

        public static void CreateFile(string path)
        {
            Console.WriteLine();
            Console.WriteLine("==================================================\n");
            Console.WriteLine("Crie um arquivo e selecione o tipo da extensão em seguida:\n");

            bool selected = false;
            while (!selected)
            {
                Console.Write("Nome do arquivo: ");
                string fileName = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("==================================================\n");

                Console.WriteLine("Selecione uma dessas extensões abaixo: ");
                Console.WriteLine("\n1) .txt\n2) .csv\n3) .ini");
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

                string filePath = Path.Combine(path, $"{char.ToUpper(fileName[0])}{fileName.Substring(1)}.{extension}");
                if (!File.Exists(filePath))
                {
                    try
                    {
                        Console.WriteLine("\nArquivo criado com sucesso!");
                        using (FileStream fs = File.Create(filePath)) { }
                        selected = true;
                        ClearConsole.Cooldown();

                        Console.Write("Deseja adicionar algum produto? (s/n) ");
                        char addProdOption = char.ToLower(Console.ReadKey().KeyChar);
                        Console.WriteLine();

                        if (addProdOption == 's')
                        {
                            ProductManager.AddProduct(filePath);
                        }
                        else
                        {
                            Console.Clear();
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

        static void OpenFile(string path)
        {
            try
            {
                using (StreamReader sr = new(path))
                {
                    string content = File.ReadAllText(path);
                    if (string.IsNullOrEmpty(content))
                    {
                        Console.WriteLine("O arquivo esta vazio");
                        ClearConsole.Cooldown();
                        return;
                    }
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadToEnd();
                        Console.WriteLine(line);
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static bool VerifyDirectory(string? path)
        {
            return !Directory.Exists(path);
        }

        static bool VerifyEmptyStrings(string name)
        {
            return string.IsNullOrEmpty(name);
        }
    }
}
