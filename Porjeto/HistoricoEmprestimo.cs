using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Porjeto
{

    public class HistoricoEmprestimo
    {
        public int IdHistorico { get; set; }
        public int IdDocumento { get; set; }
        public string ObjHistorico { get; set; }
        public Livro DocumentoEmprestado { get; set; }
        public DateTime DataDevolucao { get; set; }

        public static void ExibirHistoricoEmprestimo()
        {
            Console.Clear();
            Console.WriteLine("=== Historico Emprestimo ===");

            if (Biblioteca.HistoricoEmprestimos.Count == 0)
            {
                Console.WriteLine("Nenhum emprestimo registrado.");
                Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
                Console.ReadKey();
                return;
            }

            foreach (var historico in Biblioteca.HistoricoEmprestimos)
            {
                var usuario = Biblioteca.Usuarios.FirstOrDefault(u => u.Id == historico.IdDocumento);
                if (usuario != null)
                {
                    Console.WriteLine($"ID: {historico.IdDocumento}, Nome: {usuario.Nome}, Livro: {historico.DocumentoEmprestado.Titulo}, Email: {usuario.Email}, Data Devolução: {historico.DataDevolucao.ToString("dd/MM/yyyy")}, Histórico: {historico.ObjHistorico}");
                }
            }

            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
        }
    }
}