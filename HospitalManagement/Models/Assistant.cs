using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Assistant
    {
        public int AssistantID { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="name must be more 2 characters")]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
       public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public int InstructorID { get; set; }
        public Instructor? Instructor { get; set; }// 
        //public string userName { get; set; }
        // Relationships
        public ICollection<AssistantSchedule>? AssistantSchedules { get; set; } 

    }
}