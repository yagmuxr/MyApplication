﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyApplication.Helpers;
using MyApplication.Models;

namespace MyApplication.Controllers
{

    public class ProductsController : Controller
    {
        private AppDbContext _context;
        private ProductRepository productRepository;
        //private IHelper _helper;

        public ProductsController(AppDbContext context, IHelper helper)
        {
            productRepository = new ProductRepository();

            _context = context;
            //_helper = helper;

            ////if (!_context.Products.Any())
            ////{
            //    _context.Products.Add(new Product()
            //    {
            //        Name = "Kalem",
            //        Price = 100,
            //        Stock = 150,
            //        Color = "Red",
                    
            //    });
            //    _context.Products.Add(new Product()
            //    {
            //        Name = "Silgi",
            //        Price = 50,
            //        Stock = 100,
            //        Color = "Pink",
                    
            //    });
            //    _context.Products.Add(new Product()
            //    {
            //        Name = "Defter",
            //        Price = 200,
            //        Stock = 150,
            //        Color = "Green",
                    
            //    });
            //    _context.SaveChanges();
            //}
        }
        public IActionResult Index()
        {


            var products = _context.Products.ToList();
            return View(products);
        }

       
        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id); //primary key'e göre arama yapar !
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
            ViewBag.ExpireDate = product.Expire; 
            ViewBag.Expire = new Dictionary<string, int>() {
                {"1 Ay", 1 },
                {"3 Ay", 2 },
                {"6 Ay", 6 },
                {"12 Ay", 12 }
                };

            ViewBag.ColorSelect = new SelectList(new List<Color_SelectList>()
            {
                new() { Data = "Mavi" ,Value = "Mavi"},
                new() {Data = "Kırmızı" , Value="Kırmızı"},
                new() {Data = ""}

            }, "Value", "Data", product.Color);
            
        }

        [HttpPost]
        public IActionResult Update(Product updateProduct, int productId)
        {
            updateProduct.Id = productId;
                _context.Products.Update(updateProduct);
                _context.SaveChanges();
            TempData["status"] = "Başarıyla güncellendi"; //cookie üzerinden taşır!
            return RedirectToAction("Index");
            
        }

        [HttpGet]
        public IActionResult Add()
        {

            ViewBag.Expire = new Dictionary<string, int>() {
                {"1 Ay", 1 },
                {"3 Ay", 2 },
                {"6 Ay", 6 },
                {"12 Ay", 12 }
                };

            ViewBag.ColorSelect = new SelectList(new List<Color_SelectList>()
            {
                new() { Data = "Mavi" ,Value = "Mavi"},
                new() {Data = "Kırmızı" , Value="Kırmızı"},
                new() {Data = ""}

            }, "Value", "Data");
            return View();
        }
        [HttpPost] //Requestin body kısmında gönderilir post ile!
        public IActionResult SaveProduct(Product newProduct)
        {
            //1.Yöntem : 
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"]);
            //var stock = int.Parse(HttpContext.Request.Form["Stock"]);
            //var color = HttpContext.Request.Form["Color"].ToString();

            //2.Yöntem
            //Product newProduct = new Product() { Name = Name, Price = Price, Stock = Stock, Color = Color };
            _context.Products.Add(newProduct);
            
            _context.SaveChanges();
            TempData["status"] = "Başarıyla eklendi";
            return RedirectToAction("Index");
        }

        //[HttpGet] //get ile URL'de gözükür bilgiler
        //public IActionResult SaveProduct(Product newProduct)
        //{
        //    //1. Yöntem : 
        //    //var name = HttpContext.Request.Form["Name"].ToString();
        //    //var price = decimal.Parse(HttpContext.Request.Form["Price"]);
        //    //var stock = int.Parse(HttpContext.Request.Form["Stock"]);
        //    //var color = HttpContext.Request.Form["Color"].ToString();

        //    //2. Yöntem
        //    //Product newProduct = new Product() { Name = Name, Price = Price, Stock= Stock , Color = Color };
        //    _context.Products.Add(newProduct);
        //    _context.SaveChanges();
        //    return View();
        //}

    }

}
