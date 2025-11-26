using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Excercise
    {
        public int Id {get; set; }
        public int SessionId { get; set; }
        public virtual Session? Session { get; set; }
        public int ExcerciseTypeId { get; set; }
        public virtual ExcerciseType? ExcerciseType { get; set; }
        public string TraineeId { get; set; }
        public virtual BefitUser? Trainee { get; set; }
        public uint Weight { get; set; }
        public uint SeriesCount { get; set; }
        public uint RepsCount { get; set; }
    }
}
