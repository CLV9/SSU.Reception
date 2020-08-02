using SSU.Reception.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace SSU.Reception.Controllers
{
    [Authorize]
    public class HomeController : Controller
	{
        private readonly EnrolleeContext enrolleeDb = new EnrolleeContext();
        private readonly DirectionContext directionDb = new DirectionContext();
        private readonly int pageSize = 5000;

        public ActionResult Index(bool originalCertificateOnly = false, string search = "", bool activeOnly = true, int page = 0)
        {
            ViewData["page"] = page;
            ViewData["items_count"] = enrolleeDb.Enrolles.Count();
            ViewData["size"] = pageSize;

            ViewData["originalCertificateOnly"] = originalCertificateOnly;
            ViewData["search"] = search;
            ViewData["activeOnly"] = activeOnly;

            var filtred = enrolleeDb.GetFilterEnrollesAndSortedBySurname(originalCertificateOnly, search, activeOnly);

            //Создание RatingViewModel
            var ratingViewModel = new RatingViewModel
            {
                Enrollees = filtred
                    .SortEnrolleesByPoints()
                    .Skip(pageSize * page)
                    .Take(pageSize),
                SortedDirections = new Dictionary<Direction, IList<Enrollee>>()
            };

            foreach (var direction in directionDb.Directions)
            {
                var enrolles = enrolleeDb.Enrolles
                    .Where(x => x.FirstPriority.Name == direction.Name
                             || x.SecondPriority.Name == direction.Name
                             || x.ThirdPriority.Name == direction.Name);

                var sortedEnrollees = direction.SortEnrolleesByPoints(enrolles).ToList();

                ratingViewModel.SortedDirections.Add(direction, sortedEnrollees);
            }

            return View(ratingViewModel);
        }
    }
}