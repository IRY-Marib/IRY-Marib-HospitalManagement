using HospitalManagement.Data;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Controllers
{
    [AllowAnonymous]

    public class AssistantSchedulesController : Controller

    {
        private readonly ApplicationDbContext _context;
        public AssistantSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Index()
        {
            
            var schedules = _context.AssistantSchedules
        .Include(s => s.Assistant)  // Include Assistant entity
        .Include(s => s.Department) // Include Department entity
        .ToListAsync();

            return View(await schedules);
        }
        public async Task<IActionResult> ViewAssistantSch(string Email)
            {
            string Email1 = User.Identity!.Name;
            if (Email1 == null || _context.Assistants == null)
            {
                return NotFound();
            }

            var assistants   = _context.Assistants
        .Where (s=> s.Email== Email1) // Include Assistant entity
        
        .ToList();
            if (assistants.Count <= 0)
            {
                return NotFound();
            }

            var viewModel = new ScheduleViewModel
            {


                assistantSchedule = _context.AssistantSchedules
         .Include(s => s.Assistant)  // Include Assistant entity
         .Include(s => s.Department)
         .Where(s => s.AssistantID == (assistants[0].AssistantID))// 
         .ToList(),


                instructorSchedule = _context.InstructorSchedules
                .Include(s => s.Instructor)
       .Where(s => s.InstructorID == (assistants[0].InstructorID))//
       .ToList()

            };

            // Map schedules to calendar events
            
     viewModel.CalendarEvents = viewModel.assistantSchedule
     .Where(a => a.day != null) // Ensure schedules have valid days
    .SelectMany(a => Enumerable.Range(0, 7) // Map to any week (7 days)
        .Select(offset => new
        {
            EventDate = DateTime.Today.AddDays(offset), // Start from today and iterate
            Event = a
        }))
    .Where(e => MapDayOfWeek(e.EventDate.DayOfWeek) == e.Event.day.ToString())
    .Select(e => new CalendarEvent
    {
        Title = $"Assistant: {e.Event.Assistant?.Name} {e.Event.Assistant?.Surname},Department: {e.Event.Department?.Name}, {e.Event.shift.ToString()} ",
        Start = e.EventDate.ToString("yyyy-MM-dd") + "T" + e.Event.StartTime.ToString("HH:mm:ss"),
        End = e.EventDate.ToString("yyyy-MM-dd") + "T" + e.Event.EndTime.ToString("HH:mm:ss"),
        BackgroundColor = "#007bff",
        TakeAppointment = $"/appointments/create/{e.Event.AssistantID}" // Custom property
    })
    .ToList();
            //////////////////////
            viewModel.CalendarEvents.AddRange(viewModel.instructorSchedule
           .Where(a => a.day != null) // Ensure schedules have valid days
           .SelectMany(a => Enumerable.Range(0, 7) // Map to any week (7 days)
               .Select(offset => new
               {
                   EventDate = DateTime.Today.AddDays(offset), // Start from today and iterate
                   Event = a
               }))
           .Where(e => MapDayOfWeek(e.EventDate.DayOfWeek) == e.Event.day.ToString())
           .Select(e => new CalendarEvent
           {
               Title = $"Instructor: {e.Event.Instructor?.Name} {e.Event.Instructor?.Surname}, {e.Event.shift.ToString()} ",
               Start = e.EventDate.ToString("yyyy-MM-dd") + "T" + e.Event.StartTime.ToString("HH:mm:ss"),
               End = e.EventDate.ToString("yyyy-MM-dd") + "T" + e.Event.EndTime.ToString("HH:mm:ss"),
               BackgroundColor = "#28a745"
           }));
           
           
            //    .Select(a => new CalendarEvent
            //    {
            //        Title = $"Assistant: {a.Assistant?.Name} {a.Assistant?.Surname}",
            //        Start = a.StartTime.ToString("yyyy-MM-dd") + "T" + a.StartTime.ToString("HH:mm:ss"), // Include date and time
            //End = a.EndTime.ToString("yyyy-MM-dd") + "T" + a.EndTime.ToString("HH:mm:ss"),
            //        BackgroundColor = "#007bff"
            //    })
            //    .ToList();

            //    viewModel.CalendarEvents.AddRange(viewModel.instructorSchedule
            //        .Select(i => new CalendarEvent
            //        {
            //            Title = $"Instructor: {i.Instructor?.Name} {i.Instructor?.Surname}",
            //            Start = i.StartTime.ToString("yyyy-MM-dd") + "T" + i.StartTime.ToString("HH:mm:ss"), // Include date and time
            //End = i.EndTime.ToString("yyyy-MM-dd") + "T" + i.EndTime.ToString("HH:mm:ss"),
            //            BackgroundColor = "#28a745"
            //        }));


            return View(viewModel);
        }

        private void PopulateViewData()
        {
            ViewBag.Assistants = new SelectList(_context.Assistants, "AssistantID", "Name");
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name");


        }

        // Call this in Create and Edit GET actions
        public IActionResult CreateAssistant()
        {
            PopulateViewData();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAssistant(AssistantSchedule schedule)
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

        public IActionResult CreateInstractor()
        {
            PopulateViewData();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateInstractor(AssistantSchedule schedule)
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

            var schedule = await _context.AssistantSchedules.FindAsync(id);
            if (schedule == null) return NotFound();

            PopulateViewData();
            return View(schedule);
        }
        private string MapDayOfWeek(DayOfWeek day)
        {
            return day switch
            {
                
                        DayOfWeek.Sunday => "Mon",
                DayOfWeek.Monday => "Tue",
                DayOfWeek.Tuesday => "Wed",
                DayOfWeek.Wednesday => "Thu",
                DayOfWeek.Thursday => "Fri",
                DayOfWeek.Friday => "Sun",
                DayOfWeek.Saturday => "Sat",
                _ => ""
            };
        }

    }
}
