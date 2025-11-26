using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime Start;
        
        public DateTime End;
        [Display(Name = "Trainee")]
        public string TraineeId { get; set; }
        [Display(Name = "Trainee")]
        public virtual BefitUser? Trainee { get; set; }
    }
}
