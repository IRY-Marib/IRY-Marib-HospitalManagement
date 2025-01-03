using HospitalManagement.Data;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Controllers
{
    [AllowAnonymous]

    public class InstructorSchedulesController : Controller

    {
        private readonly ApplicationDbContext _context;
        public InstructorSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Index()
        {
            
            var schedules = _context.InstructorSchedules
        .Include(s => s.Instructor)  // Include Assistant entity
        .ToListAsync();

            return View(await schedules);
        }

        private void PopulateViewData()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name");
            ViewBag.Instructors = new SelectList(_context.Instructors, "InstructorID", "Name");


        }

        // Call this in Create and Edit GET actions
        public IActionResult CreateInstructor()
        {
            PopulateViewData();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateInstructor(InstructorSchedule schedule)
            {
            if (ModelState.IsValid)
            {
                schedule.IsAvailable = true;
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var schedule = await _context.InstructorSchedules.FindAsync(id);
            if (schedule == null) return NotFound();

            PopulateViewData();
            return View(schedule);
        }
        public async Task<IActionResult> ViewInstructorSch(string Email)
        {

            string Email1 = User.Identity!.Name;

           // Console.WriteLine(Email);
            if (Email1 == null || _context.Assistants == null)
            {
                return NotFound();
            }

            var instructors = _context.Instructors
        .Where(s => s.Email == Email1) // 
        .ToList();

            if (instructors.Count <= 0)
            {
                return NotFound();
            }

            var viewModel = new ScheduleViewModel
            {

          assistantSchedule = _context.AssistantSchedules
         .Include(s => s.Assistant)  // 
         .Include(s => s.Department)
         .Where(s => s.Assistant!.InstructorID == (instructors[0].InstructorID))// 
         .ToList(),

          instructorSchedule = _context.InstructorSchedules
          .Include(s => s.Instructor)
          .Where(s => s.InstructorID == (instructors[0].InstructorID))// 
          .ToList()

            };

            return View(viewModel);
        }

    }
}
