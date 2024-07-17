using Porjeto.LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porjeto
{
    public class Livro : Documento
    {
        public string ISBN { get; set; }

        public static void ExibirLivrosDisponiveis(bool mostrarVoltarMenu)
        {
            Console.Clear();
            Console.WriteLine("=== Livros Disponiveis ===");

            int count = 0;
            foreach (var livro in Biblioteca.Livros)
            {
                if (livro.Disponivel)
                {
                    Console.WriteLine($"ID: {livro.Id}, Titulo: {livro.Titulo}, Autor: {livro.Autor}, ISBN: {livro.ISBN}");
                    count++;
                    if (count == 15)
                    {
                        break;
                    }
                }
            }

            if (count == 0)
            {
                Console.WriteLine("Nenhum livro disponível no momento.");
            }

            if (mostrarVoltarMenu)
            {
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
                Console.ReadKey();
            }
        }
    }
}