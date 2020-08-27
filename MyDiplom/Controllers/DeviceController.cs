using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using MyDiplom.Data;
using MyDiplom.Models;
using ClosedXML.Excel;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyDiplom.Controllers
{
    
    public class DevicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Devices
        public IActionResult Index()
        {
            var temp = _context.Devices.ToList().OrderBy(s => s.Stand).ThenBy(u => u.PlaceOnStand);
            return View(temp);
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var device = await _context.Devices.FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
            //return Redirect("~/Devices/ForAdminDevice");
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Stand,PlaceOnStand,typeDevice,NumberDevice,YearDevice,DateCheck,DateFutureCheck")] Device device)
        {

            if (ModelState.IsValid)
            {
                if (device.Stand <= 100 || device.Stand >= 245)
                {
                    return RedirectToAction("ErrorStand");
                }
                if (device.PlaceOnStand < 0 || device.PlaceOnStand % 10 == 0
                    || device.PlaceOnStand % 10 == 9 || device.PlaceOnStand > 170
                    || device.PlaceOnStand % 100 == 9)
                {
                    return RedirectToAction("ErrorPlaceOnStand");
                }
                if (device.NumberDevice.ToString().Length > 6 || device.NumberDevice < 0)
                {
                    return RedirectToAction("ErrorNumberDevice");
                }
                if (device.YearDevice.ToString().Length > 4 || device.YearDevice > DateTime.Today.Year
                    || device.YearDevice < 1970)
                {
                    return RedirectToAction("ErrorYearDevice");
                }
                else
                {
                    _context.Add(device);
                }

                await _context.SaveChangesAsync();                
                    
                return RedirectToAction(nameof(Index));
            }

            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                // Вывод сообщения об ошибке
                return NotFound();
            }

            var device = await _context.Devices.FindAsync(id);
            if (device == null)
            {
                // Вывод сообщения об ошибке
                return NotFound();
            }
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Stand,PlaceOnStand,typeDevice,NumberDevice,YearDevice,DateCheck,DateFutureCheck")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(device);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Devices/ForAdminDevice");
            }
            return View(device);
        }

        // GET: Devices/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _context.Devices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return Redirect("~/Devices/ForAdminDevice");
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }


        public IActionResult Find()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Find(int number)
        {
            if ((number.ToString().Length > 6) || number < 0)
            {
                return RedirectToAction("ErrorNumberDevice");
            }
            var device = await _context.Devices.Where(p => p.NumberDevice.ToString().Contains(number.ToString())).ToListAsync();

            if (device.Count == 0)
            {
                return RedirectToAction("Search_Without_Result");
            }
            return View(device);
        }

        public IActionResult Find_for_typeDevice()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Find_for_typeDevice(string text_for_search)
        {
            if (text_for_search.Length > 10 || text_for_search == null)
            {
                return NotFound();
            }
            var device = await _context.Devices.Where(m => m.typeDevice.Contains(text_for_search)).ToListAsync();

            if (device.Count == 0 || device == null)
            {
                return RedirectToAction("Search_Without_Result");
            }

            return View(device);
        }

        public IActionResult PlanReplacing()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlanReplacing(DateTime startDate, DateTime finishDate, List<Device> tmp)
        {
            if (startDate == null || finishDate == null)
            {
                return RedirectToAction("UnexpectedError_DateTime");
            }

            var device = await _context.Devices.Where(
                   p => p.DateFutureCheck.CompareTo(startDate) > 0 &&
                   p.DateFutureCheck.CompareTo(finishDate) < 0).ToListAsync();

            if (device == null || device.Count == 0)
            {
                return RedirectToAction("Search_Without_Result");
            }
            ViewBag.StartDate = startDate.ToShortDateString();
            ViewBag.FinishDate = finishDate.ToShortDateString();

            return View(device);

        }

        public async Task<IActionResult> ShowFailures(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var failure = await _context.Failures
                .Where(m => m.DeviceId == id)
                .ToListAsync();
            if (failure == null)
            {
                return NotFound();
            }

            return View(failure);

        }

        public IActionResult AddFailure(int? id)
        {
            ViewData["DeviceId"] = id;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddFailure(Failure failure, int? id)
        {
            if (ModelState.IsValid)
            {
                _context.Add(failure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceId"] = id;

            return View(failure);
        }

        public IActionResult ErrorStand()
        {
            ViewData["Message"] = "Введён неверный номер статива";
            return View();
        }

        public IActionResult ErrorPlaceOnStand()
        {
            ViewData["Message"] = "Введено неверное место прибора на стативе";
            return View();
        }
        public IActionResult ErrorNumberDevice()
        {
            ViewData["Message"] = "Введён неверный номер прибора";
            return View();
        }
        public IActionResult ErrorYearDevice()
        {
            ViewData["Message"] = "Введён неверный год прибора";
            return View();
        }

        public IActionResult Search_Without_Result()
        {
            ViewData["Message"] = "Поиск не дал результатов";
            return View();
        }

        public IActionResult UnexpectedError_DateTime()
        {
            ViewData["Message"] = "Непредвиденная ошибка, даты не корректны";
            return View();
        }
        [HttpPost]
        public IActionResult Export(DateTime startDate, DateTime finishDate)
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {

                var myListForWriteInExcel = _context.Devices.Where(
                   p => p.DateFutureCheck.CompareTo(startDate) > 0 &&
                   p.DateFutureCheck.CompareTo(finishDate) < 0).ToList();


                var worksheet = workbook.Worksheets.Add("Приборы");

                worksheet.ColumnWidth = 10;

                worksheet.Cell("A1").Value = "№";
                worksheet.Cell("B1").Value = "Статив";
                worksheet.Cell("C1").Value = "Место";
                worksheet.Cell("D1").Value = "Тип";
                worksheet.Cell("E1").Value = "Номер";
                worksheet.Cell("F1").Value = "Год";
                worksheet.Cell("G1").Value = "Проверка";
                worksheet.Cell("H1").Value = "Замена";

                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                               
                worksheet.Cell("I1").Value = myListForWriteInExcel.Count;

                //нумерация строк/столбцов начинается с индекса 1(не 0)
                for (int i = 0; i < myListForWriteInExcel.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = i + 1;
                    worksheet.Cell(i + 2, 2).Value = myListForWriteInExcel[i].Stand;
                    worksheet.Cell(i + 2, 3).Value = myListForWriteInExcel[i].PlaceOnStand;
                    worksheet.Cell(i + 2, 4).Value = myListForWriteInExcel[i].typeDevice;
                    worksheet.Cell(i + 2, 5).Value = myListForWriteInExcel[i].NumberDevice;
                    worksheet.Cell(i + 2, 6).Value = myListForWriteInExcel[i].YearDevice;
                    worksheet.Cell(i + 2, 7).Value = myListForWriteInExcel[i].DateCheck.ToShortDateString();
                    worksheet.Cell(i + 2, 8).Value = myListForWriteInExcel[i].DateFutureCheck.ToShortDateString();
                    worksheet.Row(i + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"График замены->{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ForAdminDevice()
        {
            var temp = _context.Devices.ToList().OrderBy(s => s.Stand).ThenBy(u => u.PlaceOnStand);
            return View(temp);
        }

        public IActionResult ShowDeviceForAll()
        {
            var temp = _context.Devices.ToList().OrderBy(s => s.Stand).ThenBy(u => u.PlaceOnStand);
            return View(temp);
        }
    }
}