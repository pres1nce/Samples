using System.Web.Mvc;
using Vincent.Translations.Examples.Views.Pages;
using Vincent.Translations.Views;
using Vincent.Translations.Data.Structures;
using System.Collections.Generic;
using System;
using Vincent.Translations.Models;
using System.Text;
using System.Diagnostics;


namespace Vincent.Translations.Controllers
{
    public class TranslationsController : BaseController
    {
        public ActionResult Home(TranslationModel model)
        {

            var doc = new DefaultDocument
            {
                PageView = new TranslationsPage
                {
                    Model = new Translations.Models.TranslationModel()
                    {
                        Marketer = model.Marketer,
                        Prepend = model.Prepend,
                        User = HttpContext.User.Identity.Name.ToString()
                    }
                }
            };

            return Content(doc);
        }

        /// <summary>
        /// UNSTABLE BUILD OF TRANSLATIONS 
        /// USE FOR DEV!
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Test(TranslationModel model)
        {

            var doc = new DefaultDocument
            {
                PageView = new TranslationsPageDev("TEST", "")
                {
                    Model = new Translations.Models.TranslationModel()
                    {
                        Marketer = model.Marketer,
                        Prepend = model.Prepend
                    }
                }
            };
            return Content(doc);
        }


        /// <summary>
        /// Typical Form submission returns JSON collection if success
        /// STABLE
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload()
        {
            var marketer = HttpContext.Request.Params["marketer"];
            var slugPrepend = HttpContext.Request.Params["slugPrepend"];
            TranslationUpload trans = new TranslationUpload(slugPrepend, marketer);
            Dictionary<string, string> output = trans.UploadFile(TempLocation());
            return Json(trans._d);
        }

        /// <summary>
        /// Uploads the form async and renders the results on the page?
        /// UNSTABLE : FEATURE NOT INCLUDED IN USE
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadAPI()
        {

            var marketer = HttpContext.Request.Params["marketer"];
            var slugPrepend = HttpContext.Request.Params["slugPrepend"];
            TranslationUpload trans = new TranslationUpload(slugPrepend, marketer);
            Dictionary<string, string> output = trans.UploadFile(TempLocation(), true);
            var x = trans._d;
            return Json(x);

        }

        /// <summary>
        /// Previews the excel content
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Preview()
        {
            var marketer = HttpContext.Request.Params["marketer"];
            var slugPrepend = HttpContext.Request.Params["slugPrepend"];
            TranslationUpload trans = new TranslationUpload(slugPrepend, marketer);
            Dictionary<string, string> output = trans.UploadFile(TempLocation(), true);
            var preview = trans.Preview();
            preview = trans.spreadSheet;


            return Json(preview);
        }

        /// <summary>
        /// Promote 
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public ActionResult Promote(string env)
        {
            //determin enivronent push

            //promote translations

            return Content("TRUE");

        }





    }



}
