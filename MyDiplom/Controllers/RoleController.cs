using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyDiplom.Data;

namespace MyDiplom.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public RoleController(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _context = context;
            
        }

        public IActionResult Index() => View(_context.Roles.ToList());

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(string name)
        {
            await CreateUserRoles(_serviceProvider, name);
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Delete(string id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(m => m.Id == id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }
        private async Task CreateUserRoles(IServiceProvider serviceProvider, string name)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync(name);

            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole(name));
            }

        }
        private async Task GiveUserRoles(IServiceProvider serviceProvider, string name)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await _context.Users.FirstOrDefaultAsync(m => m.Id == name);
            await UserManager.AddToRoleAsync(user, "Admin");
            await _context.SaveChangesAsync();
            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
        }
        public async Task<ActionResult> giveAccess(string id)
        {
            await GiveUserRoles(_serviceProvider, id);
            
            return Redirect("~/Admin/Index");
        }
    }
}