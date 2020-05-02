using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using SSU.Reception.Models;

namespace SSU.Reception.Controllers
{
    public class EnrolleesController : Controller
    {
        private readonly EnrolleeContext enrolleeDb = new EnrolleeContext();
        private readonly DirectionContext directionDb = new DirectionContext();
        private readonly SchoolContext schoolDb = new SchoolContext();

        private readonly int pageSize = 15;

        // GET: Enrollees
        public async Task<ActionResult> Index(bool originalCertificateOnly = false, string search = "", bool activeOnly = true, int page = 0)
        {
            ViewData["page"] = page;
            ViewData["items_count"] = enrolleeDb.Enrolles.Count();
            ViewData["size"] = pageSize;

            ViewData["originalCertificateOnly"] = originalCertificateOnly;
            ViewData["search"] = search;
            ViewData["activeOnly"] = activeOnly;

            var all = enrolleeDb.Enrolles
                .Include(x => x.FirstPriority)
                .Include(x => x.SecondPriority)
                .Include(x => x.ThirdPriority)
                .Include(x => x.School)
                .OrderBy(x => x.Surname);

            var filtred = from x in all
                          where x.Surname.Contains(search) ||
                                x.Name.Contains(search) ||
                                x.Patronymic.Contains(search)
                          select x;

            if (activeOnly)
            {
                filtred = filtred.Where(x => x.ActivityStatus == true);
            }

            if (originalCertificateOnly)
            {
                filtred = filtred.Where(x => x.OriginalCertificate == originalCertificateOnly);
            }

            return View(await filtred
                .Skip(pageSize * page)
                .Take(pageSize)
                .ToListAsync());
        }

        // GET: Enrollees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var enrollee = enrolleeDb.Enrolles
                .Include(x => x.FirstPriority)
                .Include(x => x.SecondPriority)
                .Include(x => x.ThirdPriority)
                .Include(x => x.School)
                .Where(x => x.Id == id.Value)
                .FirstOrDefault();

            if (enrollee == null)
            {
                return HttpNotFound();
            }

            return View(enrollee);
        }

        // GET: Enrollees/Create
        public ActionResult Create()
        {
            ViewBag.Directions = new SelectList(directionDb.Directions, "Id", "Name");
            ViewBag.Schools = new SelectList(schoolDb.Schools, "Id", "Name");

            return View();
        }

        // POST: Enrollees/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Enrollee enrollee)
        {
            if (ModelState.IsValid)
            {
                enrolleeDb.Enrolles.Add(enrollee);
                await enrolleeDb.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(enrollee);
        }

        // GET: Enrollees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enrollee enrollee = await enrolleeDb.Enrolles.FindAsync(id);

            if (enrollee == null)
            {
                return HttpNotFound();
            }

            ViewBag.Directions = new SelectList(directionDb.Directions, "Id", "Name");
            ViewBag.Schools = new SelectList(schoolDb.Schools, "Id", "Name");

            return View(enrollee);
        }

        // POST: Enrollees/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Enrollee enrollee)
        {
            if (ModelState.IsValid)
            {
                enrolleeDb.Entry(enrollee).State = EntityState.Modified;
                await enrolleeDb.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Directions = new SelectList(directionDb.Directions, "Id", "Name");
            ViewBag.Schools = new SelectList(schoolDb.Schools, "Id", "Name");

            return View(enrollee);
        }

        // GET: Enrollees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Enrollee enrollee = await enrolleeDb.Enrolles.FindAsync(id);

            if (enrollee == null)
            {
                return HttpNotFound();
            }

            return View(enrollee);
        }

        // POST: Enrollees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrollee enrollee = await enrolleeDb.Enrolles.FindAsync(id);
            enrolleeDb.Enrolles.Remove(enrollee);
            await enrolleeDb.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                enrolleeDb.Dispose();
                directionDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
