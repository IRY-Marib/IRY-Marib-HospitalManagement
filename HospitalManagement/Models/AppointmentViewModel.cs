using HospitalManagement.Models;
using System;
using System.Collections.Generic;

public class AppointmentViewModel
{
    public AssistantSchedule Schedule { get; set; }
    public List<(DateTime Start, DateTime End)> TimeSlots { get; set; }
    public DateTime SelectedDate { get; set; } // Add the selected date
}
