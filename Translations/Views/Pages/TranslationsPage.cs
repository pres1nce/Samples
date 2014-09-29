using WSOD.Web.DOM;
using WSOD.Web.Foundation.UI;
using System.Collections.Generic;
using WSOD.Web.Foundation.Helpers;

using Vincent.Translations.Lib;
using Vincent.Translations.Models;
using System.Web.Security;
using WSOD.Common.Web;


namespace Vincent.Translations.Examples.Views.Pages
{
	public class TranslationsPage : NodeAliasedModule
	{

		Data.Language.Language languageObject;
		private Dictionary<string, string> languageMap = new Dictionary<string, string>();
		BrowserDetect browser = new BrowserDetect();
		public TranslationModel Model { get; set; }
		public string Mode { get; set; }



		public TranslationsPage() { }

		public TranslationsPage(string mode)
		{
			this.Mode = mode;
		}

		public override Node Render()
		{
			return
			Container.Add(
				RenderContent()
				);
		}

		public Element RenderContent()
		{
			#region

			//var content = Element.Create("div", "class", "translationCtr").Add(
			//        Element.Create("h1").Add("Translation Tester"),
			//        Element.Create("form").Add(
			//            Element.Create("label").Add("Upload your Excel file"),
			//            Element.Create("input").AddAttribute("type", "label", "name", "exampleLabel", "class", "hidden"),
			//            Element.Create("input").AddAttribute("type", "file", "name", "file")
			//    //		Element.Create("input").AddAttribute("type", "submit", "class", Util.Req.IsDevelopment ? "" : "hidden").AddAttribute("Value", "Submit")
			//        ).AddAttribute("action", "upload", "method", "post", "enctype", "multipart/form-data")

			//    );


			//Element directions = Element.Create("div").Add(Element.Create("h2", "class", "bold").Add("Click on one of the culture names to load that version of the page"));

			//content.Add(directions);

			//var list = Element.Create("ul", "style", "float:left");

			//content.Add(list);

			//content.Add(Element.Create("h2", "class", "bold").Add("If you wanted to update the query string just edit it in the address bar below and click on the 'go' button."));

			//string url = "";
			#endregion //Old crap

			Element iframe = Element.Create("iframe", "src", "#", "style", "width:1000px; height :1000px;");

			return Element.Create("div").AddClass(this.Mode).Add(
				TranslationHarness(),
				RenderForm()
				);


		}


		public Node RenderForm()
		{
			return Element.Create("div.form").Add(
					Element.Create("form", "class", "excelUpload", "action", "~/Translations/Upload", "method", "post", "enctype", "multipart/form-data").Add(
							Element.Create("span.label").Add("Upload your Excel file"),
							Element.Create("button.btn.btn-mini.fltRight", "type", "button", "data-clickaction", "close").Add("X"),
							Element.Create("div.controls").Add(
								Element.Create("input", "type", "label", "name", "marketer", "class", "", "placeholder", "Marketer", "value", Model.Marketer)
							),
							Element.Create("div.controls").Add(
								Element.Create("input", "type", "label", "name", "slugPrepend", "class", "", "placeholder", "Slug Prepend (Optional)", "value", Model.Prepend)
							),
							Element.Create("div.controls").Add(
								Element.Create("input", "type", "file", "name", "file", "placeholder", "file")
							),
							Element.Create("div.controls").Add(
								Element.Create("button").AddAttribute("type", "file", "name", "file", "value", "Submit", "class", " btn btn-primary btn-large submit").Add("Submit")
							)
					)
				);


		}

		public Element TranslationHarness()
		{

			var content = Element.Create("div", "class", "harness").Add(
					Element.Create("span.submitButton.btn.btn-primary").Add("Upload"),
					Element.Create("div.fltRight").Add(
						Element.Create("a", "class", "btn btn-info", "href", "~/Translations/Test?marketer=xx&Prepend=xx").Add("Debugger"),
				 		Element.Create("a","href","https://wiki.wsod.local/wsodwiki/index.php/Translation_Tool:_Vincent%27s", "class","btn").Add("Wiki")
					 )
					




				);




			var list = Element.Create("select", "class", "languageSelect");
			languageObject = new Data.Language.Language("en-us");
			languageMap = languageObject.WITLanguageCodeMap;




			string srcPath = Util.Req.IsDevelopment ? "/Reuters/InvestorRelations/AdvancedChart/?symbol=msft.o&language=" : "/InvestorRelations/AdvancedChart/?symbol=msft.o&language=";

			foreach (var item in languageMap)
			{
				list.Add(Element.Create("option").Add(Element.Create("a").AddAttribute("class", "langLink", "href", srcPath + item.Value.ToLower()).Add(item.Value)));

			}


			return content;



		}



	}


}
