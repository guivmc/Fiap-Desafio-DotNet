using FiapTestAPI.Database.Providers.Interfaces;
using FiapTestAPI.Database.Repositories.Interfaces;
using FiapTestAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FiapTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacaoController : ControllerBase
    {
        private readonly IRelacaoProvider _relacaoProvider;
        private readonly IRelacaoRepository _relacaoRepository;

        public RelacaoController(IRelacaoProvider relacaoProvider,
            IRelacaoRepository relacaoRepository)
        {
            this._relacaoProvider = relacaoProvider;
            this._relacaoRepository = relacaoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<RelacaoModel>> Get()
        {
            return await this._relacaoProvider.Get();
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> PostAsync([FromForm] RelacaoModel model)
        {
            RelacaoModel checkAluno = await this._relacaoProvider.GetPorAlunoETurma(model.AlunoID, model.TurmaID);

            if (checkAluno is not null)
            {
                return Conflict(new { message = "Aluno já esta nesta turma" });
            }
            else
            {
                await this._relacaoRepository.Create(model);
                return Ok();
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<dynamic>> PostAsync(int id, [FromForm] RelacaoModel model)
        {
            RelacaoModel checkAluno = await this._relacaoProvider.GetPorAlunoETurma(id, model.TurmaID);

            if (checkAluno is not null)
            {
                return Conflict(new { message = "Aluno já esta nesta turma" });
            }
            else
            {
                await this._relacaoRepository.Update(model);
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<dynamic>> Delete(int id)
        {
            await this._relacaoRepository.Delete(id);
            return Ok();
        }
    }
}
