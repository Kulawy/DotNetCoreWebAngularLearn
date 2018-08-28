
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

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }
        
        public IActionResult Index()
        {

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

    }
}
