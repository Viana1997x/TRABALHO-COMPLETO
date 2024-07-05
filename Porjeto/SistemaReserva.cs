using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Porjeto;

namespace Porjeto
{


    public class SistemaReserva
    {
        public int Id { get; set; }
        public Livro Livro { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }
        public int QuantidadeLivros { get; set; }
        public int NumeroReserva { get; set; }

        public void LivroReservado()
        {
            // Implementação da lógica para reserva do livro
        }
    }
}