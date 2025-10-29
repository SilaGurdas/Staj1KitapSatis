using Microsoft.AspNetCore.Mvc;
using BookStoreMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BookStoreMVC.Controllers
{
    public class SepetController : BaseController
    {
        private const string SepetSessionKey = "Sepet";

        // Sepeti getir, yoksa yeni liste oluştur
        private List<SepetItem> GetSepet()
        {
            var sessionData = HttpContext.Session.GetString(SepetSessionKey);
            if (string.IsNullOrEmpty(sessionData))
            {
                return new List<SepetItem>();
            }
            return JsonSerializer.Deserialize<List<SepetItem>>(sessionData);
        }

        // Sepeti Session'a kaydet
        private void SaveSepet(List<SepetItem> sepet)
        {
            var sessionData = JsonSerializer.Serialize(sepet);
            HttpContext.Session.SetString(SepetSessionKey, sessionData);
        }

        // Sepete kitap ekle
        [HttpPost]
        public IActionResult SepeteEkle(int kitapId, string kitapAdi, decimal fiyat, string? resimUrl)
        {
            var sepet = GetSepet();

            var mevcutItem = sepet.FirstOrDefault(i => i.KitapId == kitapId);
            if (mevcutItem != null)
            {
                mevcutItem.Adet++;
            }
            else
            {
                sepet.Add(new SepetItem
                {
                    KitapId = kitapId,
                    KitapAdi = kitapAdi,
                    Fiyat = fiyat,
                    Adet = 1,
                    ResimUrl = resimUrl
                });
            }

            SaveSepet(sepet);

            // Sepet sayısını güncellemek için ViewBag'e ekleyebiliriz
            ViewBag.SepetSayisi = sepet.Sum(i => i.Adet);

            return RedirectToAction("Index");
        }

        // Sepet sayfası
        public IActionResult Index()
        {
            var sepet = GetSepet();
            return View(sepet);
        }

        // Sepetten ürün çıkar
        [HttpPost]
        public IActionResult SepettenCikar(int kitapId)
        {
            var sepet = GetSepet();
            var item = sepet.FirstOrDefault(i => i.KitapId == kitapId);
            if (item != null)
            {
                sepet.Remove(item);
                SaveSepet(sepet);
            }
            return RedirectToAction("Index");
        }
    }
}
