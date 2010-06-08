using System;
using System.Xml;
using System.Web;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using System.Text.RegularExpressions;
using System.Reflection;
using System.IO;

using KWizCom.SharePoint.Utilities.SPListFindReplace.Common;

namespace KWizCom.SharePoint.Utilities.SPListFindReplace.FindReplace
{
	/// <summary>
	/// Summary description for FindReplaceComponent.
	/// </summary>
	public class FindReplaceComponent : ASynchComponentBase
	{
		private Queue m_SitesQueue = new Queue ();
		private FindReplaceParameters m_Parameters;
		private StringCollection m_RestrictedFieldList = new StringCollection();


		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="parameters"></param>
		public FindReplaceComponent(FindReplaceParameters parameters) : base(1)
		{
			m_Parameters = parameters;
			InitRestrictedFields ();			
		}

		/// <summary>
		/// Restricted list of fields
		/// </summary>
		private void InitRestrictedFields ()
		{
			m_RestrictedFieldList.Add("ID");
			m_RestrictedFieldList.Add("Modified");
			m_RestrictedFieldList.Add("Created");
			m_RestrictedFieldList.Add("Created By");
			m_RestrictedFieldList.Add("Modified By");
			m_RestrictedFieldList.Add("Type"); //icon linked to document
			m_RestrictedFieldList.Add("Checked Out To"); //link to username to user details page
			m_RestrictedFieldList.Add("File Size");
			m_RestrictedFieldList.Add("Owner");
			m_RestrictedFieldList.Add("Status");

			m_RestrictedFieldList.Add("ViewGuid");
			m_RestrictedFieldList.Add("ListViewXml");
		}

		/// <summary>
		/// Main entripoint asynchronous function
		/// </summary>
		/// <param name="componentIndex"></param>
		public override void DoWork(int componentIndex)
		{
			//FindReplace(m_Parameters.SharePointURL);
			ProcessSites();
		}

		public void ProcessSites ()
		{
			PrepareRootSite (m_Parameters.SharePointURL);

			while (m_SitesQueue.Count > 0 && !m_IsCanceled)
			{
				FindReplaceWebs(m_SitesQueue.Dequeue().ToString());
			}
		}

		/// <summary>
		/// Prepare process on root site
		/// </summary>
		/// <param name="sharePointSiteURL"></param>
		protected void PrepareRootSite (string sharePointSiteURL)
		{
			SPSite site = null;
			SPWeb web = null;

			try
			{
				site = new SPSite(sharePointSiteURL);
				web = site.OpenWeb();
				if ( web.WebTemplate == "SPS" )
				{
					if ( m_Parameters.IncludeSubSites )
					{		
						PreparePortalSites(sharePointSiteURL);
					}
					else
					{
						m_SitesQueue.Enqueue(web.Url.ToString());
					}
				}
				else
				{
					m_SitesQueue.Enqueue(web.Url.ToString());
				}				
			}
			catch(Exception ex)
			{
				OnError(ex, "PrepareRootSite");
			}
			finally
			{
				if ( web != null )
				{					
					web.Dispose();
					web = null;
				}
				if ( site != null )
				{
					site.Dispose ();
					site = null;
				}
			}
		}

