using System;
using System.Collections.Generic;
using Porjeto;

public class Program
{
    static List<Livro> livros = new List<Livro>();
    static List<Usuario> usuarios = new List<Usuario>();
    static List<Emprestimo> emprestimos = new List<Emprestimo>();
    static List<HistoricoEmprestimo> historicoEmprestimos = new List<HistoricoEmprestimo>();

    public static void Main()
    {
        InicializarDados();
        ExibirMenu();
    }

    static void InicializarDados()
    {
        for (int i = 1; i <= 15; i++)
        {
            livros.Add(new Livro { Id = i, Titulo = $"Livro {i}", Autor = $"Autor {i}", ISBN = $"ISBN{i}", Disponivel = true });
        }
    }

    static void ExibirMenu()
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
                    RegistroEmprestimo();
                    break;
                case "2":
                    ExibirLivrosDisponiveis(true);
                    break;
                case "3":
                    VerificarEmprestimo();
                    break;
                case "4":
                    ExibirHistoricoEmprestimo();
                    break;
                case "5":
                    AlterarEmprestimo();
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

    static void RegistroEmprestimo()
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

        if (usuarios.Exists(u => u.Id == usuarioId))
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

        ExibirLivrosDisponiveis(false);

        Console.Write("Escolha o ID do Livro (1-15): ");
        if (!int.TryParse(Console.ReadLine(), out int livroId) || livroId < 1 || livroId > 15)
        {
            Console.WriteLine("ID do livro inválido. Tente novamente.");
            Console.ReadKey();
            return;
        }

        Livro livro = livros.Find(l => l.Id == livroId);
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
        usuarios.Add(usuario);

        Emprestimo emprestimo = new Emprestimo
        {
            Id = emprestimos.Count + 1,
            Livro = livro,
            Usuario = usuario,
            DataEmprestimo = DateTime.Now,
            DataPrevistaDevolucao = dataDevolucao
        };

        emprestimos.Add(emprestimo);
        historicoEmprestimos.Add(new HistoricoEmprestimo
        {
            IdHistorico = historicoEmprestimos.Count + 1,
            IdDocumento = usuario.Id,
            ObjHistorico = $"Emprestado para {usuario.Nome}",
            DocumentoEmprestado = livro,
            DataDevolucao = dataDevolucao
        });

        Console.WriteLine("Emprestimo registrado com sucesso.");
        Console.ReadKey();
    }

    static void ExibirLivrosDisponiveis(bool mostrarVoltarMenu)
    {
        Console.Clear();
        Console.WriteLine("=== Livros Disponiveis ===");

        int count = 0;
        foreach (var livro in livros)
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

    static void VerificarEmprestimo()
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

        Emprestimo emprestimo = emprestimos.Find(e => e.Livro.Id == livroId);
        if (emprestimo == null)
        {
            Console.WriteLine("Livro disponível.");
        }
        else
        {
            Console.WriteLine($"Livro emprestado para {emprestimo.Usuario.Nome} (ID: {emprestimo.Usuario.Id}).");
        }
        Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
        Console.ReadKey();
    }

    static void ExibirHistoricoEmprestimo()
    {
        Console.Clear();
        Console.WriteLine("=== Historico de Emprestimos ===");

        if (historicoEmprestimos.Count == 0)
        {
            Console.WriteLine("Nenhum emprestimo registrado.");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }

        foreach (var historico in historicoEmprestimos)
        {
            var usuario = usuarios.Find(u => u.Id == historico.IdDocumento);
            if (usuario != null)
            {
                Console.WriteLine($"ID Usuario: {usuario.Id}, Nome: {usuario.Nome}, Livro: {historico.DocumentoEmprestado.Titulo}, Email: {usuario.Email}, Data de Devolução: {historico.DataDevolucao:dd/MM/yyyy}");
            }
        }
        Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
        Console.ReadKey();
    }

    static void AlterarEmprestimo()
    {
        Console.Clear();
        Console.WriteLine("=== Alterar Emprestimo ===");

        if (emprestimos.Count == 0)
        {
            Console.WriteLine("Nenhum emprestimo registrado.");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }

        Console.Write("ID do Usuario: ");
        if (!int.TryParse(Console.ReadLine(), out int usuarioId))
        {
            Console.WriteLine("ID inválido. Tente novamente.");
            Console.ReadKey();
            return;
        }

        Emprestimo emprestimo = emprestimos.Find(e => e.Usuario.Id == usuarioId);
        if (emprestimo == null)
        {
            Console.WriteLine("Emprestimo não encontrado.");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }

        ExibirLivrosDisponiveis(false);

        Console.Write("Escolha o ID do novo Livro (1-15): ");
        if (!int.TryParse(Console.ReadLine(), out int novoLivroId) || novoLivroId < 1 || novoLivroId > 15)
        {
            Console.WriteLine("ID do livro inválido. Tente novamente.");
            Console.ReadKey();
            return;
        }

        Livro novoLivro = livros.Find(l => l.Id == novoLivroId);
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
        var historico = historicoEmprestimos.Find(h => h.IdDocumento == usuarioId);
        if (historico != null)
        {
            historico.DocumentoEmprestado = novoLivro;
            historico.DataDevolucao = novaDataDevolucao;
            historico.ObjHistorico = $"Emprestimo alterado para o livro {novoLivro.Titulo}";
        }

        Console.WriteLine("Emprestimo alterado com sucesso.");
        Console.ReadKey();
    }

    static void ExibirSubmenuUsuario()
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
                    AlterarUsuario();
                    break;
                case "2":
                    DeletarUsuario();
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

    static void AlterarUsuario()
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

        Usuario usuario = usuarios.Find(u => u.Id == usuarioId);
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

    static void DeletarUsuario()
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

        Usuario usuario = usuarios.Find(u => u.Id == usuarioId);
        if (usuario == null)
        {
            Console.WriteLine("Usuario não encontrado.");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }

        Emprestimo emprestimo = emprestimos.Find(e => e.Usuario.Id == usuarioId);
        if (emprestimo != null)
        {
            emprestimos.Remove(emprestimo);
            emprestimo.Livro.Disponivel = true;
        }

        usuarios.Remove(usuario);
        historicoEmprestimos.RemoveAll(h => h.IdDocumento == usuarioId);

        Console.WriteLine("Usuario deletado com sucesso.");
        Console.ReadKey();
    }
}
