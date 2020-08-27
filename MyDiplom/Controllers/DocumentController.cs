using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyDiplom.Controllers
{
    public class DocumentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowFirstLink()
        {
            return View("_DocumentZone");
        }

        public async Task<IActionResult> Switch()
        {
            var mylist = new List<string>{ "Зазор контрольной тяги ", "Зазор рабочей тяги", "Отклонение насечки на контрольных тягах"};
            return View(mylist);
        }

        public IActionResult Light()
        {
            List<string> mylist = new List<string> { "Габариты установки светофора ", "Напряжение на лампах",  };
            return View(mylist);
        }
    }
}