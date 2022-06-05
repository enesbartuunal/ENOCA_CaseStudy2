using System.ComponentModel.DataAnnotations.Schema;

namespace ENOCA_CaseStudy.Data.Entites
{
    public class Section
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionID { get; set; }
        public string SectionName { get; set; }
    }
}
