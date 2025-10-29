using Microsoft.AspNetCore.Mvc;
using BookStoreMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace BookStoreMVC.Controllers
{
    public class FavoriController : Controller
    {
        private const string FavoriSessionKey = "Favoriler";

      
        private List<FavoriItem> GetFavoriler()
        {
            var sessionData = HttpContext.Session.GetString(FavoriSessionKey);
            if (string.IsNullOrEmpty(sessionData))
            {
                return new List<FavoriItem>();
            }
            return JsonSerializer.Deserialize<List<FavoriItem>>(sessionData);
        }

       
        private void SaveFavoriler(List<FavoriItem> favoriler)
        {
            var sessionData = JsonSerializer.Serialize(favoriler);
            HttpContext.Session.SetString(FavoriSessionKey, sessionData);
        }

       
        [HttpPost]
        public IActionResult FavorilereEkle(int kitapId, string kitapAdi, decimal fiyat, string? resimUrl)
        {
            var favoriler = GetFavoriler();

            if (!favoriler.Any(f => f.KitapId == kitapId))
            {
                favoriler.Add(new FavoriItem
                {
                    KitapId = kitapId,
                    KitapAdi = kitapAdi,
                    Fiyat = fiyat,
                    ResimUrl = resimUrl
                });

                SaveFavoriler(favoriler);
            }

            ViewBag.FavoriSayisi = favoriler.Count;

            return RedirectToAction("Index");
        }

      
        public IActionResult Index()
        {
            var favoriler = GetFavoriler();
            return View(favoriler);
        }

       
        [HttpPost]
        public IActionResult FavoridenCikar(int kitapId)
        {
            var favoriler = GetFavoriler();
            var item = favoriler.FirstOrDefault(f => f.KitapId == kitapId);
            if (item != null)
            {
                favoriler.Remove(item);
                SaveFavoriler(favoriler);
            }
            return RedirectToAction("Index");
        }
    }
}
