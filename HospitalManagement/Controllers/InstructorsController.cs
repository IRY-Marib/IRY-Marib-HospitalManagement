using HospitalManagement.Data;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HospitalManagement.Controllers
{
    [AllowAnonymous]

    public class InstructorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public InstructorsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private void PopulateViewData()
        {

            ViewBag.userManager = new SelectList(_userManager.Users, "UserName", "UserName");

        }

        // GET: Instructor
        //[Route("Instructor/Index")] //هذا روتنج يستخدم بدلا من الروتنج من الاعدادات
        public async Task<IActionResult> Index()
        {
            
                var list =await _context.Instructors.ToListAsync();
            List<Instructor> instructors;
            instructors = list;

            return View(instructors);
        }

        // GET: Instructor/Details/5
        //[Route("Instructor/Details/{id?}")] //? معناه اختياري
        public async Task<IActionResult> Details()
        {
            var list = await _context.Instructors.ToListAsync();
            List<Instructor> instructors;
            instructors = list;

            return View(instructors);
           
        }

        // GET: Instructor/Create
        public IActionResult Create()
        {
            PopulateViewData();
            return View();
        }

        // POST: Instructor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        //الاستعلام عن بيانات المساعد من قاعدة البيانات بشرط  الرقم
        // GET: Instructor/Edit/5
        [HttpGet]
     public async Task<IActionResult> Edit(int? id)
        {
            PopulateViewData();

            if (id == null || _context.Instructors == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: Instructor/Edit/5
        //حفظ التعديل في قاعدة البيانات 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,   Instructor instructor)
        {
            if (id != instructor.InstructorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.InstructorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Instructors == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.InstructorID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Instructors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Instructor'  is null.");
            }
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.InstructorID == id);
        }
    }
}
