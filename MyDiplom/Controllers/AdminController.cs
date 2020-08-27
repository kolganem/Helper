using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyDiplom.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyDiplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public AdminController(ApplicationDbContext context)
        {
            
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View(_context.Users.ToList());
        }
       
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction(nameof(Index));
        }

        

    }
}