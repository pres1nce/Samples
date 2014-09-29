using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashBored.Models
{
    public class gmailInbox
    {
        public string Title { get; set; }
        public string Author { get; set; }

    }

    public class XmlNewsList
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
    }

    public class DiggItem : XmlNewsList
    { }

    public class HNItem : XmlNewsList
    { }



    public class dashBored
    {
        public dashBored() { }
        public bool isLocal { get; set; }
        public List<gmailInbox> gmailInboxList { get; set; }
        public List<DiggItem> DiggItem = new List<DiggItem>();
        public List<HNItem> HNItem = new List<HNItem>();
        public List<XmlNewsList> rItems = new List<XmlNewsList>();
        public string MarkDownFile { get; set; }




    }
}