using System;
using System.IO;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using KWizCom.SharePoint.Utilities.SPListFindReplace.Common;

namespace KWizCom.SharePoint.Utilities.SPListFindReplace.Validation
{
	/// <summary>
	/// Summary description for SPSiteCheck.
	/// </summary>
	public class SPSiteCheck : SPPortalCheck
	{
		/// <summary>
		/// Public constructor
		/// </summary>
		/// <param name="componentIndex"></param>
		public SPSiteCheck(int componentIndex) : base(componentIndex)
		{			
		}

		/// <summary>
		/// Public copy constructor
		/// </summary>
		/// <param name="componentIndex"></param>
		public SPSiteCheck(string portalURL, int componentIndex) : base(portalURL, componentIndex)
		{				
		}

		/// <summary>
		/// Perfofrm check
		/// </summary>
		/// <param name="componentIndex"></param>
		public override void DoWork(int componentIndex)
		{
			try
			{
				CheckSite(m_SharePointPortalURL);				
			}
			catch(Exception ex)
			{
				OnError(ex, "SPSiteCheck.CheckSite");
			}			
		}

		/// <summary>
		/// Check SPS/STS site
		/// </summary>
		/// <param name="portalUrl"></param>
		public void CheckSite (string portalUrl)
		{
			using (SPGlobalAdmin gAdm = new SPGlobalAdmin())
			{
				SPVirtualServer vSrv = gAdm.OpenVirtualServer( new Uri(portalUrl) );
				if ( vSrv.Sites.Count >= 0 )
				{
					m_CurrentStatus = eCheckStatus.Passed;
					return;
				}

				m_CurrentStatus = eCheckStatus.Warning;	
			}
		}
	}
}
