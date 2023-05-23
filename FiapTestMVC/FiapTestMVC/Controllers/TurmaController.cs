using FiapTestMVC.Models;
using FiapTestWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace FiapTestMVC.Controllers
{
    public class TurmaController : Controller
    {
        private readonly IConfiguration _config;

        public TurmaController(IConfiguration configuration)
        {
            this._config = configuration;
        }

        [HttpGet]
        public IActionResult CriaTurma()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriaTurma(TurmaModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(this._config["ApiURL"]!);

                    client.DefaultRequestHeaders.Clear();

                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(model.CursoID.ToString()), "CursoID");
                    formData.Add(new StringContent(model.Turma.ToString()), "Turma");
                    formData.Add(new StringContent(model.Ano.ToString()), "Ano");

                    HttpResponseMessage Res = await client.PostAsync("api/Turma", formData);

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaTurma");
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
        [Route("/Turma/EditarTurmaAsync/{id}")]
        public async Task<IActionResult> EditarTurmaAsync(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(this._config["ApiURL"]!);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Turma/" + id);

                if (Res.IsSuccessStatusCode)
                {

                    var Turma = Res.Content.ReadAsStringAsync().Result;

                    TurmaModel model = JsonConvert.DeserializeObject<TurmaModel>(Turma)!;

                    return View(model);

                }
                else
                {
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = Res.Content.ReadAsStringAsync().Result });
                }
            }
        }

        [HttpPost]
        [Route("/Turma/Editar/{id}")]
        public async Task<IActionResult> EditarTurma(TurmaModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(this._config["ApiURL"]!);

                    client.DefaultRequestHeaders.Clear();

                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(model.CursoID.ToString()), "CursoID");
                    formData.Add(new StringContent(model.Turma.ToString()), "Turma");
                    formData.Add(new StringContent(model.Ano.ToString()), "Ano");

                    HttpResponseMessage Res = await client.PostAsync("api/Turma/" + model.ID, formData);

                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaTurma");
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


        public async Task<IActionResult> ListaTurmaAsync()
        {
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

                    return View(JsonConvert.DeserializeObject<IEnumerable<TurmaModel>>(turmaList));
                }
                else
                    return View(new List<TurmaModel>());
            }
        }

        [Route("/Turma/DeltarTurmaAsync/{id}")]
        public async Task<IActionResult> DeltarTurmaAsync(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(this._config["ApiURL"]!);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.DeleteAsync("api/Turma/" + id);

                return RedirectToAction("ListaTurma");
            }
        }
    }
}
