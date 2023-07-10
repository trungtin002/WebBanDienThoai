using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products

        public ActionResult Index(String Searchtext)
        {
            IEnumerable<Product> items = db.Products.OrderByDescending(x => x.Id);

            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));
            }
            
            return View(items);
        }

        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }

            return View(item);
        }
        public ActionResult ProductCategory( int id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }

            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult SubmitReview(string ReviewMessage, int ProductId, int Rating = 0)
        {

            if (!User.Identity.IsAuthenticated)
            {
                // Người dùng chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("review", "Products", new { id = ProductId }) });
            }
            if (ModelState.IsValid)
            {

                var review = new Comment
                {
                    ProductId = ProductId,
                    Rate = Rating,
                    Description = ReviewMessage,
                    FullName = System.Web.HttpContext.Current.User.Identity.GetUserName(),
                    CreatedDate = DateTime.Now,

                };

                db.comments.Add(review);
                db.SaveChanges();

                return RedirectToAction("review", new { id = ProductId });
            }

            // Nếu dữ liệu không hợp lệ, quay lại trang chi tiết sản phẩm
            return RedirectToAction("review", new { returnUrl = Url.Action("review", "Products", new { id = ProductId }) });
        }
        public ActionResult review(int? id)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var reviews = db.comments.Where(c => c.ProductId == id).ToList();

            // Pass the product and reviews data to the view
            ViewBag.Product = product;
            ViewBag.Reviews = reviews;

            return View();
        }
    }
}