/*
Ostatnim, najtrudniejszym, jest stworzenie własnego kontrolera. 
Tworzymy sobie pusty kontroler StatsController (czyli nie generujemy go jako element szkieletowy). 
Dostosowujemy go, by miał dostęp do kontekstu bazy danych. 
Następnie tworzymy mu akcję Index (GET), która wyświetli statystyki wykonanych ćwiczeń. 
Każdy typ ćwiczeń ma mieć wyświetlone: 
- ile razy w ciągu ostatnich czterech tygodni było dane ćwiczenie wykonywane, 
- ile łącznie powtórzeń zostało wykonanych (liczba serii * liczba powtórzeń serii, zsumować po wszystkich sesjach) oraz 
- jakie było średnie i maksymalne obciążenie.
 */
using AspNetCoreGeneratedDocument;
using BeFit.Data;
using BeFit.Models;
using BeFit.Models.BefitViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NuGet.Versioning;

namespace BeFit.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
        private DateTime GetRecentPeriod(DateTime now)
        {
            return now.Subtract(new TimeSpan(28, 0, 0, 0));
        }
        private bool IsRecentSession(int _session) 
        {
            Session? session = _context.Session.Find(_session);
            if (session == null) 
            {
                return false; 
            }
            return DateTime.Compare(session.Start, GetRecentPeriod(DateTime.Now)) > 0;
        }

        public async Task<IActionResult> Index()
        {
            /*/ v1
            //List<Session> sessions = GetRecentSessions();
            List<Session> sessions = _context.Session.Where(GetRecentSessions).ToList();
            List<Excercise> excercises = new List<Excercise>();
            for (int _sess = 0; _sess < sessions.Count; _sess++) 
            {
                _sessObj = _context.Excercise.Where(e => e.SessionId == sessions[_sess].Id).ToList();
            }
            //*/

            /* v MS
            IQueryable<EnrollmentDateGroup> data = 
                from student in _context.Students
                group student by student.EnrollmentDate into dateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
             
             */
            /*/ v2
            //eager - singel join query with all data
            var sessions = _context.Session.Where(s=>s.TraineeId == GetUserId()).Where(IsRecentSession);
            
            foreach (Session s in sessions) 
            {
                var excercises = _context.Excercise.Where(e => e.SessionId == s.Id);
                foreach (Excercise e in excercises) 
                { 
                    _context.ExcerciseType.
                }
            }
            _context.Session.Where(IsRecentSession);
            */
            // based on https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-10.0#create-an-about-page
            var excercises = _context.Excercise.Where(e => e.TraineeId == GetUserId()).Where(e=> e.Session.Start >= DateTime.Now.AddDays(-28));
            IQueryable<StatCountGroup> data =
                from excercise in excercises
                group excercise by excercise.ExcerciseTypeId into excerciseGroup
                select new StatCountGroup()
                {
                    ExcerciseTypeId = excerciseGroup.Key,
                    ExcerciseCount = excerciseGroup.Count(),
                    TotalRepsCount = excerciseGroup.Sum(e => e.RepsCount*e.SeriesCount),
                    AvgWeight = excerciseGroup.Average(e => e.Weight),
                    MaxWeight = excerciseGroup.Max(e => e.Weight)
                };

            
            return View(await data.ToListAsync());
        }
    }
}
