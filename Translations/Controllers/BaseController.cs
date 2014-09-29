using System.Text;
using System.Web.Mvc;
using WSOD.Web.Foundation.Controllers;
using WSOD.Web.Foundation.Controllers.ActionFilters;
using WSOD.Web.Foundation.UI;
using WSOD.Web.DOM;
using System.Web;
using System.IO;


namespace Vincent.Translations.Controllers
{
	//Snippet: Params
	[RequireSSL]
	[Params(Order = 1)]
	[ClientSideResolveUrl]
	public class BaseController : FoundationBaseController
	{
	//Snippet: Params
	public ActionResult Content(IRenderable m)
		{
			Response.Write(DefaultDocType);
			return Content(m.Render().ToString());
		}



	protected string SaveToTempFile(HttpPostedFileBase file)
		{
			string machineName = User.RequestData.FullyQualifiedMachineName;
			string fileName = Path.GetFileName(file.FileName);
			string filePath = @"\\" + machineName + @"\temp\" + System.Guid.NewGuid().ToString() + "-" + fileName;
			file.SaveAs(filePath);
			return filePath;
		}

	protected string TempLocation()
		{
			string machineName = User.RequestData.FullyQualifiedMachineName;
			return @"\\" + machineName + @"\temp\" + System.Guid.NewGuid().ToString();
		}

	}


}
