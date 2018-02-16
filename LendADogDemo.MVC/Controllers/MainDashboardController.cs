using LendADogDemo.MVC.Servisis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LendADogDemo.MVC.ViewModels;

namespace LendADogDemo.MVC.Controllers
{
    public class MainDashboardController : Controller
    {

        #region Initialize

        private readonly IMainDashboardService mainDashboardService;

        public MainDashboardController(IMainDashboardService _mainDashboardService)
        {
            mainDashboardService = _mainDashboardService;
        }

        #endregion

        [HttpGet]
        [Authorize]
        public ActionResult MainDashboard()
        {
            MainDashboardViewModel mainDashboard = mainDashboardService.GetMainDashboardModel();

            return View(mainDashboard);
        }
    }
}