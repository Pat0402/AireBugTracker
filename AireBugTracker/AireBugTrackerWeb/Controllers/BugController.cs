using DatabaseContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;

namespace AireBugTrackerWeb.Controllers
{
    public class BugController : Controller
    {
        private IBugService _bugService;

        public BugController(IBugService bugService)
        {
            _bugService = bugService;
        }

        // GET: BugController
        public async Task<ActionResult> IndexAsync()
        {
            var theBugs = await _bugService.GetAllAsync();

            return View(theBugs.Target);
        }

        // GET: BugController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var theBug = await _bugService.GetByIdAsync(id);

            return View(theBug.Target);
        }

        // GET: BugController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BugController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(BugDTO bug)
        {
            try
            {
                await _bugService.CreateAsync(bug);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BugController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var theBug = await _bugService.GetByIdAsync(id);

            return View(theBug.Target);
        }

        // POST: BugController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, BugDTO bug)
        {
            try
            {
                var response = await _bugService.UpdateAsync(id, bug);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
