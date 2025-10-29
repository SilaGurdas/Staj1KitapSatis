using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BookStoreMVC.Models;

public class BaseController : Controller
{
    private const string SepetSessionKey = "Sepet";
    private const string FavoriSessionKey = "Favoriler";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var sessionData = HttpContext.Session.GetString(SepetSessionKey);
        List<SepetItem> sepet = new List<SepetItem>();

        if (!string.IsNullOrEmpty(sessionData))
        {
            sepet = JsonSerializer.Deserialize<List<SepetItem>>(sessionData);
        }

        ViewBag.SepetSayisi = sepet.Sum(i => i.Adet);

       
        var favoriData = HttpContext.Session.GetString(FavoriSessionKey);
        var favoriler = string.IsNullOrEmpty(favoriData)
            ? new List<FavoriItem>()
            : JsonSerializer.Deserialize<List<FavoriItem>>(favoriData);

        ViewBag.FavoriSayisi = favoriler.Count;
    }
}
