namespace Idea.ERMT.UserControls
{
    partial class RiskMapping
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RiskMapping));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcRiskMapping = new System.Windows.Forms.TabControl();
            this.tpMap = new System.Windows.Forms.TabPage();
            this.tlpMap = new System.Windows.Forms.TableLayoutPanel();
            this.tlpScroll = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMapSettingsSaveSettings = new System.Windows.Forms.Label();
            this.lbSavedReports = new System.Windows.Forms.ListBox();
            this.btnLoadMapSettings = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteMapSettings = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveMapSettings = new System.Windows.Forms.Button();
            this.txtMapSettingsName = new System.Windows.Forms.TextBox();
            this.lblMapName = new System.Windows.Forms.Label();
            this.tlpMapSettings = new System.Windows.Forms.TableLayoutPanel();
            this.lblColor = new System.Windows.Forms.Label();
            this.tlpColors = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSelectedColor = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlColorScheme = new System.Windows.Forms.Panel();
            this.cbMapColorScheme = new System.Windows.Forms.ComboBox();
            this.btnPickColorCustom = new System.Windows.Forms.Button();
            this.lblMapSettingsColorScheme = new System.Windows.Forms.Label();
            this.tlpRegions = new System.Windows.Forms.TableLayoutPanel();
            this.lblMapSettingsRegions = new System.Windows.Forms.Label();
            this.tvMapRegions = new System.Windows.Forms.TreeView();
            this.cmRegions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTBFSelectAllChildRegions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTBFSelectAllRegionsThisLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTBFDeselectAllChildRegions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTBFDeselectAllRegionsThisLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMapDates = new System.Windows.Forms.TableLayoutPanel();
            this.btnMapAllAvailableDates = new System.Windows.Forms.Button();
            this.tlpMapDatesRange = new System.Windows.Forms.TableLayoutPanel();
            this.dtpMapDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpMapDateTo = new System.Windows.Forms.DateTimePicker();
            this.lblMapSettingsDate = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMapSettings = new System.Windows.Forms.Label();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMapSettingsMarkerTypes = new System.Windows.Forms.Label();
            this.flpMarkerType = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.tbMarkerSize = new System.Windows.Forms.TrackBar();
            this.label17 = new System.Windows.Forms.Label();
            this.tlpMapFactors = new System.Windows.Forms.TableLayoutPanel();
            this.flpMapFactors = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMapSettingsFactors = new System.Windows.Forms.Label();
            this.tsMapOptions = new System.Windows.Forms.ToolStrip();
            this.tsbWorld = new System.Windows.Forms.ToolStripButton();
            this.tsbContinent = new System.Windows.Forms.ToolStripButton();
            this.tsbCountry = new System.Windows.Forms.ToolStripButton();
            this.tsbProvinces = new System.Windows.Forms.ToolStripButton();
            this.tsb1stAdmin = new System.Windows.Forms.ToolStripButton();
            this.tsb2ndAdmin = new System.Windows.Forms.ToolStripButton();
            this.tsb3rdAdmin = new System.Windows.Forms.ToolStripButton();
            this.tsb4thAdmin = new System.Windows.Forms.ToolStripButton();
            this.tsbMarkers = new System.Windows.Forms.ToolStripButton();
            this.tsbPaths = new System.Windows.Forms.ToolStripButton();
            this.tsbPOI = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnTest = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.mapZoom1 = new Idea.ERMT.UserControls.MapZoom();
            this.winformsMap1 = new ThinkGeo.MapSuite.DesktopEdition.WinformsMap();
            this.tpCharting = new System.Windows.Forms.TabPage();
            this.tlpChartMain = new System.Windows.Forms.TableLayoutPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiChartSaveAsImage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiChartShowGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChartShowLegends = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpChartScroll = new System.Windows.Forms.TableLayoutPanel();
            this.tlpChartDates = new System.Windows.Forms.TableLayoutPanel();
            this.btnChartAllAvailableDates = new System.Windows.Forms.Button();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpChartDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpChartDateTo = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel22 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tlpChartType = new System.Windows.Forms.TableLayoutPanel();
            this.btnColumn = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnSpline = new System.Windows.Forms.Button();
            this.btnSplineArea = new System.Windows.Forms.Button();
            this.btnFastLine = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tvChartRegions = new System.Windows.Forms.TreeView();
            this.lblSelectRegionsToBeDraw = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.flpChartFactors = new System.Windows.Forms.FlowLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.flpFactorCombination = new System.Windows.Forms.FlowLayoutPanel();
            this.rbEachFactor = new System.Windows.Forms.RadioButton();
            this.rbRVofRegions = new System.Windows.Forms.RadioButton();
            this.rbRVofFactors = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel20 = new System.Windows.Forms.TableLayoutPanel();
            this.lblChartSettingsSave = new System.Windows.Forms.Label();
            this.lbSavedReportsCharting = new System.Windows.Forms.ListBox();
            this.btnLoadChartSettings = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnDeleteChartSettings = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveChartSettings = new System.Windows.Forms.Button();
            this.txtChartSettingsName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lblChartSettings = new System.Windows.Forms.Label();
            this.tpTableByRegion = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSelectARegion = new System.Windows.Forms.Label();
            this.tvSelectRegionTableByRegion = new System.Windows.Forms.TreeView();
            this.dgvTableByRegion = new Idea.ERMT.UserControls.DataGridViewExtended();
            this.cmsTableByRegionGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTableByRegion_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTableByRegion_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTableByRegion_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTableByRegion_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.btnExportToExcel1 = new System.Windows.Forms.Button();
            this.btnAddDateTableByRegion = new System.Windows.Forms.Button();
            this.btnExportToExcel3 = new System.Windows.Forms.Button();
            this.btnRegionDeleteDate = new System.Windows.Forms.Button();
            this.tpTableByFactors = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.btnFactorDeleteDate = new System.Windows.Forms.Button();
            this.btnExportToExcel2 = new System.Windows.Forms.Button();
            this.btnAddDateTableByFactor = new System.Windows.Forms.Button();
            this.btnExportToExcel4 = new System.Windows.Forms.Button();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.tvSelectFactorTableByFactor = new System.Windows.Forms.TreeView();
            this.label16 = new System.Windows.Forms.Label();
            this.tvSelectRegionsTableByFactor = new System.Windows.Forms.TreeView();
            this.cmTableByFactorRegions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSelectAllChildRegions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSelectAllRegionsThisLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDeselectAllChildRegions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeselectAllRegionsThisLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvTableByFactor = new Idea.ERMT.UserControls.DataGridViewExtended();
            this.cmsTableByFactorsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTableByFactor_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTableByFactor_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTableByFactor_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTableByFactor_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.tpStaticMarkers = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDeleteMarker = new System.Windows.Forms.Button();
            this.btnExportToExcelMarker = new System.Windows.Forms.Button();
            this.btnAddMarker = new System.Windows.Forms.Button();
            this.dgvStaticMarkers = new Idea.ERMT.UserControls.DataGridViewExtended();
            this.DateFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarkerType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmStaticMarkers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiStaticMarker_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStaticMarker_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiStaticMarker_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStaticMarker_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.tpFactorsInModel = new System.Windows.Forms.TabPage();
            this.tpRiskAndAction = new System.Windows.Forms.TabPage();
            this.cmMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiMapAddMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapEditMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapDeleteMarker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapMarkerTitles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapMarkerLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMapRegionName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapDisplayDataValue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapFactorLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapCumulativeFactorLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapCumulativeFactorSize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapCumulativeFactorSize_Small = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapCumulativeFactorSize_Medium = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapCumulativeFactorSize_Large = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapCumulativeFactorSize_ExtraLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMapSaveAsImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapSaveAsHighResolutionImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveAsKML = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiMapShowGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMapShowZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.cdSelectMapColor = new System.Windows.Forms.ColorDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tcRiskMapping.SuspendLayout();
            this.tpMap.SuspendLayout();
            this.tlpMap.SuspendLayout();
            this.tlpScroll.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlpMapSettings.SuspendLayout();
            this.tlpColors.SuspendLayout();
            this.pnlColorScheme.SuspendLayout();
            this.tlpRegions.SuspendLayout();
            this.cmRegions.SuspendLayout();
            this.tlpMapDates.SuspendLayout();
            this.tlpMapDatesRange.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMarkerSize)).BeginInit();
            this.tlpMapFactors.SuspendLayout();
            this.tsMapOptions.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tpCharting.SuspendLayout();
            this.tlpChartMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.cmChart.SuspendLayout();
            this.tlpChartScroll.SuspendLayout();
            this.tlpChartDates.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tableLayoutPanel22.SuspendLayout();
            this.tlpChartType.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.flpFactorCombination.SuspendLayout();
            this.tableLayoutPanel20.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel21.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tpTableByRegion.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableByRegion)).BeginInit();
            this.cmsTableByRegionGrid.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tpTableByFactors.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.cmTableByFactorRegions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableByFactor)).BeginInit();
            this.cmsTableByFactorsGrid.SuspendLayout();
            this.tpStaticMarkers.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStaticMarkers)).BeginInit();
            this.cmStaticMarkers.SuspendLayout();
            this.cmMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcRiskMapping
            // 
            resources.ApplyResources(this.tcRiskMapping, "tcRiskMapping");
            this.tcRiskMapping.Controls.Add(this.tpMap);
            this.tcRiskMapping.Controls.Add(this.tpCharting);
            this.tcRiskMapping.Controls.Add(this.tpTableByRegion);
            this.tcRiskMapping.Controls.Add(this.tpTableByFactors);
            this.tcRiskMapping.Controls.Add(this.tpStaticMarkers);
            this.tcRiskMapping.Controls.Add(this.tpFactorsInModel);
            this.tcRiskMapping.Controls.Add(this.tpRiskAndAction);
            this.tcRiskMapping.Name = "tcRiskMapping";
            this.tcRiskMapping.SelectedIndex = 0;
            this.tcRiskMapping.SelectedIndexChanged += new System.EventHandler(this.tcRiskMapping_SelectedIndexChanged);
            this.tcRiskMapping.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tcRiskMapping_KeyUp);
            // 
            // tpMap
            // 
            this.tpMap.Controls.Add(this.tlpMap);
            resources.ApplyResources(this.tpMap, "tpMap");
            this.tpMap.Name = "tpMap";
            this.tpMap.UseVisualStyleBackColor = true;
            // 
            // tlpMap
            // 
            this.tlpMap.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.tlpMap, "tlpMap");
            this.tlpMap.Controls.Add(this.tlpScroll, 0, 0);
            this.tlpMap.Controls.Add(this.tsMapOptions, 1, 0);
            this.tlpMap.Controls.Add(this.panel3, 1, 1);
            this.tlpMap.Name = "tlpMap";
            // 
            // tlpScroll
            // 
            resources.ApplyResources(this.tlpScroll, "tlpScroll");
            this.tlpScroll.BackColor = System.Drawing.SystemColors.Control;
            this.tlpScroll.Controls.Add(this.tableLayoutPanel2, 0, 6);
            this.tlpScroll.Controls.Add(this.tlpMapSettings, 0, 5);
            this.tlpScroll.Controls.Add(this.tlpRegions, 0, 3);
            this.tlpScroll.Controls.Add(this.tlpMapDates, 0, 1);
            this.tlpScroll.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tlpScroll.Controls.Add(this.tableLayoutPanel11, 0, 4);
            this.tlpScroll.Controls.Add(this.tlpMapFactors, 0, 2);
            this.tlpScroll.Name = "tlpScroll";
            this.tlpMap.SetRowSpan(this.tlpScroll, 3);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.lblMapSettingsSaveSettings, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbSavedReports, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnLoadMapSettings, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnDeleteMapSettings, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // lblMapSettingsSaveSettings
            // 
            resources.ApplyResources(this.lblMapSettingsSaveSettings, "lblMapSettingsSaveSettings");
            this.tableLayoutPanel2.SetColumnSpan(this.lblMapSettingsSaveSettings, 2);
            this.lblMapSettingsSaveSettings.Name = "lblMapSettingsSaveSettings";
            this.lblMapSettingsSaveSettings.Tag = "6";
            this.toolTip1.SetToolTip(this.lblMapSettingsSaveSettings, resources.GetString("lblMapSettingsSaveSettings.ToolTip"));
            // 
            // lbSavedReports
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.lbSavedReports, 2);
            resources.ApplyResources(this.lbSavedReports, "lbSavedReports");
            this.lbSavedReports.FormattingEnabled = true;
            this.lbSavedReports.Name = "lbSavedReports";
            this.lbSavedReports.SelectedIndexChanged += new System.EventHandler(this.lbSavedReports_SelectedIndexChanged);
            // 
            // btnLoadMapSettings
            // 
            resources.ApplyResources(this.btnLoadMapSettings, "btnLoadMapSettings");
            this.btnLoadMapSettings.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnLoadMapSettings.Name = "btnLoadMapSettings";
            this.btnLoadMapSettings.Tag = "1";
            this.btnLoadMapSettings.UseVisualStyleBackColor = true;
            this.btnLoadMapSettings.Click += new System.EventHandler(this.btnLoadMapAndChartSettings_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnDeleteMapSettings
            // 
            resources.ApplyResources(this.btnDeleteMapSettings, "btnDeleteMapSettings");
            this.btnDeleteMapSettings.Name = "btnDeleteMapSettings";
            this.btnDeleteMapSettings.Tag = "1";
            this.btnDeleteMapSettings.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btnSaveMapSettings, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMapSettingsName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMapName, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // btnSaveMapSettings
            // 
            resources.ApplyResources(this.btnSaveMapSettings, "btnSaveMapSettings");
            this.btnSaveMapSettings.Name = "btnSaveMapSettings";
            this.btnSaveMapSettings.Tag = "1";
            this.btnSaveMapSettings.UseVisualStyleBackColor = true;
            this.btnSaveMapSettings.Click += new System.EventHandler(this.btnSaveMapAndChartSettings_Click);
            // 
            // txtMapSettingsName
            // 
            resources.ApplyResources(this.txtMapSettingsName, "txtMapSettingsName");
            this.txtMapSettingsName.Name = "txtMapSettingsName";
            // 
            // lblMapName
            // 
            resources.ApplyResources(this.lblMapName, "lblMapName");
            this.lblMapName.Name = "lblMapName";
            // 
            // tlpMapSettings
            // 
            resources.ApplyResources(this.tlpMapSettings, "tlpMapSettings");
            this.tlpMapSettings.Controls.Add(this.lblColor, 0, 1);
            this.tlpMapSettings.Controls.Add(this.tlpColors, 0, 2);
            this.tlpMapSettings.Controls.Add(this.label4, 0, 3);
            this.tlpMapSettings.Controls.Add(this.label6, 0, 4);
            this.tlpMapSettings.Controls.Add(this.pnlColorScheme, 0, 5);
            this.tlpMapSettings.Controls.Add(this.lblMapSettingsColorScheme, 0, 0);
            this.tlpMapSettings.Name = "tlpMapSettings";
            // 
            // lblColor
            // 
            resources.ApplyResources(this.lblColor, "lblColor");
            this.lblColor.Name = "lblColor";
            // 
            // tlpColors
            // 
            resources.ApplyResources(this.tlpColors, "tlpColors");
            this.tlpColors.Controls.Add(this.pnlSelectedColor, 1, 0);
            this.tlpColors.Name = "tlpColors";
            // 
            // pnlSelectedColor
            // 
            this.pnlSelectedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pnlSelectedColor, "pnlSelectedColor");
            this.pnlSelectedColor.Name = "pnlSelectedColor";
            this.toolTip1.SetToolTip(this.pnlSelectedColor, resources.GetString("pnlSelectedColor.ToolTip"));
            this.pnlSelectedColor.Click += new System.EventHandler(this.pnlSelectedColor_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // pnlColorScheme
            // 
            this.pnlColorScheme.Controls.Add(this.cbMapColorScheme);
            this.pnlColorScheme.Controls.Add(this.btnPickColorCustom);
            resources.ApplyResources(this.pnlColorScheme, "pnlColorScheme");
            this.pnlColorScheme.Name = "pnlColorScheme";
            // 
            // cbMapColorScheme
            // 
            this.cbMapColorScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMapColorScheme.FormattingEnabled = true;
            this.cbMapColorScheme.Items.AddRange(new object[] {
            resources.GetString("cbMapColorScheme.Items"),
            resources.GetString("cbMapColorScheme.Items1"),
            resources.GetString("cbMapColorScheme.Items2"),
            resources.GetString("cbMapColorScheme.Items3"),
            resources.GetString("cbMapColorScheme.Items4")});
            resources.ApplyResources(this.cbMapColorScheme, "cbMapColorScheme");
            this.cbMapColorScheme.Name = "cbMapColorScheme";
            this.cbMapColorScheme.SelectedIndexChanged += new System.EventHandler(this.cbMapColorScheme_SelectedIndexChanged);
            // 
            // btnPickColorCustom
            // 
            resources.ApplyResources(this.btnPickColorCustom, "btnPickColorCustom");
            this.btnPickColorCustom.Image = global::Idea.ERMT.Properties.Resources._32x32_brush;
            this.btnPickColorCustom.Name = "btnPickColorCustom";
            this.btnPickColorCustom.UseVisualStyleBackColor = true;
            this.btnPickColorCustom.Click += new System.EventHandler(this.btnPickColorCustom_Click);
            // 
            // lblMapSettingsColorScheme
            // 
            resources.ApplyResources(this.lblMapSettingsColorScheme, "lblMapSettingsColorScheme");
            this.lblMapSettingsColorScheme.Name = "lblMapSettingsColorScheme";
            this.lblMapSettingsColorScheme.Tag = "5";
            this.toolTip1.SetToolTip(this.lblMapSettingsColorScheme, resources.GetString("lblMapSettingsColorScheme.ToolTip"));
            this.lblMapSettingsColorScheme.Click += new System.EventHandler(this.lblMapSettingsSection_Click);
            // 
            // tlpRegions
            // 
            resources.ApplyResources(this.tlpRegions, "tlpRegions");
            this.tlpRegions.Controls.Add(this.lblMapSettingsRegions, 0, 0);
            this.tlpRegions.Controls.Add(this.tvMapRegions, 0, 1);
            this.tlpRegions.Name = "tlpRegions";
            // 
            // lblMapSettingsRegions
            // 
            resources.ApplyResources(this.lblMapSettingsRegions, "lblMapSettingsRegions");
            this.lblMapSettingsRegions.Name = "lblMapSettingsRegions";
            this.lblMapSettingsRegions.Tag = "3";
            this.toolTip1.SetToolTip(this.lblMapSettingsRegions, resources.GetString("lblMapSettingsRegions.ToolTip"));
            this.lblMapSettingsRegions.Click += new System.EventHandler(this.lblMapSettingsSection_Click);
            // 
            // tvMapRegions
            // 
            this.tvMapRegions.CheckBoxes = true;
            this.tvMapRegions.ContextMenuStrip = this.cmRegions;
            resources.ApplyResources(this.tvMapRegions, "tvMapRegions");
            this.tvMapRegions.HideSelection = false;
            this.tvMapRegions.Name = "tvMapRegions";
            this.tvMapRegions.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvMapRegions_AfterCheck);
            this.tvMapRegions.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMapRegions_NodeMouseClick);
            // 
            // cmRegions
            // 
            this.cmRegions.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.cmRegions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTBFSelectAllChildRegions,
            this.tsmiTBFSelectAllRegionsThisLevel,
            this.toolStripSeparator4,
            this.tsmiTBFDeselectAllChildRegions,
            this.tsmiTBFDeselectAllRegionsThisLevel});
            this.cmRegions.Name = "contextMenuStrip2";
            resources.ApplyResources(this.cmRegions, "cmRegions");
            this.cmRegions.Opening += new System.ComponentModel.CancelEventHandler(this.cmRegions_Opening);
            this.cmRegions.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmRegions_ItemClicked);
            // 
            // tsmiTBFSelectAllChildRegions
            // 
            this.tsmiTBFSelectAllChildRegions.Name = "tsmiTBFSelectAllChildRegions";
            resources.ApplyResources(this.tsmiTBFSelectAllChildRegions, "tsmiTBFSelectAllChildRegions");
            // 
            // tsmiTBFSelectAllRegionsThisLevel
            // 
            this.tsmiTBFSelectAllRegionsThisLevel.Name = "tsmiTBFSelectAllRegionsThisLevel";
            resources.ApplyResources(this.tsmiTBFSelectAllRegionsThisLevel, "tsmiTBFSelectAllRegionsThisLevel");
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // tsmiTBFDeselectAllChildRegions
            // 
            this.tsmiTBFDeselectAllChildRegions.Name = "tsmiTBFDeselectAllChildRegions";
            resources.ApplyResources(this.tsmiTBFDeselectAllChildRegions, "tsmiTBFDeselectAllChildRegions");
            // 
            // tsmiTBFDeselectAllRegionsThisLevel
            // 
            this.tsmiTBFDeselectAllRegionsThisLevel.Name = "tsmiTBFDeselectAllRegionsThisLevel";
            resources.ApplyResources(this.tsmiTBFDeselectAllRegionsThisLevel, "tsmiTBFDeselectAllRegionsThisLevel");
            // 
            // tlpMapDates
            // 
            resources.ApplyResources(this.tlpMapDates, "tlpMapDates");
            this.tlpMapDates.Controls.Add(this.btnMapAllAvailableDates, 0, 3);
            this.tlpMapDates.Controls.Add(this.tlpMapDatesRange, 0, 2);
            this.tlpMapDates.Controls.Add(this.lblMapSettingsDate, 0, 0);
            this.tlpMapDates.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tlpMapDates.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpMapDates.Name = "tlpMapDates";
            // 
            // btnMapAllAvailableDates
            // 
            resources.ApplyResources(this.btnMapAllAvailableDates, "btnMapAllAvailableDates");
            this.btnMapAllAvailableDates.Name = "btnMapAllAvailableDates";
            this.btnMapAllAvailableDates.UseVisualStyleBackColor = true;
            this.btnMapAllAvailableDates.Click += new System.EventHandler(this.btnMapAllAvailableDates_Click);
            // 
            // tlpMapDatesRange
            // 
            resources.ApplyResources(this.tlpMapDatesRange, "tlpMapDatesRange");
            this.tlpMapDatesRange.Controls.Add(this.dtpMapDateFrom, 0, 0);
            this.tlpMapDatesRange.Controls.Add(this.dtpMapDateTo, 1, 0);
            this.tlpMapDatesRange.Name = "tlpMapDatesRange";
            // 
            // dtpMapDateFrom
            // 
            resources.ApplyResources(this.dtpMapDateFrom, "dtpMapDateFrom");
            this.dtpMapDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMapDateFrom.MaxDate = new System.DateTime(2049, 12, 31, 0, 0, 0, 0);
            this.dtpMapDateFrom.MinDate = new System.DateTime(1951, 1, 1, 0, 0, 0, 0);
            this.dtpMapDateFrom.Name = "dtpMapDateFrom";
            this.dtpMapDateFrom.ValueChanged += new System.EventHandler(this.dtpMapDataDate_ValueChanged);
            // 
            // dtpMapDateTo
            // 
            resources.ApplyResources(this.dtpMapDateTo, "dtpMapDateTo");
            this.dtpMapDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMapDateTo.MaxDate = new System.DateTime(2049, 12, 31, 0, 0, 0, 0);
            this.dtpMapDateTo.MinDate = new System.DateTime(1951, 1, 1, 0, 0, 0, 0);
            this.dtpMapDateTo.Name = "dtpMapDateTo";
            this.dtpMapDateTo.ValueChanged += new System.EventHandler(this.dtpMapDataDate_ValueChanged);
            // 
            // lblMapSettingsDate
            // 
            resources.ApplyResources(this.lblMapSettingsDate, "lblMapSettingsDate");
            this.lblMapSettingsDate.Name = "lblMapSettingsDate";
            this.lblMapSettingsDate.Tag = "1";
            this.toolTip1.SetToolTip(this.lblMapSettingsDate, resources.GetString("lblMapSettingsDate.ToolTip"));
            this.lblMapSettingsDate.Click += new System.EventHandler(this.lblMapSettingsSection_Click);
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.lblDateTo, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            // 
            // lblDateTo
            // 
            resources.ApplyResources(this.lblDateTo, "lblDateTo");
            this.lblDateTo.Name = "lblDateTo";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.lblMapSettings, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // lblMapSettings
            // 
            resources.ApplyResources(this.lblMapSettings, "lblMapSettings");
            this.lblMapSettings.Name = "lblMapSettings";
            // 
            // tableLayoutPanel11
            // 
            resources.ApplyResources(this.tableLayoutPanel11, "tableLayoutPanel11");
            this.tableLayoutPanel11.Controls.Add(this.lblMapSettingsMarkerTypes, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.flpMarkerType, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.tableLayoutPanel17, 0, 2);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            // 
            // lblMapSettingsMarkerTypes
            // 
            resources.ApplyResources(this.lblMapSettingsMarkerTypes, "lblMapSettingsMarkerTypes");
            this.lblMapSettingsMarkerTypes.Name = "lblMapSettingsMarkerTypes";
            this.lblMapSettingsMarkerTypes.Tag = "4";
            this.toolTip1.SetToolTip(this.lblMapSettingsMarkerTypes, resources.GetString("lblMapSettingsMarkerTypes.ToolTip"));
            this.lblMapSettingsMarkerTypes.Click += new System.EventHandler(this.lblMapSettingsSection_Click);
            // 
            // flpMarkerType
            // 
            resources.ApplyResources(this.flpMarkerType, "flpMarkerType");
            this.flpMarkerType.Name = "flpMarkerType";
            // 
            // tableLayoutPanel17
            // 
            resources.ApplyResources(this.tableLayoutPanel17, "tableLayoutPanel17");
            this.tableLayoutPanel17.Controls.Add(this.tbMarkerSize, 1, 0);
            this.tableLayoutPanel17.Controls.Add(this.label17, 0, 0);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            // 
            // tbMarkerSize
            // 
            resources.ApplyResources(this.tbMarkerSize, "tbMarkerSize");
            this.tbMarkerSize.LargeChange = 1;
            this.tbMarkerSize.Maximum = 14;
            this.tbMarkerSize.Minimum = 1;
            this.tbMarkerSize.Name = "tbMarkerSize";
            this.tbMarkerSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbMarkerSize.Value = 7;
            this.tbMarkerSize.ValueChanged += new System.EventHandler(this.tbMarkerSize_ValueChanged);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // tlpMapFactors
            // 
            resources.ApplyResources(this.tlpMapFactors, "tlpMapFactors");
            this.tlpMapFactors.Controls.Add(this.flpMapFactors, 0, 1);
            this.tlpMapFactors.Controls.Add(this.lblMapSettingsFactors, 0, 0);
            this.tlpMapFactors.Name = "tlpMapFactors";
            // 
            // flpMapFactors
            // 
            resources.ApplyResources(this.flpMapFactors, "flpMapFactors");
            this.flpMapFactors.Name = "flpMapFactors";
            // 
            // lblMapSettingsFactors
            // 
            resources.ApplyResources(this.lblMapSettingsFactors, "lblMapSettingsFactors");
            this.lblMapSettingsFactors.Name = "lblMapSettingsFactors";
            this.lblMapSettingsFactors.Tag = "2";
            this.toolTip1.SetToolTip(this.lblMapSettingsFactors, resources.GetString("lblMapSettingsFactors.ToolTip"));
            this.lblMapSettingsFactors.Click += new System.EventHandler(this.lblMapSettingsSection_Click);
            // 
            // tsMapOptions
            // 
            this.tsMapOptions.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.tsMapOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbWorld,
            this.tsbContinent,
            this.tsbCountry,
            this.tsbProvinces,
            this.tsb1stAdmin,
            this.tsb2ndAdmin,
            this.tsb3rdAdmin,
            this.tsb4thAdmin,
            this.tsbMarkers,
            this.tsbPaths,
            this.tsbPOI,
            this.btnRefresh,
            this.btnTest});
            resources.ApplyResources(this.tsMapOptions, "tsMapOptions");
            this.tsMapOptions.Name = "tsMapOptions";
            // 
            // tsbWorld
            // 
            this.tsbWorld.CheckOnClick = true;
            this.tsbWorld.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbWorld.Image = global::Idea.ERMT.Properties.Resources._32x32_mapping;
            resources.ApplyResources(this.tsbWorld, "tsbWorld");
            this.tsbWorld.Name = "tsbWorld";
            this.tsbWorld.Tag = "0";
            this.tsbWorld.Click += new System.EventHandler(this.tsbRegionLevel_Click);
            // 
            // tsbContinent
            // 
            this.tsbContinent.CheckOnClick = true;
            this.tsbContinent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbContinent.Image = global::Idea.ERMT.Properties.Resources._32x32_mapping_continente;
            resources.ApplyResources(this.tsbContinent, "tsbContinent");
            this.tsbContinent.Name = "tsbContinent";
            this.tsbContinent.Tag = "1";
            this.tsbContinent.Click += new System.EventHandler(this.tsbRegionLevel_Click);
            // 
            // tsbCountry
            // 
            this.tsbCountry.CheckOnClick = true;
            this.tsbCountry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCountry.Image = global::Idea.ERMT.Properties.Resources._32x32_mapping_country;
            resources.ApplyResources(this.tsbCountry, "tsbCountry");
            this.tsbCountry.Name = "tsbCountry";
            this.tsbCountry.Tag = "2";
            this.tsbCountry.Click += new System.EventHandler(this.tsbRegionLevel_Click);
            // 
            // tsbProvinces
            // 
            this.tsbProvinces.CheckOnClick = true;
            this.tsbProvinces.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbProvinces.Image = global::Idea.ERMT.Properties.Resources._32x32_mapping_province;
            resources.ApplyResources(this.tsbProvinces, "tsbProvinces");
            this.tsbProvinces.Name = "tsbProvinces";
            this.tsbProvinces.Tag = "3";
            this.tsbProvinces.Click += new System.EventHandler(this.tsbRegionLevel_Click);
            // 
            // tsb1stAdmin
            // 
            this.tsb1stAdmin.CheckOnClick = true;
            this.tsb1stAdmin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsb1stAdmin, "tsb1stAdmin");
            this.tsb1stAdmin.Name = "tsb1stAdmin";
            this.tsb1stAdmin.Tag = "4";
            this.tsb1stAdmin.Click += new System.EventHandler(this.tsbRegionLevel_Click);
            // 
            // tsb2ndAdmin
            // 
            this.tsb2ndAdmin.CheckOnClick = true;
            this.tsb2ndAdmin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb2ndAdmin.Image = global::Idea.ERMT.Properties.Resources._32x32_mapping_numerado5;
            resources.ApplyResources(this.tsb2ndAdmin, "tsb2ndAdmin");
            this.tsb2ndAdmin.Name = "tsb2ndAdmin";
            this.tsb2ndAdmin.Tag = "5";
            this.tsb2ndAdmin.Click += new System.EventHandler(this.tsbRegionLevel_Click);
            // 
            // tsb3rdAdmin
            // 
            this.tsb3rdAdmin.CheckOnClick = true;
            this.tsb3rdAdmin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb3rdAdmin.Image = global::Idea.ERMT.Properties.Resources._32x32_mapping_numerado6;
            resources.ApplyResources(this.tsb3rdAdmin, "tsb3rdAdmin");
            this.tsb3rdAdmin.Name = "tsb3rdAdmin";
            this.tsb3rdAdmin.Tag = "6";
            this.tsb3rdAdmin.Click += new System.EventHandler(this.tsbRegionLevel_Click);
            // 
            // tsb4thAdmin
            // 
            this.tsb4thAdmin.CheckOnClick = true;
            this.tsb4thAdmin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb4thAdmin.Image = global::Idea.ERMT.Properties.Resources._32x32_mapping_numerado7;
            resources.ApplyResources(this.tsb4thAdmin, "tsb4thAdmin");
            this.tsb4thAdmin.Name = "tsb4thAdmin";
            this.tsb4thAdmin.Tag = "7";
            this.tsb4thAdmin.Click += new System.EventHandler(this.tsbRegionLevel_Click);
            // 
            // tsbMarkers
            // 
            this.tsbMarkers.CheckOnClick = true;
            this.tsbMarkers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMarkers.Image = global::Idea.ERMT.Properties.Resources._32x32_pin_marker;
            resources.ApplyResources(this.tsbMarkers, "tsbMarkers");
            this.tsbMarkers.Name = "tsbMarkers";
            this.tsbMarkers.Tag = "8";
            this.tsbMarkers.Click += new System.EventHandler(this.tsbMarkers_Click);
            // 
            // tsbPaths
            // 
            this.tsbPaths.CheckOnClick = true;
            this.tsbPaths.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaths.Image = global::Idea.ERMT.Properties.Resources._32x32_path;
            resources.ApplyResources(this.tsbPaths, "tsbPaths");
            this.tsbPaths.Name = "tsbPaths";
            this.tsbPaths.Tag = "9";
            this.tsbPaths.Click += new System.EventHandler(this.tsbPathAndPOI_Click);
            // 
            // tsbPOI
            // 
            this.tsbPOI.CheckOnClick = true;
            this.tsbPOI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbPOI, "tsbPOI");
            this.tsbPOI.Name = "tsbPOI";
            this.tsbPOI.Tag = "10";
            this.tsbPOI.Click += new System.EventHandler(this.tsbPathAndPOI_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Idea.ERMT.Properties.Resources._32x32_refresh;
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnTest
            // 
            this.btnTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.Name = "btnTest";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.mapZoom1);
            this.panel3.Controls.Add(this.winformsMap1);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            this.tlpMap.SetRowSpan(this.panel3, 2);
            // 
            // mapZoom1
            // 
            this.mapZoom1.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.mapZoom1, "mapZoom1");
            this.mapZoom1.Name = "mapZoom1";
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790D;
            resources.ApplyResources(this.winformsMap1, "winformsMap1");
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Core.DrawingQuality.Default;
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.DesktopEdition.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Core.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.Core.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000D;
            this.winformsMap1.MinimumScale = 200D;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.DesktopEdition.MapThreadingMode.Default;
            this.toolTip1.SetToolTip(this.winformsMap1, resources.GetString("winformsMap1.ToolTip"));
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.DesktopEdition.ZoomLevelSnappingMode.None;
            this.winformsMap1.MapClick += new System.EventHandler<ThinkGeo.MapSuite.DesktopEdition.MapClickWinformsMapEventArgs>(this.winformsMap1_MapClick);
            this.winformsMap1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.winformsMap1_MouseDoubleClick);
            // 
            // tpCharting
            // 
            this.tpCharting.Controls.Add(this.tlpChartMain);
            resources.ApplyResources(this.tpCharting, "tpCharting");
            this.tpCharting.Name = "tpCharting";
            this.tpCharting.UseVisualStyleBackColor = true;
            // 
            // tlpChartMain
            // 
            resources.ApplyResources(this.tlpChartMain, "tlpChartMain");
            this.tlpChartMain.Controls.Add(this.chart1, 1, 0);
            this.tlpChartMain.Controls.Add(this.tlpChartScroll, 0, 0);
            this.tlpChartMain.Name = "tlpChartMain";
            // 
            // chart1
            // 
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.Inclination = 10;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Area3DStyle.PointDepth = 40;
            chartArea1.Area3DStyle.PointGapDepth = 20;
            chartArea1.Area3DStyle.Rotation = 20;
            chartArea1.Area3DStyle.WallWidth = 2;
            chartArea1.AxisX.LineWidth = 2;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.LineWidth = 2;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.SeaShell;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.DarkDownwardDiagonal;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.Gray;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = System.Drawing.Color.White;
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ContextMenuStrip = this.cmChart;
            resources.ApplyResources(this.chart1, "chart1");
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.IsValueShownAsLabel = true;
            series1.LabelToolTip = "Pepe (0)";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            this.chart1.Series.Add(series1);
            this.toolTip1.SetToolTip(this.chart1, resources.GetString("chart1.ToolTip"));
            // 
            // cmChart
            // 
            this.cmChart.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.cmChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChartSaveAsImage,
            this.toolStripSeparator6,
            this.tsmiChartShowGrid,
            this.tsmiChartShowLegends});
            this.cmChart.Name = "chartContextMenuStrip";
            resources.ApplyResources(this.cmChart, "cmChart");
            // 
            // tsmiChartSaveAsImage
            // 
            this.tsmiChartSaveAsImage.Name = "tsmiChartSaveAsImage";
            resources.ApplyResources(this.tsmiChartSaveAsImage, "tsmiChartSaveAsImage");
            this.tsmiChartSaveAsImage.Click += new System.EventHandler(this.tsmiChartSaveAsImage_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // tsmiChartShowGrid
            // 
            this.tsmiChartShowGrid.Checked = true;
            this.tsmiChartShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiChartShowGrid.Name = "tsmiChartShowGrid";
            resources.ApplyResources(this.tsmiChartShowGrid, "tsmiChartShowGrid");
            this.tsmiChartShowGrid.Click += new System.EventHandler(this.tsmiChartShowGrid_Click);
            // 
            // tsmiChartShowLegends
            // 
            this.tsmiChartShowLegends.Name = "tsmiChartShowLegends";
            resources.ApplyResources(this.tsmiChartShowLegends, "tsmiChartShowLegends");
            this.tsmiChartShowLegends.Click += new System.EventHandler(this.tsmiChartShowLegends_Click);
            // 
            // tlpChartScroll
            // 
            resources.ApplyResources(this.tlpChartScroll, "tlpChartScroll");
            this.tlpChartScroll.Controls.Add(this.tlpChartDates, 0, 1);
            this.tlpChartScroll.Controls.Add(this.tlpChartType, 0, 5);
            this.tlpChartScroll.Controls.Add(this.tableLayoutPanel6, 0, 4);
            this.tlpChartScroll.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tlpChartScroll.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tlpChartScroll.Controls.Add(this.tableLayoutPanel20, 0, 6);
            this.tlpChartScroll.Controls.Add(this.tableLayoutPanel8, 0, 0);
            this.tlpChartScroll.Name = "tlpChartScroll";
            // 
            // tlpChartDates
            // 
            resources.ApplyResources(this.tlpChartDates, "tlpChartDates");
            this.tlpChartDates.Controls.Add(this.btnChartAllAvailableDates, 0, 3);
            this.tlpChartDates.Controls.Add(this.tableLayoutPanel16, 0, 2);
            this.tlpChartDates.Controls.Add(this.label8, 0, 0);
            this.tlpChartDates.Controls.Add(this.tableLayoutPanel22, 0, 1);
            this.tlpChartDates.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpChartDates.Name = "tlpChartDates";
            // 
            // btnChartAllAvailableDates
            // 
            resources.ApplyResources(this.btnChartAllAvailableDates, "btnChartAllAvailableDates");
            this.btnChartAllAvailableDates.Name = "btnChartAllAvailableDates";
            this.btnChartAllAvailableDates.UseVisualStyleBackColor = true;
            this.btnChartAllAvailableDates.Click += new System.EventHandler(this.btnChartAllAvailableDates_Click);
            // 
            // tableLayoutPanel16
            // 
            resources.ApplyResources(this.tableLayoutPanel16, "tableLayoutPanel16");
            this.tableLayoutPanel16.Controls.Add(this.dtpChartDateFrom, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.dtpChartDateTo, 1, 0);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            // 
            // dtpChartDateFrom
            // 
            resources.ApplyResources(this.dtpChartDateFrom, "dtpChartDateFrom");
            this.dtpChartDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChartDateFrom.MaxDate = new System.DateTime(2049, 12, 31, 0, 0, 0, 0);
            this.dtpChartDateFrom.MinDate = new System.DateTime(1951, 1, 1, 0, 0, 0, 0);
            this.dtpChartDateFrom.Name = "dtpChartDateFrom";
            this.dtpChartDateFrom.ValueChanged += new System.EventHandler(this.dtpChartDate_ValueChanged);
            // 
            // dtpChartDateTo
            // 
            resources.ApplyResources(this.dtpChartDateTo, "dtpChartDateTo");
            this.dtpChartDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChartDateTo.MaxDate = new System.DateTime(2049, 12, 31, 0, 0, 0, 0);
            this.dtpChartDateTo.MinDate = new System.DateTime(1951, 1, 1, 0, 0, 0, 0);
            this.dtpChartDateTo.Name = "dtpChartDateTo";
            this.dtpChartDateTo.ValueChanged += new System.EventHandler(this.dtpChartDate_ValueChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.label8.Tag = "1";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            this.label8.Click += new System.EventHandler(this.lblChartSettingsSection_Click);
            // 
            // tableLayoutPanel22
            // 
            resources.ApplyResources(this.tableLayoutPanel22, "tableLayoutPanel22");
            this.tableLayoutPanel22.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel22.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel22.Name = "tableLayoutPanel22";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // tlpChartType
            // 
            resources.ApplyResources(this.tlpChartType, "tlpChartType");
            this.tlpChartType.Controls.Add(this.btnColumn, 0, 3);
            this.tlpChartType.Controls.Add(this.label11, 0, 0);
            this.tlpChartType.Controls.Add(this.btnLine, 1, 1);
            this.tlpChartType.Controls.Add(this.btnSpline, 0, 2);
            this.tlpChartType.Controls.Add(this.btnSplineArea, 1, 2);
            this.tlpChartType.Controls.Add(this.btnFastLine, 0, 1);
            this.tlpChartType.Name = "tlpChartType";
            // 
            // btnColumn
            // 
            resources.ApplyResources(this.btnColumn, "btnColumn");
            this.tlpChartType.SetColumnSpan(this.btnColumn, 2);
            this.btnColumn.Name = "btnColumn";
            this.btnColumn.UseVisualStyleBackColor = true;
            this.btnColumn.Click += new System.EventHandler(this.btnChartType_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.label11.Tag = "5";
            this.toolTip1.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            this.label11.Click += new System.EventHandler(this.lblChartSettingsSection_Click);
            // 
            // btnLine
            // 
            resources.ApplyResources(this.btnLine, "btnLine");
            this.btnLine.Name = "btnLine";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnChartType_Click);
            // 
            // btnSpline
            // 
            resources.ApplyResources(this.btnSpline, "btnSpline");
            this.btnSpline.Name = "btnSpline";
            this.btnSpline.UseVisualStyleBackColor = true;
            this.btnSpline.Click += new System.EventHandler(this.btnChartType_Click);
            // 
            // btnSplineArea
            // 
            resources.ApplyResources(this.btnSplineArea, "btnSplineArea");
            this.btnSplineArea.Name = "btnSplineArea";
            this.btnSplineArea.UseVisualStyleBackColor = true;
            this.btnSplineArea.Click += new System.EventHandler(this.btnChartType_Click);
            // 
            // btnFastLine
            // 
            resources.ApplyResources(this.btnFastLine, "btnFastLine");
            this.btnFastLine.Name = "btnFastLine";
            this.btnFastLine.Tag = "";
            this.btnFastLine.UseVisualStyleBackColor = true;
            this.btnFastLine.Click += new System.EventHandler(this.btnChartType_Click);
            // 
            // tableLayoutPanel6
            // 
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.tvChartRegions, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblSelectRegionsToBeDraw, 0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // tvChartRegions
            // 
            this.tvChartRegions.CheckBoxes = true;
            this.tvChartRegions.ContextMenuStrip = this.cmRegions;
            resources.ApplyResources(this.tvChartRegions, "tvChartRegions");
            this.tvChartRegions.Name = "tvChartRegions";
            this.tvChartRegions.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvChartRegions_AfterCheck);
            this.tvChartRegions.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMapRegions_NodeMouseClick);
            // 
            // lblSelectRegionsToBeDraw
            // 
            resources.ApplyResources(this.lblSelectRegionsToBeDraw, "lblSelectRegionsToBeDraw");
            this.lblSelectRegionsToBeDraw.Name = "lblSelectRegionsToBeDraw";
            this.lblSelectRegionsToBeDraw.Tag = "4";
            this.toolTip1.SetToolTip(this.lblSelectRegionsToBeDraw, resources.GetString("lblSelectRegionsToBeDraw.ToolTip"));
            this.lblSelectRegionsToBeDraw.Click += new System.EventHandler(this.lblChartSettingsSection_Click);
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.flpChartFactors, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // flpChartFactors
            // 
            resources.ApplyResources(this.flpChartFactors, "flpChartFactors");
            this.flpChartFactors.Name = "flpChartFactors";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            this.label9.Tag = "3";
            this.toolTip1.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            this.label9.Click += new System.EventHandler(this.lblChartSettingsSection_Click);
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.flpFactorCombination, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // flpFactorCombination
            // 
            this.flpFactorCombination.Controls.Add(this.rbEachFactor);
            this.flpFactorCombination.Controls.Add(this.rbRVofRegions);
            this.flpFactorCombination.Controls.Add(this.rbRVofFactors);
            resources.ApplyResources(this.flpFactorCombination, "flpFactorCombination");
            this.flpFactorCombination.Name = "flpFactorCombination";
            this.toolTip1.SetToolTip(this.flpFactorCombination, resources.GetString("flpFactorCombination.ToolTip"));
            // 
            // rbEachFactor
            // 
            resources.ApplyResources(this.rbEachFactor, "rbEachFactor");
            this.rbEachFactor.Checked = true;
            this.rbEachFactor.Name = "rbEachFactor";
            this.rbEachFactor.TabStop = true;
            this.rbEachFactor.Tag = "1";
            this.rbEachFactor.UseVisualStyleBackColor = true;
            this.rbEachFactor.CheckedChanged += new System.EventHandler(this.rbEachFactor_CheckedChanged);
            // 
            // rbRVofRegions
            // 
            resources.ApplyResources(this.rbRVofRegions, "rbRVofRegions");
            this.rbRVofRegions.Name = "rbRVofRegions";
            this.rbRVofRegions.Tag = "2";
            this.rbRVofRegions.UseVisualStyleBackColor = true;
            this.rbRVofRegions.CheckedChanged += new System.EventHandler(this.rbEachFactor_CheckedChanged);
            // 
            // rbRVofFactors
            // 
            resources.ApplyResources(this.rbRVofFactors, "rbRVofFactors");
            this.rbRVofFactors.Name = "rbRVofFactors";
            this.rbRVofFactors.Tag = "3";
            this.rbRVofFactors.UseVisualStyleBackColor = true;
            this.rbRVofFactors.CheckedChanged += new System.EventHandler(this.rbEachFactor_CheckedChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.label10.Tag = "2";
            this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            this.label10.Click += new System.EventHandler(this.lblChartSettingsSection_Click);
            // 
            // tableLayoutPanel20
            // 
            resources.ApplyResources(this.tableLayoutPanel20, "tableLayoutPanel20");
            this.tableLayoutPanel20.Controls.Add(this.lblChartSettingsSave, 0, 0);
            this.tableLayoutPanel20.Controls.Add(this.lbSavedReportsCharting, 0, 3);
            this.tableLayoutPanel20.Controls.Add(this.btnLoadChartSettings, 0, 4);
            this.tableLayoutPanel20.Controls.Add(this.label13, 0, 2);
            this.tableLayoutPanel20.Controls.Add(this.btnDeleteChartSettings, 1, 4);
            this.tableLayoutPanel20.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel20.Name = "tableLayoutPanel20";
            // 
            // lblChartSettingsSave
            // 
            resources.ApplyResources(this.lblChartSettingsSave, "lblChartSettingsSave");
            this.tableLayoutPanel20.SetColumnSpan(this.lblChartSettingsSave, 2);
            this.lblChartSettingsSave.Name = "lblChartSettingsSave";
            this.lblChartSettingsSave.Tag = "6";
            this.toolTip1.SetToolTip(this.lblChartSettingsSave, resources.GetString("lblChartSettingsSave.ToolTip"));
            this.lblChartSettingsSave.Click += new System.EventHandler(this.lblChartSettingsSection_Click);
            // 
            // lbSavedReportsCharting
            // 
            this.tableLayoutPanel20.SetColumnSpan(this.lbSavedReportsCharting, 2);
            resources.ApplyResources(this.lbSavedReportsCharting, "lbSavedReportsCharting");
            this.lbSavedReportsCharting.FormattingEnabled = true;
            this.lbSavedReportsCharting.Name = "lbSavedReportsCharting";
            this.lbSavedReportsCharting.SelectedIndexChanged += new System.EventHandler(this.lbSavedReportsCharting_SelectedIndexChanged);
            // 
            // btnLoadChartSettings
            // 
            resources.ApplyResources(this.btnLoadChartSettings, "btnLoadChartSettings");
            this.btnLoadChartSettings.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnLoadChartSettings.Name = "btnLoadChartSettings";
            this.btnLoadChartSettings.Tag = "2";
            this.btnLoadChartSettings.UseVisualStyleBackColor = true;
            this.btnLoadChartSettings.Click += new System.EventHandler(this.btnLoadMapAndChartSettings_Click);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // btnDeleteChartSettings
            // 
            resources.ApplyResources(this.btnDeleteChartSettings, "btnDeleteChartSettings");
            this.btnDeleteChartSettings.Name = "btnDeleteChartSettings";
            this.btnDeleteChartSettings.Tag = "2";
            this.btnDeleteChartSettings.UseVisualStyleBackColor = true;
            this.btnDeleteChartSettings.Click += new System.EventHandler(this.btnDeleteMapAndChartSettings_Click);
            // 
            // panel2
            // 
            this.tableLayoutPanel20.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.tableLayoutPanel21);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // tableLayoutPanel21
            // 
            resources.ApplyResources(this.tableLayoutPanel21, "tableLayoutPanel21");
            this.tableLayoutPanel21.Controls.Add(this.btnSaveChartSettings, 2, 0);
            this.tableLayoutPanel21.Controls.Add(this.txtChartSettingsName, 1, 0);
            this.tableLayoutPanel21.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            // 
            // btnSaveChartSettings
            // 
            resources.ApplyResources(this.btnSaveChartSettings, "btnSaveChartSettings");
            this.btnSaveChartSettings.Name = "btnSaveChartSettings";
            this.btnSaveChartSettings.Tag = "2";
            this.btnSaveChartSettings.UseVisualStyleBackColor = true;
            this.btnSaveChartSettings.Click += new System.EventHandler(this.btnSaveMapAndChartSettings_Click);
            // 
            // txtChartSettingsName
            // 
            resources.ApplyResources(this.txtChartSettingsName, "txtChartSettingsName");
            this.txtChartSettingsName.Name = "txtChartSettingsName";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // tableLayoutPanel8
            // 
            resources.ApplyResources(this.tableLayoutPanel8, "tableLayoutPanel8");
            this.tableLayoutPanel8.Controls.Add(this.lblChartSettings, 0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            // 
            // lblChartSettings
            // 
            resources.ApplyResources(this.lblChartSettings, "lblChartSettings");
            this.lblChartSettings.Name = "lblChartSettings";
            // 
            // tpTableByRegion
            // 
            this.tpTableByRegion.Controls.Add(this.tableLayoutPanel9);
            resources.ApplyResources(this.tpTableByRegion, "tpTableByRegion");
            this.tpTableByRegion.Name = "tpTableByRegion";
            this.tpTableByRegion.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel9
            // 
            resources.ApplyResources(this.tableLayoutPanel9, "tableLayoutPanel9");
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel10, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.dgvTableByRegion, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel15, 1, 1);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            // 
            // tableLayoutPanel10
            // 
            resources.ApplyResources(this.tableLayoutPanel10, "tableLayoutPanel10");
            this.tableLayoutPanel10.Controls.Add(this.lblSelectARegion, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.tvSelectRegionTableByRegion, 0, 1);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel9.SetRowSpan(this.tableLayoutPanel10, 2);
            // 
            // lblSelectARegion
            // 
            resources.ApplyResources(this.lblSelectARegion, "lblSelectARegion");
            this.lblSelectARegion.Name = "lblSelectARegion";
            // 
            // tvSelectRegionTableByRegion
            // 
            resources.ApplyResources(this.tvSelectRegionTableByRegion, "tvSelectRegionTableByRegion");
            this.tvSelectRegionTableByRegion.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvSelectRegionTableByRegion.HideSelection = false;
            this.tvSelectRegionTableByRegion.Name = "tvSelectRegionTableByRegion";
            this.tvSelectRegionTableByRegion.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.Tree_DrawNode);
            this.tvSelectRegionTableByRegion.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSelectRegionTableByRegion_NodeMouseClick);
            this.tvSelectRegionTableByRegion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvSelectRegionTableByRegion_KeyUp);
            // 
            // dgvTableByRegion
            // 
            this.dgvTableByRegion.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvTableByRegion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableByRegion.ContextMenuStrip = this.cmsTableByRegionGrid;
            resources.ApplyResources(this.dgvTableByRegion, "dgvTableByRegion");
            this.dgvTableByRegion.Name = "dgvTableByRegion";
            this.dgvTableByRegion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTableByRegion.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvTableByRegion_CellBeginEdit);
            this.dgvTableByRegion.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTableByRegion_CellFormatting);
            this.dgvTableByRegion.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTableByRegion_CellMouseDown);
            this.dgvTableByRegion.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTableByRegion_CellValueChanged);
            this.dgvTableByRegion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvTableByRegion_KeyDown);
            this.dgvTableByRegion.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvTableByRegion_KeyUp);
            // 
            // cmsTableByRegionGrid
            // 
            this.cmsTableByRegionGrid.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.cmsTableByRegionGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTableByRegion_Add,
            this.tsmiTableByRegion_Delete,
            this.toolStripSeparator8,
            this.tsmiTableByRegion_Copy,
            this.tsmiTableByRegion_Paste});
            this.cmsTableByRegionGrid.Name = "contextMenuStrip3";
            resources.ApplyResources(this.cmsTableByRegionGrid, "cmsTableByRegionGrid");
            // 
            // tsmiTableByRegion_Add
            // 
            this.tsmiTableByRegion_Add.Name = "tsmiTableByRegion_Add";
            resources.ApplyResources(this.tsmiTableByRegion_Add, "tsmiTableByRegion_Add");
            this.tsmiTableByRegion_Add.Click += new System.EventHandler(this.tsmiTableByRegion_Add_Click);
            // 
            // tsmiTableByRegion_Delete
            // 
            this.tsmiTableByRegion_Delete.Name = "tsmiTableByRegion_Delete";
            resources.ApplyResources(this.tsmiTableByRegion_Delete, "tsmiTableByRegion_Delete");
            this.tsmiTableByRegion_Delete.Click += new System.EventHandler(this.tsmiTableByRegion_Delete_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // tsmiTableByRegion_Copy
            // 
            this.tsmiTableByRegion_Copy.Name = "tsmiTableByRegion_Copy";
            resources.ApplyResources(this.tsmiTableByRegion_Copy, "tsmiTableByRegion_Copy");
            this.tsmiTableByRegion_Copy.Click += new System.EventHandler(this.tsmiTableByRegion_Copy_Click);
            // 
            // tsmiTableByRegion_Paste
            // 
            this.tsmiTableByRegion_Paste.Name = "tsmiTableByRegion_Paste";
            resources.ApplyResources(this.tsmiTableByRegion_Paste, "tsmiTableByRegion_Paste");
            this.tsmiTableByRegion_Paste.Click += new System.EventHandler(this.tsmiTableByRegion_Paste_Click);
            // 
            // tableLayoutPanel15
            // 
            resources.ApplyResources(this.tableLayoutPanel15, "tableLayoutPanel15");
            this.tableLayoutPanel15.Controls.Add(this.btnExportToExcel1, 3, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnAddDateTableByRegion, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnExportToExcel3, 2, 0);
            this.tableLayoutPanel15.Controls.Add(this.btnRegionDeleteDate, 1, 0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            // 
            // btnExportToExcel1
            // 
            resources.ApplyResources(this.btnExportToExcel1, "btnExportToExcel1");
            this.btnExportToExcel1.Name = "btnExportToExcel1";
            this.btnExportToExcel1.UseVisualStyleBackColor = true;
            this.btnExportToExcel1.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnAddDateTableByRegion
            // 
            resources.ApplyResources(this.btnAddDateTableByRegion, "btnAddDateTableByRegion");
            this.btnAddDateTableByRegion.Name = "btnAddDateTableByRegion";
            this.btnAddDateTableByRegion.UseVisualStyleBackColor = true;
            this.btnAddDateTableByRegion.Click += new System.EventHandler(this.btnAddDateTableByRegion_Click);
            // 
            // btnExportToExcel3
            // 
            resources.ApplyResources(this.btnExportToExcel3, "btnExportToExcel3");
            this.btnExportToExcel3.Name = "btnExportToExcel3";
            this.btnExportToExcel3.UseVisualStyleBackColor = true;
            this.btnExportToExcel3.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnRegionDeleteDate
            // 
            resources.ApplyResources(this.btnRegionDeleteDate, "btnRegionDeleteDate");
            this.btnRegionDeleteDate.Name = "btnRegionDeleteDate";
            this.btnRegionDeleteDate.UseVisualStyleBackColor = true;
            this.btnRegionDeleteDate.Click += new System.EventHandler(this.btnRegionDeleteDate_Click);
            // 
            // tpTableByFactors
            // 
            this.tpTableByFactors.Controls.Add(this.tableLayoutPanel12);
            resources.ApplyResources(this.tpTableByFactors, "tpTableByFactors");
            this.tpTableByFactors.Name = "tpTableByFactors";
            this.tpTableByFactors.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel12
            // 
            resources.ApplyResources(this.tableLayoutPanel12, "tableLayoutPanel12");
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel14, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel13, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.dgvTableByFactor, 1, 0);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            // 
            // tableLayoutPanel14
            // 
            resources.ApplyResources(this.tableLayoutPanel14, "tableLayoutPanel14");
            this.tableLayoutPanel14.Controls.Add(this.btnFactorDeleteDate, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.btnExportToExcel2, 3, 0);
            this.tableLayoutPanel14.Controls.Add(this.btnAddDateTableByFactor, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.btnExportToExcel4, 2, 0);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            // 
            // btnFactorDeleteDate
            // 
            resources.ApplyResources(this.btnFactorDeleteDate, "btnFactorDeleteDate");
            this.btnFactorDeleteDate.Name = "btnFactorDeleteDate";
            this.btnFactorDeleteDate.UseVisualStyleBackColor = true;
            this.btnFactorDeleteDate.Click += new System.EventHandler(this.btnFactorDeleteDate_Click);
            // 
            // btnExportToExcel2
            // 
            resources.ApplyResources(this.btnExportToExcel2, "btnExportToExcel2");
            this.btnExportToExcel2.Name = "btnExportToExcel2";
            this.btnExportToExcel2.UseVisualStyleBackColor = true;
            this.btnExportToExcel2.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnAddDateTableByFactor
            // 
            resources.ApplyResources(this.btnAddDateTableByFactor, "btnAddDateTableByFactor");
            this.btnAddDateTableByFactor.Name = "btnAddDateTableByFactor";
            this.btnAddDateTableByFactor.UseVisualStyleBackColor = true;
            this.btnAddDateTableByFactor.Click += new System.EventHandler(this.btnAddDateTableByFactor_Click);
            // 
            // btnExportToExcel4
            // 
            resources.ApplyResources(this.btnExportToExcel4, "btnExportToExcel4");
            this.btnExportToExcel4.Name = "btnExportToExcel4";
            this.btnExportToExcel4.UseVisualStyleBackColor = true;
            this.btnExportToExcel4.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // tableLayoutPanel13
            // 
            resources.ApplyResources(this.tableLayoutPanel13, "tableLayoutPanel13");
            this.tableLayoutPanel13.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.tvSelectFactorTableByFactor, 0, 1);
            this.tableLayoutPanel13.Controls.Add(this.label16, 0, 2);
            this.tableLayoutPanel13.Controls.Add(this.tvSelectRegionsTableByFactor, 0, 3);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel12.SetRowSpan(this.tableLayoutPanel13, 2);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // tvSelectFactorTableByFactor
            // 
            resources.ApplyResources(this.tvSelectFactorTableByFactor, "tvSelectFactorTableByFactor");
            this.tvSelectFactorTableByFactor.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvSelectFactorTableByFactor.HideSelection = false;
            this.tvSelectFactorTableByFactor.Name = "tvSelectFactorTableByFactor";
            this.tvSelectFactorTableByFactor.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.Tree_DrawNode);
            this.tvSelectFactorTableByFactor.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSelectFactorTableByFactor_NodeMouseClick);
            this.tvSelectFactorTableByFactor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvSelectFactorTableByFactor_KeyUp);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // tvSelectRegionsTableByFactor
            // 
            this.tvSelectRegionsTableByFactor.AllowDrop = true;
            this.tvSelectRegionsTableByFactor.CheckBoxes = true;
            this.tvSelectRegionsTableByFactor.ContextMenuStrip = this.cmTableByFactorRegions;
            resources.ApplyResources(this.tvSelectRegionsTableByFactor, "tvSelectRegionsTableByFactor");
            this.tvSelectRegionsTableByFactor.Name = "tvSelectRegionsTableByFactor";
            this.tvSelectRegionsTableByFactor.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.Tree_DrawNode);
            this.tvSelectRegionsTableByFactor.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSelectRegionsTableByFactor_NodeMouseClick);
            // 
            // cmTableByFactorRegions
            // 
            this.cmTableByFactorRegions.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.cmTableByFactorRegions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelectAllChildRegions,
            this.tsmiSelectAllRegionsThisLevel,
            this.toolStripSeparator3,
            this.tsmiDeselectAllChildRegions,
            this.tsmiDeselectAllRegionsThisLevel});
            this.cmTableByFactorRegions.Name = "contextMenuStrip2";
            resources.ApplyResources(this.cmTableByFactorRegions, "cmTableByFactorRegions");
            this.cmTableByFactorRegions.Opening += new System.ComponentModel.CancelEventHandler(this.cmTableByFactorRegions_Opening);
            this.cmTableByFactorRegions.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmTableByFactorRegions_ItemClicked);
            // 
            // tsmiSelectAllChildRegions
            // 
            this.tsmiSelectAllChildRegions.Name = "tsmiSelectAllChildRegions";
            resources.ApplyResources(this.tsmiSelectAllChildRegions, "tsmiSelectAllChildRegions");
            // 
            // tsmiSelectAllRegionsThisLevel
            // 
            this.tsmiSelectAllRegionsThisLevel.Name = "tsmiSelectAllRegionsThisLevel";
            resources.ApplyResources(this.tsmiSelectAllRegionsThisLevel, "tsmiSelectAllRegionsThisLevel");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tsmiDeselectAllChildRegions
            // 
            this.tsmiDeselectAllChildRegions.Name = "tsmiDeselectAllChildRegions";
            resources.ApplyResources(this.tsmiDeselectAllChildRegions, "tsmiDeselectAllChildRegions");
            // 
            // tsmiDeselectAllRegionsThisLevel
            // 
            this.tsmiDeselectAllRegionsThisLevel.Name = "tsmiDeselectAllRegionsThisLevel";
            resources.ApplyResources(this.tsmiDeselectAllRegionsThisLevel, "tsmiDeselectAllRegionsThisLevel");
            // 
            // dgvTableByFactor
            // 
            this.dgvTableByFactor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableByFactor.ContextMenuStrip = this.cmsTableByFactorsGrid;
            resources.ApplyResources(this.dgvTableByFactor, "dgvTableByFactor");
            this.dgvTableByFactor.Name = "dgvTableByFactor";
            this.dgvTableByFactor.RowTemplate.Height = 24;
            this.dgvTableByFactor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTableByFactor.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvTableByFactor_CellBeginEdit);
            this.dgvTableByFactor.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTableByFactor_CellEndEdit);
            this.dgvTableByFactor.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTableByFactor_CellFormatting);
            this.dgvTableByFactor.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTableByFactor_CellLeave);
            this.dgvTableByFactor.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTableByFactor_CellMouseDown);
            this.dgvTableByFactor.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTableByFactor_CellValueChanged);
            this.dgvTableByFactor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvTableByFactor_KeyDown);
            this.dgvTableByFactor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvTableByFactor_KeyUp);
            // 
            // cmsTableByFactorsGrid
            // 
            this.cmsTableByFactorsGrid.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.cmsTableByFactorsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTableByFactor_Add,
            this.tsmiTableByFactor_Delete,
            this.toolStripSeparator7,
            this.tsmiTableByFactor_Copy,
            this.tsmiTableByFactor_Paste});
            this.cmsTableByFactorsGrid.Name = "contextMenuStrip3";
            resources.ApplyResources(this.cmsTableByFactorsGrid, "cmsTableByFactorsGrid");
            // 
            // tsmiTableByFactor_Add
            // 
            this.tsmiTableByFactor_Add.Name = "tsmiTableByFactor_Add";
            resources.ApplyResources(this.tsmiTableByFactor_Add, "tsmiTableByFactor_Add");
            this.tsmiTableByFactor_Add.Click += new System.EventHandler(this.tsmiTableByFactor_Add_Click);
            // 
            // tsmiTableByFactor_Delete
            // 
            this.tsmiTableByFactor_Delete.Name = "tsmiTableByFactor_Delete";
            resources.ApplyResources(this.tsmiTableByFactor_Delete, "tsmiTableByFactor_Delete");
            this.tsmiTableByFactor_Delete.Click += new System.EventHandler(this.tsmiTableByFactor_Delete_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // tsmiTableByFactor_Copy
            // 
            this.tsmiTableByFactor_Copy.Name = "tsmiTableByFactor_Copy";
            resources.ApplyResources(this.tsmiTableByFactor_Copy, "tsmiTableByFactor_Copy");
            this.tsmiTableByFactor_Copy.Click += new System.EventHandler(this.tsmiTableByFactor_Copy_Click);
            // 
            // tsmiTableByFactor_Paste
            // 
            this.tsmiTableByFactor_Paste.Name = "tsmiTableByFactor_Paste";
            resources.ApplyResources(this.tsmiTableByFactor_Paste, "tsmiTableByFactor_Paste");
            this.tsmiTableByFactor_Paste.Click += new System.EventHandler(this.tsmiTableByFactor_Paste_Click);
            // 
            // tpStaticMarkers
            // 
            this.tpStaticMarkers.Controls.Add(this.tableLayoutPanel18);
            resources.ApplyResources(this.tpStaticMarkers, "tpStaticMarkers");
            this.tpStaticMarkers.Name = "tpStaticMarkers";
            this.tpStaticMarkers.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel18
            // 
            resources.ApplyResources(this.tableLayoutPanel18, "tableLayoutPanel18");
            this.tableLayoutPanel18.Controls.Add(this.tableLayoutPanel19, 0, 1);
            this.tableLayoutPanel18.Controls.Add(this.dgvStaticMarkers, 0, 0);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            // 
            // tableLayoutPanel19
            // 
            resources.ApplyResources(this.tableLayoutPanel19, "tableLayoutPanel19");
            this.tableLayoutPanel19.Controls.Add(this.btnDeleteMarker, 1, 0);
            this.tableLayoutPanel19.Controls.Add(this.btnExportToExcelMarker, 2, 0);
            this.tableLayoutPanel19.Controls.Add(this.btnAddMarker, 0, 0);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            // 
            // btnDeleteMarker
            // 
            resources.ApplyResources(this.btnDeleteMarker, "btnDeleteMarker");
            this.btnDeleteMarker.Name = "btnDeleteMarker";
            this.btnDeleteMarker.UseVisualStyleBackColor = true;
            this.btnDeleteMarker.Click += new System.EventHandler(this.btnDeleteMarker_Click);
            // 
            // btnExportToExcelMarker
            // 
            resources.ApplyResources(this.btnExportToExcelMarker, "btnExportToExcelMarker");
            this.btnExportToExcelMarker.Name = "btnExportToExcelMarker";
            this.btnExportToExcelMarker.UseVisualStyleBackColor = true;
            this.btnExportToExcelMarker.Click += new System.EventHandler(this.btnExportToExcelMarker_Click);
            // 
            // btnAddMarker
            // 
            resources.ApplyResources(this.btnAddMarker, "btnAddMarker");
            this.btnAddMarker.Name = "btnAddMarker";
            this.btnAddMarker.UseVisualStyleBackColor = true;
            this.btnAddMarker.Click += new System.EventHandler(this.btnAddMarker_Click);
            // 
            // dgvStaticMarkers
            // 
            resources.ApplyResources(this.dgvStaticMarkers, "dgvStaticMarkers");
            this.dgvStaticMarkers.AllowUserToAddRows = false;
            this.dgvStaticMarkers.AllowUserToDeleteRows = false;
            this.dgvStaticMarkers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStaticMarkers.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvStaticMarkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStaticMarkers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateFrom,
            this.DateTo,
            this.MarkerType,
            this.Text,
            this.Description,
            this.Latitude,
            this.Longitude});
            this.dgvStaticMarkers.ContextMenuStrip = this.cmStaticMarkers;
            this.dgvStaticMarkers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvStaticMarkers.Name = "dgvStaticMarkers";
            this.dgvStaticMarkers.RowTemplate.Height = 24;
            this.dgvStaticMarkers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStaticMarkers.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvStaticMarkers_CellBeginEdit);
            this.dgvStaticMarkers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStaticMarkers_CellDoubleClick);
            this.dgvStaticMarkers.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvStaticMarkers_CellFormatting);
            this.dgvStaticMarkers.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvStaticMarkers_DataError);
            this.dgvStaticMarkers.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgvStaticMarkers_SortCompare);
            this.dgvStaticMarkers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvStaticMarkers_KeyUp);
            // 
            // DateFrom
            // 
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            dataGridViewCellStyle1.NullValue = null;
            this.DateFrom.DefaultCellStyle = dataGridViewCellStyle1;
            this.DateFrom.FillWeight = 39.77211F;
            resources.ApplyResources(this.DateFrom, "DateFrom");
            this.DateFrom.Name = "DateFrom";
            // 
            // DateTo
            // 
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.DateTo.DefaultCellStyle = dataGridViewCellStyle2;
            this.DateTo.FillWeight = 39.77211F;
            resources.ApplyResources(this.DateTo, "DateTo");
            this.DateTo.Name = "DateTo";
            // 
            // MarkerType
            // 
            this.MarkerType.FillWeight = 39.77211F;
            resources.ApplyResources(this.MarkerType, "MarkerType");
            this.MarkerType.Name = "MarkerType";
            // 
            // Text
            // 
            this.Text.FillWeight = 39.77211F;
            resources.ApplyResources(this.Text, "Text");
            this.Text.Name = "Text";
            // 
            // Description
            // 
            this.Description.FillWeight = 43.74932F;
            resources.ApplyResources(this.Description, "Description");
            this.Description.Name = "Description";
            // 
            // Latitude
            // 
            this.Latitude.FillWeight = 39.77211F;
            resources.ApplyResources(this.Latitude, "Latitude");
            this.Latitude.Name = "Latitude";
            // 
            // Longitude
            // 
            this.Longitude.FillWeight = 39.77211F;
            resources.ApplyResources(this.Longitude, "Longitude");
            this.Longitude.Name = "Longitude";
            // 
            // cmStaticMarkers
            // 
            this.cmStaticMarkers.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.cmStaticMarkers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStaticMarker_Add,
            this.tsmiStaticMarker_Delete,
            this.toolStripSeparator9,
            this.tsmiStaticMarker_Copy,
            this.tsmiStaticMarker_Paste});
            this.cmStaticMarkers.Name = "contextMenuStrip5";
            resources.ApplyResources(this.cmStaticMarkers, "cmStaticMarkers");
            // 
            // tsmiStaticMarker_Add
            // 
            this.tsmiStaticMarker_Add.Name = "tsmiStaticMarker_Add";
            resources.ApplyResources(this.tsmiStaticMarker_Add, "tsmiStaticMarker_Add");
            this.tsmiStaticMarker_Add.Click += new System.EventHandler(this.tsmiStaticMarker_Add_Click);
            // 
            // tsmiStaticMarker_Delete
            // 
            this.tsmiStaticMarker_Delete.Name = "tsmiStaticMarker_Delete";
            resources.ApplyResources(this.tsmiStaticMarker_Delete, "tsmiStaticMarker_Delete");
            this.tsmiStaticMarker_Delete.Click += new System.EventHandler(this.tsmiStaticMarker_Delete_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            // 
            // tsmiStaticMarker_Copy
            // 
            this.tsmiStaticMarker_Copy.Name = "tsmiStaticMarker_Copy";
            resources.ApplyResources(this.tsmiStaticMarker_Copy, "tsmiStaticMarker_Copy");
            this.tsmiStaticMarker_Copy.Click += new System.EventHandler(this.tsmiStaticMarker_Copy_Click);
            // 
            // tsmiStaticMarker_Paste
            // 
            this.tsmiStaticMarker_Paste.Name = "tsmiStaticMarker_Paste";
            resources.ApplyResources(this.tsmiStaticMarker_Paste, "tsmiStaticMarker_Paste");
            this.tsmiStaticMarker_Paste.Click += new System.EventHandler(this.tsmiStaticMarker_Paste_Click);
            // 
            // tpFactorsInModel
            // 
            resources.ApplyResources(this.tpFactorsInModel, "tpFactorsInModel");
            this.tpFactorsInModel.Name = "tpFactorsInModel";
            this.tpFactorsInModel.UseVisualStyleBackColor = true;
            // 
            // tpRiskAndAction
            // 
            resources.ApplyResources(this.tpRiskAndAction, "tpRiskAndAction");
            this.tpRiskAndAction.Name = "tpRiskAndAction";
            this.tpRiskAndAction.UseVisualStyleBackColor = true;
            // 
            // cmMap
            // 
            resources.ApplyResources(this.cmMap, "cmMap");
            this.cmMap.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.cmMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMapAddMarker,
            this.tsmiMapEditMarker,
            this.tsmiMapDeleteMarker,
            this.tsmiMapMarkerTitles,
            this.tsmiMapMarkerLegend,
            this.toolStripSeparator1,
            this.tsmiMapRegionName,
            this.tsmiMapDisplayDataValue,
            this.tsmiMapFactorLegend,
            this.tsmiMapCumulativeFactorLegend,
            this.tsmiMapCumulativeFactorSize,
            this.toolStripSeparator5,
            this.tsmiMapSaveAsImage,
            this.tsmiMapSaveAsHighResolutionImage,
            this.tsmiSaveAsKML,
            this.toolStripSeparator2,
            this.tsmiMapShowGrid,
            this.tsmiMapShowZoom});
            this.cmMap.Name = "contextMenuStrip1";
            this.cmMap.ShowItemToolTips = false;
            this.cmMap.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.cmMap_Closed);
            // 
            // tsmiMapAddMarker
            // 
            this.tsmiMapAddMarker.Name = "tsmiMapAddMarker";
            resources.ApplyResources(this.tsmiMapAddMarker, "tsmiMapAddMarker");
            this.tsmiMapAddMarker.Click += new System.EventHandler(this.tsmiMapAddMarker_Click);
            // 
            // tsmiMapEditMarker
            // 
            resources.ApplyResources(this.tsmiMapEditMarker, "tsmiMapEditMarker");
            this.tsmiMapEditMarker.Name = "tsmiMapEditMarker";
            this.tsmiMapEditMarker.Click += new System.EventHandler(this.tsmiMapEditMarker_Click);
            // 
            // tsmiMapDeleteMarker
            // 
            this.tsmiMapDeleteMarker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.tsmiMapDeleteMarker, "tsmiMapDeleteMarker");
            this.tsmiMapDeleteMarker.Name = "tsmiMapDeleteMarker";
            this.tsmiMapDeleteMarker.Click += new System.EventHandler(this.tsmiMapDeleteMarker_Click);
            // 
            // tsmiMapMarkerTitles
            // 
            this.tsmiMapMarkerTitles.Name = "tsmiMapMarkerTitles";
            resources.ApplyResources(this.tsmiMapMarkerTitles, "tsmiMapMarkerTitles");
            this.tsmiMapMarkerTitles.Click += new System.EventHandler(this.tsmiMapMarkerTitles_Click);
            // 
            // tsmiMapMarkerLegend
            // 
            resources.ApplyResources(this.tsmiMapMarkerLegend, "tsmiMapMarkerLegend");
            this.tsmiMapMarkerLegend.Name = "tsmiMapMarkerLegend";
            this.tsmiMapMarkerLegend.Click += new System.EventHandler(this.tsmiMapMarkerLegend_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsmiMapRegionName
            // 
            this.tsmiMapRegionName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiMapRegionName.Name = "tsmiMapRegionName";
            resources.ApplyResources(this.tsmiMapRegionName, "tsmiMapRegionName");
            this.tsmiMapRegionName.Click += new System.EventHandler(this.tsmiMapRegionName_Click);
            // 
            // tsmiMapDisplayDataValue
            // 
            this.tsmiMapDisplayDataValue.Name = "tsmiMapDisplayDataValue";
            resources.ApplyResources(this.tsmiMapDisplayDataValue, "tsmiMapDisplayDataValue");
            this.tsmiMapDisplayDataValue.Click += new System.EventHandler(this.tsmiMapDisplayDataValue_Click);
            // 
            // tsmiMapFactorLegend
            // 
            resources.ApplyResources(this.tsmiMapFactorLegend, "tsmiMapFactorLegend");
            this.tsmiMapFactorLegend.Name = "tsmiMapFactorLegend";
            this.tsmiMapFactorLegend.Click += new System.EventHandler(this.tsmiMapFactorLegend_Click);
            // 
            // tsmiMapCumulativeFactorLegend
            // 
            resources.ApplyResources(this.tsmiMapCumulativeFactorLegend, "tsmiMapCumulativeFactorLegend");
            this.tsmiMapCumulativeFactorLegend.Name = "tsmiMapCumulativeFactorLegend";
            this.tsmiMapCumulativeFactorLegend.Click += new System.EventHandler(this.tsmiMapCumulativeFactorLegend_Click);
            // 
            // tsmiMapCumulativeFactorSize
            // 
            this.tsmiMapCumulativeFactorSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMapCumulativeFactorSize_Small,
            this.tsmiMapCumulativeFactorSize_Medium,
            this.tsmiMapCumulativeFactorSize_Large,
            this.tsmiMapCumulativeFactorSize_ExtraLarge});
            this.tsmiMapCumulativeFactorSize.Name = "tsmiMapCumulativeFactorSize";
            resources.ApplyResources(this.tsmiMapCumulativeFactorSize, "tsmiMapCumulativeFactorSize");
            // 
            // tsmiMapCumulativeFactorSize_Small
            // 
            this.tsmiMapCumulativeFactorSize_Small.Name = "tsmiMapCumulativeFactorSize_Small";
            resources.ApplyResources(this.tsmiMapCumulativeFactorSize_Small, "tsmiMapCumulativeFactorSize_Small");
            this.tsmiMapCumulativeFactorSize_Small.Click += new System.EventHandler(this.tsmiMapCumulativeFactorSize_Click);
            // 
            // tsmiMapCumulativeFactorSize_Medium
            // 
            this.tsmiMapCumulativeFactorSize_Medium.Name = "tsmiMapCumulativeFactorSize_Medium";
            resources.ApplyResources(this.tsmiMapCumulativeFactorSize_Medium, "tsmiMapCumulativeFactorSize_Medium");
            this.tsmiMapCumulativeFactorSize_Medium.Click += new System.EventHandler(this.tsmiMapCumulativeFactorSize_Click);
            // 
            // tsmiMapCumulativeFactorSize_Large
            // 
            this.tsmiMapCumulativeFactorSize_Large.Name = "tsmiMapCumulativeFactorSize_Large";
            resources.ApplyResources(this.tsmiMapCumulativeFactorSize_Large, "tsmiMapCumulativeFactorSize_Large");
            this.tsmiMapCumulativeFactorSize_Large.Click += new System.EventHandler(this.tsmiMapCumulativeFactorSize_Click);
            // 
            // tsmiMapCumulativeFactorSize_ExtraLarge
            // 
            this.tsmiMapCumulativeFactorSize_ExtraLarge.Name = "tsmiMapCumulativeFactorSize_ExtraLarge";
            resources.ApplyResources(this.tsmiMapCumulativeFactorSize_ExtraLarge, "tsmiMapCumulativeFactorSize_ExtraLarge");
            this.tsmiMapCumulativeFactorSize_ExtraLarge.Click += new System.EventHandler(this.tsmiMapCumulativeFactorSize_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // tsmiMapSaveAsImage
            // 
            this.tsmiMapSaveAsImage.Name = "tsmiMapSaveAsImage";
            resources.ApplyResources(this.tsmiMapSaveAsImage, "tsmiMapSaveAsImage");
            this.tsmiMapSaveAsImage.Click += new System.EventHandler(this.tsmiMapSaveAsImage_Click);
            // 
            // tsmiMapSaveAsHighResolutionImage
            // 
            this.tsmiMapSaveAsHighResolutionImage.Name = "tsmiMapSaveAsHighResolutionImage";
            resources.ApplyResources(this.tsmiMapSaveAsHighResolutionImage, "tsmiMapSaveAsHighResolutionImage");
            this.tsmiMapSaveAsHighResolutionImage.Click += new System.EventHandler(this.tsmiMapSaveAsHighResolutionImage_Click);
            // 
            // tsmiSaveAsKML
            // 
            this.tsmiSaveAsKML.Name = "tsmiSaveAsKML";
            resources.ApplyResources(this.tsmiSaveAsKML, "tsmiSaveAsKML");
            this.tsmiSaveAsKML.Click += new System.EventHandler(this.tsmiSaveAsKML_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsmiMapShowGrid
            // 
            this.tsmiMapShowGrid.Name = "tsmiMapShowGrid";
            resources.ApplyResources(this.tsmiMapShowGrid, "tsmiMapShowGrid");
            this.tsmiMapShowGrid.Click += new System.EventHandler(this.tsmiMapShowGrid_Click);
            // 
            // tsmiMapShowZoom
            // 
            this.tsmiMapShowZoom.Name = "tsmiMapShowZoom";
            resources.ApplyResources(this.tsmiMapShowZoom, "tsmiMapShowZoom");
            this.tsmiMapShowZoom.Click += new System.EventHandler(this.tsmiMapShowZoom_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            resources.ApplyResources(this.toolStripMenuItem7, "toolStripMenuItem7");
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            resources.ApplyResources(this.toolStripMenuItem8, "toolStripMenuItem8");
            // 
            // cdSelectMapColor
            // 
            this.cdSelectMapColor.AnyColor = true;
            this.cdSelectMapColor.Color = System.Drawing.Color.White;
            this.cdSelectMapColor.SolidColorOnly = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 300;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            // 
            // RiskMapping
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcRiskMapping);
            this.Name = "RiskMapping";
            this.tcRiskMapping.ResumeLayout(false);
            this.tpMap.ResumeLayout(false);
            this.tlpMap.ResumeLayout(false);
            this.tlpMap.PerformLayout();
            this.tlpScroll.ResumeLayout(false);
            this.tlpScroll.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlpMapSettings.ResumeLayout(false);
            this.tlpMapSettings.PerformLayout();
            this.tlpColors.ResumeLayout(false);
            this.pnlColorScheme.ResumeLayout(false);
            this.tlpRegions.ResumeLayout(false);
            this.tlpRegions.PerformLayout();
            this.cmRegions.ResumeLayout(false);
            this.tlpMapDates.ResumeLayout(false);
            this.tlpMapDates.PerformLayout();
            this.tlpMapDatesRange.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tableLayoutPanel17.ResumeLayout(false);
            this.tableLayoutPanel17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMarkerSize)).EndInit();
            this.tlpMapFactors.ResumeLayout(false);
            this.tlpMapFactors.PerformLayout();
            this.tsMapOptions.ResumeLayout(false);
            this.tsMapOptions.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tpCharting.ResumeLayout(false);
            this.tlpChartMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.cmChart.ResumeLayout(false);
            this.tlpChartScroll.ResumeLayout(false);
            this.tlpChartScroll.PerformLayout();
            this.tlpChartDates.ResumeLayout(false);
            this.tlpChartDates.PerformLayout();
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel22.ResumeLayout(false);
            this.tableLayoutPanel22.PerformLayout();
            this.tlpChartType.ResumeLayout(false);
            this.tlpChartType.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.flpFactorCombination.ResumeLayout(false);
            this.flpFactorCombination.PerformLayout();
            this.tableLayoutPanel20.ResumeLayout(false);
            this.tableLayoutPanel20.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel21.ResumeLayout(false);
            this.tableLayoutPanel21.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tpTableByRegion.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableByRegion)).EndInit();
            this.cmsTableByRegionGrid.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tpTableByFactors.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.cmTableByFactorRegions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableByFactor)).EndInit();
            this.cmsTableByFactorsGrid.ResumeLayout(false);
            this.tpStaticMarkers.ResumeLayout(false);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStaticMarkers)).EndInit();
            this.cmStaticMarkers.ResumeLayout(false);
            this.cmMap.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.TabControl tcRiskMapping;
        private System.Windows.Forms.TabPage tpMap;
        private System.Windows.Forms.TabPage tpCharting;
        private System.Windows.Forms.TableLayoutPanel tlpMap;
        private System.Windows.Forms.TableLayoutPanel tlpScroll;
        private System.Windows.Forms.TableLayoutPanel tlpMapSettings;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TableLayoutPanel tlpColors;
        private System.Windows.Forms.Panel pnlSelectedColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlColorScheme;
        private System.Windows.Forms.Button btnPickColorCustom;
        private System.Windows.Forms.TableLayoutPanel tlpRegions;
        private System.Windows.Forms.Label lblMapSettingsRegions;
        private System.Windows.Forms.TreeView tvMapRegions;
        private System.Windows.Forms.TableLayoutPanel tlpMapDates;
        private System.Windows.Forms.DateTimePicker dtpMapDateFrom;
        private System.Windows.Forms.DateTimePicker dtpMapDateTo;
        private System.Windows.Forms.Button btnMapAllAvailableDates;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblMapSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Label lblMapSettingsMarkerTypes;
        private System.Windows.Forms.FlowLayoutPanel flpMarkerType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
        private System.Windows.Forms.TrackBar tbMarkerSize;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ToolStrip tsMapOptions;
        private System.Windows.Forms.ToolStripButton tsbWorld;
        private System.Windows.Forms.ToolStripButton tsbContinent;
        private System.Windows.Forms.ToolStripButton tsbCountry;
        private System.Windows.Forms.ToolStripButton tsbProvinces;
        private System.Windows.Forms.ToolStripButton tsb1stAdmin;
        private System.Windows.Forms.ToolStripButton tsb2ndAdmin;
        private System.Windows.Forms.ToolStripButton tsb3rdAdmin;
        private System.Windows.Forms.ToolStripButton tsb4thAdmin;
        private System.Windows.Forms.ToolStripButton tsbPaths;
        private System.Windows.Forms.ToolStripButton tsbPOI;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.TabPage tpTableByRegion;
        private System.Windows.Forms.TabPage tpTableByFactors;
        private System.Windows.Forms.TabPage tpStaticMarkers;
        private System.Windows.Forms.TabPage tpRiskAndAction;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label lblSelectARegion;
        private System.Windows.Forms.TreeView tvSelectRegionTableByRegion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.Button btnExportToExcel1;
        private System.Windows.Forms.Button btnAddDateTableByRegion;
        private System.Windows.Forms.Button btnExportToExcel3;
        private System.Windows.Forms.Button btnRegionDeleteDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.Button btnExportToExcel4;
        private System.Windows.Forms.Button btnFactorDeleteDate;
        private System.Windows.Forms.Button btnExportToExcel2;
        private System.Windows.Forms.Button btnAddDateTableByFactor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TreeView tvSelectFactorTableByFactor;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TreeView tvSelectRegionsTableByFactor;
        private DataGridViewExtended dgvTableByFactor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel19;
        private System.Windows.Forms.Button btnExportToExcelMarker;
        private System.Windows.Forms.Button btnAddMarker;
        private DataGridViewExtended dgvStaticMarkers;
        private System.Windows.Forms.ColorDialog cdSelectMapColor;
        private System.Windows.Forms.TabPage tpFactorsInModel;
        private System.Windows.Forms.TableLayoutPanel tlpChartMain;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TableLayoutPanel tlpChartScroll;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TreeView tvChartRegions;
        private System.Windows.Forms.Label lblSelectRegionsToBeDraw;
        private System.Windows.Forms.TableLayoutPanel tlpChartType;
        private System.Windows.Forms.Button btnColumn;
        private System.Windows.Forms.Button btnFastLine;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnSpline;
        private System.Windows.Forms.Button btnSplineArea;
        private System.Windows.Forms.ContextMenuStrip cmChart;
        private System.Windows.Forms.ToolStripMenuItem tsmiChartShowGrid;
        private System.Windows.Forms.ContextMenuStrip cmMap;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapAddMarker;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapEditMarker;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapMarkerTitles;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapDisplayDataValue;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapMarkerLegend;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapDeleteMarker;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapRegionName;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapFactorLegend;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapCumulativeFactorLegend;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapShowGrid;
        private System.Windows.Forms.ContextMenuStrip cmsTableByFactorsGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiTableByFactor_Delete;
        private System.Windows.Forms.ToolStripMenuItem tsmiTableByFactor_Add;
        private System.Windows.Forms.ToolStripMenuItem tsmiTableByFactor_Copy;
        private System.Windows.Forms.ToolStripMenuItem tsmiTableByFactor_Paste;
        private System.Windows.Forms.ContextMenuStrip cmsTableByRegionGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiTableByRegion_Add;
        private System.Windows.Forms.ToolStripMenuItem tsmiTableByRegion_Copy;
        private System.Windows.Forms.ToolStripMenuItem tsmiTableByRegion_Paste;
        private System.Windows.Forms.ToolStripMenuItem tsmiTableByRegion_Delete;
        private System.Windows.Forms.ContextMenuStrip cmStaticMarkers;
        private System.Windows.Forms.ToolStripMenuItem tsmiStaticMarker_Delete;
        private System.Windows.Forms.ToolStripMenuItem tsmiStaticMarker_Copy;
        private System.Windows.Forms.ToolStripMenuItem tsmiStaticMarker_Paste;
        private System.Windows.Forms.ContextMenuStrip cmRegions;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectAllChildRegions;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeselectAllChildRegions;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectAllRegionsThisLevel;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeselectAllRegionsThisLevel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripButton tsbMarkers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnTest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TableLayoutPanel tlpMapDatesRange;
        private System.Windows.Forms.Label lblMapSettingsDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox lbSavedReports;
        private System.Windows.Forms.Button btnLoadMapSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteMapSettings;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMapName;
        private System.Windows.Forms.Button btnSaveMapSettings;
        private System.Windows.Forms.TextBox txtMapSettingsName;
        private System.Windows.Forms.TableLayoutPanel tlpMapFactors;
        private System.Windows.Forms.FlowLayoutPanel flpMapFactors;
        private System.Windows.Forms.Label lblMapSettingsFactors;
        private System.Windows.Forms.Label lblMapSettingsSaveSettings;
        private System.Windows.Forms.Label lblMapSettingsColorScheme;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cbMapColorScheme;
        private System.Windows.Forms.ContextMenuStrip cmTableByFactorRegions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem tsmiTBFSelectAllChildRegions;
        private System.Windows.Forms.ToolStripMenuItem tsmiTBFSelectAllRegionsThisLevel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiTBFDeselectAllChildRegions;
        private System.Windows.Forms.ToolStripMenuItem tsmiTBFDeselectAllRegionsThisLevel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapSaveAsImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapSaveAsHighResolutionImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveAsKML;
        private System.Windows.Forms.ToolStripMenuItem tsmiChartSaveAsImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.TableLayoutPanel tlpChartDates;
        private System.Windows.Forms.Button btnChartAllAvailableDates;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpChartDateFrom;
        private System.Windows.Forms.DateTimePicker dtpChartDateTo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flpChartFactors;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flpFactorCombination;
        private System.Windows.Forms.RadioButton rbEachFactor;
        private System.Windows.Forms.RadioButton rbRVofRegions;
        private System.Windows.Forms.RadioButton rbRVofFactors;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripMenuItem tsmiChartShowLegends;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel20;
        private System.Windows.Forms.Label lblChartSettingsSave;
        private System.Windows.Forms.ListBox lbSavedReportsCharting;
        private System.Windows.Forms.Button btnLoadChartSettings;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnDeleteChartSettings;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel21;
        private System.Windows.Forms.Button btnSaveChartSettings;
        private System.Windows.Forms.TextBox txtChartSettingsName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label lblChartSettings;
        private DataGridViewExtended dgvTableByRegion;
        private System.Windows.Forms.Panel panel3;
        private ThinkGeo.MapSuite.DesktopEdition.WinformsMap winformsMap1;
        private MapZoom mapZoom1;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapShowZoom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel22;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapCumulativeFactorSize;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapCumulativeFactorSize_Small;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapCumulativeFactorSize_Medium;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapCumulativeFactorSize_Large;
        private System.Windows.Forms.ToolStripMenuItem tsmiMapCumulativeFactorSize_ExtraLarge;
        private System.Windows.Forms.Button btnDeleteMarker;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateTo;
        private System.Windows.Forms.DataGridViewComboBoxColumn MarkerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Text;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitude;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem tsmiStaticMarker_Add;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    }
}
