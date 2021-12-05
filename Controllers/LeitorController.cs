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
    public class LeitorController : ControllerBase
    {
        private readonly BibliotecaDbContext _bibiotecaDbContext;

        public LeitorController(BibliotecaDbContext bibiotecaDbContext)
        {
            _bibiotecaDbContext = bibiotecaDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _bibiotecaDbContext.Leitores.ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarLeitorInput dadosEntrada)
        {
            var leitor = await _bibiotecaDbContext.Leitores.Where(x => x.Codigo == dadosEntrada.Codigo).FirstOrDefaultAsync();

            if (leitor == null)
            {
                return NotFound("Leitor não existe!");
            }

            leitor.Nome = dadosEntrada.Nome;
            leitor.CPF = dadosEntrada.CPF;
            leitor.Email = dadosEntrada.Email;
            leitor.Telefone = dadosEntrada.Telefone;

            _bibiotecaDbContext.Leitores.Update(leitor);
            await _bibiotecaDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarLeitor(LeitorInput dadosEntrada)
        {
            var leitor = new Leitor()
            {
                Nome = dadosEntrada.Nome,
                CPF = dadosEntrada.CPF,
                Email = dadosEntrada.Email,
                Telefone = dadosEntrada.Telefone
            };

            await _bibiotecaDbContext.Leitores.AddAsync(leitor);
            await _bibiotecaDbContext.SaveChangesAsync();

            return Ok(
                        new
                        {
                            success = true,
                            data = new
                            {
                                codigoLeitor = leitor.Codigo,
                                email = leitor.Email
                            }
                        }
                    );
        }

        [HttpDelete]
        public async Task<IActionResult> Deletar(int codigo)
        {
            var leitor = await _bibiotecaDbContext.Leitores.Where(x => x.Codigo == codigo).FirstOrDefaultAsync();

            if (leitor == null)
            {
                return NotFound("Leitor não encontrado!");
            }

            _bibiotecaDbContext.Leitores.Remove(leitor);
            await _bibiotecaDbContext.SaveChangesAsync();

            return Ok("Leitor excluído com sucesso!");
        }
    }
}
