// based on https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-10.0#create-an-about-page
using System.ComponentModel.DataAnnotations;

namespace BeFit.Models.BefitViewModels
{
    public class StatCountGroup
    {
        [Display(Name = "Ilość ćwiczeń")]
        public int ExcerciseCount { get; set; }
        [Display(Name = "Ilość powtórzeń")]
        public long TotalRepsCount { get; set; }
        [Display(Name = "Średnie obciążenie")]
        public double AvgWeight { get; set; }
        [Display(Name = "Maksymalne obciążenie")]
        public uint MaxWeight { get; set; }
        public string ExcerciseName { get { return ExcerciseType.Name; } }
        public int ExcerciseTypeId { get; set; }
        public virtual ExcerciseType? ExcerciseType { get; set; }
    }
}
