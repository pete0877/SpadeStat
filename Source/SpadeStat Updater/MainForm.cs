using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Reflection;
using System.IO;

namespace SpadeStat_Updater
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnClose;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(304, 64);
			this.label1.TabIndex = 0;
			this.label1.Text = @"In order to perform auto-update of the SpadeStat software you must be connected to the Internet. You should also close SpadeStat application if it is currently running. When you are ready, click on the Start button below. Update usually takes only few minutes.";
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(8, 96);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(88, 24);
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(216, 96);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(88, 24);
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 133);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnClose,
																		  this.btnStart,
																		  this.label1});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "SpadeStat Updater";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			btnStart.Enabled = false;
			
			string installedVersionString = "";
			string latestVersionString = "";

			try
			{
				// Fist ensure that update wasn't just ran and was uncompleted:
				// This can can be done simply by checking for the update.dat file
				// which would be renamed to .timestamp if update completed
				string[] files = Directory.GetFiles(Application.StartupPath, "update.dat");
				if (files.Length != 0)
				{
					MessageBox.Show("Recent update of SpadeStat software has not completed yet. To complete the update, simply run the SpadeStat application.", "Problem Found");
					Application.Exit();			
					return;
				}

				// Check the version installed:
				Assembly assembly = Assembly.LoadFrom(Application.StartupPath + "\\SpadeStat.exe");
				if (assembly == null)
					throw new Exception("Could not find SpadeStat.exe");
				installedVersionString = assembly.GetName().Version.ToString();

				// Find the latest version number availabe on the site:
				try
				{
					System.Net.WebClient webClient = new System.Net.WebClient();
					byte[] latestVersion = webClient.DownloadData("http://www.spadestat.com/latestversion.txt");
					latestVersionString = System.Text.Encoding.UTF8.GetString(latestVersion);
				}
				catch (Exception exception)
				{
					throw new Exception("Could not connect to http://www.spadestat.com/latestversion.txt");
				}

			}
			catch (Exception error)
			{
				MessageBox.Show("Following fatal error was found: " + error.Message, "Problem Found");
				Application.Exit();
				return;
			}

			// Check if we are running latest version already:
			if (latestVersionString == installedVersionString)
			{
				MessageBox.Show("Your computer is already running the latest version of SpadeStat (" + installedVersionString + "). There is no need to perform update. ", "Information");
				Application.Exit();
				return;
			}

			// Check if the site is undergoing update / maintenance.
			if (latestVersionString == "*")
			{
				MessageBox.Show("The SpadeStat site is currently undergoing maintenance. Please try again later.", "Problem Found");
				Application.Exit();
				return;
			}

			// Begin the update process:
			try
			{			
				// Download the necessary files:
				byte[] fileBuffer;
				string fileURL;
				string fileSavePath;
				FileStream fileStream;
				BinaryWriter streamWriter;

				// SpadeStat.exe
				fileURL = "http://www.spadestat.com/update/" + installedVersionString + "/SpadeStat.zip";
				try
				{
					System.Net.WebClient webClient = new System.Net.WebClient();
					fileBuffer = webClient.DownloadData(fileURL);
				}
				catch (Exception exception)
				{
					throw new Exception("Could not connect to: " + fileURL);
				}

				fileSavePath = Application.StartupPath + "\\SpadeStat.new";
				fileStream = new FileStream(fileSavePath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, false);
				if (fileStream == null)
					throw new Exception("Could not create file: " + fileSavePath);

				streamWriter = new BinaryWriter(fileStream);
				if (streamWriter == null)
					throw new Exception("Could not create file: " + fileSavePath);

				streamWriter.Write(fileBuffer);
				streamWriter.Flush();
				fileStream.Flush();
				streamWriter.Close();
				fileStream.Close();


				// SpadeStatEngine.dll
				fileURL = "http://www.spadestat.com/update/" + installedVersionString + "/SpadeStatEngine.zip";
				try
				{
					System.Net.WebClient webClient = new System.Net.WebClient();
					fileBuffer = webClient.DownloadData(fileURL);
				}
				catch (Exception exception)
				{
					throw new Exception("Could not connect to: " + fileURL);
				}

				fileSavePath = Application.StartupPath + "\\SpadeStatEngine.new";
				fileStream = new FileStream(fileSavePath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, false);
				if (fileStream == null)
					throw new Exception("Could not create file: " + fileSavePath);

				streamWriter = new BinaryWriter(fileStream);
				if (streamWriter == null)
					throw new Exception("Could not create file: " + fileSavePath);

				streamWriter.Write(fileBuffer);
				streamWriter.Flush();
				fileStream.Flush();
				streamWriter.Close();
				fileStream.Close();


				// update.dat
				fileURL = "http://www.spadestat.com/update/" + installedVersionString + "/update.zip";
				try
				{
					System.Net.WebClient webClient = new System.Net.WebClient();
					fileBuffer = webClient.DownloadData(fileURL);
				}
				catch (Exception exception)
				{
					throw new Exception("Could not connect to: " + fileURL);
				}

				fileSavePath = Application.StartupPath + "\\update.new";
				fileStream = new FileStream(fileSavePath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, false);
				if (fileStream == null)
					throw new Exception("Could not create file: " + fileSavePath);

				streamWriter = new BinaryWriter(fileStream);
				if (streamWriter == null)
					throw new Exception("Could not create file: " + fileSavePath);

				streamWriter.Write(fileBuffer);
				streamWriter.Flush();
				fileStream.Flush();
				streamWriter.Close();
				fileStream.Close();

				// Rename existing files:
				File.Move(Application.StartupPath + "\\SpadeStat.exe", Application.StartupPath + "\\SpadeStat.exe." + DateTime.Now.Ticks.ToString());
				File.Move(Application.StartupPath + "\\SpadeStatEngine.dll", Application.StartupPath + "\\SpadeStatEngine.dll." + DateTime.Now.Ticks.ToString());

				// Rename downloaded files:
				File.Move(Application.StartupPath + "\\SpadeStat.new", Application.StartupPath + "\\SpadeStat.exe");
				File.Move(Application.StartupPath + "\\SpadeStatEngine.new", Application.StartupPath + "\\SpadeStatEngine.dll");
				File.Move(Application.StartupPath + "\\update.new", Application.StartupPath + "\\update.dat");

				btnStart.Enabled = false;
			}
			catch (Exception error)
			{
				MessageBox.Show("Following fatal error was found: " + error.Message, "Problem Found");
				Application.Exit();
				return;
			}			

			MessageBox.Show("New version of SpadeStat was installed successfully. Complete the update, please run the SpadeStat application now.", "Information");
			Application.Exit();
		 }
	}
}
