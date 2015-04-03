using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using SpadeStat.Engine;
using Npgsql;

namespace SpadeStat
{
	/// <summary>
	/// Summary description for DataImportForm.
	/// </summary>
	public class DataImportForm : System.Windows.Forms.Form
	{
		protected Engine.ImportPokerStars m_importer;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ListBox fileListBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ProgressBar progressFile;
		private System.Windows.Forms.ProgressBar progressOverall;
		private System.Windows.Forms.StatusBar statusBar;
		private NpgsqlConnection dbConnection;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemove;
		private Thread m_threadImport;
		private System.Windows.Forms.Button btnAddFolder;
		private System.Windows.Forms.CheckBox chboxRenameDone;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DataImportForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public void SetDBConnection(NpgsqlConnection newDbConnection)
		{
			dbConnection = newDbConnection;
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
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.fileListBox = new System.Windows.Forms.ListBox();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.progressFile = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.progressOverall = new System.Windows.Forms.ProgressBar();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnAddFolder = new System.Windows.Forms.Button();
			this.chboxRenameDone = new System.Windows.Forms.CheckBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(8, 184);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(72, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add File";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(152, 184);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(72, 23);
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(240, 184);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(80, 23);
			this.btnStart.TabIndex = 4;
			this.btnStart.Text = "Start Import";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(424, 184);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Close";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "txt";
			this.openFileDialog.Title = "Select Import File";
			this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
			// 
			// fileListBox
			// 
			this.fileListBox.HorizontalScrollbar = true;
			this.fileListBox.Location = new System.Drawing.Point(8, 8);
			this.fileListBox.Name = "fileListBox";
			this.fileListBox.Size = new System.Drawing.Size(496, 173);
			this.fileListBox.TabIndex = 5;
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 300);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(514, 22);
			this.statusBar.TabIndex = 6;
			this.statusBar.Text = "Select poker files to import";
			// 
			// progressFile
			// 
			this.progressFile.Location = new System.Drawing.Point(120, 240);
			this.progressFile.Name = "progressFile";
			this.progressFile.Size = new System.Drawing.Size(384, 23);
			this.progressFile.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 240);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 24);
			this.label1.TabIndex = 8;
			this.label1.Text = "File Progress";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 272);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 24);
			this.label2.TabIndex = 10;
			this.label2.Text = "Overall Progress";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// progressOverall
			// 
			this.progressOverall.Location = new System.Drawing.Point(121, 272);
			this.progressOverall.Name = "progressOverall";
			this.progressOverall.Size = new System.Drawing.Size(384, 23);
			this.progressOverall.TabIndex = 9;
			// 
			// btnStop
			// 
			this.btnStop.Enabled = false;
			this.btnStop.Location = new System.Drawing.Point(320, 184);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(80, 23);
			this.btnStop.TabIndex = 5;
			this.btnStop.Text = "Stop Import";
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnAddFolder
			// 
			this.btnAddFolder.Location = new System.Drawing.Point(80, 184);
			this.btnAddFolder.Name = "btnAddFolder";
			this.btnAddFolder.Size = new System.Drawing.Size(72, 23);
			this.btnAddFolder.TabIndex = 2;
			this.btnAddFolder.Text = "Add Folder";
			this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
			// 
			// chboxRenameDone
			// 
			this.chboxRenameDone.Checked = true;
			this.chboxRenameDone.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chboxRenameDone.Location = new System.Drawing.Point(8, 208);
			this.chboxRenameDone.Name = "chboxRenameDone";
			this.chboxRenameDone.Size = new System.Drawing.Size(352, 24);
			this.chboxRenameDone.TabIndex = 7;
			this.chboxRenameDone.Text = "Rename files to *.done upon sucessfull completion.";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// DataImportForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(514, 322);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.chboxRenameDone,
																		  this.btnAddFolder,
																		  this.btnStop,
																		  this.label2,
																		  this.progressOverall,
																		  this.label1,
																		  this.progressFile,
																		  this.statusBar,
																		  this.fileListBox,
																		  this.btnCancel,
																		  this.btnStart,
																		  this.btnRemove,
																		  this.btnAdd});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.Name = "DataImportForm";
			this.ShowInTaskbar = false;
			this.Text = "Import Data";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			openFileDialog.Filter = "Text files (*.txt)|*.txt|Done files (*.done)|*.done|All files (*.*)|*.*";
			openFileDialog.ShowDialog();
		}

		private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			statusBar.Text = "Select poker files to import";

			System.Windows.Forms.OpenFileDialog fileSelector = (System.Windows.Forms.OpenFileDialog) sender;
			IEnumerator itr = fileSelector.FileNames.GetEnumerator();
			while (itr.MoveNext())
			{
				string path = (string) itr.Current;
				fileListBox.Items.Add(path);
			}			
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			statusBar.Text = "Select poker files to import";

			int selectedIndex = fileListBox.SelectedIndex;
			if (selectedIndex >= 0)
				fileListBox.Items.RemoveAt(selectedIndex);
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			progressFile.Value = 0;
			progressOverall.Value = 0;

			if (fileListBox.Items.Count == 0)
			{
				MessageBox.Show("Please Add at least one file.", "Problem Found");
				statusBar.Text = "Select poker files to import";
				return;
			}

			// Disable controls that need to be deactivated during import:
			btnAdd.Enabled = false;
			btnAddFolder.Enabled = false;
			btnCancel.Enabled = false;
			btnRemove.Enabled = false;
			btnStart.Enabled = false;
			btnStop.Enabled = true;
			chboxRenameDone.Enabled = false;
			
			m_threadImport = new Thread(new ThreadStart(StartImport));
			m_threadImport.Start();
		}

		void StartImport()
		{
			try
			{
				m_importer = new ImportPokerStars(dbConnection);

				// Register with progress event sender:
				m_importer.Changed += new ProgressEventHandler(FileProgressMade);

				progressOverall.Step = 1;
				progressOverall.Value = 0;
				progressOverall.Minimum = 0;
				progressOverall.Maximum = fileListBox.Items.Count;

				progressFile.Step = 1;
				progressFile.Value = 0;
				progressFile.Minimum = 0;
				progressFile.Maximum = 100;

				DateTime started = DateTime.Now;

				ArrayList filePaths = new ArrayList();
				IEnumerator itrTemp = fileListBox.Items.GetEnumerator();
				while (itrTemp.MoveNext())
				{
					string filePath = (string) itrTemp.Current;
					filePaths.Add(filePath);
				}

				IEnumerator itr = filePaths.GetEnumerator();
				while (itr.MoveNext())
				{
					string filePath = (string) itr.Current;
					statusBar.Text = "Importing " + filePath;

					m_importer.Import(filePath);

					progressOverall.PerformStep();
					fileListBox.Items.RemoveAt(0);

					// Rename the file if needed:
					if (chboxRenameDone.Checked)
						File.Move(filePath, filePath + ".done");
				}

				DateTime ended = DateTime.Now;

				TimeSpan span = ended.Subtract(started);
				double seconds = span.TotalSeconds;
				
				statusBar.Text = "Import completed successfully";

				string strMessage = "Import completed successfully.\n";
				strMessage += "\nPerformance stats: ";
				strMessage += "\n  Processing time [hh:mm:ss]: " + span.Hours.ToString() + ":" + span.Minutes.ToString() + ":" + span.Seconds.ToString();
				strMessage += "\n  Processed hands: " + m_importer.GetImportedHands().ToString();
				strMessage += "\n  Processed tournaments: " + m_importer.GetImportedTours().ToString();
				strMessage += "\n  Processed files: " + m_importer.GetImportedFiles().ToString();

				MessageBox.Show(strMessage, "Information");
			
				fileListBox.Items.Clear();
			}
			catch (ThreadAbortException e)
			{			
				// For some reason .Net is not throwing this type of exception.
			}			
			catch (Exception error)
			{
				if (error.Message.IndexOf("Thread was being aborted") < 0)
					MessageBox.Show("Following problem was found during import: \n\n" + error.Message, "Problem Found");
			}		

			progressFile.Value = 0;
			progressOverall.Value = 0;

			btnAdd.Enabled = true;
			btnAddFolder.Enabled = true;
			btnCancel.Enabled = true;
			btnRemove.Enabled = true;
			btnStart.Enabled = true;
			btnStop.Enabled = false;
			chboxRenameDone.Enabled = true;
		}


		// This will be called whenever the list changes.
		private void FileProgressMade(object sender, EventArgs e) 
		{			
			int progress = m_importer.GetProgressPercentage();
			progressFile.Value = progress;
			this.Show();
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			progressFile.Value = 0;
			progressOverall.Value = 0;

			btnAdd.Enabled = true;
			btnAddFolder.Enabled = true;
			btnCancel.Enabled = true;
			btnRemove.Enabled = true;
			btnStart.Enabled = true;
			btnStop.Enabled = false;
			chboxRenameDone.Enabled = true;

			m_threadImport.Abort();
		}



		private void btnAddFolder_Click(object sender, System.EventArgs e)
		{
			FolderSelectForm folderSelectForm = new FolderSelectForm();
			folderSelectForm.HeaderText = "Choose folder from under which to import all *.txt files:";
			if (folderSelectForm.ShowDialog() == DialogResult.OK)
			{
				string path = folderSelectForm.SelectedFolder;
				ImportExtensionForm extensionSelectForm = new ImportExtensionForm();
				extensionSelectForm.ShowDialog();
				if (extensionSelectForm.m_bConfirmed)
					AddFileFromFolder(path, extensionSelectForm.m_bSelectedTxt, extensionSelectForm.m_bSelectedDone);
			}			
		}

		private void AddFileFromFolder(string path, bool bTxt, bool bDone)
		{
			if (bTxt)
			{
				string[] files = Directory.GetFiles(path, "*.txt");
				IEnumerator itrFiles = files.GetEnumerator();
				while (itrFiles.MoveNext())
				{
					string file = (string) itrFiles.Current;
					fileListBox.Items.Add(file);
				}

				string[] folders = Directory.GetDirectories(path);
				IEnumerator itrFolders = folders.GetEnumerator();
				while (itrFolders.MoveNext())
				{
					string folder = (string) itrFolders.Current;
					AddFileFromFolder(folder, bTxt, bDone);
				}
			}

			if (bDone)
			{
				string[] files = Directory.GetFiles(path, "*.done");
				IEnumerator itrFiles = files.GetEnumerator();
				while (itrFiles.MoveNext())
				{
					string file = (string) itrFiles.Current;
					fileListBox.Items.Add(file);
				}

				string[] folders = Directory.GetDirectories(path);
				IEnumerator itrFolders = folders.GetEnumerator();
				while (itrFolders.MoveNext())
				{
					string folder = (string) itrFolders.Current;
					AddFileFromFolder(folder, bTxt, bDone);
				}
			}
		}

		private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
		
		}
	}
}

