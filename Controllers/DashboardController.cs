using Microsoft.AspNetCore.Mvc;
using Whitees.Extensions;
using Whitees.Interfaces;
using Whitees.ViewModels;

namespace Whitees.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository
       )
        {
            _dashboardRepository = dashboardRepository;

        }

        public async Task<IActionResult> Index()
        {
            var userShirts = await _dashboardRepository.GetShirts();
            if (userShirts.Count <= 0)
            {
                TempData["Error"] = "You haven't posted any Whitees yet. Add a new one.";
            }
            var dashboardVM = new DashboardViewModel
            {
                Shirts = userShirts,

            };

            return View(dashboardVM);
        }


    }
}