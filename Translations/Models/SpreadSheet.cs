using WSOD.Web.Foundation.Modeling;
using System.Collections.Generic;


namespace Vincent.Translations.Models
{

	public  class Row{
		public  string[] Columns { get; set; }
		public List<Cell> Cells;
	}

	public class Cell
	{
		public string Value;
	}

	public  class SpreadSheet
	{
		public Row Row { get; set; }
		public List<Row> Rows { get; set; }
		public List<Errors> Errors{ get; set; }
	}


	public class Errors
	{
		public int Id;
		public ErrorType errorType;
		public string Error;
		public string[] CellPostion;

	}


	public enum ErrorType{
		EmptyCell,
		Duplicate
	}




}

