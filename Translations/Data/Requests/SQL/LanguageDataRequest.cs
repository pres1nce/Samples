using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSOD.Common.Web.WIT;
using WSOD.Common.Web;

namespace Vincent.Translations.Data.Requests.SQL
{


    /// <summary>
    /// Config obj to define the current instance of TranslationTool
    /// ? Build out for a proper config file
    /// </summary>
    public class LanguageDataRequest //: WitModel
    {
        private readonly int _QID = 100069;
        private readonly int _LanguageQID = -222;
        private OpenSQL _sql;
        private readonly string _marketer = "RC";
        public string _slugPrepend { get; set; }
        private Dictionary<string, string> dict = new Dictionary<string, string>();

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

        /// <summary>
        /// Umm...Retireves? But not being used
        /// </summary>
        /// <param name="language"></param>
        public void Retrieve(string language)
        {

            //_sql = Util.User.NewService<OpenSQL>();
            //_sql.Label = "Get Language Translations";
            //_sql.SetInput("Query.ID", _QID);
            //_sql.SetInput("WSODIssue", "eng");

            //_sql.SetInput("Marketer", _marketer);
            //_sql.SetInput("Language", language);
            //_sql.Retrieve();


            //if (_sql.Status > 0)
            //{
            //}



        }

        /// <summary>
        /// Updates value in DB
        /// </summary>
        /// <param name="slug"></param>
        /// <param name="value"></param>
        /// <param name="language"></param>
        public void Update(string slug, string value, string language)
        {
            _sql = WSOD.Common.Web.User.Current.NewService<OpenSQL>();
            _sql.Label = "Update Language" + _marketer + " : " + slug + " : " + value;
            _sql.SetInput("Query.ID", _LanguageQID);
            _sql.SetInput("Translate.Marketer", _marketer.ToUpper());
            _sql.SetInput("Translate.Language", language);
            _sql.SetInput("Translate.ID", slug);
            _sql.SetInput("Translate.Value", value);
            _sql.Retrieve();

        }

        /// <summary>
        /// Not being used was a lookup for output...
        /// ?Not sure where it was being used >.>
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Dictionary<string, string> buildCollection(List<string> ids)
        {



            Dictionary<string, string> collection = new Dictionary<string, string>();

            int count = _sql.ValueInt("DCLCore.RowCount");


            for (int i = 0; i < count; i++)
            {
                string id = _sql.ValueString("Row[" + i + "].ID");
                if (ids.Contains(id))
                {
                    string Message = _sql.ValueString("Row[" + i + "].Value");
                    collection.Add(_sql.ValueString("Row[" + i + "].ID"), Message);//_sql.ValueString("Row[" + i + "].Value"));
                }
            }






            return collection;

        }

        /// <summary>
        /// Not being used was a lookup for output...
        /// ?Not sure where it was being used >.>
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="langaugeObject"></param>
        /// <returns></returns>
        public Dictionary<string, string> buildCollection(List<string> ids, Data.Language.Language langaugeObject)
        {



            Dictionary<string, string> collection = new Dictionary<string, string>();

            int count = _sql.ValueInt("DCLCore.RowCount");


            for (int i = 0; i < count; i++)
            {
                string id = _sql.ValueString("Row[" + i + "].ID");
                if (ids.Contains(id))
                {


                    //string Message = "埃及鎊";
                    //string Message = langaugeObject.translate(_sql.ValueString("Row[" + i + "].ID"));
                    string Message = _sql.ValueStringUTF8("Row[" + i + "].Value");

                    collection.Add(_sql.ValueString("Row[" + i + "].ID"), Message);//_sql.ValueString("Row[" + i + "].Value"));
                }
            }






            return collection;

        }


    }
}