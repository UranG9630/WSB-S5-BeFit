using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class Excercise
    {
        [Display(Name = "Identyfikator")]
        public int Id {get; set; }
        [Display(Name = "Sesja Treningowa")]
        public int SessionId { get; set; }
        [Display(Name = "Sesja Treningowa")]
        public virtual Session? Session { get; set; }
        [Display(Name = "Typ Ćwiczenia")]
        public int ExcerciseTypeId { get; set; }
        [Display(Name = "Typ Ćwiczenia")]
        public virtual ExcerciseType? ExcerciseType { get; set; }
        [Display(Name = "Trenujący")]
        public string TraineeId { get; set; }
        [Display(Name = "Trenujący")]
        public virtual BefitUser? Trainee { get; set; }
        [Display(Name = "Obciążenie")]
        public uint Weight { get; set; }
        [Display(Name = "Liczba Serii")]
        public uint SeriesCount { get; set; }
        [Display(Name = "Liczba Powtórzeń")]
        public uint RepsCount { get; set; }
    }
}
