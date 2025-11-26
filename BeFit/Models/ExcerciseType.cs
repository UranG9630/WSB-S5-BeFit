using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExcerciseType
    {
        public int Id { get; set; }
        
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
