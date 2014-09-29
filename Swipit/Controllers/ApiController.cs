using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SwipIt.Core;
using ConnectionHubDemo;
using SignalR.Client.Hubs;

namespace SwipIt.Controllers
{


    public class ApiController : SwipController
    {
        //
        // GET: /Action/


        public JsonpResult Index()
        {


            var a = new
            {
                Title = "HEY"
            };

            Response.ContentType = "Application/JSON";
            return Jsonp(a);
        }



        //Post


        public ActionResult PostHub()
        {

            string testData = HttpContext.Request.Params["data"];


            var model = new ConnectionHub().test(testData);


            return Content(model);


        }


      






    }
}
