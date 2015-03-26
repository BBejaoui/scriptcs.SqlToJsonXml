/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 3/22/2015
 * Time: 3:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;   
using System.Configuration;

namespace scriptcs.SqlToXmlJson
{
	/// <summary>
	/// Configuration section &lt;ConnectionString&gt;
	/// </summary>
	/// <remarks>
	/// Assign properties to your child class that has the attribute 
	/// <c>[ConfigurationProperty]</c> to store said properties in the xml.
	/// </remarks>
	static public class ConnectionStringSettings
	{
		
		static public string RetrieveInfo(string configFilePath)
		{
		    /* This code provides access to configuration files using OpenMappedExeConfiguration,method. You can use the OpenExeConfiguration method instead. For further informatons, consult the MSDN, it gives you more inforamtions about config files access methods*/
		    ExeConfigurationFileMap oConfigFile = new ExeConfigurationFileMap();
		    oConfigFile.ExeConfigFilename = configFilePath;
		    Configuration oConfiguration = ConfigurationManager.OpenMappedExeConfiguration(oConfigFile, ConfigurationUserLevel.None);
		    //Define the name of the connection
		    string oName = oConfiguration.ConnectionStrings.ConnectionStrings["ConnectionStringSettings"].Name;
		    //Define the connection string of the connection
		    string oConnectionString = oConfiguration.ConnectionStrings.ConnectionStrings["ConnectionStringSettings"].ConnectionString;
		    //Show the parameters oNname oConnectionString
		    return oConnectionString;
		    
		}
	}
	
}

