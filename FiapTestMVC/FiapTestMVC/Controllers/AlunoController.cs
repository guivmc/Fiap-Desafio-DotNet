using FiapTestMVC.Models;
using FiapTestWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace FiapTestMVC.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IConfiguration _config;

        public AlunoController(IConfiguration configuration) {
            this._config = configuration;
        }

        [HttpGet]
        public IActionResult CriaAluno()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CriaAluno(AlunoModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(this._config["ApiURL"]!);

                    client.DefaultRequestHeaders.Clear();

                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(model.Nome), "Nome");
                    formData.Add(new StringContent(model.Senha), "Senha");
                    formData.Add(new StringContent(model.Usuario), "Usuario");

                    HttpResponseMessage Res = await client.PostAsync("api/Aluno", formData);

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaAluno");
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

        [HttpGet]
        [Route("/Aluno/Editar/{id}")]
        public async Task<IActionResult> EditarAlunoAsync(int id)
        {
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

                    AlunoModel model = JsonConvert.DeserializeObject<AlunoModel>(aluno)!;
                    model.Senha = string.Empty;

                    return View(model);
                }
                else
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = Res.Content.ReadAsStringAsync().Result });
                }
            }
        }

        [HttpPost]
        [Route("/Aluno/Editar/{id}")]
        public async Task<IActionResult> EditarAluno(AlunoModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(this._config["ApiURL"]!);

                    client.DefaultRequestHeaders.Clear();

                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(model.Nome), "Nome");
                    formData.Add(new StringContent(model.Senha), "Senha");
                    formData.Add(new StringContent(model.Usuario), "Usuario");

                    HttpResponseMessage Res = await client.PostAsync("api/Aluno/" + model.ID, formData);

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaAluno");
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


        [HttpGet]
        public async Task<IActionResult> ListaAlunoAsync()
        {
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

                    return View(JsonConvert.DeserializeObject<IEnumerable<AlunoModel>>(alunoList));

                }
                else
                    return View(new List<AlunoModel>());
            }
        }

        [Route("/Aluno/DeltarAlunoAsync/{id}")]
        public async Task<IActionResult> DeltarAlunoAsync(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(this._config["ApiURL"]!);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.DeleteAsync("api/Aluno/" + id);

                return RedirectToAction("ListaAluno");
            }
        }
    }
}
