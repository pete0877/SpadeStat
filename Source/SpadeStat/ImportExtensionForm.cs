using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SpadeStat
{
	/// <summary>
	/// Summary description for ImportExtensionForm.
	/// </summary>
	public class ImportExtensionForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox cbTxt;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public bool m_bSelectedTxt = false;
		public bool m_bSelectedDone = false;
		private System.Windows.Forms.CheckBox cbDone;
		public bool m_bConfirmed = false;

		public ImportExtensionForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.cbTxt = new System.Windows.Forms.CheckBox();
			this.cbDone = new System.Windows.Forms.CheckBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(256, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Please select file extensions to import:";
			// 
			// cbTxt
			// 
			this.cbTxt.Checked = true;
			this.cbTxt.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbTxt.Location = new System.Drawing.Point(32, 32);
			this.cbTxt.Name = "cbTxt";
			this.cbTxt.Size = new System.Drawing.Size(232, 24);
			this.cbTxt.TabIndex = 1;
			this.cbTxt.Text = "Text files (*.txt)";
			// 
			// cbDone
			// 
			this.cbDone.Location = new System.Drawing.Point(32, 64);
			this.cbDone.Name = "cbDone";
			this.cbDone.Size = new System.Drawing.Size(232, 24);
			this.cbDone.TabIndex = 2;
			this.cbDone.Text = "Already imported files (*.done)";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(176, 104);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Close";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(32, 104);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 23);
			this.btnOk.TabIndex = 8;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// ImportExtensionForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 143);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnOk,
																		  this.btnCancel,
																		  this.cbDone,
																		  this.cbTxt,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportExtensionForm";
			this.ShowInTaskbar = false;
			this.Text = "Extension selection";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			m_bConfirmed = true;
			m_bSelectedTxt = cbTxt.Checked;
			m_bSelectedDone = cbDone.Checked;

			Close();
		}
	}
}
