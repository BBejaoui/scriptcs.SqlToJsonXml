/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 3/22/2015
 * Time: 3:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.IO;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using ScriptCs.Contracts;

namespace scriptcs.SqlToXmlJson
{
	/// <summary>
	/// Description of SqlToXmlOrJson.
	/// </summary>
	public class SqlToXmlJsonContext :IScriptPackContext
	{
	   
        StringBuilder _sBuilder;
	    DataTable _table;
		public void initialize(string commandText,
	                           string entityName,
	                           string configurationFilePath=@"app.config")
		{
			//Connection string "Server=PODCAST;Database=NORTHWND;Trusted_Connection=True;";
			string cstring = scriptcs.SqlToXmlJson.ConnectionStringSettings.RetrieveInfo(configurationFilePath);
			SqlConnection connection = new SqlConnection(cstring);
			
		    try {
				SqlCommand command = new SqlCommand();
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = commandText; 
				command.Connection = connection;
				connection.Open();
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				_table = new DataTable(entityName);
				adapter.Fill(_table);
             }catch (SqlException caught) {
				
				Debug.Print(caught.Message);
			}
			finally{
				connection.Close();
			}
		}
		
		public bool serialize(SerializationType serializationType,
	                          string filePath)
		{
			
			switch (serializationType) {
				case SerializationType.JSON:
					sqltoJson(_table,filePath);
					Debug.WriteLine("Data is serializaed to JSON");
					return true;
				case SerializationType.XML:
					sqltoXml(_table,filePath);
					Debug.WriteLine("Data is serializaed to XML");
					return true;
				default:
					sqltoJson(_table,filePath);
					Debug.WriteLine("Data is serializaed to JSON");
					return true;
			}
		}
		
		protected void sqltoXml(DataTable table, string filePath){
		  
			 _sBuilder = new StringBuilder();

	        _sBuilder.Append("<?xml version=\"1.0\"?>\n");
	        _sBuilder.AppendFormat("<{0}>\n",table.TableName);  
	        foreach (DataRow drow in table.Rows)
	        {
	        	_sBuilder.AppendFormat("<{0}>\n",table.TableName.TrimEnd(new char[]{'s'}));
	        	foreach(DataColumn dColumn in table.Columns){
	        		_sBuilder.AppendFormat("<{0}>",dColumn.ColumnName);
	        		_sBuilder.AppendFormat("{0}",drow[dColumn].ToString());
	        		_sBuilder.AppendFormat("</{0}>\n",dColumn.ColumnName);                                		                             
	        	}
	        	_sBuilder.AppendFormat("</{0}>\n",table.TableName.TrimEnd(new char[]{'s'}));
	        }
	        _sBuilder.AppendFormat("</{0}>",table.TableName);
	        
			string result = _sBuilder.ToString();
			WriteToFile(filePath,result);
			
		}
		protected void sqltoJson(DataTable table,string filePath){
		    _sBuilder = new StringBuilder();

	        _sBuilder.Append("{\"");
	        _sBuilder.Append(table.TableName);
	        _sBuilder.Append("\"\n:[");

        bool first = true;
        foreach (DataRow drow in table.Rows)
        {
            if (first)
            {
                _sBuilder.Append("{\n");
                first = false;
            }
            else
                _sBuilder.Append(",{\n");

            bool firstColumn = true;
            foreach (DataColumn column in table.Columns)
            {
                if (firstColumn)
                {
                    _sBuilder.Append(string.Format("\"{0}\":\"{1}\"\n", column.ColumnName, drow[column].ToString()));
                    firstColumn = false;
                }
                else
                _sBuilder.Append(string.Format(",\"{0}\":\"{1}\"\n", column.ColumnName, drow[column].ToString()));
            }
            _sBuilder.Append("}\n");
        }

        _sBuilder.Append("]}");

          string result = _sBuilder.ToString();	
          WriteToFile(filePath,result);
		}
		
		private void WriteToFile(string path,string output)
		{
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
		        {
		            
		           file.Write(output);
		        }
		}
	}
}
