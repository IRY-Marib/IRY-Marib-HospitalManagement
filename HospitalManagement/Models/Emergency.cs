using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManagement.Models
{
    public class Emergency
    {
        //public int EmergencyID { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }
        //public DateTime DatePosted  { get; set; }
        //public bool IsEmailSent { get; set; }

        //// Relationships
        //public int DepartmentID { get; set; }
        //public Department Department { get; set; }
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsSent { get; set; } = false;
    }
}
