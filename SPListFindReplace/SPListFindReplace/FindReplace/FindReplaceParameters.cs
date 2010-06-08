using System;
using System.Collections.Specialized;

#if KWIZCOM
namespace KWizCom.SharePoint.Utilities.SPListFindReplace.FindReplace
#else
namespace Amdocs.SharePoint.Tools.SPListFindReplace.FindReplace
#endif
{
	/// <summary>
	/// Summary description for FindReplaceParameters.
	/// </summary>
	public class FindReplaceParameters
	{
		protected string m_SharePointURL = String.Empty;
		protected bool m_IncludeSubSites = false;
		protected bool m_IncludeWebParts = false;
		protected bool m_IncludeXmlFiles = false;
		protected bool m_IncludeLists = false;

		protected string m_FindClause = String.Empty;
		protected string m_ReplaceClause =String.Empty;

		protected StringCollection m_ListList = new StringCollection();
		protected StringCollection m_FieldList = new StringCollection();
		
		protected StringCollection m_WebPartNamesList = new StringCollection();
		protected StringCollection m_WebPartMetaList = new StringCollection();

		protected StringCollection m_XmlFilesLibrariesMetaList = new StringCollection();
		protected StringCollection m_XmlFilesDocumentsMetaList = new StringCollection();
	
		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="parameters"></param>
		public FindReplaceParameters()
		{			
		}

		#region PUBLIC PROPERTIES

		public string SharePointURL
		{
			get { return m_SharePointURL; }
			set { m_SharePointURL = value; }
		}

		public bool IncludeSubSites
		{
			get{ return m_IncludeSubSites; }
			set { m_IncludeSubSites = value; }
		}

		public bool IncludeLists
		{
			get{ return m_IncludeLists; }
			set { m_IncludeLists = value; }
		}

		public bool IncludeWebParts
		{
			get{ return m_IncludeWebParts; }
			set { m_IncludeWebParts = value; }
		}

		public bool IncludeXmlFilesLibraries
		{
			get{ return m_IncludeXmlFiles; }
			set { m_IncludeXmlFiles = value; }
		}

		public string FindClause
		{
			get { return m_FindClause; }
			set { m_FindClause = value; }
		}

		public string ReplaceClause
		{
			get { return m_ReplaceClause; }
			set { m_ReplaceClause = value; }
		}

		public StringCollection ListList
		{
			get { return m_ListList; }
			set { m_ListList = value; }
		}

		public StringCollection FieldList
		{
			get { return m_FieldList; }
			set { m_FieldList = value; }
		}

		public StringCollection WebPartMetaList
		{
			get { return m_WebPartMetaList; }
			set { m_WebPartMetaList = value; }
		}

		public StringCollection WebPartNamesList
		{
			get { return m_WebPartNamesList; }
			set { m_WebPartNamesList = value; }
		}

		public StringCollection XmlFilesLibrariesMetaList
		{
			get { return m_XmlFilesLibrariesMetaList; }
			set { m_XmlFilesLibrariesMetaList = value; }
		}

		public StringCollection XmlFilesDocumentsMetaList
		{
			get { return m_XmlFilesDocumentsMetaList; }
			set { m_XmlFilesDocumentsMetaList = value; }
		}

		public bool IsListsFiltered
		{
			get { return (m_ListList.Count > 0 ); }
		}

		public bool IsFieldsFiltered
		{
			get { return (m_FieldList.Count > 0); }
		}

		public bool IsWebPartsFiltered
		{
			get { return (m_WebPartMetaList.Count > 0); }
		}

		public bool IsWebPartsNamesFiltered
		{
			get { return (m_WebPartNamesList.Count > 0); }
		}

		/// <summary>
		/// XMl/Text file libraries
		/// </summary>
		public bool IsXmlFilesFiltered
		{
			get { return (m_XmlFilesLibrariesMetaList.Count > 0); }
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsXmlDocumentsFiltered
		{
			get { return (m_XmlFilesDocumentsMetaList.Count > 0); }
		}

		#endregion
	}
}
