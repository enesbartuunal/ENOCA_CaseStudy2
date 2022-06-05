using System.ComponentModel.DataAnnotations.Schema;

namespace ENOCA_CaseStudy.Data.Entites
{
    public class Faculty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FacultyID { get; set; }
        public string FacultyName { get; set; }
        public List<Section> Sections { get; set; }
        public List<Student> Students { get; set; }
    }
}
