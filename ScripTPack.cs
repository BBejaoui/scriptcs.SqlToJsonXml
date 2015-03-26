/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 3/23/2015
 * Time: 12:46 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using ScriptCs.Contracts;

namespace scriptcs.SqlToXmlJson
{
	/// <summary>
	/// Description of SqlToJsonOrXmlPack.
	/// </summary>
	public class ScriptPack :IScriptPack
	{
		IScriptPackContext IScriptPack.GetContext()
        {
			return new SqlToXmlJsonContext();
        }

        void IScriptPack.Initialize(IScriptPackSession session)
        {
        	session.ImportNamespace("scriptcs.SqlToXmlJson");
        	session.ImportNamespace("ScriptCs.Contracts");
        }
        void IScriptPack.Terminate()
        {
        	
        }
	}
}
