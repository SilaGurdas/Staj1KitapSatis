using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BookStoreMVC.Models; 

namespace BookStoreMVC.Controllers
{
    public class KitaplarController : BaseController
    {
        private readonly HttpClient _httpClient;

        public KitaplarController()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7062/api/")
            };
        }
        public async Task<IActionResult> Index()
        {
            List<Kitap> kitaplar = new();

            try
            {
                kitaplar = await _httpClient.GetFromJsonAsync<List<Kitap>>("kitaplar");
            }
            catch (Exception ex)
            {
               
                ViewBag.ErrorMessage = "Kitaplar yüklenirken hata oluştu: " + ex.Message;
            }

            return View(kitaplar);
        }
        public async Task<IActionResult> Detay(int id)
        {
            Kitap? kitap = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7062"); 

                var response = await httpClient.GetAsync($"/api/Kitaplar/{id}");

                if (response.IsSuccessStatusCode)
                {
                    kitap = await response.Content.ReadFromJsonAsync<Kitap>();
                }
                else
                {
                    ViewBag.ErrorMessage = "Kitap bilgisi alınamadı.";
                }
            }
            if (kitap == null)
            {
                return NotFound();
            }
            return View(kitap);
        }

    }
}

