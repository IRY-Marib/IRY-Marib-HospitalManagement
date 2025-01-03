using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Models
{
    public class AssistantSchedule
    {
        public int AssistantScheduleId { get; set; }
        public DateTime StartTime { get; set; } // THIS SUCH AS 8:00 AM
        public DateTime EndTime { get; set; }//THIS SUCH AS 13:00 PM

        // Relationships
        public int AssistantID { get; set; } 
        public Assistant? Assistant { get; set; }// LIST OF ASSISTANTS
        public int DepartmentID { get; set; }
        public Department? Department { get; set; }// LIST OF DEPARTMENTS
                                                 
        [Column(TypeName = "nvarchar(20)")]
        public Days day { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public Shifts shift { get; set; }
        public Minutes timeConsuming { get; set; } // Enum property
        public bool IsAvailable { get; set; } // True if available

    }





}
