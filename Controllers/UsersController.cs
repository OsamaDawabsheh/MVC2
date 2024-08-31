using Microsoft.AspNetCore.Mvc;
using MVC3.Data;
using MVC3.Models;

namespace MVC3.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult NonActive()
        {
            var users = _context.users.ToList();
            var activeUsers = _context.users.Count(users => users.IsActive == true);
            var nonActiveUsers = _context.users.Where(users => users.IsActive == false).ToList();
            ViewBag.ActiveUsers = activeUsers;
            ViewBag.NonActiveUsers = nonActiveUsers.Count();
            ViewBag.AllUsers = users.Count();
            return View(nonActiveUsers);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            var users = _context.users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {

            return View(); 
        }

        [HttpPost]
        public IActionResult Login(User us)
        {
            var checkUser = _context.users.Where(user => user.Name == us.Name && user.Password == us.Password);
            if (checkUser.Any())
            {
                return RedirectToAction("NonActive");

            }
            ViewBag.error = "invalid username or password";
            return View();
        }

        public IActionResult Active(int id)
        {
            var user = _context.users.Find(id);
            user.IsActive = true;
            _context.SaveChanges();

            return RedirectToAction("NonActive");
        }
    }
}
