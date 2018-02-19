using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LendADogDemo.MVC.Helpers
{
    public class CSharpRazorViewEngine : RazorViewEngine
    {
        private static string[] NewPartialViewFormats = new[] 
        {
           "~/Views/{1}/{1}PartialViews/{0}.cshtml",
           "~/Views/Shared/{0}.cshtml"
        };
        public CSharpRazorViewEngine()
        {
            AreaViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            AreaMasterLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            AreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{1}PartialViews/_{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            MasterLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            PartialViewLocationFormats = new[]
            {
                "~/Views/{1}/{1}PartialViews/_{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            FileExtensions = new[]
            {
                "cshtml"
            };
        }
    }
}