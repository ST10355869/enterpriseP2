using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace enterpriseP2.Controllers
{
    public class FarmerController : Controller
    {
        // GET: FarmerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FarmerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FarmerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FarmerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FarmerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FarmerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FarmerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
