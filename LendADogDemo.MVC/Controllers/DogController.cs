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
    public class DogController : Controller
    {
        private IDogService _dogService;

        public DogController()
        {

        }
        public DogController(IDogService dogService)
        {
            _dogService = dogService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddNewDog()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewDog([Bind(Exclude = "Photo,DogOwnerID,DogID")] DogViewModel dogToBeCreated, HttpPostedFileBase uploadPhoto)
        {
            string userId = User.Identity.GetUserId();
            
            //var errors = ModelState.Where(v => v.Value.Errors.Any());
            if (ModelState.IsValid)
            {
                if (_dogService.CreateDog(dogToBeCreated, userId, uploadPhoto))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(dogToBeCreated);
                }
            }
            else
            {
                return View(dogToBeCreated);
            }
        }
    }
}