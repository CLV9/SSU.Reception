using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SSU.Reception.Models;
using System.Linq;
using System.Collections.Generic;

namespace SSU.Reception.Controllers
{
    [Authorize]
	public class DirectionsController : Controller
	{
		private readonly DirectionContext directionsDb = new DirectionContext();
		private readonly EnrolleeContext enrolleeDb = new EnrolleeContext();

		// GET: Directions
		public async Task<ActionResult> Index()
		{
			return View(await directionsDb.Directions.ToListAsync());
		}

		// GET: Directions/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Directions/Create
		[HttpPost, ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Direction direction)
		{
			if (ModelState.IsValid)
			{
				directionsDb.Directions.Add(direction);
				await directionsDb.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View(direction);
		}

		// GET: Directions/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Direction direction = await directionsDb.Directions.FindAsync(id);
			if (direction == null)
			{
				return HttpNotFound();
			}
			return View(direction);
		}

		// POST: Directions/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(Direction direction)
		{
			if (ModelState.IsValid)
			{
				directionsDb.Entry(direction).State = EntityState.Modified;
				await directionsDb.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(direction);
		}

		// GET: Directions/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Direction direction = await directionsDb.Directions.FindAsync(id);
			if (direction == null)
			{
				return HttpNotFound();
			}
			return View(direction);
		}

		// POST: Directions/Delete/5
		[HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int? id)
		{
			Direction direction = await directionsDb.Directions.FindAsync(id);
			directionsDb.Directions.Remove(direction);
			await directionsDb.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public ActionResult Rating(int? id, bool originalCertificateOnly = false, bool activeOnly = true)
		{
			if (directionsDb.Directions.FirstOrDefault(x => x.Id == id) == null)
				return HttpNotFound();

			ViewData["originalCertificateOnly"] = originalCertificateOnly;
			ViewData["activeOnly"] = activeOnly;

			// Фильтрация абитуриентов по запросу
			var filtred = enrolleeDb.GetFilterEnrollesAndSortedBySurname(originalCertificateOnly, "", activeOnly)
									.SortEnrolleesByPoints();


			// Создаём список направлений и абитуриентов, которые проходят на эти направления
			var directionsDict = new Dictionary<Direction, List<Enrollee>>();
			foreach (var dir in directionsDb.Directions)
			{
				directionsDict.Add(dir, new List<Enrollee>());
			}

			// Распределение абитуриентов
			foreach (var enrollee in filtred)
			{
				bool isPlaced = false;

				// Проверяем, помещается ли абитуриент в список по первому приоритету
				var firstPriorityId = enrollee.FirstPriority.Id;
				var firstPriorityDir = directionsDict.FirstOrDefault(x => x.Key.Id == firstPriorityId);

				// Количество абитуриентов в текущем списке по первому приоритету
				var currentFirstPriorityDirCount = firstPriorityDir.Value.Count;
				if (currentFirstPriorityDirCount < firstPriorityDir.Key.BudgetPlaces)
				{
					isPlaced = true;
					firstPriorityDir.Value.Add(enrollee);
				}
				// Если абитуриент не помещается по первому приоритету, то пробуем раскидать его во второй
				else if (enrollee.SecondPriority != null)
				{
					var secondPriorityId = enrollee.SecondPriority.Id;
					var secondPriorityDir = directionsDict.FirstOrDefault(x => x.Key.Id == secondPriorityId);

					// Количество абитуриентов в текущем списке по второму приоритету
					var currentSecondPriorityDirCount = secondPriorityDir.Value.Count;
					if (currentSecondPriorityDirCount < secondPriorityDir.Key.BudgetPlaces)
					{
						isPlaced = true;
						secondPriorityDir.Value.Add(enrollee);
					}
					// Если абитуриент не помещается по второму приоритету, то пробуем закинуть его по третьему
					else if (enrollee.ThirdPriority != null)
					{
						var thirdPriorityId = enrollee.ThirdPriority.Id;
						var thirdPriorityDir = directionsDict.FirstOrDefault(x => x.Key.Id == thirdPriorityId);

						// Количество абитуриентов в текущем списке по третьему приоритету
						var currentThirdPriorityDirCount = thirdPriorityDir.Value.Count;
						if (currentThirdPriorityDirCount < thirdPriorityDir.Key.BudgetPlaces)
						{
							isPlaced = true;
							thirdPriorityDir.Value.Add(enrollee);
						}
					}
				}

				// Если абитуриент не попал ни в 1 список, то закидываем его по первому приоритету
				if (isPlaced == false)
				{
					firstPriorityDir.Value.Add(enrollee);
				}
			}

			var targetDirection = directionsDict.First(x => x.Key.Id == id);
			ViewData["DirectionName"] = targetDirection.Key.Name;

			return View(targetDirection.Value);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				directionsDb.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
