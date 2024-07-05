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
        public DateTime DataDevolucao { get; set; } // Adiciona a data de devolução
    }
}