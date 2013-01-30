using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using KWizCom.SharePoint.Utilities.SPListFindReplace.Controls;
using KWizCom.SharePoint.Utilities.SPListFindReplace.Common;
using KWizCom.SharePoint.Utilities.SPListFindReplace.FindReplace;
using KWizCom.SharePoint.Utilities.SPListFindReplace.Validation;

namespace KWizCom.SharePoint.Utilities.SPListFindReplace
{
	/// <summary>
	/// Summary description for FindAndReplace.
	/// </summary>
	public class FindAndReplace : System.Windows.Forms.Form
	{	
		private Logger m_Logger;
		private FindReplaceParameters m_Parameters;
		private FindReplaceComponent m_Component;
		private Validation.SPSiteCheck m_PortalCheck;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxLogFile;
		private System.Windows.Forms.Label label5;
		private ListSiteDefinition listSiteDefinitionLists;
		private ListSiteDefinition listSiteDefinitionFields;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.GroupBox groupBox4;
		private OutputWindowControl richTextBox1;
		private System.Windows.Forms.Button buttonOpenFolder;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TextBox textBoxReplace;
		private System.Windows.Forms.TextBox textBoxFind;
		private PromptedTextBox textBoxSiteURL;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.LinkLabel linkKwizCom;
		private System.Windows.Forms.CheckBox checkBoxSubSites;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxSearchWebParts;
		private ListSiteDefinition listSiteDefinitionWebParts;
		private System.Windows.Forms.CheckBox checkBoxXmlSearch;
		private System.Windows.Forms.Panel panelWebPartCheck;
		private System.Windows.Forms.GroupBox groupBoxList;
		private System.Windows.Forms.CheckBox checkBoxListProperties;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBoxFindReplace;
		private System.Windows.Forms.GroupBox groupBoxSite;
		private System.Windows.Forms.GroupBox groupBoxLogFile;
		private System.Windows.Forms.GroupBox groupBoxTextFiles;
		private System.Windows.Forms.GroupBox groupBoxWebParts;
		private System.Windows.Forms.Panel panelListCheck;
		private ListSiteDefinition listSiteDefinitionXmlLibraries;
		private ListSiteDefinition listSiteDefinitionXMLDocuments;
		private ListSiteDefinition listSiteDefinitionWebPartNames;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FindAndReplace()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_Parameters = new FindReplaceParameters();

			m_Component = new FindReplaceComponent(m_Parameters);
			m_Component.Error += new ComponentErrorEventHandler(m_Component_Error);
			m_Component.Trace += new ComponentErrorEventHandler(m_Component_Trace);
			m_Component.Complete += new ComponentEventHandler(m_Component_Complete);
			m_Component.Abort += new ComponentEventHandler(m_Component_Abort);

