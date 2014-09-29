using WSOD.Web.DOM;
using System.Linq;
//Removed
using WSOD.Web.ResourceManagement;
using WSOD.Web.Foundation.UI;
using Vincent.Translations.Views.Modules.Shared;


namespace Vincent.Translations.Views
{
	public class DefaultDocument : HtmlDocument
	{
		public override Node Render()
		{
			ResourceManager rm = ResourceManager.Default;

			Body.Add(PageView);

			Head.Add(
				Element.Create("title").Add(Title));
            //Head.AddHtml('<link rel="shortcut icon" href="/path/to/favicon.ico">');

                			// Package Styles
			Head.AddHtml(
				rm.RenderStylesPackageHtml("~/Content/Packages/Common.css.package")
				);

			// Added Styles
			Head.AddHtml(
				rm.RenderStylesListHtml(Styles.Instance.Distinct()));

//            Head.AddHtml(
//                    @"<link media=""all"" rel=""stylesheet"" type=""text/css"" href=""/Includes/csslib/wsodBase.1.css"">
//					<link media=""all"" rel=""stylesheet"" type=""text/css"" href=""Content/Styles/Site.css"">
//					<link media=""all"" rel=""stylesheet"" type=""text/css"" href=""Content/Styles/bootstrap.css"">"
//                );


		

			// Setup WSOD Object
//            Body.AddHtml(
//                @"<script type=""text/javascript"">var WSOD = {};</script>
//				<script type=""text/javascript"" src=""/Includes/jslib/jquery/jquery-1.8.0.min.js""></script>
//				<script type=""text/javascript"" src=""/Includes/jslib/Function/Function.1.js""></script>
//				<script type=""text/javascript"" src=""/Includes/jslib/date.js"" ></script>				
//				<script type=""text/javascript"" src=""/includes/jslib/jQuery/serializer/jquery.serializer-1.js"" ></script>
//				<script type=""text/javascript"" src=""/Includes/jslib/Serializer/serializer.3.js"" ></script>			
//				<script type=""text/javascript"" src=""Content/Scripts/Libs/Module.js""></script>
//				<script type=""text/javascript"" src=""Content/Scripts/Libs/Helpers.js""></script>
//				<script type=""text/javascript"" src=""Content/Scripts/Translations.js""></script>
//				");

			Body.AddHtml(
		          @"<script type=""text/javascript"">var WSOD = {};</script>"
				);

			// Adds inlines, and scripts.

			// Possibly Analytics Code Could be added here:
			// Body.Add(new AnalysticsModule());
			// or
			// Body.Add(new HitBoxModule());
			// etc

			// Package Scripts
			Body.AddHtml(
				rm.RenderScriptsPackageHtml("~/Content/Packages/Common.js.package")
			);
				


		

			// Added Scripts
			Body.AddHtml(
			rm.RenderScriptsListHtml(Scripts.Instance.Distinct()));

			// Inline Scripts 
			// TODO: See also the InlinesScriptModule in WSOD.Web.Foundation.UI
			//		That has a more sophisticated renderring.
			Body.Add(new InlinesModule());


			

			return Container;
		}
	}
}
