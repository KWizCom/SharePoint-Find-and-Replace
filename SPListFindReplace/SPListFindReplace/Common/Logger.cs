using System;
using System.IO;

namespace KWizCom.SharePoint.Utilities.SPListFindReplace.Common
{
	/// <summary>
	/// Summary description for Logger.
	/// </summary>
	public class Logger
	{
		public const string DEFAULT_LOG_FILENAME = "SharePointFR.txt";

		private string m_LogFileName;
		private string m_LogFileBaseName = DEFAULT_LOG_FILENAME;

		/// <summary>
		/// PUblic constructor
		/// </summary>
		/// <param name="logFileLocation"></param>
		public Logger(string logFileLocation)
		{
			m_LogFileName = Path.Combine(logFileLocation, DateTime.Now.ToString("yyMMdd") + m_LogFileBaseName);
		}

		/// <summary>
		/// Get/Set property with full path to log file
		/// </summary>
		public string LogFileLocation
		{
			get { return m_LogFileName; }
			set { m_LogFileName = value; }
		}

		/// <summary>
		/// Get/Set property with base log file name.
		/// </summary>
		public string LogFileBaseName
		{
			get { return m_LogFileBaseName; }
			set { m_LogFileBaseName = value; }
		}

		/// <summary>
		/// Write log
		/// </summary>
		/// <param name="message"></param>
		/// <param name="sourceName"></param>
		/// <param name="ex"></param>
		public void Write(string message, string sourceName, Exception ex)
		{
			StringWriter w = new StringWriter();
			w.WriteLine("----------------------");           
			w.WriteLine(String.Format("{0}", DateTime.Now.ToString()));
			w.WriteLine(String.Format("\t{0}", sourceName.ToString()));
			w.WriteLine(String.Format("\t{0}", message.ToString()));

			Exception ex1 = ex;
			while (ex1 != null)
			{
				w.WriteLine("\t" + ex1.Message);
				ex1 = ex1.InnerException;
			}
			w.WriteLine();

			if (!File.Exists(m_LogFileName))
			{
				FileStream f1 = File.Create(m_LogFileName);
				if (f1 != null)
					f1.Close();
			}

			StreamWriter f = new StreamWriter(m_LogFileName, true);
			f.Write(w.ToString());
			f.Flush();
			f.Close();
		}
	}
}
