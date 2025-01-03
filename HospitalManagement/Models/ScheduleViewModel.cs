using HospitalManagement.Models;
using System;
using System.Collections.Generic;

public class ScheduleViewModel
{
    public List<AssistantSchedule> assistantSchedule { get; set; }
    public List<InstructorSchedule> instructorSchedule { get; set; }
    public List<CalendarEvent> CalendarEvents { get; set; } // Add this property



}
public class CalendarEvent
{
    public string Title { get; set; }
    public string Start { get; set; } // ISO format (e.g., "2024-12-18T10:00:00")
    public string End { get; set; }
    public string BackgroundColor { get; set; }
    public string Day { get; set; }    // Day of the week
    public string DepartmentName { get; set; }    
    public string Shift { get; set; }
    public string   TakeAppointment { get; set; }
}