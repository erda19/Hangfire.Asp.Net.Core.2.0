using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aaa.Models;
using Hangfire;

namespace aaa.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            BackgroundJob.Enqueue(() =>  testJob());
            return View();
        }

        public void testJob()
        {
             Console.WriteLine("Fire-and-forget!");
        }

        public IActionResult Contact()
        {
            RecurringJob.AddOrUpdate(
                                        () => Console.WriteLine("Recurring!"),
                                        Cron.MinuteInterval(1));
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
