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
    public class AutoresController : ControllerBase
    {
        private readonly BibliotecaDbContext _bibiotecaDbContext;

        public AutoresController(BibliotecaDbContext bibiotecaDbContext)
        {
            _bibiotecaDbContext = bibiotecaDbContext;
        }

        [HttpGet("filtrar-por-nome")]
        public async Task<IActionResult> FiltrarPorNome(string nome)
        {
            var autores = await _bibiotecaDbContext.Autores.Where(x => x.Nome.Contains(nome)).ToListAsync();

            if (autores.Any())
            {
                return Ok(autores);
            }

            return NotFound("Nenhum dado encontrado!");
        }

        [HttpGet("listar-todos")]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _bibiotecaDbContext.Autores.ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(AtualizarAutorInput dadosEntrada)
        {
            var autor = await _bibiotecaDbContext.Autores.Where(x => x.Codigo == dadosEntrada.Codigo).FirstOrDefaultAsync();

            if (autor == null)
            {
                return NotFound("Autor não encontrado!");
            }

            autor.Nome = dadosEntrada.Nome;
            _bibiotecaDbContext.Autores.Update(autor);
            await _bibiotecaDbContext.SaveChangesAsync();

            return Ok(autor);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarAutor(AutorInput dadosEntrada)
        {
            var autor = new Autor()
            {
                Nome = dadosEntrada.Nome
            };

            await _bibiotecaDbContext.Autores.AddAsync(autor);
            await _bibiotecaDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Deletar(int codigo)
        {
            var autor = await _bibiotecaDbContext.Autores.Where(x => x.Codigo == codigo).FirstOrDefaultAsync();

            if (autor == null)
            {
                return NotFound("Autor não encontrado!");
            }

            _bibiotecaDbContext.Autores.Remove(autor);
            await _bibiotecaDbContext.SaveChangesAsync();

            return Ok("Autor excluído com sucesso!");
        }
    }
}
