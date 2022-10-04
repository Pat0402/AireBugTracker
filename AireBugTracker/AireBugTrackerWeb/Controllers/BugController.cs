using AireBugTrackerWeb.Models;
using DatabaseContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using Services.Models;
using System.Diagnostics;

namespace AireBugTrackerWeb.Controllers
{
    public class BugController : Controller
    {
        private IBugService _bugService;
        private IUserService _userService;

        public BugController(IBugService bugService, IUserService userService)
        {
            _bugService = bugService;
            _userService = userService;
        }

        // GET: BugController
        public async Task<ActionResult> IndexAsync()
        {
            var theBugs = await _bugService.GetOpenBugsAsync();

            return View(theBugs.Target);
        }

        // GET: BugController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var theBug = await _bugService.GetByIdAsync(id);

            if(theBug.Status == System.Net.HttpStatusCode.NotFound)
            {
                Response.StatusCode = 404;
                return View("404");
            }

            return View(theBug.Target);
        }

        // GET: BugController/Create
        public async Task<ActionResult> CreateAsync()
        {
            await PopulateUserSelect(null);

            return View();
        }

        // POST: BugController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(BugDTO bug)
        {
            if (!ModelState.IsValid)
            {
                await PopulateUserSelect(bug.UserId);
                return View(bug);
            }

            await _bugService.CreateAsync(bug);

            return RedirectToAction(nameof(Index));
        }

        // GET: BugController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var theBug = await _bugService.GetByIdAsync(id);
            await PopulateUserSelect(null);

            return View(theBug.Target);
        }

        // POST: BugController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, BugDTO bug)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await PopulateUserSelect(bug.UserId);
                    return View(bug);
                }

                var response = await _bugService.UpdateAsync(id, bug);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await PopulateUserSelect(bug.UserId);
                return View(bug);
            }
        }

        private async Task PopulateUserSelect(int? selected)
        {
            var userResponse = await _userService.GetAllAsync();
            var theUsers = userResponse.Target.Select(u => new SelectUserViewModel
            {
                Name = u.Name,
                Id = u.Id
            }).ToList();
            theUsers.Insert(0, new SelectUserViewModel { Name = "Please select a user", Id = null });

            var userSelectList = new SelectList(theUsers, "Id", "Name", selected);
            ViewBag.AllUsers = userSelectList;
        }
    }
}
