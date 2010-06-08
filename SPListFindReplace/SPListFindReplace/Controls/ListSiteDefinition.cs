using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace KWizCom.SharePoint.Utilities.SPListFindReplace.Controls
{
	/// <summary>
	/// Summary description for ListSiteDefinition.
	/// </summary>
	public class ListSiteDefinition : System.Windows.Forms.UserControl
	{
		public event EventHandler PromtedTextChanged;

		private System.Windows.Forms.Label labelCaption;
		private System.Windows.Forms.RadioButton radioButtonAll;
		private System.Windows.Forms.RadioButton radioButtonNames;
		private PromptedTextBox textBoxPromptValues;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ListSiteDefinition()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.labelCaption = new System.Windows.Forms.Label();
			this.radioButtonAll = new System.Windows.Forms.RadioButton();
			this.radioButtonNames = new System.Windows.Forms.RadioButton();
			this.textBoxPromptValues = new PromptedTextBox();
			this.SuspendLayout();
			// 
			// labelCaption
			// 
			this.labelCaption.Location = new System.Drawing.Point(0, 2);
			this.labelCaption.Name = "labelCaption";
			this.labelCaption.Size = new System.Drawing.Size(80, 16);
			this.labelCaption.TabIndex = 0;
			this.labelCaption.Text = "Sites:";
			this.labelCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// radioButtonAll
			// 
			this.radioButtonAll.Location = new System.Drawing.Point(80, 0);
			this.radioButtonAll.Name = "radioButtonAll";
			this.radioButtonAll.Size = new System.Drawing.Size(40, 24);
			this.radioButtonAll.TabIndex = 1;
			this.radioButtonAll.Text = "All";
			this.radioButtonAll.CheckedChanged += new System.EventHandler(this.radioButtonAll_CheckedChanged);
			// 
			// radioButtonNames
			// 
			this.radioButtonNames.Location = new System.Drawing.Point(128, 0);
			this.radioButtonNames.Name = "radioButtonNames";
			this.radioButtonNames.Size = new System.Drawing.Size(56, 24);
			this.radioButtonNames.TabIndex = 2;
			this.radioButtonNames.Text = "Names";
			// 
			// textBoxPromptValues
			// 
			this.textBoxPromptValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPromptValues.FocusSelect = true;
			this.textBoxPromptValues.Location = new System.Drawing.Point(184, 1);
			this.textBoxPromptValues.Name = "textBoxPromptValues";
			this.textBoxPromptValues.PromptFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.textBoxPromptValues.PromptForeColor = System.Drawing.SystemColors.GrayText;
			this.textBoxPromptValues.PromptText = "[name];[name];[name];";
			this.textBoxPromptValues.Size = new System.Drawing.Size(256, 21);
			this.textBoxPromptValues.TabIndex = 0;
			this.textBoxPromptValues.Text = "";
			this.textBoxPromptValues.TextChanged += new System.EventHandler(this.textBoxPromptValues_TextChanged);
			// 
			// ListSiteDefinition
			// 
			this.Controls.Add(this.textBoxPromptValues);
			this.Controls.Add(this.radioButtonNames);
			this.Controls.Add(this.radioButtonAll);
			this.Controls.Add(this.labelCaption);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(177)));
			this.Name = "ListSiteDefinition";
			this.Size = new System.Drawing.Size(440, 22);
			this.Load += new System.EventHandler(this.ListSiteDefinition_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void radioButtonAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( radioButtonAll.Checked )
			{
				this.textBoxPromptValues.Enabled = false;
			}
			else
			{
				this.textBoxPromptValues.Enabled = true;
			}

			OnPromtedTextChanged();
		}

		private void ListSiteDefinition_Load(object sender, System.EventArgs e)
		{
			radioButtonAll.Checked = true;
		}

		private void textBoxPromptValues_TextChanged(object sender, System.EventArgs e)
		{
			OnPromtedTextChanged ();
		}

		#region PUBLIC PROPERTIES

		/// <summary>
		/// Get/Set property represents caption of the control
		/// </summary>
		public string Caption
		{
			get { return this.labelCaption.Text; }
			set { this.labelCaption.Text = value; }
		}

		/// <summary>
		/// Readonly property: is all selected
		/// </summary>
		public bool IsAll
		{
			get { return this.radioButtonAll.Checked ; }
		}

		/// <summary>
		/// List of selected items (lists or sites)
		/// </summary>
		public string[] SelectedList
		{
			get { return GetSelectedList(); }
		}

		/// <summary>
		/// 
		/// </summary>
		public string PromptText
		{
			get
			{
				return this.textBoxPromptValues.PromptText;
			}
			set
			{
				this.textBoxPromptValues.PromptText = value;
			}
		}

		#endregion

		/// <summary>
		/// Helper function to fire TextChanged event
		/// </summary>
		protected void OnPromtedTextChanged()
		{
			if ( PromtedTextChanged != null )
			{
				PromtedTextChanged(this, new EventArgs());
			}
		}

		/// <summary>
		/// Parse values of textbox
		/// </summary>
		/// <returns></returns>
		protected string[] GetSelectedList ()
		{
			if ( this.textBoxPromptValues.Text.Length > 0 )
			{
				return this.textBoxPromptValues.Text.Split(';');
			}

			return new string[0];
		}
	}
}
