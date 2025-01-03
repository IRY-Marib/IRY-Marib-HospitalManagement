using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        //[Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ? Description { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull  = false)]

        public int? TotalBeds { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public int? AvailableBeds { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public int? PatientCount { get; set; }

        // Relationships
        public ICollection<AssistantSchedule>? Schedules { get; set; }
        public ICollection<Emergency>? Emergencies { get; set; }
    }
}
