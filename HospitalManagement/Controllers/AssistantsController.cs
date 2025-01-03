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
    public class AssistantsController : Controller
    {
        private readonly ApplicationDbContext _context; 
        private readonly UserManager<IdentityUser> _userManager;


        public AssistantsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private void PopulateViewData()
        {

            ViewBag.Instructors = new SelectList(_context.Instructors, "InstructorID", "Name");
            ViewBag.userManager= new SelectList(_userManager.Users, "UserName", "UserName");

        }
        // GET: Assistants
        //[Route("Assistants/Index")] //هذا روتنج يستخدم بدلا من الروتنج من الاعدادات
        public async Task<IActionResult> Index()
        {
            var assistant = _context.Assistants
        .Include(s => s.Instructor)  // Include Instructor entity
       .ToListAsync();
        return View(await assistant);
        }

        // GET: Assistants/Details/5
        //[Route("Assistants/Details/{id?}")] //? معناه اختياري
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Assistants == null)
            {
                return NotFound();
            }

            var assistant = await _context.Assistants
                .FirstOrDefaultAsync(m => m.AssistantID == id);
            if (assistant == null)
            {
                return NotFound();
            }

            return View(assistant);
        }

        // GET: Assistants/Create
        public IActionResult Create()
        {
            PopulateViewData();
            return View();
        }

        // POST: Assistants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Assistant assistant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assistant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assistant);
        }

        //الاستعلام عن بيانات المساعد من قاعدة البيانات بشرط  الرقم
        // GET: Assistants/Edit/5
        [HttpGet]
     public async Task<IActionResult> Edit(int? id)
        {
            PopulateViewData();
            if (id == null || _context.Assistants == null)
            {
                return NotFound();
            }

            var assistant = await _context.Assistants.FindAsync(id);
            if (assistant == null)
            {
                return NotFound();
            }
            return View(assistant);
        }

        // POST: Assistants/Edit/5
        //حفظ التعديل في قاعدة البيانات 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Assistant assistant)
        {
            if (id != assistant.AssistantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assistant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssistantExists(assistant.AssistantID))
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
            return View(assistant);
        }

        // GET: Assistants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Assistants == null)
            {
                return NotFound();
            }

            var assistant = await _context.Assistants
                .FirstOrDefaultAsync(m => m.AssistantID == id);
            if (assistant == null)
            {
                return NotFound();
            }

            return View(assistant);
        }

        // POST: Assistants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Assistants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Assistants'  is null.");
            }
            var assistant = await _context.Assistants.FindAsync(id);
            if (assistant != null)
            {
                _context.Assistants.Remove(assistant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssistantExists(int id)
        {
            return _context.Assistants.Any(e => e.AssistantID == id);
        }
    }
}
