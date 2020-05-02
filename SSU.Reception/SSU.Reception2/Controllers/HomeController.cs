using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SSU.Reception.Models;


namespace SSU.Reception.Controllers
{
    public class HomeController : Controller
	{
        private EnrolleeContext enrolleeDb = new EnrolleeContext();
        private DirectionContext directionDb = new DirectionContext();
        private readonly int pageSize = 15;

        public ActionResult Index(bool? originalCertificate, string search = "", bool activeOnly = false, int pageNum = 0)
        {
            ViewData["PageNum"] = pageNum;
            ViewData["ItemsCount"] = enrolleeDb.Enrolles.Count();
            ViewData["PageSize"] = pageSize;

            ViewData["OriginalCertificate"] = originalCertificate;
            ViewData["Search"] = search;
            ViewData["ActiveOnly"] = activeOnly;

            var all = enrolleeDb.Enrolles
                .Include(x => x.FirstPriority)
                .Include(x => x.SecondPriority)
                .Include(x => x.ThirdPriority)
                .Include(x => x.School)
                .OrderBy(x => x.Surname);

            var filtred = from x in all
                          where x.Surname.Contains(search) ||
                                x.Name.Contains(search) ||
                                x.Patronymic.Contains(search) ||
                                x.ExamYear.ToString().Contains(search) ||
                                x.PersonalPhone.Contains(search)
                          select x;

            if (activeOnly)
            {
                filtred = filtred.Where(x => x.ActivityStatus == true);
            }

            if (originalCertificate != null)
            {
                filtred = filtred.Where(x => x.OriginalCertificate == originalCertificate);
            }

            //Создание RatingViewModel
            var ratingViewModel = new RatingViewModel
            {
                Enrollees = filtred.Skip(pageSize * pageNum).Take(pageSize),
                SortedDirections = new Dictionary<Direction, IList<Enrollee>>()
            };

            foreach (var direction in directionDb.Directions)
            {
                var enrolles = enrolleeDb.Enrolles
                    .Where(x => x.FirstPriority.Name == direction.Name 
                             || x.SecondPriority.Name == direction.Name
                             || x.ThirdPriority.Name == direction.Name)
                    .OrderByDescending(TotalEnrolleePoints)
                    .ToList();

                ratingViewModel.SortedDirections.Add(direction, enrolles);
            }

            return View(ratingViewModel);
        }

        private int TotalEnrolleePoints(Enrollee enrollee)
        {
            var score = enrollee.MathScore + enrollee.RussianScore;
            if (enrollee.CSScore != null)
                score += enrollee.CSScore.Value;
            return score;
        }

        public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}