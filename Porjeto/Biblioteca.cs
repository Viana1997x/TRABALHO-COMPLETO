using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porjeto
{
    public static class Biblioteca
    {
        public static List<Livro> Livros = new List<Livro>();
        public static List<Usuario> Usuarios = new List<Usuario>();
        public static List<Emprestimo> Emprestimos = new List<Emprestimo>();
        public static List<HistoricoEmprestimo> HistoricoEmprestimos = new List<HistoricoEmprestimo>();

        public static void InicializarDados()
        {
            for (int i = 1; i <= 15; i++)
            {
                Livros.Add(new Livro { Id = i, Titulo = $"Livro {i}", Autor = $"Autor {i}", ISBN = $"ISBN{i}", Disponivel = true });
            }
        }

        public static void ExibirMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Gerenciamento de Biblioteca ===");
                Console.WriteLine("1. Registro de Emprestimo");
                Console.WriteLine("2. Livros Disponiveis");
                Console.WriteLine("3. Verificar Emprestimo");
                Console.WriteLine("4. Historico Emprestimo");
                Console.WriteLine("5. Alterar Emprestimo");
                Console.WriteLine("6. Alterar Usuario e Deletar Usuario");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Usuario.RegistrarEmprestimo();
                        break;
                    case "2":
                        Livro.ExibirLivrosDisponiveis(true);
                        break;
                    case "3":
                        Emprestimo.VerificarEmprestimo();
                        break;
                    case "4":
                        HistoricoEmprestimo.ExibirHistoricoEmprestimo();
                        break;
                    case "5":
                        Emprestimo.AlterarEmprestimo();
                        break;
                    case "6":
                        ExibirSubmenuUsuario();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void ExibirSubmenuUsuario()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Submenu de Usuario ===");
                Console.WriteLine("1. Alterar Usuario");
                Console.WriteLine("2. Deletar Usuario");
                Console.WriteLine("0. Voltar ao Menu Principal");
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Usuario.AlterarUsuario();
                        break;
                    case "2":
                        Usuario.DeletarUsuario();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
