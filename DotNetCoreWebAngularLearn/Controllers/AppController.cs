
using DotNetCoreWebAngularLearn.Data;
using DotNetCoreWebAngularLearn.Services;
using DotNetCoreWebAngularLearn.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWebAngularLearn.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IMyRepository _repository;

        //jak już mamy IMyRepository to zamieniamy MyDataContext context na IMyRepository context w konstruktorze
        public AppController(IMailService mailService, IMyRepository repository )
        {
            _mailService = mailService;
            _repository = repository;
        }
        
        public IActionResult Index()
        {
            //var result = _context.Products.ToList();
            var result = _repository.GetAllProducts();
            return View();
        }

        [HttpGet("contact")] //daje tyle, że adres url to nie localhost:8888/app/contact tylko localhost:8888/contact
        public IActionResult Contact()
        {
            //ViewBag.Title = "Contact Us";

            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            //żeby pobrać dane trzeba użyć model binding dla modelu

            if (ModelState.IsValid)
            {
                // send email
                _mailService.SendMessage("catatau1992@gmail.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Send";
                ModelState.Clear();

            }
            else
            {
                //show errors
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        public IActionResult Shop()
        {
            //var results = _context.Products
            //    .OrderBy(p => p.Category)
            //    .ToList();

            // lub linquery : 
            //var results = from p in _context.Products
            //              orderby p.Category
            //              select p;    i wtedy jeszcze return results.ToList();


            //jak już mamy _repository nie _context to
            var results = _repository.GetAllProducts();

            return View(results);
        }

    }
}
