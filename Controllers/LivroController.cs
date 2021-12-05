using ExercicioBiblioteca.Context;
using ExercicioBiblioteca.InputModel;
using ExercicioBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ExercicioBiblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {  
        private readonly BibliotecaDbContext _bibiotecaDbContext;

        public LivroController(BibliotecaDbContext bibiotecaDbContext)
        {
            _bibiotecaDbContext = bibiotecaDbContext;
        }

        [HttpGet("filtrar-por-ano-lancamento")]
        public async Task<IActionResult> FiltrarPorAnoLancamento(int anoLancamento)
        {
            var livros = await _bibiotecaDbContext.Livros.Where(x => x.AnoLancamento == anoLancamento).ToListAsync();
            
            if (livros.Any())
            {
                return Ok(livros);
            }

            return NotFound("Nenhum livro encontrado!");
        }

        [HttpGet("filtrar-por-descricao")]
        public async Task<IActionResult> FiltrarPorDescricao(string descricao)
        {
            var livros = await _bibiotecaDbContext.Livros.Where(x => x.Descricao.Contains(descricao)).ToListAsync();

            if (livros.Any())
            {
                return Ok(livros);
            }

            return NotFound("Nenhum registro encontrado!");
        }

        [HttpGet("filtrar-por-isbn")]
        public async Task<IActionResult> FiltrarPorISBN(int isbn)
        {
            var livro = await _bibiotecaDbContext.Livros.Where(x => x.ISBN == isbn).FirstOrDefaultAsync();

            if (livro == null)
            {
                return NotFound("Livro não existe no banco de dados!");
            }
            
            return Ok(livro);
        }

        [HttpGet("filtrar-todos")]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _bibiotecaDbContext.Livros.ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarLivroInput dadosEntrada)
        {
            var livro = await _bibiotecaDbContext.Livros.Where(x => x.Codigo == dadosEntrada.Codigo).FirstOrDefaultAsync();

            if (livro == null)
            {
                return NotFound("Livro não existe!");
            }

            livro.Descricao = dadosEntrada.Descricao;
            livro.ISBN = dadosEntrada.ISBN;
            livro.AnoLancamento = dadosEntrada.AnoLancamento;

            _bibiotecaDbContext.Livros.Update(livro);
            await _bibiotecaDbContext.SaveChangesAsync();

            return Ok(livro);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarLivro(LivroInput dadosEntrada)
        {
            var livro = new Livro()
            {
                Descricao = dadosEntrada.Descricao,
                ISBN = dadosEntrada.ISBN,
                AnoLancamento = dadosEntrada.AnoLancamento,
                CodigoEditora = dadosEntrada.CodigoEditora,
                CodigoAutor = dadosEntrada.CodigoAutor
            };

            await _bibiotecaDbContext.Livros.AddAsync(livro);
            await _bibiotecaDbContext.SaveChangesAsync();

            return Ok(
                        new
                        {
                            success = true,
                            data = new
                            {
                                descricao = livro.Descricao,
                                isbn = livro.ISBN
                            }
                        }
                    );
        }

        [HttpDelete]
        public async Task<IActionResult> Deletar(int codigo)
        {
            var livro = await _bibiotecaDbContext.Livros.Where(x => x.Codigo == codigo).FirstOrDefaultAsync();

            if (livro == null)
            {
                return NotFound("Resgistro não encontrado!");
            }

            _bibiotecaDbContext.Livros.Remove(livro);
            await _bibiotecaDbContext.SaveChangesAsync();

            return Ok("Registro deletado com sucesso!");
        }
    }
}
