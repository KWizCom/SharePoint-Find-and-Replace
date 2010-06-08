using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

using KWizCom.SharePoint.Utilities.SPListFindReplace.Common;

namespace KWizCom.SharePoint.Utilities.SPListFindReplace.Validation
{
	/// <summary>
	/// Summary description for SPPortalCheck.
	/// </summary>
	public class SPPortalCheck : ASynchComponentBase
	{
		protected string m_SharePointPortalURL = string.Empty;

		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="componentIndex"></param>
		public SPPortalCheck(int componentIndex) : base(componentIndex)
		{			
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="portalURL"></param>
		/// <param name="componentIndex"></param>
		public SPPortalCheck(string portalURL, int componentIndex):base(componentIndex)
		{
			m_SharePointPortalURL = portalURL;
		}

		/// <summary>
		/// Perform check
		/// </summary>
		/// <param name="componentIndex"></param>
		public override void DoWork(int componentIndex)
		{
			try
			{
				CheckPortal(m_SharePointPortalURL);				
			}
			catch(Exception ex)
			{
				OnError(ex, "SPPortalCheck.CheckPortal");
			}			
		}

		/// <summary>
		/// Get portal website
		/// </summary>
		/// <returns></returns>
		public SPWeb GetPortalSite (string portalUrl)
		{
			SPWeb PortalWeb = null;
			
			using (SPGlobalAdmin gAdm = new SPGlobalAdmin())
			{
				SPVirtualServer vSrv = gAdm.OpenVirtualServer( new Uri(portalUrl) );

				for(int i = 0; i < vSrv.Sites.Count; i++)
				{
					using (SPSite site = vSrv.Sites[i])
					{
						if( site.RootWeb.WebTemplate == "SPS" )
						{
							PortalWeb = site.RootWeb;
							break;
						}
					}
				}
			}

			return PortalWeb;
		}

		/// <summary>
		/// Return an specified site accoding to URL
		/// </summary>
		/// <param name="siteURL"></param>
		/// <returns></returns>
		public SPWeb GetSite(string portalURL, string siteURL)
		{
			SPWeb ResultSite = null;
			
			SPGlobalAdmin gAdm = new SPGlobalAdmin();
			SPVirtualServer vSrv = gAdm.OpenVirtualServer( new Uri(portalURL) );

			foreach(SPSite site in vSrv.Sites)
			{
				if( site.Url.ToLower() == siteURL.ToLower() )
				{
					ResultSite = site.RootWeb;
					break;
				}
			}

			return ResultSite;
		}

		/// <summary>
		/// Check existance of SharePoint Portal
		/// </summary>
		/// <param name="portalUrl"></param>
		/// <returns></returns>
		public void CheckPortal (string portalUrl)
		{
			using (SPWeb web = GetPortalSite(portalUrl))
			{
				if (  web!= null )
				{
					m_CurrentStatus = eCheckStatus.Passed;					
				}
				else
					m_CurrentStatus = eCheckStatus.Warning;
			}			
		}
	}
}
