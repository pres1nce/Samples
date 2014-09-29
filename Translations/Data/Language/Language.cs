using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vincent.Translations.Data.Language
{
	/// <summary>
	/// Language class for front-end
	/// </summary>
	public class Language
	{

		#region properties


		public WSOD.Common.Web.User User { get { return (WSOD.Common.Web.User)HttpContext.Current.User; } }

		public static string DefaultLanguageCode = "ENG";

		private Dictionary<string, string> translations { get; set; }

		public Dictionary<string, string> WITLanguageCodeMap = new Dictionary<string, string>()
		{
				{ "en-us", "en-US "},
				{ "ca-es", "ca-ES"},
				{ "zh-chs", "zh-CHS"},
				{ "zh-cht", "zh-CHT"},
				{"nl-nl", "nl-NL"},
				{"en-gb", "en-GB"},
				{"fr-fr", "fr-FR"},
				{"de-de", "de-DE"},
				{"he-il", "he-IL"},
				{"it-it", "it-IT"},
				{ "ja-jp", "ja-JP" }
				,{ "nn-no", "nn-NO"},
				{ "ru-ru", "ru-RU"},
				{ "es-es", "es-ES"},
				{ "sv-se", "sv-SE"}

		};

		protected WSOD.Common.Web.WIT.Language lang;
		protected string marketer;

		protected string _langCode;
		public string LanguageCode
		{
			get
			{
				return _langCode;
			}
			set
			{
				_langCode = value;
			}
		}

		#endregion



		public Language(string LanguageCode)
		{
			this.lang = new WSOD.Common.Web.WIT.Language();
			this.marketer = User.Site.Marketer;

			this.LanguageCode = LanguageCode;
		}


		//Supports passing in translations dictionary from languageDataStructures's buildCollection
		public Language(string LanguageCode, Dictionary<string, string> translations)
		{
			this.lang = new WSOD.Common.Web.WIT.Language();
			this.marketer = User.Site.Marketer;
			this.LanguageCode = LanguageCode;
			this.translations = translations;

		}

		public string GetWITLanguageCode(string SiteLanguageCode)
		{
			if (!String.IsNullOrEmpty(SiteLanguageCode) && WITLanguageCodeMap.ContainsKey(SiteLanguageCode))
			{
				return WITLanguageCodeMap[SiteLanguageCode];
			}
			else
			{
				return "EN";
			}
		}

		//Overriding the orig Get method with the UTF encoded one for RT's stuff
		public string translate(string slug)
		{
			string value;
			lang.Get(GetWITLanguageCode(LanguageCode).ToUpper(), slug, System.Text.Encoding.UTF8, out value);
			//lang.Get(GetWITLanguageCode(LanguageCode), slug, out value);


			return value;
		}

		public string translate(string slug, string languageCode)
		{
			string value;
			lang.Get(GetWITLanguageCode(languageCode), slug, out value);

			return value;
		}

		public string translate(string slug, bool debug)
		{
			string value;
			lang.Get(GetWITLanguageCode(LanguageCode), slug, out value);

			return value;
		}

		public string Value(string slug)
		{
			string s = "IR." + slug;
			string value;
			bool hasValue = this.translations.ContainsKey(s);
			if (hasValue)
			{
				value = this.translations[s];
			}
			else
			{
				lang.Get(GetWITLanguageCode(LanguageCode), s, System.Text.Encoding.UTF8, out value);
			}

			return value;


		}
	}
}