			m_Logger = new Logger(Application.StartupPath);			
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindAndReplace));
            this.groupBoxFindReplace = new System.Windows.Forms.GroupBox();
            this.textBoxReplace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxSite = new System.Windows.Forms.GroupBox();
            this.checkBoxSubSites = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxSearchWebParts = new System.Windows.Forms.CheckBox();
            this.checkBoxXmlSearch = new System.Windows.Forms.CheckBox();
            this.groupBoxLogFile = new System.Windows.Forms.GroupBox();
            this.buttonOpenFolder = new System.Windows.Forms.Button();
            this.textBoxLogFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.linkKwizCom = new System.Windows.Forms.LinkLabel();
            this.groupBoxTextFiles = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelWebPartCheck = new System.Windows.Forms.Panel();
            this.groupBoxList = new System.Windows.Forms.GroupBox();
            this.panelListCheck = new System.Windows.Forms.Panel();
            this.checkBoxListProperties = new System.Windows.Forms.CheckBox();
            this.groupBoxWebParts = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listSiteDefinitionWebPartNames = new KWizCom.SharePoint.Utilities.SPListFindReplace.Controls.ListSiteDefinition();
            this.listSiteDefinitionWebParts = new KWizCom.SharePoint.Utilities.SPListFindReplace.Controls.ListSiteDefinition();
            this.listSiteDefinitionXMLDocuments = new KWizCom.SharePoint.Utilities.SPListFindReplace.Controls.ListSiteDefinition();
            this.listSiteDefinitionXmlLibraries = new KWizCom.SharePoint.Utilities.SPListFindReplace.Controls.ListSiteDefinition();
            this.richTextBox1 = new KWizCom.SharePoint.Utilities.SPListFindReplace.Controls.OutputWindowControl();
            this.textBoxSiteURL = new KWizCom.SharePoint.Utilities.SPListFindReplace.Controls.PromptedTextBox();
            this.listSiteDefinitionLists = new KWizCom.SharePoint.Utilities.SPListFindReplace.Controls.ListSiteDefinition();
            this.listSiteDefinitionFields = new KWizCom.SharePoint.Utilities.SPListFindReplace.Controls.ListSiteDefinition();
            this.groupBoxFindReplace.SuspendLayout();
            this.groupBoxSite.SuspendLayout();
            this.groupBoxLogFile.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxTextFiles.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelWebPartCheck.SuspendLayout();
            this.groupBoxList.SuspendLayout();
            this.panelListCheck.SuspendLayout();
            this.groupBoxWebParts.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxFindReplace
            // 
            this.groupBoxFindReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFindReplace.Controls.Add(this.textBoxReplace);
            this.groupBoxFindReplace.Controls.Add(this.label2);
            this.groupBoxFindReplace.Controls.Add(this.textBoxFind);
            this.groupBoxFindReplace.Controls.Add(this.label1);
            this.groupBoxFindReplace.Location = new System.Drawing.Point(4, 27);
            this.groupBoxFindReplace.Name = "groupBoxFindReplace";
            this.groupBoxFindReplace.Size = new System.Drawing.Size(456, 56);
            this.groupBoxFindReplace.TabIndex = 0;
            this.groupBoxFindReplace.TabStop = false;
            this.groupBoxFindReplace.Text = "String Definition";
            // 
            // textBoxReplace
            // 
            this.textBoxReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReplace.Location = new System.Drawing.Point(288, 22);
            this.textBoxReplace.Name = "textBoxReplace";
            this.textBoxReplace.Size = new System.Drawing.Size(160, 21);
            this.textBoxReplace.TabIndex = 3;
            this.textBoxReplace.TextChanged += new System.EventHandler(this.textBoxReplace_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(234, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Replace:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxFind
            // 
            this.textBoxFind.Location = new System.Drawing.Point(64, 22);
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.Size = new System.Drawing.Size(160, 21);
            this.textBoxFind.TabIndex = 1;
            this.textBoxFind.TextChanged += new System.EventHandler(this.textBoxFind_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxSite
            // 
            this.groupBoxSite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSite.Controls.Add(this.checkBoxSubSites);
            this.groupBoxSite.Controls.Add(this.textBoxSiteURL);
            this.groupBoxSite.Controls.Add(this.label3);
            this.groupBoxSite.Location = new System.Drawing.Point(4, 89);
            this.groupBoxSite.Name = "groupBoxSite";
            this.groupBoxSite.Size = new System.Drawing.Size(456, 72);
            this.groupBoxSite.TabIndex = 1;
            this.groupBoxSite.TabStop = false;
            this.groupBoxSite.Text = "SharePoint Sites";
            this.groupBoxSite.Enter += new System.EventHandler(this.groupBoxSite_Enter);
            // 
            // checkBoxSubSites
            // 
            this.checkBoxSubSites.Checked = true;
            this.checkBoxSubSites.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSubSites.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxSubSites.Location = new System.Drawing.Point(17, 46);
            this.checkBoxSubSites.Name = "checkBoxSubSites";
            this.checkBoxSubSites.Size = new System.Drawing.Size(128, 22);
            this.checkBoxSubSites.TabIndex = 6;
            this.checkBoxSubSites.Text = "Include all sub-sites";
            this.checkBoxSubSites.CheckedChanged += new System.EventHandler(this.checkBoxSubSites_CheckedChanged);
            // 
            // label3
            // 
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(8, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Site URL:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxSearchWebParts
            // 
            this.checkBoxSearchWebParts.Checked = true;
            this.checkBoxSearchWebParts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSearchWebParts.Location = new System.Drawing.Point(8, -2);
            this.checkBoxSearchWebParts.Name = "checkBoxSearchWebParts";
            this.checkBoxSearchWebParts.Size = new System.Drawing.Size(200, 24);
            this.checkBoxSearchWebParts.TabIndex = 9;
            this.checkBoxSearchWebParts.Text = "Search web parts";
            this.checkBoxSearchWebParts.CheckedChanged += new System.EventHandler(this.checkBoxSearchWebParts_CheckedChanged);
            // 
            // checkBoxXmlSearch
            // 
            this.checkBoxXmlSearch.Checked = true;
            this.checkBoxXmlSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxXmlSearch.Location = new System.Drawing.Point(8, -4);
            this.checkBoxXmlSearch.Name = "checkBoxXmlSearch";
            this.checkBoxXmlSearch.Size = new System.Drawing.Size(224, 22);
            this.checkBoxXmlSearch.TabIndex = 11;
            this.checkBoxXmlSearch.Text = "Search text documents";
            this.checkBoxXmlSearch.CheckedChanged += new System.EventHandler(this.checkBoxXmlSearch_CheckedChanged);
            // 
            // groupBoxLogFile
            // 
            this.groupBoxLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLogFile.Controls.Add(this.buttonOpenFolder);
            this.groupBoxLogFile.Controls.Add(this.textBoxLogFile);
            this.groupBoxLogFile.Controls.Add(this.label5);
            this.groupBoxLogFile.Location = new System.Drawing.Point(4, 414);
            this.groupBoxLogFile.Name = "groupBoxLogFile";
            this.groupBoxLogFile.Size = new System.Drawing.Size(456, 56);
            this.groupBoxLogFile.TabIndex = 4;
            this.groupBoxLogFile.TabStop = false;
            this.groupBoxLogFile.Text = "Log";
            // 
            // buttonOpenFolder
            // 
            this.buttonOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenFolder.Location = new System.Drawing.Point(426, 22);
            this.buttonOpenFolder.Name = "buttonOpenFolder";
            this.buttonOpenFolder.Size = new System.Drawing.Size(22, 22);
            this.buttonOpenFolder.TabIndex = 2;
            this.buttonOpenFolder.Text = "...";
            this.buttonOpenFolder.Click += new System.EventHandler(this.buttonOpenFolder_Click);
            // 
            // textBoxLogFile
            // 
            this.textBoxLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogFile.Location = new System.Drawing.Point(64, 22);
            this.textBoxLogFile.Name = "textBoxLogFile";
            this.textBoxLogFile.Size = new System.Drawing.Size(360, 21);
            this.textBoxLogFile.TabIndex = 1;
            this.textBoxLogFile.Text = "textBoxFind";
            // 
            // label5
            // 
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(8, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Log File:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.Location = new System.Drawing.Point(385, 476);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 22);
            this.buttonExit.TabIndex = 5;
            this.buttonExit.Text = "Exit";
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.Location = new System.Drawing.Point(4, 476);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(128, 22);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Start Find && Replace";
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(140, 476);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 22);
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "Stop";
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Controls.Add(this.richTextBox1);
            this.groupBox4.Location = new System.Drawing.Point(4, 504);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(456, 105);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Real-Time Status";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(392, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // linkKwizCom
            // 
            this.linkKwizCom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkKwizCom.Location = new System.Drawing.Point(360, 612);
            this.linkKwizCom.Name = "linkKwizCom";
            this.linkKwizCom.Size = new System.Drawing.Size(100, 23);
            this.linkKwizCom.TabIndex = 9;
            this.linkKwizCom.TabStop = true;
            this.linkKwizCom.Text = "www.kwizcom.com";
            // 
            // groupBoxTextFiles
            // 
            this.groupBoxTextFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTextFiles.Controls.Add(this.listSiteDefinitionXMLDocuments);
            this.groupBoxTextFiles.Controls.Add(this.panel2);
            this.groupBoxTextFiles.Controls.Add(this.listSiteDefinitionXmlLibraries);
            this.groupBoxTextFiles.Location = new System.Drawing.Point(4, 336);
            this.groupBoxTextFiles.Name = "groupBoxTextFiles";
            this.groupBoxTextFiles.Size = new System.Drawing.Size(456, 72);
            this.groupBoxTextFiles.TabIndex = 10;
            this.groupBoxTextFiles.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBoxXmlSearch);
            this.panel2.Location = new System.Drawing.Point(9, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(143, 22);
            this.panel2.TabIndex = 0;
            // 
            // panelWebPartCheck
            // 
            this.panelWebPartCheck.Controls.Add(this.checkBoxSearchWebParts);
            this.panelWebPartCheck.Location = new System.Drawing.Point(9, -3);
            this.panelWebPartCheck.Name = "panelWebPartCheck";
            this.panelWebPartCheck.Size = new System.Drawing.Size(119, 22);
            this.panelWebPartCheck.TabIndex = 0;
            // 
            // groupBoxList
            // 
            this.groupBoxList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxList.Controls.Add(this.panelListCheck);
            this.groupBoxList.Controls.Add(this.listSiteDefinitionLists);
            this.groupBoxList.Controls.Add(this.listSiteDefinitionFields);
            this.groupBoxList.Location = new System.Drawing.Point(4, 171);
            this.groupBoxList.Name = "groupBoxList";
            this.groupBoxList.Size = new System.Drawing.Size(456, 72);
            this.groupBoxList.TabIndex = 7;
            this.groupBoxList.TabStop = false;
            // 
            // panelListCheck
            // 
            this.panelListCheck.Controls.Add(this.checkBoxListProperties);
            this.panelListCheck.Location = new System.Drawing.Point(9, -3);
            this.panelListCheck.Name = "panelListCheck";
            this.panelListCheck.Size = new System.Drawing.Size(135, 22);
            this.panelListCheck.TabIndex = 9;
            // 
            // checkBoxListProperties
            // 
            this.checkBoxListProperties.Checked = true;
            this.checkBoxListProperties.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxListProperties.Location = new System.Drawing.Point(8, -1);
            this.checkBoxListProperties.Name = "checkBoxListProperties";
            this.checkBoxListProperties.Size = new System.Drawing.Size(176, 24);
            this.checkBoxListProperties.TabIndex = 0;
            this.checkBoxListProperties.Text = "Search list properties";
            this.checkBoxListProperties.CheckedChanged += new System.EventHandler(this.checkBoxListProperties_CheckedChanged);
            // 
            // groupBoxWebParts
            // 
            this.groupBoxWebParts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxWebParts.Controls.Add(this.listSiteDefinitionWebPartNames);
            this.groupBoxWebParts.Controls.Add(this.panelWebPartCheck);
            this.groupBoxWebParts.Controls.Add(this.listSiteDefinitionWebParts);
            this.groupBoxWebParts.Location = new System.Drawing.Point(4, 254);
            this.groupBoxWebParts.Name = "groupBoxWebParts";
            this.groupBoxWebParts.Size = new System.Drawing.Size(456, 72);
            this.groupBoxWebParts.TabIndex = 11;
            this.groupBoxWebParts.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(472, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // listSiteDefinitionWebPartNames
            // 
            this.listSiteDefinitionWebPartNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSiteDefinitionWebPartNames.Caption = "Web Parts:";
            this.listSiteDefinitionWebPartNames.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listSiteDefinitionWebPartNames.Location = new System.Drawing.Point(8, 18);
            this.listSiteDefinitionWebPartNames.Name = "listSiteDefinitionWebPartNames";
            this.listSiteDefinitionWebPartNames.PromptText = "[Web Part Name];[Namespace.Type]";
            this.listSiteDefinitionWebPartNames.Size = new System.Drawing.Size(440, 22);
            this.listSiteDefinitionWebPartNames.TabIndex = 11;
            // 
            // listSiteDefinitionWebParts
            // 
            this.listSiteDefinitionWebParts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSiteDefinitionWebParts.Caption = "Properties:";
            this.listSiteDefinitionWebParts.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listSiteDefinitionWebParts.Location = new System.Drawing.Point(8, 42);
            this.listSiteDefinitionWebParts.Name = "listSiteDefinitionWebParts";
            this.listSiteDefinitionWebParts.PromptText = "[name];[name];[name];";
            this.listSiteDefinitionWebParts.Size = new System.Drawing.Size(440, 22);
            this.listSiteDefinitionWebParts.TabIndex = 10;
            // 
            // listSiteDefinitionXMLDocuments
            // 
            this.listSiteDefinitionXMLDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSiteDefinitionXMLDocuments.Caption = "Documents:";
            this.listSiteDefinitionXMLDocuments.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listSiteDefinitionXMLDocuments.Location = new System.Drawing.Point(8, 42);
            this.listSiteDefinitionXMLDocuments.Name = "listSiteDefinitionXMLDocuments";
            this.listSiteDefinitionXMLDocuments.PromptText = "[*.xml];[myfile.txt]";
            this.listSiteDefinitionXMLDocuments.Size = new System.Drawing.Size(440, 22);
            this.listSiteDefinitionXMLDocuments.TabIndex = 13;
            // 
            // listSiteDefinitionXmlLibraries
            // 
            this.listSiteDefinitionXmlLibraries.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSiteDefinitionXmlLibraries.Caption = "Doc. Libraries:";
            this.listSiteDefinitionXmlLibraries.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listSiteDefinitionXmlLibraries.Location = new System.Drawing.Point(8, 18);
            this.listSiteDefinitionXmlLibraries.Name = "listSiteDefinitionXmlLibraries";
            this.listSiteDefinitionXmlLibraries.PromptText = "[name];[name];[name];";
            this.listSiteDefinitionXmlLibraries.Size = new System.Drawing.Size(440, 22);
            this.listSiteDefinitionXmlLibraries.TabIndex = 12;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(8, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(440, 82);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // textBoxSiteURL
            // 
            this.textBoxSiteURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSiteURL.FocusSelect = true;
            this.textBoxSiteURL.Location = new System.Drawing.Point(64, 24);
            this.textBoxSiteURL.Name = "textBoxSiteURL";
            this.textBoxSiteURL.PromptFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxSiteURL.PromptForeColor = System.Drawing.SystemColors.GrayText;
            this.textBoxSiteURL.PromptText = "http://localhost";
            this.textBoxSiteURL.Size = new System.Drawing.Size(384, 21);
            this.textBoxSiteURL.TabIndex = 5;
            this.textBoxSiteURL.TextChanged += new System.EventHandler(this.textBoxSiteURL_TextChanged);
            // 
            // listSiteDefinitionLists
            // 
            this.listSiteDefinitionLists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSiteDefinitionLists.Caption = "Lists:";
            this.listSiteDefinitionLists.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listSiteDefinitionLists.Location = new System.Drawing.Point(8, 18);
            this.listSiteDefinitionLists.Name = "listSiteDefinitionLists";
            this.listSiteDefinitionLists.PromptText = "[name];[name];[name];";
            this.listSiteDefinitionLists.Size = new System.Drawing.Size(440, 22);
            this.listSiteDefinitionLists.TabIndex = 7;
            this.listSiteDefinitionLists.PromtedTextChanged += new System.EventHandler(this.listSiteDefinitionLists_PromtedTextChanged);
            // 
            // listSiteDefinitionFields
            // 
            this.listSiteDefinitionFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listSiteDefinitionFields.Caption = "Fields:";
            this.listSiteDefinitionFields.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.listSiteDefinitionFields.Location = new System.Drawing.Point(8, 42);
            this.listSiteDefinitionFields.Name = "listSiteDefinitionFields";
            this.listSiteDefinitionFields.PromptText = "[name];[name];[name];";
            this.listSiteDefinitionFields.Size = new System.Drawing.Size(440, 22);
            this.listSiteDefinitionFields.TabIndex = 8;
            this.listSiteDefinitionFields.PromtedTextChanged += new System.EventHandler(this.listSiteDefinitionLists_PromtedTextChanged);
            // 
            // FindAndReplace
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(472, 633);
            this.Controls.Add(this.groupBoxWebParts);
            this.Controls.Add(this.groupBoxTextFiles);
            this.Controls.Add(this.linkKwizCom);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBoxSite);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.groupBoxFindReplace);
            this.Controls.Add(this.groupBoxLogFile);
            this.Controls.Add(this.groupBoxList);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(480, 660);
            this.Name = "FindAndReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KWizCom SharePoint Find and Replace";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FindAndReplace_Closing);
            this.Load += new System.EventHandler(this.FindAndReplace_Load);
            this.groupBoxFindReplace.ResumeLayout(false);
            this.groupBoxFindReplace.PerformLayout();
            this.groupBoxSite.ResumeLayout(false);
            this.groupBoxSite.PerformLayout();
            this.groupBoxLogFile.ResumeLayout(false);
            this.groupBoxLogFile.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxTextFiles.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelWebPartCheck.ResumeLayout(false);
            this.groupBoxList.ResumeLayout(false);
            this.panelListCheck.ResumeLayout(false);
            this.groupBoxWebParts.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try
			{
				Application.Run(new FindAndReplace());
			}
			catch(Exception ex)
			{
				Logger log = new Logger(Path.GetTempPath());
				log.LogFileBaseName = Logger.DEFAULT_LOG_FILENAME;
				
				log.Write("Critical error!", "SPListFindReplace.Main", ex);
 
				MessageBox.Show("A critical error occured! View log file for details!","SharePoint List String Find & Replace",MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		#region MAIN OPERATIONS

		/// <summary>
		/// Enable/disable start and exit buttins
		/// </summary>
		/// <param name="isEnable"></param>
		private void EnableButtons(bool isEnable)
		{
            if (this.InvokeRequired)//thread safe ui update
                this.Invoke(new MethodInvoker(() => EnableButtons(isEnable)));
            else
            {
                this.buttonExit.Enabled = isEnable;
                this.buttonStart.Enabled = isEnable;
                this.buttonStop.Enabled = !isEnable;
                this.pictureBox1.Visible = !isEnable;
            }
		}

		protected void PrepareParameters ()
		{
			m_Parameters.SharePointURL = this.textBoxSiteURL.Text.Trim();

			m_Parameters.IncludeSubSites = this.checkBoxSubSites.Checked;
			m_Parameters.IncludeWebParts = this.checkBoxSearchWebParts.Checked;
			m_Parameters.IncludeXmlFilesLibraries = this.checkBoxXmlSearch.Checked;
			m_Parameters.IncludeLists = this.checkBoxListProperties.Checked;

			m_Parameters.FindClause = this.textBoxFind.Text.Trim();
			m_Parameters.ReplaceClause = this.textBoxReplace.Text.Trim();
			
			// Xml Files
			if ( this.listSiteDefinitionXmlLibraries.IsAll )
			{
				m_Parameters.XmlFilesLibrariesMetaList = new StringCollection();
			}
			else
			{
				StringCollection s = new StringCollection();
				foreach(string v in this.listSiteDefinitionXmlLibraries.SelectedList)
					s.Add(v.Trim().ToUpper());
				m_Parameters.XmlFilesLibrariesMetaList = s;
			}
			if ( this.listSiteDefinitionXMLDocuments.IsAll )
			{
				m_Parameters.XmlFilesDocumentsMetaList = new StringCollection();
			}
			else
			{
				StringCollection s = new StringCollection();
				foreach(string v in this.listSiteDefinitionXMLDocuments.SelectedList)
					s.Add(v.Trim().ToUpper());
				m_Parameters.XmlFilesDocumentsMetaList = s;
			}

			// WebParts
			if ( this.listSiteDefinitionWebParts.IsAll )
			{
				m_Parameters.WebPartMetaList = new StringCollection();
			}
			else
			{
				StringCollection s = new StringCollection();
				foreach(string v in this.listSiteDefinitionWebParts.SelectedList)
					s.Add(v.Trim().ToUpper());
				m_Parameters.WebPartMetaList = s;
			}
			if ( this.listSiteDefinitionWebPartNames.IsAll )
			{
				m_Parameters.WebPartNamesList = new StringCollection();
			}
			else
			{
				StringCollection s = new StringCollection();
				foreach(string v in this.listSiteDefinitionWebPartNames.SelectedList)
					s.Add(v.Trim().ToUpper());
				m_Parameters.WebPartNamesList = s;
			}

			// Lists
			if ( this.listSiteDefinitionLists.IsAll )
			{				
				m_Parameters.ListList = new StringCollection();
			}
			else
			{
				StringCollection s = new StringCollection();
				foreach(string v in this.listSiteDefinitionLists.SelectedList)
					s.Add(v.Trim().ToUpper());
				m_Parameters.ListList = s;
			}

			// Fields
			if ( this.listSiteDefinitionFields.IsAll )
			{
				m_Parameters.FieldList = new StringCollection();
			}
			else
			{
				StringCollection s = new StringCollection();
				foreach(string v in this.listSiteDefinitionFields.SelectedList)
					s.Add(v.Trim().ToUpper());
				m_Parameters.FieldList = s;
			}
		}

		
		protected void StartFindReplace ()
		{
			this.richTextBox1.Text = "\r\n";
			this.richTextBox1.Focus();

			PrepareParameters ();
			InitValidateSPSite();
		}

		
		protected void WriteRealTimeLine(string message)
		{
			WriteRealTimeLine(message, "", null);
		}
		
		
		protected void WriteRealTimeLine(string message, string source)
		{
			WriteRealTimeLine(message, source, null);
		}

		
		protected void WriteRealTimeLine(string message, string source, Exception ex)
		{
			if ( message.Length == 0 )
			{
				this.richTextBox1.OutputText("");
				return;
			}

			string LineToRealTime;
			DateTime TimeStamp = DateTime.Now;

			m_Logger.Write(message, source, ex);
			if ( ex == null )
			{
				LineToRealTime = String.Format(@"{0}, {1} {2} ", TimeStamp.ToShortDateString(), TimeStamp.ToShortTimeString(), message);
			}
			else
			{
				Exception ex1 = ex;
				while (ex1 != null )
				{
					message += ex1.Message + "; ";
					ex1 = ex1.InnerException;
				}

				LineToRealTime = String.Format(@"{0}, {1}: ERROR: {2} ", TimeStamp.ToShortDateString(), TimeStamp.ToShortTimeString(), message);
			}
			
			this.richTextBox1.OutputText(LineToRealTime);
		}

		
		#endregion

		private void checkBoxSubSites_CheckedChanged(object sender, System.EventArgs e)
		{			
		}

		private void checkBoxListProperties_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( checkBoxListProperties.Checked )
			{
				listSiteDefinitionLists.Enabled = true;
				listSiteDefinitionFields.Enabled = true;
			}
			else
			{
				listSiteDefinitionLists.Enabled = false;
				listSiteDefinitionFields.Enabled = false;
			}
		}

		private void checkBoxSearchWebParts_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( checkBoxSearchWebParts.Checked )
			{
				listSiteDefinitionWebParts.Enabled = true;
				listSiteDefinitionWebPartNames.Enabled = true;
			}
			else
			{
				listSiteDefinitionWebParts.Enabled = false;
				listSiteDefinitionWebPartNames.Enabled = false;
			}
		}

		private void checkBoxXmlSearch_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( checkBoxXmlSearch.Checked )
			{
				listSiteDefinitionXmlLibraries.Enabled = true;
				listSiteDefinitionXMLDocuments.Enabled = true;
			}
			else
			{
				listSiteDefinitionXmlLibraries.Enabled = false;
				listSiteDefinitionXMLDocuments.Enabled = false;
			}
		}

		private void buttonOpenFolder_Click(object sender, System.EventArgs e)
		{
			string LogFile = this.textBoxLogFile.Text;
			string LogFileName = Logger.DEFAULT_LOG_FILENAME;

			if ( LogFile.Length > 0 )
			{
				string FolderName = Path.GetPathRoot(LogFile);
				LogFileName = Path.GetFileName(LogFile);
				this.folderBrowserDialog1.SelectedPath = Path.GetFullPath(LogFile);
			}
			else
			{
				this.folderBrowserDialog1.SelectedPath = Path.GetTempPath();
			}

			if ( this.folderBrowserDialog1.ShowDialog() == DialogResult.OK )			
			{
				this.textBoxLogFile.Text = Path.Combine(this.folderBrowserDialog1.SelectedPath, LogFileName);

				m_Logger.LogFileLocation = this.folderBrowserDialog1.SelectedPath;
				m_Logger.LogFileBaseName = LogFileName;
			}
		}

		
		private void FindAndReplace_Load(object sender, System.EventArgs e)
		{
			this.checkBoxSubSites.Enabled = true;
			this.checkBoxSubSites.Checked = false;
			this.checkBoxSearchWebParts.Checked = false;
			this.checkBoxXmlSearch.Checked = false;

			this.textBoxLogFile.Text = Path.Combine(Application.StartupPath, Logger.DEFAULT_LOG_FILENAME);	

#if DEBUG
			this.textBoxSiteURL.Text = "http://localhost/sites/Test/sub/default.aspx";
#endif
		
			this.Text = Constants.ProductName + " " + Constants.ProductVersion;
			this.linkKwizCom.Click += new EventHandler(linkKwizCom_Click);
		}
		
		#region TEXTCHANGE EVENTS

		private void textBoxFind_TextChanged(object sender, System.EventArgs e)
		{
			EnableStartButton ();
		}

		
		private void textBoxReplace_TextChanged(object sender, System.EventArgs e)
		{
			EnableStartButton ();
		}

		
		private void textBoxSiteURL_TextChanged(object sender, System.EventArgs e)
		{			
			EnableStartButton ();
		}		

		private void listSiteDefinitionLists_PromtedTextChanged(object sender, System.EventArgs e)
		{
			EnableStartButton ();
		}
		
		private void EnableStartButton ()
		{
			this.buttonStart.Enabled = ( this.textBoxFind.Text.Length > 0 && 
				this.textBoxReplace.Text.Length > 0 &&
				this.textBoxSiteURL.Text.Length > 0 &&
				(listSiteDefinitionLists.IsAll || (listSiteDefinitionLists.IsAll == false && listSiteDefinitionLists.SelectedList.Length > 0 )) &&
				(listSiteDefinitionFields.IsAll || (listSiteDefinitionFields.IsAll == false && listSiteDefinitionFields.SelectedList.Length > 0 )));
		}


		#endregion

		#region BUTTONS EVENTS
		
		private void buttonStart_Click(object sender, System.EventArgs e)
		{
			EnableButtons(false);
			PrepareParameters ();
			StartFindReplace ();
		}

		
		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			m_Component.Stop ();
		}

		
		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		
		#endregion

		#region F&R COMPONENT EVENTS

		private void m_Component_Error(object sender, ComponentErrorEventArgs e)
		{
			WriteRealTimeLine(e.SourceName, "", e.InnerException);
		}

		
		private void m_Component_Trace(object sender, ComponentErrorEventArgs e)
		{
			WriteRealTimeLine(e.InnerException.Message, e.SourceName);
		}

		
		private void m_Component_Complete(object sender, ComponentEventArgs e)
		{
			WriteRealTimeLine("*     Find & Replace finished   *");			
			WriteRealTimeLine("**************************");
			EnableButtons(true);
		}

		
		private void m_Component_Abort(object sender, ComponentEventArgs e)
		{				
			WriteRealTimeLine("*     Find & Replace canceled     *");			
			WriteRealTimeLine("**************************");
			EnableButtons(true);
		}
		
		
		#endregion

		private void FindAndReplace_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ( !this.buttonExit.Enabled )
			{
				if ( MessageBox.Show(@"The Find & Replace process is still running.\r\nDo you want to stop execution and exit the appliction?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes )
				{
					e.Cancel = false;
				}
				else
					e.Cancel = true;
			}
		}


		#region VALIDATION PORTAL

		private void InitValidateSPSite ()
		{
			WriteRealTimeLine("**********************");
			WriteRealTimeLine("Start checking SharePoint portal...");

			m_PortalCheck = new SPSiteCheck(m_Parameters.SharePointURL, 1);
			m_PortalCheck.Complete += new ComponentEventHandler(m_PortalCheck_Complete);
			m_PortalCheck.Error += new ComponentErrorEventHandler(m_PortalCheck_Error);

			m_PortalCheck.Start ();
		}

		
		private void m_PortalCheck_Complete(object sender, ComponentEventArgs e)
		{
			string RealTimeLine = string.Empty;

			switch(e.CurrentStatus)
			{
				case eCheckStatus.Passed: RealTimeLine = "Checking SharePoint site is succeeded."; break;
				case eCheckStatus.Failed: RealTimeLine = "Checking SharePoint site is failed."; break;
				case eCheckStatus.Warning: RealTimeLine = "Some problelm discovered by checking SharePoint site."; break;
			}

			WriteRealTimeLine(RealTimeLine);

			if ( e.CurrentStatus == eCheckStatus.Passed )
				m_Component.Start();
			else
			{
				EnableButtons(true);
			}
		}

		
		private void m_PortalCheck_Error(object sender, ComponentErrorEventArgs e)
		{
			WriteRealTimeLine("Validation failed!", e.SourceName, e.InnerException);
		}

		
		#endregion		


		private void linkKwizCom_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(this.linkKwizCom.Text);
		}

		private void groupBoxSite_Enter(object sender, System.EventArgs e)
		{
		
		}

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

	}
}
