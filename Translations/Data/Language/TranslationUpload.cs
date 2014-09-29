using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vincent.Translations.Data.Requests.SQL;
using Excel;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using Vincent.Translations.Models;

namespace Vincent.Translations.Data.Structures
{
	/// <summary>
	/// Upload Object
	/// Takes values sets and uploads to DB and Outputs stack when finished
	/// Returning a dictionary for now...
	/// </summary>
	public class TranslationUpload
	{


		LanguageDataRequest lang;
		private string slugPrepend { get; set; }
		private string marketer { get; set; }
		public string[] _languages;
		public Dictionary<string, string> _d = new Dictionary<string, string>();
		private int intD = 0;

		public SpreadSheet spreadSheet = new SpreadSheet();
		private List<string> Columns = new List<string>();
		

		public TranslationUpload()
		{



		}

		/// <summary>
		/// Custom Upload based on slugPrepend and Marketer
		/// Constructor just get/sets the values
		/// </summary>
		/// <param name="slugPrepend"></param>
		/// <param name="marketer"></param>
		public TranslationUpload(string slugPrepend, string marketer)
		{

			this.slugPrepend = String.IsNullOrEmpty(slugPrepend)? "" :  slugPrepend + ".";
			this.marketer = marketer;
		}

		
		//Hebrew testing ?
		/// <summary>
		/// Updates the slug; Inserts/Updates into SQL; Updates the global dictionary;
		/// </summary>
		/// <param name="slug"></param>
		/// <param name="value"></param>
		/// <param name="language"></param>
		public void UpdateSlug(string slug, string value, string language)
		{

			if (!String.IsNullOrEmpty(language))
			{
				
				string val2 = value;
				lang = new LanguageDataRequest();
				lang._slugPrepend = slugPrepend;

				System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;
				byte[] srcBytes = utf_8.GetBytes(value);
				value = System.Text.Encoding.Default.GetString(srcBytes);

				//form is iso
				//value = System.Text.Encoding.GetEncoding(1589);

				lang.Update(slugPrepend + slug, value, language);
				_d.Add(language + "/" + slugPrepend + slug + "/" + intD++, val2);




			}



		}

		//Stable Block not sure if stil usable
		#region 
		//Stable 
		//public void UpdateSlug(string slug, string value, string language)
		//{

		//    if (!String.IsNullOrEmpty(language))
		//    {

		//        string val2 = value;
		//        lang = new LanguageDataRequest();

		//        System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;
		//        byte[] srcBytes = utf_8.GetBytes(value);
		//        value = System.Text.Encoding.Default.GetString(srcBytes);

		//            lang.Update(slugPrepend + slug, value, language);

		//        _d.Add(language + "/" + slugPrepend + slug + "/" + intD++, val2);




		//    }



		//}
		#endregion
		public SpreadSheet BuildSpreadSheet()
		{

			var error = new Errors()
				{
					Id = 0,
					CellPostion = new string[] { "A", "1" },
					errorType = ErrorType.EmptyCell
				};

			return new SpreadSheet()
			{
				Rows = new List<Row>() { },
				Row = new Row()
				{
					Columns = Columns.ToArray(),
					Cells = new List<Cell>() { }
				},
				Errors = new List<Errors>()
				{
					error
				}
			};


		}

		/// <summary>
		/// Preview
		/// </summary>
		/// <returns> Spreadsheet</returns>
		public SpreadSheet Preview()
		{
			if (this.spreadSheet == null)
			//if (this.spreadSheet == null)
			{
				List<Cell> cells = new List<Cell>() { };

				cells.Add(new Cell()
				{
					Value = "Hellow World"
				});

				var error = new Errors()
					 {
						 Id = 0,
						 CellPostion = new string[] { "A", "1" },
						 errorType = ErrorType.EmptyCell
					 };


				return new SpreadSheet()
				{
					Rows = new List<Row>() { },
					Row = new Row()
					{
						Columns = this._languages,
						Cells = new List<Cell>() { }
					},
					Errors = new List<Errors>() { }

				};
			}
			else
			{
				return spreadSheet;
			}

		}

		/// <summary>
		/// HELPER
		/// Don't know what this bugger is doing here
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public string StripNumbers(string input)
		{
			Regex regEx = new Regex("[0-9]+");
			StringBuilder sb = new StringBuilder();

			string numPull = "";

			foreach (char a in input)
			{
				if (!regEx.IsMatch(a.ToString()))
				{
					sb.Append(a);
				}
				else
				{
					numPull += a;
				}
			}

			return sb.ToString();
		}


		/// <summary>
		/// Language Definition ? Pull out?
		/// </summary>
		/// <param name="excelReader"></param>
		/// <returns></returns>
		protected string[] getLanguages(IExcelDataReader excelReader)
		{

			Columns = new List<string>();
			Columns.Add("Key");

			List<string> languages = new List<string>();
			excelReader.Read();
			for (var i = 1; i < excelReader.FieldCount; i++)
			{
				languages.Add(excelReader.GetString(i));
				Columns.Add(excelReader.GetString(i));
			};
			excelReader.Close();


			return languages.ToArray();
		}

