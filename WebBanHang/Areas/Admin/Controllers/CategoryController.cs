using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models.EF;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
            private ApplicationDbContext db = new ApplicationDbContext();
            // GET: Admin/Category
            public ActionResult Index(string Searchtext)
            {
               

                IEnumerable<Category> items = db.Categories.OrderByDescending(x => x.Id);
                if (!string.IsNullOrEmpty(Searchtext))
                {
                    items = items.Where(x => x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));
                }
            return View(items);
            }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {


                category.CreatedDate = DateTime.Now;
                category.ModifiedrDate = DateTime.Now;
                category.Alias = WebBanHang.Models.Common.Fitler.FilterChar(category.Title);
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public ActionResult Edit(int id) 
        {
            var item = db.Categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(category);
                category.ModifiedrDate = DateTime.Now;
                category.Alias = WebBanHang.Models.Common.Fitler.FilterChar(category.Title);
                db.Entry(category).Property(x=>x.Title).IsModified = true;
                db.Entry(category).Property(x => x.Description).IsModified = true;
                db.Entry(category).Property(x => x.Link).IsModified = true;
                db.Entry(category).Property(x => x.Alias).IsModified = true;
                db.Entry(category).Property(x => x.SeoTitle).IsModified = true;
                db.Entry(category).Property(x => x.SeoDescription).IsModified = true;
                db.Entry(category).Property(x => x.SeoKeywords).IsModified = true;
                db.Entry(category).Property(x => x.Position).IsModified = true;
                db.Entry(category).Property(x => x.ModifiedrDate).IsModified = true;
                db.Entry(category).Property(x => x.ModifierBy).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Categories.Find(id);
            if(item != null)
            {
                db.Categories.Remove(item); 
                db.SaveChanges();
                return Json(new { succes = true });
            }
            return Json(new { succes = false });
        }
    }
}