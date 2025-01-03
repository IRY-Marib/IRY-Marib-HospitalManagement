namespace HospitalManagement.Models
{
    public class Appointment
    {

        public int AppointmentID { get; set; }
        public DateTime AppointmentTime { get; set; } // Time of appointment
        public string PatientName { get; set; }
        public string PatientEmail { get; set; }
        public string PatientMobile { get; set; }

        // Relationships
        public int ScheduleID { get; set; }
        public AssistantSchedule AssistantSchedule { get; set; }
       

    }
}
