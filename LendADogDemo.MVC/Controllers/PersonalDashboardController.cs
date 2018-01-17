using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LendADogDemo.MVC.Servisis;
using LendADogDemo.MVC.ViewModels;
using Microsoft.AspNet.Identity;

namespace LendADogDemo.MVC.Controllers
{
    public class PersonalDashboardController : Controller
    {
        private IPersonalDashboardService _personalDashboardService;

        public PersonalDashboardController()
        {
                
        }
        public PersonalDashboardController(IPersonalDashboardService personalDashboardService)
        {
            _personalDashboardService = personalDashboardService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult PersonalDashboardMain(string userId)
        {
            PersonalDashboardViewModel personalDashboard = new PersonalDashboardViewModel();
            
            if(userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            personalDashboard = _personalDashboardService.GetMyPersonalDashboardModel(userId);
            return View(personalDashboard);
        }
    }
}