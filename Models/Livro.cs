namespace ExercicioBiblioteca.Models
{
    public class Livro
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public int ISBN { get; set; }
        public int AnoLancamento { get; set; }
        public Autor Autor { get; set; }
        public int CodigoAutor { get; set; }
        public string CodigoEditora { get; set; }
    }

}
