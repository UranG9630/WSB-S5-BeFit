using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Excercise
    {
        public int Id {get; set; }
        [Display(Name = "Session")]
        public int SessionId { get; set; }
        [Display(Name = "Session")]
        public virtual Session? Session { get; set; }
        [Display(Name = "Excercise Type")]
        public int ExcerciseTypeId { get; set; }
        [Display(Name = "Excercise Type")]
        public virtual ExcerciseType? ExcerciseType { get; set; }
        [Display(Name = "Trainee")]
        public string TraineeId { get; set; }
        [Display(Name = "Trainee")]
        public virtual BefitUser? Trainee { get; set; }
        [Display(Name = "Weight")]
        public uint Weight { get; set; }
        [Display(Name = "Count of Series")]
        public uint SeriesCount { get; set; }
        [Display(Name = "Count of Reps")]
        public uint RepsCount { get; set; }
    }
}
