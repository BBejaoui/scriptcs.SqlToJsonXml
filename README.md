# scriptcs.SqlToJsonXml
This scriptcs project aims to convert a sql server datatable to json or xml file

You can visit the nuget.org to download this package.
https://www.nuget.org/packages/scriptcs.SqlToJsonXml/
Of Corse you I assume that the scriptcs is aleary installed  on your machine otherwise you can follow the 
installation instuctions in http://www.scritpcs.net/

  Once the package is downloaded from nuget.org/packages/scriptcs.SqlToJsonXml/ to a folder of your choice say 
  c:\\test for example
  
  1. Move the both files start.csx and app.config to c:\test
  2. Open Sql server management studio and create a stored procedure that retrieves data from a table of your choise
  for my case I downloaded the NORTHWND data base from codeplex and created the below stored procedure that retrieves data 
  from dbo.PRODUCTS
  
  USE [NORTHWND]
GO
/****** Object:  StoredProcedure [dbo].[EXPORTDATA]    Script Date: 3/26/2015 2:10:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Bejaoui,,B>
-- Description:	<retrieves data from products table,,>
-- =============================================
ALTER PROC [dbo].[EXPORTDATA]
AS
 BEGIN  
    SELECT * FROM dbo.Products
 END;
  3. Add the appropriate connection string to your sql server instance within the app.config file that you retrieved 
  a long with start.csx few moments ago for the convinience I strongly advise to makea visit to 
  connectionstrings.com you find there how to create the approperiate connection string according to your case. The configuration 
  file looks like this 
  <?xml version="1.0" encoding="utf-8"?>
<configuration>
	<startup>
		<supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5.2" />
	</startup>
    <connectionStrings>
	  <add name="ConnectionStringSettings" connectionString="Server=Servername;Database=dbname;Trusted_Connection=True;" />
	</connectionStrings>

</configuration>
4. Open a command line and change the directory to c:\test  using the cd command (cd c:\test)
5. Execute the command  scriptcs start.csx and wait until the job is done
The code inside start.csx should look like this bellow 

var instance = Require<SqlToXmlJsonContext>();
instance.initialize("dbo.EXPORTDATA",
	                 "Products",
	                 @"app.config");
bool result = instance.serialize(SerializationType.JSON,
	                          @"C:\test\products.json");
					 
if(result==true) Console.WriteLine("The job is done");	

Of Corse if you want xml serialization then substitute the json by xml at SerializationType enumeration as well 
as the extension

Enjoy !!!
