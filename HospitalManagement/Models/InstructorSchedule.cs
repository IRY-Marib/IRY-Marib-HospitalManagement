using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Models
{
    public class InstructorSchedule
    {
        public int InstructorScheduleId { get; set; }
        public DateTime StartTime { get; set; } // THIS SUCH AS 8:00 AM
        public DateTime EndTime { get; set; }//THIS SUCH AS 13:00 PM

        // Relationships
       
        public int InstructorID { get; set; }
        public Instructor? Instructor { get; set; }// LIST OF ASSISTANTS
        
                                                   
        [Column(TypeName = "nvarchar(20)")]
        public Days day { get; set; }
        [Column(TypeName = "nvarchar(20)")]

        public Shifts shift { get; set; }
        //public Minutes timeConsuming { get; set; } // Enum property
        public bool IsAvailable { get; set; } // True if available

    }





}