		/// <summary>
		/// Uploads the file from tempLocation()
		/// </summary>
		/// <param name="loc"></param>
		/// <returns></returns>
		public Dictionary<string, string> UploadFile(string loc)
		{

			Dictionary<string, string> dict;

			//Ummm 
			string label = HttpContext.Current.Request.Form["exampleLabel"];

			//Grab current context of file
			HttpPostedFile hpf = HttpContext.Current.Request.Files[0] as HttpPostedFile;
			//Save file

			//set absolute path to file
			string saveFile = loc + hpf.FileName;
			

			//Try/Catch : Check if file is saved
			try
			{
				hpf.SaveAs(saveFile);
			}
			catch (Exception e)
			{

				throw e;
			}

			////////////////////////
			bool isXls = hpf.FileName.Contains(".xlsx") ? false : true;


			string filePath = saveFile;

			FileStream stream;
			IExcelDataReader excelReader;
			/// Write a conditional to support xls and xlsx
			if (isXls)
			{

				stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

				//1. Reading from a binary Excel file ('97-2003 format; *.xls)
				excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

			}
			else
			{
				//1. Reading from *.xlsx
				stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
				excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

			}

			//...
			//2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
			//IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
			//...
			//3. DataSet - The result of each spreadsheet will be created in the result.Tables
			//DataSet result = excelReader.AsDataSet();
			//...
			//4. DataSet - Create column names from first row
			excelReader.IsFirstRowAsColumnNames = true;
			DataSet result = excelReader.AsDataSet();
			List<string> slug = new List<string>();
			List<string> value = new List<string>();
			Dictionary<string, string> xlsDict = new Dictionary<string, string>();

			bool notTitle = true;
			/// Create method to grab each columns language and store it
			/// I'm thining of hard coding it for now; until i decide to make a pre canned map for it
			_languages = getLanguages(excelReader);

			while (excelReader.Read())
			{
				if (excelReader.GetString(0) == null)
				{
					break;
				}
				else if (notTitle)
				{

					string slugItem = excelReader.GetString(0);
					//for now were assuming a single Key : Value sheet
					string valueItem = String.IsNullOrEmpty(excelReader.GetString(1)) ? slugItem : excelReader.GetString(1);
					bool isMulti = Regex.Split(slugItem, "\n").Length > 1 ? true : false;
					if (isMulti)
					{
							readRow(excelReader, isMulti);
					}
					else
					{
							readRow(excelReader);
					}
				}
				notTitle = true;
			}

			//6. Free resources (IExcelDataReader is IDisposable)
			excelReader.Close();
			System.IO.File.Delete(saveFile);
			dict = new Dictionary<string, string>();
			dict.Add("Upload", "Sucess");

			for (var i = 0; i < slug.Count; i++)
			{
				dict.Add(i + slug[i].ToString(), value[i].ToString());
			}
			return _d;

		}

		/// <summary>
		/// Debug User for upload : Doesn't write to DB?
		/// UNSTABLE
		/// 
		/// </summary>
		/// <param name="loc"></param>
		/// <param name="debug"></param>
		/// <returns></returns>
		public Dictionary<string, string> UploadFile(string loc, bool debug)
		{

			Dictionary<string, string> dict;

			string label = HttpContext.Current.Request.Form["exampleLabel"];
			HttpPostedFile hpf = HttpContext.Current.Request.Files[0] as HttpPostedFile;
			//Save file
			string saveFile = loc+hpf.FileName;

			try
			{
				hpf.SaveAs(saveFile);
			}
			catch (Exception e)
			{
				
				throw e; 
			}
			////////////////////////
			bool isXls = hpf.FileName.Contains(".xlsx") ? false : true;
			string filePath = saveFile;
			FileStream stream;
			IExcelDataReader excelReader;
			/// Write a conditional to support xls and xlsx
			if (isXls)
			{
				stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
				//1. Reading from a binary Excel file ('97-2003 format; *.xls)
				excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
			}
			else
			{
				//1. Reading from *.xlsx
				 stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
				 excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
			}
		
			//...
			//2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
			//IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
			//...
			//3. DataSet - The result of each spreadsheet will be created in the result.Tables
			//DataSet result = excelReader.AsDataSet();
			//...
			//4. DataSet - Create column names from first row
			excelReader.IsFirstRowAsColumnNames = true;
			DataSet result = excelReader.AsDataSet();


			List<string> slug = new List<string>();
			List<string> value = new List<string>();
			Dictionary<string, string> xlsDict = new Dictionary<string, string>();


			//5. Data Reader methods
			bool notTitle = true;
			/// Create method to grab each columns language and store it
			/// I'm thining of hard coding it for now; until i decide to make a pre canned map for it
			_languages = getLanguages(excelReader);
			this.spreadSheet = BuildSpreadSheet();
			while (excelReader.Read())
			{
				//If null or EOF
				if (excelReader.GetString(0) == null)
				{
					break;
				}
				else if (notTitle)
				{

					string slugItem = excelReader.GetString(0);
					//for now were assuming a single Key : Value sheet
					string valueItem = String.IsNullOrEmpty(excelReader.GetString(1)) ? slugItem : excelReader.GetString(1);
					bool isMulti = Regex.Split(slugItem, "\n").Length > 1 ? true : false;
					if (isMulti)
					{
					    if (!debug)
					    {
						    readRow(excelReader, isMulti);
					    }
					}
					else
					{
						readRow(excelReader, isMulti, debug);
					}
				}
				notTitle = true;
			}

			//6. Free resources (IExcelDataReader is IDisposable)
			excelReader.Close();
			System.IO.File.Delete(saveFile);
			dict = new Dictionary<string, string>();
			dict.Add("Upload", "Sucess");

			for (var i = 0; i < slug.Count; i++)
			{
				dict.Add(i + slug[i].ToString(), value[i].ToString());
			}
			return _d;
		}

