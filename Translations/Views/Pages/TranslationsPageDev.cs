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
	public class TranslationsPageDev : NodeAliasedModule
	{
		private Dictionary<string, string> languageMap = new Dictionary<string, string>();
		
		public TranslationModel Model { get; set; }
		public string Mode { get; set; }
		public string Person{ get; set; }


		public TranslationsPageDev() { }

		public TranslationsPageDev(string mode)
		{
			this.Mode = mode;
		}

		public TranslationsPageDev(string mode, string person)
		{
			this.Mode = mode;
			this.Person = person;
		}

		public override Node Render()
		{
			return
			Container.Add(
				RenderContent()
			);
		}


		public Element ProfileContainer()
		{

			return Element.Create("div.user").Add(
					//Element.Create("img.avatar", "src", "~/Content/Images/batpug.jpg"),
					string.Format("Welcome Back, {0}", Person)
				);

		}


		public Element RenderContent()
		{

			return Element.Create("div").AddClass(this.Mode).Add(				
				TranslationHarness()				
				).Add(
					Element.Create("div.output#output1").Add(
						Element.Create("div#myGrid", "style", "width:100%;height:720px;")
					)
				);

		}

		public INode RenderFooterModules()
		{
			var duplicates = Node("div.errors.hide").Add(Node("h3").Add("Duplicates"));
			var textArea = Node("div.slangbox-container").Add(Node("textarea.askent").AddAttribute("placeholder", "Slangbox! Start typing in here and you'll see the glyphs you can use! (Type vowels)"));
			var suggestions = Node("div.suggest").Add(
					Node("span").Add("Suggestions?  "),
					Node("a","href","mailto:vincent.cifelli@markit.com").Add("Contact Me")
				);
			return Node("div").Add(duplicates, textArea,suggestions);
			
		}


		public Node RenderForm()
		{	
			return Element.Create("div.form").Add(
				Element.Create("form", "id","myForm1", "class", "excelUpload", "action", "~/Translations/Preview", "method", "post", "enctype", "multipart/form-data").Add(
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
								Element.Create("button").AddAttribute("type", "file", "name", "file", "value", "Submit", "class", " btn btn-danger btn-large submit ").Add("Submit"),
								Element.Create("button", "class", " btn btn-primary btn-large  fltRight preview").Add("Preview")
							)

							)

				);


		}

		public Element TranslationHarness()
		{

			var content = Element.Create("div", "class", "harness").Add(
					ProfileContainer(),
					 Element.Create("span.submitButton.btn.btn-primary").Add("Upload"),
                     RenderForm(),
					 Element.Create("div.fltRight").Add(
						Element.Create("a", "class", "btn btn-info", "href", "~/Translations").Add("Tester"),
						Element.Create("a", "href", "https://wiki.wsod.local/wsodwiki/index.php/Translation_Tool:_Vincent%27s", "class", "btn ").Add("Wiki")
					 )

				);

			return content;



		}



	}


}
