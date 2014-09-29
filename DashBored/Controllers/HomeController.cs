using DashBored.Models;
using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace DashBored.Controllers
{
    public class HomeController : Controller
    {
        public DashBored.Models.dashBored Model = new DashBored.Models.dashBored();

        void GetRemoteContent()
        {

            Model.isLocal = HttpContext.Request.Url.Host == "localhost";


            if (Model.isLocal)
            {
                var email = new GmailHandler("email", "pw");
                XmlDocument myXml = email.GetGmailAtom();

                XmlElement root = myXml.DocumentElement;
                XmlNodeList xmlnode = myXml.GetElementsByTagName("entry");

                Model.gmailInboxList = new List<Models.gmailInbox>();
                for (int i = 0; i < xmlnode.Count; i++)
                {
                    var item = new gmailInbox()
                    {
                        Title = xmlnode[i].FirstChild.InnerText,
                        Author = xmlnode[i].LastChild.InnerText
                    };

                    Model.gmailInboxList.Add(item);

                };

            }



            XmlDocument doc = new XmlDocument();
            doc.Load("http://digg.com/rss/topstories.xml");
            XmlNodeList docNode = doc.GetElementsByTagName("title");
            for (int i = 1; i < docNode.Count; i++)
            {
                XmlNode item = docNode[i];
                Model.DiggItem.Add(new DiggItem()
                {
                    Title = item.InnerText,
                    Link = doc.GetElementsByTagName("link")[i].InnerText
                });



            };



            //
            XmlDocument yDoc = new XmlDocument();
            yDoc.Load("http://feeds.feedburner.com/hacker-news-feed?format=xml");
            XmlNodeList yDocNode = yDoc.GetElementsByTagName("item");
            for (int i = 1; i < yDocNode.Count; i++)
            {
                XmlNode item = yDocNode[i];
                Model.HNItem.Add(new HNItem()
                {
                    Title = ((item).FirstChild).InnerText,
                    Link = yDoc.GetElementsByTagName("link")[i].InnerText
                });



            };

            XmlDocument rDoc = new XmlDocument();
            rDoc.Load("http://www.reddit.com/.rss");
            XmlNodeList rDocNode = rDoc.GetElementsByTagName("item");
            for (int i = 0; i < 10; i++)
            {
                XmlNode item = rDocNode[i];
                Model.rItems.Add(new XmlNewsList()
                {
                    Title = ((item).FirstChild).InnerText,
                    Link = rDoc.GetElementsByTagName("link")[i].InnerText
                });



            };
        }

        void GetMarkdownFile()
        {
            string mdFile = "";
            using (StreamReader sr = new StreamReader(@"C:\mdFile.txt"))
            {
                String line = sr.ReadToEnd();

                mdFile += line;

            }

            MarkdownSharp.Markdown md = new MarkdownSharp.Markdown();

            Model.MarkDownFile = md.Transform(mdFile);
        }


        public ActionResult Index()
        {

            GetMarkdownFile();
            GetRemoteContent();

            return View(Model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Verse()
        {
            return View();
        }

    }

    /// <summary>
    /// Coming from some source online...I think StackOverflow
    /// </summary>
    public class GmailHandler
    {
        private string username;
        private string password;
        private string gmailAtomUrl;

        public string GmailAtomUrl
        {
            get { return gmailAtomUrl; }
            set { gmailAtomUrl = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public GmailHandler(string _Username, string _Password, string _GmailAtomUrl)
        {
            Username = _Username;
            Password = _Password;
            GmailAtomUrl = _GmailAtomUrl;
        }

        public GmailHandler(string _Username, string _Password)
        {
            Username = _Username;
            Password = _Password;
            GmailAtomUrl = "https://mail.google.com/mail/feed/atom";
        }
        public XmlDocument GetGmailAtom()
        {
            byte[] buffer = new byte[8192];
            int byteCount = 0;
            XmlDocument _feedXml = null;
            try
            {
                System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
                WebRequest webRequest = WebRequest.Create(GmailAtomUrl);

                webRequest.PreAuthenticate = true;

                System.Net.NetworkCredential credentials = new NetworkCredential(this.Username, this.Password);
                webRequest.Credentials = credentials;

                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();

                while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
                    sBuilder.Append(System.Text.Encoding.ASCII.GetString(buffer, 0, byteCount));


                _feedXml = new XmlDocument();
                _feedXml.LoadXml(sBuilder.ToString());


            }
            catch (Exception ex)
            {
                //add error handling
                throw ex;
            }
            return _feedXml;
        }
    }
}
