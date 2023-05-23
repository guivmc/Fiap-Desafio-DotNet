using FiapTestMVC.Models;
using FiapTestWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace FiapTestMVC.Controllers
{
    public class RelacaoController : Controller
    {
        private readonly IConfiguration _config;

        public RelacaoController(IConfiguration configuration)
        {
            this._config = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> CriaRelacaoAsync()
        {
            List<AlunoModel> alunos = new List<AlunoModel>();
            List<TurmaModel> turmas = new List<TurmaModel>();

            //Pega Alunos
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(this._config["ApiURL"]!);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Aluno");

                if (Res.IsSuccessStatusCode)
                {

                    var alunoList = Res.Content.ReadAsStringAsync().Result;

                    alunos = JsonConvert.DeserializeObject<IEnumerable<AlunoModel>>(alunoList)!.ToList();
                }
                else
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = Res.Content.ReadAsStringAsync().Result });
                }
            }

            //Pega Turmas
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(this._config["ApiURL"]!);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Turma");

                if (Res.IsSuccessStatusCode)
                {
                    var turmaList = Res.Content.ReadAsStringAsync().Result;

                    turmas = JsonConvert.DeserializeObject<IEnumerable<TurmaModel>>(turmaList)!.ToList();
                }
                else
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = Res.Content.ReadAsStringAsync().Result });
                }
            }

            return View(new RelacaoModel { Alunos = alunos, Turmas = turmas });
        }

        [HttpPost]
        public async Task<IActionResult> CriaRelacaoAsync(RelacaoModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(this._config["ApiURL"]!);

                    client.DefaultRequestHeaders.Clear();

                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(model.AlunoID.ToString()), "AlunoID");
                    formData.Add(new StringContent(model.TurmaID.ToString()), "TurmaID");

                    HttpResponseMessage Res = await client.PostAsync("api/Relacao", formData);

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaRelacao");
                    }
                    else
                    {
                        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = Res.Content.ReadAsStringAsync().Result });
                    }
                }
            }
            else
            {
                return RedirectToAction("ListaRelacao");
            }
        }

        [HttpGet]
        [Route("/Relacao/Editar/{id}")]
        public async Task<IActionResult> EditarRelacaoAsync(int id)
        {
            AlunoModel alunoModel = new AlunoModel();
            List<TurmaModel> turmas = new List<TurmaModel>();

            //Pega Aluno
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(this._config["ApiURL"]!);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Aluno/" + id);

                if (Res.IsSuccessStatusCode)
                {

                    var aluno = Res.Content.ReadAsStringAsync().Result;

                    alunoModel = JsonConvert.DeserializeObject<AlunoModel>(aluno)!;
                    alunoModel.Senha = string.Empty;
                }
                else
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = Res.Content.ReadAsStringAsync().Result });
                }
            }

            //Pega Turmas
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(this._config["ApiURL"]!);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Turma");

                if (Res.IsSuccessStatusCode)
                {

                    var turmaList = Res.Content.ReadAsStringAsync().Result;
                    turmas = JsonConvert.DeserializeObject<IEnumerable<TurmaModel>>(turmaList)!.ToList();
                }
                else
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = Res.Content.ReadAsStringAsync().Result });
                }
            }

            return View(new RelacaoModel { AlunoID = alunoModel.ID, AlunoNome = alunoModel.Nome, Turmas = turmas });
        }

        [HttpPost]
        [Route("/Relacao/Editar/{id}")]
        public async Task<IActionResult> EditarRelacaoAsync(RelacaoModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(this._config["ApiURL"]!);

                    client.DefaultRequestHeaders.Clear();

                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(model.AlunoID.ToString()), "AlunoID");
                    formData.Add(new StringContent(model.TurmaID.ToString()), "TurmaID");

                    HttpResponseMessage Res = await client.PostAsync("api/Relacao/" + model.AlunoID, formData);

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaRelacao");
                    }
                    else
                    {
                        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = Res.Content.ReadAsStringAsync().Result });
                    }
                }
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> ListaRelacaoAsync()
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(this._config["ApiURL"]!);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Relacao");

                if (Res.IsSuccessStatusCode)
                {
                    var turmaList = Res.Content.ReadAsStringAsync().Result;

                    return View(JsonConvert.DeserializeObject<IEnumerable<RelacaoModel>>(turmaList));
                }
                else
                    return View(new List<RelacaoModel>());
            }
        }

        [Route("/Relacao/DeltaRelacaoAsync/{id}")]
        public async Task<IActionResult> DeltaRelacaoAsync(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(this._config["ApiURL"]!);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.DeleteAsync("api/Relacao/" + id);

                return RedirectToAction("ListaRelacao");
            }
        }
    }
}
