using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SpadeStat
{
	/// <summary>
	/// Summary description for PositionCritForm.
	/// </summary>
	public class PositionCritForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnPl2Pos1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnPl2Pos2;
		private System.Windows.Forms.Button btnPl3Pos2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnPl3Pos1;
		private System.Windows.Forms.Button btnPl3Pos3;
		private System.Windows.Forms.Button btnPl4Pos3;
		private System.Windows.Forms.Button btnPl4Pos1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnPl4Pos2;
		private System.Windows.Forms.Button btnPl4Pos4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnPl5Pos4;
		private System.Windows.Forms.Button btnPl5Pos3;
		private System.Windows.Forms.Button btnPl5Pos1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnPl5Pos2;
		private System.Windows.Forms.Button btnPl5Pos5;
		private System.Windows.Forms.Button btnPl6Pos5;
		private System.Windows.Forms.Button btnPl6Pos4;
		private System.Windows.Forms.Button btnPl6Pos3;
		private System.Windows.Forms.Button btnPl6Pos1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnPl6Pos2;
		private System.Windows.Forms.Button btnPl6Pos6;
		private System.Windows.Forms.Button btnPl7Pos3;
		private System.Windows.Forms.Button btnPl7Pos1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnPl7Pos2;
		private System.Windows.Forms.Button btnPl7Pos6;
		private System.Windows.Forms.Button btnPl7Pos5;
		private System.Windows.Forms.Button btnPl7Pos4;
		private System.Windows.Forms.Button btnPl7Pos7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnPl8Pos7;
		private System.Windows.Forms.Button btnPl8Pos6;
		private System.Windows.Forms.Button btnPl8Pos5;
		private System.Windows.Forms.Button btnPl8Pos4;
		private System.Windows.Forms.Button btnPl8Pos3;
		private System.Windows.Forms.Button btnPl8Pos1;
		private System.Windows.Forms.Button btnPl8Pos2;
		private System.Windows.Forms.Button btnPl8Pos8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnPl9Pos8;
		private System.Windows.Forms.Button btnPl9Pos7;
		private System.Windows.Forms.Button btnPl9Pos6;
		private System.Windows.Forms.Button btnPl9Pos5;
		private System.Windows.Forms.Button btnPl9Pos4;
		private System.Windows.Forms.Button btnPl9Pos3;
		private System.Windows.Forms.Button btnPl9Pos1;
		private System.Windows.Forms.Button btnPl9Pos2;
		private System.Windows.Forms.Button btnPl9Pos9;
		private string[] m_positionCriteria;

		public PositionCritForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public void SetPositionCriteria(string[] positionCriteria)
		{
			m_positionCriteria = positionCriteria;
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
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnPl2Pos1 = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnPl2Pos2 = new System.Windows.Forms.Button();
			this.btnPl3Pos2 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.btnPl3Pos1 = new System.Windows.Forms.Button();
			this.btnPl3Pos3 = new System.Windows.Forms.Button();
			this.btnPl4Pos3 = new System.Windows.Forms.Button();
			this.btnPl4Pos1 = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.btnPl4Pos2 = new System.Windows.Forms.Button();
			this.btnPl4Pos4 = new System.Windows.Forms.Button();
			this.btnPl5Pos4 = new System.Windows.Forms.Button();
			this.btnPl5Pos3 = new System.Windows.Forms.Button();
			this.btnPl5Pos1 = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.btnPl5Pos2 = new System.Windows.Forms.Button();
			this.btnPl5Pos5 = new System.Windows.Forms.Button();
			this.btnPl6Pos5 = new System.Windows.Forms.Button();
			this.btnPl6Pos4 = new System.Windows.Forms.Button();
			this.btnPl6Pos3 = new System.Windows.Forms.Button();
			this.btnPl6Pos1 = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.btnPl6Pos2 = new System.Windows.Forms.Button();
			this.btnPl6Pos6 = new System.Windows.Forms.Button();
			this.btnPl7Pos6 = new System.Windows.Forms.Button();
			this.btnPl7Pos5 = new System.Windows.Forms.Button();
			this.btnPl7Pos4 = new System.Windows.Forms.Button();
			this.btnPl7Pos3 = new System.Windows.Forms.Button();
			this.btnPl7Pos1 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.btnPl7Pos2 = new System.Windows.Forms.Button();
			this.btnPl7Pos7 = new System.Windows.Forms.Button();
			this.btnPl8Pos7 = new System.Windows.Forms.Button();
			this.btnPl8Pos6 = new System.Windows.Forms.Button();
			this.btnPl8Pos5 = new System.Windows.Forms.Button();
			this.btnPl8Pos4 = new System.Windows.Forms.Button();
			this.btnPl8Pos3 = new System.Windows.Forms.Button();
			this.btnPl8Pos1 = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.btnPl8Pos2 = new System.Windows.Forms.Button();
			this.btnPl8Pos8 = new System.Windows.Forms.Button();
			this.btnPl9Pos8 = new System.Windows.Forms.Button();
			this.btnPl9Pos7 = new System.Windows.Forms.Button();
			this.btnPl9Pos6 = new System.Windows.Forms.Button();
			this.btnPl9Pos5 = new System.Windows.Forms.Button();
			this.btnPl9Pos4 = new System.Windows.Forms.Button();
			this.btnPl9Pos3 = new System.Windows.Forms.Button();
			this.btnPl9Pos1 = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.btnPl9Pos2 = new System.Windows.Forms.Button();
			this.btnPl9Pos9 = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Players:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(24, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "2";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(64, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Positions:";
			// 
			// btnPl2Pos1
			// 
			this.btnPl2Pos1.Location = new System.Drawing.Point(64, 32);
			this.btnPl2Pos1.Name = "btnPl2Pos1";
			this.btnPl2Pos1.Size = new System.Drawing.Size(24, 24);
			this.btnPl2Pos1.TabIndex = 3;
			this.btnPl2Pos1.Text = "1";
			this.btnPl2Pos1.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(64, 248);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnPl2Pos2
			// 
			this.btnPl2Pos2.Location = new System.Drawing.Point(88, 32);
			this.btnPl2Pos2.Name = "btnPl2Pos2";
			this.btnPl2Pos2.Size = new System.Drawing.Size(24, 24);
			this.btnPl2Pos2.TabIndex = 5;
			this.btnPl2Pos2.Text = "2";
			this.btnPl2Pos2.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl3Pos2
			// 
			this.btnPl3Pos2.Location = new System.Drawing.Point(88, 56);
			this.btnPl3Pos2.Name = "btnPl3Pos2";
			this.btnPl3Pos2.Size = new System.Drawing.Size(24, 24);
			this.btnPl3Pos2.TabIndex = 6;
			this.btnPl3Pos2.Text = "2";
			this.btnPl3Pos2.Click += new System.EventHandler(this.PositionChange);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "3";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnPl3Pos1
			// 
			this.btnPl3Pos1.Location = new System.Drawing.Point(64, 56);
			this.btnPl3Pos1.Name = "btnPl3Pos1";
			this.btnPl3Pos1.Size = new System.Drawing.Size(24, 24);
			this.btnPl3Pos1.TabIndex = 8;
			this.btnPl3Pos1.Text = "1";
			this.btnPl3Pos1.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl3Pos3
			// 
			this.btnPl3Pos3.Location = new System.Drawing.Point(112, 56);
			this.btnPl3Pos3.Name = "btnPl3Pos3";
			this.btnPl3Pos3.Size = new System.Drawing.Size(24, 24);
			this.btnPl3Pos3.TabIndex = 9;
			this.btnPl3Pos3.Text = "3";
			this.btnPl3Pos3.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl4Pos3
			// 
			this.btnPl4Pos3.Location = new System.Drawing.Point(112, 80);
			this.btnPl4Pos3.Name = "btnPl4Pos3";
			this.btnPl4Pos3.Size = new System.Drawing.Size(24, 24);
			this.btnPl4Pos3.TabIndex = 13;
			this.btnPl4Pos3.Text = "3";
			this.btnPl4Pos3.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl4Pos1
			// 
			this.btnPl4Pos1.Location = new System.Drawing.Point(64, 80);
			this.btnPl4Pos1.Name = "btnPl4Pos1";
			this.btnPl4Pos1.Size = new System.Drawing.Size(24, 24);
			this.btnPl4Pos1.TabIndex = 12;
			this.btnPl4Pos1.Text = "1";
			this.btnPl4Pos1.Click += new System.EventHandler(this.PositionChange);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 83);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(24, 16);
			this.label5.TabIndex = 11;
			this.label5.Text = "4";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnPl4Pos2
			// 
			this.btnPl4Pos2.Location = new System.Drawing.Point(88, 80);
			this.btnPl4Pos2.Name = "btnPl4Pos2";
			this.btnPl4Pos2.Size = new System.Drawing.Size(24, 24);
			this.btnPl4Pos2.TabIndex = 10;
			this.btnPl4Pos2.Text = "2";
			this.btnPl4Pos2.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl4Pos4
			// 
			this.btnPl4Pos4.Location = new System.Drawing.Point(136, 80);
			this.btnPl4Pos4.Name = "btnPl4Pos4";
			this.btnPl4Pos4.Size = new System.Drawing.Size(24, 24);
			this.btnPl4Pos4.TabIndex = 14;
			this.btnPl4Pos4.Text = "4";
			this.btnPl4Pos4.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl5Pos4
			// 
			this.btnPl5Pos4.Location = new System.Drawing.Point(136, 104);
			this.btnPl5Pos4.Name = "btnPl5Pos4";
			this.btnPl5Pos4.Size = new System.Drawing.Size(24, 24);
			this.btnPl5Pos4.TabIndex = 19;
			this.btnPl5Pos4.Text = "4";
			this.btnPl5Pos4.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl5Pos3
			// 
			this.btnPl5Pos3.Location = new System.Drawing.Point(112, 104);
			this.btnPl5Pos3.Name = "btnPl5Pos3";
			this.btnPl5Pos3.Size = new System.Drawing.Size(24, 24);
			this.btnPl5Pos3.TabIndex = 18;
			this.btnPl5Pos3.Text = "3";
			this.btnPl5Pos3.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl5Pos1
			// 
			this.btnPl5Pos1.Location = new System.Drawing.Point(64, 104);
			this.btnPl5Pos1.Name = "btnPl5Pos1";
			this.btnPl5Pos1.Size = new System.Drawing.Size(24, 24);
			this.btnPl5Pos1.TabIndex = 17;
			this.btnPl5Pos1.Text = "1";
			this.btnPl5Pos1.Click += new System.EventHandler(this.PositionChange);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 107);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(24, 16);
			this.label6.TabIndex = 16;
			this.label6.Text = "5";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnPl5Pos2
			// 
			this.btnPl5Pos2.Location = new System.Drawing.Point(88, 104);
			this.btnPl5Pos2.Name = "btnPl5Pos2";
			this.btnPl5Pos2.Size = new System.Drawing.Size(24, 24);
			this.btnPl5Pos2.TabIndex = 15;
			this.btnPl5Pos2.Text = "2";
			this.btnPl5Pos2.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl5Pos5
			// 
			this.btnPl5Pos5.Location = new System.Drawing.Point(160, 104);
			this.btnPl5Pos5.Name = "btnPl5Pos5";
			this.btnPl5Pos5.Size = new System.Drawing.Size(24, 24);
			this.btnPl5Pos5.TabIndex = 20;
			this.btnPl5Pos5.Text = "5";
			this.btnPl5Pos5.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl6Pos5
			// 
			this.btnPl6Pos5.Location = new System.Drawing.Point(160, 128);
			this.btnPl6Pos5.Name = "btnPl6Pos5";
			this.btnPl6Pos5.Size = new System.Drawing.Size(24, 24);
			this.btnPl6Pos5.TabIndex = 26;
			this.btnPl6Pos5.Text = "5";
			this.btnPl6Pos5.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl6Pos4
			// 
			this.btnPl6Pos4.Location = new System.Drawing.Point(136, 128);
			this.btnPl6Pos4.Name = "btnPl6Pos4";
			this.btnPl6Pos4.Size = new System.Drawing.Size(24, 24);
			this.btnPl6Pos4.TabIndex = 25;
			this.btnPl6Pos4.Text = "4";
			this.btnPl6Pos4.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl6Pos3
			// 
			this.btnPl6Pos3.Location = new System.Drawing.Point(112, 128);
			this.btnPl6Pos3.Name = "btnPl6Pos3";
			this.btnPl6Pos3.Size = new System.Drawing.Size(24, 24);
			this.btnPl6Pos3.TabIndex = 24;
			this.btnPl6Pos3.Text = "3";
			this.btnPl6Pos3.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl6Pos1
			// 
			this.btnPl6Pos1.Location = new System.Drawing.Point(64, 128);
			this.btnPl6Pos1.Name = "btnPl6Pos1";
			this.btnPl6Pos1.Size = new System.Drawing.Size(24, 24);
			this.btnPl6Pos1.TabIndex = 23;
			this.btnPl6Pos1.Text = "1";
			this.btnPl6Pos1.Click += new System.EventHandler(this.PositionChange);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 131);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(24, 16);
			this.label7.TabIndex = 22;
			this.label7.Text = "6";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnPl6Pos2
			// 
			this.btnPl6Pos2.Location = new System.Drawing.Point(88, 128);
			this.btnPl6Pos2.Name = "btnPl6Pos2";
			this.btnPl6Pos2.Size = new System.Drawing.Size(24, 24);
			this.btnPl6Pos2.TabIndex = 21;
			this.btnPl6Pos2.Text = "2";
			this.btnPl6Pos2.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl6Pos6
			// 
			this.btnPl6Pos6.Location = new System.Drawing.Point(184, 128);
			this.btnPl6Pos6.Name = "btnPl6Pos6";
			this.btnPl6Pos6.Size = new System.Drawing.Size(24, 24);
			this.btnPl6Pos6.TabIndex = 27;
			this.btnPl6Pos6.Text = "6";
			this.btnPl6Pos6.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl7Pos6
			// 
			this.btnPl7Pos6.Location = new System.Drawing.Point(184, 152);
			this.btnPl7Pos6.Name = "btnPl7Pos6";
			this.btnPl7Pos6.Size = new System.Drawing.Size(24, 24);
			this.btnPl7Pos6.TabIndex = 34;
			this.btnPl7Pos6.Text = "6";
			this.btnPl7Pos6.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl7Pos5
			// 
			this.btnPl7Pos5.Location = new System.Drawing.Point(160, 152);
			this.btnPl7Pos5.Name = "btnPl7Pos5";
			this.btnPl7Pos5.Size = new System.Drawing.Size(24, 24);
			this.btnPl7Pos5.TabIndex = 33;
			this.btnPl7Pos5.Text = "5";
			this.btnPl7Pos5.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl7Pos4
			// 
			this.btnPl7Pos4.Location = new System.Drawing.Point(136, 152);
			this.btnPl7Pos4.Name = "btnPl7Pos4";
			this.btnPl7Pos4.Size = new System.Drawing.Size(24, 24);
			this.btnPl7Pos4.TabIndex = 32;
			this.btnPl7Pos4.Text = "4";
			this.btnPl7Pos4.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl7Pos3
			// 
			this.btnPl7Pos3.Location = new System.Drawing.Point(112, 152);
			this.btnPl7Pos3.Name = "btnPl7Pos3";
			this.btnPl7Pos3.Size = new System.Drawing.Size(24, 24);
			this.btnPl7Pos3.TabIndex = 31;
			this.btnPl7Pos3.Text = "3";
			this.btnPl7Pos3.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl7Pos1
			// 
			this.btnPl7Pos1.Location = new System.Drawing.Point(64, 152);
			this.btnPl7Pos1.Name = "btnPl7Pos1";
			this.btnPl7Pos1.Size = new System.Drawing.Size(24, 24);
			this.btnPl7Pos1.TabIndex = 30;
			this.btnPl7Pos1.Text = "1";
			this.btnPl7Pos1.Click += new System.EventHandler(this.PositionChange);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 155);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(24, 16);
			this.label8.TabIndex = 29;
			this.label8.Text = "7";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnPl7Pos2
			// 
			this.btnPl7Pos2.Location = new System.Drawing.Point(88, 152);
			this.btnPl7Pos2.Name = "btnPl7Pos2";
			this.btnPl7Pos2.Size = new System.Drawing.Size(24, 24);
			this.btnPl7Pos2.TabIndex = 28;
			this.btnPl7Pos2.Text = "2";
			this.btnPl7Pos2.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl7Pos7
			// 
			this.btnPl7Pos7.Location = new System.Drawing.Point(208, 152);
			this.btnPl7Pos7.Name = "btnPl7Pos7";
			this.btnPl7Pos7.Size = new System.Drawing.Size(24, 24);
			this.btnPl7Pos7.TabIndex = 35;
			this.btnPl7Pos7.Text = "7";
			this.btnPl7Pos7.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl8Pos7
			// 
			this.btnPl8Pos7.Location = new System.Drawing.Point(208, 176);
			this.btnPl8Pos7.Name = "btnPl8Pos7";
			this.btnPl8Pos7.Size = new System.Drawing.Size(24, 24);
			this.btnPl8Pos7.TabIndex = 43;
			this.btnPl8Pos7.Text = "7";
			this.btnPl8Pos7.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl8Pos6
			// 
			this.btnPl8Pos6.Location = new System.Drawing.Point(184, 176);
			this.btnPl8Pos6.Name = "btnPl8Pos6";
			this.btnPl8Pos6.Size = new System.Drawing.Size(24, 24);
			this.btnPl8Pos6.TabIndex = 42;
			this.btnPl8Pos6.Text = "6";
			this.btnPl8Pos6.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl8Pos5
			// 
			this.btnPl8Pos5.Location = new System.Drawing.Point(160, 176);
			this.btnPl8Pos5.Name = "btnPl8Pos5";
			this.btnPl8Pos5.Size = new System.Drawing.Size(24, 24);
			this.btnPl8Pos5.TabIndex = 41;
			this.btnPl8Pos5.Text = "5";
			this.btnPl8Pos5.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl8Pos4
			// 
			this.btnPl8Pos4.Location = new System.Drawing.Point(136, 176);
			this.btnPl8Pos4.Name = "btnPl8Pos4";
			this.btnPl8Pos4.Size = new System.Drawing.Size(24, 24);
			this.btnPl8Pos4.TabIndex = 40;
			this.btnPl8Pos4.Text = "4";
			this.btnPl8Pos4.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl8Pos3
			// 
			this.btnPl8Pos3.Location = new System.Drawing.Point(112, 176);
			this.btnPl8Pos3.Name = "btnPl8Pos3";
			this.btnPl8Pos3.Size = new System.Drawing.Size(24, 24);
			this.btnPl8Pos3.TabIndex = 39;
			this.btnPl8Pos3.Text = "3";
			this.btnPl8Pos3.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl8Pos1
			// 
			this.btnPl8Pos1.Location = new System.Drawing.Point(64, 176);
			this.btnPl8Pos1.Name = "btnPl8Pos1";
			this.btnPl8Pos1.Size = new System.Drawing.Size(24, 24);
			this.btnPl8Pos1.TabIndex = 38;
			this.btnPl8Pos1.Text = "1";
			this.btnPl8Pos1.Click += new System.EventHandler(this.PositionChange);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 180);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(24, 16);
			this.label9.TabIndex = 37;
			this.label9.Text = "8";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnPl8Pos2
			// 
			this.btnPl8Pos2.Location = new System.Drawing.Point(88, 176);
			this.btnPl8Pos2.Name = "btnPl8Pos2";
			this.btnPl8Pos2.Size = new System.Drawing.Size(24, 24);
			this.btnPl8Pos2.TabIndex = 36;
			this.btnPl8Pos2.Text = "2";
			this.btnPl8Pos2.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl8Pos8
			// 
			this.btnPl8Pos8.Location = new System.Drawing.Point(232, 176);
			this.btnPl8Pos8.Name = "btnPl8Pos8";
			this.btnPl8Pos8.Size = new System.Drawing.Size(24, 24);
			this.btnPl8Pos8.TabIndex = 44;
			this.btnPl8Pos8.Text = "8";
			this.btnPl8Pos8.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl9Pos8
			// 
			this.btnPl9Pos8.Location = new System.Drawing.Point(232, 200);
			this.btnPl9Pos8.Name = "btnPl9Pos8";
			this.btnPl9Pos8.Size = new System.Drawing.Size(24, 24);
			this.btnPl9Pos8.TabIndex = 53;
			this.btnPl9Pos8.Text = "8";
			this.btnPl9Pos8.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl9Pos7
			// 
			this.btnPl9Pos7.Location = new System.Drawing.Point(208, 200);
			this.btnPl9Pos7.Name = "btnPl9Pos7";
			this.btnPl9Pos7.Size = new System.Drawing.Size(24, 24);
			this.btnPl9Pos7.TabIndex = 52;
			this.btnPl9Pos7.Text = "7";
			this.btnPl9Pos7.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl9Pos6
			// 
			this.btnPl9Pos6.Location = new System.Drawing.Point(184, 200);
			this.btnPl9Pos6.Name = "btnPl9Pos6";
			this.btnPl9Pos6.Size = new System.Drawing.Size(24, 24);
			this.btnPl9Pos6.TabIndex = 51;
			this.btnPl9Pos6.Text = "6";
			this.btnPl9Pos6.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl9Pos5
			// 
			this.btnPl9Pos5.Location = new System.Drawing.Point(160, 200);
			this.btnPl9Pos5.Name = "btnPl9Pos5";
			this.btnPl9Pos5.Size = new System.Drawing.Size(24, 24);
			this.btnPl9Pos5.TabIndex = 50;
			this.btnPl9Pos5.Text = "5";
			this.btnPl9Pos5.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl9Pos4
			// 
			this.btnPl9Pos4.Location = new System.Drawing.Point(136, 200);
			this.btnPl9Pos4.Name = "btnPl9Pos4";
			this.btnPl9Pos4.Size = new System.Drawing.Size(24, 24);
			this.btnPl9Pos4.TabIndex = 49;
			this.btnPl9Pos4.Text = "4";
			this.btnPl9Pos4.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl9Pos3
			// 
			this.btnPl9Pos3.Location = new System.Drawing.Point(112, 200);
			this.btnPl9Pos3.Name = "btnPl9Pos3";
			this.btnPl9Pos3.Size = new System.Drawing.Size(24, 24);
			this.btnPl9Pos3.TabIndex = 48;
			this.btnPl9Pos3.Text = "3";
			this.btnPl9Pos3.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl9Pos1
			// 
			this.btnPl9Pos1.Location = new System.Drawing.Point(64, 200);
			this.btnPl9Pos1.Name = "btnPl9Pos1";
			this.btnPl9Pos1.Size = new System.Drawing.Size(24, 24);
			this.btnPl9Pos1.TabIndex = 47;
			this.btnPl9Pos1.Text = "1";
			this.btnPl9Pos1.Click += new System.EventHandler(this.PositionChange);
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(16, 204);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(24, 16);
			this.label10.TabIndex = 46;
			this.label10.Text = "9";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnPl9Pos2
			// 
			this.btnPl9Pos2.Location = new System.Drawing.Point(88, 200);
			this.btnPl9Pos2.Name = "btnPl9Pos2";
			this.btnPl9Pos2.Size = new System.Drawing.Size(24, 24);
			this.btnPl9Pos2.TabIndex = 45;
			this.btnPl9Pos2.Text = "2";
			this.btnPl9Pos2.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnPl9Pos9
			// 
			this.btnPl9Pos9.Location = new System.Drawing.Point(256, 200);
			this.btnPl9Pos9.Name = "btnPl9Pos9";
			this.btnPl9Pos9.Size = new System.Drawing.Size(24, 24);
			this.btnPl9Pos9.TabIndex = 54;
			this.btnPl9Pos9.Text = "9";
			this.btnPl9Pos9.Click += new System.EventHandler(this.PositionChange);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(152, 248);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 24);
			this.btnCancel.TabIndex = 55;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// PositionCritForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(298, 287);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnCancel,
																		  this.btnPl9Pos9,
																		  this.btnPl9Pos8,
																		  this.btnPl9Pos7,
																		  this.btnPl9Pos6,
																		  this.btnPl9Pos5,
																		  this.btnPl9Pos4,
																		  this.btnPl9Pos3,
																		  this.btnPl9Pos1,
																		  this.label10,
																		  this.btnPl9Pos2,
																		  this.btnPl8Pos8,
																		  this.btnPl8Pos7,
																		  this.btnPl8Pos6,
																		  this.btnPl8Pos5,
																		  this.btnPl8Pos4,
																		  this.btnPl8Pos3,
																		  this.btnPl8Pos1,
																		  this.label9,
																		  this.btnPl8Pos2,
																		  this.btnPl7Pos7,
																		  this.btnPl7Pos6,
																		  this.btnPl7Pos5,
																		  this.btnPl7Pos4,
																		  this.btnPl7Pos3,
																		  this.btnPl7Pos1,
																		  this.label8,
																		  this.btnPl7Pos2,
																		  this.btnPl6Pos6,
																		  this.btnPl6Pos5,
																		  this.btnPl6Pos4,
																		  this.btnPl6Pos3,
																		  this.btnPl6Pos1,
																		  this.label7,
																		  this.btnPl6Pos2,
																		  this.btnPl5Pos5,
																		  this.btnPl5Pos4,
																		  this.btnPl5Pos3,
																		  this.btnPl5Pos1,
																		  this.label6,
																		  this.btnPl5Pos2,
																		  this.btnPl4Pos4,
																		  this.btnPl4Pos3,
																		  this.btnPl4Pos1,
																		  this.label5,
																		  this.btnPl4Pos2,
																		  this.btnPl3Pos3,
																		  this.btnPl3Pos1,
																		  this.label4,
																		  this.btnPl3Pos2,
																		  this.btnPl2Pos2,
																		  this.btnOk,
																		  this.btnPl2Pos1,
																		  this.label3,
																		  this.label2,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PositionCritForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Positions";
			this.Load += new System.EventHandler(this.PositionCritForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			string positionSpec;

			// 2 players:
			positionSpec = "";
			if (btnPl2Pos1.FlatStyle == FlatStyle.Flat)
				positionSpec += " 1";
			if (btnPl2Pos2.FlatStyle == FlatStyle.Flat)
				positionSpec += " 2";
			if (positionSpec != "")
				m_positionCriteria[0] = "2: " + positionSpec;
			else
				m_positionCriteria[0] = null;

			// 3 players:
			positionSpec = "";
			if (btnPl3Pos1.FlatStyle == FlatStyle.Flat)
				positionSpec += " 1";
			if (btnPl3Pos2.FlatStyle == FlatStyle.Flat)
				positionSpec += " 2";
			if (btnPl3Pos3.FlatStyle == FlatStyle.Flat)
				positionSpec += " 3";
			if (positionSpec != "")
				m_positionCriteria[1] = "3: " + positionSpec;
			else
				m_positionCriteria[1] = null;

			// 4 players:
			positionSpec = "";
			if (btnPl4Pos1.FlatStyle == FlatStyle.Flat)
				positionSpec += " 1";
			if (btnPl4Pos2.FlatStyle == FlatStyle.Flat)
				positionSpec += " 2";
			if (btnPl4Pos3.FlatStyle == FlatStyle.Flat)
				positionSpec += " 3";
			if (btnPl4Pos4.FlatStyle == FlatStyle.Flat)
				positionSpec += " 4";
			if (positionSpec != "")
				m_positionCriteria[2] = "4: " + positionSpec;
			else
				m_positionCriteria[2] = null;

			// 5 players:
			positionSpec = "";
			if (btnPl5Pos1.FlatStyle == FlatStyle.Flat)
				positionSpec += " 1";
			if (btnPl5Pos2.FlatStyle == FlatStyle.Flat)
				positionSpec += " 2";
			if (btnPl5Pos3.FlatStyle == FlatStyle.Flat)
				positionSpec += " 3";
			if (btnPl5Pos4.FlatStyle == FlatStyle.Flat)
				positionSpec += " 4";
			if (btnPl5Pos5.FlatStyle == FlatStyle.Flat)
				positionSpec += " 5";
			if (positionSpec != "")
				m_positionCriteria[3] = "5: " + positionSpec;
			else
				m_positionCriteria[3] = null;

			// 6 players:
			positionSpec = "";
			if (btnPl6Pos1.FlatStyle == FlatStyle.Flat)
				positionSpec += " 1";
			if (btnPl6Pos2.FlatStyle == FlatStyle.Flat)
				positionSpec += " 2";
			if (btnPl6Pos3.FlatStyle == FlatStyle.Flat)
				positionSpec += " 3";
			if (btnPl6Pos4.FlatStyle == FlatStyle.Flat)
				positionSpec += " 4";
			if (btnPl6Pos5.FlatStyle == FlatStyle.Flat)
				positionSpec += " 5";
			if (btnPl6Pos6.FlatStyle == FlatStyle.Flat)
				positionSpec += " 6";
			if (positionSpec != "")
				m_positionCriteria[4] = "6: " + positionSpec;
			else
				m_positionCriteria[4] = null;

			// 7 players:
			positionSpec = "";
			if (btnPl7Pos1.FlatStyle == FlatStyle.Flat)
				positionSpec += " 1";
			if (btnPl7Pos2.FlatStyle == FlatStyle.Flat)
				positionSpec += " 2";
			if (btnPl7Pos3.FlatStyle == FlatStyle.Flat)
				positionSpec += " 3";
			if (btnPl7Pos4.FlatStyle == FlatStyle.Flat)
				positionSpec += " 4";
			if (btnPl7Pos5.FlatStyle == FlatStyle.Flat)
				positionSpec += " 5";
			if (btnPl7Pos6.FlatStyle == FlatStyle.Flat)
				positionSpec += " 6";
			if (btnPl7Pos7.FlatStyle == FlatStyle.Flat)
				positionSpec += " 7";
			if (positionSpec != "")
				m_positionCriteria[5] = "7: " + positionSpec;
			else
				m_positionCriteria[5] = null;

			// 8 players:
			positionSpec = "";
			if (btnPl8Pos1.FlatStyle == FlatStyle.Flat)
				positionSpec += " 1";
			if (btnPl8Pos2.FlatStyle == FlatStyle.Flat)
				positionSpec += " 2";
			if (btnPl8Pos3.FlatStyle == FlatStyle.Flat)
				positionSpec += " 3";
			if (btnPl8Pos4.FlatStyle == FlatStyle.Flat)
				positionSpec += " 4";
			if (btnPl8Pos5.FlatStyle == FlatStyle.Flat)
				positionSpec += " 5";
			if (btnPl8Pos6.FlatStyle == FlatStyle.Flat)
				positionSpec += " 6";
			if (btnPl8Pos7.FlatStyle == FlatStyle.Flat)
				positionSpec += " 7";
			if (btnPl8Pos8.FlatStyle == FlatStyle.Flat)
				positionSpec += " 8";
			if (positionSpec != "")
				m_positionCriteria[6] = "8: " + positionSpec;
			else
				m_positionCriteria[6] = null;

			// 9 players:
			positionSpec = "";
			if (btnPl9Pos1.FlatStyle == FlatStyle.Flat)
				positionSpec += " 1";
			if (btnPl9Pos2.FlatStyle == FlatStyle.Flat)
				positionSpec += " 2";
			if (btnPl9Pos3.FlatStyle == FlatStyle.Flat)
				positionSpec += " 3";
			if (btnPl9Pos4.FlatStyle == FlatStyle.Flat)
				positionSpec += " 4";
			if (btnPl9Pos5.FlatStyle == FlatStyle.Flat)
				positionSpec += " 5";
			if (btnPl9Pos6.FlatStyle == FlatStyle.Flat)
				positionSpec += " 6";
			if (btnPl9Pos7.FlatStyle == FlatStyle.Flat)
				positionSpec += " 7";
			if (btnPl9Pos8.FlatStyle == FlatStyle.Flat)
				positionSpec += " 8";
			if (btnPl9Pos9.FlatStyle == FlatStyle.Flat)
				positionSpec += " 9";
			if (positionSpec != "")
				m_positionCriteria[7] = "9: " + positionSpec;
			else
				m_positionCriteria[7] = null;

			Close();
		}

		private void PositionCritForm_Load(object sender, System.EventArgs e)
		{
			string positionSpec = null;

			// 2 players
			positionSpec = m_positionCriteria[0]; 
			if (positionSpec != null) 
			{
				if (positionSpec.IndexOf('1', 1) > 0)
				{					
					btnPl2Pos1.FlatStyle = FlatStyle.Flat;
					btnPl2Pos1.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('2', 1) > 0)
				{					
					btnPl2Pos2.FlatStyle = FlatStyle.Flat;
					btnPl2Pos2.BackColor = Color.Yellow;
				}
			}

			// 3 players
			positionSpec = m_positionCriteria[1]; 
			if (positionSpec != null) 
			{
				if (positionSpec.IndexOf('1', 1) > 0)
				{					
					btnPl3Pos1.FlatStyle = FlatStyle.Flat;
					btnPl3Pos1.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('2', 1) > 0)
				{					
					btnPl3Pos2.FlatStyle = FlatStyle.Flat;
					btnPl3Pos2.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('3', 1) > 0)
				{					
					btnPl3Pos3.FlatStyle = FlatStyle.Flat;
					btnPl3Pos3.BackColor = Color.Yellow;
				}
			}

			// 4 players
			positionSpec = m_positionCriteria[2]; 
			if (positionSpec != null) 
			{
				if (positionSpec.IndexOf('1', 1) > 0)
				{					
					btnPl4Pos1.FlatStyle = FlatStyle.Flat;
					btnPl4Pos1.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('2', 1) > 0)
				{					
					btnPl4Pos2.FlatStyle = FlatStyle.Flat;
					btnPl4Pos2.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('3', 1) > 0)
				{					
					btnPl4Pos3.FlatStyle = FlatStyle.Flat;
					btnPl4Pos3.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('4', 1) > 0)
				{					
					btnPl4Pos4.FlatStyle = FlatStyle.Flat;
					btnPl4Pos4.BackColor = Color.Yellow;
				}
			}


			// 5 players
			positionSpec = m_positionCriteria[3]; 
			if (positionSpec != null) 
			{
				if (positionSpec.IndexOf('1', 1) > 0)
				{					
					btnPl5Pos1.FlatStyle = FlatStyle.Flat;
					btnPl5Pos1.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('2', 1) > 0)
				{					
					btnPl5Pos2.FlatStyle = FlatStyle.Flat;
					btnPl5Pos2.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('3', 1) > 0)
				{					
					btnPl5Pos3.FlatStyle = FlatStyle.Flat;
					btnPl5Pos3.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('4', 1) > 0)
				{					
					btnPl5Pos4.FlatStyle = FlatStyle.Flat;
					btnPl5Pos4.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('5', 1) > 0)
				{					
					btnPl5Pos5.FlatStyle = FlatStyle.Flat;
					btnPl5Pos5.BackColor = Color.Yellow;
				}
			}


			// 6 players
			positionSpec = m_positionCriteria[4]; 
			if (positionSpec != null) 
			{
				if (positionSpec.IndexOf('1', 1) > 0)
				{					
					btnPl6Pos1.FlatStyle = FlatStyle.Flat;
					btnPl6Pos1.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('2', 1) > 0)
				{					
					btnPl6Pos2.FlatStyle = FlatStyle.Flat;
					btnPl6Pos2.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('3', 1) > 0)
				{					
					btnPl6Pos3.FlatStyle = FlatStyle.Flat;
					btnPl6Pos3.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('4', 1) > 0)
				{					
					btnPl6Pos4.FlatStyle = FlatStyle.Flat;
					btnPl6Pos4.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('5', 1) > 0)
				{					
					btnPl6Pos5.FlatStyle = FlatStyle.Flat;
					btnPl6Pos5.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('6', 1) > 0)
				{					
					btnPl6Pos6.FlatStyle = FlatStyle.Flat;
					btnPl6Pos6.BackColor = Color.Yellow;
				}
			}


			// 7 players
			positionSpec = m_positionCriteria[5]; 
			if (positionSpec != null) 
			{
				if (positionSpec.IndexOf('1', 1) > 0)
				{					
					btnPl7Pos1.FlatStyle = FlatStyle.Flat;
					btnPl7Pos1.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('2', 1) > 0)
				{					
					btnPl7Pos2.FlatStyle = FlatStyle.Flat;
					btnPl7Pos2.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('3', 1) > 0)
				{					
					btnPl7Pos3.FlatStyle = FlatStyle.Flat;
					btnPl7Pos3.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('4', 1) > 0)
				{					
					btnPl7Pos4.FlatStyle = FlatStyle.Flat;
					btnPl7Pos4.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('5', 1) > 0)
				{					
					btnPl7Pos5.FlatStyle = FlatStyle.Flat;
					btnPl7Pos5.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('6', 1) > 0)
				{					
					btnPl7Pos6.FlatStyle = FlatStyle.Flat;
					btnPl7Pos6.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('7', 1) > 0)
				{					
					btnPl7Pos7.FlatStyle = FlatStyle.Flat;
					btnPl7Pos7.BackColor = Color.Yellow;
				}
			}


			// 8 players
			positionSpec = m_positionCriteria[6]; 
			if (positionSpec != null) 
			{
				if (positionSpec.IndexOf('1', 1) > 0)
				{					
					btnPl8Pos1.FlatStyle = FlatStyle.Flat;
					btnPl8Pos1.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('2', 1) > 0)
				{					
					btnPl8Pos2.FlatStyle = FlatStyle.Flat;
					btnPl8Pos2.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('3', 1) > 0)
				{					
					btnPl8Pos3.FlatStyle = FlatStyle.Flat;
					btnPl8Pos3.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('4', 1) > 0)
				{					
					btnPl8Pos4.FlatStyle = FlatStyle.Flat;
					btnPl8Pos4.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('5', 1) > 0)
				{					
					btnPl8Pos5.FlatStyle = FlatStyle.Flat;
					btnPl8Pos5.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('6', 1) > 0)
				{					
					btnPl8Pos6.FlatStyle = FlatStyle.Flat;
					btnPl8Pos6.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('7', 1) > 0)
				{					
					btnPl8Pos7.FlatStyle = FlatStyle.Flat;
					btnPl8Pos7.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('8', 1) > 0)
				{					
					btnPl8Pos8.FlatStyle = FlatStyle.Flat;
					btnPl8Pos8.BackColor = Color.Yellow;
				}
			}

			// 9 players
			positionSpec = m_positionCriteria[7]; 
			if (positionSpec != null) 
			{
				if (positionSpec.IndexOf('1', 1) > 0)
				{					
					btnPl9Pos1.FlatStyle = FlatStyle.Flat;
					btnPl9Pos1.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('2', 1) > 0)
				{					
					btnPl9Pos2.FlatStyle = FlatStyle.Flat;
					btnPl9Pos2.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('3', 1) > 0)
				{					
					btnPl9Pos3.FlatStyle = FlatStyle.Flat;
					btnPl9Pos3.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('4', 1) > 0)
				{					
					btnPl9Pos4.FlatStyle = FlatStyle.Flat;
					btnPl9Pos4.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('5', 1) > 0)
				{					
					btnPl9Pos5.FlatStyle = FlatStyle.Flat;
					btnPl9Pos5.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('6', 1) > 0)
				{					
					btnPl9Pos6.FlatStyle = FlatStyle.Flat;
					btnPl9Pos6.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('7', 1) > 0)
				{					
					btnPl9Pos7.FlatStyle = FlatStyle.Flat;
					btnPl9Pos7.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('8', 1) > 0)
				{					
					btnPl9Pos8.FlatStyle = FlatStyle.Flat;
					btnPl9Pos8.BackColor = Color.Yellow;
				}
				if (positionSpec.IndexOf('9', 1) > 0)
				{					
					btnPl9Pos9.FlatStyle = FlatStyle.Flat;
					btnPl9Pos9.BackColor = Color.Yellow;
				}
			}
		}

		private void PositionChange(object sender, System.EventArgs e)
		{
			Button btn = (Button) sender;
			if (btn.FlatStyle != FlatStyle.Flat)
			{
				btn.FlatStyle = FlatStyle.Flat;
				btn.BackColor = Color.Yellow;
			}
			else
			{
				btn.FlatStyle = FlatStyle.Standard;
				btn.BackColor = SystemColors.InactiveBorder;
			}	
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}
 
	}
}
