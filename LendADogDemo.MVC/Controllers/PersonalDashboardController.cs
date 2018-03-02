using System.Net;
using System.Web.Mvc;
using LendADogDemo.MVC.Services;
using Microsoft.AspNet.Identity;
using LendADogDemo.Entities.Helpers;

namespace LendADogDemo.MVC.Controllers
{
    [RoutePrefix("PersonalDashboard")]
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

        [HttpGet,Authorize,Route("PersonalDashboard")]
        public ActionResult PersonalDashboard()
        {
            return View();
        }

        [Route("ShowPhoto/{dogId:int}")]
        [HttpGet,AjaxAuthorize,Route("ShowPhoto/{dogId?}")]
        public ActionResult ShowPhoto(int? dogId)
        {
            if(dogId == null)
            {
                return null;
            }
            else
            {
                var imageToDisplay = personalDashboardService.GetLastImage((int)dogId);

                return imageToDisplay != null
                    ? File(imageToDisplay, "image/jpeg")
                    : null;
            }
        }

        [HttpGet,AjaxAuthorize,Route("DogsPerUser")]
        public ActionResult DogsPerUser()
        {
            string userId = User.Identity.GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var dogsPerUser = personalDashboardService.GetDogsPerUser(userId);

                return PartialView(dogsPerUser);
            }
        }

        [HttpGet, AjaxAuthorize, Route("ConversationsPerUser")]
        public ActionResult ConversationsPerUser()
        {
            string userId = User.Identity.GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var conversationsPerUser = personalDashboardService.GetConversationsPerUser(userId);

                return PartialView(conversationsPerUser);
            }    
        }

        [HttpGet, AjaxAuthorize, Route("ConfirmationsPerUser")]
        public ActionResult ConfirmationsPerUser()
        {
            string userId = User.Identity.GetUserId();

            if (string.IsNullOrWhiteSpace(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var confirmationsPerUser = personalDashboardService.GetConfirmationsPerUser(userId);

                return PartialView(confirmationsPerUser);
            }
        }
    }
}