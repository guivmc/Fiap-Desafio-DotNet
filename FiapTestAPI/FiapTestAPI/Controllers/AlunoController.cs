using FiapTestAPI.Database.Providers.Interfaces;
using FiapTestAPI.Database.Repositories.Interfaces;
using FiapTestAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.RegularExpressions;
using static FiapTestAPI.Database.Providers.Interfaces.IAlunoProvider;

namespace FiapTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoProvider _alunoProvider;
        private readonly IAlunoRepository _alunoRepository;

        public AlunoController(IAlunoProvider alunoProvider,
            IAlunoRepository alunoRepository)
        {
            this._alunoProvider = alunoProvider;
            this._alunoRepository = alunoRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<AlunoModel>> Get()
        {
            return await this._alunoProvider.Get();
        }

        [HttpGet("{id}")]
        public async Task<AlunoModel> Get(int id)
        {
            return await this._alunoProvider.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> PostAsync([FromForm] AlunoModel aluno)
        {
            AlunoModel checkAluno = await this._alunoProvider.Get(aluno.Usuario);

            if (checkAluno is not null)
            {
                return Conflict(new { message = "Usuário já existe" });
            }
            else
            {
                if (this.ValidaSenha(aluno.Senha))
                {

                    string hashedPassword = new PasswordHasher<string>().HashPassword(aluno.Usuario, aluno.Senha);

                    aluno.Senha = hashedPassword;

                    await this._alunoRepository.Create(aluno);
                    return Ok();
                }
                else
                {
                    return Conflict(new { message = "Senha Fraca" });
                }
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<dynamic>> PostAsync(int id, [FromForm] AlunoModel aluno)
        {
            AlunoModel checkAluno = await this._alunoProvider.Get(aluno.Usuario);

            if (checkAluno is not null)
            {
                return Conflict(new { message = "Usuário já existe" });
            }
            else
            {
                if (this.ValidaSenha(aluno.Senha))
                {

                    string hashedPassword = new PasswordHasher<string>().HashPassword(aluno.Usuario, aluno.Senha);

                    aluno.Senha = hashedPassword;
                    aluno.ID = id;

                    await this._alunoRepository.Update(aluno);
                    return Ok();
                }
                else
                {
                    return Conflict(new { message = "Senha Fraca" });
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<dynamic>> Delete(int id)
        {
            await this._alunoRepository.Delete(id);
            return Ok();
        }

        private bool ValidaSenha(string senha)
        {
            //Check size
            if (senha.Length < 8)
            {
                return false;
            }
            else if (senha.Length > 32)
            {
                return false;
            }

            //check for unwanted chars
            if (Regex.Match(senha, @"[^a-zA-Z0-9À-ÿ!@#$%^&*()_+\-=\[\]{};:\\|,.<>\/?]").Success)
            {
                return false;
            }

            bool hasNumber = false;
            bool hasLowerLetter = false;
            bool hasUpperLetter = false;
            bool hasSpecialCharacter = false;

            //Check for min amount of chars
            if ((Regex.Match(senha, @"[a-z]").Success))  
            {
                hasLowerLetter = true;
            }
            if ((Regex.Match(senha, @"[A-Z]").Success)) 
            {
                hasUpperLetter = true;
            }

            if ((Regex.Match(senha, @"[0-9]").Success))
            {
                hasNumber = true;
            }
            if ((Regex.Match(senha, @"[À-ÿ]").Success) || (Regex.Match(senha, @"[!@#$%^&*()_+\-=\[\]{};:\\|,.<>\/?]").Success))
            {
                hasSpecialCharacter = true;
            }

            return hasNumber && hasLowerLetter && hasUpperLetter && hasSpecialCharacter;
        }
    }
}
