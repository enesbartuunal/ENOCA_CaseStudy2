using System.ComponentModel.DataAnnotations.Schema;

namespace ENOCA_CaseStudy.Data.Entites
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SectionName { get; set; }
        public int FacultyID { get; set; }

        public Faculty Faculty { get; set; }
    }
}
