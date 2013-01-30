using System;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace KWizCom.SharePoint.Utilities.SPListFindReplace.Controls
{
	/// <summary>
	/// Summary description for OutputWindowControl.
	/// </summary>
	public class OutputWindowControl : RichTextBox
	{
		public OutputWindowControl()
		{			
		}

		public void OutputText(string text)
		{
            if (this.InvokeRequired)//thread safe ui update
                this.Invoke(new MethodInvoker(()=>OutputText(text)));
            else
    			SelectedText = text + "\r\n";
		}
	}
}
