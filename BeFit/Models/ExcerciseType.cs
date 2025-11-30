using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExcerciseType
    {
        [Display(Name = "Identyfikator")]
        public int Id { get; set; }

        [Display(Name = "Nazwa Ćwiczenia")]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
