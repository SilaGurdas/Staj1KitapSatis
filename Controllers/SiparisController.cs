using Microsoft.AspNetCore.Mvc;
using BookStoreMVC.Models;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

public class SiparisController : Controller
{
    private readonly HttpClient _httpClient;

    public SiparisController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost]
    public async Task<IActionResult> Tamamla()
    {
        var sessionData = HttpContext.Session.GetString("Sepet");
        if (string.IsNullOrEmpty(sessionData))
            return RedirectToAction("Index", "Sepet");

        var sepet = JsonSerializer.Deserialize<List<SepetItem>>(sessionData);

      
        int kullaniciId = 1; 

        foreach (var item in sepet)
        {
            var siparis = new
            {
                KullaniciId = kullaniciId,
                KitapId = item.KitapId,
                Adet = item.Adet,
                Tarih = DateTime.Now
            };

            var json = JsonSerializer.Serialize(siparis);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("https://localhost:7062/api/Siparis", content);
        }

        
        HttpContext.Session.Remove("Sepet");

        return RedirectToAction("SiparisTamamlandi");
    }

    public IActionResult SiparisTamamlandi()
    {
        return View(); 
    }
}
