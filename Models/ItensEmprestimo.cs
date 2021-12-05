namespace ExercicioBiblioteca.Models
{
    public class ItensEmprestimo
    {
        public int CodigoItem { get; set; }
        public Emprestimo Emprestimo { get; set; }
        public int NumeroEmprestimo { get; set; }
        public Livro Livro { get; set; }
        public int CodigoLivro { get; set; }
    }
}
