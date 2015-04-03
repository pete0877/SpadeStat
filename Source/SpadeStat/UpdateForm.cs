using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Npgsql;

namespace SpadeStat
{
	/// <summary>
	/// Form used for updating database with update script file.
	/// </summary>
	public class UpdateForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Button btnBegin;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Path the update script.
		/// </summary>
		private string m_path;

		/// <summary>
		/// Database connection used for all operations.
		/// </summary>
		private NpgsqlConnection m_dbConnection;

		/// <summary>
		/// Transaction used for all operations.
		/// </summary>
		private NpgsqlTransaction m_dbTransaction;


		/// <summary>
		/// Constructor
		/// </summary>
		public UpdateForm()
		{
			InitializeComponent();
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
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnBegin = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Location = new System.Drawing.Point(8, 8);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(296, 40);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "Your database needs to be updated. This might take a few minutes. Click on the Be" +
				"gin button below to begin.";
			// 
			// btnBegin
			// 
			this.btnBegin.Location = new System.Drawing.Point(104, 56);
			this.btnBegin.Name = "btnBegin";
			this.btnBegin.Size = new System.Drawing.Size(96, 24);
			this.btnBegin.TabIndex = 1;
			this.btnBegin.Text = "Begin";
			this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
			// 
			// UpdateForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 93);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnBegin,
																		  this.lblMessage});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateForm";
			this.ShowInTaskbar = false;
			this.Text = "SpadeStat database update";
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// Event hander for clicking on the Begin button. 
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnBegin_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (m_path != null && m_path != "")
				{
					btnBegin.Enabled = false;
					lblMessage.Text = "Please wait...";

					// Read in the whole file:
					FileStream updateFile = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true);
					StreamReader inputStream = new StreamReader(updateFile);
					string updateContent = inputStream.ReadToEnd();
					inputStream.Close();
					updateFile.Close();	

					m_dbConnection.Open();
					m_dbTransaction = m_dbConnection.BeginTransaction();
				
					// Split the content file by lines:
					string[] lines = updateContent.Split('\n');
					IEnumerator itr = lines.GetEnumerator();
					while (itr.MoveNext())
					{
						string line = (string) itr.Current;
						line = line.Replace("\n", "");
						if (line.Length == 0)
							continue;

						if (line.IndexOf("--") == 0)
							MessageBox.Show(line.Remove(0, 2), "Information");
						else
						{
							// Execute the command in the database:
							NpgsqlCommand command = m_dbTransaction.Connection.CreateCommand();	
							command.Transaction = m_dbTransaction;
							command.CommandText = line;
							command.ExecuteNonQuery();
						}
					}
				}

				m_dbTransaction.Commit();
				m_dbConnection.Close();
				
				// Rename the file with time-stamp.
				File.Move(m_path, m_path + "." + DateTime.Now.Ticks.ToString());

				MessageBox.Show("Database update completed sucessfully.", "Information");
				Close();
			} 
			catch (Exception error)
			{
				if (m_dbTransaction != null)
					m_dbTransaction.Rollback();
				m_dbConnection.Close();

				MessageBox.Show("Error occured during the update process. Please contact the SpadeStat technical support at http://www.spadestat.com", "Problem Found");
				Application.Exit();
			}
		}


		/// <summary>
		/// Accessor method. Sets the script file path to be ran. This method should be called before the dialog is opened.
		/// </summary>
		/// <param name="path">Full path to the file which needs to be ran</param>
		public void SetUpdateFile(string path)
		{
			m_path = path;
		}


		/// <summary>
		/// Sets the database connection to be used on all operations.
		/// </summary>
		/// <param name="dbConnection">Databse connection</param>
		public void SetDBConnection(NpgsqlConnection dbConnection)
		{
			m_dbConnection = dbConnection;
		}
	}
}
