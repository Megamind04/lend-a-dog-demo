using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LendADogDemo.MVC.Servisis;
using LendADogDemo.MVC.ViewModels;
using Microsoft.AspNet.Identity;
using LendADogDemo.Entities.Helpers;
using System.Web.Script.Serialization;

namespace LendADogDemo.MVC.Controllers
{
    public class PersonalDashboardController : Controller
    {

        #region Initialize

        private readonly IPersonalDashboardService personalDashboardService;

        public PersonalDashboardController()
        {

        }
        public PersonalDashboardController(IPersonalDashboardService _personalDashboardService)
        {
            personalDashboardService = _personalDashboardService;
        }

        #endregion

        //[Authorize]
        [HttpGet]
        public ActionResult PersonalDashboardMain()
        {
            string userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            PersonalDashboardViewModel personalDashboard = personalDashboardService.GetMyPersonalDashboardModel(userId);
            return View(personalDashboard);
        }

        public ActionResult ShowPhoto(int dogId)
        {
            var imageToDisplay = personalDashboardService.GetLastImage(dogId);

            return imageToDisplay != null
                ? File(imageToDisplay, "image/jpeg")
                : null;
        }

        [HttpPost]
        [AjaxAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult AnswerToRequest(PrivateMessageBoardViewModel dataToPost)
        {
            string userId = User.Identity.GetUserId();

            //if (string.IsNullOrEmpty(userId))
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            //}

            if (!ModelState.IsValid)
            {
                return Json(data: new
                {
                    success = false,
                    errors = ModelState.Values
                });
            }
            else
            {
                if (personalDashboardService.CreatePrivetMessage(dataToPost, userId))
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult PersonalDashboard()
        {
            return View();
        }

        [HttpGet]
        [AjaxAuthorize]
        public ActionResult JsonDogsPerUser()
        {
            string userId = User.Identity.GetUserId();

            var bla = personalDashboardService.GetDogsPerUser(userId);
            
            if(bla != null)
            {
                //JsonResult igi = new JsonResult();
                //igi.Data = bla;
                //igi.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                //igi.MaxJsonLength = Int32.MaxValue;
                return Json(data: bla,behavior:JsonRequestBehavior.AllowGet);
                //return igi;
            }

            return Json(false);
        }
    }
}