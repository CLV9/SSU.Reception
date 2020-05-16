using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SSU.Reception.Models;
using System.Linq;
using System.Collections.Generic;

namespace SSU.Reception.Controllers
{
	public class DirectionsController : Controller
	{
		private readonly DirectionContext db = new DirectionContext();
		private readonly EnrolleeContext enrolleeDb = new EnrolleeContext();

		// GET: Directions
		public async Task<ActionResult> Index()
		{
			return View(await db.Directions.ToListAsync());
		}

		// GET: Directions/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Directions/Create
		// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
		// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Name,BudgetPlaces")] Direction direction)
		{
			if (ModelState.IsValid)
			{
				db.Directions.Add(direction);
				await db.SaveChangesAsync();
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
			Direction direction = await db.Directions.FindAsync(id);
			if (direction == null)
			{
				return HttpNotFound();
			}
			return View(direction);
		}

		// POST: Directions/Edit/5
		// Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
		// сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Name,BudgetPlaces")] Direction direction)
		{
			if (ModelState.IsValid)
			{
				db.Entry(direction).State = EntityState.Modified;
				await db.SaveChangesAsync();
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
			Direction direction = await db.Directions.FindAsync(id);
			if (direction == null)
			{
				return HttpNotFound();
			}
			return View(direction);
		}

		// POST: Directions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int? id)
		{
			Direction direction = await db.Directions.FindAsync(id);
			db.Directions.Remove(direction);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public ActionResult Rating(int? id, bool originalCertificateOnly = false, bool activeOnly = true)
		{
			if (db.Directions.FirstOrDefault(x => x.Id == id) == null)
				return HttpNotFound();

			ViewData["originalCertificateOnly"] = originalCertificateOnly;
			ViewData["activeOnly"] = activeOnly;

			// Фильтрация абитуриентов по запросу
			var filtred = enrolleeDb.FilterEnrolles(originalCertificateOnly, "", activeOnly)
									.OrderByDescending(TotalEnrolleePoints)
									.OrderByDescending(x => (int)x.ReceiptStatus);
							 

			// Создаём список направлений и абитуриентов, которые проходят на эти направления
			var directionsDict = new Dictionary<Direction, List<Enrollee>>();
			foreach (var dir in db.Directions)
			{
				directionsDict.Add(dir, new List<Enrollee>());
			}

			foreach (var enrollee in filtred)
			{
				// Проверяем, помещается ли абитуриент в список по первому приоритету
				var firstPriorityId = enrollee.FirstPriority.Id;
				var firstPriorityDir = directionsDict.FirstOrDefault(x => x.Key.Id == firstPriorityId);

				// Количество абитуриентов в текущем списке по первому приоритету
				var currentFirstPriorityDirCount = firstPriorityDir.Value.Count;
				if (currentFirstPriorityDirCount < firstPriorityDir.Key.BudgetPlaces)
				{
					firstPriorityDir.Value.Add(enrollee);
				}
				// Если абитуриент не помещается по первому приоритету, то пробуем раскидать его во второй
				else if (enrollee.SecondPriority != null)
				{
					var secondPriorityId = enrollee.SecondPriority.Id;
					var secondPriorityDir = directionsDict.FirstOrDefault(x => x.Key.Id == secondPriorityId);

					// Количество абитуриентов в текущем списке по второму приоритету
					var currentSecondPriorityDirCount = firstPriorityDir.Value.Count;
					if (currentSecondPriorityDirCount < secondPriorityDir.Key.BudgetPlaces)
					{
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
							thirdPriorityDir.Value.Add(enrollee);
						}
					}
				}
			}

			var targetDirection = directionsDict.First(x => x.Key.Id == id);

			return View(targetDirection.Value);
		}

		private int TotalEnrolleePoints(Enrollee enrollee)
		{
			var score = enrollee.MathScore + enrollee.RussianScore;

			if (enrollee.CSScore != null)
				score += enrollee.CSScore.Value;

			return score;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
