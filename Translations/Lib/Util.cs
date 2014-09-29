using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSOD.Web.Foundation.Requests;
using WSOD.Common.Web;
using System.Text;
using WSOD.Web.Common;
using System.Security.Cryptography;


namespace Vincent.Translations.Lib
{
	public static class Util
	{
		public static Params Req
		{
			get
			{
				return Params.Instance;
			}
		}

		internal static EnvironmentType Environment
		{
			get
			{
				return User.RequestData.Environment;
			}
		}

		public static User User
		{
			get
			{
				return HttpContext.Current.User as User;
			}
		}

		//private static FormatBase _format = null;
		//public static FormatBase Format
		//{
		//    get
		//    {
		//        if (_format == null)
		//        {
		//            _format = new FormatBase();
		//        }
		//        return _format;
		//    }
		//}

		/// <summary>
		/// Being used in Viewer 
		/// ?Abstract this to a diff state
		/// </summary>
		/// <returns></returns>
		public static string GetBaseUrl()
		{


			Uri url = HttpContext.Current.Request.Url;
			StringBuilder baseUrl = new StringBuilder(GetScheme());
			baseUrl.Append(url.Host);
			baseUrl.Append("/");
			if (Req.IsDevelopment)
			{
				baseUrl.Append("Reuters/");
			}
			baseUrl.Append("InvestorRelations/AdvancedChart/");

			return baseUrl.ToString();
		}


		/// <summary>
		/// Not sure if I'm using this right now
		/// </summary>
		/// <returns></returns>
		public static string GetQueryString()
		{
			return HttpContext.Current.Request.QueryString.ToString();
		}

		/// <summary>
		/// Util 
		/// </summary>
		/// <returns></returns>
		public static string GetScheme()
		{
			/*
			if (Req.IsDevelopment) {
				return "http://";
			} else {
				return "http://";
			}
			 * */
			return "http://";
		}

		//public static string FormHash(string data)
		//{
		//    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

		//    // Convert the input string to a byte array and compute the hash.
		//    byte[] bytes = md5Hasher.ComputeHash(Encoding.Default.GetBytes(data));


		//    // Create a new Stringbuilder to collect the bytes
		//    // and create a string.
		//    StringBuilder sBuilder = new StringBuilder();

		//    // Loop through each byte of the hashed data 
		//    // and format each one as a hexadecimal string.
		//    for (int i = 0; i < bytes.Length; i++)
		//    {
		//        sBuilder.Append(bytes[i].ToString("x2"));
		//    }

		//    // Return the hexadecimal string.
		//    return sBuilder.ToString();
		//}

		//public static double CalculateDuration(string chartDuration, string symbol, InceptionDate inception)
		//{

		//    if (chartDuration == null)
		//    {
		//        return 0;
		//    }

		//    double days = 0;

		//    if (chartDuration == ChartDefaults.YTD)
		//    {
		//        days = GetYtdDuration();
		//    }
		//    else if (chartDuration == ChartDefaults.MAX)
		//    {
		//        days = inception.DaysSinceInception;
		//    }
		//    else
		//    {
		//        days = Convert.ToDouble(chartDuration);

		//        //if days are greater than the inception
		//        //use the inception
		//        if (days > inception.DaysSinceInception)
		//        {
		//            days = inception.DaysSinceInception;
		//        }

		//    }
		//    return days;
		//}

		//public static IntervalDataPeriod ValidateIntervalDataPeriod(int days, IntervalDataPeriod interval)
		//{

		//    string period = interval.DataPeriod;

		//    bool isValid = (days <= 31 && period == ReqParamsMap.MINUTE)
		//        || (days > 1 && period == ReqParamsMap.DAY)
		//        || (days > 7 && period == ReqParamsMap.WEEK)
		//        || (days > 31 && period == ReqParamsMap.MONTH)
		//        || (days > 93 && period == ReqParamsMap.QUARTER)
		//        || (days > 365 && period == ReqParamsMap.YEAR);

		//    if (!isValid)
		//    {
		//        interval = FindSmartFrequency(days);
		//    }
		//    return interval;
		//}

		//public static IntervalDataPeriod FindSmartFrequency(int days)
		//{
		//    KeyValuePair<int, string> prev = ReqParamsMap.SmartFrequencies.FirstOrDefault();
		//    foreach (KeyValuePair<int, string> x in ReqParamsMap.SmartFrequencies)
		//    {
		//        if (days < x.Key)
		//        {
		//            break;
		//        }
		//        prev = x;
		//    }
		//    return ReqParamsMap.Frequencies[prev.Value];
		//}

		public static int GetYtdDuration()
		{
			DateTime today = DateTime.Today;
			DateTime firstDayOfYear = new DateTime(today.Year, 1, 1);
			TimeSpan ts = today.Subtract(firstDayOfYear);
			var days = ts.Days;

			return days;
		}

		/// <summary>
		/// pass a hex string and prepends hash
		/// also supports pipe delimited hex strings
		/// </summary>
		/// <param name="hex"></param>
		/// <returns></returns>
		//public static string PrependHash(string hex)
		//{

		//    if (hex == null || (hex != null && hex.Contains('#')))
		//    {
		//        return hex;
		//    }

		//    string[] data = hex.Split('|');

		//    for (int i = 0, len = data.Length; i < len; i++)
		//    {
		//        data[i] = String.Format("#{0}", data[i]);
		//    }

		//    return string.Join("|", data);
		//}

		//public static string FormatPixel(int value)
		//{
		//    return String.Format("{0}px", value);
		//}



		public static bool IsDebugOn()
		{
			return Util.User.RequestData.IsDebugRequested.GetValueOrDefault(false);
		}


		public static bool IsTranslating()
		{
			return true;
		}

		public static string GetTempFilename(string extension)
		{
			return System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + extension;
		}

		public static string GetTempExcelFilename()
		{
			return GetTempFilename(".xls");
		}

		//public static void LogException(Exception e)
		//{
		//    Logger.LogMessage(e.Message);
		//    Logger.LogMessage(e.Source);
		//    Logger.LogMessage(e.StackTrace);
		//}
	}
}