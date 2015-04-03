using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace SpadeStat
{
	/// <summary>
	/// Summary description for RegForm.
	/// </summary>
	public class RegForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbRequestKey;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnOpenOrderForm;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox tbLicenseKey;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button tbnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RegForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RegForm));
			this.tbRequestKey = new System.Windows.Forms.TextBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.btnOpenOrderForm = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.tbLicenseKey = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.tbnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbRequestKey
			// 
			this.tbRequestKey.Location = new System.Drawing.Point(16, 72);
			this.tbRequestKey.Multiline = true;
			this.tbRequestKey.Name = "tbRequestKey";
			this.tbRequestKey.ReadOnly = true;
			this.tbRequestKey.Size = new System.Drawing.Size(584, 72);
			this.tbRequestKey.TabIndex = 0;
			this.tbRequestKey.Text = "";
			// 
			// btnGenerate
			// 
			this.btnGenerate.Location = new System.Drawing.Point(408, 48);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(192, 23);
			this.btnGenerate.TabIndex = 1;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(408, 144);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(192, 24);
			this.btnCopy.TabIndex = 6;
			this.btnCopy.Text = "Copy to Clipboard";
			this.btnCopy.Click += new System.EventHandler(this.button1_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(176, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Request Key:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(624, 24);
			this.label3.TabIndex = 8;
			this.label3.Text = "To unlock the trial installation of this software, follow these 3 easy steps:";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Step 1:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(64, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(392, 16);
			this.label4.TabIndex = 10;
			this.label4.Text = "Use the Generate button below to create license Request Key.";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(64, 192);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(544, 40);
			this.label5.TabIndex = 12;
			this.label5.Text = "Fill out the on-line order form. You will need to provide the Request Key generat" +
				"ed above. You can use the button below to automatically open browser window and " +
				"pre-populate the form or simply go to:  ";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 192);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 16);
			this.label6.TabIndex = 11;
			this.label6.Text = "Step 2:";
			// 
			// btnOpenOrderForm
			// 
			this.btnOpenOrderForm.Location = new System.Drawing.Point(408, 224);
			this.btnOpenOrderForm.Name = "btnOpenOrderForm";
			this.btnOpenOrderForm.Size = new System.Drawing.Size(192, 23);
			this.btnOpenOrderForm.TabIndex = 13;
			this.btnOpenOrderForm.Text = "Open the Order Form";
			this.btnOpenOrderForm.Click += new System.EventHandler(this.btnOpenOrderForm_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(64, 220);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(176, 14);
			this.linkLabel1.TabIndex = 14;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://www.spadestat.com/order";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(64, 272);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(536, 32);
			this.label7.TabIndex = 16;
			this.label7.Text = "During the order process you will receive additional instructions. You will recei" +
				"ve your License Key via email. Paste the key into the text area below and click " +
				"on Ok to finalize the process. ";
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(8, 272);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(64, 16);
			this.label8.TabIndex = 15;
			this.label8.Text = "Step 3:";
			// 
			// tbLicenseKey
			// 
			this.tbLicenseKey.Location = new System.Drawing.Point(16, 328);
			this.tbLicenseKey.Multiline = true;
			this.tbLicenseKey.Name = "tbLicenseKey";
			this.tbLicenseKey.Size = new System.Drawing.Size(584, 72);
			this.tbLicenseKey.TabIndex = 17;
			this.tbLicenseKey.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 312);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(176, 16);
			this.label9.TabIndex = 18;
			this.label9.Text = "License Key:";
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(88, 416);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(192, 24);
			this.btnOk.TabIndex = 19;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// tbnCancel
			// 
			this.tbnCancel.Location = new System.Drawing.Point(328, 416);
			this.tbnCancel.Name = "tbnCancel";
			this.tbnCancel.Size = new System.Drawing.Size(192, 24);
			this.tbnCancel.TabIndex = 20;
			this.tbnCancel.Text = "Cancel";
			this.tbnCancel.Click += new System.EventHandler(this.tbnCancel_Click);
			// 
			// RegForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(616, 453);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tbnCancel,
																		  this.btnOk,
																		  this.label9,
																		  this.tbLicenseKey,
																		  this.label7,
																		  this.label8,
																		  this.linkLabel1,
																		  this.btnOpenOrderForm,
																		  this.label5,
																		  this.label6,
																		  this.label4,
																		  this.label1,
																		  this.label3,
																		  this.label2,
																		  this.btnCopy,
																		  this.btnGenerate,
																		  this.tbRequestKey});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RegForm";
			this.ShowInTaskbar = false;
			this.Text = "Software License";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnGenerate_Click(object sender, System.EventArgs e)
		{
			tbRequestKey.Text = "";
			MessageBox.Show("This might take few seconds. Click on the Ok button to begin.", "Information");

			string CPUID = "";
			ManagementClass cpuInfo = new ManagementClass("Win32_Processor"); 
			ManagementObjectCollection cpuInfoItr = cpuInfo.GetInstances(); 
			foreach(ManagementObject cpu in cpuInfoItr) 
			{
				CPUID = cpu["ProcessorId"].ToString();		
				break;
			}

			// Create license request object:
			LicenseRequest request = new LicenseRequest();
			request.CPUID = CPUID;

			IFormatter formatter = new BinaryFormatter();
			MemoryStream stream = new MemoryStream();
			formatter.Serialize(stream, request);
	
			string requestKey = Utils.EncryptStream(stream);
			stream.Close();

			tbRequestKey.Text = requestKey;

			Clipboard.SetDataObject(requestKey);
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			string requestKey = tbRequestKey.Text;
			if (requestKey == null || requestKey.Length == 0)
			{
				MessageBox.Show("Use the Generate button first", "Problem Found");
				return;
			}
			Clipboard.SetDataObject(requestKey);
			MessageBox.Show("The generated Request Key has been copied to your clipboard.", "Information");
		}

		private void tbnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			string requestKey = tbRequestKey.Text;

			IFormatter formatter = new BinaryFormatter();
			MemoryStream stream = Utils.DecryptStream(requestKey);
			LicenseRequest request = (LicenseRequest) formatter.Deserialize(stream);
			stream.Close();

			MessageBox.Show("CPU ID confirmed: " + request.CPUID, "Information");

			Close();
		}

		private void btnOpenOrderForm_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
