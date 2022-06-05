using System.ComponentModel.DataAnnotations;

namespace ENOCA_CaseStudy.Models
{
    public class StudentDto
    {
        public int StudentID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        
        public int FacultyID { get; set; }
        public string FacultyName { get; set; }

        public string SectionName { get; set; }
    }
}
