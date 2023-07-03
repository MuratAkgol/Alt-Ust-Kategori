using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ders51_Ust_Alt_Kategori.Models;

namespace ders51_Ust_Alt_Kategori.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        Iakademi42Entities db = new Iakademi42Entities();
        public ActionResult Index()
        {
            List<Araba> araba=db.Araba.ToList();
            dropDoldur();
            return View(araba);
        }
        void dropDoldur()
        {
            List<Araba> menuList=db.Araba.Where(x=>x.UstMenuID==0).ToList();
            ViewData["siteMenuList"] = menuList.Select(x=> new SelectListItem { Text = x.Adi, Value = x.categoryID.ToString() });
        }
        [HttpPost]
        public ActionResult Index(Araba cat)
        {
            if (cat.UstMenuID == null)
            {
                cat.UstMenuID = 0;
            }
            db.Araba.Add(cat);
            db.SaveChanges();
            return RedirectToAction("Index"); //httpget den gider.
        }  
        public JsonResult AltKategoriSil(int id)
        {
            Araba araba = db.Araba.FirstOrDefault(a => a.categoryID == id);
            db.Araba.Remove(araba);
            db.SaveChanges();
            return Json("araba");
        }
        public JsonResult AnaKategoriSil(int id)
        {
            //alt kategori sildik
           List<Araba> araba = db.Araba.Where(a => a.UstMenuID == id).ToList();
            foreach (var item in araba)
            {
                db.Araba.Remove(item);
                db.SaveChanges();
            }
            //kategori sildik
            Araba arabaa = db.Araba.FirstOrDefault(a => a.categoryID == id);
            db.Araba.Remove(arabaa);
            db.SaveChanges();

            return Json("");

            
        }
    }
}