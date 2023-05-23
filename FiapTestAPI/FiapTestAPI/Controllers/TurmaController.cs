using FiapTestAPI.Database.Providers.Interfaces;
using FiapTestAPI.Database.Repositories.Interfaces;
using FiapTestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FiapTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaProvider _turmaProvider;
        private readonly ITurmaRepository _turmaRepository;

        public TurmaController(ITurmaProvider turmaProvider,
            ITurmaRepository turmaRepository)
        {
            this._turmaProvider = turmaProvider;
            this._turmaRepository = turmaRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<TurmaModel>> Get()
        {
            return await this._turmaProvider.Get();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<dynamic>> Delete(int id)
        {
            await this._turmaRepository.Delete(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<TurmaModel> Get(int id)
        {
            return await this._turmaProvider.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> PostAsync([FromForm] TurmaModel turmaModel)
        {           
            TurmaModel checkTurma = await this._turmaProvider.Get(turmaModel.Turma);

            if (checkTurma is not null)
            {
                return Conflict(new { message = "Turma já existe" });
            }
            else
            {
                if (DateTime.Now.Year <= turmaModel.Ano)
                {
                    await this._turmaRepository.Create(turmaModel);
                    return Ok();
                }
                else
                {
                    return Conflict(new { message = "Ano deve ser atual ou futuro" });
                }
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<dynamic>> PostAsync(int id, [FromForm] TurmaModel turmaModel)
        {
            TurmaModel checkTurma = await this._turmaProvider.Get(turmaModel.Turma);

            if (checkTurma is not null)
            {
                return Conflict(new { message = "Turma já existe" });
            }
            else
            {
                if (DateTime.Now.Year <= turmaModel.Ano)
                {
                    turmaModel.ID = id;
                    await this._turmaRepository.Update(turmaModel);                    

                    return Ok();
                }
                else
                {
                    return Conflict(new { message = "Ano deve ser atual ou futuro" });
                }
            }
        }

    }
}
