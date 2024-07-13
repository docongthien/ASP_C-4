using DemoUser.Data;
using DemoUser.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoUser.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDbContext _context;
        
        public UserController(UserDbContext context)
        {
            _context = context;
        }
        public IActionResult List()
        {
            return View(_context.Users.ToList());
        }


        
        [HttpPost]
        public IActionResult Create(USER user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("List");
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View(new USER());
        }


        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Age,Gender,hobbies,country")] USER user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(user);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            return View(user);
        }



        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
