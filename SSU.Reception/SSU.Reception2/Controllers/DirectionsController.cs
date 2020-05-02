using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SSU.Reception.Models;

namespace SSU.Reception.Controllers
{
	public class DirectionsController : Controller
	{
		private readonly DirectionContext db = new DirectionContext();

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
