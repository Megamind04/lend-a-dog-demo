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

        [HttpGet]
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

            var dogsPerUser = personalDashboardService.GetDogsPerUser(userId);

            if (dogsPerUser != null)
            {
                //JsonResult JsonDogsPerUser = new JsonResult
                //{
                //    Data = dogsPerUser,
                //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                //    MaxJsonLength = int.MaxValue
                //};
                //return JsonDogsPerUser;
                //return Json(dogsPerUser,JsonRequestBehavior.AllowGet);
                return PartialView("_PersonalDashboardDogs",dogsPerUser);
            }

            return null;
        }

        [HttpGet]
        [AjaxAuthorize]
        public ActionResult ConversationsPerUser()
        {
            string userId = User.Identity.GetUserId();

            var conversationsPerUser = personalDashboardService.GetConversationsPerUser(userId);

            return PartialView("_PersonalDashboardConversations", conversationsPerUser);
        }

        [HttpGet]
        [AjaxAuthorize]
        public ActionResult ConfirmationsPerUser()
        {
            string userId = User.Identity.GetUserId();

            var confirmationsPerUser = personalDashboardService.GetConfirmationsPerUser(userId);

            return PartialView("_PersonalDashboardConfirmations", confirmationsPerUser);
        }
    }
}