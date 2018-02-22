using LendADogDemo.Entities.Helpers;
using LendADogDemo.MVC.Servisis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LendADogDemo.MVC.Controllers
{
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


        [HttpGet]
        [AjaxAuthorize]
        public ActionResult VerifyDogOwnerDisplay(string UserID)
        {
            var user = dogOwnerService.GetUnverifiedDogOwner(UserID);

            return PartialView(user);
        }
    }
}