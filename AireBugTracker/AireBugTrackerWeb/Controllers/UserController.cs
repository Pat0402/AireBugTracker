using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;

namespace AireUserTrackerWeb.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: UserController
        public async Task<ActionResult> IndexAsync()
        {
            var theUsers = await _userService.GetAllAsync();

            return View(theUsers.Target);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var theUser = await _userService.GetByIdAsync(id);

            return View(theUser.Target);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                await _userService.CreateAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(user);
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var theUser = await _userService.GetByIdAsync(id);

            return View(theUser.Target);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, UserDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }

                var response = await _userService.UpdateAsync(id, user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
