using System;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace SpadeStat
{
	/// <summary>
	/// Dialog form used to select an import folder
	/// </summary>
	public class FolderSelectForm : FolderNameEditor
	{
		/// <summary>
		/// Helper class from .Net
		/// </summary>
		private FolderBrowser m_fileBrowser = new FolderBrowser();

		/// <summary>
		/// Form header description
		/// </summary>
		private string m_headerText			= "Choose Directory";

		/// <summary>
		/// Selected path (folder)
		/// </summary>
		private string m_selectedFolder		= "";


		/// <summary>
		/// Constructor
		/// </summary>
		public FolderSelectForm()
		{
		}


		/// <summary>
		/// Opens the dialog.
		/// </summary>
		/// <returns></returns>
		private DialogResult RunDialog()
		{
			m_fileBrowser.Description = HeaderText;
			m_fileBrowser.StartLocation = FolderBrowserFolder.MyComputer;

			DialogResult result = m_fileBrowser.ShowDialog();
			if (result == DialogResult.OK)
				m_selectedFolder = m_fileBrowser.DirectoryPath;
			else
				m_selectedFolder = String.Empty;

			return result;
		}


		/// <summary>
		/// Shows the dialog.
		/// </summary>
		/// <returns></returns>
		public DialogResult ShowDialog()
		{
			return RunDialog();
		}


		/// <summary>
		/// Sets and gets description to be used in the dialog
		/// </summary>
		public string HeaderText
		{
			set { m_headerText = value; }
			get { return m_headerText; }
		}


		/// <summary>
		/// Gets the selected folder
		/// </summary>
		public string SelectedFolder
		{
			get { return m_selectedFolder; }
		}


	}
}
