using WSOD.Web.DOM;
using WSOD.Web.Foundation.UI;


namespace Vincent.Translations.Views.Modules.Shared
{
	public class InlinesModule : IRenderable
	{
		public Node Render()
		{
			return
			Element.Create("script").AddHtml(
				string.Join("\n", Inlines.Instance.ToArray()));
		}
	}
}
