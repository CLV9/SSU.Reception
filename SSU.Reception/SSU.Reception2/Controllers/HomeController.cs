using SSU.Reception.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace SSU.Reception.Controllers
{
    [Authorize]
    public class HomeController : Controller
	{
        private EnrolleeContext enrolleeDb = new EnrolleeContext();
        private DirectionContext directionDb = new DirectionContext();
        private readonly int pageSize = 15;

        public ActionResult Index(bool originalCertificateOnly = false, string search = "", bool activeOnly = true, int page = 0)
        {
            ViewData["page"] = page;
            ViewData["items_count"] = enrolleeDb.Enrolles.Count();
            ViewData["size"] = pageSize;

            ViewData["originalCertificateOnly"] = originalCertificateOnly;
            ViewData["search"] = search;
            ViewData["activeOnly"] = activeOnly;

            var filtred = enrolleeDb.FilterEnrolles(originalCertificateOnly, search, activeOnly);

            //Создание RatingViewModel
            var ratingViewModel = new RatingViewModel
            {
                Enrollees = filtred.Skip(pageSize * page).Take(pageSize),
                SortedDirections = new Dictionary<Direction, IList<Enrollee>>()
            };

            foreach (var direction in directionDb.Directions)
            {
                var enrolles = enrolleeDb.Enrolles
                    .Where(x => x.FirstPriority.Name == direction.Name 
                             || x.SecondPriority.Name == direction.Name
                             || x.ThirdPriority.Name == direction.Name)
                    .OrderByDescending(TotalEnrolleePoints)
                    .OrderByDescending(x => (int)x.ReceiptStatus)
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
	}
}