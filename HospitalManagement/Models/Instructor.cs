namespace HospitalManagement.Models
{
    public class Instructor
    {
        public int InstructorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Relationships
        public ICollection<Assistant>? Assistants { get; set; }
        public ICollection<InstructorSchedule>? InstructorSchedules { get; set; }
        public ICollection<Appointment> ?Appointments { get; set; }
        //public ICollection<Availability>? Availabilities { get; set; }
    }
}