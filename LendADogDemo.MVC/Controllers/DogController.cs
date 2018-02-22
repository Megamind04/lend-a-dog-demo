using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LendADogDemo.Entities.Helpers;
using LendADogDemo.MVC.Servisis;
using LendADogDemo.MVC.ViewModels;
using Microsoft.AspNet.Identity;

namespace LendADogDemo.MVC.Controllers
{
    public class DogController : Controller
    {
        private IDogService dogService;

        public DogController()
        {

        }
        public DogController(IDogService _dogService)
        {
            dogService = _dogService;
        }

        [HttpGet]
        [AjaxAuthorize]
        public ActionResult AddNewDog()
        {
            //return View();
            return PartialView();
        }

        [HttpPost]
        [AjaxAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewDog([Bind(Exclude = "Photo,DogOwnerID,DogID")] DogViewModel dogToBeCreated, HttpPostedFileBase uploadPhoto)
        {
            string userId = User.Identity.GetUserId();
            
            //var errors = ModelState.Where(v => v.Value.Errors.Any());
            if (ModelState.IsValid)
            {
                if (dogService.CreateDog(dogToBeCreated, userId, uploadPhoto))
                {
                    return Json(true);
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Json(false);
                    //return View(dogToBeCreated);
                }
            }
            else
            {
                return Json(false);
                //return View(dogToBeCreated);
            }
        }

        [HttpPost]
        [AjaxAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDog(int DogID)
        {
            if (dogService.DeleteDog(DogID))
            {
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        [AjaxAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditDog(DogViewModel DogToBeEdited)
        {
            if (!ModelState.IsValid)
            {
                //var errorList = ModelState.Values.SelectMany(m => m.Errors)
                //                 .Select(e => e.ErrorMessage)
                //                 .ToList();
                //return Json(errorList);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (dogService.EditDog(DogToBeEdited))
            {
                return Json(true);
            }
            return Json(false);
        }

        [HttpGet]
        [AjaxAuthorize]
        public ActionResult EditDogDisplay(int DogID)
        {
            var dogForEdit = dogService.GetDog(DogID);

            return PartialView(dogForEdit);
        }

        [HttpGet]
        [AjaxAuthorize]
        public ActionResult DeleteDogDisplay(int DogID)
        {
            var dogForDelete = dogService.GetDog(DogID);

            return PartialView(dogForDelete);
        }
    }
}