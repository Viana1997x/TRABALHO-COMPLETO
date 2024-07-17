using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porjeto
{

    public class Emprestimo
    {
        public int Id { get; set; }
        public Livro Livro { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }

        public static void VerificarEmprestimo()
        {
            Console.Clear();
            Console.WriteLine("=== Verificar Emprestimo ===");

            Console.Write("ID do Livro: ");
            if (!int.TryParse(Console.ReadLine(), out int livroId))
            {
                Console.WriteLine("ID inválido. Tente novamente.");
                Console.ReadKey();
                return;
            }

            Livro livro = Biblioteca.Livros.Find(l => l.Id == livroId);
            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado.");
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
                Console.ReadKey();
                return;
            }

            if (livro.Disponivel)
            {
                Console.WriteLine("Livro disponível.");
            }
            else
            {
                Emprestimo emprestimo = Biblioteca.Emprestimos.Find(e => e.Livro.Id == livroId);
                if (emprestimo != null)
                {
                    Console.WriteLine($"Livro emprestado para Usuario ID: {emprestimo.Usuario.Id}, Nome: {emprestimo.Usuario.Nome}");
                }
            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
        }

        public static void AlterarEmprestimo()
        {
            Console.Clear();
            Console.WriteLine("=== Alterar Emprestimo ===");

            Console.Write("ID do Usuario: ");
            if (!int.TryParse(Console.ReadLine(), out int usuarioId))
            {
                Console.WriteLine("ID inválido. Tente novamente.");
                Console.ReadKey();
                return;
            }

            Emprestimo emprestimo = Biblioteca.Emprestimos.Find(e => e.Usuario.Id == usuarioId);
            if (emprestimo == null)
            {
                Console.WriteLine("Emprestimo não encontrado.");
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
                Console.ReadKey();
                return;
            }

            Livro.ExibirLivrosDisponiveis(false);

            Console.Write("Escolha o ID do novo Livro (1-15): ");
            if (!int.TryParse(Console.ReadLine(), out int novoLivroId) || novoLivroId < 1 || novoLivroId > 15)
            {
                Console.WriteLine("ID do livro inválido. Tente novamente.");
                Console.ReadKey();
                return;
            }

            Livro novoLivro = Biblioteca.Livros.Find(l => l.Id == novoLivroId);
            if (novoLivro == null || !novoLivro.Disponivel)
            {
                Console.WriteLine("Livro não disponível. Tente novamente.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nova Data de Devolução (dd/MM/yyyy): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime novaDataDevolucao))
            {
                Console.WriteLine("Data inválida. Tente novamente.");
                Console.ReadKey();
                return;
            }

            // Libera o livro antigo
            emprestimo.Livro.Disponivel = true;

            // Atualiza o empréstimo
            emprestimo.Livro = novoLivro;
            emprestimo.DataPrevistaDevolucao = novaDataDevolucao;

            // Marca o novo livro como indisponível
            novoLivro.Disponivel = false;

            // Atualiza o histórico
            var historico = Biblioteca.HistoricoEmprestimos.Find(h => h.IdDocumento == usuarioId);
            if (historico != null)
            {
                historico.DocumentoEmprestado = novoLivro;
                historico.DataDevolucao = novaDataDevolucao;
                historico.ObjHistorico = $"Emprestimo alterado para o livro {novoLivro.Titulo}";
            }

            Console.WriteLine("Emprestimo alterado com sucesso.");
            Console.ReadKey();
        }
    }
}