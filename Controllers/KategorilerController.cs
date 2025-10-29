using BookStoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMVC.Controllers
{
    public class KategorilerController : BaseController
    {
        private readonly string apiUrl = "https://localhost:7062/api/Kategoriler";

        public async Task<IActionResult> Index()
        {
            List<Kategori>? kategoriler = new();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    kategoriler = await response.Content.ReadFromJsonAsync<List<Kategori>>();
                }
                else
                {
                    ViewBag.ErrorMessage = "Kategoriler alınamadı.";
                }
            }

            return View(kategoriler);
        }


        public async Task<IActionResult> Detay(int id)
        {
            Kategori? kategori = null;

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{apiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    kategori = await response.Content.ReadFromJsonAsync<Kategori>();
                }
                else
                {
                    ViewBag.ErrorMessage = "Kategori detayları alınamadı.";
                }
            }

            return View(kategori);
        }
    }
}

