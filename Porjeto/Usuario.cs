using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porjeto
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public static void RegistrarEmprestimo()
        {
            Console.Clear();
            Console.WriteLine("=== Registro de Emprestimo ===");

            Console.Write("ID do Usuario: ");
            if (!int.TryParse(Console.ReadLine(), out int usuarioId))
            {
                Console.WriteLine("ID inválido. Tente novamente.");
                Console.ReadKey();
                return;
            }

            if (Biblioteca.Usuarios.Exists(u => u.Id == usuarioId))
            {
                Console.WriteLine("ID de usuario já existe. Tente novamente.");
                Console.ReadKey();
                return;
            }

            Usuario usuario = new Usuario { Id = usuarioId };
            Console.Write("Nome do Usuario: ");
            usuario.Nome = Console.ReadLine();
            Console.Write("Email do Usuario: ");
            usuario.Email = Console.ReadLine();

            Livro.ExibirLivrosDisponiveis(false);

            Console.Write("Escolha o ID do Livro (1-15): ");
            if (!int.TryParse(Console.ReadLine(), out int livroId) || livroId < 1 || livroId > 15)
            {
                Console.WriteLine("ID do livro inválido. Tente novamente.");
                Console.ReadKey();
                return;
            }

            Livro livro = Biblioteca.Livros.Find(l => l.Id == livroId);
            if (livro == null || !livro.Disponivel)
            {
                Console.WriteLine("Livro não disponível. Tente novamente.");
                Console.ReadKey();
                return;
            }

            Console.Write("Data de Devolução (dd/MM/yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataDevolucao))
            {
                Console.WriteLine("Data inválida. Tente novamente.");
                Console.ReadKey();
                return;
            }

            livro.Disponivel = false;
            Biblioteca.Usuarios.Add(usuario);

            Emprestimo emprestimo = new Emprestimo
            {
                Id = Biblioteca.Emprestimos.Count + 1,
                Livro = livro,
                Usuario = usuario,
                DataEmprestimo = DateTime.Now,
                DataPrevistaDevolucao = dataDevolucao
            };

            Biblioteca.Emprestimos.Add(emprestimo);
            Biblioteca.HistoricoEmprestimos.Add(new HistoricoEmprestimo
            {
                IdHistorico = Biblioteca.HistoricoEmprestimos.Count + 1,
                IdDocumento = usuario.Id,
                ObjHistorico = $"Emprestado para {usuario.Nome}",
                DocumentoEmprestado = livro,
                DataDevolucao = dataDevolucao
            });

            Console.WriteLine("Emprestimo registrado com sucesso.");
            Console.ReadKey();
        }

        public static void AlterarUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== Alterar Usuario ===");

            Console.Write("ID do Usuario: ");
            if (!int.TryParse(Console.ReadLine(), out int usuarioId))
            {
                Console.WriteLine("ID inválido. Tente novamente.");
                Console.ReadKey();
                return;
            }

            Usuario usuario = Biblioteca.Usuarios.Find(u => u.Id == usuarioId);
            if (usuario == null)
            {
                Console.WriteLine("Usuario não encontrado.");
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
                Console.ReadKey();
                return;
            }

            Console.Write("Novo Nome do Usuario: ");
            usuario.Nome = Console.ReadLine();
            Console.Write("Novo Email do Usuario: ");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Usuario alterado com sucesso.");
            Console.ReadKey();
        }

        public static void DeletarUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== Deletar Usuario ===");

            Console.Write("ID do Usuario: ");
            if (!int.TryParse(Console.ReadLine(), out int usuarioId))
            {
                Console.WriteLine("ID inválido. Tente novamente.");
                Console.ReadKey();
                return;
            }

            Usuario usuario = Biblioteca.Usuarios.Find(u => u.Id == usuarioId);
            if (usuario == null)
            {
                Console.WriteLine("Usuario não encontrado.");
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
                Console.ReadKey();
                return;
            }

            Emprestimo emprestimo = Biblioteca.Emprestimos.Find(e => e.Usuario.Id == usuarioId);
            if (emprestimo != null)
            {
                Biblioteca.Emprestimos.Remove(emprestimo);
                emprestimo.Livro.Disponivel = true;
            }

            Biblioteca.Usuarios.Remove(usuario);
            Biblioteca.HistoricoEmprestimos.RemoveAll(h => h.IdDocumento == usuarioId);

            Console.WriteLine("Usuario deletado com sucesso.");
            Console.ReadKey();
        }
    }
}