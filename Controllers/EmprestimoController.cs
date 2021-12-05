using ExercicioBiblioteca.Context;
using ExercicioBiblioteca.InputModel;
using ExercicioBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExercicioBiblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimoController : ControllerBase
    {
        private readonly BibliotecaDbContext _bibiotecaDbContext;

        public EmprestimoController(BibliotecaDbContext bibiotecaDbContext)
        {
            _bibiotecaDbContext = bibiotecaDbContext;
        }

        [HttpGet("listar-emprestimo")]
        public async Task<IActionResult> ListarEmprestimos()
        {
            return Ok(await _bibiotecaDbContext.Emprestimos
                                .Include(x => x.Leitor)
                                .Include(x => x.Itens)
                                    .ThenInclude(x => x.Livro)
                                .ToListAsync());
        }

        [HttpPost("cadastrar-emprestimo")]
        public async Task<IActionResult> CadastrarEmprestimo(EmprestimoInput dadosEntrada)
        {
            var emprestimo = new Emprestimo()
            {
                CodigoLeitor = dadosEntrada.CodigoLeitor,
                DataEmprestimo = DateTime.Now,
                DataDevolucao = DateTime.Now.AddDays(3)
            };

            await _bibiotecaDbContext.Emprestimos.AddAsync(emprestimo);
            await _bibiotecaDbContext.SaveChangesAsync();

            return Ok(
                        new
                        {
                            numeroEmprestimoGerado = emprestimo.Numero
                        }
                    );
        }

        [HttpGet("listar-itens-emprestimo")]
        public async Task<IActionResult> ListarItensEmprestimos()
        {
            return Ok(await _bibiotecaDbContext.ItensEmprestimos.ToListAsync());
        }

        [HttpPost("cadastrar-item-emprestimo")]
        public async Task<IActionResult> CadastrarItensEmprestimo(ItensEmprestimoInput dadosEntrada)
        {
            var livro = await _bibiotecaDbContext.Livros.Where(x => x.Codigo == dadosEntrada.CodigoLivro).FirstOrDefaultAsync();
            if (livro != null)
            {

            var itensEmprestimo = new ItensEmprestimo()
            {
                NumeroEmprestimo = dadosEntrada.NumeroEmprestimo,
                CodigoLivro = dadosEntrada.CodigoLivro
            };

            await _bibiotecaDbContext.ItensEmprestimos.AddAsync(itensEmprestimo);
            await _bibiotecaDbContext.SaveChangesAsync();
            
            
            return Ok();
            }

            return NotFound("Livro não encontrado!");
        }
    }
}
