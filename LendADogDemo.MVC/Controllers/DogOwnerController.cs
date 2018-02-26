using LendADogDemo.Entities.Helpers;
using LendADogDemo.MVC.Servisis;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Mvc;
using LendADogDemo.MVC.ViewModels;

namespace LendADogDemo.MVC.Controllers
{
    [RoutePrefix("DogOwner")]
    public class DogOwnerController : Controller
    {
        #region Initialize

        private readonly IDogOwnerService dogOwnerService;

        public DogOwnerController()
        {

        }
        public DogOwnerController(IDogOwnerService _dogOwnerService)
        {
            dogOwnerService = _dogOwnerService;
        }

        #endregion

        [HttpGet,AjaxAuthorize,Route("OpenConversation/{OtherId?}")]
        public ActionResult OpenConversation(string OtherId)
        {
            if (string.IsNullOrWhiteSpace(OtherId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var privateConversations = dogOwnerService.ConversationBetweenTwoUsers(userId,OtherId);

                return PartialView(privateConversations);
            }
        }

        [HttpGet,AjaxAuthorize,Route("VerifyDogOwnerDisplay/{UserID?}")]
        public ActionResult VerifyDogOwnerDisplay(string UserID)
        {
            if (string.IsNullOrWhiteSpace(UserID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var user = dogOwnerService.GetUnverifiedDogOwner(UserID);

                return PartialView(user);
            }
        }

        [HttpGet,AjaxAuthorize,Route("DenyDogOwnerDisplay/{RequestFromID?}")]
        public ActionResult DenyDogOwnerDisplay(string RequestFromID)
        {
            if (string.IsNullOrWhiteSpace(RequestFromID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var user = dogOwnerService.GetUnverifiedDogOwner(RequestFromID);

                return PartialView(user);
            }
        }

        [HttpPost,AjaxAuthorize,Route("VerifyDogOwner/{RequestFromID?}")]
        public ActionResult VerifyDogOwner(string RequestFromID)
        {
            if (string.IsNullOrWhiteSpace(RequestFromID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                if (dogOwnerService.VerifyUser(RequestFromID))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
        }

        [HttpPost, AjaxAuthorize, Route("DenyDogOwner/{RequestFromID?}")]
        public ActionResult DenyDogOwner(string RequestFromID)
        {
            if (string.IsNullOrWhiteSpace(RequestFromID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var ReciverID = User.Identity.GetUserId();
                if (dogOwnerService.DenyUser(ReciverID,RequestFromID))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
        }

        [HttpPost,AjaxAuthorize,Route("SubmitConversation")]
        public ActionResult SubmitConversation(PrivateConversationBetweenTwoUsers conversation)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                string userId = User.Identity.GetUserId();

                if (dogOwnerService.AddConversation(userId,conversation.NewConversation))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
        }
    }
}