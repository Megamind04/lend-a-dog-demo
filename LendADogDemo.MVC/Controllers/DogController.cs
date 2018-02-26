using System.Net;
using System.Web;
using System.Web.Mvc;
using LendADogDemo.Entities.Helpers;
using LendADogDemo.MVC.Servisis;
using LendADogDemo.MVC.ViewModels;
using Microsoft.AspNet.Identity;

namespace LendADogDemo.MVC.Controllers
{
    [RoutePrefix("Dog")]
    public class DogController : Controller
    {
        #region Initialize

        private readonly IDogService dogService;

        public DogController()
        {

        }
        public DogController(IDogService _dogService)
        {
            dogService = _dogService;
        }

        #endregion

        [HttpGet,AjaxAuthorize,Route("AddNewDog")]
        public ActionResult AddNewDog()
        {
            return PartialView();
        }

        [HttpGet,AjaxAuthorize,Route("EditDogDisplay")]
        public ActionResult EditDogDisplay(int? DogID)
        {
            if (DogID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var dogForEdit = dogService.GetDog((int)DogID);

                return PartialView(dogForEdit);
            } 
        }

        [HttpGet, AjaxAuthorize, Route("DeleteDogDisplay")]
        public ActionResult DeleteDogDisplay(int? DogID)
        {
            if (DogID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var dogForDelete = dogService.GetDog((int)DogID);

                return PartialView(dogForDelete);
            }  
        }

        [HttpPost,ValidateAntiForgeryToken,AjaxAuthorize, Route("AddNewDog")]
        public ActionResult AddNewDog([Bind(Exclude = "Photo,DogOwnerID,DogID")] DogViewModel dogToBeCreated, HttpPostedFileBase uploadPhoto)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                string userId = User.Identity.GetUserId();

                if (dogService.CreateDog(dogToBeCreated, userId, uploadPhoto))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
        }

        [HttpPost, ValidateAntiForgeryToken, AjaxAuthorize, Route("DeleteDog/{DogID?}")]
        public ActionResult DeleteDog(int? DogID)
        {
            if(DogID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                if (dogService.DeleteDog((int)DogID))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
        }

        [HttpPost, ValidateAntiForgeryToken, AjaxAuthorize, Route("EditDog")]
        public ActionResult EditDog(DogViewModel DogToBeEdited)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                if (dogService.EditDog(DogToBeEdited))
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