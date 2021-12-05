using System;
using System.Collections;
using System.Collections.Generic;

namespace ExercicioBiblioteca.Models
{
    public class Emprestimo
    {
        public int Numero { get; set; }
        public Leitor Leitor { get; set; }
        public int CodigoLeitor { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public ICollection<ItensEmprestimo> Itens { get; set; }
    }
}
