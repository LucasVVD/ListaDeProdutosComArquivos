using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListaDeProdutosComArquivos.Entities;
using ListaDeProdutosComArquivos.Entities.Enums;
using ListaDeProdutosComArquivos.Entities.Files;
using ListaDeProdutosComArquivos.Entities.ClearConsoleCooldown;

namespace ListaDeProdutosComArquivos.Menus
{
    internal class Interface
    {
        public static void Menu()
        {
            Console.Clear();
            bool leave = false;
            while (!leave)
            {
                Console.WriteLine("\t\tBem-Vindo!\n");
                Console.WriteLine("==================================================\n");
                Console.WriteLine("Selecione algumas das opções abaixo:\n");
                Console.WriteLine("1) Criar uma pasta\n2) Criar um arquivo\n3) Deletar alguma pasta\n");
                Console.Write("Opção: ");
                char option = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();

                switch (option)
                {
                    case '1':
                        FileManager.CreateFolder();
                        break;
                    case '2':
                        FileManager.ListFiles();
                        break;
                    case '3':
                        FileManager.DeleteFolder();
                        break;
                    case '4':
                        leave = true;
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida");
                        ClearConsole.Cooldown();
                        break;
                }
            }
        }
    }
}