		/// <summary>
		/// Reads current row of spreadsheet
		/// </summary>
		/// <param name="excelReader"></param>
		private void readRow(IExcelDataReader excelReader)
		{
			string slugItem = excelReader.GetString(0);

			for (var i = 1; i < excelReader.FieldCount; i++)
			{
				string valueItem = String.IsNullOrEmpty(excelReader.GetString(i)) ? this.slugPrepend+slugItem : excelReader.GetString(i);
				string x = _languages[i - 1] + "  Value  : " + valueItem;
				//_d.Add(slugItem + "(" + _languages[i-1] + ")", valueItem);


				this.UpdateSlug(slugItem, valueItem, _languages[i - 1]);


			}


		}

        /// <summary>
        /// Reads row for debugging
        /// </summary>
        /// <param name="excelReader"></param>
        /// <param name="isMulti"></param>
        /// <param name="debug"></param>
		private void readRow(IExcelDataReader excelReader, bool isMulti, bool debug)
		{
			string slugItem = excelReader.GetString(0);

			Row row = new Row()
			{
				Cells = new List<Cell>() { }
			};

			row.Cells.Add(new Cell()
			{
				Value = slugItem
			});

			for (var i = 1; i < excelReader.FieldCount; i++)
			{
				string valueItem = String.IsNullOrEmpty(excelReader.GetString(i)) ? this.slugPrepend + slugItem : excelReader.GetString(i);
				string x = _languages[i - 1] + "  Value  : " + valueItem;
				try
				{
					_d.Add(slugItem + "(" + _languages[i - 1] + ")", valueItem);
				}
				catch (Exception e)
				{

					this.renderError(excelReader.Depth, valueItem);
					continue;

				}


				row.Cells.Add(new Cell()
				{
					Value = valueItem
				});

			}

			try
			{
				this.spreadSheet.Rows.Add(row);
			}
			catch (Exception e)
			{
				throw e;
			}


		}

        /// <summary>
        /// Adds Render error to specified item
        /// </summary>
        /// <param name="indexValue"></param>
        /// <param name="valueItem"></param>
		private void renderError(int indexValue, string valueItem)
		{
			this.spreadSheet.Errors.Add(new Errors()
			{
				Id = indexValue,
				Error = String.Format(valueItem),
				errorType = ErrorType.Duplicate
			});

		}

		/// <summary>
		/// Reads current row of spreadsheet but breaks out multi values stuck in cells...
		/// ? Consider writin formatting or better helpers to elleviate this problem
		/// </summary>
		/// <param name="excelReader"></param>
		/// <param name="isMulti"></param>
		private void readRow(IExcelDataReader excelReader, bool isMulti)
		{

			string slugItem = excelReader.GetString(0);
			//

			#region
			//string valueItem = String.IsNullOrEmpty(excelReader.GetString(1)) ? slugItem : excelReader.GetString(1);


			//string[] stack = Regex.Split(slugItem, "\n");
			//string[] stackValue = Regex.Split(valueItem, "\n");



			//for (var i = 0; i < stack.Length; i++)
			//{
			//    //slug.Add(stack[i]);
			//    //value.Add(stackValue[i]);
			//    string x = stack[i] + " : " + stackValue[i];
			//    //	stack[i]
			//    //this.UpdateSlug(stack[i], stackValue[i], label);


			//}
			#endregion

			Dictionary<string, string> d = new Dictionary<string, string>();


			for (var i = 1; i < excelReader.FieldCount; i++)
			{
				string valueItem = String.IsNullOrEmpty(excelReader.GetString(i)) ? slugItem : excelReader.GetString(i);
				string[] stack = Regex.Split(slugItem, "\n");
				string[] stackValue = Regex.Split(valueItem, "\n");

				for (var j = 0; j < stack.Length; j++)
				{
					//slug.Add(stack[i]);
					//value.Add(stackValue[i]);
					//string x = stack[j] + " ( " + _languages[i]  +  "  )  : " + stackValue[j];
					d.Add(stack[j] + " ( " + _languages[i - 1] + "  )", stackValue[j]);
					//	stack[i]
					this.UpdateSlug(stack[j], stackValue[j], _languages[i - 1]);


				}







			}

			//append to global dict
			Dictionary<string, string> x = d;




		}



	}
}