		/// <summary>
		/// Prepare child sites of sharepoint portal
		/// </summary>
		/// <param name="sharePointSiteURL"></param>
		protected void PreparePortalSites(string sharePointSiteURL)
		{
			using(SPGlobalAdmin gAdm = new SPGlobalAdmin())
			{				
				SPVirtualServer vSrv = gAdm.OpenVirtualServer( new Uri(sharePointSiteURL) );				
				for(int i = 0; i < vSrv.Sites.Count; i++)
				{
					using(SPSite Site = vSrv.Sites[i])
					{
						using(SPWeb web = Site.OpenWeb())
						{
							m_SitesQueue.Enqueue(web.Url.ToString());	
						}
					}
				}
				
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
		}

		/// <summary>
		/// Run find&replace process on root of the specified site (or site collection)
		/// </summary>
		/// <param name="sharePointSiteURL"></param>
		public void FindReplace(string sharePointSiteURL)
		{
			SPSite site = null;
			SPWeb web = null;

			m_IsCanceled = false;
			m_IsCanceling = false;

			try
			{	
				site = new SPSite(sharePointSiteURL);
				web = site.OpenWeb();	
				web.AllowUnsafeUpdates = true;

				if ( web.WebTemplate == "SPS" )
				{
					if ( m_Parameters.IncludeSubSites )
					{
						SPGlobalAdmin gAdm = new SPGlobalAdmin();
						SPVirtualServer vSrv = gAdm.OpenVirtualServer( new Uri(sharePointSiteURL) );
						foreach(SPSite Site in vSrv.Sites)
						{
							web = Site.OpenWeb();
							FindReplaceWebs(web);						
						}
					}
					else
					{
						FindReplaceWebs(web);
					}
				}
				else
				{
					FindReplaceWebs(web);
				}
			}
			catch(Exception ex)
			{
				OnError(ex, "FindReplaceComponent.FindReplace");
			}	
			finally
			{
				if ( web != null )
				{
					web.Close ();
					web.Dispose();
					web = null;
				}
			}
		}

		/// <summary>
		/// Start process on site webpart properties and xml files
		/// </summary>
		/// <param name="web"></param>
		protected void FindReplaceWebPartsAndFiles (SPWeb web)
		{
			if ( web != null )
			{
				SPFolder Folder = web.RootFolder;
				Crawl(Folder);				
			}
		}

		/// <summary>
		/// Read, find, replace and save content of XML file within a some document library.
		/// </summary>
		/// <param name="file"></param>
		protected void ReadXmlFile(SPFile file)
		{
			if ( file != null )
			{
				try
				{
					OnTrace("Start searching file " + file.Name ,"ReadXmlFile" );

					Stream myStream = new MemoryStream(file.OpenBinary());
				
					TextReader tr = new StreamReader(myStream);
					string Text = tr.ReadToEnd ();

//					XmlDocument myDoc = new XmlDocument();
//					myDoc.Load(myStream);

					if ( IsStringFound(Text) )
					{
						StringBuilder myString = new StringBuilder();
						myString.Append(GetReplacedString(Text));

						byte[] bytes = System.Text.Encoding.UTF8.GetBytes(myString.ToString ());
						file.SaveBinary(bytes);
						
						OnTrace("*** File " + file.Name  + " is succesfully replaced!","ReadXmlFile" );
					}

					tr.Close ();
					myStream.Close ();
				}
				catch(Exception ex)
				{
					string message = ex.Message;
				}
			}
		}

		/// <summary>
		/// Reflection crawl process of webpart properties and xml files
		/// </summary>
		/// <param name="folder"></param>
		protected void Crawl(SPFolder folder)
		{
			SPFile MyFile = null;

			if ( folder != null )
			{
				foreach(SPFile CurrentFile in folder.Files)
				{
					MyFile = CurrentFile;

					try
					{
						string FileExtension = Path.GetExtension(CurrentFile.Url);
					
						if ( CurrentFile.InDocumentLibrary
							&& CurrentFile.Item != null
							&& m_Parameters.IncludeXmlFilesLibraries 
							&& XmlFileLibraryInFilteredList (CurrentFile.Item.ParentList as SPDocumentLibrary)
							&& XmlFileLibraryInFilteredList(CurrentFile))
						{
							ReadXmlFile(CurrentFile);
							continue;
						}
						else if ( FileExtension.ToLower() == ".aspx"  && m_Parameters.IncludeWebParts)
						{
							CrawlWebPartPage(CurrentFile);							
						}
					}
					catch(Exception ex)
					{
						OnError(ex, "ERROR: Cannot process file " + MyFile.Name);
					}
				}

				foreach(SPFolder SubFolder in folder.SubFolders)
				{
					Crawl(SubFolder);
				}								
			}
		}

		/// <summary>
		/// Check all webpart in the specified webpartpage
		/// </summary>
		/// <param name="CurrentFile"></param>
		protected void CrawlWebPartPage(SPFile CurrentFile)
		{			
			using (SPWeb web = CurrentFile.ParentFolder.ParentWeb)
			{
				SPWebPartCollection parts = web.GetWebPartCollection(CurrentFile.Url , Storage.Shared);
				if ( parts != null )
				{

				//OnTrace("Start search " + CurrentFile.Url.ToString() + " webpart's page...", "");

					for (int i = 0; i < parts.Count; i++)
					{		
						using (WebPart wptWebPart = parts[i] )
						{							
							if ( WebPartInWebPartList(wptWebPart) )
							{
								string[] webPartNames = wptWebPart.ToString().Split('.');
								string WebPartSmallName = webPartNames[webPartNames.Length-1].ToString();

								OnTrace("Start search webpart " + WebPartSmallName, "");

								try
								{
									PropertyInfo[] pinProperties = wptWebPart.GetType().GetProperties(
										BindingFlags.Public | BindingFlags.Instance);
							
									try
									{	
										foreach (PropertyInfo p in pinProperties)
										{												
											if ( FindReplaceWebPartProperty(wptWebPart, p) )
												parts.SaveChanges(wptWebPart.StorageKey);
										}
									}	
									catch(Exception ex)
									{
										string sre = ex.Message;
									}
								}
								catch(Exception ex)
								{
									string s = ex.Message;
								}

								OnTrace("Finish search webpart " + WebPartSmallName, "");
							}
							GC.Collect();
							GC.WaitForPendingFinalizers();
						}
					}
				}

				GC.Collect();
				GC.WaitForPendingFinalizers();
			}

			//OnTrace("Finish search " + CurrentFile.Url.ToString() + " webpart's page...", "");
		}

		/// <summary>
		/// Run find & replace process on WebPart property
		/// </summary>
		/// <param name="webPart"></param>
		/// <param name="propertyInfo"></param>
		/// <returns></returns>
		protected bool FindReplaceWebPartProperty(WebPart webPart, PropertyInfo propertyInfo)
		{
			bool Result = false;

			if ( webPart == null ) 
				throw new ArgumentNullException("webPart");

			if ( propertyInfo == null ) 
				throw new ArgumentNullException("propertyInfo");

			PropertyInfo pinProperty = propertyInfo;
			try
			{
				if ( IsInRestrictedFields(propertyInfo) ) return false;
				
				if ( WebPartPropertyInFilteredList(propertyInfo))
				{									
					object CurrentValue = propertyInfo.GetValue(webPart, null);
					CurrentValue = ReplacePropertyValue(CurrentValue, propertyInfo.PropertyType.ToString());
					if ( CurrentValue != null )
					{
						propertyInfo.SetValue(webPart, CurrentValue, null);	
						Result = true;

						OnTrace("*** Property " + propertyInfo.Name + " succesfully replaced!", "FindReplaceWebPartProperty");
					}	
				}		
			}
			catch(Exception ex)
			{
				OnError(ex, String.Format("ERROR: process property {0} of webpart {1} failed!", propertyInfo.Name, webPart.ToString()));
			}

			return Result;
		}

		protected object ReplacePropertyValue(object valueToReplace, string reflactionType)
		{
			object Result = null;
			if ( valueToReplace != null )
			{
				switch(reflactionType)
				{
					case "System.String": Result = ReplacePropertyValue(valueToReplace.ToString ()); break;
					case "System.Xml.XmlElement" : Result = ReplacePropertyValue(valueToReplace as System.Xml.XmlElement); break;
				}
			}

			return Result;
		}

		protected object ReplacePropertyValue(string valueToReplace)
		{
			object Result = null;

			if ( valueToReplace == null ) return Result;

			if ( IsStringFound(valueToReplace) )
			{
				Result = GetReplacedString(valueToReplace);
			}

			return Result;
		}

		protected object ReplacePropertyValue(System.Xml.XmlElement valueToReplace)
		{
			object Result = null;

			if ( valueToReplace == null ) return Result;

			if ( IsStringFound(valueToReplace.InnerText.ToString()) )
			{				
				XmlDocument xmlDoc = new XmlDocument();
				
				XmlElement xmlElement = xmlDoc.CreateElement(valueToReplace.Name);
				xmlElement.InnerXml = GetReplacedString(valueToReplace.InnerXml);

				Result = xmlElement;
			}

			return Result;
		}

		/// <summary>
		/// Process find&replace operation on SPWeb Lists
		/// </summary>
		/// <param name="web"></param>
		protected void FindReplaceLists(SPWeb web)
		{
			if ( web != null )
			{
				SPListCollection Lists = web.Lists;
				foreach(SPList List in Lists)
				{
					if ( m_IsCanceling )
					{
						m_IsCanceled = true;
						break;
					}				

					if ( ListInFilteredList(List) )
					{	
						OnTrace(@"Start search list " + List.Title, "");

						foreach(SPListItem Item in List.Items)
						{
							FindReplaceFields(Item);
						}

						OnTrace(@"Finish search list " + List.Title, "");
					}					
				}
			}
		}

		/// <summary>
		/// Checks is searched string exists within destination
		/// </summary>
		/// <param name="stringToCheck"></param>
		/// <returns></returns>
		protected bool IsStringFound(string stringToCheck)
		{
			bool Result = false;

			if(stringToCheck.ToLower().IndexOf(m_Parameters.FindClause.ToLower()) > -1)
			{
				Result = true;
			}

			return Result;
		}

		/// <summary>
		/// Replace process
		/// </summary>
		/// <param name="stringToReplace"></param>
		/// <returns></returns>
		protected string GetReplacedString(string stringToReplace)
		{
			return Regex.Replace (stringToReplace.ToString(), m_Parameters.FindClause, m_Parameters.ReplaceClause, RegexOptions.IgnoreCase);
		}

		/// <summary>
		/// Process find&Replace operation on SDPList fields
		/// </summary>
		/// <param name="list"></param>
		protected void FindReplaceFields(SPListItem listItem)
		{
			if ( listItem != null )
			{
				
					SPFieldCollection Fields = listItem.Fields;
					foreach(SPField Field in Fields)
					{
						if ( m_IsCanceling )
						{
							m_IsCanceled = true;
							break;
						}

						if ( IsInRestrictedFields(Field) ) continue;

						if ( FieldInFieldsList(Field) )
						{
							string OldValue = String.Empty;
							try
							{
								OldValue = listItem[Field.Title].ToString ();
							}
							catch {}

							try
							{
								if ( IsStringFound(listItem[Field.Title].ToString()) )
								{
									listItem[Field.Title] = GetReplacedString(listItem[Field.Title].ToString());
									listItem.Update();
								
									OnTrace("*** Field " + Field.Title + " is succesfully replaced!", "");
								}

							}
							catch(System.NullReferenceException ex)
							{
								string s = ex.Message;
							}
							catch(Exception ex)
							{
								OnError(ex, "FindReplaceComponent.FindReplaceFields");
							}
						}						
					}
				
			}
		}

		/// <summary>
		/// Run find & replace
		/// </summary>
		/// <param name="siteURL"></param>
		protected void FindReplaceWebs(string siteURL)
		{
			SPWeb web = null;

			if ( m_IsCanceling )
			{
				m_IsCanceled = true;
				return;
			}

			try
			{
				using(SPSite site = new SPSite(siteURL))
				{
					using (web = site.OpenWeb() )
					{
						web.AllowUnsafeUpdates = true;

						OnTrace(@"*** Site:  " + web.Url + "  ***", "");

						if ( m_Parameters.IncludeLists )
							FindReplaceLists (web);

						if ( m_Parameters.IncludeWebParts || m_Parameters.IncludeXmlFilesLibraries )
						{
							FindReplaceWebPartsAndFiles(web);
						}

						if ( m_Parameters.IncludeSubSites )
						{
							SPWebCollection SubWebs = web.Webs;
							for(int i =0; i < SubWebs.Count; i++)
							{
								if ( m_IsCanceling )
								{
									m_IsCanceled = true;
									break;
								}				

								using ( SPWeb ChildWeb = SubWebs[i] )
								{
									m_SitesQueue.Enqueue(ChildWeb.Url.ToString());
									GC.Collect();
									GC.WaitForPendingFinalizers();

								}
							}							
						}
						GC.Collect();
						GC.WaitForPendingFinalizers();
					}
					
					GC.Collect();
					GC.WaitForPendingFinalizers();
				}
			}
			catch(Exception ex)
			{
				OnError(ex, "FindReplaceWebs");
			}
			finally
			{
				if ( web != null ) web.Dispose ();					
			}
		}

		/// <summary>
		/// Run find & replace on the specified web
		/// </summary>
		/// <param name="web"></param>
		protected void FindReplaceWebs(SPWeb web)
		{
			if ( m_IsCanceling )
			{
				m_IsCanceled = true;
				return;
			}

			OnTrace(@"*** Site:  " + web.Url + "  ***", "");

			if ( m_Parameters.IncludeLists )
				FindReplaceLists (web);

			if ( m_Parameters.IncludeWebParts || m_Parameters.IncludeXmlFilesLibraries )
			{
				FindReplaceWebPartsAndFiles(web);
			}

			SPWebCollection SubWebs = web.Webs;
			foreach(SPWeb ChildWeb in SubWebs)
			{
				if ( m_IsCanceling )
				{
					m_IsCanceled = true;
					break;
				}				

				if ( m_Parameters.IncludeSubSites )
				{
					FindReplaceWebs(ChildWeb);
				}
			}
		}

		/// <summary>
		/// Check the specified xml file in specified files list
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		protected bool XmlFileLibraryInFilteredList (SPFile file)
		{
			bool Result = false;			
			string Extension = "*" + Path.GetExtension(file.Url);

			if ( file != null )
			{
				if ( !m_Parameters.IsXmlDocumentsFiltered ||
					m_Parameters.XmlFilesDocumentsMetaList.Contains(file.Name.ToUpper()) ||
					m_Parameters.XmlFilesDocumentsMetaList.Contains(Extension.ToUpper()))
				{
					Result = true;
				}
			}

			return Result;
		}

		/// <summary>
		/// Check the specified xml file libraries on restricted list
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		protected bool XmlFileLibraryInFilteredList (SPDocumentLibrary fileLibrary)
		{
			bool Result = false;
			
			if ( fileLibrary != null )
			{
				if ( !m_Parameters.IsXmlFilesFiltered ||
					m_Parameters.XmlFilesLibrariesMetaList.Contains(fileLibrary.Title.ToUpper()))
				{
					Result = true;
				}
			}

			return Result;
		}

		/// <summary>
		/// Check the specified webpart porperty on restricted list
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		protected bool WebPartPropertyInFilteredList (PropertyInfo propertyInfo)
		{
			bool Result = false;
			
			if ( !m_Parameters.IsWebPartsFiltered ||
				m_Parameters.WebPartMetaList.Contains(propertyInfo.Name.ToUpper()))
			{
				Result = true;
			}
 

			return Result;
		}

		/// <summary>
		/// Check the specified webpart porperty on restricted list
		/// This check include webpart name, type and full type
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		protected bool WebPartInWebPartList (WebPart webPart)
		{
			bool Result = false;
			string[] webPartName = webPart.ToString().Split('.');
			if ( !m_Parameters.IsWebPartsNamesFiltered ||
				m_Parameters.WebPartNamesList.Contains(webPart.Title.ToUpper()) ||
				m_Parameters.WebPartNamesList.Contains(webPartName[webPartName.Length-1].ToString().ToUpper()) ||
				m_Parameters.WebPartNamesList.Contains(webPart.ToString().ToUpper()))
			{
				Result = true;
			}
 

			return Result;
		}

		/// <summary>
		/// Check the specified list on restricted list
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		protected bool ListInFilteredList (SPList list)
		{
			bool Result = false;
			
			if ( !m_Parameters.IsListsFiltered ||
				m_Parameters.ListList.Contains(list.Title.ToUpper()))
			{
				Result = true;
			}
 

			return Result;
		}

		/// <summary>
		/// Check the specified field on restricted list
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		protected bool FieldInFieldsList(SPField field)
		{
			bool Result = false;
			
			if ( !m_Parameters.IsFieldsFiltered ||
				m_Parameters.FieldList.Contains(field.Title.ToUpper()))
			{
				Result = true;
			}
 

			return Result;
		}

		/// <summary>
		/// Check list's field for restriction policy
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		protected bool IsInRestrictedFields(SPField field)
		{
			bool Result = false;

			if ( m_RestrictedFieldList.Contains(field.Title) || 
				field.ReadOnlyField ||
				field.Type == SPFieldType.Invalid)
				Result = true;

			return Result;
		}

		/// <summary>
		/// Check dinamic webpart property on restriction policy
		/// </summary>
		/// <param name="fieldInfo"></param>
		/// <returns></returns>
		protected bool IsInRestrictedFields(PropertyInfo fieldInfo)
		{
			bool Result = false;

			if ( m_RestrictedFieldList.Contains(fieldInfo.Name) ||
				fieldInfo.CanWrite == false)
			{
				return true;				
			}

			string PropType = fieldInfo.PropertyType.ToString();
			switch ( PropType )
			{
				case "System.String":
				case "System.Xml.XmlElement":
					Result = false;
					break;
				default: Result = true; break;
			}

			return Result;
		}
	}
}
