using HospitalManagement.Data;
using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Controllers
{
    [AllowAnonymous]
        public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index(DateTime? selectedDate = null)
        {
            // Use today's date if no date is selected
            DateTime dateToUse = selectedDate ?? DateTime.Today;

            // Get the day of the week (e.g., "Saturday")
            string dayOfWeek = dateToUse.ToString("ddd", System.Globalization.CultureInfo.InvariantCulture);
            Days selectedDay;
            if (Enum.TryParse(dayOfWeek, true, out selectedDay))
            {
                var schedules =  _context.AssistantSchedules
            .Include(s => s.Assistant)
            .Include(s => s.Department)
            .Where(s => s.IsAvailable  && s.day == selectedDay) // Compare enum values and only fetch schedules marked as available
                                        .ToList();  //
                                                    
            
               
                // Generate appointment view models
                var appointmentViewModels = schedules.Select(schedule =>
                {
                    var timeSlots = GenerateTimeSlots(schedule, dateToUse);
                    return new AppointmentViewModel
                    {
                        Schedule = schedule,
                        TimeSlots = timeSlots,
                        SelectedDate = dateToUse
                    };
                }).ToList();

                ViewBag.SelectedDate = dateToUse; // Pass the selected date to the view
                return View(appointmentViewModels);

            }
            else
            {
                var schedules = new List<AssistantSchedule>();
                return View(schedules);
            }

           
            
        }

        // Updated GenerateTimeSlots method to accept the selected date
        private List<(DateTime Start, DateTime End)> GenerateTimeSlots(AssistantSchedule schedule, DateTime selectedDate)
        {
            var timeSlots = new List<(DateTime Start, DateTime End)>();

            DateTime slotStart = selectedDate.Date.Add(schedule.StartTime.TimeOfDay); // Merge date with time
            DateTime slotEnd = selectedDate.Date.Add(schedule.EndTime.TimeOfDay);    // Merge date with time

            while (slotStart < slotEnd)
            {

                DateTime nextSlotEnd = slotStart.AddMinutes((double)schedule.timeConsuming);
                if (nextSlotEnd <= slotEnd) // Ensure slots stay within bounds
                {
                    timeSlots.Add((slotStart, nextSlotEnd));
                }
                slotStart = nextSlotEnd;
            }

            return timeSlots;
        }


       
    }
}
