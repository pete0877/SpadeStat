using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Security;
using Microsoft.Win32;
using Npgsql;
using SpadeStat.Engine;


namespace SpadeStat
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form,  DebugFeeder
	{
		private System.Windows.Forms.Button btnPlayerAdd;
		private System.Windows.Forms.ListBox lbPlayers;
		private System.Windows.Forms.Button btnPlayerRemove;
		private System.Windows.Forms.Button btnPlayersClear;
		private System.Windows.Forms.ComboBox cbReportType;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox cbPlayer;
		private NpgsqlConnection dbConnection;
		private System.Net.WebClient webClient;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox tbSQLStatement;
		private System.Windows.Forms.Button btnRunSQL;
		private System.Windows.Forms.DataGrid dgSQLResults;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
		private System.Windows.Forms.MenuItem mitmPlayerData;
		private System.Windows.Forms.MenuItem mitmBankRoll;
		private System.Windows.Forms.MenuItem mitmQueryDatabase;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.ImageList ilMain;
		private System.Windows.Forms.TabPage tabReports;
		private System.Windows.Forms.TabPage tabBankRoll;
		private System.Windows.Forms.TabPage tabPlayerData;
		private System.Windows.Forms.TabPage tabQueryDatabase;
		private System.Windows.Forms.TabPage tabCriteria;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.ImageList ilReports;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.TextBox tbPDCritName;
		private System.Windows.Forms.ListBox lbPDResults;
		private System.Windows.Forms.Button btnPDSearch;
		private System.Windows.Forms.Button btnPDShowPlayer;
		private System.Windows.Forms.TextBox tbPDCritLocation;
		private System.Windows.Forms.TextBox tbPDCritNotes;
		private System.Windows.Forms.Button btnPDSelect;
		private System.Windows.Forms.Button btnPDClear;
		private System.Windows.Forms.GroupBox gbPDDetails;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.TextBox tbPDPlayerLocation;
		private System.Windows.Forms.TextBox tbPDPlayerName;
		private System.Windows.Forms.TextBox tbPDPlayerNotes;
		private System.Windows.Forms.Button btnPDSave;
		private System.Windows.Forms.TextBox tbPDPlayerID;
		private System.Windows.Forms.MenuItem mitmFile;
		private System.Windows.Forms.MenuItem mitmImportFile;
		private System.Windows.Forms.MenuItem mitmImportEmail;
		private System.Windows.Forms.MenuItem mitmExit;
		private System.Windows.Forms.MenuItem mitmTools;
		private System.Windows.Forms.MenuItem mitmReports;
		private System.Windows.Forms.MenuItem mitmOptions;
		private System.Windows.Forms.MenuItem mitmConfiguration;
		private System.Windows.Forms.MenuItem mitmClearDatabase;
		private System.Windows.Forms.MenuItem mitmHelp;
		private System.Windows.Forms.MenuItem mitmUserGuide;
		private System.Windows.Forms.MenuItem mitmAbout;
		private System.Windows.Forms.MenuItem mitmLicencse;
		private System.Windows.Forms.MenuItem mitmUpdates;
		private System.Windows.Forms.Button btnGenerateReport;
		private System.Windows.Forms.Button btnClearCriteria;
		private System.Windows.Forms.DateTimePicker dtStartFrom;
		private System.Windows.Forms.CheckBox cbStartFrom;
		private System.Windows.Forms.CheckBox cbLimitYes;
		private System.Windows.Forms.CheckBox cbLimitNo;
		private System.Windows.Forms.CheckBox cbLimitPot;
		private System.Windows.Forms.CheckBox cbGameTypeHoldem;
		private System.Windows.Forms.CheckBox cbGameTypeOmaha;
		private System.Windows.Forms.CheckBox cbGameType7Stud;
		private System.Windows.Forms.CheckBox cbGameType7StudHiLo;
		private System.Windows.Forms.CheckBox cbTourTypeSnG;
		private System.Windows.Forms.CheckBox cbTourTypeMulti;
		private System.Windows.Forms.CheckBox cbTourTypeHeadUp;
		private System.Windows.Forms.TextBox tbPlayerNumFrom;
		private System.Windows.Forms.TextBox tbPlayerNumTo;
		private System.Windows.Forms.TextBox tbBlindAmtFrom;
		private System.Windows.Forms.TextBox tbBlindAmtTo;
		private System.Windows.Forms.Button btnPosClear;
		private System.Windows.Forms.Button btnPosSelect;
		private System.Windows.Forms.CheckBox cbIncludeRingGames;
		private System.Windows.Forms.CheckBox cbIncludeTourGames;
		private System.Windows.Forms.TextBox tbHandID;
		private System.Windows.Forms.TextBox tbTourID;
		private System.Windows.Forms.RadioButton rbResultsOn5;
		private System.Windows.Forms.RadioButton rbResultsOn4;
		private System.Windows.Forms.RadioButton rbResultsOn3;
		private System.Windows.Forms.RadioButton rbResultsOn2;
		private System.Windows.Forms.RadioButton rbResultsOn1;
		private System.Windows.Forms.DateTimePicker dtStartTo;
		private System.Windows.Forms.CheckBox cbStartTo;
		private System.Windows.Forms.ListBox lbCombos;
		private System.Windows.Forms.Button btnComboRemove;
		private System.Windows.Forms.Button btnComboClear;
		private System.Windows.Forms.Button btnComboAdd;
		private System.Windows.Forms.ComboBox cbCard1;
		private System.Windows.Forms.ListBox lbPositions;
		private System.Windows.Forms.ComboBox cbCard4;
		private System.Windows.Forms.ComboBox cbCard3;
		private System.Windows.Forms.ComboBox cbCard2;
		private System.Windows.Forms.Label lblCritPlayers;
		private System.Windows.Forms.Label lblCritDateRange;
		private System.Windows.Forms.Label lblCritTourType;
		private System.Windows.Forms.Label lblCritPlayersNum;
		private System.Windows.Forms.Label lblCritBlindAmout;
		private System.Windows.Forms.Label lblCritPosition;
		private System.Windows.Forms.Label lblCritHandCombos;
		private System.Windows.Forms.Label lblCritHandID;
		private System.Windows.Forms.Label lblCritTourID;
		private System.Windows.Forms.Label lblCritLimitType;
		private System.Windows.Forms.Label lblCritGameType;
		private System.Windows.Forms.Label lblCritInclHands;
		private System.Windows.Forms.CheckBox cbGameTypeOmahaHiLo;
		private System.Windows.Forms.ComboBox cbPokerSite;
		private System.ComponentModel.IContainer components;
		private System.Data.DataSet dsBankRoll;
		private System.Data.DataColumn dataColumn1;
		private System.Data.DataView dvBankRoll;
		private System.Windows.Forms.DataGrid dgBankRoll;
		private System.Windows.Forms.Button btnBRDelete;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dtpBRStartDt;
		private System.Windows.Forms.DateTimePicker dtpBREndDt;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbBRStartTimeHour;
		private System.Windows.Forms.ComboBox cbBRStartTimeMin;
		private System.Windows.Forms.ComboBox cbBRStartTimeAP;
		private System.Windows.Forms.ComboBox cbBREndTimeAP;
		private System.Windows.Forms.ComboBox cbBREndTimeMin;
		private System.Windows.Forms.ComboBox cbBREndTimeHour;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox tbBRNetAmt;
		private System.Windows.Forms.TextBox tbBRNetTime;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.ComboBox cbBRPokerType;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.ComboBox cbBRGameType;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.ComboBox cbBRBetType;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.TextBox tbBRGroupingAmt;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.ComboBox cbBRSite;
		private System.Windows.Forms.TabControl tabReportTabs;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TextBox tbBRTotal;
		private System.Data.DataColumn dataColumn2;
		private System.Data.DataTable tblTable;
		private System.Windows.Forms.RadioButton rbResultsOnCSV;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private string[] m_positionCriteria;
		private System.Windows.Forms.ComboBox cbCardSuiting;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cbSortBy;
		private string m_currentReportFile;
		private AxSHDocVw.AxWebBrowser axWebBrowser1;
		private System.Windows.Forms.TextBox tbSQLDebug;
		private AxSHDocVw.AxWebBrowser axWebBrowser2;
		private AxSHDocVw.AxWebBrowser axWebBrowser3;
		private AxSHDocVw.AxWebBrowser axWebBrowser4;
		private AxSHDocVw.AxWebBrowser axWebBrowser5;
		private System.Windows.Forms.TabPage tabDebug;
		private System.Data.DataColumn dataColumn3;
		private System.Data.DataColumn dataColumn4;
		private System.Data.DataColumn dataColumn5;
		private System.Data.DataColumn dataColumn6;
		private System.Data.DataColumn dataColumn7;
		private System.Data.DataColumn dataColumn8;
		private System.Data.DataColumn dataColumn9;
		private System.Data.DataColumn dataColumn10;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.Button btnBRAdd;
		private System.Windows.Forms.Button btnBRSave;
		private Hashtable m_orderOptions;
		private System.Windows.Forms.Button btnBRClear;
		private int m_BRCurrentRowID;

		/// <summary>
		/// Construcutor for the main form.
		/// </summary>
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();			

			m_positionCriteria = new string[8];

			DisplayHTMLPage("Templates\\StartUp.htm");
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.mitmFile = new System.Windows.Forms.MenuItem();
			this.mitmImportFile = new System.Windows.Forms.MenuItem();
			this.mitmImportEmail = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.mitmExit = new System.Windows.Forms.MenuItem();
			this.mitmTools = new System.Windows.Forms.MenuItem();
			this.mitmReports = new System.Windows.Forms.MenuItem();
			this.mitmPlayerData = new System.Windows.Forms.MenuItem();
			this.mitmBankRoll = new System.Windows.Forms.MenuItem();
			this.mitmQueryDatabase = new System.Windows.Forms.MenuItem();
			this.mitmOptions = new System.Windows.Forms.MenuItem();
			this.mitmConfiguration = new System.Windows.Forms.MenuItem();
			this.mitmClearDatabase = new System.Windows.Forms.MenuItem();
			this.mitmHelp = new System.Windows.Forms.MenuItem();
			this.mitmUserGuide = new System.Windows.Forms.MenuItem();
			this.mitmAbout = new System.Windows.Forms.MenuItem();
			this.mitmLicencse = new System.Windows.Forms.MenuItem();
			this.mitmUpdates = new System.Windows.Forms.MenuItem();
			this.lblCritPlayers = new System.Windows.Forms.Label();
			this.btnPlayerAdd = new System.Windows.Forms.Button();
			this.lbPlayers = new System.Windows.Forms.ListBox();
			this.btnPlayerRemove = new System.Windows.Forms.Button();
			this.btnPlayersClear = new System.Windows.Forms.Button();
			this.lblCritDateRange = new System.Windows.Forms.Label();
			this.cbReportType = new System.Windows.Forms.ComboBox();
			this.dtStartFrom = new System.Windows.Forms.DateTimePicker();
			this.cbStartFrom = new System.Windows.Forms.CheckBox();
			this.cbLimitYes = new System.Windows.Forms.CheckBox();
			this.cbLimitNo = new System.Windows.Forms.CheckBox();
			this.cbLimitPot = new System.Windows.Forms.CheckBox();
			this.cbGameTypeHoldem = new System.Windows.Forms.CheckBox();
			this.cbGameTypeOmaha = new System.Windows.Forms.CheckBox();
			this.cbGameTypeOmahaHiLo = new System.Windows.Forms.CheckBox();
			this.cbGameType7Stud = new System.Windows.Forms.CheckBox();
			this.cbGameType7StudHiLo = new System.Windows.Forms.CheckBox();
			this.lblCritTourType = new System.Windows.Forms.Label();
			this.cbTourTypeSnG = new System.Windows.Forms.CheckBox();
			this.cbTourTypeMulti = new System.Windows.Forms.CheckBox();
			this.cbTourTypeHeadUp = new System.Windows.Forms.CheckBox();
			this.lblCritPlayersNum = new System.Windows.Forms.Label();
			this.tbPlayerNumFrom = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.tbPlayerNumTo = new System.Windows.Forms.TextBox();
			this.lblCritBlindAmout = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.tbBlindAmtFrom = new System.Windows.Forms.TextBox();
			this.tbBlindAmtTo = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.btnGenerateReport = new System.Windows.Forms.Button();
			this.lblCritPosition = new System.Windows.Forms.Label();
			this.btnPosClear = new System.Windows.Forms.Button();
			this.btnPosSelect = new System.Windows.Forms.Button();
			this.cbPlayer = new System.Windows.Forms.ComboBox();
			this.axWebBrowser1 = new AxSHDocVw.AxWebBrowser();
			this.dbConnection = new Npgsql.NpgsqlConnection();
			this.webClient = new System.Net.WebClient();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabReports = new System.Windows.Forms.TabPage();
			this.tabReportTabs = new System.Windows.Forms.TabControl();
			this.tabCriteria = new System.Windows.Forms.TabPage();
			this.cbSortBy = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.cbCardSuiting = new System.Windows.Forms.ComboBox();
			this.rbResultsOnCSV = new System.Windows.Forms.RadioButton();
			this.lblCritInclHands = new System.Windows.Forms.Label();
			this.lbPositions = new System.Windows.Forms.ListBox();
			this.cbCard4 = new System.Windows.Forms.ComboBox();
			this.cbCard3 = new System.Windows.Forms.ComboBox();
			this.cbCard2 = new System.Windows.Forms.ComboBox();
			this.cbCard1 = new System.Windows.Forms.ComboBox();
			this.btnComboRemove = new System.Windows.Forms.Button();
			this.btnComboClear = new System.Windows.Forms.Button();
			this.btnComboAdd = new System.Windows.Forms.Button();
			this.lbCombos = new System.Windows.Forms.ListBox();
			this.lblCritHandCombos = new System.Windows.Forms.Label();
			this.dtStartTo = new System.Windows.Forms.DateTimePicker();
			this.cbIncludeRingGames = new System.Windows.Forms.CheckBox();
			this.cbIncludeTourGames = new System.Windows.Forms.CheckBox();
			this.tbHandID = new System.Windows.Forms.TextBox();
			this.lblCritHandID = new System.Windows.Forms.Label();
			this.tbTourID = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.cbPokerSite = new System.Windows.Forms.ComboBox();
			this.lblCritTourID = new System.Windows.Forms.Label();
			this.btnClearCriteria = new System.Windows.Forms.Button();
			this.rbResultsOn5 = new System.Windows.Forms.RadioButton();
			this.rbResultsOn4 = new System.Windows.Forms.RadioButton();
			this.rbResultsOn3 = new System.Windows.Forms.RadioButton();
			this.rbResultsOn2 = new System.Windows.Forms.RadioButton();
			this.rbResultsOn1 = new System.Windows.Forms.RadioButton();
			this.label17 = new System.Windows.Forms.Label();
			this.lblCritLimitType = new System.Windows.Forms.Label();
			this.lblCritGameType = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.cbStartTo = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.axWebBrowser2 = new AxSHDocVw.AxWebBrowser();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.axWebBrowser3 = new AxSHDocVw.AxWebBrowser();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.axWebBrowser4 = new AxSHDocVw.AxWebBrowser();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.axWebBrowser5 = new AxSHDocVw.AxWebBrowser();
			this.tabDebug = new System.Windows.Forms.TabPage();
			this.tbSQLDebug = new System.Windows.Forms.TextBox();
			this.ilReports = new System.Windows.Forms.ImageList(this.components);
			this.label14 = new System.Windows.Forms.Label();
			this.tabBankRoll = new System.Windows.Forms.TabPage();
			this.btnBRClear = new System.Windows.Forms.Button();
			this.btnBRSave = new System.Windows.Forms.Button();
			this.tbBRTotal = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.cbBRSite = new System.Windows.Forms.ComboBox();
			this.tbBRGroupingAmt = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.cbBRBetType = new System.Windows.Forms.ComboBox();
			this.label31 = new System.Windows.Forms.Label();
			this.cbBRGameType = new System.Windows.Forms.ComboBox();
			this.label20 = new System.Windows.Forms.Label();
			this.cbBRPokerType = new System.Windows.Forms.ComboBox();
			this.tbBRNetTime = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.tbBRNetAmt = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.cbBREndTimeAP = new System.Windows.Forms.ComboBox();
			this.cbBREndTimeMin = new System.Windows.Forms.ComboBox();
			this.cbBREndTimeHour = new System.Windows.Forms.ComboBox();
			this.cbBRStartTimeAP = new System.Windows.Forms.ComboBox();
			this.cbBRStartTimeMin = new System.Windows.Forms.ComboBox();
			this.cbBRStartTimeHour = new System.Windows.Forms.ComboBox();
			this.dtpBREndDt = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.dtpBRStartDt = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBRAdd = new System.Windows.Forms.Button();
			this.btnBRDelete = new System.Windows.Forms.Button();
			this.dgBankRoll = new System.Windows.Forms.DataGrid();
			this.dvBankRoll = new System.Data.DataView();
			this.tblTable = new System.Data.DataTable();
			this.dataColumn1 = new System.Data.DataColumn();
			this.dataColumn2 = new System.Data.DataColumn();
			this.dataColumn3 = new System.Data.DataColumn();
			this.dataColumn4 = new System.Data.DataColumn();
			this.dataColumn5 = new System.Data.DataColumn();
			this.dataColumn6 = new System.Data.DataColumn();
			this.dataColumn7 = new System.Data.DataColumn();
			this.dataColumn8 = new System.Data.DataColumn();
			this.dataColumn9 = new System.Data.DataColumn();
			this.dataColumn10 = new System.Data.DataColumn();
			this.tabPlayerData = new System.Windows.Forms.TabPage();
			this.gbPDDetails = new System.Windows.Forms.GroupBox();
			this.tbPDPlayerID = new System.Windows.Forms.TextBox();
			this.btnPDSave = new System.Windows.Forms.Button();
			this.tbPDPlayerNotes = new System.Windows.Forms.TextBox();
			this.tbPDPlayerLocation = new System.Windows.Forms.TextBox();
			this.tbPDPlayerName = new System.Windows.Forms.TextBox();
			this.label30 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.btnPDClear = new System.Windows.Forms.Button();
			this.btnPDSelect = new System.Windows.Forms.Button();
			this.tbPDCritNotes = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.tbPDCritLocation = new System.Windows.Forms.TextBox();
			this.btnPDShowPlayer = new System.Windows.Forms.Button();
			this.btnPDSearch = new System.Windows.Forms.Button();
			this.label23 = new System.Windows.Forms.Label();
			this.lbPDResults = new System.Windows.Forms.ListBox();
			this.tbPDCritName = new System.Windows.Forms.TextBox();
			this.tabQueryDatabase = new System.Windows.Forms.TabPage();
			this.dgSQLResults = new System.Windows.Forms.DataGrid();
			this.btnRunSQL = new System.Windows.Forms.Button();
			this.label22 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label21 = new System.Windows.Forms.Label();
			this.tbSQLStatement = new System.Windows.Forms.TextBox();
			this.ilMain = new System.Windows.Forms.ImageList(this.components);
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.dsBankRoll = new System.Data.DataSet();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).BeginInit();
			this.tabMain.SuspendLayout();
			this.tabReports.SuspendLayout();
			this.tabReportTabs.SuspendLayout();
			this.tabCriteria.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser2)).BeginInit();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser3)).BeginInit();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser4)).BeginInit();
			this.tabPage5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser5)).BeginInit();
			this.tabDebug.SuspendLayout();
			this.tabBankRoll.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgBankRoll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dvBankRoll)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tblTable)).BeginInit();
			this.tabPlayerData.SuspendLayout();
			this.gbPDDetails.SuspendLayout();
			this.tabQueryDatabase.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgSQLResults)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsBankRoll)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mitmFile,
																					 this.mitmTools,
																					 this.mitmOptions,
																					 this.mitmHelp});
			// 
			// mitmFile
			// 
			this.mitmFile.Index = 0;
			this.mitmFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mitmImportFile,
																					 this.mitmImportEmail,
																					 this.menuItem17,
																					 this.mitmExit});
			this.mitmFile.Text = "File";
			// 
			// mitmImportFile
			// 
			this.mitmImportFile.Index = 0;
			this.mitmImportFile.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
			this.mitmImportFile.Text = "Import From File(s)";
			this.mitmImportFile.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// mitmImportEmail
			// 
			this.mitmImportEmail.Index = 1;
			this.mitmImportEmail.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
			this.mitmImportEmail.Text = "Import From E-Mail";
			this.mitmImportEmail.Click += new System.EventHandler(this.mitmImportEmail_Click);
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 2;
			this.menuItem17.Text = "-";
			// 
			// mitmExit
			// 
			this.mitmExit.Index = 3;
			this.mitmExit.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
			this.mitmExit.Text = "Exit";
			this.mitmExit.Click += new System.EventHandler(this.mitmExit_Click);
			// 
			// mitmTools
			// 
			this.mitmTools.Index = 1;
			this.mitmTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mitmReports,
																					  this.mitmBankRoll,
																					  this.mitmPlayerData,
																					  this.mitmQueryDatabase});
			this.mitmTools.Text = "Tools";
			// 
			// mitmReports
			// 
			this.mitmReports.Index = 0;
			this.mitmReports.Shortcut = System.Windows.Forms.Shortcut.CtrlR;
			this.mitmReports.Text = "Reports";
			this.mitmReports.Click += new System.EventHandler(this.mitmRepotrs_Click);
			// 
			// mitmPlayerData
			// 
			this.mitmPlayerData.Index = 2;
			this.mitmPlayerData.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
			this.mitmPlayerData.Text = "Player Data";
			this.mitmPlayerData.Click += new System.EventHandler(this.mitmPlayerData_Click);
			// 
			// mitmBankRoll
			// 
			this.mitmBankRoll.Index = 1;
			this.mitmBankRoll.Shortcut = System.Windows.Forms.Shortcut.CtrlB;
			this.mitmBankRoll.Text = "Bank Roll";
			this.mitmBankRoll.Click += new System.EventHandler(this.mitmBankRoll_Click);
			// 
			// mitmQueryDatabase
			// 
			this.mitmQueryDatabase.Index = 3;
			this.mitmQueryDatabase.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
			this.mitmQueryDatabase.Text = "Query Database";
			this.mitmQueryDatabase.Click += new System.EventHandler(this.mitmQueryDatabase_Click);
			// 
			// mitmOptions
			// 
			this.mitmOptions.Index = 2;
			this.mitmOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mitmConfiguration,
																						this.mitmClearDatabase});
			this.mitmOptions.Text = "Options";
			// 
			// mitmConfiguration
			// 
			this.mitmConfiguration.Index = 0;
			this.mitmConfiguration.Text = "Configuration";
			this.mitmConfiguration.Click += new System.EventHandler(this.mitmConfiguration_Click);
			// 
			// mitmClearDatabase
			// 
			this.mitmClearDatabase.Index = 1;
			this.mitmClearDatabase.Text = "Clear Database";
			this.mitmClearDatabase.Click += new System.EventHandler(this.mitmClearDatabase_Click);
			// 
			// mitmHelp
			// 
			this.mitmHelp.Index = 3;
			this.mitmHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mitmUserGuide,
																					 this.mitmAbout,
																					 this.mitmLicencse,
																					 this.mitmUpdates});
			this.mitmHelp.Text = "Help";
			// 
			// mitmUserGuide
			// 
			this.mitmUserGuide.Index = 0;
			this.mitmUserGuide.Shortcut = System.Windows.Forms.Shortcut.F1;
			this.mitmUserGuide.Text = "User Guide";
			this.mitmUserGuide.Click += new System.EventHandler(this.mitmUserGuide_Click);
			// 
			// mitmAbout
			// 
			this.mitmAbout.Index = 1;
			this.mitmAbout.Text = "About";
			this.mitmAbout.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// mitmLicencse
			// 
			this.mitmLicencse.Index = 2;
			this.mitmLicencse.Text = "License";
			this.mitmLicencse.Click += new System.EventHandler(this.menuItem13_Click);
			// 
			// mitmUpdates
			// 
			this.mitmUpdates.Index = 3;
			this.mitmUpdates.Shortcut = System.Windows.Forms.Shortcut.CtrlU;
			this.mitmUpdates.Text = "Check for Updates";
			this.mitmUpdates.Click += new System.EventHandler(this.menuItem14_Click);
			// 
			// lblCritPlayers
			// 
			this.lblCritPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritPlayers.Location = new System.Drawing.Point(16, 56);
			this.lblCritPlayers.Name = "lblCritPlayers";
			this.lblCritPlayers.Size = new System.Drawing.Size(132, 16);
			this.lblCritPlayers.TabIndex = 3;
			this.lblCritPlayers.Text = "For players:";
			// 
			// btnPlayerAdd
			// 
			this.btnPlayerAdd.Enabled = false;
			this.btnPlayerAdd.Location = new System.Drawing.Point(272, 80);
			this.btnPlayerAdd.Name = "btnPlayerAdd";
			this.btnPlayerAdd.Size = new System.Drawing.Size(64, 20);
			this.btnPlayerAdd.TabIndex = 4;
			this.btnPlayerAdd.Text = "Add";
			this.btnPlayerAdd.Click += new System.EventHandler(this.btnPlayerAdd_Click);
			// 
			// lbPlayers
			// 
			this.lbPlayers.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.lbPlayers.Enabled = false;
			this.lbPlayers.Location = new System.Drawing.Point(16, 76);
			this.lbPlayers.Name = "lbPlayers";
			this.lbPlayers.ScrollAlwaysVisible = true;
			this.lbPlayers.Size = new System.Drawing.Size(252, 82);
			this.lbPlayers.Sorted = true;
			this.lbPlayers.TabIndex = 5;
			// 
			// btnPlayerRemove
			// 
			this.btnPlayerRemove.Enabled = false;
			this.btnPlayerRemove.Location = new System.Drawing.Point(272, 108);
			this.btnPlayerRemove.Name = "btnPlayerRemove";
			this.btnPlayerRemove.Size = new System.Drawing.Size(64, 20);
			this.btnPlayerRemove.TabIndex = 6;
			this.btnPlayerRemove.Text = "Remove";
			this.btnPlayerRemove.Click += new System.EventHandler(this.btnPlayerRemove_Click);
			// 
			// btnPlayersClear
			// 
			this.btnPlayersClear.Enabled = false;
			this.btnPlayersClear.Location = new System.Drawing.Point(272, 136);
			this.btnPlayersClear.Name = "btnPlayersClear";
			this.btnPlayersClear.Size = new System.Drawing.Size(64, 20);
			this.btnPlayersClear.TabIndex = 7;
			this.btnPlayersClear.Text = "Clear";
			this.btnPlayersClear.Click += new System.EventHandler(this.btnPlayersClear_Click);
			// 
			// lblCritDateRange
			// 
			this.lblCritDateRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritDateRange.Location = new System.Drawing.Point(8, 276);
			this.lblCritDateRange.Name = "lblCritDateRange";
			this.lblCritDateRange.Size = new System.Drawing.Size(108, 16);
			this.lblCritDateRange.TabIndex = 8;
			this.lblCritDateRange.Text = "Date range:";
			this.lblCritDateRange.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cbReportType
			// 
			this.cbReportType.Location = new System.Drawing.Point(116, 16);
			this.cbReportType.Name = "cbReportType";
			this.cbReportType.Size = new System.Drawing.Size(184, 21);
			this.cbReportType.Sorted = true;
			this.cbReportType.TabIndex = 9;
			this.cbReportType.Click += new System.EventHandler(this.cbReportType_Click);
			this.cbReportType.SelectedIndexChanged += new System.EventHandler(this.cbReportType_SelectedIndexChanged);
			// 
			// dtStartFrom
			// 
			this.dtStartFrom.Enabled = false;
			this.dtStartFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtStartFrom.Location = new System.Drawing.Point(184, 272);
			this.dtStartFrom.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
			this.dtStartFrom.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
			this.dtStartFrom.Name = "dtStartFrom";
			this.dtStartFrom.Size = new System.Drawing.Size(84, 20);
			this.dtStartFrom.TabIndex = 10;
			// 
			// cbStartFrom
			// 
			this.cbStartFrom.Enabled = false;
			this.cbStartFrom.Location = new System.Drawing.Point(128, 272);
			this.cbStartFrom.Name = "cbStartFrom";
			this.cbStartFrom.Size = new System.Drawing.Size(52, 24);
			this.cbStartFrom.TabIndex = 11;
			this.cbStartFrom.Text = "from";
			this.cbStartFrom.CheckedChanged += new System.EventHandler(this.cbStartFrom_CheckedChanged);
			// 
			// cbLimitYes
			// 
			this.cbLimitYes.Checked = true;
			this.cbLimitYes.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbLimitYes.Enabled = false;
			this.cbLimitYes.Location = new System.Drawing.Point(364, 324);
			this.cbLimitYes.Name = "cbLimitYes";
			this.cbLimitYes.Size = new System.Drawing.Size(48, 24);
			this.cbLimitYes.TabIndex = 15;
			this.cbLimitYes.Text = "Limit";
			// 
			// cbLimitNo
			// 
			this.cbLimitNo.Checked = true;
			this.cbLimitNo.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbLimitNo.Enabled = false;
			this.cbLimitNo.Location = new System.Drawing.Point(364, 350);
			this.cbLimitNo.Name = "cbLimitNo";
			this.cbLimitNo.Size = new System.Drawing.Size(71, 15);
			this.cbLimitNo.TabIndex = 16;
			this.cbLimitNo.Text = "No Limit";
			// 
			// cbLimitPot
			// 
			this.cbLimitPot.Checked = true;
			this.cbLimitPot.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbLimitPot.Enabled = false;
			this.cbLimitPot.Location = new System.Drawing.Point(364, 368);
			this.cbLimitPot.Name = "cbLimitPot";
			this.cbLimitPot.Size = new System.Drawing.Size(72, 20);
			this.cbLimitPot.TabIndex = 17;
			this.cbLimitPot.Text = "Pot Limit";
			// 
			// cbGameTypeHoldem
			// 
			this.cbGameTypeHoldem.Checked = true;
			this.cbGameTypeHoldem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbGameTypeHoldem.Enabled = false;
			this.cbGameTypeHoldem.Location = new System.Drawing.Point(560, 324);
			this.cbGameTypeHoldem.Name = "cbGameTypeHoldem";
			this.cbGameTypeHoldem.Size = new System.Drawing.Size(64, 24);
			this.cbGameTypeHoldem.TabIndex = 19;
			this.cbGameTypeHoldem.Text = "Hold\'em";
			// 
			// cbGameTypeOmaha
			// 
			this.cbGameTypeOmaha.Checked = true;
			this.cbGameTypeOmaha.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbGameTypeOmaha.Enabled = false;
			this.cbGameTypeOmaha.Location = new System.Drawing.Point(560, 344);
			this.cbGameTypeOmaha.Name = "cbGameTypeOmaha";
			this.cbGameTypeOmaha.Size = new System.Drawing.Size(72, 24);
			this.cbGameTypeOmaha.TabIndex = 20;
			this.cbGameTypeOmaha.Text = "Omaha";
			// 
			// cbGameTypeOmahaHiLo
			// 
			this.cbGameTypeOmahaHiLo.Checked = true;
			this.cbGameTypeOmahaHiLo.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbGameTypeOmahaHiLo.Enabled = false;
			this.cbGameTypeOmahaHiLo.Location = new System.Drawing.Point(560, 364);
			this.cbGameTypeOmahaHiLo.Name = "cbGameTypeOmahaHiLo";
			this.cbGameTypeOmahaHiLo.TabIndex = 21;
			this.cbGameTypeOmahaHiLo.Text = "Omaha Hi/Lo";
			// 
			// cbGameType7Stud
			// 
			this.cbGameType7Stud.Checked = true;
			this.cbGameType7Stud.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbGameType7Stud.Enabled = false;
			this.cbGameType7Stud.Location = new System.Drawing.Point(656, 324);
			this.cbGameType7Stud.Name = "cbGameType7Stud";
			this.cbGameType7Stud.TabIndex = 22;
			this.cbGameType7Stud.Text = "7 Card Stud";
			// 
			// cbGameType7StudHiLo
			// 
			this.cbGameType7StudHiLo.Checked = true;
			this.cbGameType7StudHiLo.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbGameType7StudHiLo.Enabled = false;
			this.cbGameType7StudHiLo.Location = new System.Drawing.Point(656, 344);
			this.cbGameType7StudHiLo.Name = "cbGameType7StudHiLo";
			this.cbGameType7StudHiLo.Size = new System.Drawing.Size(116, 24);
			this.cbGameType7StudHiLo.TabIndex = 23;
			this.cbGameType7StudHiLo.Text = "7 Card Stud Hi/Lo";
			// 
			// lblCritTourType
			// 
			this.lblCritTourType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritTourType.Location = new System.Drawing.Point(8, 328);
			this.lblCritTourType.Name = "lblCritTourType";
			this.lblCritTourType.Size = new System.Drawing.Size(108, 16);
			this.lblCritTourType.TabIndex = 24;
			this.lblCritTourType.Text = "Tournament types:";
			this.lblCritTourType.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cbTourTypeSnG
			// 
			this.cbTourTypeSnG.Checked = true;
			this.cbTourTypeSnG.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbTourTypeSnG.Enabled = false;
			this.cbTourTypeSnG.Location = new System.Drawing.Point(128, 344);
			this.cbTourTypeSnG.Name = "cbTourTypeSnG";
			this.cbTourTypeSnG.TabIndex = 25;
			this.cbTourTypeSnG.Text = "Sit and Go";
			// 
			// cbTourTypeMulti
			// 
			this.cbTourTypeMulti.Checked = true;
			this.cbTourTypeMulti.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbTourTypeMulti.Enabled = false;
			this.cbTourTypeMulti.Location = new System.Drawing.Point(128, 324);
			this.cbTourTypeMulti.Name = "cbTourTypeMulti";
			this.cbTourTypeMulti.TabIndex = 26;
			this.cbTourTypeMulti.Text = "Multi";
			// 
			// cbTourTypeHeadUp
			// 
			this.cbTourTypeHeadUp.Checked = true;
			this.cbTourTypeHeadUp.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbTourTypeHeadUp.Enabled = false;
			this.cbTourTypeHeadUp.Location = new System.Drawing.Point(128, 364);
			this.cbTourTypeHeadUp.Name = "cbTourTypeHeadUp";
			this.cbTourTypeHeadUp.Size = new System.Drawing.Size(92, 24);
			this.cbTourTypeHeadUp.TabIndex = 27;
			this.cbTourTypeHeadUp.Text = "Head Up";
			// 
			// lblCritPlayersNum
			// 
			this.lblCritPlayersNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritPlayersNum.Location = new System.Drawing.Point(8, 172);
			this.lblCritPlayersNum.Name = "lblCritPlayersNum";
			this.lblCritPlayersNum.Size = new System.Drawing.Size(112, 20);
			this.lblCritPlayersNum.TabIndex = 28;
			this.lblCritPlayersNum.Text = "Number of players:";
			this.lblCritPlayersNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbPlayerNumFrom
			// 
			this.tbPlayerNumFrom.Enabled = false;
			this.tbPlayerNumFrom.Location = new System.Drawing.Point(184, 168);
			this.tbPlayerNumFrom.Name = "tbPlayerNumFrom";
			this.tbPlayerNumFrom.Size = new System.Drawing.Size(48, 20);
			this.tbPlayerNumFrom.TabIndex = 29;
			this.tbPlayerNumFrom.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(128, 172);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 16);
			this.label8.TabIndex = 30;
			this.label8.Text = "between";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(128, 196);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(28, 16);
			this.label9.TabIndex = 31;
			this.label9.Text = "and";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbPlayerNumTo
			// 
			this.tbPlayerNumTo.Enabled = false;
			this.tbPlayerNumTo.Location = new System.Drawing.Point(184, 192);
			this.tbPlayerNumTo.Name = "tbPlayerNumTo";
			this.tbPlayerNumTo.Size = new System.Drawing.Size(48, 20);
			this.tbPlayerNumTo.TabIndex = 32;
			this.tbPlayerNumTo.Text = "";
			// 
			// lblCritBlindAmout
			// 
			this.lblCritBlindAmout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritBlindAmout.Location = new System.Drawing.Point(8, 224);
			this.lblCritBlindAmout.Name = "lblCritBlindAmout";
			this.lblCritBlindAmout.Size = new System.Drawing.Size(112, 16);
			this.lblCritBlindAmout.TabIndex = 33;
			this.lblCritBlindAmout.Text = "Big blind amout:";
			this.lblCritBlindAmout.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(128, 224);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(48, 16);
			this.label11.TabIndex = 34;
			this.label11.Text = "between";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbBlindAmtFrom
			// 
			this.tbBlindAmtFrom.Enabled = false;
			this.tbBlindAmtFrom.Location = new System.Drawing.Point(184, 220);
			this.tbBlindAmtFrom.Name = "tbBlindAmtFrom";
			this.tbBlindAmtFrom.Size = new System.Drawing.Size(48, 20);
			this.tbBlindAmtFrom.TabIndex = 35;
			this.tbBlindAmtFrom.Text = "";
			// 
			// tbBlindAmtTo
			// 
			this.tbBlindAmtTo.Enabled = false;
			this.tbBlindAmtTo.Location = new System.Drawing.Point(184, 244);
			this.tbBlindAmtTo.Name = "tbBlindAmtTo";
			this.tbBlindAmtTo.Size = new System.Drawing.Size(48, 20);
			this.tbBlindAmtTo.TabIndex = 36;
			this.tbBlindAmtTo.Text = "";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(128, 244);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(28, 16);
			this.label12.TabIndex = 37;
			this.label12.Text = "and";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnGenerateReport
			// 
			this.btnGenerateReport.Location = new System.Drawing.Point(248, 396);
			this.btnGenerateReport.Name = "btnGenerateReport";
			this.btnGenerateReport.Size = new System.Drawing.Size(264, 24);
			this.btnGenerateReport.TabIndex = 38;
			this.btnGenerateReport.Text = "Generate Report";
			this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
			// 
			// lblCritPosition
			// 
			this.lblCritPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritPosition.Location = new System.Drawing.Point(312, 184);
			this.lblCritPosition.Name = "lblCritPosition";
			this.lblCritPosition.Size = new System.Drawing.Size(64, 16);
			this.lblCritPosition.TabIndex = 39;
			this.lblCritPosition.Text = "Position:";
			this.lblCritPosition.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnPosClear
			// 
			this.btnPosClear.Enabled = false;
			this.btnPosClear.Location = new System.Drawing.Point(408, 180);
			this.btnPosClear.Name = "btnPosClear";
			this.btnPosClear.Size = new System.Drawing.Size(48, 20);
			this.btnPosClear.TabIndex = 40;
			this.btnPosClear.Text = "Clear";
			this.btnPosClear.Click += new System.EventHandler(this.btnPosClear_Click);
			// 
			// btnPosSelect
			// 
			this.btnPosSelect.Enabled = false;
			this.btnPosSelect.Location = new System.Drawing.Point(384, 180);
			this.btnPosSelect.Name = "btnPosSelect";
			this.btnPosSelect.Size = new System.Drawing.Size(24, 20);
			this.btnPosSelect.TabIndex = 41;
			this.btnPosSelect.Text = "...";
			this.btnPosSelect.Click += new System.EventHandler(this.btnPosSelect_Click);
			// 
			// cbPlayer
			// 
			this.cbPlayer.Enabled = false;
			this.cbPlayer.Location = new System.Drawing.Point(152, 52);
			this.cbPlayer.Name = "cbPlayer";
			this.cbPlayer.Size = new System.Drawing.Size(184, 21);
			this.cbPlayer.TabIndex = 43;
			this.cbPlayer.Click += new System.EventHandler(this.cbPlayer_Click);
			// 
			// axWebBrowser1
			// 
			this.axWebBrowser1.ContainingControl = this;
			this.axWebBrowser1.Enabled = true;
			this.axWebBrowser1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser1.OcxState")));
			this.axWebBrowser1.Size = new System.Drawing.Size(780, 464);
			this.axWebBrowser1.TabIndex = 44;
			// 
			// dbConnection
			// 
			this.dbConnection.ConnectionString = "SERVER=127.0.0.1;PORT=5454;USER ID=postgres;PASSWORD=spadestat;DATABASE=spadestat" +
				";";
			// 
			// webClient
			// 
			this.webClient.BaseAddress = "";
			this.webClient.Credentials = null;
			this.webClient.Headers = ((System.Net.WebHeaderCollection)(resources.GetObject("webClient.Headers")));
			this.webClient.QueryString = ((System.Collections.Specialized.NameValueCollection)(resources.GetObject("webClient.QueryString")));
			// 
			// tabMain
			// 
			this.tabMain.Controls.AddRange(new System.Windows.Forms.Control[] {
																				  this.tabReports,
																				  this.tabBankRoll,
																				  this.tabPlayerData,
																				  this.tabQueryDatabase});
			this.tabMain.ImageList = this.ilMain;
			this.tabMain.ItemSize = new System.Drawing.Size(71, 20);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(792, 524);
			this.tabMain.TabIndex = 46;
			// 
			// tabReports
			// 
			this.tabReports.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.tabReportTabs,
																					 this.label14});
			this.tabReports.ImageIndex = 0;
			this.tabReports.Location = new System.Drawing.Point(4, 24);
			this.tabReports.Name = "tabReports";
			this.tabReports.Size = new System.Drawing.Size(784, 496);
			this.tabReports.TabIndex = 0;
			this.tabReports.Text = "Reports";
			// 
			// tabReportTabs
			// 
			this.tabReportTabs.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.tabCriteria,
																						this.tabPage1,
																						this.tabPage2,
																						this.tabPage3,
																						this.tabPage4,
																						this.tabPage5,
																						this.tabDebug});
			this.tabReportTabs.ImageList = this.ilReports;
			this.tabReportTabs.Location = new System.Drawing.Point(0, 28);
			this.tabReportTabs.Name = "tabReportTabs";
			this.tabReportTabs.SelectedIndex = 0;
			this.tabReportTabs.Size = new System.Drawing.Size(792, 472);
			this.tabReportTabs.TabIndex = 45;
			// 
			// tabCriteria
			// 
			this.tabCriteria.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.cbSortBy,
																					  this.label6,
																					  this.cbCardSuiting,
																					  this.rbResultsOnCSV,
																					  this.lblCritInclHands,
																					  this.lbPositions,
																					  this.cbCard4,
																					  this.cbCard3,
																					  this.cbCard2,
																					  this.cbCard1,
																					  this.btnComboRemove,
																					  this.btnComboClear,
																					  this.btnComboAdd,
																					  this.lbCombos,
																					  this.lblCritHandCombos,
																					  this.dtStartTo,
																					  this.cbIncludeRingGames,
																					  this.cbIncludeTourGames,
																					  this.tbHandID,
																					  this.lblCritHandID,
																					  this.tbTourID,
																					  this.label19,
																					  this.cbPokerSite,
																					  this.lblCritTourID,
																					  this.btnClearCriteria,
																					  this.rbResultsOn5,
																					  this.rbResultsOn4,
																					  this.rbResultsOn3,
																					  this.rbResultsOn2,
																					  this.rbResultsOn1,
																					  this.label17,
																					  this.lblCritLimitType,
																					  this.lblCritGameType,
																					  this.label1,
																					  this.cbTourTypeHeadUp,
																					  this.label5,
																					  this.tbPlayerNumTo,
																					  this.cbGameTypeHoldem,
																					  this.tbPlayerNumFrom,
																					  this.lblCritPlayersNum,
																					  this.lblCritPlayers,
																					  this.lbPlayers,
																					  this.cbStartFrom,
																					  this.label8,
																					  this.btnPlayerRemove,
																					  this.cbGameType7StudHiLo,
																					  this.cbTourTypeMulti,
																					  this.cbGameTypeOmahaHiLo,
																					  this.cbLimitYes,
																					  this.cbTourTypeSnG,
																					  this.cbGameType7Stud,
																					  this.cbLimitNo,
																					  this.cbGameTypeOmaha,
																					  this.cbStartTo,
																					  this.cbReportType,
																					  this.label9,
																					  this.cbLimitPot,
																					  this.cbPlayer,
																					  this.lblCritDateRange,
																					  this.btnPlayersClear,
																					  this.dtStartFrom,
																					  this.lblCritTourType,
																					  this.label4,
																					  this.lblCritPosition,
																					  this.label11,
																					  this.btnPosClear,
																					  this.btnGenerateReport,
																					  this.lblCritBlindAmout,
																					  this.label12,
																					  this.btnPosSelect,
																					  this.tbBlindAmtFrom,
																					  this.tbBlindAmtTo,
																					  this.btnPlayerAdd});
			this.tabCriteria.ImageIndex = 0;
			this.tabCriteria.Location = new System.Drawing.Point(4, 23);
			this.tabCriteria.Name = "tabCriteria";
			this.tabCriteria.Size = new System.Drawing.Size(784, 445);
			this.tabCriteria.TabIndex = 0;
			this.tabCriteria.Text = "Criteria";
			// 
			// cbSortBy
			// 
			this.cbSortBy.Enabled = false;
			this.cbSortBy.Location = new System.Drawing.Point(64, 396);
			this.cbSortBy.Name = "cbSortBy";
			this.cbSortBy.Size = new System.Drawing.Size(148, 21);
			this.cbSortBy.Sorted = true;
			this.cbSortBy.TabIndex = 79;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 400);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(52, 16);
			this.label6.TabIndex = 78;
			this.label6.Text = "Sort by:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cbCardSuiting
			// 
			this.cbCardSuiting.Enabled = false;
			this.cbCardSuiting.Items.AddRange(new object[] {
															   "-",
															   "SS",
															   "DS",
															   "OS"});
			this.cbCardSuiting.Location = new System.Drawing.Point(720, 180);
			this.cbCardSuiting.Name = "cbCardSuiting";
			this.cbCardSuiting.Size = new System.Drawing.Size(40, 21);
			this.cbCardSuiting.TabIndex = 77;
			this.cbCardSuiting.Text = "-";
			// 
			// rbResultsOnCSV
			// 
			this.rbResultsOnCSV.Location = new System.Drawing.Point(468, 420);
			this.rbResultsOnCSV.Name = "rbResultsOnCSV";
			this.rbResultsOnCSV.Size = new System.Drawing.Size(100, 24);
			this.rbResultsOnCSV.TabIndex = 76;
			this.rbResultsOnCSV.Text = "Excel / CSV";
			// 
			// lblCritInclHands
			// 
			this.lblCritInclHands.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritInclHands.Location = new System.Drawing.Point(412, 112);
			this.lblCritInclHands.Name = "lblCritInclHands";
			this.lblCritInclHands.Size = new System.Drawing.Size(108, 16);
			this.lblCritInclHands.TabIndex = 75;
			this.lblCritInclHands.Text = "Included Hands:";
			this.lblCritInclHands.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbPositions
			// 
			this.lbPositions.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.lbPositions.Enabled = false;
			this.lbPositions.Location = new System.Drawing.Point(308, 204);
			this.lbPositions.Name = "lbPositions";
			this.lbPositions.ScrollAlwaysVisible = true;
			this.lbPositions.Size = new System.Drawing.Size(152, 108);
			this.lbPositions.Sorted = true;
			this.lbPositions.TabIndex = 74;
			// 
			// cbCard4
			// 
			this.cbCard4.Enabled = false;
			this.cbCard4.Items.AddRange(new object[] {
														 "-",
														 "x",
														 "2",
														 "3",
														 "4",
														 "5",
														 "6",
														 "7",
														 "8",
														 "9",
														 "10",
														 "J",
														 "Q",
														 "K",
														 "A"});
			this.cbCard4.Location = new System.Drawing.Point(680, 180);
			this.cbCard4.Name = "cbCard4";
			this.cbCard4.Size = new System.Drawing.Size(36, 21);
			this.cbCard4.TabIndex = 73;
			this.cbCard4.Text = "-";
			// 
			// cbCard3
			// 
			this.cbCard3.Enabled = false;
			this.cbCard3.Items.AddRange(new object[] {
														 "-",
														 "x",
														 "2",
														 "3",
														 "4",
														 "5",
														 "6",
														 "7",
														 "8",
														 "9",
														 "10",
														 "J",
														 "Q",
														 "K",
														 "A"});
			this.cbCard3.Location = new System.Drawing.Point(644, 180);
			this.cbCard3.Name = "cbCard3";
			this.cbCard3.Size = new System.Drawing.Size(36, 21);
			this.cbCard3.TabIndex = 72;
			this.cbCard3.Text = "-";
			// 
			// cbCard2
			// 
			this.cbCard2.Enabled = false;
			this.cbCard2.Items.AddRange(new object[] {
														 "-",
														 "x",
														 "2",
														 "3",
														 "4",
														 "5",
														 "6",
														 "7",
														 "8",
														 "9",
														 "10",
														 "J",
														 "Q",
														 "K",
														 "A"});
			this.cbCard2.Location = new System.Drawing.Point(608, 180);
			this.cbCard2.Name = "cbCard2";
			this.cbCard2.Size = new System.Drawing.Size(36, 21);
			this.cbCard2.TabIndex = 71;
			this.cbCard2.Text = "-";
			// 
			// cbCard1
			// 
			this.cbCard1.Enabled = false;
			this.cbCard1.Items.AddRange(new object[] {
														 "-",
														 "2",
														 "3",
														 "4",
														 "5",
														 "6",
														 "7",
														 "8",
														 "9",
														 "10",
														 "J",
														 "Q",
														 "K",
														 "A"});
			this.cbCard1.Location = new System.Drawing.Point(572, 180);
			this.cbCard1.Name = "cbCard1";
			this.cbCard1.Size = new System.Drawing.Size(36, 21);
			this.cbCard1.TabIndex = 70;
			this.cbCard1.Text = "-";
			// 
			// btnComboRemove
			// 
			this.btnComboRemove.Enabled = false;
			this.btnComboRemove.Location = new System.Drawing.Point(692, 232);
			this.btnComboRemove.Name = "btnComboRemove";
			this.btnComboRemove.Size = new System.Drawing.Size(64, 20);
			this.btnComboRemove.TabIndex = 68;
			this.btnComboRemove.Text = "Remove";
			this.btnComboRemove.Click += new System.EventHandler(this.btnComboRemove_Click);
			// 
			// btnComboClear
			// 
			this.btnComboClear.Enabled = false;
			this.btnComboClear.Location = new System.Drawing.Point(692, 260);
			this.btnComboClear.Name = "btnComboClear";
			this.btnComboClear.Size = new System.Drawing.Size(64, 20);
			this.btnComboClear.TabIndex = 69;
			this.btnComboClear.Text = "Clear";
			this.btnComboClear.Click += new System.EventHandler(this.btnComboClear_Click);
			// 
			// btnComboAdd
			// 
			this.btnComboAdd.Enabled = false;
			this.btnComboAdd.Location = new System.Drawing.Point(692, 204);
			this.btnComboAdd.Name = "btnComboAdd";
			this.btnComboAdd.Size = new System.Drawing.Size(64, 20);
			this.btnComboAdd.TabIndex = 67;
			this.btnComboAdd.Text = "Add";
			this.btnComboAdd.Click += new System.EventHandler(this.btnComboAdd_Click);
			// 
			// lbCombos
			// 
			this.lbCombos.BackColor = System.Drawing.SystemColors.InactiveBorder;
			this.lbCombos.Enabled = false;
			this.lbCombos.Location = new System.Drawing.Point(508, 204);
			this.lbCombos.Name = "lbCombos";
			this.lbCombos.ScrollAlwaysVisible = true;
			this.lbCombos.Size = new System.Drawing.Size(180, 108);
			this.lbCombos.Sorted = true;
			this.lbCombos.TabIndex = 66;
			// 
			// lblCritHandCombos
			// 
			this.lblCritHandCombos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritHandCombos.Location = new System.Drawing.Point(512, 184);
			this.lblCritHandCombos.Name = "lblCritHandCombos";
			this.lblCritHandCombos.Size = new System.Drawing.Size(56, 16);
			this.lblCritHandCombos.TabIndex = 63;
			this.lblCritHandCombos.Text = "Cambos:";
			this.lblCritHandCombos.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// dtStartTo
			// 
			this.dtStartTo.Enabled = false;
			this.dtStartTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtStartTo.Location = new System.Drawing.Point(184, 296);
			this.dtStartTo.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
			this.dtStartTo.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
			this.dtStartTo.Name = "dtStartTo";
			this.dtStartTo.Size = new System.Drawing.Size(84, 20);
			this.dtStartTo.TabIndex = 62;
			// 
			// cbIncludeRingGames
			// 
			this.cbIncludeRingGames.Checked = true;
			this.cbIncludeRingGames.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbIncludeRingGames.Enabled = false;
			this.cbIncludeRingGames.Location = new System.Drawing.Point(524, 132);
			this.cbIncludeRingGames.Name = "cbIncludeRingGames";
			this.cbIncludeRingGames.Size = new System.Drawing.Size(136, 24);
			this.cbIncludeRingGames.TabIndex = 61;
			this.cbIncludeRingGames.Text = "Include ring-games";
			// 
			// cbIncludeTourGames
			// 
			this.cbIncludeTourGames.Checked = true;
			this.cbIncludeTourGames.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbIncludeTourGames.Enabled = false;
			this.cbIncludeTourGames.Location = new System.Drawing.Point(524, 108);
			this.cbIncludeTourGames.Name = "cbIncludeTourGames";
			this.cbIncludeTourGames.Size = new System.Drawing.Size(136, 24);
			this.cbIncludeTourGames.TabIndex = 60;
			this.cbIncludeTourGames.Text = "Include tournaments";
			// 
			// tbHandID
			// 
			this.tbHandID.Enabled = false;
			this.tbHandID.Location = new System.Drawing.Point(524, 80);
			this.tbHandID.Name = "tbHandID";
			this.tbHandID.Size = new System.Drawing.Size(140, 20);
			this.tbHandID.TabIndex = 59;
			this.tbHandID.Text = "";
			// 
			// lblCritHandID
			// 
			this.lblCritHandID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritHandID.Location = new System.Drawing.Point(412, 84);
			this.lblCritHandID.Name = "lblCritHandID";
			this.lblCritHandID.Size = new System.Drawing.Size(108, 16);
			this.lblCritHandID.TabIndex = 58;
			this.lblCritHandID.Text = "Hand ID:";
			this.lblCritHandID.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbTourID
			// 
			this.tbTourID.Enabled = false;
			this.tbTourID.Location = new System.Drawing.Point(524, 52);
			this.tbTourID.Name = "tbTourID";
			this.tbTourID.Size = new System.Drawing.Size(140, 20);
			this.tbTourID.TabIndex = 57;
			this.tbTourID.Text = "";
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label19.Location = new System.Drawing.Point(412, 24);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(108, 16);
			this.label19.TabIndex = 56;
			this.label19.Text = "Poker site:";
			this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cbPokerSite
			// 
			this.cbPokerSite.Enabled = false;
			this.cbPokerSite.Items.AddRange(new object[] {
															 "PokerStars.com"});
			this.cbPokerSite.Location = new System.Drawing.Point(524, 20);
			this.cbPokerSite.Name = "cbPokerSite";
			this.cbPokerSite.Size = new System.Drawing.Size(140, 21);
			this.cbPokerSite.Sorted = true;
			this.cbPokerSite.TabIndex = 55;
			this.cbPokerSite.Text = "PokerStars.com";
			// 
			// lblCritTourID
			// 
			this.lblCritTourID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritTourID.Location = new System.Drawing.Point(412, 56);
			this.lblCritTourID.Name = "lblCritTourID";
			this.lblCritTourID.Size = new System.Drawing.Size(108, 16);
			this.lblCritTourID.TabIndex = 54;
			this.lblCritTourID.Text = "Tournament ID:";
			this.lblCritTourID.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnClearCriteria
			// 
			this.btnClearCriteria.Location = new System.Drawing.Point(640, 412);
			this.btnClearCriteria.Name = "btnClearCriteria";
			this.btnClearCriteria.Size = new System.Drawing.Size(124, 24);
			this.btnClearCriteria.TabIndex = 53;
			this.btnClearCriteria.Text = "Clear Criteria";
			this.btnClearCriteria.Click += new System.EventHandler(this.btnClearCriteria_Click);
			// 
			// rbResultsOn5
			// 
			this.rbResultsOn5.Location = new System.Drawing.Point(428, 420);
			this.rbResultsOn5.Name = "rbResultsOn5";
			this.rbResultsOn5.Size = new System.Drawing.Size(32, 24);
			this.rbResultsOn5.TabIndex = 52;
			this.rbResultsOn5.Text = "5";
			// 
			// rbResultsOn4
			// 
			this.rbResultsOn4.Location = new System.Drawing.Point(396, 420);
			this.rbResultsOn4.Name = "rbResultsOn4";
			this.rbResultsOn4.Size = new System.Drawing.Size(32, 24);
			this.rbResultsOn4.TabIndex = 51;
			this.rbResultsOn4.Text = "4";
			// 
			// rbResultsOn3
			// 
			this.rbResultsOn3.Location = new System.Drawing.Point(364, 420);
			this.rbResultsOn3.Name = "rbResultsOn3";
			this.rbResultsOn3.Size = new System.Drawing.Size(36, 24);
			this.rbResultsOn3.TabIndex = 50;
			this.rbResultsOn3.Text = "3";
			// 
			// rbResultsOn2
			// 
			this.rbResultsOn2.Location = new System.Drawing.Point(332, 420);
			this.rbResultsOn2.Name = "rbResultsOn2";
			this.rbResultsOn2.Size = new System.Drawing.Size(32, 24);
			this.rbResultsOn2.TabIndex = 49;
			this.rbResultsOn2.Text = "2";
			// 
			// rbResultsOn1
			// 
			this.rbResultsOn1.Checked = true;
			this.rbResultsOn1.Location = new System.Drawing.Point(300, 420);
			this.rbResultsOn1.Name = "rbResultsOn1";
			this.rbResultsOn1.Size = new System.Drawing.Size(32, 24);
			this.rbResultsOn1.TabIndex = 48;
			this.rbResultsOn1.TabStop = true;
			this.rbResultsOn1.Text = "1";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(192, 424);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(108, 20);
			this.label17.TabIndex = 47;
			this.label17.Text = "... on results page #:";
			// 
			// lblCritLimitType
			// 
			this.lblCritLimitType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritLimitType.Location = new System.Drawing.Point(272, 328);
			this.lblCritLimitType.Name = "lblCritLimitType";
			this.lblCritLimitType.Size = new System.Drawing.Size(84, 16);
			this.lblCritLimitType.TabIndex = 46;
			this.lblCritLimitType.Text = "Limit types:";
			this.lblCritLimitType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblCritGameType
			// 
			this.lblCritGameType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCritGameType.Location = new System.Drawing.Point(464, 328);
			this.lblCritGameType.Name = "lblCritGameType";
			this.lblCritGameType.Size = new System.Drawing.Size(88, 16);
			this.lblCritGameType.TabIndex = 45;
			this.lblCritGameType.Text = "Game types:";
			this.lblCritGameType.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(20, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 16);
			this.label1.TabIndex = 44;
			this.label1.Text = "Type of report:";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(200, -24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 24);
			this.label5.TabIndex = 18;
			this.label5.Text = "Game types:";
			// 
			// cbStartTo
			// 
			this.cbStartTo.Enabled = false;
			this.cbStartTo.Location = new System.Drawing.Point(128, 296);
			this.cbStartTo.Name = "cbStartTo";
			this.cbStartTo.Size = new System.Drawing.Size(52, 20);
			this.cbStartTo.TabIndex = 12;
			this.cbStartTo.Text = "to";
			this.cbStartTo.CheckedChanged += new System.EventHandler(this.cbStartTo_CheckedChanged);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(296, -24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 24);
			this.label4.TabIndex = 14;
			this.label4.Text = "Bet types:";
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.axWebBrowser1});
			this.tabPage1.ImageIndex = 1;
			this.tabPage1.Location = new System.Drawing.Point(4, 23);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(784, 445);
			this.tabPage1.TabIndex = 1;
			this.tabPage1.Text = "Results 1";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.axWebBrowser2});
			this.tabPage2.ImageIndex = 1;
			this.tabPage2.Location = new System.Drawing.Point(4, 23);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(784, 445);
			this.tabPage2.TabIndex = 2;
			this.tabPage2.Text = "Results 2";
			// 
			// axWebBrowser2
			// 
			this.axWebBrowser2.ContainingControl = this;
			this.axWebBrowser2.Enabled = true;
			this.axWebBrowser2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser2.OcxState")));
			this.axWebBrowser2.Size = new System.Drawing.Size(780, 464);
			this.axWebBrowser2.TabIndex = 45;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.axWebBrowser3});
			this.tabPage3.ImageIndex = 1;
			this.tabPage3.Location = new System.Drawing.Point(4, 23);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(784, 445);
			this.tabPage3.TabIndex = 3;
			this.tabPage3.Text = "Results 3";
			// 
			// axWebBrowser3
			// 
			this.axWebBrowser3.ContainingControl = this;
			this.axWebBrowser3.Enabled = true;
			this.axWebBrowser3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser3.OcxState")));
			this.axWebBrowser3.Size = new System.Drawing.Size(780, 464);
			this.axWebBrowser3.TabIndex = 45;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.axWebBrowser4});
			this.tabPage4.ImageIndex = 1;
			this.tabPage4.Location = new System.Drawing.Point(4, 23);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(784, 445);
			this.tabPage4.TabIndex = 4;
			this.tabPage4.Text = "Results 4";
			// 
			// axWebBrowser4
			// 
			this.axWebBrowser4.ContainingControl = this;
			this.axWebBrowser4.Enabled = true;
			this.axWebBrowser4.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser4.OcxState")));
			this.axWebBrowser4.Size = new System.Drawing.Size(780, 464);
			this.axWebBrowser4.TabIndex = 45;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.axWebBrowser5});
			this.tabPage5.ImageIndex = 1;
			this.tabPage5.Location = new System.Drawing.Point(4, 23);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(784, 445);
			this.tabPage5.TabIndex = 5;
			this.tabPage5.Text = "Results 5";
			// 
			// axWebBrowser5
			// 
			this.axWebBrowser5.ContainingControl = this;
			this.axWebBrowser5.Enabled = true;
			this.axWebBrowser5.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser5.OcxState")));
			this.axWebBrowser5.Size = new System.Drawing.Size(780, 464);
			this.axWebBrowser5.TabIndex = 45;
			// 
			// tabDebug
			// 
			this.tabDebug.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.tbSQLDebug});
			this.tabDebug.Location = new System.Drawing.Point(4, 23);
			this.tabDebug.Name = "tabDebug";
			this.tabDebug.Size = new System.Drawing.Size(784, 445);
			this.tabDebug.TabIndex = 6;
			this.tabDebug.Text = "SQL DEBUG";
			// 
			// tbSQLDebug
			// 
			this.tbSQLDebug.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(128)));
			this.tbSQLDebug.Location = new System.Drawing.Point(8, 8);
			this.tbSQLDebug.Multiline = true;
			this.tbSQLDebug.Name = "tbSQLDebug";
			this.tbSQLDebug.Size = new System.Drawing.Size(764, 428);
			this.tbSQLDebug.TabIndex = 1;
			this.tbSQLDebug.Text = "";
			// 
			// ilReports
			// 
			this.ilReports.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.ilReports.ImageSize = new System.Drawing.Size(16, 16);
			this.ilReports.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilReports.ImageStream")));
			this.ilReports.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(0, 12);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(524, 16);
			this.label14.TabIndex = 46;
			this.label14.Text = "Specify type of report, report criteria and which results page you would like to " +
				"display the report on:";
			// 
			// tabBankRoll
			// 
			this.tabBankRoll.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.btnBRClear,
																					  this.btnBRSave,
																					  this.tbBRTotal,
																					  this.label35,
																					  this.label34,
																					  this.cbBRSite,
																					  this.tbBRGroupingAmt,
																					  this.label33,
																					  this.label32,
																					  this.cbBRBetType,
																					  this.label31,
																					  this.cbBRGameType,
																					  this.label20,
																					  this.cbBRPokerType,
																					  this.tbBRNetTime,
																					  this.label18,
																					  this.tbBRNetAmt,
																					  this.label16,
																					  this.label15,
																					  this.label13,
																					  this.label10,
																					  this.cbBREndTimeAP,
																					  this.cbBREndTimeMin,
																					  this.cbBREndTimeHour,
																					  this.cbBRStartTimeAP,
																					  this.cbBRStartTimeMin,
																					  this.cbBRStartTimeHour,
																					  this.dtpBREndDt,
																					  this.label3,
																					  this.dtpBRStartDt,
																					  this.label2,
																					  this.btnBRAdd,
																					  this.btnBRDelete,
																					  this.dgBankRoll});
			this.tabBankRoll.ImageIndex = 1;
			this.tabBankRoll.Location = new System.Drawing.Point(4, 24);
			this.tabBankRoll.Name = "tabBankRoll";
			this.tabBankRoll.Size = new System.Drawing.Size(784, 496);
			this.tabBankRoll.TabIndex = 2;
			this.tabBankRoll.Text = "Bank Roll";
			// 
			// btnBRClear
			// 
			this.btnBRClear.Location = new System.Drawing.Point(696, 80);
			this.btnBRClear.Name = "btnBRClear";
			this.btnBRClear.Size = new System.Drawing.Size(68, 20);
			this.btnBRClear.TabIndex = 42;
			this.btnBRClear.Text = "Clear";
			this.btnBRClear.Click += new System.EventHandler(this.btnBRClear_Click);
			// 
			// btnBRSave
			// 
			this.btnBRSave.Enabled = false;
			this.btnBRSave.Location = new System.Drawing.Point(624, 80);
			this.btnBRSave.Name = "btnBRSave";
			this.btnBRSave.Size = new System.Drawing.Size(68, 20);
			this.btnBRSave.TabIndex = 41;
			this.btnBRSave.Text = "Save";
			this.btnBRSave.Click += new System.EventHandler(this.btnBRSave_Click);
			// 
			// tbBRTotal
			// 
			this.tbBRTotal.BackColor = System.Drawing.SystemColors.Info;
			this.tbBRTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbBRTotal.Location = new System.Drawing.Point(108, 4);
			this.tbBRTotal.Name = "tbBRTotal";
			this.tbBRTotal.ReadOnly = true;
			this.tbBRTotal.Size = new System.Drawing.Size(92, 20);
			this.tbBRTotal.TabIndex = 40;
			this.tbBRTotal.TabStop = false;
			this.tbBRTotal.Text = "";
			this.tbBRTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label35
			// 
			this.label35.Location = new System.Drawing.Point(4, 4);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(104, 20);
			this.label35.TabIndex = 39;
			this.label35.Text = "Current Total ($):";
			this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label34
			// 
			this.label34.Location = new System.Drawing.Point(324, 84);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(68, 16);
			this.label34.TabIndex = 38;
			this.label34.Text = "Site:";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbBRSite
			// 
			this.cbBRSite.Items.AddRange(new object[] {
														  "24h Poker (B2B)",
														  "Absolute Poker",
														  "Action Poker (Tiger Gaming) ",
														  "America\'s Card Room",
														  "Bodog Poker",
														  "Bugsy\'s Club",
														  "Caribbean Sun",
														  "Classic Poker",
														  "Empire Poker",
														  "Gaming Club (Prima)",
														  "Golden Palace (Apex)",
														  "Interpoker",
														  "Intertops Poker",
														  "Jet Set Poker",
														  "Littlewoods Poker",
														  "Pacific Poker",
														  "Paradise Poker",
														  "Party Poker",
														  "Poker NOW",
														  "Poker Plex",
														  "Poker Room",
														  "Poker Stars",
														  "Poker World",
														  "Ritz Club London",
														  "True Poker",
														  "Ultimate Bet",
														  "Victor Chandler (Apex)",
														  "Victoria\'s Poker",
														  "WPex",
														  "William Hill"});
			this.cbBRSite.Location = new System.Drawing.Point(396, 80);
			this.cbBRSite.Name = "cbBRSite";
			this.cbBRSite.Size = new System.Drawing.Size(128, 21);
			this.cbBRSite.TabIndex = 11;
			// 
			// tbBRGroupingAmt
			// 
			this.tbBRGroupingAmt.Location = new System.Drawing.Point(672, 56);
			this.tbBRGroupingAmt.Name = "tbBRGroupingAmt";
			this.tbBRGroupingAmt.Size = new System.Drawing.Size(92, 20);
			this.tbBRGroupingAmt.TabIndex = 14;
			this.tbBRGroupingAmt.Text = "";
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(568, 60);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(104, 16);
			this.label33.TabIndex = 35;
			this.label33.Text = "Grouping ($):";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(324, 60);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(68, 16);
			this.label32.TabIndex = 34;
			this.label32.Text = "Bet type:";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbBRBetType
			// 
			this.cbBRBetType.Items.AddRange(new object[] {
															 "Limit",
															 "No Limit",
															 "Pot Limit"});
			this.cbBRBetType.Location = new System.Drawing.Point(396, 56);
			this.cbBRBetType.Name = "cbBRBetType";
			this.cbBRBetType.Size = new System.Drawing.Size(128, 21);
			this.cbBRBetType.TabIndex = 10;
			// 
			// label31
			// 
			this.label31.Location = new System.Drawing.Point(324, 36);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(68, 16);
			this.label31.TabIndex = 32;
			this.label31.Text = "Game type:";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbBRGameType
			// 
			this.cbBRGameType.Items.AddRange(new object[] {
															  "Hold\'em",
															  "Omaha",
															  "Omaha Hi/Lo",
															  "7-Card Stud",
															  "7-Card Stud Hi/Lo"});
			this.cbBRGameType.Location = new System.Drawing.Point(396, 32);
			this.cbBRGameType.Name = "cbBRGameType";
			this.cbBRGameType.Size = new System.Drawing.Size(128, 21);
			this.cbBRGameType.TabIndex = 9;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(324, 12);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(68, 16);
			this.label20.TabIndex = 30;
			this.label20.Text = "Poker type:";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbBRPokerType
			// 
			this.cbBRPokerType.Items.AddRange(new object[] {
															   "Sit and Go tournament",
															   "Multitable tournament",
															   "Heads-up tournament",
															   "Ring game",
															   "Bonus money",
															   "Other"});
			this.cbBRPokerType.Location = new System.Drawing.Point(396, 8);
			this.cbBRPokerType.Name = "cbBRPokerType";
			this.cbBRPokerType.Size = new System.Drawing.Size(128, 21);
			this.cbBRPokerType.TabIndex = 8;
			// 
			// tbBRNetTime
			// 
			this.tbBRNetTime.Location = new System.Drawing.Point(672, 8);
			this.tbBRNetTime.Name = "tbBRNetTime";
			this.tbBRNetTime.Size = new System.Drawing.Size(92, 20);
			this.tbBRNetTime.TabIndex = 12;
			this.tbBRNetTime.Text = "";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(568, 12);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(104, 16);
			this.label18.TabIndex = 27;
			this.label18.Text = "Net Change (min):";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbBRNetAmt
			// 
			this.tbBRNetAmt.Location = new System.Drawing.Point(672, 32);
			this.tbBRNetAmt.Name = "tbBRNetAmt";
			this.tbBRNetAmt.Size = new System.Drawing.Size(92, 20);
			this.tbBRNetAmt.TabIndex = 13;
			this.tbBRNetAmt.Text = "";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(568, 36);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(104, 16);
			this.label16.TabIndex = 25;
			this.label16.Text = "Net Change ($):";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(248, 36);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(48, 16);
			this.label15.TabIndex = 24;
			this.label15.Text = "am / pm";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(212, 36);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(32, 16);
			this.label13.TabIndex = 23;
			this.label13.Text = "min";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(176, 36);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(32, 16);
			this.label10.TabIndex = 22;
			this.label10.Text = "hour";
			// 
			// cbBREndTimeAP
			// 
			this.cbBREndTimeAP.Items.AddRange(new object[] {
															   "AM",
															   "PM"});
			this.cbBREndTimeAP.Location = new System.Drawing.Point(248, 76);
			this.cbBREndTimeAP.Name = "cbBREndTimeAP";
			this.cbBREndTimeAP.Size = new System.Drawing.Size(36, 21);
			this.cbBREndTimeAP.TabIndex = 7;
			// 
			// cbBREndTimeMin
			// 
			this.cbBREndTimeMin.Items.AddRange(new object[] {
																"0",
																"1",
																"2",
																"3",
																"4",
																"5",
																"6",
																"7",
																"8",
																"9",
																"10",
																"11",
																"12",
																"13",
																"14",
																"15",
																"16",
																"17",
																"18",
																"19",
																"20",
																"21",
																"22",
																"23",
																"24",
																"25",
																"26",
																"27",
																"28",
																"29",
																"30",
																"31",
																"32",
																"33",
																"34",
																"35",
																"36",
																"37",
																"38",
																"39",
																"40",
																"41",
																"42",
																"43",
																"44",
																"45",
																"46",
																"47",
																"48",
																"49",
																"50",
																"51",
																"52",
																"53",
																"54",
																"55",
																"56",
																"57",
																"58",
																"59"});
			this.cbBREndTimeMin.Location = new System.Drawing.Point(212, 76);
			this.cbBREndTimeMin.Name = "cbBREndTimeMin";
			this.cbBREndTimeMin.Size = new System.Drawing.Size(36, 21);
			this.cbBREndTimeMin.TabIndex = 6;
			// 
			// cbBREndTimeHour
			// 
			this.cbBREndTimeHour.Items.AddRange(new object[] {
																 "1",
																 "2",
																 "3",
																 "4",
																 "5",
																 "6",
																 "7",
																 "8",
																 "9",
																 "10",
																 "11",
																 "12"});
			this.cbBREndTimeHour.Location = new System.Drawing.Point(176, 76);
			this.cbBREndTimeHour.Name = "cbBREndTimeHour";
			this.cbBREndTimeHour.Size = new System.Drawing.Size(36, 21);
			this.cbBREndTimeHour.TabIndex = 5;
			// 
			// cbBRStartTimeAP
			// 
			this.cbBRStartTimeAP.Items.AddRange(new object[] {
																 "AM",
																 "PM"});
			this.cbBRStartTimeAP.Location = new System.Drawing.Point(248, 52);
			this.cbBRStartTimeAP.Name = "cbBRStartTimeAP";
			this.cbBRStartTimeAP.Size = new System.Drawing.Size(36, 21);
			this.cbBRStartTimeAP.TabIndex = 3;
			// 
			// cbBRStartTimeMin
			// 
			this.cbBRStartTimeMin.Items.AddRange(new object[] {
																  "0",
																  "1",
																  "2",
																  "3",
																  "4",
																  "5",
																  "6",
																  "7",
																  "8",
																  "9",
																  "10",
																  "11",
																  "12",
																  "13",
																  "14",
																  "15",
																  "16",
																  "17",
																  "18",
																  "19",
																  "20",
																  "21",
																  "22",
																  "23",
																  "24",
																  "25",
																  "26",
																  "27",
																  "28",
																  "29",
																  "30",
																  "31",
																  "32",
																  "33",
																  "34",
																  "35",
																  "36",
																  "37",
																  "38",
																  "39",
																  "40",
																  "41",
																  "42",
																  "43",
																  "44",
																  "45",
																  "46",
																  "47",
																  "48",
																  "49",
																  "50",
																  "51",
																  "52",
																  "53",
																  "54",
																  "55",
																  "56",
																  "57",
																  "58",
																  "59"});
			this.cbBRStartTimeMin.Location = new System.Drawing.Point(212, 52);
			this.cbBRStartTimeMin.Name = "cbBRStartTimeMin";
			this.cbBRStartTimeMin.Size = new System.Drawing.Size(36, 21);
			this.cbBRStartTimeMin.TabIndex = 2;
			// 
			// cbBRStartTimeHour
			// 
			this.cbBRStartTimeHour.Items.AddRange(new object[] {
																   "1",
																   "2",
																   "3",
																   "4",
																   "5",
																   "6",
																   "7",
																   "8",
																   "9",
																   "10",
																   "11",
																   "12"});
			this.cbBRStartTimeHour.Location = new System.Drawing.Point(176, 52);
			this.cbBRStartTimeHour.Name = "cbBRStartTimeHour";
			this.cbBRStartTimeHour.Size = new System.Drawing.Size(36, 21);
			this.cbBRStartTimeHour.TabIndex = 1;
			// 
			// dtpBREndDt
			// 
			this.dtpBREndDt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpBREndDt.Location = new System.Drawing.Point(80, 76);
			this.dtpBREndDt.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
			this.dtpBREndDt.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
			this.dtpBREndDt.Name = "dtpBREndDt";
			this.dtpBREndDt.Size = new System.Drawing.Size(92, 20);
			this.dtpBREndDt.TabIndex = 4;
			this.dtpBREndDt.Value = new System.DateTime(2005, 3, 1, 0, 0, 0, 0);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 16);
			this.label3.TabIndex = 12;
			this.label3.Text = "Ended:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpBRStartDt
			// 
			this.dtpBRStartDt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpBRStartDt.Location = new System.Drawing.Point(80, 52);
			this.dtpBRStartDt.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
			this.dtpBRStartDt.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
			this.dtpBRStartDt.Name = "dtpBRStartDt";
			this.dtpBRStartDt.Size = new System.Drawing.Size(92, 20);
			this.dtpBRStartDt.TabIndex = 0;
			this.dtpBRStartDt.Value = new System.DateTime(2005, 3, 1, 0, 0, 0, 0);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "Started:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnBRAdd
			// 
			this.btnBRAdd.Location = new System.Drawing.Point(552, 80);
			this.btnBRAdd.Name = "btnBRAdd";
			this.btnBRAdd.Size = new System.Drawing.Size(68, 20);
			this.btnBRAdd.TabIndex = 15;
			this.btnBRAdd.Text = "Add";
			this.btnBRAdd.Click += new System.EventHandler(this.btnBRAdd_Click);
			// 
			// btnBRDelete
			// 
			this.btnBRDelete.Location = new System.Drawing.Point(8, 116);
			this.btnBRDelete.Name = "btnBRDelete";
			this.btnBRDelete.Size = new System.Drawing.Size(148, 20);
			this.btnBRDelete.TabIndex = 17;
			this.btnBRDelete.Text = "Delete Selected Record";
			this.btnBRDelete.Click += new System.EventHandler(this.btnBRDelete_Click);
			// 
			// dgBankRoll
			// 
			this.dgBankRoll.AllowDrop = true;
			this.dgBankRoll.AllowNavigation = false;
			this.dgBankRoll.AllowSorting = false;
			this.dgBankRoll.CaptionBackColor = System.Drawing.SystemColors.InactiveBorder;
			this.dgBankRoll.DataMember = "";
			this.dgBankRoll.DataSource = this.dvBankRoll;
			this.dgBankRoll.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgBankRoll.Location = new System.Drawing.Point(8, 116);
			this.dgBankRoll.Name = "dgBankRoll";
			this.dgBankRoll.ReadOnly = true;
			this.dgBankRoll.Size = new System.Drawing.Size(768, 372);
			this.dgBankRoll.TabIndex = 0;
			this.dgBankRoll.TabStop = false;
			this.dgBankRoll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgBankRoll_MouseUp);
			// 
			// dvBankRoll
			// 
			this.dvBankRoll.AllowEdit = false;
			this.dvBankRoll.AllowNew = false;
			this.dvBankRoll.Table = this.tblTable;
			// 
			// tblTable
			// 
			this.tblTable.Columns.AddRange(new System.Data.DataColumn[] {
																			this.dataColumn1,
																			this.dataColumn2,
																			this.dataColumn3,
																			this.dataColumn4,
																			this.dataColumn5,
																			this.dataColumn6,
																			this.dataColumn7,
																			this.dataColumn8,
																			this.dataColumn9,
																			this.dataColumn10});
			this.tblTable.Constraints.AddRange(new System.Data.Constraint[] {
																				new System.Data.UniqueConstraint("Constraint1", new string[] {
																																				 "bankrollid"}, true)});
			this.tblTable.MinimumCapacity = 1000;
			this.tblTable.PrimaryKey = new System.Data.DataColumn[] {
																		this.dataColumn1};
			this.tblTable.TableName = "Table";
			// 
			// dataColumn1
			// 
			this.dataColumn1.AllowDBNull = false;
			this.dataColumn1.Caption = "ID";
			this.dataColumn1.ColumnName = "bankrollid";
			this.dataColumn1.DataType = typeof(int);
			// 
			// dataColumn2
			// 
			this.dataColumn2.ColumnName = "startdt";
			this.dataColumn2.DataType = typeof(System.DateTime);
			// 
			// dataColumn3
			// 
			this.dataColumn3.ColumnName = "enddt";
			this.dataColumn3.DataType = typeof(System.DateTime);
			// 
			// dataColumn4
			// 
			this.dataColumn4.ColumnName = "netchangeamt";
			this.dataColumn4.DataType = typeof(System.Double);
			// 
			// dataColumn5
			// 
			this.dataColumn5.ColumnName = "nettimeamt";
			this.dataColumn5.DataType = typeof(System.Double);
			// 
			// dataColumn6
			// 
			this.dataColumn6.ColumnName = "typecd";
			// 
			// dataColumn7
			// 
			this.dataColumn7.ColumnName = "gametypcd";
			// 
			// dataColumn8
			// 
			this.dataColumn8.ColumnName = "bettypcd";
			// 
			// dataColumn9
			// 
			this.dataColumn9.ColumnName = "bbamt";
			this.dataColumn9.DataType = typeof(System.Double);
			// 
			// dataColumn10
			// 
			this.dataColumn10.ColumnName = "sitecd";
			// 
			// tabPlayerData
			// 
			this.tabPlayerData.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.gbPDDetails,
																						this.label28,
																						this.btnPDClear,
																						this.btnPDSelect,
																						this.tbPDCritNotes,
																						this.label27,
																						this.label26,
																						this.label25,
																						this.tbPDCritLocation,
																						this.btnPDShowPlayer,
																						this.btnPDSearch,
																						this.label23,
																						this.lbPDResults,
																						this.tbPDCritName});
			this.tabPlayerData.ImageIndex = 2;
			this.tabPlayerData.Location = new System.Drawing.Point(4, 24);
			this.tabPlayerData.Name = "tabPlayerData";
			this.tabPlayerData.Size = new System.Drawing.Size(784, 496);
			this.tabPlayerData.TabIndex = 1;
			this.tabPlayerData.Text = "Player Data";
			// 
			// gbPDDetails
			// 
			this.gbPDDetails.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tbPDPlayerID,
																					  this.btnPDSave,
																					  this.tbPDPlayerNotes,
																					  this.tbPDPlayerLocation,
																					  this.tbPDPlayerName,
																					  this.label30,
																					  this.label24,
																					  this.label29});
			this.gbPDDetails.Location = new System.Drawing.Point(28, 148);
			this.gbPDDetails.Name = "gbPDDetails";
			this.gbPDDetails.Size = new System.Drawing.Size(724, 348);
			this.gbPDDetails.TabIndex = 13;
			this.gbPDDetails.TabStop = false;
			this.gbPDDetails.Text = "Player data:";
			// 
			// tbPDPlayerID
			// 
			this.tbPDPlayerID.Enabled = false;
			this.tbPDPlayerID.Location = new System.Drawing.Point(444, 24);
			this.tbPDPlayerID.Name = "tbPDPlayerID";
			this.tbPDPlayerID.Size = new System.Drawing.Size(104, 20);
			this.tbPDPlayerID.TabIndex = 17;
			this.tbPDPlayerID.Text = "";
			this.tbPDPlayerID.Visible = false;
			// 
			// btnPDSave
			// 
			this.btnPDSave.Enabled = false;
			this.btnPDSave.Location = new System.Drawing.Point(108, 236);
			this.btnPDSave.Name = "btnPDSave";
			this.btnPDSave.Size = new System.Drawing.Size(144, 20);
			this.btnPDSave.TabIndex = 10;
			this.btnPDSave.Text = "Save";
			this.btnPDSave.Click += new System.EventHandler(this.btnPDSave_Click);
			// 
			// tbPDPlayerNotes
			// 
			this.tbPDPlayerNotes.Enabled = false;
			this.tbPDPlayerNotes.Location = new System.Drawing.Point(108, 72);
			this.tbPDPlayerNotes.Multiline = true;
			this.tbPDPlayerNotes.Name = "tbPDPlayerNotes";
			this.tbPDPlayerNotes.Size = new System.Drawing.Size(440, 160);
			this.tbPDPlayerNotes.TabIndex = 9;
			this.tbPDPlayerNotes.Text = "";
			// 
			// tbPDPlayerLocation
			// 
			this.tbPDPlayerLocation.Enabled = false;
			this.tbPDPlayerLocation.Location = new System.Drawing.Point(108, 48);
			this.tbPDPlayerLocation.Name = "tbPDPlayerLocation";
			this.tbPDPlayerLocation.Size = new System.Drawing.Size(192, 20);
			this.tbPDPlayerLocation.TabIndex = 8;
			this.tbPDPlayerLocation.Text = "";
			// 
			// tbPDPlayerName
			// 
			this.tbPDPlayerName.Enabled = false;
			this.tbPDPlayerName.Location = new System.Drawing.Point(108, 24);
			this.tbPDPlayerName.Name = "tbPDPlayerName";
			this.tbPDPlayerName.Size = new System.Drawing.Size(192, 20);
			this.tbPDPlayerName.TabIndex = 7;
			this.tbPDPlayerName.Text = "";
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(24, 28);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(80, 16);
			this.label30.TabIndex = 14;
			this.label30.Text = "Player name:";
			this.label30.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(24, 76);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(80, 16);
			this.label24.TabIndex = 16;
			this.label24.Text = "Notes:";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(24, 52);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(80, 16);
			this.label29.TabIndex = 15;
			this.label29.Text = "Location:";
			this.label29.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(300, 16);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(152, 16);
			this.label28.TabIndex = 12;
			this.label28.Text = "Search results:";
			// 
			// btnPDClear
			// 
			this.btnPDClear.Location = new System.Drawing.Point(196, 108);
			this.btnPDClear.Name = "btnPDClear";
			this.btnPDClear.Size = new System.Drawing.Size(88, 20);
			this.btnPDClear.TabIndex = 11;
			this.btnPDClear.Text = "Clear";
			this.btnPDClear.Click += new System.EventHandler(this.btnPDClear_Click);
			// 
			// btnPDSelect
			// 
			this.btnPDSelect.Enabled = false;
			this.btnPDSelect.Location = new System.Drawing.Point(608, 88);
			this.btnPDSelect.Name = "btnPDSelect";
			this.btnPDSelect.Size = new System.Drawing.Size(144, 20);
			this.btnPDSelect.TabIndex = 6;
			this.btnPDSelect.Text = "Select for report";
			this.btnPDSelect.Click += new System.EventHandler(this.btnPDSelect_Click);
			// 
			// tbPDCritNotes
			// 
			this.tbPDCritNotes.Location = new System.Drawing.Point(92, 84);
			this.tbPDCritNotes.Name = "tbPDCritNotes";
			this.tbPDCritNotes.Size = new System.Drawing.Size(192, 20);
			this.tbPDCritNotes.TabIndex = 2;
			this.tbPDCritNotes.Text = "";
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(92, 16);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(152, 16);
			this.label27.TabIndex = 8;
			this.label27.Text = "Player search criteria";
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(8, 88);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(80, 16);
			this.label26.TabIndex = 7;
			this.label26.Text = "Notes:";
			this.label26.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(8, 64);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(80, 16);
			this.label25.TabIndex = 6;
			this.label25.Text = "Location:";
			this.label25.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbPDCritLocation
			// 
			this.tbPDCritLocation.Location = new System.Drawing.Point(92, 60);
			this.tbPDCritLocation.Name = "tbPDCritLocation";
			this.tbPDCritLocation.Size = new System.Drawing.Size(192, 20);
			this.tbPDCritLocation.TabIndex = 1;
			this.tbPDCritLocation.Text = "";
			// 
			// btnPDShowPlayer
			// 
			this.btnPDShowPlayer.Enabled = false;
			this.btnPDShowPlayer.Location = new System.Drawing.Point(608, 56);
			this.btnPDShowPlayer.Name = "btnPDShowPlayer";
			this.btnPDShowPlayer.Size = new System.Drawing.Size(144, 20);
			this.btnPDShowPlayer.TabIndex = 5;
			this.btnPDShowPlayer.Text = "Show player data";
			this.btnPDShowPlayer.Click += new System.EventHandler(this.btnPDShowPlayer_Click);
			// 
			// btnPDSearch
			// 
			this.btnPDSearch.Location = new System.Drawing.Point(92, 108);
			this.btnPDSearch.Name = "btnPDSearch";
			this.btnPDSearch.Size = new System.Drawing.Size(88, 20);
			this.btnPDSearch.TabIndex = 3;
			this.btnPDSearch.Text = "Search";
			this.btnPDSearch.Click += new System.EventHandler(this.btnPDSearch_Click);
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(8, 40);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(80, 16);
			this.label23.TabIndex = 2;
			this.label23.Text = "Player name:";
			this.label23.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lbPDResults
			// 
			this.lbPDResults.Location = new System.Drawing.Point(300, 36);
			this.lbPDResults.Name = "lbPDResults";
			this.lbPDResults.Size = new System.Drawing.Size(300, 95);
			this.lbPDResults.TabIndex = 4;
			this.lbPDResults.DoubleClick += new System.EventHandler(this.lbPDResults_DoubleClick);
			// 
			// tbPDCritName
			// 
			this.tbPDCritName.Location = new System.Drawing.Point(92, 36);
			this.tbPDCritName.Name = "tbPDCritName";
			this.tbPDCritName.Size = new System.Drawing.Size(192, 20);
			this.tbPDCritName.TabIndex = 0;
			this.tbPDCritName.Text = "";
			// 
			// tabQueryDatabase
			// 
			this.tabQueryDatabase.Controls.AddRange(new System.Windows.Forms.Control[] {
																						   this.dgSQLResults,
																						   this.btnRunSQL,
																						   this.label22,
																						   this.linkLabel1,
																						   this.label21,
																						   this.tbSQLStatement});
			this.tabQueryDatabase.ImageIndex = 3;
			this.tabQueryDatabase.Location = new System.Drawing.Point(4, 24);
			this.tabQueryDatabase.Name = "tabQueryDatabase";
			this.tabQueryDatabase.Size = new System.Drawing.Size(784, 496);
			this.tabQueryDatabase.TabIndex = 3;
			this.tabQueryDatabase.Text = "Query Database";
			// 
			// dgSQLResults
			// 
			this.dgSQLResults.DataMember = "";
			this.dgSQLResults.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgSQLResults.Location = new System.Drawing.Point(12, 164);
			this.dgSQLResults.Name = "dgSQLResults";
			this.dgSQLResults.ReadOnly = true;
			this.dgSQLResults.Size = new System.Drawing.Size(776, 352);
			this.dgSQLResults.TabIndex = 5;
			this.dgSQLResults.TabStop = false;
			// 
			// btnRunSQL
			// 
			this.btnRunSQL.Location = new System.Drawing.Point(288, 4);
			this.btnRunSQL.Name = "btnRunSQL";
			this.btnRunSQL.Size = new System.Drawing.Size(164, 20);
			this.btnRunSQL.TabIndex = 1;
			this.btnRunSQL.Text = "Run SQL";
			this.btnRunSQL.Click += new System.EventHandler(this.btnRunSQL_Click);
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(336, 148);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(172, 16);
			this.label22.TabIndex = 3;
			this.label22.Text = "To get help with SQL syntax visit: ";
			this.label22.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(512, 148);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(264, 20);
			this.linkLabel1.TabIndex = 2;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://www.postgresql.org/docs/7.4/static/sql.html";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(4, 8);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(284, 16);
			this.label21.TabIndex = 1;
			this.label21.Text = "Type in SQL statement compatible with PostGreSQL";
			// 
			// tbSQLStatement
			// 
			this.tbSQLStatement.Location = new System.Drawing.Point(4, 24);
			this.tbSQLStatement.Multiline = true;
			this.tbSQLStatement.Name = "tbSQLStatement";
			this.tbSQLStatement.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbSQLStatement.Size = new System.Drawing.Size(776, 120);
			this.tbSQLStatement.TabIndex = 0;
			this.tbSQLStatement.Text = "";
			// 
			// ilMain
			// 
			this.ilMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.ilMain.ImageSize = new System.Drawing.Size(16, 16);
			this.ilMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
			this.ilMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// dsBankRoll
			// 
			this.dsBankRoll.DataSetName = "BankRoll";
			this.dsBankRoll.Locale = new System.Globalization.CultureInfo("en-US");
			this.dsBankRoll.Tables.AddRange(new System.Data.DataTable[] {
																			this.tblTable});
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "csv";
			this.saveFileDialog.FileName = "report.csv";
			this.saveFileDialog.Title = "Choose location to save the report file";
			this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_FileOk);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(792, 525);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabMain});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "SpadeStat";
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.Move += new System.EventHandler(this.MainForm_Move);
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).EndInit();
			this.tabMain.ResumeLayout(false);
			this.tabReports.ResumeLayout(false);
			this.tabReportTabs.ResumeLayout(false);
			this.tabCriteria.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser2)).EndInit();
			this.tabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser3)).EndInit();
			this.tabPage4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser4)).EndInit();
			this.tabPage5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser5)).EndInit();
			this.tabDebug.ResumeLayout(false);
			this.tabBankRoll.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgBankRoll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dvBankRoll)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tblTable)).EndInit();
			this.tabPlayerData.ResumeLayout(false);
			this.gbPDDetails.ResumeLayout(false);
			this.tabQueryDatabase.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgSQLResults)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsBankRoll)).EndInit();
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


		/// <summary>
		/// On-click handler for menu item responsible for displaying the About form.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			Form formAbout = new AboutForm();
			formAbout.ShowDialog();
		}


		/// <summary>
		/// On-click handler for menu item responsible for displaying the File Import form.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			DataImportForm formImport = new DataImportForm();
			formImport.SetDBConnection(dbConnection);
			formImport.ShowDialog();			
		}


		/// <summary>
		/// Internal method used to display page of particular path.
		/// </summary>
		/// <param name="relativePath"></param>
		private void DisplayHTMLPage(string relativePath)
		{
			object a = 0;  
			object b = 0;  
			object c = 0;  
			object d = 0; 
			string fullPath = Application.StartupPath + "\\" + relativePath;

			if (rbResultsOn1.Checked)
				axWebBrowser1.Navigate(fullPath, ref a, ref b, ref c, ref d);
			else if (rbResultsOn2.Checked)
				axWebBrowser2.Navigate(fullPath, ref a, ref b, ref c, ref d);
			else if (rbResultsOn3.Checked)
				axWebBrowser3.Navigate(fullPath, ref a, ref b, ref c, ref d);
			else if (rbResultsOn4.Checked)
				axWebBrowser4.Navigate(fullPath, ref a, ref b, ref c, ref d);
			else if (rbResultsOn5.Checked)
				axWebBrowser5.Navigate(fullPath, ref a, ref b, ref c, ref d);		
		}


		/// <summary>
		/// On-click handler for button responsible for generating a report.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnGenerateReport_Click(object sender, System.EventArgs e)
		{
			if (cbReportType.Text == "" || cbReportType.Text == null)
			{
				MessageBox.Show("Please select report type first", "Problem Found");
				return;
			}



			dbConnection.Open();
			NpgsqlTransaction dbTransaction = dbConnection.BeginTransaction();

			NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
			command.Transaction = dbTransaction;
			command.CommandText = "select reportid from report where reportnm = '" + cbReportType.Text + "'";
			NpgsqlDataReader reader = command.ExecuteReader();

			int reportID = 0;
			if(reader.Read())
				reportID = reader.GetInt32(0);

			reader.Close();

			dbConnection.Close();	

			Engine.ReportGenerator generator = new Engine.ReportGenerator(dbConnection);
			try
			{
				generator.m_debugFeeder = this;

				// Add all the where criterions:
				Hashtable criterions = new Hashtable();

				// Player selection criteria:
				if (lbPlayers.Enabled && lbPlayers.Items.Count > 0)
				{
					string critPlayerNames = "";
					IEnumerator itr = lbPlayers.Items.GetEnumerator();
					while (itr.MoveNext())
					{
						string playerNm = (string) itr.Current;
						playerNm = playerNm.Replace("'", "''");
						if (critPlayerNames == "")
							critPlayerNames += "(playernm = '" + playerNm + "')";
						else
							critPlayerNames += " OR (playernm = '" + playerNm + "')";
					}

					if (critPlayerNames != "")
						criterions["critplayernmflg"] = critPlayerNames;
				}
				

				// Number of players criteria:
				string critPlayerNumb = "";
				if (tbPlayerNumFrom.Enabled && tbPlayerNumFrom.Text.Trim() != "")
				{
					try 
					{
						int n = int.Parse(tbPlayerNumFrom.Text);
						critPlayerNumb += "totalplayernum >= " + n.ToString();
					} 
					catch (Exception error)
					{
						MessageBox.Show("You have entered invalid number for 'Number of players'", "Problem Found");
						return;
					}
				}

				if (tbPlayerNumTo.Enabled && tbPlayerNumTo.Text.Trim() != "")
				{
					try 
					{
						int n = int.Parse(tbPlayerNumTo.Text);
						if (critPlayerNumb != "")
							critPlayerNumb += " AND ";

						critPlayerNumb += "totalplayernum <= " + n.ToString();
					} 
					catch (Exception error)
					{
						MessageBox.Show("You have entered invalid number for 'Number of players'", "Problem Found");
						return;
					}
				}
				if (critPlayerNumb != "")
					criterions["critnumplayerflg"] = critPlayerNumb;
				

				// Big blind amount criteria:
				string critBigBlindAmt = "";
				if (tbBlindAmtFrom.Enabled && tbBlindAmtFrom.Text.Trim() != "")
				{
					if 
						(
						(cbLimitYes.Checked && (cbLimitNo.Checked || cbLimitPot.Checked))	||
						(!cbLimitYes.Checked && !cbLimitNo.Checked && !cbLimitPot.Checked)	
						)
					{
						MessageBox.Show("When entering range for the blind amount, you must select 'Limit type' of 'Limit' or 'No Limit'/'Pot Limit'", "Problem Found");
						return;						
					}

					try 
					{
						float f = float.Parse(tbBlindAmtFrom.Text);
						if (cbLimitYes.Checked)
							critBigBlindAmt += "bigbetamt >= " + f.ToString();
						else
							critBigBlindAmt += "bigblindamt >= " + f.ToString();
					} 
					catch (Exception error)
					{
						MessageBox.Show("You have entered invalid number for 'Big blind amout:'", "Problem Found");
						return;
					}
				}

				if (tbBlindAmtTo.Enabled && tbBlindAmtTo.Text.Trim() != "")
				{
					if 
									(
						(cbLimitYes.Checked && (cbLimitNo.Checked || cbLimitPot.Checked))	||
						(!cbLimitYes.Checked && !cbLimitNo.Checked && !cbLimitPot.Checked)	
									)
					{
						MessageBox.Show("When entering range for the blind amount, you must select 'Limit type' of 'Limit' or 'No Limit'/'Pot Limit'", "Problem Found");
						return;						
					}

					try 
					{
						float f = float.Parse(tbBlindAmtTo.Text);
						if (critBigBlindAmt != "")
							critBigBlindAmt += " AND ";

						if (cbLimitYes.Checked)
							critBigBlindAmt += "bigbetamt <= " + f.ToString();
						else
							critBigBlindAmt += "bigblindamt <= " + f.ToString();
					} 
					catch (Exception error)
					{
						MessageBox.Show("You have entered invalid number for 'Big blind amout:'", "Problem Found");
						return;
					}
				}
				if (critBigBlindAmt != "")
					criterions["critlimitrangeflg"] = critBigBlindAmt; 
				

				// Start date range criteria
				string critStartDate = "";
				if (dtStartFrom.Enabled)
				{
					dtStartFrom.Value = dtStartFrom.Value.AddHours(-1 * dtStartFrom.Value.Hour);
					dtStartFrom.Value = dtStartFrom.Value.AddMinutes(-1 * dtStartFrom.Value.Minute);
					dtStartFrom.Value = dtStartFrom.Value.AddSeconds(-1 * dtStartFrom.Value.Second);
					critStartDate += "startdt >= '" + dtStartFrom.Value + "'";
				}

				if (dtStartTo.Enabled)
				{
					if (critStartDate != "")
						critStartDate += " AND ";

					dtStartTo.Value = dtStartTo.Value.AddHours(23 - dtStartTo.Value.Hour);
					dtStartTo.Value = dtStartTo.Value.AddMinutes(59 - dtStartTo.Value.Minute);
					dtStartTo.Value = dtStartTo.Value.AddSeconds(59 - dtStartTo.Value.Second);
					critStartDate += "startdt <= '" + dtStartTo.Value + "'";
				}

				if (critStartDate != "")
					criterions["critdaterangeflg"] = critStartDate; 


				// Tournament types criteria:
				if (cbTourTypeHeadUp.Enabled) // Assumption here is made that the rest are enabled as well
				{
					string critTourTypes = "";
					if (!cbTourTypeHeadUp.Checked)
					{
						if (critTourTypes != "")
							critTourTypes += " AND ";

						critTourTypes += "tournamenttypcd != 'HeadUp'";
					}

					if (!cbTourTypeMulti.Checked)
					{
						if (critTourTypes != "")
							critTourTypes += " AND ";

						critTourTypes += "tournamenttypcd != 'Multi'";
					}

					if (!cbTourTypeSnG.Checked)
					{
						if (critTourTypes != "")
							critTourTypes += " AND ";

						critTourTypes += "tournamenttypcd != 'SNG'";
					}

					if (critTourTypes != "")
						criterions["crittournamenttypflg"] = critTourTypes;
				}
				

				// Bet type (limit) criteria:
				if (cbLimitYes.Enabled) // Assumption here is made that the rest are enabled as well
				{
					string critBetType = "";
					if (!cbLimitYes.Checked)
					{
						if (critBetType != "")
							critBetType += " AND ";

						critBetType += "bettypcd != 'Limit'";
					}

					if (!cbLimitNo.Checked)
					{
						if (critBetType != "")
							critBetType += " AND ";

						critBetType += "bettypcd != 'No Limit'";
					}

					if (!cbLimitPot.Checked)
					{
						if (critBetType != "")
							critBetType += " AND ";

						critBetType += "bettypcd != 'Pot Limit'";
					}

					if (critBetType != "")
						criterions["critbettypflg"] = critBetType;
				}


				// Game type criteria:
				if (cbGameTypeHoldem.Enabled) // Assumption here is made that the rest are enabled as well
				{
					string critGameType = "";
					if (!cbGameTypeHoldem.Checked)
					{
						if (critGameType != "")
							critGameType += " AND ";

						critGameType += "gametypcd != 'Hold''em'";
					}
					if (!cbGameTypeOmaha.Checked)
					{
						if (critGameType != "")
							critGameType += " AND ";

						critGameType += "gametypcd != 'Omaha'";
					}
					if (!cbGameTypeOmahaHiLo.Checked)
					{
						if (critGameType != "")
							critGameType += " AND ";

						critGameType += "gametypcd != 'Omaha Hi/Lo'";
					}
					if (!cbGameType7Stud.Checked)
					{
						if (critGameType != "")
							critGameType += " AND ";

						critGameType += "gametypcd != '7 Card Stud'";
					}
					if (!cbGameType7StudHiLo.Checked)
					{
						if (critGameType != "")
							critGameType += " AND ";

						critGameType += "gametypcd != '7 Card Stud Hi/Lo'";
					}

					if (critGameType != "")
						criterions["critgametypflg"] = critGameType;
				}


				// Tournament ID criteria:
				if (tbTourID.Enabled && tbTourID.Text.Trim() != "")
				{
					try 
					{
						// Remove typical extra characters:
						tbTourID.Text = tbTourID.Text.Replace("#", "");
						tbTourID.Text = tbTourID.Text.Replace(":", "");
						tbTourID.Text = tbTourID.Text.Replace(" ", "");
						tbTourID.Text = tbTourID.Text.Replace("-", "");						

						int n = int.Parse(tbTourID.Text);
						string critTourID = "tournamentid = " + n.ToString();
						criterions["crittounamendidflg"] = critTourID;	
					} 
					catch (Exception error)
					{
						MessageBox.Show("You have entered invalid number for 'Tournament ID'", "Problem Found");
						return;
					}
				}


				// Hand ID criteria:
				if (tbHandID.Enabled && tbHandID.Text.Trim() != "")
				{
					try 
					{
						// Remove typical extra characters:
						tbHandID.Text = tbHandID.Text.Replace("#", "");
						tbHandID.Text = tbHandID.Text.Replace(":", "");
						tbHandID.Text = tbHandID.Text.Replace(" ", "");
						tbHandID.Text = tbHandID.Text.Replace("-", "");						

						int n = int.Parse(tbHandID.Text);
						string critHandID = "handid = " + n.ToString();
						criterions["crithandidflg"] = critHandID;
					} 
					catch (Exception error)
					{
						MessageBox.Show("You have entered invalid number for 'Hand ID'", "Problem Found");
						return;
					}
				}


				// Inclusion of ring vs tournament games criteria:
				if (cbIncludeTourGames.Enabled) // Assumption here is made that the rest are enabled as well
				{
					string critRingTour = "";
					if (!cbIncludeTourGames.Checked)
					{
						if (critRingTour != "")
							critRingTour += " AND ";

						critRingTour += "tournamentid = 0";	// Assumption here is that tournament hands have non-zero tournament ID
					}

					if (!cbIncludeRingGames.Checked)
					{
						if (critRingTour != "")
							critRingTour += " AND ";

						critRingTour += "tournamentid <> 0";	// Assumption here is that ring hands have zero for tournament ID
					}

					if (critRingTour != "")
						criterions["crittabletypeflg"] = critRingTour; 
				}


				// Position criteria:
				if (btnPosSelect.Enabled)
				{
					IEnumerator itr = m_positionCriteria.GetEnumerator();
					string critPosition = "";
					while (itr.MoveNext())
					{
						string positionItem = (string) itr.Current;
						if (positionItem != null && positionItem.Length > 0)
						{
							string[] lineParams = positionItem.Split(':');
							string playersNum = lineParams[0];
							string[] positions = lineParams[1].Split(' ');

							if (critPosition != "")
								critPosition += " OR ";

							critPosition += "(totalplayernum = " + playersNum + " AND (";
							IEnumerator itrPos = positions.GetEnumerator();
							string critPositionItem = "";
							while (itrPos.MoveNext())
							{
								string position = (string) itrPos.Current;
								if (position != " " && position != "")
								{
									if (critPositionItem == "")
										critPositionItem += "positionnum = " + position;
									else
										critPositionItem += " OR positionnum = " + position;
								}								
							}
							critPosition += critPositionItem;
							critPosition += "))";
						}
					}

					if (critPosition != "")
						criterions["critpositionflg"] = critPosition;
				}


				// Combo criteria:
				if (lbCombos.Enabled)
				{
					string critCombo = "";

					IEnumerator itr = lbCombos.Items.GetEnumerator();
					while (itr.MoveNext())
					{
						string combo = (string) itr.Current;
						string suitSpec = "";
						string[] cardSpecs = combo.Split(' ');
						string[] cards = new string[14];
						int neededCards = 4;
						IEnumerator itrCards = cardSpecs.GetEnumerator();
						while (itrCards.MoveNext())
						{
							string card = (string) itrCards.Current;
							card = card.Replace(" ", "");
							if (card == "")
								continue;

							if (card.IndexOf('-') > 0)
							{
								// Dealing with suit specification
								if (card == "(Single-suited)")
									suitSpec = "SS";
								else if (card == "(Double-suited)")
									suitSpec = "DS";
								else if (card == "(Off-suit)")
									suitSpec = "OS";
							}
							else
							{
								int pos = 0;
								if (card == "2" ||
									card == "3" ||
									card == "4" ||
									card == "5" ||
									card == "6" ||
									card == "7" ||
									card == "8" ||
									card == "9"
									)
									pos = int.Parse(card) - 1;
								else if (card == "10")
								{
									pos = 9;
									card = "T";
								}
								else if (card == "J")
									pos = 10;
								else if (card == "Q")
									pos = 11;
								else if (card == "K")
									pos = 12;
								else if (card == "A")
									pos = 13;
								else if (card == "x")
									pos = 0;

								if (cards[pos] == null)
									cards[pos] = card;
								else
									cards[pos] += card;

								neededCards--;
							}
						}					

						string sortedCombo = "";
						for (int n = 13; n >= 0; n--)
							if (cards[n] != null)
								sortedCombo += cards[n];

						if (neededCards > 0)
							sortedCombo += new string('x', neededCards);

						if (critCombo != "")
							critCombo += " OR ";
						
						critCombo += "(HandComboTxt = '" + sortedCombo + "'";
						if (suitSpec != "")
							critCombo += " AND SuitedStsCd = '" + suitSpec + "')"; 
						else
							critCombo += ")";
					}

					if (critCombo != "")
						criterions["crithandcomboflg"] = critCombo;
				}


				generator.SetCriterions(criterions);

				if (cbSortBy.Enabled)
				{					
					string sortField = (string) m_orderOptions[cbSortBy.SelectedItem.ToString()];
					if (sortField != null && sortField != "") 
						generator.SetOrderBy(sortField.Replace(" ", ""));
				}
			
				MessageBox.Show("This might take up to few minutes. Click on the Ok button to begin.", "Information");

				ReportGenerator.ReportOutputType outputType = ReportGenerator.ReportOutputType.HTML;
				if (rbResultsOnCSV.Checked)
					outputType = ReportGenerator.ReportOutputType.CSV;
				
				m_currentReportFile = generator.GenerateReport(reportID, Application.StartupPath, outputType);

				if (outputType == ReportGenerator.ReportOutputType.HTML)
				{
					DisplayHTMLPage(m_currentReportFile);				

					if (rbResultsOn1.Checked)
						tabReportTabs.SelectedIndex = 1;
					else if (rbResultsOn2.Checked)
						tabReportTabs.SelectedIndex = 2;
					else if (rbResultsOn3.Checked)
						tabReportTabs.SelectedIndex = 3;
					else if (rbResultsOn4.Checked)
						tabReportTabs.SelectedIndex = 4;
					else if (rbResultsOn5.Checked)
						tabReportTabs.SelectedIndex = 5;				
					
					MessageBox.Show("Report generation completed successfully", "Information");
				}
				else if (outputType == ReportGenerator.ReportOutputType.CSV)
				{
					saveFileDialog.ShowDialog();
				}
			} 
			catch (Exception error)
			{
				dbConnection.Close();

				MessageBox.Show("Following problem was found during report generation: \n\n" + error.Message, "Problem Found");				
				DisplayHTMLPage("Templates\\StartUp.htm");								
			}
		}


		/// <summary>
		/// Temporary method used for debugging report generations.
		/// </summary>
		/// <param name="info"></param>
		public void FeedDebugInfo(string info)
		{
			tbSQLDebug.Text = info;
		}


		/// <summary>
		/// Event handler for on-Click event of the player drop-down. Populates the player drop-down with names if it is empty.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void cbPlayer_Click(object sender, System.EventArgs e)
		{
			if (cbPlayer.Items.Count > 0)
				return;
		

			try
			{
				dbConnection.Open();
				NpgsqlTransaction dbTransaction = dbConnection.BeginTransaction();

				NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
				command.Transaction = dbTransaction;
				command.CommandText = "select distinct playernm from player";
				NpgsqlDataReader reader = command.ExecuteReader();

				while(reader.Read())
				{
					string playerNm = reader.GetString(0);
					cbPlayer.Items.Add(playerNm);
				}
				reader.Close();
				dbConnection.Close();	
			
				cbPlayer.Hide();
				cbPlayer.Show();		
				cbPlayer.DroppedDown = true;	
			}
			catch (Exception error)
			{
				MessageBox.Show("Could not execute player search. Following error was returned from the database service: " + error.Message, "Problem Found");
				dbConnection.Close();		
			}
		}


		/// <summary>
		/// On-Click event handler of the add button which adds the players to be selected for report criteria.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnPlayerAdd_Click(object sender, System.EventArgs e)
		{
			if (cbPlayer.Text == "" || cbPlayer.Text == null)
			{
				MessageBox.Show("Please select a player first", "Problem Found");
				return;
			}

			lbPlayers.Items.Add(cbPlayer.Text);
			cbPlayer.Text = "";
		}


		/// <summary>
		/// On-Click event handler of the add button which removes the players to be selected for report criteria.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnPlayerRemove_Click(object sender, System.EventArgs e)
		{
			if (lbPlayers.SelectedIndex >= 0)
				lbPlayers.Items.RemoveAt(lbPlayers.SelectedIndex);
		}


		/// <summary>
		/// On-Click event handler of the add button which clears the list of players to be selected for report criteria.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnPlayersClear_Click(object sender, System.EventArgs e)
		{
			lbPlayers.Items.Clear();
		}


		/// <summary>
		/// On-Click event handler for displaying registration (licence form)
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("This feature not available yet", "Information");
			return;

			RegForm regImport = new RegForm();
			regImport.ShowDialog();
		}


		/// <summary>
		/// On-Click event handler for the software update dialog.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void menuItem14_Click(object sender, System.EventArgs e)
		{			
			string installedVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

			try
			{
				byte[] latestVersion = webClient.DownloadData("http://www.spadestat.com/latestversion.txt");
				string latestVersionString = System.Text.Encoding.UTF8.GetString(latestVersion);
				if (installedVersion == latestVersionString)
					MessageBox.Show("You are already running the latest version of SpadeStat (" + installedVersion + "). There is no need to perform update. ", "Information");
				else
					MessageBox.Show("New version of SpadeStat is available for download (" + latestVersionString + "). To perform the update, simply quit this application and run the Auto-Update program from the SpadeStat menu.", "Information");
			}
			catch (Exception exception)
			{
				MessageBox.Show("Could not retreive http://www.spadestat.com/latestversion.txt. Please ensure your computer is connected to the Internet.", "Problem Found");
			}
		
		}


		/// <summary>
		/// On-Click event handler for the button used for running custom SQL within the database.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnRunSQL_Click(object sender, System.EventArgs e)
		{
			string sqlString = tbSQLStatement.Text;
			if (sqlString == "" || sqlString == null)
			{
				MessageBox.Show("Please type in a SQL statement first", "Problem Found");				
				return;
			}

			string sqlStringFiltered = sqlString.ToLower();			
			if (sqlStringFiltered.IndexOf("delete") >= 0)
			{
				MessageBox.Show("Cannot execute SQL statements containing 'delete' keyword", "Problem Found");
				return;
			}
			if (sqlStringFiltered.IndexOf("insert") >= 0)
			{
				MessageBox.Show("Cannot execute SQL statements containing 'insert' keyword", "Problem Found");
				return;
			}

			try
			{
				dbConnection.Open();
				NpgsqlDataAdapter dbCommand = new NpgsqlDataAdapter(sqlString, dbConnection);
				DataSet dataSet = new DataSet();
				dataSet.DataSetName = "Results";
				dgSQLResults.DataSource = dataSet;
				
				dbCommand.Fill(dataSet, "Results");
				dbConnection.Close();
					
				dgSQLResults.NavigateTo(0, "Results");
			}
			catch (Exception exception)
			{
				dbConnection.Close();
				MessageBox.Show("Could not execute the SQL statement. Following error was returned from the database service: " + exception.Message, "Problem Found");
			}
		}


		/// <summary>
		/// On-Click event handler for witching to the Reports tab.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void mitmRepotrs_Click(object sender, System.EventArgs e)
		{
			IEnumerator itr = tabMain.TabPages.GetEnumerator();
			while (itr.MoveNext())
			{
				TabPage page = (TabPage) itr.Current;
				if (page.Text == "Reports")
				{
					tabMain.SelectedTab = page;
					return;
				}			
			}	
		}


		/// <summary>
		/// On-Click event handler for witching to the BankRoll tab.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void mitmBankRoll_Click(object sender, System.EventArgs e)
		{
			IEnumerator itr = tabMain.TabPages.GetEnumerator();
			while (itr.MoveNext())
			{
				TabPage page = (TabPage) itr.Current;
				if (page.Text == "Bank Roll")
				{
					tabMain.SelectedTab = page;
					return;
				}			
			}				
		}

		
		/// <summary>
		/// On-Click event handler for witching to the Player Data tab.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void mitmPlayerData_Click(object sender, System.EventArgs e)
		{
			IEnumerator itr = tabMain.TabPages.GetEnumerator();
			while (itr.MoveNext())
			{
				TabPage page = (TabPage) itr.Current;
				if (page.Text == "Player Data")
				{
					tabMain.SelectedTab = page;
					return;
				}			
			}			
		}


		/// <summary>
		/// On-Click event handler for witching to the Query Database tab.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void mitmQueryDatabase_Click(object sender, System.EventArgs e)
		{
			IEnumerator itr = tabMain.TabPages.GetEnumerator();
			while (itr.MoveNext())
			{
				TabPage page = (TabPage) itr.Current;
				if (page.Text == "Query Database")
				{
					tabMain.SelectedTab = page;
					return;
				}			
			}			
		}


		/// <summary>
		/// On-Click event handler for clearing the screen on the Player Data tab.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnPDClear_Click(object sender, System.EventArgs e)
		{
			tbPDCritLocation.Text = "";
			tbPDCritName.Text = "";
			tbPDCritNotes.Text = "";			
			lbPDResults.Items.Clear();
			btnPDSave.Enabled = false;
			tbPDPlayerLocation.Text = "";
			tbPDPlayerName.Text = "";
			tbPDPlayerNotes.Text = "";
			tbPDPlayerNotes.Enabled = false;
			btnPDShowPlayer.Enabled = false;
			btnPDSelect.Enabled = false;
		}


		/// <summary>
		/// On-Click event handler for the button used for searching for players on the Player Data tab.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnPDSearch_Click(object sender, System.EventArgs e)
		{
			lbPDResults.Items.Clear();
			btnPDSave.Enabled = false;
			tbPDPlayerLocation.Text = "";
			tbPDPlayerName.Text = "";
			tbPDPlayerNotes.Text = "";
			tbPDPlayerNotes.Enabled = false;
			btnPDShowPlayer.Enabled = false;
			btnPDSelect.Enabled = false;

			string critName = tbPDCritName.Text.Trim();
			string critLocation = tbPDCritLocation.Text.Trim();
			string critNotes = tbPDCritNotes.Text.Trim();

			if (critName == "" && critLocation == "" && critNotes == "")
			{
				MessageBox.Show("Please provide at least one search criteria", "Problem Found");
				return;
			}

			try
			{
				dbConnection.Open();
				NpgsqlCommand command = dbConnection.CreateCommand();
				string sqlStatement = "select playernm from player where 1 = 1";
				if (critName != "")
					sqlStatement += " and playernm like '%" + critName.Replace("'", "''") + "%'";
				if (critLocation != "")
					sqlStatement += " and locationtxt like '%" + critLocation.Replace("'", "''") + "%'";
				if (critNotes != "")
					sqlStatement += " and playernotetxt like '%" + critNotes.Replace("'", "''") + "%'";
				sqlStatement += " order by playernm";

				command.CommandText = sqlStatement;
				NpgsqlDataReader reader = command.ExecuteReader();
				
				bool foundAny = false;
				while(reader.Read())
				{
					string playerNm = reader.GetString(0);
					lbPDResults.Items.Add(playerNm);
					foundAny = true;
				}
				reader.Close();
				dbConnection.Close();		

				if (!foundAny)
				{
					MessageBox.Show("No players were found who match the criteria.", "Information");				
					dbConnection.Close();					
				}

				btnPDShowPlayer.Enabled = true;
				btnPDSelect.Enabled = true;
			}	
			catch (Exception exception)
			{				
				MessageBox.Show("Could not execute player search. Following error was returned from the database service: " + exception.Message, "Problem Found");
				dbConnection.Close();
			}
		}


		/// <summary>
		/// On-Click event handler for the button used for showing player data on the Player Data tab.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnPDShowPlayer_Click(object sender, System.EventArgs e)
		{
			PDShowPlayerData();		
		}


		/// <summary>
		/// On-Click event handler for the button used for saving player data on the Player Data tab.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnPDSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				dbConnection.Open();
				NpgsqlTransaction dbTransaction = dbConnection.BeginTransaction();

				Player player = new Player(dbTransaction, int.Parse(tbPDPlayerID.Text));
				player.Initialize();

				player.m_PlayerNoteTxt = tbPDPlayerNotes.Text;
				player.Save();
                
				dbTransaction.Commit();
				dbConnection.Close();

				MessageBox.Show("Player data was saved.", "Information");

				btnPDSave.Enabled = false;
				tbPDPlayerLocation.Text = "";
				tbPDPlayerName.Text = "";
				tbPDPlayerNotes.Text = "";
				tbPDPlayerNotes.Enabled = false;
			}
			catch (Exception exception)
			{				
				MessageBox.Show("Could not save player data. Following error was returned from the database service: " + exception.Message, "Problem Found");
				dbConnection.Close();
			}	
		}


		/// <summary>
		/// On-Click event handler for the double-click action on results list off of search for player on the Player Data tab.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void lbPDResults_DoubleClick(object sender, System.EventArgs e)
		{
			PDShowPlayerData();
		}


		/// <summary>
		/// On-Click event handler for the button used to show player data on the Player Data tab.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void PDShowPlayerData()
		{
			if (lbPDResults.Items.Count == 0 || lbPDResults.SelectedItem == null)
			{
				MessageBox.Show("You must select a player first.", "Problem Found");
				return;
			}

			string playerNm = lbPDResults.SelectedItem.ToString();
			
			try
			{
				dbConnection.Open();
				NpgsqlCommand command = dbConnection.CreateCommand();
				string sqlStatement = "select playerid, locationtxt, playernotetxt from player where playernm = '" + playerNm.Replace("'", "''") + "'";
				command.CommandText = sqlStatement;
				NpgsqlDataReader reader = command.ExecuteReader();
				
				bool foundAny = false;
				if (reader.Read())
				{
					int playerID = reader.GetInt32(0);
					string locationTxt = "";
					if (!reader.IsDBNull(1))
						locationTxt = reader.GetString(1);

					string playernoteTxt = "";
					if (!reader.IsDBNull(2))
						playernoteTxt = reader.GetString(2);

					tbPDPlayerName.Text = playerNm;
					tbPDPlayerLocation.Text = locationTxt;					
					tbPDPlayerNotes.Text = playernoteTxt;
					tbPDPlayerID.Text = playerID.ToString();

					tbPDPlayerNotes.Enabled = true;
					btnPDSave.Enabled = true;

					foundAny = true;
				}
				reader.Close();
				dbConnection.Close();		

				if (!foundAny)
				{
					dbConnection.Close();
					MessageBox.Show("Could not retreive player data.", "Problem Found");				
				}
			}	
			catch (Exception exception)
			{				
				MessageBox.Show("Could not retreive player data. Following error was returned from the database service: " + exception.Message, "Problem Found");
				dbConnection.Close();
			}			
		}


		/// <summary>
		/// On-Click event handler for the drop-down list which contains list of reports available within the system. Populates the list if empty.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void cbReportType_Click(object sender, System.EventArgs e)
		{
			if (cbReportType.Items.Count > 0)
				return;

			dbConnection.Open();
			try
			{
				NpgsqlTransaction dbTransaction = dbConnection.BeginTransaction();

				NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
				command.Transaction = dbTransaction;
				command.CommandText = "select reportid, reportnm from report order by reportnm";
				NpgsqlDataReader reader = command.ExecuteReader();

				while(reader.Read())
				{
					int reportID = reader.GetInt32(0);
					string reportNm = reader.GetString(1);
					cbReportType.Items.Add(reportNm);
				}
				reader.Close();
			}
			catch (Exception error)
			{			
				MessageBox.Show("Could not retreive report definition. Following error was returned from the database service: " + error.Message, "Problem Found");
				dbConnection.Close();
				return;
			}

			dbConnection.Close();		
						
			cbReportType.Hide();
			cbReportType.Show();		
			cbReportType.DroppedDown = true;			
		}


		/// <summary>
		/// On-click handler for menu item responsible for clearing report criteria
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void btnClearCriteria_Click(object sender, System.EventArgs e)
		{
			lbPlayers.Items.Clear();
			lbCombos.Items.Clear();
			lbPositions.Items.Clear();
			m_positionCriteria = new string[8];
			tbPlayerNumFrom.Text = "";
			tbPlayerNumTo.Text = "";
			tbBlindAmtFrom.Text = "";
			tbBlindAmtTo.Text = "";
			tbTourID.Text = "";
			tbHandID.Text = "";			
			cbStartFrom.Checked = false;
			cbStartTo.Checked = false;
			dtStartFrom.Enabled = false;
			dtStartTo.Enabled = false;
			cbIncludeTourGames.Checked = true;
			cbIncludeRingGames.Checked = true;
			cbTourTypeMulti.Checked = true;
			cbTourTypeSnG.Checked = true;
			cbTourTypeHeadUp.Checked = true;
			cbLimitYes.Checked = true;
			cbLimitNo.Checked = true;
			cbLimitPot.Checked = true;
			cbGameTypeHoldem.Checked = true;
			cbGameTypeOmaha.Checked = true;
			cbGameTypeOmahaHiLo.Checked = true;
			cbGameType7Stud.Checked = true;
			cbGameType7StudHiLo.Checked = true;
			cbCard1.SelectedIndex = 0;
			cbCard2.SelectedIndex = 0;
			cbCard3.SelectedIndex = 0;
			cbCard4.SelectedIndex = 0;
			cbCardSuiting.SelectedIndex = 0;
			cbSortBy.SelectedIndex = 0;
		}


		/// <summary>
		/// On-click handler for menu item responsible for opening dialog for importing files via email
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void mitmImportEmail_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("This feature not available yet", "Information");
		}


		/// <summary>
		/// On-click handler for menu item responsible for terminating the application
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void mitmExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();	
		}


		/// <summary>
		/// On-click handler for menu item responsible for opening dialog for setting configuration parameters
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void mitmConfiguration_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("This feature not available yet", "Information");
		}


		/// <summary>
		/// On-click handler for menu item responsible for opening dialog for clearning database.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void mitmClearDatabase_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("This feature not available yet", "Information");
		}


		/// <summary>
		/// On-click handler for menu item responsible for opening the user guide.
		/// </summary>
		/// <param name="sender">Event sender</param>
		/// <param name="e">Event arguments</param>
		private void mitmUserGuide_Click(object sender, System.EventArgs e)
		{
			Process userGuideProcess = new Process();
			string userGuideString = Application.StartupPath + "\\resources\\spadestatuserguide.htm";
			ProcessStartInfo userGuideInfo = new ProcessStartInfo("iexplore.exe", userGuideString);
			// userGuideInfo.UseShellExecute = false;
			userGuideProcess.StartInfo = userGuideInfo;
			userGuideProcess.Start();		
		}

		/// <summary>
		/// Event handler for form-loading event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, System.EventArgs e)
		{
			// Determine if the expiration has gone by:
			if (DateTime.Now > DateTime.Parse("7/1/2005"))
			{
				MessageBox.Show("This version of the SpadeStat software has expired. Please visit http://www.spadestat.com to get latest version.", "Information");
				Application.Exit();
				return;
			}

			
			// Determine if database has been initialized yet:
			RegistryKey key = Registry.LocalMachine;
			key = key.OpenSubKey(@"Software\SpadeStat", true);
			if (key != null && int.Parse(key.GetValue("bDBInitialized").ToString()) == 0)
			{
				// Attempt to see if the database connection can be opened
				NpgsqlConnection checkConnection = new NpgsqlConnection();
				bool dbTestSucceeded = true;
				try
				{
					checkConnection.ConnectionString = "SERVER=127.0.0.1;PORT=5454;USER ID=postgres;PASSWORD=spadestat;DATABASE=template1";
					checkConnection.Open();						
					NpgsqlCommand command = checkConnection.CreateCommand();
					command.CommandText = "select count(*) from report";
					command.ExecuteNonQuery();						
					checkConnection.Close();	
				}
				catch (Exception error)
				{				
					dbTestSucceeded = false;
				}

				if (!dbTestSucceeded)
				{
					MessageBox.Show("The PostGreSQL 8.0 database in which SpadeStat stored hand history data will now be installed and initialized. This might take few minutes.", "Information");


					// Create the service user:
					try
					{						
						Process userCreateProcess = new Process();
						string userCreateString = "user /add /y spadestatdb spadestatdb /EXPIRES:NEVER /COMMENT:\"SpadeStat DB Service Account\" /FULLNAME:\"SpadeStat DB Service Account\"";
						ProcessStartInfo userCreateInfo = new ProcessStartInfo("net.exe", userCreateString);
						userCreateInfo.UseShellExecute = false;
						userCreateProcess.StartInfo = userCreateInfo;
						userCreateProcess.Start();
						while (!userCreateProcess.HasExited)
							Thread.Sleep(500);
							
						int result = userCreateProcess.ExitCode;
						if (result != 0)
						{
							MessageBox.Show("Could not create database service user. Error code: " + result.ToString(), "Problem Found");
							Application.Exit();
							return;
						}
					}
					catch (Exception error)
					{
						MessageBox.Show("Could not create database service user. Following error was found: " + error.Message, "Problem Found");
						Application.Exit();
						return;
					}


					// Grant rights to the service user:
					try
					{						
						Process userRightsProcess = new Process();
						string userRightsString = "-u spadestatdb +r SeServiceLogonRight";
						ProcessStartInfo userRightsInfo = new ProcessStartInfo("ntrights.exe", userRightsString);
						userRightsInfo.UseShellExecute = false;
						userRightsProcess.StartInfo = userRightsInfo;
						userRightsProcess.Start();
						while (!userRightsProcess.HasExited)
							Thread.Sleep(500);
							
						int result = userRightsProcess.ExitCode;
						if (result != 0)
						{
							MessageBox.Show("Could not grant the database service user certain rights. Error code: " + result.ToString(), "Problem Found");
							Application.Exit();
							return;
						}
					}
					catch (Exception error)
					{
						MessageBox.Show("Could not grant the database service user certain rights. Following error was found: " + error.Message, "Problem Found");
						Application.Exit();
						return;
					}


					// Install PostGreSQL:
					try
					{						
						Process installProcess = new Process();
						string installString = "/i postgresql-8.0-int.msi /qb! ADDLOCAL=server,psql,pgadmin,npgsql,psqlodbc,docs DOINITDB=1 INTERNALLAUNCH=1 DOSERVICE=1 SERVICEACCOUNT=spadestatdb SERVICEPASSWORD=spadestatdb SUPERPASSWORD=spadestat LISTENPORT=5454 BASEDIR=\"" + Application.StartupPath + "\\PGSQL\" SERVICENAME=\"SpadeStat PostGreSQL Database Service\"";
						ProcessStartInfo installInfo = new ProcessStartInfo("msiexec.exe", installString);
						installInfo.UseShellExecute = false;
						installProcess.StartInfo = installInfo;
						installProcess.Start();
						while (!installProcess.HasExited)
							Thread.Sleep(1000);
							
						int result = installProcess.ExitCode;
						if (result != 0)
						{
							MessageBox.Show("Could not install the database service. Error code: " + result.ToString(), "Problem Found");
							Application.Exit();
							return;
						}
					}
					catch (Exception error)
					{
						MessageBox.Show("Could not install the database service. Following error was returned from the database service: " + error.Message, "Problem Found");
						Application.Exit();
						return;
					}

					// Create the database, run init script
					NpgsqlConnection initConnection = new NpgsqlConnection();
					try
					{
						initConnection.ConnectionString = "SERVER=127.0.0.1;PORT=5454;USER ID=postgres;PASSWORD=spadestat;DATABASE=template1";
						initConnection.Open();						
						NpgsqlCommand command = initConnection.CreateCommand();
						command.CommandText = "CREATE DATABASE spadestat OWNER postgres";
						command.ExecuteNonQuery();						
						initConnection.Close();	
					}
					catch (Exception error)
					{
						MessageBox.Show("Could not create the database. Following error was returned from the database service: " + error.Message, "Problem Found");
						initConnection.Close();		
						Application.Exit();
						return;
					}

					try
					{
						// Read in the whole initialization file:
						FileStream initFile = new FileStream(Application.StartupPath + "\\initscript.sql", FileMode.Open, FileAccess.Read, FileShare.None, 4096, true);
						StreamReader inputStream = new StreamReader(initFile);
						string initContent = inputStream.ReadToEnd();
						inputStream.Close();
						initFile.Close();	

						dbConnection.Open();
						NpgsqlCommand command = dbConnection.CreateCommand();	
						command.CommandText = initContent;
						command.ExecuteNonQuery();
						dbConnection.Close();
					}
					catch (Exception error)
					{
						MessageBox.Show("Could not execute the initialization script. Following error found: " + error.Message, "Problem Found");
						initConnection.Close();	
						Application.Exit();
						return;
					}

					// Mark sucessfull DB initialization:
					key.SetValue("bDBInitialized", "1");
				}
			}
				

			// Determine if update files is provided:
			if (File.Exists(Application.StartupPath + "\\update.dat"))
			{
				UpdateForm formUpdate = new UpdateForm();
				formUpdate.SetUpdateFile(Application.StartupPath + "\\update.dat");
				formUpdate.SetDBConnection(dbConnection);
				formUpdate.ShowDialog();							
			}

			BRRefreshList();
		}

		private void cbStartFrom_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbStartFrom.Checked)
			{
				dtStartFrom.Enabled = true;				
			}
			else
			{
				dtStartFrom.Enabled = false;
			}
		}

		private void cbStartTo_CheckedChanged(object sender, System.EventArgs e)
		{
			if (cbStartTo.Checked)
			{
				dtStartTo.Enabled = true;				
			}
			else
			{
				dtStartTo.Enabled = false;
			}		
		}

		private void cbReportType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			bool crithandcomboflg = false;
			bool critpositionflg = false;
			bool critnumplayerflg = false;
			bool critgametypflg = false;
			bool critbettypflg = false;
			bool crittournamenttypflg = false;
			bool critplayernmflg = false;
			bool critdaterangeflg = false;
			bool critlimitrangeflg = false;
			bool crittabletypeflg = false;
			bool crittounamendidflg = false;
			bool crithandidflg = false;
            
			m_orderOptions = new Hashtable();
			string orderBy = "";
			
			dbConnection.Open();
			try
			{
				string reportNm = cbReportType.SelectedItem.ToString();
				if (reportNm == "")
				{
					dbConnection.Close();
					return;
				}

				NpgsqlTransaction dbTransaction = dbConnection.BeginTransaction();

				int reportID = Report.FindReportID(dbTransaction, reportNm);
				if (reportID == -1)
					throw new Exception("Report ID not found");

				NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
				command.Transaction = dbTransaction;
				command.CommandText = "select crithandcomboflg, critpositionflg, critnumplayerflg, critgametypflg, critbettypflg, crittournamenttypflg, critplayernmflg, critdaterangeflg, critlimitrangeflg, crittabletypeflg, crittounamendidflg, crithandidflg, orderby from reportsection where reportid = " + reportID.ToString();
				NpgsqlDataReader reader = command.ExecuteReader();
				
				while(reader.Read())
				{
					if (reader.GetInt32(0) > 0)
						crithandcomboflg = true;
					if (reader.GetInt32(1) > 0)
						critpositionflg = true;
					if (reader.GetInt32(2) > 0)
						critnumplayerflg = true;
					if (reader.GetInt32(3) > 0)
						critgametypflg = true;
					if (reader.GetInt32(4) > 0)
						critbettypflg = true;
					if (reader.GetInt32(5) > 0)
						crittournamenttypflg = true;
					if (reader.GetInt32(6) > 0)
						critplayernmflg = true;
					if (reader.GetInt32(7) > 0)
						critdaterangeflg = true;
					if (reader.GetInt32(8) > 0)
						critlimitrangeflg = true;
					if (reader.GetInt32(9) > 0)
						crittabletypeflg = true;
					if (reader.GetInt32(10) > 0)
						crittounamendidflg = true;
					if (reader.GetInt32(11) > 0)
						crithandidflg = true;
					if (reader.GetString(12) != null)
						orderBy = reader.GetString(12);

					// Combine the order by's into one hash dictionary:
					string[] orderBys = orderBy.Split(',');
					IEnumerator itr = orderBys.GetEnumerator();
					while (itr.MoveNext())
					{
						string orderBySpec = (string) itr.Current;
						string[] orderByParams = orderBySpec.Split('|');
						string field = orderByParams[0];
						string label = orderByParams[1];

						m_orderOptions[label] = field;
					}
				}

				reader.Close();
				dbConnection.Close();
			}
			catch (Exception error)
			{			
				MessageBox.Show("Could not retreive report definition. Following error was returned from the database service: " + error.Message, "Problem Found");
				dbConnection.Close();
				return;
			}

			// Populate order by-box or disable it:
			if (m_orderOptions.Count == 0)
			{
				cbSortBy.Items.Clear();
				cbSortBy.Enabled = false;
			}
			else
			{
				cbSortBy.Items.Clear();
				cbSortBy.Items.Add("-");
				IDictionaryEnumerator itr = m_orderOptions.GetEnumerator();
				while (itr.MoveNext())
				{
					string label = (string) itr.Key;
					cbSortBy.Items.Add(label);
				}

				cbSortBy.Enabled = true;
				cbSortBy.SelectedIndex = 0;
			}

			
			if (crithandcomboflg)
			{
				lbCombos.BackColor = SystemColors.Window;
				lbCombos.Enabled = true;
				btnComboAdd.Enabled = true;
				btnComboRemove.Enabled = true;
				btnComboClear.Enabled = true;
				cbCard1.Enabled = true;
				cbCard1.SelectedIndex = 0;
				cbCard2.Enabled = true;
				cbCard2.SelectedIndex = 0;
				cbCard3.Enabled = true;
				cbCard3.SelectedIndex = 0;
				cbCard4.Enabled = true;
				cbCard4.SelectedIndex = 0;
				cbCardSuiting.Enabled = true;
				cbCardSuiting.SelectedIndex = 0;
				lblCritHandCombos.BackColor = Color.Yellow;
			}
			else
			{
				lbCombos.BackColor = SystemColors.InactiveBorder;
				lbCombos.Enabled = false;
				btnComboAdd.Enabled = false;
				btnComboRemove.Enabled = false;
				btnComboClear.Enabled = false;
				cbCard1.Enabled = false;
				cbCard1.SelectedIndex = 0;
				cbCard2.Enabled = false;
				cbCard2.SelectedIndex = 0;
				cbCard3.Enabled = false;
				cbCard3.SelectedIndex = 0;
				cbCard4.Enabled = false;
				cbCard4.SelectedIndex = 0;
				cbCardSuiting.Enabled = false;
				cbCardSuiting.SelectedIndex = 0;
				lblCritHandCombos.BackColor = SystemColors.InactiveBorder;
			}

			if (critpositionflg)
			{
				btnPosClear.Enabled = true;
				btnPosSelect.Enabled = true;
				lblCritPosition.BackColor = Color.Yellow;
				lbPositions.BackColor = SystemColors.Window;
			}
			else
			{
				btnPosClear.Enabled = false;
				btnPosSelect.Enabled = false;
				lblCritPosition.BackColor = SystemColors.InactiveBorder;
				lbPositions.BackColor = SystemColors.InactiveBorder;
			}

			if (critnumplayerflg)
			{
				tbPlayerNumFrom.Enabled = true;
				tbPlayerNumTo.Enabled = true;
				lblCritPlayersNum.BackColor = Color.Yellow;
			}
			else
			{
				tbPlayerNumFrom.Enabled = false;
				tbPlayerNumTo.Enabled = false;
				lblCritPlayersNum.BackColor = SystemColors.InactiveBorder;
			}

			if (critgametypflg)
			{
				cbGameType7Stud.Enabled = true;
				cbGameType7StudHiLo.Enabled = true;
				cbGameTypeOmahaHiLo.Enabled = true;
				cbGameTypeHoldem.Enabled = true;
				cbGameTypeOmaha.Enabled = true;
				lblCritGameType.BackColor = Color.Yellow;
			}
			else
			{
				cbGameType7Stud.Enabled = false;
				cbGameType7StudHiLo.Enabled = false;
				cbGameTypeOmahaHiLo.Enabled = false;
				cbGameTypeHoldem.Enabled = false;
				cbGameTypeOmaha.Enabled = false;
				lblCritGameType.BackColor = SystemColors.InactiveBorder;
			}

			if (critbettypflg)
			{
				cbLimitNo.Enabled = true;
				cbLimitYes.Enabled = true;
				cbLimitPot.Enabled = true;
				lblCritLimitType.BackColor = Color.Yellow;
			}
			else
			{
				cbLimitNo.Enabled = false;
				cbLimitYes.Enabled = false;
				cbLimitPot.Enabled = false;
				lblCritLimitType.BackColor = SystemColors.InactiveBorder;
			}

			if (crittournamenttypflg)
			{
				cbTourTypeHeadUp.Enabled = true;
				cbTourTypeMulti.Enabled = true;
				cbTourTypeSnG.Enabled = true;
				lblCritTourType.BackColor = Color.Yellow;
			}
			else
			{
				cbTourTypeHeadUp.Enabled = false;
				cbTourTypeMulti.Enabled = false;
				cbTourTypeSnG.Enabled = false;
				lblCritTourType.BackColor = SystemColors.InactiveBorder;
			}

			if (critplayernmflg)
			{
				cbPlayer.Enabled = true;
				lbPlayers.Enabled = true;
				lbPlayers.BackColor = SystemColors.Window;						
				btnPlayerAdd.Enabled = true;
				btnPlayerRemove.Enabled = true;
				btnPlayersClear.Enabled = true;
				lblCritPlayers.BackColor = Color.Yellow;
			}
			else
			{
				cbPlayer.Enabled = false;
				lbPlayers.Enabled = false;
				lbPlayers.BackColor = SystemColors.InactiveBorder;
				btnPlayerAdd.Enabled = false;
				btnPlayerRemove.Enabled = false;
				btnPlayersClear.Enabled = false;
				lblCritPlayers.BackColor = SystemColors.InactiveBorder;
			}

			if (critdaterangeflg)
			{
				cbStartFrom.Enabled = true;
				cbStartTo.Enabled = true;
				if (cbStartFrom.Checked)
					dtStartFrom.Enabled = true;
				else
					dtStartFrom.Enabled = false;

				if (cbStartTo.Checked)
					dtStartTo.Enabled = true;
				else
					dtStartTo.Enabled = false;
				lblCritDateRange.BackColor = Color.Yellow;

			}
			else
			{
				cbStartFrom.Enabled = false;
				cbStartTo.Enabled = false;
				dtStartFrom.Enabled = false;
				dtStartTo.Enabled = false;
				lblCritDateRange.BackColor = SystemColors.InactiveBorder;
			}

			if (critlimitrangeflg)
			{
				tbBlindAmtFrom.Enabled = true;
				tbBlindAmtTo.Enabled = true;
				lblCritBlindAmout.BackColor = Color.Yellow;
			}
			else
			{
				tbBlindAmtFrom.Enabled = false;
				tbBlindAmtTo.Enabled = false;
				lblCritBlindAmout.BackColor = SystemColors.InactiveBorder;				
			}

		}

		private void btnComboClear_Click(object sender, System.EventArgs e)
		{
			lbCombos.Items.Clear();
		}

		private void btnComboRemove_Click(object sender, System.EventArgs e)
		{
			if (lbCombos.SelectedIndex >= 0)
				lbCombos.Items.RemoveAt(lbCombos.SelectedIndex);		
		}

		private void btnComboAdd_Click(object sender, System.EventArgs e)
		{
			if (cbCard1.Text == "-" && cbCard2.Text == "-" && cbCard3.Text == "-" && cbCard4.Text == "-")
			{
				MessageBox.Show("Please select at least one card first", "Problem Found");
				return;
			}

			string cardCombo = "";
			if (cbCard1.Text != "-")
				cardCombo += cbCard1.Text + " ";
			if (cbCard2.Text != "-")
				cardCombo += cbCard2.Text + " ";
			if (cbCard3.Text != "-")
				cardCombo += cbCard3.Text + " ";
			if (cbCard4.Text != "-")
				cardCombo += cbCard4.Text + " ";
			if (cbCardSuiting.Text != "-")
			{
				if (cbCardSuiting.Text == "SS")
					cardCombo += "(Single-suited)";				
				if (cbCardSuiting.Text == "DS")
					cardCombo += "(Double-suited)";				
				if (cbCardSuiting.Text == "OS")
					cardCombo += "(Off-suit)";
			}

			lbCombos.Items.Add(cardCombo);
			cbCard1.SelectedIndex = 0;
			cbCard2.SelectedIndex = 0;
			cbCard3.SelectedIndex = 0;
			cbCard4.SelectedIndex = 0;			
			cbCardSuiting.SelectedIndex = 0;
		}

		private void btnPosClear_Click(object sender, System.EventArgs e)
		{
			lbPositions.Items.Clear(); 
			m_positionCriteria = new string[8];
		}

		private void btnPosSelect_Click(object sender, System.EventArgs e)
		{
			PositionCritForm formPosCrit = new PositionCritForm();
			formPosCrit.SetPositionCriteria(m_positionCriteria);
			formPosCrit.ShowDialog();
			lbPositions.Items.Clear();
			IEnumerator itr = m_positionCriteria.GetEnumerator();
			while (itr.MoveNext())
			{
				string line = (string) itr.Current;
				if (line != null && line.Length > 0)
					lbPositions.Items.Add(line);
			}			
		}

		private void btnPDSelect_Click(object sender, System.EventArgs e)
		{
			
			if (lbPDResults.Items.Count == 0 || lbPDResults.SelectedItem == null)
			{
				MessageBox.Show("You must select a player first.", "Problem Found");
				return;
			}

			string playerNm = lbPDResults.SelectedItem.ToString();
			lbPlayers.Items.Add(playerNm);
			tabMain.SelectedIndex = 0;
			tabReportTabs.SelectedIndex = 0;
		}

		private void dgBankRoll_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			BRShowCurrentRecord();
		}

		private void BRShowCurrentRecord()
		{			
			if (dgBankRoll.CurrentRowIndex >=0)
			{
				dgBankRoll.Select(dgBankRoll.CurrentRowIndex);
				DataRow row = dvBankRoll.Table.Rows[dgBankRoll.CurrentRowIndex];
				string rowID = row.ItemArray.GetValue(0).ToString();	
				m_BRCurrentRowID = int.Parse(rowID);

				try
				{
					dbConnection.Open();
					NpgsqlTransaction dbTransaction = dbConnection.BeginTransaction();
					PlayerBankroll bankroll = new PlayerBankroll(dbTransaction, m_BRCurrentRowID);
					bankroll.Initialize();
					dbConnection.Close();

					tbBRGroupingAmt.Text = bankroll.m_BBAmt.ToString();
					tbBRNetAmt.Text = bankroll.m_NetChangeAmt.ToString();
					tbBRNetAmt.Text = bankroll.m_NetChangeAmt.ToString();
					tbBRNetTime.Text = bankroll.m_NetTimeAmt.ToString();
					cbBRBetType.Text = bankroll.m_BetTypCd.ToString();
					cbBRGameType.Text = bankroll.m_GameTypCd.ToString();
					cbBRSite.Text = bankroll.m_SiteCd.ToString();
					cbBRPokerType.Text = bankroll.m_TypeCd.ToString();
					dtpBRStartDt.Value = bankroll.m_StartDt;
					dtpBREndDt.Value = bankroll.m_EndDt;

					if (bankroll.m_StartDt.Hour >= 12)
					{
						if (bankroll.m_StartDt.Hour == 12)							
							cbBRStartTimeHour.Text = "12";
						else
							cbBRStartTimeHour.Text = (bankroll.m_StartDt.Hour - 12).ToString();
						cbBRStartTimeAP.Text = "PM";
					}
					else
					{					
						if (bankroll.m_StartDt.Hour == 0)
							cbBRStartTimeHour.Text = "12";
						else
							cbBRStartTimeHour.Text = (bankroll.m_StartDt.Hour).ToString();
						cbBRStartTimeAP.Text = "AM";
					}
					cbBRStartTimeMin.Text = bankroll.m_StartDt.Minute.ToString();

					if (bankroll.m_EndDt.Hour >= 12)
					{
						if (bankroll.m_EndDt.Hour == 12)							
							cbBREndTimeHour.Text = "12";
						else
							cbBREndTimeHour.Text = (bankroll.m_EndDt.Hour - 12).ToString();
						cbBREndTimeAP.Text = "PM";
					}
					else
					{					
						if (bankroll.m_EndDt.Hour == 0)
							cbBREndTimeHour.Text = "12";
						else
							cbBREndTimeHour.Text = (bankroll.m_EndDt.Hour).ToString();
						cbBREndTimeAP.Text = "AM";
					}
					cbBREndTimeMin.Text = bankroll.m_EndDt.Minute.ToString();

					btnBRSave.Enabled = true;

				}					
				catch (Exception error)
				{
					MessageBox.Show("Could not execute bankroll command. Following error was returned from the database service: " + error.Message, "Problem Found");
					dbConnection.Close();		
				}	
				
			}			
		}

		private void btnBRDelete_Click(object sender, System.EventArgs e)
		{
			if (dgBankRoll.CurrentRowIndex >= 0)
			{
				try
				{
					dgBankRoll.Select(dgBankRoll.CurrentRowIndex);
					DataRow row = dvBankRoll.Table.Rows[dgBankRoll.CurrentRowIndex];
					string rowID = row.ItemArray.GetValue(0).ToString();
				

					dbConnection.Open();
					NpgsqlTransaction dbTransaction = dbConnection.BeginTransaction();

					NpgsqlCommand command = dbTransaction.Connection.CreateCommand();
					command.Transaction = dbTransaction;
					command.CommandText = "delete from playerbankroll where bankrollid = '" + rowID + "'";
					int n = command.ExecuteNonQuery();
					dbTransaction.Commit();

					dbConnection.Close();
		
					BRRefreshList();
				}
				catch (Exception error)
				{
					MessageBox.Show("Could not execute bankroll command. Following error was returned from the database service: " + error.Message, "Problem Found");
					dbConnection.Close();		
				}	
			}
			else
				MessageBox.Show("Please first select row to be deleted", "Problem found");

		}

		private void saveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				File.Copy(Application.StartupPath + "\\" + m_currentReportFile, saveFileDialog.FileName);
			}
			catch (Exception error)
			{
				MessageBox.Show("Could not write the target file. Please make sure no application has the target file already open and try again.", "Problem Found");
			}
		}

		private void btnBRAdd_Click(object sender, System.EventArgs e)
		{
			if (!ValidateBREntry())
				return;

			try
			{
				dbConnection.Open();
				NpgsqlTransaction dbTransaction = dbConnection.BeginTransaction();

				UUIDGenerator generator = new UUIDGenerator(dbTransaction);
				PlayerBankroll bankRoll = new PlayerBankroll(dbTransaction, generator);				
				bankRoll.Initialize();
				
				BRUpdateRecord(bankRoll);

				bankRoll.Save();                
				generator.Save();

				dbTransaction.Commit();
				dbConnection.Close();
		
				BRRefreshList();				
			}
			catch (Exception error)
			{
				MessageBox.Show("Could not execute bankroll command. Following error was returned from the database service: " + error.Message, "Problem Found");
				dbConnection.Close();		
			}	

			
			BRClearEntry();
		}

		private void BRUpdateRecord(PlayerBankroll bankroll)
		{
			bankroll.m_NetChangeAmt = decimal.Parse(tbBRNetAmt.Text);			

			decimal bbAmt = 0;
			try
			{
				bbAmt = decimal.Parse(tbBRGroupingAmt.Text);
			}
			catch (Exception parseError) {}
			bankroll.m_BBAmt = bbAmt;

			decimal netAmt = 0;
			try
			{
				netAmt = decimal.Parse(tbBRNetAmt.Text);
			}
			catch (Exception parseError) {}
			bankroll.m_NetChangeAmt = netAmt;

			decimal netTime = 0;
			try
			{
				netTime = decimal.Parse(tbBRNetTime.Text);
			}
			catch (Exception parseError) {}
			bankroll.m_NetTimeAmt = netTime;

			bankroll.m_BetTypCd = cbBRBetType.Text;
			bankroll.m_GameTypCd = cbBRGameType.Text;
			bankroll.m_SiteCd = cbBRSite.Text;
			bankroll.m_TypeCd = cbBRPokerType.Text;
			bankroll.m_StartDt = DateTime.Parse(dtpBRStartDt.Value.ToShortDateString() + " " + cbBRStartTimeHour.Text + ":" + cbBRStartTimeMin.Text + " " + cbBRStartTimeAP.Text);
			bankroll.m_EndDt = DateTime.Parse(dtpBREndDt.Value.ToShortDateString() + " " + cbBREndTimeHour.Text + ":" + cbBREndTimeMin.Text + " " + cbBREndTimeAP.Text);
		}


		private void btnBRSave_Click(object sender, System.EventArgs e)
		{
			if (m_BRCurrentRowID == -1)
			{
				MessageBox.Show("First you must select a row to edit.", "Problem Found");
				return;
			}

			if (!ValidateBREntry())
				return;

			try
			{
				dbConnection.Open();
				NpgsqlTransaction dbTransaction = dbConnection.BeginTransaction();

				PlayerBankroll bankRoll = new PlayerBankroll(dbTransaction, m_BRCurrentRowID);				
				bankRoll.Initialize();
				
				BRUpdateRecord(bankRoll);

				bankRoll.Save();                

				dbTransaction.Commit();
				dbConnection.Close();
		
				BRRefreshList();				
			}
			catch (Exception error)
			{
				MessageBox.Show("Could not execute bankroll command. Following error was returned from the database service: " + error.Message, "Problem Found");
				dbConnection.Close();	
				return;
			}	
			
			BRClearEntry();

			MessageBox.Show("Record saved", "Information");
		}

		private bool ValidateBREntry()
		{
			if (cbBRPokerType.SelectedIndex == -1)
			{
				MessageBox.Show("You must first select 'Poker type'.", "Problem Found");
				return false;
			}

			if (cbBRGameType.SelectedIndex == -1)
			{
				MessageBox.Show("You must first select 'Game type'.", "Problem Found");
				return false;
			}

			if (cbBRBetType.SelectedIndex == -1)
			{
				MessageBox.Show("You must first select 'Bet type'.", "Problem Found");
				return false;
			}

			if (cbBRSite.SelectedIndex == -1)
			{
				MessageBox.Show("You must first select 'Site'.", "Problem Found");
				return false;
			}

			long lNetTime = 0;
			try
			{
				lNetTime = long.Parse(tbBRNetTime.Text);
			}
			catch (Exception parseError)
			{}

			if (lNetTime <= 0)
			{
				MessageBox.Show("You must first enter positive whole number for 'Net Change (min)'.", "Problem Found");
				return false;
			}

			decimal lNetAmt = 0;
			try
			{
				lNetAmt = decimal.Parse(tbBRNetAmt.Text);
			}
			catch (Exception parseError)
			{
				MessageBox.Show("You must first enter 'Net Change ($)'.", "Problem Found");
				return false;
			}

			decimal lGroupAmt = 0;
			try
			{
				lGroupAmt = decimal.Parse(tbBRGroupingAmt.Text);
			}
			catch (Exception parseError)
			{}


			if (dtpBRStartDt.Value > dtpBREndDt.Value)
			{
				MessageBox.Show("Start date must be before or same as end date.", "Problem Found");
				return false;			
			}

			if (
				cbBRStartTimeHour.SelectedIndex == -1 ||
				cbBRStartTimeMin.SelectedIndex == -1 ||
				cbBRStartTimeAP.SelectedIndex == -1
				)
			{
				MessageBox.Show("You must first enter valid start date and time.", "Problem Found");
				return false;
			}

			if (
				cbBREndTimeHour.SelectedIndex == -1 ||
				cbBREndTimeMin.SelectedIndex == -1 ||
				cbBREndTimeAP.SelectedIndex == -1
				)
			{
				MessageBox.Show("You must first enter valid end date and time.", "Problem Found");
				return false;
			}

			return true;
		}

		private void BRClearEntry()
		{
			cbBRPokerType.SelectedIndex = -1;
			cbBRGameType.SelectedIndex = -1;
			cbBRBetType.SelectedIndex = -1;
			cbBRSite.SelectedIndex = -1;
			tbBRNetTime.Text = "";
			tbBRNetAmt.Text = "";
			tbBRGroupingAmt.Text = "";
			dtpBRStartDt.Value = DateTime.Now;
			dtpBREndDt.Value = DateTime.Now;
			cbBRStartTimeHour.SelectedIndex = -1;
			cbBRStartTimeMin.SelectedIndex = -1;
			cbBRStartTimeAP.SelectedIndex = -1;
			cbBREndTimeHour.SelectedIndex = -1;
			cbBREndTimeMin.SelectedIndex = -1;
			cbBREndTimeAP.SelectedIndex = -1;

			m_BRCurrentRowID = -1;

			btnBRSave.Enabled = false;
		}

		private void BRRefreshList()
		{
			dsBankRoll.Tables[0].Rows.Clear();

			dsBankRoll.Tables[0].Columns[0].ColumnName = "bankrollid";
			dsBankRoll.Tables[0].Columns[1].ColumnName = "startdt";
			dsBankRoll.Tables[0].Columns[2].ColumnName = "enddt";
			dsBankRoll.Tables[0].Columns[3].ColumnName = "netchangeamt";
			dsBankRoll.Tables[0].Columns[4].ColumnName = "nettimeamt";
			dsBankRoll.Tables[0].Columns[5].ColumnName = "typecd";
			dsBankRoll.Tables[0].Columns[6].ColumnName = "gametypcd";
			dsBankRoll.Tables[0].Columns[7].ColumnName = "bettypcd";				
			dsBankRoll.Tables[0].Columns[8].ColumnName = "bbamt";
			dsBankRoll.Tables[0].Columns[9].ColumnName = "sitecd";

			dbConnection.Open();
			NpgsqlDataAdapter adapter = new NpgsqlDataAdapter("select bankrollid, startdt, enddt, netchangeamt, nettimeamt, typecd, gametypcd, bettypcd, bbamt, sitecd from playerbankroll order by startdt desc", dbConnection);
			adapter.Fill(dsBankRoll);	

			NpgsqlCommand command = dbConnection.CreateCommand();
			command.CommandText = "select sum(netchangeamt) as total from playerbankroll";
			NpgsqlDataReader reader = command.ExecuteReader();

			decimal total = 0;
			if(reader.Read())
			{
				object o = reader.GetValue(0);
				if (o.GetType().ToString() != "System.DBNull")
					total = reader.GetDecimal(0);
			}

			reader.Close();			

			dbConnection.Close();

			dsBankRoll.Tables[0].Columns[0].ColumnName = "ID";
			dsBankRoll.Tables[0].Columns[1].ColumnName = "Started";
			dsBankRoll.Tables[0].Columns[2].ColumnName = "Ended";
			dsBankRoll.Tables[0].Columns[3].ColumnName = "Net Change ($)";
			dsBankRoll.Tables[0].Columns[4].ColumnName = "Net Change (min)";
			dsBankRoll.Tables[0].Columns[5].ColumnName = "Poker type";
			dsBankRoll.Tables[0].Columns[6].ColumnName = "Game type";
			dsBankRoll.Tables[0].Columns[7].ColumnName = "Bet type";				
			dsBankRoll.Tables[0].Columns[8].ColumnName = "Grouping ($)";
			dsBankRoll.Tables[0].Columns[9].ColumnName = "Site";	

			tbBRTotal.Text = total.ToString();			
		}

		private void ResizeControls()
		{
			System.Drawing.Size mainTabSize = new System.Drawing.Size(Size.Width - 9, Size.Height - 47);
			tabMain.Size = mainTabSize;

			System.Drawing.Size reportTabSize = new System.Drawing.Size(Size.Width - 17, Size.Height - 104);
			tabReportTabs.Size = reportTabSize;		
		
			System.Drawing.Size reportWindowSize = new System.Drawing.Size(Size.Width - 26, Size.Height - 130);
			axWebBrowser1.Size = reportWindowSize;	
			axWebBrowser2.Size = reportWindowSize;	
			axWebBrowser3.Size = reportWindowSize;	
			axWebBrowser4.Size = reportWindowSize;	
			axWebBrowser5.Size = reportWindowSize;	
		}
	

		private void MainForm_Move(object sender, System.EventArgs e)
		{
			ResizeControls();
		}

		private void MainForm_SizeChanged(object sender, System.EventArgs e)
		{
			ResizeControls();
		}

		private void MainForm_Resize(object sender, System.EventArgs e)
		{
			ResizeControls();
		}

		private void btnBRClear_Click(object sender, System.EventArgs e)
		{
			BRClearEntry();
		}
	}
}


