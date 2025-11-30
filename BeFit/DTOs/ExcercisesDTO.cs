using BeFit.Models;
using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs
{
    public class ExcercisesDTO
    {
        public int Id { get; set; }
        [Display(Name = "Session")]
        public int SessionId { get; set; }
        [Display(Name = "Session")]
        public virtual Session? Session { get; set; }
        [Display(Name = "Excercise Type")]
        public int ExcerciseTypeId { get; set; }
        [Display(Name = "Excercise Type")]
        public virtual ExcerciseType? ExcerciseType { get; set; }
        [Display(Name = "Weight")]
        public uint Weight { get; set; }
        [Display(Name = "Count of Series")]
        public uint SeriesCount { get; set; }
        [Display(Name = "Count of Reps")]
        public uint RepsCount { get; set; }

        public ExcercisesDTO() { }
        public ExcercisesDTO(Excercise excercise) 
        {
            Id = excercise.Id;
            SessionId = excercise.SessionId;
            ExcerciseTypeId = excercise.ExcerciseTypeId;
            Weight = excercise.Weight;
            SeriesCount = excercise.SeriesCount;
            RepsCount = excercise.RepsCount;
        }
    }
}
