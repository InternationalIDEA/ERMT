using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Idea.Entities;
using Idea.Facade;
using Idea.Utils;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using Marker = Idea.Entities.Marker;
using MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle;
using Region = Idea.Entities.Region;
using ThinkGeoMarker = ThinkGeo.MapSuite.DesktopEdition.Marker;
using System.Drawing.Printing;
using System.Globalization;

namespace Idea.ERMT.UserControls
{
    public partial class RiskMapping : ERMTUserControl
    {
        //base Layer Overlay for the map control
        private LayerOverlay _layerOverlay;

        //overlay for the markers
        private SimpleMarkerOverlay _markersOverlay;

        //overlay for the cumulative factors
        private SimpleMarkerOverlay _cumulativeFactorsOverlay;

        //overlay for the path and POI
        private LayerOverlay _pathAndPOILayerOverlay;

        //list of the current model shapefiles.
        private List<ShapeFileFeatureLayer> _modelLayers;

        //list of the current model regions.
        private List<Region> _modelRegions;

        //list of the current model related regions.
        private List<Region> _modelRelatedRegions;

        //list of the current ModelFactos
        private List<ModelFactor> _modelFactors;

        //list of the current non-cumulative factors
        private List<ModelFactor> _modelRegularFactors = new List<ModelFactor>();

        //list of the current non-cumulative factors
        private List<ModelFactor> _modelFactorsCumulative = new List<ModelFactor>();

        //the model's colors
        private ClassBreakStyle _modelClassBreakStyle = new ClassBreakStyle();

        List<ModelFactorData> _modelData = new List<ModelFactorData>();
        List<ModelFactorData> _modelDataCumulative = new List<ModelFactorData>();

        private int _modelMinScale;
        private int _modelMaxScale;

        private List<MarkerType> _modelMarkerTypes;

        private List<ThinkGeoMarker> _modelThinkGeoMarkers;

        private List<ThinkGeoMarker> _modelThinkGeoCumulativeMarkers = new List<ThinkGeoMarker>();

        private bool _reloadModelData = false;

        private bool _setInitialExtent = true;

        private readonly Style _regionNameStyle = MapHelper.GetRegionNameStyle();
        private readonly Layer _gridLayer = MapHelper.GetGraticuleAdornmentLayer();

        private List<Dictionary<int, decimal>> _calculatedData;

        private List<Factor> _selectedFactors = new List<Factor>();

        //used as a flag to avoid running the same method while it's working.
        private bool _methodRunning;

        private bool _leave;
        private bool _valueChanged;
        private bool _riskMappingControlFirstCharge = true;
        private bool _loadLastSettings = true;

        private SeriesChartType _lastChartType = SeriesChartType.FastLine;
        private TreeNode _lastTreeNode;


        private readonly ChartLegend _chartLegend = new ChartLegend { Visible = false };
        private MapLegend _scaleFactorsLegend;
        private MapLegend _cumulativeFactorsLegend;
        private MapLegend _markersLegend;

        public RiskMapping()
        {
            //disabling this illegalcrossthreadcall is ONLY for the cumulative markers that are created as markers
            //somehow it seems that if the region has more than 1 cumulative marker, ThinkGeo blows up.
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            EventManager.OnModelChanged += OnModelChanged;
            EventManager.OnModelUpdated += OnModelChanged;
            this.SizeChanged += RiskMapping_SizeChanged;

            LoadTypeChart();

            OnModelChanged(new object(), new EventArgs());
            mapZoom1.OnZoomIn += mapZoom1_OnZoomIn;
            mapZoom1.OnZoomOut += mapZoom1_OnZoomOut;
            winformsMap1.ExtentOverlay.MapMouseWheel += ExtentOverlay_MapMouseWheel;
#if DEBUG
            btnTest.Visible = true;
#endif
        }

        void RiskMapping_SizeChanged(object sender, EventArgs e)
        {
            if (_setInitialExtent)
            {
                SetCurrentExtent();
                RefreshMap();
                _setInitialExtent = false;
            }
        }

        #region Map control
        /// <summary>
        /// Occurs when the current model is changed, whatever the source of the change might be.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnModelChanged(object sender, EventArgs e)
        {
            if (ERMTSession.Instance.CurrentModel != null && ERMTSession.Instance.CurrentModel.IDModel != 0)
            {
                LoadingForm.ShowLoading();
                Enabled = true;
                //To avoid throwing check changed events, set flag to true
                _riskMappingControlFirstCharge = true;

                ClearControlsAndData();

                Model model = ERMTSession.Instance.CurrentModel;

                LoadMinAndMaxDates();

                LoadFactors(model);

                LoadShapeFiles(model);

                LoadRegions(model);

                ShowTitle();

                LoadRegionsAndFactorsToTable(model);

                LoadMarkerTypes();

                LoadChartingFactors(model, tvMapRegions.Nodes[0]);

                LoadReports(model);

                if (_loadLastSettings)
                {
                    if (ERMTSession.Instance.CurrentReport != null)
                    {
                        LoadLastSettings();
                    }
                    _loadLastSettings = false;
                }
                //else
                //{
                //    cbMapColorScheme.SelectedIndex = 1;
                //}

                CacheModelData();

                CalculateFactorData();

                SetColorScheme();

                ConfigureShapesVisibility();

                tvSelectRegionsTableByFactor_NodeMouseClick(null, null);

                AddMapOverlays();

                SetRegionNameAndDataVisibility();

                SetMapGridVisibility();

                LoadMarkers();

                AddMarkersToMarkersOverlay();

                GetCumulativeFactors();

                SetMarkersVisibility();

                if (tcRiskMapping.SelectedIndex != 0)
                {
                    //the map is NOT the current tab.
                    tcRiskMapping_SelectedIndexChanged(tcRiskMapping, new EventArgs());
                }

                if (!_setInitialExtent)
                {
                    //means that the user changed the model. We do need to set the extent.
                    SetCurrentExtent();
                }

                UpdateScaleFactorLegend();

                UpdateCumulativeFactorsLegend();

                UpdateMarkersLegend();

                RefreshMap();

                _riskMappingControlFirstCharge = false;
                LoadingForm.Fadeout();
            }
            else
            {
                Enabled = false;
            }

        }


        private void ClearControlsAndData()
        {
            _modelData = new List<ModelFactorData>();
            _modelDataCumulative = new List<ModelFactorData>();
            _calculatedData = new List<Dictionary<int, decimal>>();
            _selectedFactors = new List<Factor>();

            SetMarkersVisibility(false, true);
            Application.DoEvents();
            _modelThinkGeoMarkers = new List<ThinkGeoMarker>();
            SetCumulativeMarkersVisibility(false);
            Application.DoEvents();
            _modelThinkGeoCumulativeMarkers = new List<ThinkGeoMarker>();

            if (_markersOverlay != null)
            {
                _markersOverlay.IsVisible = false;
            }

            Application.DoEvents();

            if (_scaleFactorsLegend != null)
            {
                _scaleFactorsLegend.Clear();
            }

            if (_cumulativeFactorsLegend != null)
            {
                _cumulativeFactorsLegend.Clear();
            }

            if (_markersLegend != null)
            {
                _markersLegend.Clear();
            }


            flpMapFactors.Controls.Clear();
            flpChartFactors.Controls.Clear();
            flpMarkerType.Controls.Clear();
            tvSelectFactorTableByFactor.Nodes.Clear();
            tvSelectRegionsTableByFactor.Nodes.Clear();

            //reset layer buttons
            foreach (ToolStripButton ts in (tsMapOptions.Items))
            {
                ts.Checked = false;
            }
        }

        private void SetCumulativeMarkersVisibility(Boolean visible)
        {
            //JPF: this commented code was an attempt to improve this method, but later I found there was no need. I'm keeping it here just in case...
            //if (_modelThinkGeoCumulativeMarkers != null)
            //{
            //    if (_cumulativeFactorsOverlay == null)
            //    {
            //        return;
            //    }

            //    foreach (var item in _cumulativeFactorsOverlay.Markers)
            //    {
            //        item.Visible = visible;
            //        if (fullClean)
            //        {
            //            winformsMap1.Controls.Remove(item);
            //            item.Dispose();
            //        }
            //    }
            //}
            foreach (ThinkGeoMarker modelThinkGeoCumulativeMarker in _modelThinkGeoCumulativeMarkers)
            {
                modelThinkGeoCumulativeMarker.Visible = visible;
            }
        }

        private void tsbRegionLevel_Click(object sender, EventArgs e)
        {
            ToolStripButton tsb = (ToolStripButton)sender;
            SetLayerVisibility(tsb);
            RefreshMap();
        }

        private void tsbPathAndPOI_Click(object sender, EventArgs e)
        {
            if (_riskMappingControlFirstCharge)
            {
                return;
            }

            _pathAndPOILayerOverlay.Layers.Clear();

            if (!tsbPaths.Checked && !tsbPOI.Checked)
            {
                //nothing to do, none is checked.
                _pathAndPOILayerOverlay.IsVisible = false;
            }

            foreach (Region modelRegion in _modelRegions.OrderBy(r => r.RegionLevel).Where(r => !String.IsNullOrEmpty(r.PathFileName) || !String.IsNullOrEmpty(r.POIFileName)))
            {
                if (!String.IsNullOrEmpty(modelRegion.PathFileName) && tsbPaths.Checked)
                {
                    _pathAndPOILayerOverlay.Layers.Add(MapHelper.GetRegionPathLayer(modelRegion));
                }
                if (!String.IsNullOrEmpty(modelRegion.POIFileName) && tsbPOI.Checked)
                {
                    _pathAndPOILayerOverlay.Layers.Add(MapHelper.GetRegionPOILayer(modelRegion));
                }
            }
            _pathAndPOILayerOverlay.IsVisible = true;

            RefreshMap();
        }

        void ExtentOverlay_MapMouseWheel(object sender, MapMouseWheelInteractiveOverlayEventArgs e)
        {
            var scaledExtent = e.InteractionArguments.MouseWheelDelta > 100 ?
                ExtentHelper.ZoomIn(winformsMap1.CurrentExtent, 1) :
                ExtentHelper.ZoomOut(winformsMap1.CurrentExtent, 1);
            e.InteractionArguments.CurrentExtent = scaledExtent;
        }

        private void CacheRegionsRelatedToCurrentModel()
        {
            Model currentModel = ERMTSession.Instance.CurrentModel;
            Region currentModelRegion = RegionHelper.Get(currentModel.IDRegion);
            int aux = currentModelRegion.RegionLevel;
        }

        /// <summary>
        /// Adds the map's overlays
        /// </summary>
        private void AddMapOverlays()
        {
            winformsMap1.Overlays.Clear();
            _layerOverlay = new LayerOverlay { Name = "main", IsBase = true };
            _markersOverlay = MapHelper.GetMarkersOverlay(winformsMap1);
            _pathAndPOILayerOverlay = new LayerOverlay { Name = "pathandpoi" };
            _cumulativeFactorsOverlay = MapHelper.GetCumulativeFactorsOverlay(winformsMap1);

            foreach (ShapeFileFeatureLayer shapeFileFeatureLayer in _modelLayers)
            {
                _layerOverlay.Layers.Add(shapeFileFeatureLayer);
            }

            winformsMap1.Overlays.Add(_layerOverlay);
            winformsMap1.Overlays.Add(_markersOverlay);
            winformsMap1.Overlays.Add(_pathAndPOILayerOverlay);

            //this is to avoid the bug that made markers dissappear after showing the context menu without selecting an option.
            winformsMap1.ExtentOverlay = new ExtentInteractiveOverlayForContext();
        }

        private void tsmiMapShowZoom_Click(object sender, EventArgs e)
        {
            tsmiMapShowZoom.Checked = !tsmiMapShowZoom.Checked;
            mapZoom1.Left = winformsMap1.Left + winformsMap1.Width - mapZoom1.Width - 30;
            mapZoom1.Visible = tsmiMapShowZoom.Checked;
        }

        void mapZoom1_OnZoomOut(object sender, EventArgs e)
        {
            winformsMap1.ZoomOut(5);
            winformsMap1.Refresh();
        }

        void mapZoom1_OnZoomIn(object sender, EventArgs e)
        {
            winformsMap1.ZoomIn(5);
            winformsMap1.Refresh();
        }

        private void tvMapRegions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ((TreeView)sender).SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                _lastTreeNode = e.Node;
            }
        }

        private void SetRegionVisibility(Region region, bool visible)
        {
            ShapeFileFeatureLayer shapeFileFeatureLayer = MapHelper.GetRegionFeatureLayer(region, _modelLayers);

            if (shapeFileFeatureLayer == null)
            {
                return;
            }

            string featureID = (region.ShapeFileIndex != null ? (region.ShapeFileIndex + 1).ToString() : "1");
            if (featureID != "-1")
            {
                List<string> featureIDs = new List<string>();
                if (featureID == "1" && region.ShapeFileIndex == null)
                {
                    //means that the index is actually null. So, we need to get a list of featureIDs
                    shapeFileFeatureLayer.FeatureSource.Open();
                    shapeFileFeatureLayer.FeatureIdsToExclude.Clear();
                    featureIDs.AddRange(shapeFileFeatureLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns).Select(f => f.Id));
                    shapeFileFeatureLayer.FeatureSource.Close();
                }

                if (visible)
                {
                    if (featureIDs.Count > 0)
                    {
                        shapeFileFeatureLayer.FeatureIdsToExclude.Clear();
                    }
                    else
                    {
                        while (shapeFileFeatureLayer.FeatureIdsToExclude.Contains(featureID))
                        {
                            shapeFileFeatureLayer.FeatureIdsToExclude.Remove(featureID);
                        }
                    }
                }
                else
                {
                    if (featureIDs.Count > 0)
                    {
                        foreach (string id in featureIDs)
                        {
                            shapeFileFeatureLayer.FeatureIdsToExclude.Add(id);
                        }
                    }
                    else
                    {
                        if (!shapeFileFeatureLayer.FeatureIdsToExclude.Contains(featureID))
                        {
                            shapeFileFeatureLayer.FeatureIdsToExclude.Add(featureID);
                        }
                    }

                }
            }

            if (_selectedFactors.Any(f => f.CumulativeFactor))
            {
                //there's at least one cumulative factor selected.
                foreach (ThinkGeoMarker modelThinkGeoCumulativeMarker in _modelThinkGeoCumulativeMarkers)
                {
                    Region r = (Region)modelThinkGeoCumulativeMarker.Tag;
                    if (r.IDRegion == region.IDRegion)
                    {
                        modelThinkGeoCumulativeMarker.Visible = visible && shapeFileFeatureLayer.IsVisible;
                    }
                }
            }
        }

        private void btnPickColorCustom_Click(object sender, EventArgs e)
        {
            ScaleCustomColor customForm = new ScaleCustomColor();
            if (customForm.ShowDialog() == DialogResult.OK)
            {
                SetColorScheme();
                RefreshMarkers();
                RefreshMap();
            }
        }

        private void pnlSelectedColor_Click(object sender, EventArgs e)
        {
            if (cdSelectMapColor.ShowDialog() == DialogResult.OK)
            {
                pnlSelectedColor.BackColor = cdSelectMapColor.Color;
                cbMapColorScheme.SelectedIndex = 0;
                SetColorScheme();
                RefreshMarkers();
                RefreshMap();
            }
        }


        private void tsmiMapSaveAsImage_Click(object sender, EventArgs e)
        {
            const string extension = ".jpg";
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = ERMTSession.Instance.CurrentModel.Name + extension,
                Filter =
                    "Jpg Image (*.jpg)|*.jpg|Windows Bitmap (*.bmp)|*.bmp|TIFF Image(*.tif)|*.tif|GIF Image(*.gif)|*.gif"
            };
            ImageFormat saveFormat = ImageFormat.Jpeg;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (Path.GetExtension(saveFileDialog.FileName).ToLower())
                {
                    case "jpg":
                        {
                            saveFormat = ImageFormat.Jpeg;
                            break;
                        }
                    case "gif":
                        {
                            saveFormat = ImageFormat.Gif;
                            break;
                        }
                    case "tif":
                        {
                            saveFormat = ImageFormat.Tiff;
                            break;
                        }
                    case "bmp":
                        {
                            saveFormat = ImageFormat.Bmp;
                            break;
                        }
                }

                Bitmap bitmap = new Bitmap(winformsMap1.Width, winformsMap1.Height);
                bitmap.SetResolution(300.0F, 300.0F);
                //winformsMap1.dr
                winformsMap1.DrawToBitmap(bitmap, winformsMap1.DisplayRectangle);
                //Bitmap auxBitmap = winformsMap1.GetBitmap(winformsMap1.Width, winformsMap1.Height);
                bitmap.Save(saveFileDialog.FileName, saveFormat);

                ////SaveAsImage(saveFileDialog.FileName, saveFormat, winformsMap1.Width, winformsMap1.Height);
                //SaveAsImage(saveFileDialog.FileName, saveFormat, 1440, 900);
            }
        }

        private void tsmiMapSaveAsHighResolutionImage_Click(object sender, EventArgs e)
        {
            const string extension = ".jpg";
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = ERMTSession.Instance.CurrentModel.Name + extension,
                Filter =
                    "Jpg Image (*.jpg)|*.jpg|Windows Bitmap (*.bmp)|*.bmp|TIFF Image(*.tif)|*.tif|GIF Image(*.gif)|*.gif"
            };
            ImageFormat saveFormat = ImageFormat.Jpeg;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (Path.GetExtension(saveFileDialog.FileName).ToLower())
                {
                    case "jpg":
                        {
                            saveFormat = ImageFormat.Jpeg;
                            break;
                        }
                    case "gif":
                        {
                            saveFormat = ImageFormat.Gif;
                            break;
                        }
                    case "tif":
                        {
                            saveFormat = ImageFormat.Tiff;
                            break;
                        }
                    case "bmp":
                        {
                            saveFormat = ImageFormat.Bmp;
                            break;
                        }
                }

                SaveAsImage(saveFileDialog.FileName, saveFormat, 4967, 3508);
            }
        }

        public void SaveAsImage(String fileName, ImageFormat imageFormat, int height, int width)
        {
            Bitmap bitmap = new Bitmap(width, height);
            MapEngine mapEngine = new MapEngine
            {
                CurrentExtent =
                    ExtentHelper.GetDrawingExtent(winformsMap1.CurrentExtent, bitmap.Width, bitmap.Height)
            };


            GeoBrush gb = new GeoSolidBrush(new GeoColor(winformsMap1.BackColor.A, winformsMap1.BackColor.R, winformsMap1.BackColor.G, winformsMap1.BackColor.B));
            mapEngine.BackgroundFillBrush = gb;
            if (tsmiMapShowGrid.Checked)
            {
                mapEngine.StaticLayers.Add(_gridLayer);
            }

            foreach (ShapeFileFeatureLayer shapeFileFeatureLayer in _modelLayers)
            {
                mapEngine.StaticLayers.Add(shapeFileFeatureLayer);
            }

            //now add the markers.
            List<int> markerTypeIds = (from CheckBox c in flpMarkerType.Controls where c.Checked select ((MarkerType)c.Tag).IDMarkerType).ToList();
            foreach (int idMarkerType in markerTypeIds)
            {
                MarkerType mt = MarkerTypeHelper.Get(idMarkerType);
                InMemoryFeatureLayer markerLayer = new InMemoryFeatureLayer();
                Image image = ImageHelper.GetMarkerImage(mt);
                var ms = new MemoryStream();
                image.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                markerLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(new GeoImage(ms));
                markerLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                markerLayer.FeatureSource.Open();
                markerLayer.FeatureSource.BeginTransaction();
                foreach (ThinkGeoMarker item in _markersOverlay.Markers.Where(m => ((Marker)m.Tag).IDMarkerType == idMarkerType))
                {
                    markerLayer.InternalFeatures.Add(new Feature(item.Position));
                }
                markerLayer.FeatureSource.CommitTransaction();
                markerLayer.FeatureSource.Close();
                mapEngine.StaticLayers.Add(markerLayer);
            }

            //now add the cumulative factors.
            foreach (ThinkGeoMarker cumulativeFactor in _modelThinkGeoCumulativeMarkers.Where(m => m.Visible))
            {
                InMemoryFeatureLayer markerLayer = new InMemoryFeatureLayer();
                Panel pnl = (Panel)cumulativeFactor.Controls[0];
                Bitmap bmp = new Bitmap(pnl.Width, pnl.Height);
                pnl.DrawToBitmap(bmp, new Rectangle(0, 0, pnl.Width, pnl.Height));
                var ms = new MemoryStream();
                bmp.Save(ms, ImageFormat.Jpeg);
                ms.Position = 0;
                markerLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(new GeoImage(ms));
                markerLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                markerLayer.FeatureSource.Open();
                markerLayer.FeatureSource.BeginTransaction();
                markerLayer.InternalFeatures.Add(new Feature(cumulativeFactor.Position));
                markerLayer.FeatureSource.CommitTransaction();
                markerLayer.FeatureSource.Close();
                mapEngine.StaticLayers.Add(markerLayer);
            }

            mapEngine.OpenAllLayers();
            mapEngine.DrawStaticLayers(bitmap, GeographyUnit.DecimalDegree);
            mapEngine.CloseAllLayers();

            bitmap.Save(fileName, imageFormat);
        }

        private void tsmiSaveAsKML_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = ERMTSession.Instance.CurrentModel.Name + ".kml",
                Filter = "KML (*.kml)|*.kml"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            String kmlPath = Path.GetDirectoryName(saveFileDialog.FileName);

            String kmlImagePath = kmlPath + "\\Images\\";

            if (!Directory.Exists(kmlImagePath))
            {
                Directory.CreateDirectory(kmlImagePath);
            }

            KmlMapEngine kmlMapEngine = new KmlMapEngine();
            //mapEngine.StaticLayers.Add(austinRoadLayer);

            foreach (ShapeFileFeatureLayer shapeFileFeatureLayer in _modelLayers)
            {
                if (shapeFileFeatureLayer.IsVisible)
                {
                    kmlMapEngine.StaticLayers.Add(shapeFileFeatureLayer);
                }
            }

            kmlMapEngine.OpenAllLayers();
            //mapEngine.CurrentExtent = austinRoadLayer.GetBoundingBox();
            kmlMapEngine.CurrentExtent = winformsMap1.CurrentExtent;

            StringBuilder kmlBuilder = new StringBuilder();
            kmlMapEngine.DrawStaticLayers(kmlBuilder, GeographyUnit.DecimalDegree);

            if (tsmiMapRegionName.Checked)
            {
                //need to add region names.
                kmlBuilder.Replace("</Document></kml>", kmlMapEngine.DrawRegionNames(GetCheckedRegions(), _modelLayers) + "</Document></kml>");
            }

            if (tsbMarkers.Checked)
            {
                //need to add markers
                foreach (ThinkGeoMarker modelThinkGeoMarker in _modelThinkGeoMarkers.Where(m => m.Visible))
                {
                    kmlBuilder.Replace("</Document></kml>", kmlMapEngine.GetMarkerXML(modelThinkGeoMarker) + "</Document></kml>");
                }

                List<int> markerTypeIds = (from CheckBox c in flpMarkerType.Controls where c.Checked select ((MarkerType)c.Tag).IDMarkerType).ToList();
                foreach (int idMarkerType in markerTypeIds)
                {
                    MarkerType mt = MarkerTypeHelper.Get(idMarkerType);
                    string fileName = DirectoryAndFileHelper.GetMarkerTypeImagePath(mt.Symbol);
                    File.Copy(fileName, kmlImagePath + mt.Symbol, true);
                }
            }

            foreach (ThinkGeoMarker marker in _modelThinkGeoCumulativeMarkers.Where(m => m.Visible))
            {
                //need to add markers
                kmlBuilder.Replace("</Document></kml>", kmlMapEngine.GetCumulativeFactorXML(marker) + "</Document></kml>");
                CreateCumulativeFactorImage(marker, kmlImagePath);
            }



            kmlMapEngine.CloseAllLayers();

            string result = kmlBuilder.ToString();
            StreamWriter sw = new StreamWriter(saveFileDialog.FileName);
            sw.Write(result);
            sw.Close();

            CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("SaveAsKMLOK"));

        }

        private static void CreateCumulativeFactorImage(ThinkGeoMarker cumulativeFactorMarker, string directory)
        {
            Panel pnl = (Panel)cumulativeFactorMarker.Controls[0];
            int width = pnl.Width;
            int height = pnl.Height;
            Bitmap bmp = new Bitmap(width, height);
            pnl.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));

            bmp.Save(directory + "\\" + cumulativeFactorMarker.Name + ".jpg", ImageFormat.Jpeg);
        }

        private void tvMapRegions_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Region region = (((Region)e.Node.Tag));

            SetRegionVisibility(region, e.Node.Checked);

            RefreshMap();
        }

        private void tsmiMapRegionName_Click(object sender, EventArgs e)
        {
            tsmiMapRegionName.Checked = !tsmiMapRegionName.Checked;

            SetRegionNameAndDataVisibility();

            if (tsbMarkers.Checked)
            {
                RefreshMarkers();
            }

            RefreshMap();
        }

        private void SetRegionNameAndDataVisibility()
        {
            foreach (ShapeFileFeatureLayer shapeFileFeatureLayer in _modelLayers)
            {
                if ((tsmiMapRegionName.Checked || tsmiMapDisplayDataValue.Checked))
                {
                    if (!(shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Contains(_regionNameStyle)))
                    {
                        shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(_regionNameStyle);
                        shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                    }
                }
                else
                {
                    if (shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Contains(_regionNameStyle))
                    {
                        shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Remove(_regionNameStyle);
                    }

                    shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshMap();
        }

        private void dtpMapDataDate_ValueChanged(object sender, EventArgs e)
        {
            if (!_riskMappingControlFirstCharge)
            {
                CacheModelData();
                CalculateFactorData();
                LoadMarkers();
                ClearCumulativeFactors();
                GetCumulativeFactors();
                AddMarkersToMarkersOverlay();
                SetColorScheme();
                RefreshMarkers();
                RefreshMap();
            }
        }

        public void SetCurrentExtent()
        {
            try
            {
                ShapeFileFeatureLayer mainShapeFileFeatureLayer = MapHelper.GetRegionFeatureLayer(ERMTSession.Instance.CurrentModelMainRegion, _modelLayers);

                if (mainShapeFileFeatureLayer != null)
                {
                    mainShapeFileFeatureLayer.Open();

                    if (ERMTSession.Instance.CurrentModelMainRegion.ShapeFileIndex != null)
                    {
                        Feature feature = mainShapeFileFeatureLayer.FeatureSource.GetFeatureById(
                            Convert.ToString(ERMTSession.Instance.CurrentModelMainRegion.ShapeFileIndex + 1),
                            ReturningColumnsType.NoColumns);
                        winformsMap1.CurrentExtent = feature.GetBoundingBox();
                    }
                    else
                    {
                        winformsMap1.CurrentExtent = mainShapeFileFeatureLayer.GetBoundingBox();
                    }


                    mainShapeFileFeatureLayer.Close();
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Reconfigures the layer visibility
        /// </summary>
        private void ConfigureShapesVisibility()
        {
            foreach (object control in tsMapOptions.Items)
            {
                if (control.GetType() == typeof(ToolStripButton))
                {
                    ToolStripButton btn = (ToolStripButton)control;
                    if (Convert.ToInt32(btn.Tag) < 8 && btn.Tag != null)
                    {
                        SetLayerVisibility(btn);
                    }
                }
            }
        }

        private void SetLayerVisibility(ToolStripButton tsb)
        {
            int regionLevel = Convert.ToInt32(tsb.Tag);
            List<Region> checkedRegions = GetCheckedRegions(tvMapRegions.Nodes);

            foreach (ShapeFileFeatureLayer shapeFileFeatureLayer in _modelLayers.Where(ml => Convert.ToInt32(ml.Name.Substring(0, 1)) == regionLevel))
            {
                shapeFileFeatureLayer.IsVisible = tsb.Checked;
                shapeFileFeatureLayer.FeatureIdsToExclude.Clear();

                foreach (ThinkGeoMarker thinkGeoCumulativeMarker in _modelThinkGeoCumulativeMarkers)
                {
                    Region aux = (Region)thinkGeoCumulativeMarker.Tag;
                    if (thinkGeoCumulativeMarker.Controls[0].GetType() != typeof(Panel))
                    {
                        continue;
                    }
                    ModelFactor mf = (ModelFactor)((Panel)thinkGeoCumulativeMarker.Controls[0]).Tag;
                    if (aux.RegionLevel == regionLevel && _selectedFactors.Any(f => f.IdFactor == mf.IDFactor) && GetCheckedRegions().Any(r => r.IDRegion == aux.IDRegion))
                    {
                        thinkGeoCumulativeMarker.Visible = tsb.Checked;
                    }
                }

                if (shapeFileFeatureLayer.IsVisible)
                {
                    try
                    {
                        shapeFileFeatureLayer.Open();
                        List<Feature> shapeFileFeatures = shapeFileFeatureLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns).ToList();
                        foreach (Feature feature in shapeFileFeatures)
                        {
                            FileInfo shapefileFileInfo = new FileInfo(shapeFileFeatureLayer.ShapePathFileName);
                            int index = Convert.ToInt32(feature.Id) - 1;
                            //Region region =
                            //    GetRegionByShapeFileAndIndex(shapefileFileInfo, index)
                            //    ?? GetRegionByShapeFile(shapefileFileInfo);

                            if (regionLevel < ERMTSession.Instance.CurrentModelMainRegion.RegionLevel)
                            {
                                //the region's level is lower that the current model's. So there's no need to hide or show
                                //regions based on their featureID
                                continue;
                            }

                            Region region =
                                GetRegionByShapeFileAndIndex(shapefileFileInfo, index);

                            if (region == null && (ERMTSession.Instance.CurrentModelMainRegion.RegionLevel != regionLevel && ERMTSession.Instance.CurrentModelMainRegion.ShapeFileIndex != null))
                            {
                                //the region isn't included in the current model. It has to be hidden.
                                shapeFileFeatureLayer.FeatureIdsToExclude.Add(feature.Id);
                                continue;
                            }

                            if (region == null)
                            {
                                continue;
                            }

                            if (region.RegionLevel < ERMTSession.Instance.CurrentModelMainRegion.RegionLevel)
                            {
                                //if the region's level is lower that the current model's level, there's nothing to do.
                                continue;
                            }

                            if (!checkedRegions.Select(r => r.IDRegion).Contains(region.IDRegion))
                            {
                                shapeFileFeatureLayer.FeatureIdsToExclude.Add(feature.Id);
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        if (shapeFileFeatureLayer.IsOpen)
                        {
                            shapeFileFeatureLayer.Close();
                        }
                    }

                }
            }

            if (tsbMarkers.Checked)
            {
                RefreshMarkers();
            }
        }

        /// <summary>
        /// Returns a region in _modelRegions having the region level equal to the parameter. Only returns a region if it's the only one
        /// with that level.
        /// </summary>
        /// <param name="regionLevel"></param>
        /// <returns></returns>
        private Region GetRegionByRegionLevel(int regionLevel)
        {
            List<Region> aux = _modelRegions.Where(r => r.RegionLevel == regionLevel).ToList();

            if (aux.Count == 1)
            {
                return aux[0];
            }

            return null;
        }

        private void LoadShapeFiles(Model model)
        {
            List<String> errors = new List<string>();

            _modelLayers = MapHelper.GetModelFeatureLayers(model, ref errors);

            foreach (ShapeFileFeatureLayer shapeFileFeatureLayer in _modelLayers)
            {
                shapeFileFeatureLayer.FeatureSource.CustomColumnFetch += FeatureSource_CustomColumnFetch;
            }
        }

        /// <summary>
        /// Occurs when one of the custom columns we've added to each shapefile requires a value.
        /// After every winformMap.Refresh, the custom data gets cleaned.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FeatureSource_CustomColumnFetch(object sender, CustomColumnFetchEventArgs e)
        {
            if (e.ColumnValue != string.Empty)
            {
                return;
            }

            ShapeFileFeatureSource sffs = new ShapeFileFeatureSource();
            if (sender.GetType() == (typeof(ShapeFileFeatureSource)))
            {
                sffs = (ShapeFileFeatureSource)sender;
            }
            FileInfo shapefileFileInfo = new FileInfo(sffs.ShapePathFileName);
            //Region currentRegion = GetRegionByShapeFileAndIndex(shapefileFileInfo, Convert.ToInt32(e.Id) - 1) ??
            //                       GetRegionByShapeFile(shapefileFileInfo);

            Region currentRegion = GetRegionByShapeFileAndIndex(shapefileFileInfo, Convert.ToInt32(e.Id) - 1);

            //if (currentRegion == null)
            //{
            //    //try to get it from the DB.
            //    currentRegion = RegionHelper.GetRegionByShapeFileAndIndex(shapefileFileInfo, Convert.ToInt32(e.Id) - 1);
            //}

            if (currentRegion != null && currentRegion.RegionName.ToLower() == "mendoza")
            {
                string hochochocoasd = "asdasd";
            }

            if (e.ColumnName == "ermtregionname")
            {
                if (tsmiMapDisplayDataValue.Checked)
                {
                    string displayValue = string.Empty;
                    if (currentRegion != null)
                    {
                        if (tsmiMapRegionName.Checked)
                        {
                            displayValue = currentRegion.RegionName;
                        }

                        int value = (int)(currentRegion != null && _calculatedData.Count > 0 && _calculatedData[currentRegion.RegionLevel] != null &&
                            _calculatedData[currentRegion.RegionLevel].ContainsKey(currentRegion.IDRegion)
                            ? _calculatedData[currentRegion.RegionLevel][currentRegion.IDRegion]
                            : 0);
                        if (value != 0)
                        {
                            if (tsmiMapRegionName.Checked)
                            {
                                displayValue += "\r\n";
                            }

                            int aux = displayValue.Length / 2;
                            for (int i = 0; i < aux; i++)
                            {
                                displayValue += " ";
                            }

                            displayValue += value;
                        }
                    }

                    e.ColumnValue = displayValue != "0" ? displayValue : string.Empty;
                }
                else
                {
                    e.ColumnValue = (currentRegion != null ? currentRegion.RegionName : string.Empty);
                }

            }

            if (e.ColumnName == "factorvalue")
            {
                int value = (int)(currentRegion != null && _calculatedData.Count > 0 && _calculatedData[currentRegion.RegionLevel] != null &&
                            _calculatedData[currentRegion.RegionLevel].ContainsKey(currentRegion.IDRegion)
                            ? _calculatedData[currentRegion.RegionLevel][currentRegion.IDRegion]
                            : 0);
                e.ColumnValue = value.ToString();
            }
        }

        /// <summary>
        /// Caches in memory the model's data.
        /// </summary>
        private void CacheModelData()
        {
            //clear what we have.
            _modelData = new List<ModelFactorData>();
            _modelDataCumulative = new List<ModelFactorData>();

            //get all the regions related to the model.
            List<Region> regions = RegionHelper.GetAllRelated(ERMTSession.Instance.CurrentModel.IDRegion);

            string regionIDs = regions.Aggregate(string.Empty, (current, region) => current + region.IDRegion + ",");

            regionIDs = regionIDs.Substring(0, regionIDs.Length - 1);

            foreach (ModelFactor modelCumulativeFactor in _modelFactorsCumulative)
            {
                List<ModelFactorData> mfd = ModelFactorDataHelper.GetBetweenDatesModelFactorRegion(dtpMapDateFrom.Value.Subtract(dtpMapDateFrom.Value.TimeOfDay), dtpMapDateTo.Value.AddDays(1).Subtract(dtpMapDateTo.Value.TimeOfDay.Add(new TimeSpan(0, 0, 1))), modelCumulativeFactor.IDModelFactor, regionIDs);
                _modelDataCumulative.AddRange(mfd);
            }

            foreach (ModelFactor modelFactor in _modelRegularFactors)
            {
                _modelData.AddRange(ModelFactorDataHelper.GetMapData(modelFactor.IDModelFactor, regionIDs, dtpMapDateFrom.Value.Subtract(dtpMapDateFrom.Value.TimeOfDay), dtpMapDateTo.Value.AddDays(1).Subtract(dtpMapDateTo.Value.TimeOfDay.Add(new TimeSpan(0, 0, 1)))));
            }
        }

        private void SetColorScheme()
        {
            _modelMaxScale = 0;
            _modelMinScale = 1000000000;

            //obtenemos máximos y mínimos en todo el modelo.
            List<int> mfaux = (from a in _modelData select a.IDModelFactor).Distinct().ToList();
            foreach (int idModelFactor in mfaux)
            {
                ModelFactor mf = _modelFactors.FirstOrDefault(mf2 => mf2.IDModelFactor == idModelFactor);

                if (mf != null && mf.ScaleMax > _modelMaxScale)
                {
                    _modelMaxScale = mf.ScaleMax;
                }
                if (mf != null && mf.ScaleMin < _modelMinScale)
                {
                    _modelMinScale = mf.ScaleMin;
                }
            }

            if (_modelMinScale == 1000000000)
            {
                _modelMinScale = 0;
            }

            //si hay factores de escala seleccionados, entonces reseteamos los valores y los seteamos con lo que está seleccionado.
            //List<Factor> selectedScaleFactors = GetCheckedFactors().Where(f => !f.CumulativeFactor).ToList();
            List<ModelFactor> selectedScaleFactors =
                GetCheckedModelFactors().Where(mf => !(FactorHelper.Get(mf.IDFactor).CumulativeFactor)).ToList();

            if (selectedScaleFactors.Count > 0)
            {
                _modelMaxScale = 0;
                _modelMinScale = 1000000000;
            }

            foreach (ModelFactor factor in selectedScaleFactors)
            {
                Boolean factorInCurrentData = _modelData.Any(mfd => mfd.IDModelFactor == factor.IDModelFactor
                    && mfd.Date >= dtpMapDateFrom.Value && mfd.Date <= dtpMapDateTo.Value);

                if (factor.ScaleMax > _modelMaxScale && factorInCurrentData)
                {
                    _modelMaxScale = factor.ScaleMax;
                }

                if (factor.ScaleMin < _modelMinScale && factorInCurrentData)
                {
                    _modelMinScale = factor.ScaleMin;
                }
            }

            if (_modelMinScale == 1000000000)
            {
                //means that there's a scale factor selected, but it had no data for the selected dates.
                _modelMinScale = 0;
            }

            btnPickColorCustom.Enabled = false;

            if (cbMapColorScheme.SelectedIndex != 0)
            {
                pnlSelectedColor.BackColor = DefaultBackColor;
            }

            switch (cbMapColorScheme.SelectedIndex)
            {
                case 1:
                    {
                        _modelClassBreakStyle = ColorHelper.GetTrafficLightClassBreakStyle(((_modelMaxScale - _modelMinScale) + 1));
                        break;
                    }
                case 2:
                    {
                        _modelClassBreakStyle = ColorHelper.GetTemperatureClassBreakStyle(((_modelMaxScale - _modelMinScale) + 1));
                        break;
                    }
                case 3:
                    {
                        _modelClassBreakStyle = ColorHelper.GetColorGradientClassBreakStyle(Color.Gray, ((_modelMaxScale - _modelMinScale) + 1));
                        break;
                    }
                case 4:
                    {
                        btnPickColorCustom.Enabled = true;
                        _modelClassBreakStyle = ColorHelper.GetCustomClassBreakStyle(ERMTSession.Instance.CurrentModel.IDModel, ((_modelMaxScale - _modelMinScale) + 1));
                        if (_modelClassBreakStyle.ColumnName == "empty")
                        {
                            _modelClassBreakStyle = ColorHelper.GetColorGradientClassBreakStyle(pnlSelectedColor.BackColor, ((_modelMaxScale - _modelMinScale) + 1));
                        }
                        break;
                    }
                default:
                    {
                        _modelClassBreakStyle = ColorHelper.GetColorGradientClassBreakStyle(pnlSelectedColor.BackColor, ((_modelMaxScale - _modelMinScale) + 1));
                        break;
                    }
            }

            _modelClassBreakStyle.ColumnName = "factorvalue";

            foreach (ShapeFileFeatureLayer shapeFileFeatureLayer in _modelLayers)
            {
                shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(_modelClassBreakStyle);
                shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            }

            if (_scaleFactorsLegend != null)
            {
                _scaleFactorsLegend.SetColorScheme(_modelClassBreakStyle);
            }
        }

        /// <summary>
        /// Calculates the values for the regular factors that will be used by the FeatureSource_CustomColumnFetch method.
        /// </summary>
        private void CalculateFactorData()
        {
            _calculatedData = new List<Dictionary<int, decimal>>();

            dtpMapDateFrom.Value = dtpMapDateFrom.Value.Subtract(dtpMapDateFrom.Value.TimeOfDay);
            dtpMapDateTo.Value = dtpMapDateTo.Value.AddDays(1).Subtract(dtpMapDateTo.Value.TimeOfDay.Add(new TimeSpan(0, 0, 1)));

            for (int i = 0; i < 8; i++)
            {
                _calculatedData.Add(new Dictionary<int, decimal>());
            }

            foreach (Region currentRegion in _modelRegions)
            {
                #region Calculate regular factors value.
                decimal acumProm = 0;
                decimal acumWeight = 0;
                foreach (ModelFactor modelFactor in _modelRegularFactors.Where(mrf => GetCheckedFactorsIDs().Contains(mrf.IDFactor)))
                {
                    if ((from a in _modelData
                         where a.IDModelFactor == modelFactor.IDModelFactor &&
                         a.IDRegion == currentRegion.IDRegion &&
                         a.Date >= dtpMapDateFrom.Value &&
                         a.Date <= dtpMapDateTo.Value
                         select a.Data).Any())
                    {
                        decimal prom = (from a in _modelData
                                        where a.IDModelFactor == modelFactor.IDModelFactor &&
                                        a.IDRegion == currentRegion.IDRegion &&
                                        a.Date >= dtpMapDateFrom.Value &&
                                        a.Date <= dtpMapDateTo.Value
                                        select a.Data).Average();
                        prom = prom * modelFactor.Weight / 100;
                        acumProm += prom;
                        acumWeight += (decimal)modelFactor.Weight / 100;
                    }
                }
                if (acumWeight == 0)
                {
                    acumWeight = 1;
                }

                acumProm = acumProm / acumWeight;


                var level = Math.Round(acumProm, 0, MidpointRounding.AwayFromZero);

                _calculatedData[currentRegion.RegionLevel][currentRegion.IDRegion] = (acumProm > 0 ? level : 0);
                #endregion
            }
        }

        /// <summary>
        /// Returns a region in _modelRegions having the specified shapefile and index
        /// </summary>
        /// <param name="shapeFileInfo"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private Region GetRegionByShapeFileAndIndex(FileInfo shapeFileInfo, int index)
        {
            Region aux = null;

            String databaseShapeFileName =
                shapeFileInfo.Directory.ToString().Substring(shapeFileInfo.Directory.ToString().Length - 1, 1) + "\\" +
                shapeFileInfo.Name;

            aux = _modelRegions.FirstOrDefault(r => r.ShapeFileName.ToLower().Equals(databaseShapeFileName.ToLower()) &&
                ((index == 0 && r.ShapeFileIndex == null) || (r.ShapeFileIndex == index)));

            return aux;
        }

        private Region GetRegionByShapeFile(FileInfo shapeFileInfo)
        {
            Region aux = null;

            String databaseShapeFileName =
                shapeFileInfo.Directory.ToString().Substring(shapeFileInfo.Directory.ToString().Length - 1, 1) + "\\" +
                shapeFileInfo.Name;

            if (_modelRegions.Where(r => r.ShapeFileName.ToLower().Equals(databaseShapeFileName.ToLower()))
                    .ToList()
                    .Count == 1)
            {
                aux = _modelRegions.FirstOrDefault(r => r.ShapeFileName.ToLower().Equals(databaseShapeFileName.ToLower()));
            }

            return aux;
        }

        private List<int> GetCheckedFactorsIDs()
        {
            return (from object control in flpMapFactors.Controls where control.GetType() == typeof(CheckBox) select (CheckBox)control into chk where chk.Checked select ((ModelFactor)chk.Tag).IDFactor).ToList();
        }

        private List<Factor> GetCheckedFactors()
        {
            return GetCheckedFactorsIDs().Select(idFactor => FactorHelper.Get(idFactor)).ToList();
        }

        private List<ModelFactor> GetCheckedModelFactors()
        {
            return (from object control in flpMapFactors.Controls where control.GetType() == typeof(CheckBox) select (CheckBox)control into chk where chk.Checked select ((ModelFactor)chk.Tag)).ToList();
        }

        private List<Region> GetCheckedRegions()
        {
            return GetCheckedRegions(tvMapRegions.Nodes);
        }

        private List<Region> GetCheckedRegions(TreeNodeCollection nodes)
        {
            List<Region> aux = new List<Region>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    Region region = (Region)node.Tag;
                    aux.Add(region);
                }
                aux.AddRange(GetCheckedRegions(node.Nodes));
            }
            return aux;
        }

        private void LoadRegions(Model model)
        {
            //EL primer nodo que se muestra es el seleccionado para el modelo.
            int modelLevel = 0;
            tvMapRegions.Nodes.Clear();
            tvChartRegions.Nodes.Clear();
            Region modelRegion = RegionHelper.Get(model.IDRegion);
            List<Region> regions = new List<Region>();
            //agrego el mundo, si no es la base del modelo
            if (model.IDRegion != 1)
            {
                regions.Add(RegionHelper.GetWorld());
            }

            if (modelRegion.IDParent.HasValue && modelRegion.IDParent != 1)
            {
                //si el padre de la region del modelo NO es el mundo, agrego el padre.
                regions.Add(RegionHelper.Get(modelRegion.IDParent.Value));
            }

            //agrego la región del modelo.
            regions.Add(modelRegion);

            //agrego los hijos
            regions.AddRange(RegionHelper.GetAllChilds(modelRegion.IDRegion));

            _modelRegions = regions;

            modelLevel = modelRegion.RegionLevel;
            ((ToolStripButton)tsMapOptions.Items[modelLevel]).Checked = true;
            TreeNode node = new TreeNode { Text = modelRegion.RegionName, Name = modelRegion.IDRegion.ToString(), Tag = modelRegion };
            TreeNode nodeCharting = new TreeNode { Text = modelRegion.RegionName, Name = modelRegion.IDRegion.ToString(), Tag = modelRegion };
            TreeNode nodeTableByFactor = new TreeNode { Text = modelRegion.RegionName, Name = modelRegion.IDRegion.ToString(), Tag = modelRegion };

            Application.DoEvents();

            tvMapRegions.Nodes.Add(node);
            tvChartRegions.Nodes.Add(nodeCharting);

            ((ToolStripButton)tsMapOptions.Items[modelLevel]).Checked = true;
            tvSelectRegionsTableByFactor.Nodes.Add(nodeTableByFactor);

            AddChildRegions(tvMapRegions.Nodes[0], regions);
            AddChildRegions(tvChartRegions.Nodes[0], regions);
            AddChildRegions(tvSelectRegionsTableByFactor.Nodes[0], regions);

            tvMapRegions.SelectedNode = tvMapRegions.Nodes[0];
            tvMapRegions.Nodes[0].Checked = true;
            tvMapRegions.Focus();

            tvMapRegions.ExpandAll();
            tvChartRegions.ExpandAll();
            tvSelectRegionsTableByFactor.ExpandAll();
            LoadingForm.Fadeout();
        }

        private void LoadFactors(Model model)
        {
            flpMapFactors.Controls.Clear();
            flpChartFactors.Controls.Clear();
            _modelRegularFactors.Clear();
            _modelFactorsCumulative.Clear();

            _modelFactors = ModelFactorHelper.GetByModel(model);

            foreach (ModelFactor modelFactor in _modelFactors)
            {
                Factor factor = FactorHelper.Get(modelFactor.IDFactor);
                CheckBox chkModelFactor = new CheckBox { Text = factor.Name, AutoSize = false, Tag = modelFactor };
                if (factor.CumulativeFactor)
                {
                    MapHelper.CreateCumulativeFactorImage(factor);
                    _modelFactorsCumulative.Add(modelFactor);
                    chkModelFactor.Text += " - " + ResourceHelper.GetResourceText("Cumulative");
                }
                else
                {
                    _modelRegularFactors.Add(modelFactor);
                }

                chkModelFactor.CheckedChanged += chkModelFactor_CheckedChanged;
                chkModelFactor.AutoSize = false;
                chkModelFactor.Height = 20;
                chkModelFactor.Width = flpMapFactors.Width - 40;
                chkModelFactor.MaximumSize = new Size(240, 40);
                flpMapFactors.Controls.Add(chkModelFactor);

                CheckBox cbFactor = new CheckBox { Text = factor.Name, AutoSize = false, Tag = modelFactor, Checked = false };
                if (factor.CumulativeFactor)
                {
                    cbFactor.Text += " - " + ResourceHelper.GetResourceText("Cumulative");
                }

                cbFactor.CheckedChanged += chkChartFactor_CheckedChanged;
                cbFactor.AutoSize = false;
                cbFactor.Height = 20;
                cbFactor.Width = flpChartFactors.Width - 40;
                cbFactor.MaximumSize = new Size(240, 40);
                flpChartFactors.Controls.Add(cbFactor);
            }

            foreach (CheckBox chkFactor in flpMapFactors.Controls)
            {
                ModelFactorData mfd = ModelFactorDataHelper.GetModelFactorLastDate(model.IDModel);
                if (mfd != null)
                {
                    if (((ModelFactor)chkFactor.Tag).IDModelFactor == mfd.IDModelFactor)
                    {
                        chkFactor.Checked = true;
                        break;
                    }
                }
            }

            if (flpMapFactors.Controls.Count > 0)
            {
                ((CheckBox)flpMapFactors.Controls[0]).Checked = true;
            }

            if (flpMapFactors.Controls.Count == 0)
            {
                flpMapFactors.Controls.Add(new Label { Text = ResourceHelper.GetResourceText("NoFactorsInModel"), AutoSize = true, Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold) });
                flpChartFactors.Controls.Add(new Label { Text = ResourceHelper.GetResourceText("NoFactorsInModel"), AutoSize = true, Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold) });
            }
        }

        private void chkModelFactor_CheckedChanged(object sender, EventArgs e)
        {
            ModelFactor modelFactor = ((ModelFactor)((CheckBox)sender).Tag);
            Factor factor = FactorHelper.Get(modelFactor.IDFactor);
            CheckBox chkSender = ((CheckBox)sender);

            if (chkSender.Checked)
            {
                _selectedFactors.Add(factor);
            }
            else
            {
                Factor f = _selectedFactors.First(f2 => f2.IdFactor == factor.IdFactor);
                while (f != null)
                {
                    _selectedFactors.Remove(f);
                    f = _selectedFactors.FirstOrDefault(f2 => f2.IdFactor == factor.IdFactor);
                }
            }

            if (_riskMappingControlFirstCharge)
            {
                return;
            }

            //a factor has been checked or unchecked. We need to calculate the data again.
            CalculateFactorData();

            if (factor.CumulativeFactor)
            {
                ClearCumulativeFactors();
                GetCumulativeFactors();
                UpdateCumulativeFactorsLegend();
            }
            else
            {
                SetColorScheme();
                UpdateScaleFactorLegend();
            }

            RefreshMarkers();
            RefreshMap();
        }

        private void ClearCumulativeFactors()
        {
            SetCumulativeMarkersVisibility(false);
            _modelThinkGeoCumulativeMarkers = new List<ThinkGeoMarker>();
        }

        private void GetCumulativeFactors()
        {
            List<ModelFactor> selectedCumulativeModelFactors =
                _modelFactorsCumulative.Where(mrf => GetCheckedFactorsIDs().Contains(mrf.IDFactor)).ToList();

            List<Region> regionsWithCumulativeDate = ModelFactorDataHelper.GetRegionsWithData(dtpMapDateFrom.Value,
                dtpMapDateTo.Value, selectedCumulativeModelFactors);

            foreach (Region currentRegion in regionsWithCumulativeDate)
            {
                bool visible = GetCheckedRegions().Select(r => r.IDRegion).Contains(currentRegion.IDRegion);

                List<ModelFactor> regionModelFactors = new List<ModelFactor>();
                List<decimal> regionValues = new List<decimal>();
                foreach (ModelFactor modelFactorCumulative in selectedCumulativeModelFactors)
                {
                    decimal sum = 0;

                    if ((from a in _modelDataCumulative
                         where
                             a.IDModelFactor == modelFactorCumulative.IDModelFactor &&
                             a.IDRegion == currentRegion.IDRegion &&
                             a.Date >= dtpMapDateFrom.Value &&
                             a.Date <= dtpMapDateTo.Value
                         select a.Data).Any())
                    {
                        sum = (from a in _modelDataCumulative
                               where a.IDModelFactor == modelFactorCumulative.IDModelFactor
                               && a.IDRegion == currentRegion.IDRegion &&
                               a.Date >= dtpMapDateFrom.Value &&
                               a.Date <= dtpMapDateTo.Value

                               select a.Data).Sum();
                    }

                    if (sum != 0)
                    {
                        regionModelFactors.Add(modelFactorCumulative);
                        regionValues.Add(sum);

                    }
                }
                AddCumulativeFactor(currentRegion, regionModelFactors, regionValues, visible);
            }

            AddMarkersToCumulativeOverlay();
        }

        private void AddCumulativeFactor(Region region, List<ModelFactor> regionModelFactors, List<decimal> regionValues, Boolean visible)
        {
            ShapeFileFeatureLayer shapeFileFeatureLayer = MapHelper.GetRegionFeatureLayer(region, _modelLayers);
            PointShape pointShape = MapHelper.GetFeatureCenterPoint(shapeFileFeatureLayer, (region.ShapeFileIndex + 1 ?? 1));

            if (pointShape == null)
            {
                return;
            }

            ThinkGeoMarker thinkGeoMarker = new ThinkGeoMarker(pointShape)
            {
                Name = region.IDRegion.ToString(),
                Image = MapHelper.GetCumulativeGenericImage(),
                Tag = region,
                YOffset = 3,
                Visible = visible
            };

            Panel pnl = new Panel { Padding = new Padding(0), Margin = new Padding(0), Height = 13, Width = 0 };

            for (int i = 0; i < regionModelFactors.Count; i++)
            {
                ModelFactor regionModelFactor = regionModelFactors[i];
                pnl.Tag = regionModelFactor;
                String value = regionValues[i].ToString();
                thinkGeoMarker.Visible = GetCheckedRegions().Any(r => r.IDRegion == region.IDRegion) && shapeFileFeatureLayer.IsVisible;
                Factor factor = _selectedFactors.First(f => f.IdFactor == regionModelFactor.IDFactor);
                Color backColor = SystemColors.ControlText;
                Color fontColor = SystemColors.ControlText;

                if (factor.Color.Split(',').Length > 1)
                {
                    backColor = ColorTranslator.FromHtml(factor.Color.Split(',')[0]);
                    fontColor = ColorTranslator.FromHtml(factor.Color.Split(',')[1]);
                }

                if (value.EndsWith("00"))
                {
                    //if the precition is 00, remove it.
                    value = value.Substring(0, value.Length - 3);
                }

                Font textFont = GetCumulativeFactorFont();
                Size size = TextRenderer.MeasureText(value, textFont);

                Label lbl = new Label
                {
                    Font = textFont,
                    Text = value,
                    BackColor = backColor,
                    Size = size,
                    ForeColor = fontColor,
                    TextAlign = ContentAlignment.TopLeft,
                    Padding = new Padding(0),
                    Margin = new Padding(0),
                    Location = new Point(pnl.Width, 0)
                };

                //JPF: https://borderless.atlassian.net/browse/ERMT-7 Cumulative Markers - Center Label
                thinkGeoMarker.XOffset += (float)((value.Length) * Convert.ToDecimal("-3.5"));

                if (lbl.Height > pnl.Height)
                {
                    pnl.Height = lbl.Height;
                }
                pnl.Width += lbl.Width;
                pnl.Controls.Add(lbl);
                lbl.BringToFront();
            }
            thinkGeoMarker.Height = pnl.Height;
            thinkGeoMarker.Width = pnl.Width;
            thinkGeoMarker.Controls.Add(pnl);
            pnl.BringToFront();

            _modelThinkGeoCumulativeMarkers.Add(thinkGeoMarker);
        }

        private Font GetCumulativeFactorFont()
        {
            Font textFont = new Font(FontFamily.GenericSansSerif, 8);
            if (tsmiMapCumulativeFactorSize_Medium.Checked)
            {
                textFont = new Font(FontFamily.GenericSansSerif, 10);
            }
            if (tsmiMapCumulativeFactorSize_Large.Checked)
            {
                textFont = new Font(FontFamily.GenericSansSerif, 12);
            }
            if (tsmiMapCumulativeFactorSize_ExtraLarge.Checked)
            {
                textFont = new Font(FontFamily.GenericSansSerif, 14);
            }

            return textFont;
        }

        private void tsmiMapCumulativeFactorSize_Click(object sender, EventArgs e)
        {
            tsmiMapCumulativeFactorSize_Small.Checked = false;
            tsmiMapCumulativeFactorSize_Medium.Checked = false;
            tsmiMapCumulativeFactorSize_Large.Checked = false;
            tsmiMapCumulativeFactorSize_ExtraLarge.Checked = false;

            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            item.Checked = true;

            ClearCumulativeFactors();
            GetCumulativeFactors();
            UpdateCumulativeFactorsLegend();

            if (tsbMarkers.Checked)
            {
                RefreshMarkers();
            }
            RefreshMap();
        }

        private void tsmiMapFactorLegend_Click(object sender, EventArgs e)
        {
            tsmiMapFactorLegend.Checked = !tsmiMapFactorLegend.Checked;
            UpdateScaleFactorLegend();
        }

        public void HideLegends()
        {
            if (tsmiMapFactorLegend.Checked)
            {
                tsmiMapFactorLegend_Click(new object(), new EventArgs());
            }

            if (tsmiMapCumulativeFactorLegend.Checked)
            {
                tsmiMapCumulativeFactorLegend_Click(new object(), new EventArgs());
            }

            if (tsmiMapMarkerLegend.Checked)
            {
                tsmiMapMarkerLegend_Click(new object(), new EventArgs());
            }

            if (tsmiChartShowLegends.Checked)
            {
                tsmiChartShowLegends_Click(new object(), new EventArgs());
            }
        }

        private void UpdateScaleFactorLegend()
        {
            if (_scaleFactorsLegend == null)
            {
                _scaleFactorsLegend = new MapLegend();
                _scaleFactorsLegend.Owner = ParentForm;
                _scaleFactorsLegend.Top = 200;
                _scaleFactorsLegend.Left = (int)tlpMap.ColumnStyles[0].Width + 120;
                _scaleFactorsLegend.SetColorScheme(_modelClassBreakStyle);
            }

            if (!tsmiMapFactorLegend.Checked)
            {
                _scaleFactorsLegend.Visible = false;
                return;
            }

            _scaleFactorsLegend.Title = string.Empty;

            var results = from f in _selectedFactors
                          group f by f.IdFactor into g
                          select new { FactorID = g.Key, Factors = g.ToList() };

            List<String> selectedScaleFactors = new List<String>();

            foreach (var result in results)
            {
                Factor firstOrDefault = result.Factors.FirstOrDefault();
                if (firstOrDefault != null && !firstOrDefault.CumulativeFactor)
                    selectedScaleFactors.Add(firstOrDefault.Name);
            }

            if (selectedScaleFactors.Count == 0)
            {
                _scaleFactorsLegend.Visible = false;
                return;
            }
            _scaleFactorsLegend.Visible = true;

            if (selectedScaleFactors.Count == 1)
            {
                _scaleFactorsLegend.Title = selectedScaleFactors[0];
            }
            else
            {
                string scaleFactorsTitle = ResourceHelper.GetResourceText("ResultantValueOf");
                foreach (String selectedScaleFactor in selectedScaleFactors)
                {
                    scaleFactorsTitle += selectedScaleFactor + ", ";
                }

                scaleFactorsTitle = scaleFactorsTitle.Substring(0, scaleFactorsTitle.Length - 2);
                _scaleFactorsLegend.Title = scaleFactorsTitle;
            }


            _scaleFactorsLegend.BackColor = winformsMap1.BackColor;
            _scaleFactorsLegend.TopMost = false;
            _scaleFactorsLegend.Owner = this.ParentForm;
            _scaleFactorsLegend.Visible = tsmiMapFactorLegend.Checked;
        }

        private void tsmiMapCumulativeFactorLegend_Click(object sender, EventArgs e)
        {
            tsmiMapCumulativeFactorLegend.Checked = !tsmiMapCumulativeFactorLegend.Checked;
            UpdateCumulativeFactorsLegend();
        }

        private void UpdateCumulativeFactorsLegend()
        {
            if (_cumulativeFactorsLegend == null)
            {
                _cumulativeFactorsLegend = new MapLegend();
                _cumulativeFactorsLegend.Owner = ParentForm;
                _cumulativeFactorsLegend.Left = (int)tlpMap.ColumnStyles[0].Width + 120;
                _cumulativeFactorsLegend.Top = 400;
            }

            List<Factor> selectedCumulativeFactors = _selectedFactors.Where(f => f.CumulativeFactor).ToList();
            if (!tsmiMapCumulativeFactorLegend.Checked || selectedCumulativeFactors.Count == 0)
            {
                _cumulativeFactorsLegend.Visible = false;
                _cumulativeFactorsLegend.Title = string.Empty;
                return;
            }
            _cumulativeFactorsLegend.Visible = true;

            _cumulativeFactorsLegend.Title = ResourceHelper.GetResourceText("CumulativeFactors");
            _cumulativeFactorsLegend.ShowCumulativeFactorsLegends(selectedCumulativeFactors);
            _cumulativeFactorsLegend.BackColor = winformsMap1.BackColor;
            _cumulativeFactorsLegend.TopMost = false;
            _cumulativeFactorsLegend.Owner = this.ParentForm;
            _cumulativeFactorsLegend.Visible = tsmiMapCumulativeFactorLegend.Checked;
        }

        private void tsmiMapMarkerLegend_Click(object sender, EventArgs e)
        {
            tsmiMapMarkerLegend.Checked = !tsmiMapMarkerLegend.Checked;
            UpdateMarkersLegend();
        }

        private void UpdateMarkersLegend()
        {
            if (_markersLegend == null)
            {
                _markersLegend = new MapLegend();
                _markersLegend.Owner = ParentForm;
                _markersLegend.Left = 400;
                _markersLegend.Top = 200 + (winformsMap1.Height / 2);
            }

            List<int> markerTypeIds =
                (from CheckBox c in flpMarkerType.Controls where c.Checked select ((MarkerType)c.Tag).IDMarkerType).ToList();
            if (!tsmiMapMarkerLegend.Checked || markerTypeIds.Count == 0)
            {
                _markersLegend.Visible = false;
                _markersLegend.Title = string.Empty;
                return;
            }
            _markersLegend.Visible = true;

            _markersLegend.Title = "Markers";
            _markersLegend.ShowMarkersLegends(markerTypeIds);
            _markersLegend.BackColor = winformsMap1.BackColor;
            _markersLegend.TopMost = false;
            _markersLegend.Owner = this.ParentForm;
            _markersLegend.Visible = tsmiMapMarkerLegend.Checked;
        }


        private void AddMarkersToCumulativeOverlay()
        {
            //SimpleMarkerOverlay markersOverlay = (SimpleMarkerOverlay)winformsMap1.Overlays.FirstOrDefault(o => o.Name == "markers");
            if (_cumulativeFactorsOverlay != null)
            {
                SetMarkersVisibility(false);
                _cumulativeFactorsOverlay.MapControl = null;
                _cumulativeFactorsOverlay.Markers.Clear();
                winformsMap1.Overlays.Remove(_cumulativeFactorsOverlay);
            }

            if (_modelThinkGeoCumulativeMarkers.Count == 0)
            {
                //there are no markers, nothing to do.
                return;
            }

            _cumulativeFactorsOverlay = MapHelper.GetCumulativeFactorsOverlay(winformsMap1);
            _cumulativeFactorsOverlay.IsVisible = true;

            foreach (ThinkGeoMarker modelCumulativeThinkGeoMarker in _modelThinkGeoCumulativeMarkers)
            {
                _cumulativeFactorsOverlay.Markers.Add(modelCumulativeThinkGeoMarker);
            }

            winformsMap1.Overlays.Add(_cumulativeFactorsOverlay);
        }

        private void tsmiMapDisplayDataValue_Click(object sender, EventArgs e)
        {
            tsmiMapDisplayDataValue.Checked = !tsmiMapDisplayDataValue.Checked;

            SetRegionNameAndDataVisibility();

            if (tsbMarkers.Checked)
            {
                RefreshMarkers();
            }

            RefreshMap();
        }

        private void RefreshMap()
        {
            winformsMap1.Refresh();
        }

        private void tsmiMapShowGrid_Click(object sender, EventArgs e)
        {
            tsmiMapShowGrid.Checked = !tsmiMapShowGrid.Checked;

            SetMapGridVisibility();

            RefreshMap();
        }

        private void cbMapColorScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetColorScheme();
            RefreshMarkers();
            RefreshMap();
        }

        private void SetMapGridVisibility()
        {
            if (tsmiMapShowGrid.Checked)
            {
                if (!_layerOverlay.Layers.Contains(_gridLayer))
                {
                    _layerOverlay.Layers.Insert(0, _gridLayer);
                }
            }
            else
            {
                if (_layerOverlay.Layers.Contains(_gridLayer))
                {
                    _layerOverlay.Layers.Remove(_gridLayer);
                }
            }
        }

        private void winformsMap1_MapClick(object sender, MapClickWinformsMapEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Right)
            {
                tsmiMapAddMarker.Enabled = true;
                tsmiMapEditMarker.Enabled = false;
                tsmiMapDeleteMarker.Enabled = false;

                tsmiMapAddMarker.Tag = e.WorldLocation;
                Point point = new Point
                {
                    X = (int)e.ScreenX,
                    Y = (int)e.ScreenY
                };

                cmMap.Show(winformsMap1, point);
            }
        }

        private void cmMap_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            ExtentInteractiveOverlayForContext extentInteractiveOverlayForContext = winformsMap1.ExtentOverlay as ExtentInteractiveOverlayForContext;
            if (extentInteractiveOverlayForContext != null)
            {
                extentInteractiveOverlayForContext.ContextMenuJustClosed = true;
            }

            RefreshMap();
        }

        private void tsmiMapAddMarker_Click(object sender, EventArgs e)
        {
            PointShape markerWorldPosition = (PointShape)((ToolStripMenuItem)sender).Tag;

            if (ERMTSession.Instance.CurrentUser.IDRole < 4)
            {
                if (MarkerTypeHelper.GetAll().Count == 0)
                {
                    CustomMessageBox.ShowError(ResourceHelper.GetResourceText("NoMarkerTypesDefined"));
                    return;
                }

                MarkerPickForm markerPF = new MarkerPickForm { StartPosition = FormStartPosition.CenterParent };
                PointShape markerPosition = markerWorldPosition;

                markerPF.Title = string.Empty;
                markerPF.TextContent = string.Empty;
                markerPF.Latitude = (decimal)markerPosition.Y;
                markerPF.Longitude = (decimal)markerPosition.X;
                markerPF.TitleColor = Color.Black;

                if (markerPF.ShowDialog() != DialogResult.OK)
                {
                    markerPF.Close();
                    return;
                }

                if (markerPF.Title != string.Empty && markerPF.Latitude.ToString() != string.Empty &&
                    markerPF.Longitude.ToString() != string.Empty && markerPF.MarkerType.IDMarkerType != 0)
                {
                    Marker marker = MarkerHelper.GetNew();
                    marker.Name = markerPF.Title;
                    marker.Description = markerPF.TextContent;
                    marker.Color = ColorTranslator.ToHtml(markerPF.TitleColor);

                    marker.Latitude = markerPF.Latitude;
                    marker.Longitude = markerPF.Longitude;
                    marker.DateFrom = markerPF.From;
                    marker.DateTo = markerPF.To;
                    marker.IDMarkerType = markerPF.MarkerType.IDMarkerType;
                    marker.IDModel = ERMTSession.Instance.CurrentModel.IDModel;

                    foreach (CheckBox c in flpMarkerType.Controls)
                    {
                        if (((MarkerType)c.Tag).IDMarkerType != marker.IDMarkerType)
                        {
                            continue;
                        }
                        c.Checked = true;
                        break;
                    }
                    markerPF.Close();

                    MarkerHelper.Save(marker);
                    if (!_modelMarkerTypes.Where(mt => mt.IDMarkerType == marker.IDMarkerType).Any())
                    {
                        //the marker type is not currently included in the model.
                        MarkerType mt = MarkerTypeHelper.Get(marker.IDMarkerType);
                        AddMarkerTypeCheckBox(mt, true);
                    }

                    LoadMarkers();
                    AddMarkersToMarkersOverlay();
                    RefreshMap();
                }
                else
                {
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("MarkerValidation"));
                }
            }
        }

        /// <summary>
        /// Loads all the Marker Types to the FlowLayoutPanel MarketTypes
        /// </summary>
        private void LoadMarkerTypes()
        {
            flpMarkerType.Controls.Clear();
            _modelMarkerTypes = new List<MarkerType>();
            List<Marker> markers = MarkerHelper.GetByModelId(ERMTSession.Instance.CurrentModel.IDModel);
            if (markers.Count == 0) return;

            foreach (int idMarkerType in (from a in markers select a.IDMarkerType).Distinct())
            {
                MarkerType mt = MarkerTypeHelper.Get(idMarkerType);
                AddMarkerTypeCheckBox(mt);
            }
        }

        private void AddMarkerTypeCheckBox(MarkerType markerType, Boolean selected = false)
        {
            if (markerType.IDMarkerType == 0) return;
            CheckBox c = new CheckBox { Text = markerType.Name, AutoSize = false, Width = 165, Height = 47, Tag = markerType, Checked = selected };
            c.AutoSize = false;
            c.CheckedChanged += chkMarkerType_CheckedChanged;
            c.Height = 25;
            c.Width = flpMarkerType.Width - 40;
            c.MaximumSize = new Size(240, 40);
            flpMarkerType.Controls.Add(c);
            _modelMarkerTypes.Add(markerType);
        }

        private void RemoveMarkerTypeCheckBox(MarkerType markerType)
        {
            CheckBox markerTypeToRemove = null;
            foreach (Control control in flpMarkerType.Controls)
            {
                if (control.GetType() == typeof(CheckBox))
                {
                    MarkerType mt = (MarkerType)control.Tag;
                    if (mt.IDMarkerType == markerType.IDMarkerType)
                    {
                        markerTypeToRemove = (CheckBox)control;
                    }
                }
            }

            if (markerTypeToRemove != null)
            {
                flpMarkerType.Controls.Remove(markerTypeToRemove);
                MarkerType aux =
                    _modelMarkerTypes.FirstOrDefault(mt2 => mt2.IDMarkerType == markerType.IDMarkerType);
                if (aux != null)
                {
                    _modelMarkerTypes.Remove(aux);
                }

            }
        }

        private void chkMarkerType_CheckedChanged(object sender, EventArgs e)
        {
            if (!_riskMappingControlFirstCharge)
            {
                LoadMarkers();
                AddMarkersToMarkersOverlay();
                UpdateMarkersLegend();
            }

            foreach (Overlay overlay in winformsMap1.Overlays)
            {
                winformsMap1.Refresh(overlay);
            }

            RefreshMap();
        }

        private void LoadMarkers()
        {
            if (!_methodRunning)
            {
                _methodRunning = true;
                _modelThinkGeoMarkers = new List<ThinkGeoMarker>();

                List<int> markerTypeIds = (from CheckBox c in flpMarkerType.Controls where c.Checked select ((MarkerType)c.Tag).IDMarkerType).ToList();
                List<Marker> markers = new List<Marker>();
                foreach (int i in markerTypeIds)
                {
                    markers.AddRange(
                        MarkerHelper.GetByModelIdAndMarkerTypeIdAndFromAndTo(
                            ERMTSession.Instance.CurrentModel.IDModel, i,
                            DateTime.Parse(dtpMapDateFrom.Value.ToShortDateString()),
                            DateTime.Parse(dtpMapDateTo.Value.ToShortDateString())));
                }

                foreach (Marker marker in markers)
                {
                    MarkerType markerType = MarkerTypeHelper.Get(marker.IDMarkerType);
                    marker.MarkerType = markerType;
                    Image markerTypeImage = ImageHelper.GetMarkerImage(markerType);
                    ThinkGeoMarker thinkGeoMarker = new ThinkGeoMarker((double)marker.Longitude,
                        (double)marker.Latitude)
                    {
                        Text = marker.Name,
                        Tag = marker,
                        Name = marker.IDMarker.ToString(),
                        ToolTipText = marker.Description,
                        Image = markerTypeImage,
                        Height = markerTypeImage.Height,
                        Width = markerTypeImage.Width
                    };

                    Panel pnl = new Panel { Height = thinkGeoMarker.Height, Width = thinkGeoMarker.Width };
                    Label lbl = new Label { Text = marker.Name, AutoSize = true, ForeColor = (!String.IsNullOrEmpty(marker.Color) ? ColorTranslator.FromHtml(marker.Color) : Color.Black) };
                    pnl.Controls.Add(lbl);
                    thinkGeoMarker.Controls.Add(pnl);
                    pnl.Dock = DockStyle.Fill;
                    lbl.Top = (thinkGeoMarker.Image.Height - lbl.Height) / 2;
                    pnl.BringToFront();
                    pnl.Visible = tsmiMapMarkerTitles.Checked;
                    pnl.Click += thinkGeoMarker_Click;
                    lbl.Click += thinkGeoMarker_Click;
                    thinkGeoMarker.Click += thinkGeoMarker_Click;
                    _modelThinkGeoMarkers.Add(thinkGeoMarker);
                }

                _methodRunning = false;
            }
        }


        private void winformsMap1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RectangleShape rs = ExtentHelper.CenterAt(winformsMap1.CurrentExtent, e.X, e.Y, winformsMap1.Width, winformsMap1.Height);
            winformsMap1.CurrentExtent = rs;
        }

        void thinkGeoMarker_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
            ThinkGeoMarker marker;
            if (sender.GetType() == typeof(Panel))
            {
                //the marker title was showing. They clicked the PANEL.
                marker = (ThinkGeoMarker)((Panel)sender).Parent;
            }
            else if (sender.GetType() == typeof(Label))
            {
                //the marker title was showing. They clicked the LABEL.
                marker = (ThinkGeoMarker)((Label)sender).Parent.Parent;
            }
            else
            {
                //the marker title was off.
                marker = (ThinkGeoMarker)sender;
            }

            if (marker == null)
            {
                return;
            }

            if (mouseEventArgs.Button == MouseButtons.Right)
            {
                tsmiMapAddMarker.Enabled = false;
                tsmiMapEditMarker.Enabled = true;
                tsmiMapDeleteMarker.Enabled = true;
                tsmiMapEditMarker.Tag = marker;
                tsmiMapDeleteMarker.Tag = marker;
                Point point = new Point();
                point.X = marker.Location.X + winformsMap1.Location.X + mouseEventArgs.X + (int)tlpMap.ColumnStyles[0].Width + 80;
                point.Y = marker.Location.Y + winformsMap1.Location.Y + mouseEventArgs.Y + 180;
                cmMap.Show(point);
            }
        }

        private void AddMarkersToMarkersOverlay()
        {
            //SimpleMarkerOverlay markersOverlay = (SimpleMarkerOverlay)winformsMap1.Overlays.FirstOrDefault(o => o.Name == "markers");
            if (_markersOverlay != null)
            {
                SetMarkersVisibility(false);
                _markersOverlay.MapControl = null;
                _markersOverlay.Markers.Clear();
                winformsMap1.Overlays.Remove(_markersOverlay);
            }

            if (_modelThinkGeoMarkers.Count == 0)
            {
                //there are no markers, nothing to do.
                return;
            }

            _markersOverlay = MapHelper.GetMarkersOverlay(winformsMap1);
            _markersOverlay.IsVisible = tsbMarkers.Checked;

            foreach (ThinkGeoMarker modelThinkGeoMarker in _modelThinkGeoMarkers)
            {
                _markersOverlay.Markers.Add(modelThinkGeoMarker);
            }

            winformsMap1.Overlays.Add(_markersOverlay);
            //winformsMap1.Overlays.Insert(0, _markersOverlay);
        }

        private void SetMarkersVisibility(Boolean visible, Boolean fullClean = false)
        {
            if (_markersOverlay != null)
            {
                foreach (var item in _markersOverlay.Markers)
                {
                    item.Visible = visible;
                    if (fullClean)
                    {
                        winformsMap1.Controls.Remove(item);
                        item.Dispose();
                    }
                }
            }
        }

        private void tsbMarkers_Click(object sender, EventArgs e)
        {
            SetMarkersVisibility();

            RefreshMap();
        }

        private void SetMarkersVisibility()
        {
            //SimpleMarkerOverlay markersOverlay = (SimpleMarkerOverlay)winformsMap1.Overlays.FirstOrDefault(o => o.Name == "markers");
            if (_markersOverlay != null)
            {
                _markersOverlay.IsVisible = tsbMarkers.Checked;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //Create a new bitmap.
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                           Screen.PrimaryScreen.Bounds.Height,
                                           PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            // Save the screenshot to the specified path that the user has chosen.
            bmpScreenshot.Save(@"c:\_code\Screenshot.png", ImageFormat.Png);
        }

        private void tsmiMapMarkerTitles_Click(object sender, EventArgs e)
        {
            tsmiMapMarkerTitles.Checked = !tsmiMapMarkerTitles.Checked;

            foreach (ThinkGeoMarker geoMarker in _modelThinkGeoMarkers)
            {
                if (geoMarker.Controls.Count > 0 && geoMarker.Controls[0].GetType() == typeof(Panel))
                {
                    Panel pnl = (Panel)geoMarker.Controls[0];
                    pnl.Visible = tsmiMapMarkerTitles.Checked;
                }
            }
        }

        private void LoadMinAndMaxDates()
        {
            DateTime minDateToShow = DateTime.Now;
            DateTime maxDateToShow = DateTime.Now;
            DateTime? markerMinDate = MarkerHelper.GetMinDateByModelID(ERMTSession.Instance.CurrentModel.IDModel);
            DateTime? markerMaxDate = MarkerHelper.GetMaxDateByModelID(ERMTSession.Instance.CurrentModel.IDModel);
            DateTime? modelFactorDataMinDate =
                ModelFactorDataHelper.GetMinorDate(ERMTSession.Instance.CurrentModel.IDModel);
            DateTime? modelFactorDataMaxDate =
                ModelFactorDataHelper.GetMajorDate(ERMTSession.Instance.CurrentModel.IDModel);

            if (markerMinDate != null)
            {
                minDateToShow = (DateTime)markerMinDate;
            }
            if (modelFactorDataMinDate.HasValue && modelFactorDataMinDate < minDateToShow)
            {
                minDateToShow = modelFactorDataMinDate.Value;
            }

            if (markerMaxDate != null)
            {
                maxDateToShow = (DateTime)markerMaxDate;
            }

            if (modelFactorDataMaxDate.HasValue && modelFactorDataMaxDate > maxDateToShow)
            {
                maxDateToShow = modelFactorDataMaxDate.Value;
            }

            minDateToShow = minDateToShow.Subtract(minDateToShow.TimeOfDay);
            maxDateToShow = maxDateToShow.AddDays(1).Subtract(maxDateToShow.TimeOfDay.Add(new TimeSpan(0, 0, 1)));

            dtpMapDateFrom.Value = minDateToShow;
            dtpChartDateFrom.Value = minDateToShow;

            dtpMapDateTo.Value = maxDateToShow;
            dtpChartDateTo.Value = maxDateToShow;
        }

        private void ResizeMarkers()
        {
            List<MarkerType> selectedMarkerTypes = (from Control control in flpMarkerType.Controls where control.GetType() == typeof(CheckBox) select (CheckBox)control into chk where chk.Checked select (MarkerType)chk.Tag).ToList();

            foreach (ThinkGeoMarker thinkGeoMarker in _markersOverlay.Markers
                .Where(m => (selectedMarkerTypes.Select(mt => mt.IDMarkerType).Contains(((Marker)m.Tag).IDMarkerType))))
            {
                Size size = new Size();
                Marker marker = (Marker)thinkGeoMarker.Tag;
                MarkerType markerType = selectedMarkerTypes.FirstOrDefault(mt => mt.IDMarkerType == marker.IDMarkerType);
                switch (markerType.Size.ToLower())
                {
                    case "small":
                        size.Height = 15;
                        size.Width = 15;
                        break;
                    case "medium":
                        size.Height = 30;
                        size.Width = 30;
                        break;
                    case "large":
                        size.Height = 60;
                        size.Width = 60;
                        break;
                    default:
                        size.Height = 30;
                        size.Width = 30;
                        break;
                }

                size.Height = size.Height - (size.Height * (tbMarkerSize.Value - 5) * 10 / 100);
                size.Width = size.Width - (size.Width * (tbMarkerSize.Value - 5) * 10 / 100);

                PointShape ps = new PointShape((double)marker.Longitude, (double)marker.Latitude);
                thinkGeoMarker.Position = ps;
                thinkGeoMarker.Height = size.Height;
                thinkGeoMarker.Width = size.Width;
                thinkGeoMarker.Image = ImageHelper.GetMarkerImage(markerType, size);


            }

        }

        private void SwitchSettingsVisibility()
        {
            switch (tcRiskMapping.SelectedIndex)
            {

                case 0:
                    {
                        tlpMap.ColumnStyles[0].Width = tlpMap.ColumnStyles[0].Width == 0 ? 290 : 0;
                        SetCurrentExtent();
                        RefreshMap();
                        break;
                    }
                case 1:
                    {
                        tlpChartMain.ColumnStyles[0].Width = tlpChartMain.ColumnStyles[0].Width == 0 ? 290 : 0;
                        break;
                    }
            }
        }

        private void tcRiskMapping_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F8:
                    {
                        SwitchSettingsVisibility();
                        break;
                    }
                case Keys.F11:
                    {
                        int hWnd;
                        switch (tcRiskMapping.SelectedIndex)
                        {

                            case 0:
                                {
                                    RiskMappingFullScreen fsm = new RiskMappingFullScreen
                                    {
                                        FormBorderStyle = FormBorderStyle.None,
                                        WindowState = FormWindowState.Maximized
                                        //TopMost = true
                                    };
                                    fsm.SetupMap(_modelLayers, _markersOverlay, _cumulativeFactorsOverlay, _pathAndPOILayerOverlay);
                                    hWnd = FindWindow("Shell_TrayWnd", "");
                                    ShowWindow(hWnd, SW_HIDE);
                                    fsm.OnClose += fsm_OnClose;

                                    if (_markersLegend != null)
                                    {
                                        _markersLegend.TopMost = true;
                                        _markersLegend.Owner = fsm;
                                    }

                                    if (_cumulativeFactorsLegend != null)
                                    {
                                        _cumulativeFactorsLegend.TopMost = true;
                                        _cumulativeFactorsLegend.Owner = fsm;
                                    }

                                    if (_scaleFactorsLegend != null)
                                    {
                                        _scaleFactorsLegend.TopMost = true;
                                        _scaleFactorsLegend.Owner = fsm;
                                    }

                                    fsm.Show();
                                    fsm.SetCurrentExtent();
                                    fsm.RefreshMap();
                                    break;
                                }
                            case 1:
                                {
                                    RiskMappingFullScreenChart fsc = new RiskMappingFullScreenChart();
                                    GenerateChart(fsc.ChartControl);
                                    fsc.FormBorderStyle = FormBorderStyle.None;
                                    fsc.WindowState = FormWindowState.Maximized;
                                    fsc.TopMost = true;
                                    hWnd = FindWindow("Shell_TrayWnd", "");
                                    ShowWindow(hWnd, SW_HIDE);
                                    _chartLegend.Owner = fsc;
                                    _chartLegend.TopMost = true;
                                    //markerTypeLegend.Visible = true;

                                    //fsc.FormClosed += fsc_FormClosed;
                                    fsc.OnClose += fsc_OnClose;

                                    fsc.Show();

                                    break;
                                }
                        }
                        break;
                    }
            }

        }

        void fsm_OnClose(object sender, EventArgs e)
        {
            if (_markersLegend != null)
            {
                _markersLegend.TopMost = false;
                _markersLegend.Owner = this.ParentForm;
            }

            if (_cumulativeFactorsLegend != null)
            {
                _cumulativeFactorsLegend.TopMost = false;
                _cumulativeFactorsLegend.Owner = this.ParentForm;
            }

            if (_scaleFactorsLegend != null)
            {
                _scaleFactorsLegend.TopMost = false;
                _scaleFactorsLegend.Owner = this.ParentForm;
            }
        }

        void fsc_OnClose(object sender, EventArgs e)
        {
            _chartLegend.TopMost = false;
            _chartLegend.Owner = this.ParentForm;
        }

        private void tbMarkerSize_ValueChanged(object sender, EventArgs e)
        {
            ResizeMarkers();

            RefreshMarkers();

            RefreshMap();
        }

        private void tsmiMapEditMarker_Click(object sender, EventArgs e)
        {
            ThinkGeoMarker thinkGeoMarker = (ThinkGeoMarker)tsmiMapEditMarker.Tag;
            Marker marker = (Marker)thinkGeoMarker.Tag;

            if (ERMTSession.Instance.CurrentUser.IDRole < 4)
            {
                MarkerPickForm markerPF = new MarkerPickForm
                {
                    StartPosition = FormStartPosition.CenterParent,
                    Title = marker.Name,
                    TextContent = marker.Description,
                    Latitude = marker.Latitude,
                    Longitude = marker.Longitude,
                    From = DateTime.Parse(marker.DateFrom.ToShortDateString()),
                    To = DateTime.Parse(marker.DateTo.ToShortDateString()),
                    MarkerType = MarkerTypeHelper.Get(marker.IDMarkerType),
                    TitleColor = (!String.IsNullOrEmpty(marker.Color) ? ColorTranslator.FromHtml(marker.Color) : Color.Black)
                };

                if (markerPF.ShowDialog() != DialogResult.OK)
                {
                    markerPF.Close();
                    return;
                }

                if (markerPF.Title != string.Empty && markerPF.Latitude.ToString() != string.Empty &&
                    markerPF.Longitude.ToString() != string.Empty && markerPF.MarkerType.IDMarkerType != 0)
                {
                    marker.Name = markerPF.Title;
                    marker.Description = markerPF.TextContent;
                    marker.Latitude = markerPF.Latitude;
                    marker.Longitude = markerPF.Longitude;
                    marker.DateFrom = markerPF.From;
                    marker.DateTo = markerPF.To;
                    marker.IDMarkerType = markerPF.MarkerType.IDMarkerType;
                    marker.IDModel = ERMTSession.Instance.CurrentModel.IDModel;
                    marker.Color = ColorTranslator.ToHtml(markerPF.TitleColor);
                    markerPF.Close();

                    MarkerHelper.Save(marker);

                    LoadMarkers();
                    AddMarkersToMarkersOverlay();
                    RefreshMap();
                }
                else
                {
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("MarkerValidation"));
                }
            }
        }

        private void tsmiMapDeleteMarker_Click(object sender, EventArgs e)
        {
            ThinkGeoMarker thinkGeoMarker = (ThinkGeoMarker)tsmiMapDeleteMarker.Tag;
            Marker marker = (Marker)thinkGeoMarker.Tag;
            MarkerHelper.Delete(marker);
            thinkGeoMarker.Visible = false;
            _markersOverlay.Markers.Remove(thinkGeoMarker);
            bool removeMarkerType = true;
            foreach (ThinkGeoMarker geoMarker in _markersOverlay.Markers)
            {
                if (((Marker)geoMarker.Tag).IDMarkerType == marker.IDMarkerType)
                {
                    removeMarkerType = false;
                }
            }

            if (removeMarkerType)
            {
                MarkerType mt = MarkerTypeHelper.Get(marker.IDMarkerType);
                RemoveMarkerTypeCheckBox(mt);
            }

            RefreshMarkers();
            RefreshMap();
        }

        private void RefreshMarkers()
        {
            SetMarkersVisibility(false);

            Application.DoEvents();

            SetMarkersVisibility(true);
        }



        #endregion

        public void SaveCurrentModelView()
        {
            string fileName = DirectoryAndFileHelper.ModelViewConfigurationFile;
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            SaveLastSettings();

            XmlDocument doc = new XmlDocument();
            if (ERMTSession.Instance.CurrentModel != null)
            {
                doc.LoadXml("<last><model id=\"" + ERMTSession.Instance.CurrentModel.IDModel + "\"/><report/></last>");
                if (ERMTSession.Instance.CurrentReport != null)
                {
                    doc.SelectSingleNode("/last/report").InnerXml = ERMTSession.Instance.CurrentReport.Parameters;
                }

                doc.Save(fileName);
            }
        }

        private void LoadLastSettings()
        {
            Report r = ERMTSession.Instance.CurrentReport;
            XmlDocument doc = new XmlDocument();
            try
            {
                tvMapRegions.AfterCheck -= tvMapRegions_AfterCheck;
                tvChartRegions.AfterCheck -= tvChartRegions_AfterCheck;
                if (r.Parameters != "")
                {
                    doc.LoadXml(r.Parameters);
                    XmlNode node = (XmlNode)doc.DocumentElement;
                    //load from file only if the app has just been open
                    if (ERMTSession.Instance.CurrentModel == null ||
                        (ERMTSession.Instance.CurrentModel != null && ERMTSession.Instance.CurrentModel.IDModel == 0))
                        ERMTSession.Instance.CurrentModel =
                            ModelHelper.GetModel(int.Parse(node.Attributes["model"].Value));

                    //Do this only the Model in session is the same as the one on the file (Check for first load no model and then import)
                    if (ERMTSession.Instance.CurrentModel.IDModel == int.Parse(node.Attributes["model"].Value))
                    {
                        dtpMapDateFrom.Value = DateTime.Parse(node.Attributes["mapDateFrom"].Value);
                        dtpMapDateTo.Value = DateTime.Parse(node.Attributes["mapDateTo"].Value);
                        dtpChartDateFrom.Value = DateTime.Parse(node.Attributes["chartDateFrom"].Value);
                        dtpChartDateTo.Value = DateTime.Parse(node.Attributes["chartDateTo"].Value);
                        MarkControlsToLoad(flpMapFactors.Controls, node, "mapFactors");
                        MarkControlsToLoad(flpChartFactors.Controls, node, "chartFactors");
                        _methodRunning = true;
                        MarkRegionsToLoad(tvMapRegions.Nodes, node, "region1");
                        MarkRegionsToLoad(tvChartRegions.Nodes, node, "region2");
                        _methodRunning = false;
                        BuildTable(tvSelectRegionTableByRegion.Nodes, node, "regionToTable");
                        BuildTable(tvSelectFactorTableByFactor.Nodes, node, "factorToTable");

                        int colorSchemeIndex = 0;
                        int.TryParse(node.Attributes["colorScheme"].Value, out colorSchemeIndex);
                        cbMapColorScheme.SelectedIndex = (colorSchemeIndex >= 0 ? colorSchemeIndex : 0);

                        int intPnlSelectedColor = Convert.ToInt32(node.Attributes["selectedColor"].Value);

                        if (intPnlSelectedColor == -986896 && cbMapColorScheme.SelectedIndex < 1)
                        {
                            cbMapColorScheme.SelectedIndex = 1;
                        }


                        pnlSelectedColor.BackColor = Color.FromArgb(intPnlSelectedColor);
                        LoadRadioButtonCollections(flpFactorCombination.Controls, node, "factorCombination");
                        MarkerTypesToLoad(flpMarkerType.Controls, node, "markerType");
                        //LoadMarkersToLastSettings(mapControl1, doc);
                        LoadToolStripItems(node.Attributes["toolStripItems"].Value);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                tvMapRegions.AfterCheck += tvMapRegions_AfterCheck;
                tvChartRegions.AfterCheck += tvChartRegions_AfterCheck;
            }

            ShowTitle();
        }

        private void SaveLastSettings()
        {
            Report r = ERMTSession.Instance.CurrentReport ?? ReportHelper.GetNew();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<report/>");


            if (ERMTSession.Instance.CurrentModel == null)
            {
                return;
            }

            XmlAttribute at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("model"));
            at.Value = ERMTSession.Instance.CurrentModel.IDModel.ToString();
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("mapDateFrom"));
            at.Value = dtpMapDateFrom.Value.ToString();
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("mapDateTo"));
            at.Value = dtpMapDateTo.Value.ToString();
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("chartDateFrom"));
            at.Value = dtpChartDateFrom.Value.ToString();
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("chartDateTo"));
            at.Value = dtpChartDateTo.Value.ToString();

            string ids = string.Empty;
            if (flpMapFactors.Controls.Count > 0 && flpMapFactors.Controls[0].GetType() == typeof(CheckBox))
                foreach (CheckBox c in flpMapFactors.Controls)
                {
                    if (c.Checked)
                    {
                        ids += ((ModelFactor)c.Tag).IDFactor + ";";
                    }
                }
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("mapFactors"));
            at.Value = ids;

            ids = string.Empty;
            if (flpChartFactors.Controls.Count > 0 && flpChartFactors.Controls[0].GetType() == typeof(CheckBox))
                foreach (CheckBox c in flpChartFactors.Controls)
                {
                    if (c.Checked)
                    {
                        ids += ((ModelFactor)c.Tag).IDFactor.ToString() + ";";
                    }
                }
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("chartFactors"));
            at.Value = ids;

            ids = cbMapColorScheme.SelectedIndex.ToString();
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("colorScheme"));
            at.Value = ids;

            ids = pnlSelectedColor.BackColor.ToArgb().ToString();
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("selectedColor"));
            at.Value = ids;

            ids = string.Empty;
            if (flpFactorCombination.Controls.Count > 0 && flpFactorCombination.Controls[0].GetType() == typeof(RadioButton))
            {
                foreach (RadioButton rb in flpFactorCombination.Controls)
                {
                    if (rb.Checked)
                        ids = rb.Name;
                }
            }
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("factorCombination"));
            at.Value = ids;

            ids = string.Empty;
            GetCheckedRegions(tvMapRegions.Nodes, ref ids);
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("region1"));
            at.Value = ids;

            ids = string.Empty;
            GetCheckedRegions(tvChartRegions.Nodes, ref ids);
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("region2"));
            at.Value = ids;

            ids = string.Empty;
            ids = GetCheckedNodeToTable(tvSelectRegionTableByRegion.Nodes, ref ids);
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("regionToTable"));
            at.Value = ids;

            ids = string.Empty;
            ids = GetCheckedNodeToTable(tvSelectFactorTableByFactor.Nodes, ref ids);
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("factorToTable"));
            at.Value = ids;

            ids = (from CheckBox c in flpMarkerType.Controls where c.Checked select c).Aggregate(string.Empty, (current, c) => current + (((MarkerType)c.Tag).IDMarkerType + ";"));
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("markerType"));
            at.Value = ids;

            ids = (tsMapOptions.Items).Cast<ToolStripButton>().Where(ts => ts.Checked).Aggregate(string.Empty, (current, ts) => current + (ts.Name + ";"));
            /*//!foreach (Symbol s in mapControl1.Symbols)
            {
                if (s.Layer != "Symbol" && s.Layer != "CumulativeFactor")
                    doc.DocumentElement.AppendChild(doc.CreateElement("MarkerId").CloneNode(false)).Attributes.Append(doc.CreateAttribute("ID")).Value = ((IMarker)s.Tag).ID.ToString();
            }*/
            at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("toolStripItems"));
            at.Value = ids;

            r.Parameters = doc.OuterXml;
            ERMTSession.Instance.CurrentReport = r;
        }

        public override void ShowTitle()
        {
            if (ERMTSession.Instance.CurrentModel == null ||
                (ERMTSession.Instance.CurrentModel != null && ERMTSession.Instance.CurrentModel.IDModel == 0))
            {
                ViewManager.ShowTitle(string.Empty);
                return;
            }

            ViewManager.ShowTitle(ERMTSession.Instance.CurrentModel.Name);
        }

        private void LoadTypeChart()
        {
            btnFastLine.Tag = SeriesChartType.FastLine;
            btnLine.Tag = SeriesChartType.Line;
            btnColumn.Tag = SeriesChartType.Column;
            btnSpline.Tag = SeriesChartType.Spline;
            btnSplineArea.Tag = SeriesChartType.SplineArea;
        }

        private void LoadToolStripItems(string p)
        {
            if (string.IsNullOrEmpty(p)) return;
            foreach (ToolStripButton item in tsMapOptions.Items)
            {
                item.Checked = p.Contains(item.Name);
            }
        }

        private void LoadRadioButtonCollections(ControlCollection controls, XmlNode node, string attributeName)
        {
            foreach (object r in controls)
            {
                if (r.GetType() == typeof(RadioButton))
                {
                    if (((RadioButton)r).Name == node.Attributes[attributeName].Value)
                    {
                        ((RadioButton)r).Checked = true;
                    }
                }
            }
        }

        private void MarkerTypesToLoad(ControlCollection controls, XmlNode node, string attributeName)
        {
            //flag para que no salte el evento
            _methodRunning = true;
            foreach (CheckBox c in controls)
            {
                c.Checked = (node.Attributes[attributeName].Value.Contains(((MarkerType)c.Tag).IDMarkerType.ToString() + ";") ? true : false);
            }
            _methodRunning = false;
        }

        private string GetCheckedNodeToTable(TreeNodeCollection nodes, ref string id)
        {
            foreach (TreeNode n in nodes)
            {
                if (!n.Checked)
                    GetCheckedNodeToTable(n.Nodes, ref id);
                else
                {
                    id = ((Region)n.Tag).IDRegion.ToString();
                    break;
                }
            }
            return id;
        }

        private void GetCheckedRegions(TreeNodeCollection nodes, ref string ids)
        {
            foreach (TreeNode n in nodes)
            {
                if (n.Checked)
                    ids += ((Region)n.Tag).IDRegion.ToString() + ";";
                GetCheckedRegions(n.Nodes, ref ids);
            }
        }

        private void GetAllChildRegionNodes(TreeNode parentNode, ref List<TreeNode> nodes)
        {
            foreach (TreeNode n in parentNode.Nodes)
            {
                nodes.Add(n);
                GetAllChildRegionNodes(n, ref nodes);
            }
        }

        private void MarkControlsToLoad(ControlCollection controls, XmlNode node, string attributeName)
        {
            foreach (CheckBox c in controls)
            {
                c.Checked = (node.Attributes[attributeName].Value.Contains(((ModelFactor)c.Tag).IDFactor + ";") ? true : false);
            }
        }

        private void BuildTable(TreeNodeCollection nodes, XmlNode node, string attributeName)
        {
            foreach (TreeNode n in nodes)
            {
                if (node.Attributes[attributeName].Value != string.Empty)
                    n.Checked = (node.Attributes[attributeName].Value.Contains(((Region)n.Tag).IDRegion.ToString() + ";") ? true : false);
            }
        }

        private void LoadRegionsAndFactorsToTable(Model model)
        {
            tvSelectRegionTableByRegion.Nodes.Clear();
            tvSelectRegionsTableByFactor.Nodes.Clear();

            Region modelRegion = RegionHelper.Get(model.IDRegion);

            //agrego el mundo
            List<Region> regions = new List<Region> { RegionHelper.GetWorld() };

            if (modelRegion.IDParent.HasValue && modelRegion.IDParent != 1)
            {
                //si el padre de la region del modelo NO es el mundo, agrego el padre.
                regions.Add(RegionHelper.Get(modelRegion.IDParent.Value));
            }

            //agrego la región del modelo.
            regions.Add(modelRegion);

            //agrego los hijos
            regions.AddRange(RegionHelper.GetAllChilds(modelRegion.IDRegion));

            Region current = (from a in regions where a.IDRegion == model.IDRegion select a).FirstOrDefault();
            TreeNode nodeRegionByRegion = new TreeNode { Text = current.RegionName, Name = current.IDRegion.ToString(), Tag = current };
            TreeNode nodeRegionByFactor = new TreeNode { Text = current.RegionName, Name = current.IDRegion.ToString(), Tag = current };
            tvSelectRegionTableByRegion.Nodes.Add(nodeRegionByRegion);
            tvSelectRegionsTableByFactor.Nodes.Add(nodeRegionByFactor);
            AddChildRegions(tvSelectRegionTableByRegion.Nodes[0], regions);
            AddChildRegions(tvSelectRegionsTableByFactor.Nodes[0], regions);
            tvSelectRegionTableByRegion.ExpandAll();
            tvSelectRegionsTableByFactor.ExpandAll();
            tvSelectFactorTableByFactor.Nodes.Clear();

            List<ModelFactor> modelFactors = ModelFactorHelper.GetByModel(model);
            foreach (ModelFactor modelFactor in modelFactors)
            {
                Factor factor = FactorHelper.Get(modelFactor.IDFactor);
                TreeNode factorNode = new TreeNode() { Text = factor.Name + " ", Name = factor.IdFactor.ToString(), Tag = modelFactor };
                tvSelectFactorTableByFactor.Nodes.Add(factorNode);
            }

            dgvTableByFactor.Rows.Clear();
            dgvTableByFactor.Columns.Clear();

            dgvTableByRegion.Rows.Clear();
            dgvTableByRegion.Columns.Clear();
        }



        private void LoadReports(Model model)
        {
            int index1 = -1;
            int index2 = -1;
            if (lbSavedReports.SelectedItem != null)
                index1 = lbSavedReports.SelectedIndex;
            if (lbSavedReportsCharting.SelectedItem != null)
                index2 = lbSavedReportsCharting.SelectedIndex;
            lbSavedReports.DataSource = null;
            lbSavedReportsCharting.DataSource = null;
            btnLoadMapSettings.Enabled = true;
            btnLoadChartSettings.Enabled = true;
            btnDeleteMapSettings.Enabled = true;
            btnDeleteChartSettings.Enabled = true;
            lbSavedReports.DisplayMember = "Name";
            lbSavedReports.ValueMember = "IDReport";
            lbSavedReportsCharting.DisplayMember = "Name";
            lbSavedReportsCharting.ValueMember = "IDReport";
            List<Report> reports = ReportHelper.GetByModel(model.IDModel);
            List<Report> reportsSavedReports = new List<Report>();
            List<Report> reportsSavedReportsCharting = new List<Report>();
            foreach (Report report in reports)
            {
                if (report.Type == 1)
                    reportsSavedReports.Add(report);
                else
                    reportsSavedReportsCharting.Add(report);
            }
            lbSavedReports.DataSource = reportsSavedReports;
            if (index1 != -1 && lbSavedReports.Items.Count > 0)
                lbSavedReports.SelectedIndex = index1;
            lbSavedReportsCharting.DataSource = reportsSavedReportsCharting;
            if (index2 != -1 && lbSavedReportsCharting.Items.Count > 0)
                try
                {
                    lbSavedReportsCharting.SelectedIndex = index2;
                }
                catch
                {
                    lbSavedReportsCharting.SelectedIndex = index2 - 1;
                }
            if (lbSavedReports.SelectedItem == null)
            {
                btnLoadMapSettings.Enabled = false;
                btnDeleteMapSettings.Enabled = false;
            }
            if (lbSavedReportsCharting.SelectedItem == null)
            {
                btnLoadChartSettings.Enabled = false;
                btnDeleteChartSettings.Enabled = false;
            }
        }

        private void LoadChartingFactors(Model model, TreeNode treeNode)
        {
            bool visible = true;
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Checked)
                {
                    List<ModelFactorData> mfd = ModelFactorDataHelper.GetModelFactorWithDataAvailable(model.IDModel, ((Region)node.Tag).IDRegion);
                    if (mfd.Count > 0)
                    {
                        ModelFactor modelFactor = ModelFactorHelper.Get(mfd[0].IDModelFactor);
                        CheckBox cbFactor = new CheckBox { Text = FactorHelper.Get(modelFactor.IDFactor).Name, AutoSize = false, Height = 40, Width = 260, MaximumSize = new Size(260, 40), Tag = modelFactor };
                        cbFactor.CheckedChanged += chkChartFactor_CheckedChanged;
                        if (flpChartFactors.Controls.Cast<CheckBox>().Any(factor => ((ModelFactor)factor.Tag).IDModelFactor == modelFactor.IDModelFactor))
                        {
                            visible = false;
                        }
                        cbFactor.Enabled = visible;
                        visible = true;
                    }
                }
                LoadChartingFactors(model, node);
            }

            if (flpMapFactors.Controls.Count == 0)
            {
                flpChartFactors.Controls.Add(new Label { Text = ResourceHelper.GetResourceText("NoFactorsInModel"), AutoSize = true, Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold) });
            }
        }

        private void chkChartFactor_CheckedChanged(object sender, EventArgs e)
        {
            GenerateChart();
        }

        private void AddChildRegions(TreeNode treeNode, List<Region> regions)
        {
            foreach (Region r in regions)
            {
                if (r.IDParent.ToString() == treeNode.Name)
                {
                    TreeNode t = new TreeNode() { Text = r.RegionName, Name = r.IDRegion.ToString(), Tag = r };
                    treeNode.Nodes.Add(t);
                    AddChildRegions(t, regions);
                }
            }
        }

        private static void AddRegionsToGenerate(TreeNodeCollection nodes, ref string nodeIds, ref List<Region> regions)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    nodeIds += ((Region)node.Tag).IDRegion + ",";
                    regions.Add((Region)node.Tag);
                }
                AddRegionsToGenerate(node.Nodes, ref nodeIds, ref regions);
            }
        }

        private ChartArea GenerateChartArea(string name)
        {
            ChartArea chartAreaAux = new ChartArea(name);
            chartAreaAux.AxisX.LineWidth = 2;
            chartAreaAux.AxisX.Enabled = (tsmiChartShowGrid.Checked ? AxisEnabled.True : AxisEnabled.False);
            chartAreaAux.AxisX.MajorGrid.LineColor = Color.Gray;
            chartAreaAux.AxisX.MajorGrid.Enabled = tsmiChartShowGrid.Checked;
            chartAreaAux.AxisX.MinorGrid.Enabled = tsmiChartShowGrid.Checked;
            chartAreaAux.AxisX.MinorGrid.LineColor = Color.Gainsboro;

            chartAreaAux.AxisY.LineWidth = 2;
            chartAreaAux.AxisY.Enabled = (tsmiChartShowGrid.Checked ? AxisEnabled.True : AxisEnabled.False);
            chartAreaAux.AxisY.MajorGrid.Enabled = tsmiChartShowGrid.Checked;
            chartAreaAux.AxisY.MinorGrid.Enabled = tsmiChartShowGrid.Checked;
            chartAreaAux.AxisY.MajorGrid.LineColor = Color.Gray;
            chartAreaAux.AxisY.MinorGrid.LineColor = Color.Gainsboro;
            chartAreaAux.AxisY.MinorTickMark.LineColor = Color.Gainsboro;
            chartAreaAux.BackColor = Color.FromArgb(224, 224, 224);
            chartAreaAux.BackHatchStyle = ChartHatchStyle.DarkDownwardDiagonal;
            chartAreaAux.BackSecondaryColor = Color.White;
            return chartAreaAux;
        }

        private Legend GenerateLegend()
        {
            return new Legend
            {
                Docking = Docking.Left,
                TextWrapThreshold = 500,
                LegendStyle = LegendStyle.Table,
                IsDockedInsideChartArea = true,
                Alignment = StringAlignment.Near,
                TitleFont = new Font("Tahoma", 10, FontStyle.Bold),
                Font = new Font("Tahoma", 7, FontStyle.Regular),
                DockedToChartArea = "Graphic",
                ItemColumnSpacing = 1
            };
        }

        private static Series GenerateSeries(string name, string chartAreaName)
        {
            Series series1 = new Series(name)
                                 {
                                     BorderWidth = 2,
                                     ChartArea = chartAreaName,
                                     ChartType = SeriesChartType.Line,
                                     LegendText = name
                                 };
            return series1;
        }

        private void GenerateChartEachFactorForEachRegion(List<ModelFactor> modelFactors, List<Region> regions, string nodeIds, ref Chart floatChart, SeriesChartType chartType, Chart chart)
        {
            int j = 0;
            double max = 0;
            decimal maxCum = 0;
            decimal minCum = 100000;
            int heightLegends = 20;

            foreach (ModelFactor modelFactor in modelFactors)
            {
                string nameFactor = FactorHelper.Get(modelFactor.IDFactor).Name;
                List<ModelFactorData> modelFactorDatas = ModelFactorDataHelper.GetBetweenDatesModelFactorRegion(
                    dtpChartDateFrom.Value.Subtract(dtpChartDateFrom.Value.TimeOfDay), dtpChartDateTo.Value.AddDays(1).Subtract(dtpChartDateTo.Value.TimeOfDay.Add(new TimeSpan(0, 0, 1))), modelFactor.IDModelFactor, nodeIds);
                foreach (Region region in regions)
                {
                    List<ModelFactorData> regionData = (from d in modelFactorDatas where d.IDRegion == region.IDRegion orderby d.Date ascending select d).ToList();
                    if (regionData.Count > 0)
                    {

                        if (chart.Series.IsUniqueName(region.RegionName + " (" + nameFactor + ")") && chart.Series.IsUniqueName(region.RegionName + " (" + nameFactor + "-Cumulative)"))
                        {
                            if (!FactorHelper.Get(modelFactor.IDFactor).CumulativeFactor)
                            {
                                chart.Series.Add(GenerateSeries(region.RegionName + " (" + nameFactor + ")", chart.ChartAreas[0].Name));
                                floatChart.Series.Add(GenerateSeries(region.RegionName + " (" + nameFactor + ")", floatChart.ChartAreas[0].Name));
                            }
                            else
                            {
                                chart.Series.Add(GenerateSeries(region.RegionName + " (" + nameFactor + "-Cumulative)", chart.ChartAreas[0].Name));
                                floatChart.Series.Add(GenerateSeries(region.RegionName + " (" + nameFactor + "-Cumulative)", floatChart.ChartAreas[0].Name));
                            }

                            floatChart.Series[j].Color = chart.Series[j].Color;
                            chart.Series[j].ChartArea = "Graphic";
                            floatChart.Series[j].ChartArea = "Graphic";

                            chart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy";
                            chart.Series[j].XValueType = ChartValueType.DateTime;
                            floatChart.Series[j].XValueType = ChartValueType.DateTime;

                            chart.Series[j].YValueType = ChartValueType.Double;
                            floatChart.Series[j].YValueType = ChartValueType.Double;

                            chart.Series[j].ChartType = chartType;
                            floatChart.Series[j].ChartType = chartType;

                            if (!FactorHelper.Get(modelFactor.IDFactor).CumulativeFactor)
                            {
                                chart.Series[j].LabelToolTip = region.RegionName + " (" + nameFactor + ")";
                                floatChart.Series[j].LabelToolTip = region.RegionName + " (" + nameFactor + ")";
                            }
                            else
                            {
                                chart.Series[j].LabelToolTip = region.RegionName + " (" + nameFactor + "-Cumulative)";
                                floatChart.Series[j].LabelToolTip = region.RegionName + " (" + nameFactor + "-Cumulative)";
                            }

                            foreach (ModelFactorData data in regionData)
                            {
                                if (data.Data > maxCum)
                                {
                                    maxCum = data.Data;
                                }
                                if (data.Data < minCum)
                                {
                                    minCum = data.Data;
                                }
                                decimal roundData = data.Data;
                                chart.Series[j].Points.AddXY(data.Date, roundData);
                                chart.Series[j].Points[chart.Series[j].Points.Count - 1].IsValueShownAsLabel = true;
                                chart.Series[j].Points[chart.Series[j].Points.Count - 1].MarkerStyle = MarkerStyle.Circle;
                            }
                            Factor f = FactorHelper.Get(modelFactor.IDFactor);
                            if (!f.CumulativeFactor)
                            {
                                //if the factor is NOT cumulative, then use the MAXSCALE from ModelFactor
                                if (modelFactor.ScaleMax > max)
                                {
                                    max = modelFactor.ScaleMax;
                                }
                            }
                            else
                            {
                                if ((double)maxCum > max)
                                {
                                    max = (double)maxCum;
                                }
                            }

                            j++;
                        }
                    }

                    heightLegends += 15;
                }
            }

            chart.ChartAreas[0].AxisY.Maximum = max + 0.5;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            _chartLegend.Height = Math.Max(heightLegends, 40);
        }

        private void GenerateChartResultantValueOfRegions(List<ModelFactor> modelFactors, List<Region> regions, ref Chart floatChart, SeriesChartType chartType, string nodeIds, Chart chart)
        {
            int max = 0;
            int heightLegends = 20;
            int widthLegends = 0;
            decimal maxAxis = 0;
            List<ModelFactorData> mfdList = new List<ModelFactorData>();
            foreach (ModelFactor imf in modelFactors)
            {
                if (imf.ScaleMax > max)
                {
                    max = imf.ScaleMax;
                }

                mfdList.AddRange(ModelFactorDataHelper.GetBetweenDatesModelFactorRegion(dtpChartDateFrom.Value.Subtract(dtpChartDateFrom.Value.TimeOfDay), dtpChartDateTo.Value.AddDays(1).Subtract(dtpChartDateTo.Value.TimeOfDay.Add(new TimeSpan(0, 0, 1))), imf.IDModelFactor, nodeIds));
            }

            mfdList = mfdList.OrderBy(mfd => mfd.Date).ToList();

            List<DateTime> dates = new List<DateTime>();
            foreach (ModelFactorData mfd in mfdList)
            {
                if (dates.Contains(mfd.Date)) continue;
                dates.Add(mfd.Date);
            }
            int j = 0;

            List<int> seriesToDelete = new List<int>();
            foreach (Region r in regions)
            {
                bool hasCumulativeData = false;
                if (!chart.Series.IsUniqueName(r.RegionName)) continue;
                DateTime minDate = (from a in mfdList orderby a.Date ascending select a.Date).FirstOrDefault();
                DateTime maxDate = (from a in mfdList orderby a.Date descending select a.Date).FirstOrDefault();

                //Hay datos para la region entre las fechas? Si no hay, no la agrego.
                if (!(from a in mfdList where a.Date >= minDate && a.Date <= maxDate && a.IDRegion == r.IDRegion select a.Data).Any())
                    continue;

                chart.Series.Add(GenerateSeries(r.RegionName + " - Cumulative Factors", chart.ChartAreas[0].Name));
                floatChart.Series.Add(GenerateSeries(r.RegionName + " - Cumulative Factors", floatChart.ChartAreas[0].Name));

                floatChart.Series[j].Color = chart.Series[j].Color;
                chart.Series[j].ChartArea = "Graphic";
                floatChart.Series[j].ChartArea = "Graphic";

                chart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy";
                chart.Series[j].XValueType = ChartValueType.DateTime;
                floatChart.Series[j].XValueType = ChartValueType.DateTime;

                chart.Series[j].YValueType = ChartValueType.Double;
                floatChart.Series[j].YValueType = ChartValueType.Double;

                chart.Series[j].ChartType = chartType;
                floatChart.Series[j].ChartType = chartType;

                chart.Series[j].LabelToolTip = r.RegionName + " - Cumulative Factors";
                floatChart.Series[j].LabelToolTip = r.RegionName + " - Cumulative Factors";


                chart.Series.Add(GenerateSeries(r.RegionName, chart.ChartAreas[0].Name));
                floatChart.Series.Add(GenerateSeries(r.RegionName, floatChart.ChartAreas[0].Name));

                floatChart.Series[j + 1].Color = chart.Series[j + 1].Color;
                chart.Series[j + 1].ChartArea = "Graphic";
                floatChart.Series[j + 1].ChartArea = "Graphic";

                chart.Series[j + 1].XValueType = ChartValueType.DateTime;
                floatChart.Series[j + 1].XValueType = ChartValueType.DateTime;

                chart.Series[j + 1].YValueType = ChartValueType.Double;
                floatChart.Series[j + 1].YValueType = ChartValueType.Double;

                chart.Series[j + 1].ChartType = chartType;
                floatChart.Series[j + 1].ChartType = chartType;

                chart.Series[j + 1].LabelToolTip = r.RegionName;
                floatChart.Series[j + 1].LabelToolTip = r.RegionName;

                foreach (DateTime date in dates)
                {
                    decimal prom = 0, acumWeight = 0;
                    int cumulativeData = 0;
                    bool valueFound = false;
                    if ((from a in mfdList where a.Date == date && a.IDRegion == r.IDRegion select a.Data).Any())
                    {
                        foreach (var v in (from a in mfdList where a.Date == date && a.IDRegion == r.IDRegion orderby a.Date ascending select a))
                        {
                            valueFound = true;
                            ModelFactor mf = ModelFactorHelper.Get(v.IDModelFactor);
                            Factor f = FactorHelper.Get(mf.IDFactor);
                            if (!f.CumulativeFactor)
                            {
                                prom += v.Data * (from a in modelFactors where a.IDModelFactor == v.IDModelFactor select a.Weight).FirstOrDefault() / 100;
                                acumWeight += (decimal)(from a in modelFactors where a.IDModelFactor == v.IDModelFactor select a.Weight).FirstOrDefault() / 100;
                                if (mf.ScaleMax > maxAxis)
                                {
                                    maxAxis = mf.ScaleMax;
                                }
                            }
                            else
                            {
                                cumulativeData += (int)v.Data;
                                if (cumulativeData > maxAxis)
                                    maxAxis = cumulativeData;
                            }
                        }
                        if (acumWeight != 0)
                        {
                            prom = Math.Round(prom / acumWeight, 2);
                            if (prom > maxAxis)
                                maxAxis = prom;
                        }

                    }
                    if (cumulativeData > 0)
                    {
                        hasCumulativeData = true;
                    }

                    if (valueFound)
                    {
                        chart.Series[j + 1].Points.AddXY(date, prom);
                        chart.Series[j + 1].Points[chart.Series[j + 1].Points.Count - 1].IsValueShownAsLabel = true;
                        chart.Series[j + 1].Points[chart.Series[j + 1].Points.Count - 1].MarkerStyle = MarkerStyle.Circle;

                        chart.Series[j].Points.AddXY(date, cumulativeData);
                        chart.Series[j].Points[chart.Series[j].Points.Count - 1].IsValueShownAsLabel = true;
                        chart.Series[j].Points[chart.Series[j].Points.Count - 1].MarkerStyle = MarkerStyle.Circle;
                    }


                }

                double auxWidth = (r.RegionName + " - Cumulative Factors").Length * 5.5 + 25;

                if (!hasCumulativeData)
                {
                    seriesToDelete.Add(j);
                    auxWidth = r.RegionName.Length * 5.5 + 25;
                }

                if (widthLegends < auxWidth)
                {
                    widthLegends = (int)auxWidth;
                }

                heightLegends += 15;

                j += 2;
            }

            foreach (int s in seriesToDelete.OrderByDescending(i => i))
            {
                chart.Series.Remove(chart.Series[s]);
                floatChart.Series.Remove(floatChart.Series[s]);
                //heightLegends -= 15;
            }

            j = 0;
            foreach (Series s in chart.Series)
            {
                floatChart.Series[j].Color = s.Color;
                j++;
            }

            chart.ChartAreas[0].AxisY.Maximum = double.Parse(maxAxis.ToString()) + 0.5;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            _chartLegend.Height = Math.Max(heightLegends, 40);
        }

        private void GenerateChartResultantValueOfFactors(List<ModelFactor> modelFactors, ref Chart floatChart, SeriesChartType chartType, string nodeIds, Chart chart)
        {
            int max = 0;
            decimal maxAxis = 0;
            int heightLegends = 20;
            int widthLegends = 0;
            int j = 0;
            List<ModelFactorData> mfdList = new List<ModelFactorData>();
            foreach (ModelFactor imf in modelFactors)
            {
                if (imf.ScaleMax > max)
                {
                    max = imf.ScaleMax;
                }
                mfdList.AddRange(ModelFactorDataHelper.GetBetweenDatesModelFactorRegion(dtpChartDateFrom.Value.Subtract(dtpChartDateFrom.Value.TimeOfDay), dtpChartDateTo.Value.AddDays(1).Subtract(dtpChartDateTo.Value.TimeOfDay.Add(new TimeSpan(0, 0, 1))), imf.IDModelFactor, nodeIds));
            }
            List<DateTime> dates = new List<DateTime>();
            foreach (ModelFactorData mfd in mfdList)
            {
                if (dates.Contains(mfd.Date)) continue;
                dates.Add(mfd.Date);
            }

            foreach (ModelFactor mf in modelFactors)
            {
                Factor f = FactorHelper.Get(mf.IDFactor);
                if (!(from a in mfdList where a.IDModelFactor == mf.IDModelFactor select a.Data).Any())
                    continue;

                if (!f.CumulativeFactor)
                {
                    chart.Series.Add(GenerateSeries(f.Name, chart.ChartAreas[0].Name));
                    floatChart.Series.Add(GenerateSeries(f.Name, floatChart.ChartAreas[0].Name));
                    chart.Series[j].LabelToolTip = f.Name;
                    floatChart.Series[j].LabelToolTip = f.Name;
                }
                else
                {
                    chart.Series.Add(GenerateSeries(f.Name + " - Cumulative", chart.ChartAreas[0].Name));
                    floatChart.Series.Add(GenerateSeries(f.Name + " - Cumulative", floatChart.ChartAreas[0].Name));
                    chart.Series[j].LabelToolTip = f.Name + " - Cumulative";
                    floatChart.Series[j].LabelToolTip = f.Name + " - Cumulative";
                }

                floatChart.Series[j].Color = chart.Series[j].Color;
                chart.Series[j].ChartArea = "Graphic";
                floatChart.Series[j].ChartArea = "Graphic";

                chart.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy";
                chart.Series[j].XValueType = ChartValueType.DateTime;
                floatChart.Series[j].XValueType = ChartValueType.DateTime;

                chart.Series[j].YValueType = ChartValueType.Double;
                floatChart.Series[j].YValueType = ChartValueType.Double;

                chart.Series[j].ChartType = chartType;
                floatChart.Series[j].ChartType = chartType;

                foreach (var date in (from a in dates orderby a.Date ascending select a))
                {
                    if ((from a in mfdList where a.Date == date && a.IDModelFactor == mf.IDModelFactor select a.Data).Any())
                        if (!f.CumulativeFactor)
                        {
                            decimal prom = Math.Round((from a in mfdList where a.Date == date && a.IDModelFactor == mf.IDModelFactor select a.Data).Average(), 2);
                            chart.Series[j].Points.AddXY(date, prom);
                            chart.Series[j].Points[chart.Series[j].Points.Count - 1].IsValueShownAsLabel = true;
                            chart.Series[j].Points[chart.Series[j].Points.Count - 1].MarkerStyle = MarkerStyle.Circle;
                            if (mf.ScaleMax > maxAxis)
                                maxAxis = mf.ScaleMax;
                        }
                        else
                        {
                            int cumulativeData = (int)(from a in mfdList where a.Date == date && a.IDModelFactor == mf.IDModelFactor select a.Data).Sum();
                            chart.Series[j].Points.AddXY(date, cumulativeData);
                            chart.Series[j].Points[chart.Series[j].Points.Count - 1].IsValueShownAsLabel = true;
                            chart.Series[j].Points[chart.Series[j].Points.Count - 1].MarkerStyle = MarkerStyle.Circle;
                            if (cumulativeData > maxAxis)
                                maxAxis = cumulativeData;
                        }
                }
                j++;
                heightLegends += 15;
                string width = f.Name;
                double aux = width.Length * 5.5 + 25;
                if (widthLegends < aux)
                {
                    widthLegends = (int)aux;
                }
            }
            chart.ChartAreas[0].AxisY.Maximum = double.Parse((maxAxis).ToString()) + 0.5;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            _chartLegend.Height = Math.Max(heightLegends, 40);
            _chartLegend.Width = Math.Max(widthLegends, 200);
        }

        private void GenerateChart()
        {
            GenerateChart(chart1);
        }

        private void GenerateChart(Chart chart)
        {
            List<ModelFactor> lModelFactors = (from CheckBox c in flpChartFactors.Controls where c.Checked select ((ModelFactor)c.Tag)).ToList();
            GenerateChart(_lastChartType, lModelFactors, chart);
        }

        private void GenerateChart(SeriesChartType chartType, List<ModelFactor> modelFactors, Chart chart)
        {
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.ChartAreas.Clear();
            int factorCombination = (from RadioButton rb in flpFactorCombination.Controls where rb.Checked select int.Parse(rb.Tag.ToString())).FirstOrDefault();

            string nodeIds = string.Empty;
            List<Region> regions = new List<Region>();
            AddRegionsToGenerate(tvChartRegions.Nodes, ref nodeIds, ref regions);
            if (regions.Count == 0) return;
            chart.ChartAreas.Add(GenerateChartArea("Graphic"));

            _chartLegend.TopMost = false;
            _chartLegend.Owner = this.ParentForm;
            _chartLegend.Visible = (tcRiskMapping.SelectedTab == tpCharting && tsmiChartShowLegends.Checked);

            //floatchart es igual al otro chart, solo que muestra las legends unicamente
            Chart floatChart = ((Chart)_chartLegend.Controls[0].Controls[0]);
            floatChart.Series.Clear();
            floatChart.Legends.Clear();
            floatChart.ChartAreas.Clear();
            floatChart.ChartAreas.Add(GenerateChartArea("Graphic"));

            nodeIds = nodeIds.Length > 0 ? nodeIds.Substring(0, nodeIds.Length - 1) : nodeIds;

            floatChart.Legends.Add(GenerateLegend());

            switch (factorCombination)
            {
                case 1:
                    {
                        GenerateChartEachFactorForEachRegion(modelFactors, regions, nodeIds, ref floatChart, chartType, chart);
                        break;
                    }
                case 2:
                    {
                        GenerateChartResultantValueOfRegions(modelFactors, regions, ref floatChart, chartType, nodeIds, chart);
                        break;
                    }
                default:
                    {
                        GenerateChartResultantValueOfFactors(modelFactors, ref floatChart, chartType, nodeIds, chart);
                        break;
                    }
            }
        }

        private static void GetCheckedRegionIds(TreeNodeCollection nodes, ref List<int> ids)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    Region region = (Region)node.Tag;
                    ids.Add(region.IDRegion);
                }
                GetCheckedRegionIds(node.Nodes, ref ids);
            }
        }

        private void btnChartType_Click(object sender, EventArgs e)
        {
            List<ModelFactor> modelFactors = (from CheckBox cbFactor in flpChartFactors.Controls where cbFactor.Checked select (ModelFactor)cbFactor.Tag).ToList();
            _lastChartType = ((SeriesChartType)((Button)sender).Tag);
            GenerateChart(_lastChartType, modelFactors, chart1);
        }

        private void lbSavedReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSavedReports.SelectedItem != null)
            {
                btnLoadMapSettings.Enabled = true;
                btnDeleteMapSettings.Enabled = true;
                txtMapSettingsName.Text = ((Report)lbSavedReports.SelectedItem).Name;
            }
            else
            {
                btnLoadMapSettings.Enabled = false;
                btnDeleteMapSettings.Enabled = false;
                txtMapSettingsName.Text = string.Empty;
            }
        }

        private void MarkRegionsToLoad(TreeNodeCollection nodes, XmlNode node, string attributeName)
        {
            foreach (TreeNode n in nodes)
            {
                n.Checked = node.Attributes[attributeName].Value.Contains(((Region)n.Tag).IDRegion + ";");
                MarkRegionsToLoad(n.Nodes, node, attributeName);
            }
        }



        private void CheckRegionOnThisLevel(TreeNode node, bool check)
        {
            TreeNode parentNode = node.Parent;
            if (parentNode == null) return;
            while (parentNode.PrevNode != null)
                parentNode = parentNode.PrevNode;
            _methodRunning = true;
            do
            {
                foreach (TreeNode n in parentNode.Nodes)
                {
                    n.Checked = check;
                    SetRegionVisibility((Region)n.Tag, check);
                }
                parentNode = parentNode.NextNode;
            } while (parentNode != null);

            _methodRunning = false;
        }

        private void CheckNodesInLevel(int nodeLevel, TreeNode node, Boolean check)
        {
            _methodRunning = true;
            foreach (TreeNode t in node.Nodes)
            {
                if (t.Level == nodeLevel)
                {
                    t.Checked = check;
                    SetRegionVisibility((Region)t.Tag, check);
                }

                CheckNodesInLevel(nodeLevel, t, check);
            }
            _methodRunning = false;
        }

        private void CheckChildsDraw(TreeNode node, bool check)
        {
            _methodRunning = true;
            foreach (TreeNode t in node.Nodes)
            {
                t.Checked = check;
                SetRegionVisibility((Region)t.Tag, check);
                CheckChilds(t, check);
            }
            _methodRunning = false;
            //DrawMap();
            RefreshMap();
        }

        private void CheckChilds(TreeNode node, bool check)
        {
            foreach (TreeNode t in node.Nodes)
            {
                t.Checked = check;
                SetRegionVisibility((Region)t.Tag, check);
                CheckChilds(t, check);
            }
        }

        private void tvSelectRegionTableByRegion_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LoadTableByRegion(e.Node);
            }
        }

        private void LoadTableByRegion()
        {
            if (tvSelectRegionTableByRegion.SelectedNode != null)
            {
                LoadTableByRegion(tvSelectRegionTableByRegion.SelectedNode);
            }
        }

        private void LoadTableByRegion(TreeNode e)
        {
            if (e == null)
                return;

            dgvTableByRegion.Rows.Clear();
            dgvTableByRegion.Columns.Clear();
            Model model = ERMTSession.Instance.CurrentModel;
            List<ModelFactor> modelFactors = ModelFactorHelper.GetByModel(model);
            List<Factor> factors = new List<Factor>();
            dgvTableByRegion.Columns.Add(new DataGridViewColumn { Name = "colDate", HeaderText = ResourceHelper.GetResourceText("Date"), DataPropertyName = "Date", CellTemplate = new DataGridViewTextBoxCell(), ValueType = Type.GetType("DateTime"), Visible = true });


            DataGridViewCellStyle cellStyleDefault = new DataGridViewCellStyle { Font = new Font("Arial", 10, FontStyle.Regular) };

            DataGridViewCellStyle cellStyleBold = new DataGridViewCellStyle { Font = new Font("Arial", 10, FontStyle.Bold) };

            foreach (ModelFactor modelFactor in modelFactors)
            {
                dgvTableByRegion.Columns.Add(new DataGridViewColumn() { HeaderText = FactorHelper.Get(modelFactor.IDFactor).Name, DataPropertyName = "Data", CellTemplate = new DataGridViewTextBoxCell(), ValueType = Type.GetType("string"), Tag = modelFactor, Visible = true, FillWeight = 1 });
            }
            List<ModelFactorData> modelFactorDatas = ModelFactorDataHelper.GetModelFactorWithDataAvailable(model.IDModel, int.Parse(e.Name));
            List<DateTime> allDates = new List<DateTime>();
            foreach (ModelFactorData modelFactorData in modelFactorDatas)
            {
                if (allDates.Contains(modelFactorData.Date)) continue;
                allDates.Add(modelFactorData.Date);
            }
            foreach (DateTime date in allDates)
            {
                List<string> dgvRow = new List<string> { date.ToString("d", GeneralHelper.GetDateFormat()) };
                for (int col = 1; col <= modelFactors.Count; col++)
                {
                    ModelFactorData modelFactorData = (from a in modelFactorDatas where a.Date == date && a.IDModelFactor == ((ModelFactor)dgvTableByRegion.Columns[col].Tag).IDModelFactor && a.IDRegion == ((Region)e.Tag).IDRegion select a).FirstOrDefault();
                    dgvRow.Add(modelFactorData != null ? modelFactorData.Data.ToString() : "N/D");
                }
                dgvTableByRegion.Rows.Add(dgvRow.ToArray());
            }
            int row = 0;

            dgvTableByRegion.DefaultCellStyle = cellStyleDefault;

            foreach (DateTime date in allDates)
            {
                int col = 1;
                foreach (ModelFactor modelFactor in modelFactors)
                {

                    ModelFactorData modelFactorData = (from a in modelFactorDatas where a.Date == date && a.IDModelFactor == modelFactor.IDModelFactor && a.IDRegion == ((Region)e.Tag).IDRegion select a).FirstOrDefault();
                    if (modelFactorData != null)
                    {
                        dgvTableByRegion.Rows[row].Cells[col].Style = cellStyleBold;
                        dgvTableByRegion.Rows[row].Cells[col].Tag = modelFactorData;
                    }

                    col++;
                }
                row++;
            }
            dgvTableByRegion.AllowUserToAddRows = false;
            dgvTableByRegion.AllowUserToDeleteRows = false;
        }

        private void ShowRegionsInTableByFactors(List<int> regionIds)
        {
            if (regionIds.Count == 0)
            {
                foreach (DataGridViewColumn c in dgvTableByFactor.Columns)
                    c.Visible = false;
            }
            else
            {
                foreach (DataGridViewColumn c in dgvTableByFactor.Columns)
                {
                    if (((Region)c.Tag) == null) continue; ;
                    c.Visible = regionIds.Contains(((Region)c.Tag).IDRegion);
                }
            }
            dgvTableByFactor.Columns[0].Visible = true;
        }

        private void tvSelectRegionsTableByFactor_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e != null)
            {
                ((TreeView)sender).SelectedNode = e.Node;
                _lastTreeNode = e.Node;

                if (e.Button == MouseButtons.Right)
                {
                    return;
                }

                LoadingForm.ShowLoading();
                tvSelectFactorTableByFactor.Enabled = false;
                tvSelectRegionsTableByFactor.Enabled = false;
                dgvTableByFactor.Visible = false;
                if (tvSelectFactorTableByFactor.SelectedNode == null)
                {
                    tvSelectFactorTableByFactor.Enabled = true;
                    tvSelectRegionsTableByFactor.Enabled = true;
                    dgvTableByFactor.Visible = true;
                    LoadingForm.Fadeout();
                    return;
                }
                foreach (DataGridViewColumn c in dgvTableByFactor.Columns)
                {
                    if (((Region)c.Tag) == null) continue;
                    if (((Region)c.Tag).IDRegion != ((Region)e.Node.Tag).IDRegion)
                        continue;
                    c.Visible = e.Node.Checked;
                }
                if (dgvTableByFactor.Columns.Count > 0)
                {
                    dgvTableByFactor.Columns[0].Visible = true;
                }
                LoadingForm.Fadeout();
                tvSelectFactorTableByFactor.Enabled = true;
                tvSelectRegionsTableByFactor.Enabled = true;
                dgvTableByFactor.Visible = true;
                if (tvSelectFactorTableByFactor.SelectedNode != null)
                {
                    LoadTableByFactor(tvSelectFactorTableByFactor.SelectedNode);
                }

            }
            else
            {
                foreach (DataGridViewColumn c in dgvTableByFactor.Columns)
                {
                    c.Visible = false;
                }

            }
        }

        private void tvSelectFactorTableByFactor_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LoadTableByFactor(e.Node);
            }
        }

        private void LoadTableByFactor()
        {
            if (tvSelectFactorTableByFactor.SelectedNode != null)
            {
                LoadTableByFactor(tvSelectFactorTableByFactor.SelectedNode);
            }
        }

        private void LoadTableByFactor(TreeNode e)
        {
            if (e == null) return;
            LoadingForm.ShowLoading();
            tvSelectFactorTableByFactor.Enabled = false;
            tvSelectRegionsTableByFactor.Enabled = false;
            dgvTableByFactor.Visible = false;

            Application.DoEvents();
            List<ModelFactorData> modelFactorDatas = new List<ModelFactorData>();
            dgvTableByFactor.Rows.Clear();
            dgvTableByFactor.Columns.Clear();
            Model model = ModelHelper.GetModel(ERMTSession.Instance.CurrentModel.IDModel);
            List<Region> regions = new List<Region> { RegionHelper.Get(model.IDRegion) };

            List<Region> regionsToAdd = RegionHelper.GetAllChilds(model.IDRegion);

            DataGridViewCellStyle cellStyleDefault = new DataGridViewCellStyle { Font = new Font("Arial", 10, FontStyle.Regular) };

            DataGridViewCellStyle cellStyleBold = new DataGridViewCellStyle { Font = new Font("Arial", 10, FontStyle.Bold) };

            regions.AddRange(regionsToAdd);

            ModelFactor modelFactor = ModelFactorHelper.GetByModelAndFactorId(model.IDModel, int.Parse(e.Name)).FirstOrDefault();
            List<DateTime> allDates = new List<DateTime>();
            if (modelFactor != null)
            {
                modelFactorDatas = ModelFactorDataHelper.GetByModelFactor(modelFactor);
                if (modelFactorDatas.Count > 0)
                {
                    List<int> checkedRegionIDList = new List<int>();
                    GetCheckedRegionIds(tvSelectRegionsTableByFactor.Nodes, ref checkedRegionIDList);
                    foreach (ModelFactorData mfd in modelFactorDatas)
                    {
                        if (!allDates.Contains(mfd.Date) && checkedRegionIDList.Contains(mfd.IDRegion))
                        {
                            allDates.Add(mfd.Date);
                        }
                    }
                }
            }
            dgvTableByFactor.Columns.Add(new DataGridViewColumn() { Name = "colDate", FillWeight = 1, HeaderText = ResourceHelper.GetResourceText("Date"), DataPropertyName = "Date", CellTemplate = new DataGridViewTextBoxCell(), ValueType = Type.GetType("DateTime"), Visible = true });
            foreach (Region region in regions)
            {
                dgvTableByFactor.Columns.Add(new DataGridViewColumn() { HeaderText = region.RegionName, DataPropertyName = "Data", CellTemplate = new DataGridViewTextBoxCell(), ValueType = Type.GetType("string"), Tag = region, FillWeight = 1 });
                Application.DoEvents();
            }

            dgvTableByFactor.DefaultCellStyle = cellStyleDefault;

            foreach (DateTime date in allDates)
            {
                List<string> dgvRow = new List<string> { date.ToString("d", GeneralHelper.GetDateFormat()) };
                foreach (Region region in regions)
                {
                    ModelFactorData modelFactorData = (from a in modelFactorDatas where a.Date == date && a.IDModelFactor == modelFactor.IDModelFactor && a.IDRegion == region.IDRegion select a).FirstOrDefault();

                    dgvRow.Add(modelFactorData != null ? modelFactorData.Data.ToString() : "N/D");
                    Application.DoEvents();
                }
                dgvTableByFactor.Rows.Add(dgvRow.ToArray());
            }
            int row = 0;
            foreach (DateTime date in allDates)
            {
                int col = 1;
                foreach (Region region in regions)
                {
                    ModelFactorData modelFactorData = (from a in modelFactorDatas where a.Date == date && a.IDModelFactor == modelFactor.IDModelFactor && a.IDRegion == region.IDRegion select a).FirstOrDefault();
                    if (modelFactorData != null)
                    {
                        dgvTableByFactor.Rows[row].Cells[col].Style = cellStyleBold;
                        dgvTableByFactor.Rows[row].Cells[col].Tag = modelFactorData;
                    }

                    col++;
                }
                row++;
            }

            dgvTableByFactor.AllowUserToAddRows = false;
            dgvTableByFactor.AllowUserToDeleteRows = false;
            List<int> regionIds = new List<int>();
            GetCheckedRegionIds(tvSelectRegionsTableByFactor.Nodes, ref regionIds);
            ShowRegionsInTableByFactors(regionIds);
            tvSelectFactorTableByFactor.Enabled = true;
            tvSelectRegionsTableByFactor.Enabled = true;
            dgvTableByFactor.Visible = true;
            Application.DoEvents();
            LoadingForm.Fadeout();
        }

        private void dgvTableByRegion_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_valueChanged) return;
            //Clean errors
            dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
            if (_leave)
            {
                foreach (DataGridViewCell c in dgvTableByRegion.Rows[e.RowIndex].Cells)
                {
                    if (c.ColumnIndex == 0) continue;
                    ModelFactorData modelFactorData = ((ModelFactorData)(dgvTableByRegion.Rows[e.RowIndex].Cells[c.ColumnIndex].Tag));
                    if (modelFactorData == null) continue;
                    try
                    {
                        modelFactorData.Date = DateTime.Parse(dgvTableByRegion.Rows[e.RowIndex].Cells[0].Value.ToString(), GeneralHelper.GetDateFormat());
                        dgvTableByRegion.Rows[e.RowIndex].Cells[c.ColumnIndex].Tag = ModelFactorDataHelper.Save(modelFactorData);
                        _reloadModelData = true;
                    }
                    catch (Exception)
                    {
                        CustomMessageBox.ShowError(ResourceHelper.GetResourceText("InvalidDate"));
                        _valueChanged = true;
                        dgvTableByRegion.Rows[e.RowIndex].Cells[0].Value = modelFactorData.Date.ToString("d", GeneralHelper.GetDateFormat());
                        _valueChanged = false;
                        return;
                    }
                }
            }
            else
            {
                ModelFactorData modelFactorData = ((ModelFactorData)(dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag));
                dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
                if (e.ColumnIndex != 0)
                {
                    try
                    {
                        ModelFactor modelFactor;
                        decimal data = decimal.Parse(dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        Factor factor;
                        if (modelFactorData != null)
                        {
                            modelFactor = ModelFactorHelper.Get(modelFactorData.IDModelFactor);
                            factor = FactorHelper.Get(modelFactor.IDFactor);
                            modelFactorData = ModelFactorDataHelper.Get(modelFactorData);
                        }
                        else
                        {
                            modelFactorData = ModelFactorDataHelper.GetNew();
                            modelFactor = ((ModelFactor)(dgvTableByRegion.Columns[e.ColumnIndex].Tag));
                            modelFactorData.IDModelFactor = modelFactor.IDModelFactor;
                            modelFactorData.IDRegion = int.Parse(tvSelectRegionTableByRegion.SelectedNode.Name);
                            factor = FactorHelper.Get(modelFactor.IDFactor);
                        }
                        if (factor.CumulativeFactor)
                        {
                            modelFactorData.Data = data;
                            modelFactorData.Date = DateTime.Parse(dgvTableByRegion.Rows[e.RowIndex].Cells[0].Value.ToString(), GeneralHelper.GetDateFormat());
                            ModelFactorData m = ModelFactorDataHelper.Save(modelFactorData);
                            _reloadModelData = true;
                            dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = m;
                            _valueChanged = true;
                            dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = m.Data.ToString();
                            _valueChanged = false;
                        }
                        else
                        {
                            if (modelFactor.ScaleMin <= data && modelFactor.ScaleMax >= data)
                            {
                                //El factor no admite decimales
                                if ((modelFactor.Interval - Math.Truncate(modelFactor.Interval)) == 0)
                                {
                                    //El valor tiene decimales
                                    if (data - Math.Truncate(data) != 0)
                                    {
                                        //lo redondeo
                                        data = Math.Round(data, MidpointRounding.AwayFromZero);
                                        dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Input data has been rounded to fit the range of factor.";
                                    }
                                }
                                else //el factor admite decimales
                                {
                                    if ((data % (decimal)modelFactor.Interval) != 0)
                                    {
                                        if (modelFactor.Interval % 1 == 0)
                                            data = ((data % modelFactor.Interval) < (modelFactor.Interval / 2)) ? Math.Truncate(data) : Math.Truncate(data) + modelFactor.Interval;
                                        else
                                            data = ((data % 1) < (modelFactor.Interval)) ? Math.Truncate(data) : Math.Truncate(data) + modelFactor.Interval;
                                        dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Input data has been rounded to fit the range of factor.";
                                    }
                                }

                                modelFactorData.Data = data;
                                modelFactorData.Date = DateTime.Parse(dgvTableByRegion.Rows[e.RowIndex].Cells[0].Value.ToString(), GeneralHelper.GetDateFormat());
                                ModelFactorData m = ModelFactorDataHelper.Save(modelFactorData);
                                _reloadModelData = true;
                                dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = m;
                                _valueChanged = true;
                                dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = m.Data.ToString();
                                _valueChanged = false;
                            }

                            else
                            {
                                dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Input data outside the range of the scale.";
                                _valueChanged = true;
                                if (modelFactorData.IDModelFactorData == 0)
                                {
                                    dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font("Arial", 10, FontStyle.Regular);
                                    dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N/D";
                                }
                                else
                                {
                                    dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font("Arial", 10, FontStyle.Bold);
                                    dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = modelFactorData.Data.ToString();
                                }
                                _valueChanged = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Input data must be decimal.";
                        _valueChanged = true;
                        if (modelFactorData != null)
                        {
                            dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font("Arial", 10, FontStyle.Bold);
                            dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = modelFactorData.Data.ToString();
                        }
                        else
                        {
                            dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font("Arial", 10, FontStyle.Regular);
                            dgvTableByRegion.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N/D";
                        }
                        _valueChanged = false;
                    }
                }
            }
        }

        private void dgvTableByFactor_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_valueChanged) return;
            //Clean errors
            dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
            if (_leave)
            {
                foreach (DataGridViewCell c in dgvTableByFactor.Rows[e.RowIndex].Cells)
                {
                    if (c.ColumnIndex == 0) continue;
                    ModelFactorData modelFactorData = ((ModelFactorData)(dgvTableByFactor.Rows[e.RowIndex].Cells[c.ColumnIndex].Tag));
                    if (modelFactorData == null) //A new cell?
                    {
                        continue;
                        /*modelFactorData = ModelFactorDataHelper.GetNew();
                        modelFactorData.*/
                    };
                    try
                    {
                        modelFactorData.Date = DateTime.Parse(dgvTableByFactor.Rows[e.RowIndex].Cells[0].Value.ToString(), GeneralHelper.GetDateFormat());
                        dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = ModelFactorDataHelper.Save(modelFactorData);
                        _reloadModelData = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ResourceHelper.GetResourceText("InvalidDate"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _valueChanged = true;
                        dgvTableByFactor.Rows[e.RowIndex].Cells[0].Value = modelFactorData.Date.ToString("d", GeneralHelper.GetDateFormat());
                        _valueChanged = false;
                        return;
                    }
                }
            }
            else
            {
                ModelFactorData modelFactorData = ((ModelFactorData)(dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag));
                dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
                if (e.ColumnIndex != 0)
                {
                    try
                    {
                        ModelFactor modelFactor;
                        decimal data = decimal.Parse(dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        Factor factor;
                        if (modelFactorData != null)
                        {
                            modelFactor = ModelFactorHelper.Get(((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDModelFactor);
                            factor = FactorHelper.Get(ModelFactorHelper.Get(((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDModelFactor).IDFactor);
                            modelFactorData = ModelFactorDataHelper.Get(modelFactorData);
                        }
                        else
                        {
                            modelFactorData = ModelFactorDataHelper.GetNew();
                            modelFactor = ModelFactorHelper.Get(((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDModelFactor);
                            modelFactorData.IDModelFactor = modelFactor.IDModelFactor;
                            modelFactorData.IDRegion = ((Region)dgvTableByFactor.Columns[e.ColumnIndex].Tag).IDRegion;
                            modelFactorData.Date = DateTime.Parse(dgvTableByFactor.Rows[e.RowIndex].Cells[0].Value.ToString(), GeneralHelper.GetDateFormat());
                            factor = FactorHelper.Get(modelFactor.IDFactor);
                        }

                        if (factor.CumulativeFactor)
                        {
                            //if (data - Math.Truncate(data) != 0)
                            //{
                            //    data = Math.Round(data, MidpointRounding.AwayFromZero);
                            //    dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Input data has been rounded to fit the range of factor.";
                            //}
                            modelFactorData.Data = data;
                            modelFactorData.Date = DateTime.Parse(dgvTableByFactor.Rows[e.RowIndex].Cells[0].Value.ToString(), GeneralHelper.GetDateFormat());
                            ModelFactorData m = ModelFactorDataHelper.Save(modelFactorData);
                            _reloadModelData = true;
                            dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = m;
                            _valueChanged = true;
                            dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = m.Data.ToString();
                            _valueChanged = false;
                        }
                        else
                        {
                            if (modelFactor.ScaleMin <= data && modelFactor.ScaleMax >= data)
                            {
                                if ((modelFactor.Interval - Math.Truncate(modelFactor.Interval)) == 0)
                                {
                                    if (data - Math.Truncate(data) != 0)
                                    {
                                        data = Math.Round(data, MidpointRounding.AwayFromZero);
                                        dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Input data has been rounded to fit the range of factor.";
                                    }
                                }
                                else
                                {
                                    if ((data % (decimal)modelFactor.Interval) != 0)
                                    {
                                        if (modelFactor.Interval % 1 == 0)
                                            data = ((data % modelFactor.Interval) < (modelFactor.Interval / 2)) ? Math.Truncate(data) : Math.Truncate(data) + modelFactor.Interval;
                                        else
                                            data = ((data % 1) < (modelFactor.Interval)) ? Math.Truncate(data) : Math.Truncate(data) + modelFactor.Interval;
                                        dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Input data has been rounded to fit the range of factor.";
                                    }
                                }


                                modelFactorData.Data = data;
                                ModelFactorData m = ModelFactorDataHelper.Save(modelFactorData);
                                _reloadModelData = true;
                                dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = m;
                                _valueChanged = true;
                                dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = m.Data.ToString();
                                _valueChanged = false;
                            }
                            else
                            {
                                dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Input data outside the range of the scale.";
                                _valueChanged = true;
                                if (modelFactorData.IDModelFactorData == 0)
                                {
                                    dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font("Arial", 10, FontStyle.Regular);
                                    dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N/D";
                                }
                                else
                                {
                                    dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font("Arial", 10, FontStyle.Bold);
                                    dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = modelFactorData.Data.ToString();
                                }
                                _valueChanged = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Input data must be decimal.";
                        _valueChanged = true;
                        if (modelFactorData != null)
                        {
                            dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font("Arial", 10, FontStyle.Bold);
                            dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = modelFactorData.Data.ToString();
                        }
                        else
                        {
                            dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = new Font("Arial", 10, FontStyle.Regular);
                            dgvTableByFactor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N/D";
                        }
                        _valueChanged = false;
                    }
                }
            }
            _valueChanged = false;
        }

        private void dgvTableByRegion_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _valueChanged = false;
            _leave = (e.ColumnIndex == 0);
        }

        private void dgvTableByFactor_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            _valueChanged = false;
            _leave = (e.ColumnIndex == 0);
        }


        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        private bool ESC = false;

        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private void btnAddDateTableByRegion_Click(object sender, EventArgs e)
        {
            if (dgvTableByRegion.ColumnCount == 0)
                return;

            DateTime date =
                ModelFactorDataHelper.GetAvailableDateForPastedDataByRegionAndModel(
                    ((Region)tvSelectRegionTableByRegion.SelectedNode.Tag).IDRegion, ERMTSession.Instance.CurrentModel.IDModel);

            List<string> dgvRow = new List<string>();
            dgvRow.Add(date.ToString("d", GeneralHelper.GetDateFormat()));

            List<ModelFactor> mdList = ModelFactorHelper.GetByModel(ERMTSession.Instance.CurrentModel);
            dgvRow.AddRange(mdList.Select(mf => "N/D"));
            dgvTableByRegion.Rows.Add(dgvRow.ToArray());
            //dgvTableByRegion.Rows[dgvTableByRegion.Rows.Count - 1].ReadOnly = true;
            dgvTableByRegion.Rows[dgvTableByRegion.Rows.Count - 1].Cells[0].ReadOnly = true;
            foreach (DataGridViewRow row in dgvTableByRegion.Rows)
            {
                row.Cells[0].Selected = false;
            }
            dgvTableByRegion.Rows[dgvTableByRegion.Rows.Count - 1].Cells[0].Selected = true;
            dgvTableByRegion.CurrentCell = dgvTableByRegion.Rows[dgvTableByRegion.Rows.Count - 1].Cells[0];
            dgvTableByRegion.BeginEdit(true);
            dgvTableByRegion.Rows[dgvTableByRegion.Rows.Count - 1].Cells[0].ReadOnly = false;
        }

        private void btnAddDateTableByFactor_Click(object sender, EventArgs e)
        {
            if (dgvTableByFactor.ColumnCount == 0)
                return;
            if (tvSelectFactorTableByFactor.SelectedNode != null)
            {
                DateTime date =
                ModelFactorDataHelper.GetAvailableDateForPastedDataByModelFactor(
                    ((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDModelFactor);
                List<string> dgvRow = new List<string> { date.ToString("d", GeneralHelper.GetDateFormat()) };
                List<Region> regions = new List<Region> { RegionHelper.Get(ERMTSession.Instance.CurrentModel.IDRegion) };
                List<Region> regionsToAdd = RegionHelper.GetAllChilds(regions[0].IDRegion);
                regions.AddRange(regionsToAdd);
                dgvRow.AddRange(regions.Select(r => "N/D"));
                dgvTableByFactor.Rows.Add(dgvRow.ToArray());
                //dgvTableByFactor.Rows[dgvTableByFactor.Rows.Count - 1].ReadOnly = true;
                //dgvTableByFactor.Rows[dgvTableByFactor.Rows.Count - 1].Cells[0].ReadOnly = true;
                foreach (DataGridViewRow row in dgvTableByFactor.Rows)
                {
                    row.Cells[0].Selected = false;
                }
                dgvTableByFactor.Rows[dgvTableByFactor.Rows.Count - 1].Cells[0].Selected = true;
                dgvTableByFactor.CurrentCell = dgvTableByFactor.Rows[dgvTableByFactor.Rows.Count - 1].Cells[0];
                dgvTableByFactor.BeginEdit(true);
                _leave = true;
            }
            else
            {
                MessageBox.Show("Please select a factor to add a new date.");
            }
        }

        private void rbEachFactor_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                List<ModelFactor> lModelFactors = (from CheckBox c in flpChartFactors.Controls where c.Checked select ((ModelFactor)c.Tag)).ToList();
                GenerateChart(_lastChartType, lModelFactors, chart1);
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            TreeNode factorNode = tvSelectFactorTableByFactor.SelectedNode;
            TreeNode regionNode = tvSelectRegionTableByRegion.SelectedNode;
            string path = string.Empty;
            switch (((Button)sender).Name)
            {
                case "btnExportToExcel1":
                    ExportCSVButton.ExportToCSV(dgvTableByRegion, ERMTSession.Instance.CurrentModel.Name + "-" + ((Region)tvSelectRegionTableByRegion.SelectedNode.Tag).RegionName, false);
                    break;
                case "btnExportToExcel2":
                    if (tvSelectFactorTableByFactor.SelectedNode == null)
                    {
                        MessageBox.Show("Please select a factor", "Alert", MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        break;
                    }
                    ExportCSVButton.ExportToCSV(dgvTableByFactor, ERMTSession.Instance.CurrentModel.Name + "-" + FactorHelper.Get(((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDFactor).Name, false);
                    break;
                case "btnExportToExcel3":
                    if (fbd.ShowDialog() != DialogResult.OK)
                        return;
                    path = fbd.SelectedPath;
                    LoadingForm.ShowLoading();
                    List<TreeNode> nodes = new List<TreeNode>();
                    foreach (TreeNode n in tvSelectRegionTableByRegion.Nodes)
                    {
                        nodes.Add(n);
                        GetAllChildRegionNodes(n, ref nodes);
                    }

                    foreach (TreeNode node in nodes)
                    {
                        tvSelectRegionTableByRegion_NodeMouseClick(null, new TreeNodeMouseClickEventArgs(node, MouseButtons.Left, 0, 0, 0));
                        ExportCSVButton.ExportToCSV(dgvTableByRegion, path + "\\" + ERMTSession.Instance.CurrentModel.Name + "-" + ((Region)node.Tag).RegionName, true);
                    }
                    tvSelectRegionTableByRegion.SelectedNode = regionNode;
                    tvSelectRegionTableByRegion_NodeMouseClick(null, new TreeNodeMouseClickEventArgs(regionNode, MouseButtons.Left, 0, 0, 0));
                    LoadingForm.Fadeout();
                    break;
                case "btnExportToExcel4":
                    if (fbd.ShowDialog() != DialogResult.OK)
                        return;
                    LoadingForm.ShowLoading();
                    path = fbd.SelectedPath;
                    foreach (TreeNode node in tvSelectFactorTableByFactor.Nodes)
                    {
                        tvSelectFactorTableByFactor_NodeMouseClick(null, new TreeNodeMouseClickEventArgs(node, MouseButtons.Left, 0, 0, 0));
                        ExportCSVButton.ExportToCSV(dgvTableByFactor, path + "\\" + ERMTSession.Instance.CurrentModel.Name + "-" + FactorHelper.Get(((ModelFactor)node.Tag).IDFactor).Name, true);
                    }
                    tvSelectFactorTableByFactor.SelectedNode = factorNode;
                    tvSelectFactorTableByFactor_NodeMouseClick(null, new TreeNodeMouseClickEventArgs(factorNode, MouseButtons.Left, 0, 0, 0));
                    LoadingForm.Fadeout();
                    break;
            }
        }


        private void tcRiskMapping_SelectedIndexChanged(object sender, EventArgs e)
        {
            _chartLegend.Visible = false;

            if (_scaleFactorsLegend != null)
            {
                _scaleFactorsLegend.Visible = false;
            }

            if (_cumulativeFactorsLegend != null)
            {
                _cumulativeFactorsLegend.Visible = false;
            }

            if (_markersLegend != null)
            {
                _markersLegend.Visible = false;
            }

            switch (((TabControl)sender).SelectedTab.Name)
            {
                case "tpMap":
                    {
                        if (_scaleFactorsLegend != null)
                        {
                            _scaleFactorsLegend.Visible = tsmiMapFactorLegend.Checked;
                        }

                        if (_cumulativeFactorsLegend != null)
                        {
                            _cumulativeFactorsLegend.Visible = tsmiMapCumulativeFactorLegend.Checked;
                        }

                        if (_markersLegend != null)
                        {
                            _markersLegend.Visible = tsmiMapMarkerLegend.Checked;
                        }

                        if (_reloadModelData)
                        {
                            CacheModelData();
                            SetCumulativeMarkersVisibility(false);
                            CalculateFactorData();
                            GetCumulativeFactors();
                            SetColorScheme();
                            RefreshMarkers();
                            _reloadModelData = false;
                        }

                        RefreshMap();
                        break;
                    }
                case "tpCharting":
                    {
                        _chartLegend.Visible = tsmiChartShowLegends.Checked;
                        GenerateChart();
                        break;
                    }
                case "tpTableByRegion":
                    {
                        if (tvSelectRegionTableByRegion.SelectedNode != null)
                            tvSelectRegionTableByRegion_NodeMouseClick(tvSelectRegionTableByRegion, new TreeNodeMouseClickEventArgs(tvSelectRegionTableByRegion.SelectedNode, MouseButtons.Left, 1, 0, 0));
                        else
                            dgvTableByRegion.Rows.Clear();
                        break;
                    }
                case "tpTableByFactors":
                    {
                        if (tvSelectFactorTableByFactor.SelectedNode != null)
                            tvSelectFactorTableByFactor_NodeMouseClick(tvSelectFactorTableByFactor, new TreeNodeMouseClickEventArgs(tvSelectFactorTableByFactor.SelectedNode, MouseButtons.Left, 1, 0, 0));
                        else
                            dgvTableByFactor.Rows.Clear();
                        break;
                    }
                case "tpStaticMarkers":
                    {
                        LoadMarkersData();
                        break;
                    }
                case "tpFactorsInModel":
                    {
                        tpFactorsInModel.Controls.Clear();
                        IndexUserControl indexUserControl = new IndexUserControl
                        {
                            IndexContentType = IndexContentType.KnowledgeResourcesModel,
                            Dock = DockStyle.Fill
                        };
                        tpFactorsInModel.Controls.Add(indexUserControl);
                        indexUserControl.ShowHtml();
                        break;
                    }
                case "tpRiskAndAction":
                    {
                        tpRiskAndAction.Controls.Clear();
                        RiskAndActionRegister riskAndActionRegister = new RiskAndActionRegister { Dock = DockStyle.Fill };
                        tpRiskAndAction.Controls.Add(riskAndActionRegister);
                        break;
                    }

            }
        }

        private void tvSelectRegionTableByRegion_KeyUp(object sender, KeyEventArgs e)
        {
            tvSelectRegionTableByRegion_NodeMouseClick(sender, new TreeNodeMouseClickEventArgs(((TreeView)sender).SelectedNode, MouseButtons.None, 0, 0, 0));
        }

        private void tvSelectFactorTableByFactor_KeyUp(object sender, KeyEventArgs e)
        {
            tvSelectFactorTableByFactor_NodeMouseClick(sender, new TreeNodeMouseClickEventArgs(((TreeView)sender).SelectedNode, MouseButtons.None, 0, 0, 0));
        }

        private void dgvTableByFactor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V && tvSelectFactorTableByFactor.SelectedNode != null)
            {
                tsmiTableByFactor_Paste_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C && tvSelectFactorTableByFactor.SelectedNode != null)
            {
                tsmiTableByFactor_Copy_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void dgvTableByRegion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V && tvSelectRegionTableByRegion.SelectedNode != null)
            {
                tsmiTableByRegion_Paste_Click(sender, e);
            }
            if (e.Control && e.KeyCode == Keys.C && tvSelectRegionTableByRegion.SelectedNode != null)
            {
                tsmiTableByRegion_Copy_Click(sender, e);
            }
        }

        private void dgvTableByRegion_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (e.RowIndex < 0 || ((DataGridView)sender).Rows[e.RowIndex].Selected) return;
            btnRegionDeleteDate.Enabled = ((DataGridView)sender).SelectedRows.Count > 0;
            foreach (DataGridViewRow r in ((DataGridView)sender).Rows)
            {
                r.Selected = r.Index == e.RowIndex;
            }
        }

        private void dgvTableByFactor_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (e.RowIndex < 0 || ((DataGridView)sender).Rows[e.RowIndex].Selected) return;
            foreach (DataGridViewRow r in ((DataGridView)sender).Rows)
            {
                r.Selected = r.Index == e.RowIndex;
            }
        }

        private void dgvTableByRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 46) return;
            int row = ((DataGridView)sender).CurrentCellAddress.Y;
            int col = ((DataGridView)sender).CurrentCellAddress.X;
            ModelFactorData mfd = (ModelFactorData)((DataGridView)sender).Rows[row].Cells[col].Tag;
            if (mfd != null)
            {
                ModelFactorDataHelper.Delete(mfd);
                tvSelectRegionTableByRegion_NodeMouseClick(tvSelectRegionTableByRegion, new TreeNodeMouseClickEventArgs(tvSelectRegionTableByRegion.SelectedNode, MouseButtons.Left, 1, 0, 0));
            }
        }

        private void dgvTableByFactor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 46) return;

            if (e.Control && e.KeyCode == Keys.V && tvSelectFactorTableByFactor.SelectedNode != null)
            {
                tsmiTableByFactor_Paste_Click(sender, e);
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C && tvSelectFactorTableByFactor.SelectedNode != null)
            {
                tsmiTableByFactor_Copy_Click(sender, e);
                e.SuppressKeyPress = true;
            }

            int row = ((DataGridView)sender).CurrentCellAddress.Y;
            int col = ((DataGridView)sender).CurrentCellAddress.X;
            ModelFactorData mfd = (ModelFactorData)((DataGridView)sender).Rows[row].Cells[col].Tag;
            if (mfd != null && mfd.IDModelFactorData != 0)
            {
                ModelFactorDataHelper.Delete(mfd);
                tvSelectFactorTableByFactor_NodeMouseClick(tvSelectFactorTableByFactor, new TreeNodeMouseClickEventArgs(tvSelectFactorTableByFactor.SelectedNode, MouseButtons.Left, 1, 0, 0));
            }
        }

        #region "Static Markers Tab"

        private void LoadMarkersData()
        {
            dgvStaticMarkers.Rows.Clear();
            List<MarkerType> markerTypes = MarkerTypeHelper.GetAll();
            DataGridViewComboBoxColumn markerTypesColumn = (DataGridViewComboBoxColumn)dgvStaticMarkers.Columns[2];
            markerTypesColumn.DisplayMember = "Name";
            markerTypesColumn.ValueMember = "IDMarkerType";
            markerTypesColumn.DataSource = markerTypes;
            if (ERMTSession.Instance.CurrentModel != null)
            {
                List<Marker> markers = MarkerHelper.GetByModelId(ERMTSession.Instance.CurrentModel.IDModel);
                foreach (Marker marker in markers)
                {
                    LoadMarkerToGrid(markerTypes, marker);
                }
            }
        }

        private void LoadMarkerToGrid(List<MarkerType> markerTypes, Marker marker)
        {
            int i;
            i = dgvStaticMarkers.Rows.Add(marker.DateFrom.ToString("d", GeneralHelper.GetDateFormat()), (marker.DateTo != marker.DateFrom ? marker.DateTo.ToString("d", GeneralHelper.GetDateFormat()) : string.Empty), marker.IDMarkerType, marker.Name, marker.Description, /*marker.Color,*/ marker.Latitude, marker.Longitude);
            DataGridViewRow r = dgvStaticMarkers.Rows[i];
            r.Tag = marker;
        }

        private void dgvStaticMarkers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
        }

        private void dgvStaticMarkers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ERMTSession.Instance.CurrentUser.IDRole < 4)
            {
                Marker marker = (Marker)dgvStaticMarkers.Rows[e.RowIndex].Tag;
                DataGridViewRow currentRow = dgvStaticMarkers.Rows[e.RowIndex];
                MarkerPickForm markerPF = new MarkerPickForm();
                markerPF.StartPosition = FormStartPosition.CenterParent;
                markerPF.Title = currentRow.Cells["Text"].Value.ToString();
                markerPF.TextContent = currentRow.Cells["Description"].Value.ToString();
                markerPF.Latitude = Convert.ToDecimal(currentRow.Cells["Latitude"].Value.ToString());
                markerPF.Longitude = Convert.ToDecimal(currentRow.Cells["Longitude"].Value.ToString());
                markerPF.From = DateTime.Parse(currentRow.Cells["DateFrom"].Value.ToString());
                markerPF.To = (currentRow.Cells["DateTo"].Value != null && !String.IsNullOrEmpty(currentRow.Cells["DateTo"].Value.ToString())
                    ? DateTime.Parse(currentRow.Cells["DateTo"].Value.ToString())
                    : DateTime.Parse(currentRow.Cells["DateFrom"].Value.ToString()));
                markerPF.MarkerType =
                    MarkerTypeHelper.Get(Convert.ToInt32(currentRow.Cells["MarkerType"].Value.ToString()));
                markerPF.TitleColor = (!String.IsNullOrEmpty(marker.Color)
                    ? ColorTranslator.FromHtml(marker.Color)
                    : Color.Black);

                if (markerPF.ShowDialog() != DialogResult.OK)
                {
                    markerPF.Close();
                    return;
                }

                if (markerPF.Title != string.Empty && markerPF.Latitude.ToString() != string.Empty &&
                    markerPF.Longitude.ToString() != string.Empty && markerPF.MarkerType.IDMarkerType != 0)
                {
                    marker.Name = markerPF.Title;
                    marker.Description = markerPF.TextContent;
                    marker.Latitude = markerPF.Latitude;
                    marker.Longitude = markerPF.Longitude;
                    marker.DateFrom = markerPF.From;
                    marker.DateTo = markerPF.To;
                    marker.IDMarkerType = markerPF.MarkerType.IDMarkerType;
                    marker.IDModel = ERMTSession.Instance.CurrentModel.IDModel;
                    marker.Color = ColorTranslator.ToHtml(markerPF.TitleColor);
                    markerPF.Close();

                    MarkerHelper.Save(marker);

                    LoadMarkersData();
                }
                else
                {
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("MarkerValidation"));
                }
            }
        }

        private void dgvStaticMarkers_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //Color column is not editable
            if (e.ColumnIndex == 5)
                e.Cancel = true;
        }

        private void tsmiStaticMarker_Copy_Click(object sender, EventArgs e)
        {
            CopyMarkerRow();
        }

        private void tsmiStaticMarker_Paste_Click(object sender, EventArgs e)
        {
            PasteMarkerRow();
        }

        private void tsmiStaticMarker_Delete_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ConfirmDeleteMarkers"), CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo)
                == CustomMessageBoxReturnValue.Ok)
            {
                List<DataGridViewRow> rows = new List<DataGridViewRow>();
                foreach (DataGridViewRow r in dgvStaticMarkers.SelectedRows)
                {
                    MarkerHelper.Delete((Marker)r.Tag);
                    rows.Add(r);
                }
                foreach (DataGridViewRow r in rows)
                    dgvStaticMarkers.Rows.Remove(r);
            }
        }

        private void btnAddMarker_Click(object sender, EventArgs e)
        {
            List<MarkerType> types = MarkerTypeHelper.GetAll();
            if (types.Count == 0)
            {
                CustomMessageBox.ShowError(ResourceHelper.GetResourceText("NoMarkerTypes"));
                return;
            }

            MarkerPickForm markerPF = new MarkerPickForm
            {
                StartPosition = FormStartPosition.CenterParent,
                Title = string.Empty,
                TextContent = string.Empty,
                Latitude = 0,
                Longitude = 0,
                TitleColor = Color.Black
            };


            if (markerPF.ShowDialog() != DialogResult.OK)
            {
                markerPF.Close();
                return;
            }

            if (markerPF.Title != string.Empty && markerPF.Latitude.ToString() != string.Empty &&
                markerPF.Longitude.ToString() != string.Empty && markerPF.MarkerType.IDMarkerType != 0)
            {
                Marker marker = MarkerHelper.GetNew();
                marker.Name = markerPF.Title;
                marker.Description = markerPF.TextContent;
                marker.Color = ColorTranslator.ToHtml(markerPF.TitleColor);
                // ((Button)markerPF.Controls.Find("btnPickAColor", true)[0]).Enabled ? ColorTranslator.ToHtml(markerPF.Color) : string.Empty;

                marker.Latitude = markerPF.Latitude;
                marker.Longitude = markerPF.Longitude;
                marker.DateFrom = markerPF.From;
                marker.DateTo = markerPF.To;
                marker.IDMarkerType = markerPF.MarkerType.IDMarkerType;
                marker.IDModel = ERMTSession.Instance.CurrentModel.IDModel;

                foreach (CheckBox c in flpMarkerType.Controls)
                {
                    if (((MarkerType)c.Tag).IDMarkerType != marker.IDMarkerType)
                    {
                        continue;
                    }
                    c.Checked = true;
                    break;
                }
                markerPF.Close();

                MarkerHelper.Save(marker);
                if (!_modelMarkerTypes.Where(mt => mt.IDMarkerType == marker.IDMarkerType).Any())
                {
                    //the marker type is not currently included in the model.
                    MarkerType mt = MarkerTypeHelper.Get(marker.IDMarkerType);
                    AddMarkerTypeCheckBox(mt, true);
                }

                LoadMarkersData();
                chkMarkerType_CheckedChanged(new object(), new EventArgs());
            }
        }

        private void btnExportToExcelMarker_Click(object sender, EventArgs e)
        {
            ExportCSVButton.ExportToCSV(dgvStaticMarkers, ERMTSession.Instance.CurrentModel.Name + "-Static Markers", false);
        }

        private void dgvStaticMarkers_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteMarkerRow();
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                CopyMarkerRow();
            }
        }

        private void CopyMarkerRow()
        {
            String csv = "";
            foreach (DataGridViewRow v in dgvStaticMarkers.SelectedRows)
            {
                foreach (DataGridViewCell f in v.Cells)
                {
                    csv += (f.FormattedValue is string ? f.FormattedValue.ToString() : "") + "\t";
                }
                csv = csv.Substring(0, csv.Length - 2);
                csv += Environment.NewLine;
            }
            if (csv.Length > 0)
                csv = csv.Substring(0, csv.Length - 3);
            byte[] blob = System.Text.Encoding.Unicode.GetBytes(csv);
            MemoryStream s = new MemoryStream(blob);
            DataObject data = new DataObject();
            data.SetData(DataFormats.Text, s);
            Clipboard.SetDataObject(data, true);
        }

        private void PasteMarkerRow()
        {
            string clipboardData = Clipboard.GetText();
            if (clipboardData.Length == 0)
            {
                CustomMessageBox.ShowError("Nothing in the clipboard");
                return;
            }

            StringReader sr = new StringReader(clipboardData);
            string line = string.Empty;
            List<MarkerType> markerTypes = MarkerTypeHelper.GetAll();
            List<Marker> newMarkers = new List<Marker>();
            while ((line = sr.ReadLine()) != null)
            {
                Marker newMarker = MarkerHelper.GetNew();
                bool valid = true;
                //Validations
                string[] markerValues = line.Split('\t');
                int colCount = markerValues.Length;

                if (colCount != dgvStaticMarkers.Columns.Count)
                {
                    CustomMessageBox.ShowError(ResourceHelper.GetResourceText("ColumnCountNoMatch"));
                    return;
                }

                //Validations
                DateTime d, d2 = new DateTime();
                float f;
                if (DateTime.TryParse(markerValues[0], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out d) && d > new DateTime(1900, 1, 1) && d < new DateTime(2200, 1, 1))
                {
                    newMarker.DateFrom = d;
                    newMarker.DateTo = d;
                }
                else
                {
                    //CustomMessageBox.ShowError("Datetime error. InstalledUICUlture: " + CultureInfo.InstalledUICulture);
                    newMarker.DateFrom = DateTime.Now;
                    newMarker.DateTo = DateTime.Now;
                    valid = false;
                }

                //if (DateTime.TryParse(markerValues[1], CultureInfo.InstalledUICulture, DateTimeStyles.None, out d2) &&
                //    d2 > new DateTime(1900, 1, 1) && d2 < new DateTime(2200, 1, 1))
                if (DateTime.TryParse(markerValues[1], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out d2) &&
                    d2 > new DateTime(1900, 1, 1) && d2 < new DateTime(2200, 1, 1))
                {
                    newMarker.DateTo = d2;
                }
                else
                {
                    //CustomMessageBox.ShowError("Datetime error 2. InstalledUICUlture: " + CultureInfo.InstalledUICulture);
                    if (markerValues[1] != string.Empty)
                    {
                        valid = false;
                    }
                }

                if (markerValues[2] != string.Empty)
                {
                    int idMarkerType =
                        (from m in markerTypes where m.Name.ToLower() == markerValues[2].ToLower() select m.IDMarkerType)
                            .FirstOrDefault();
                    if (idMarkerType == 0)
                        idMarkerType = markerTypes[0].IDMarkerType;
                    newMarker.IDMarkerType = idMarkerType;
                }
                else
                {
                    valid = false;
                }

                newMarker.Name = markerValues[3];
                newMarker.Description = markerValues[4];
                newMarker.IDModel = ERMTSession.Instance.CurrentModel.IDModel;

                if (markerValues[5] != string.Empty)
                {
                    if (float.TryParse(markerValues[5], out f))
                    {
                        if (f > -90F && f < 90F)
                        {
                            newMarker.Latitude = (decimal)f;
                        }
                        else valid = false;
                    }
                    else valid = false;
                }

                if (markerValues[6] != string.Empty)
                {
                    if (float.TryParse(markerValues[6], out f))
                    {
                        if (f > -180F && f < 180F)
                        {
                            newMarker.Longitude = (decimal)f;
                        }
                        else valid = false;
                    }
                    else valid = false;
                }

                if (valid)
                {
                    newMarker.Color = newMarker.Color ?? string.Empty;
                    newMarker.Name = newMarker.Name ?? string.Empty;

                    newMarker = MarkerHelper.Save(newMarker);
                    newMarkers.Add(newMarker);
                }
            }
            foreach (Marker marker in newMarkers)
            {
                LoadMarkerToGrid(markerTypes, marker);
            }
        }

        private void btnDeleteMarker_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ConfirmDeleteMarkers"),
                CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo)
                == CustomMessageBoxReturnValue.Ok)
            {
                for (int i = 0; i < dgvStaticMarkers.SelectedRows.Count; i++)
                {
                    Marker marker = (Marker)dgvStaticMarkers.SelectedRows[i].Tag;

                    ThinkGeoMarker thinkGeoMarkerToRemove = new ThinkGeoMarker();
                    foreach (ThinkGeoMarker thinkGeoMarker in _markersOverlay.Markers)
                    {
                        Marker m = (Marker)thinkGeoMarker.Tag;
                        if (m.IDMarker == marker.IDMarker)
                        {
                            thinkGeoMarkerToRemove = thinkGeoMarker;
                        }
                    }

                    thinkGeoMarkerToRemove.Visible = false;
                    _markersOverlay.Markers.Remove(thinkGeoMarkerToRemove);
                    bool removeMarkerType = true;
                    foreach (ThinkGeoMarker geoMarker in _markersOverlay.Markers)
                    {
                        if (((Marker)geoMarker.Tag).IDMarkerType == marker.IDMarkerType)
                        {
                            removeMarkerType = false;
                        }
                    }

                    if (removeMarkerType)
                    {
                        MarkerType mt = MarkerTypeHelper.Get(marker.IDMarkerType);
                        RemoveMarkerTypeCheckBox(mt);
                    }

                    MarkerHelper.Delete(marker);
                }
                LoadMarkersData();

                RefreshMarkers();
                RefreshMap();
            }
        }

        #endregion

        private void Tree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            // Draw the background and node text for a selected node.
            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                // Draw the background of the selected node. 
                e.Graphics.FillRectangle(Brushes.SteelBlue, e.Node.Bounds);

                // Retrieve the node font. If the node font has not been set,
                // use the TreeView font.
                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;

                // Draw the node text.
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White,
                    Rectangle.Inflate(e.Bounds, 2, 0));

            }

            // Use the default background and node text.
            else
            {
                e.DrawDefault = true;
            }

            // If the node has focus, draw the focus rectangle.
            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }
        }

        private void btnRegionDeleteDate_Click(object sender, EventArgs e)
        {
            if (dgvTableByRegion.RowCount > 0)
            {
                DeleteDate dd = new DeleteDate();
                if (dd.ShowDialog() == DialogResult.OK)
                {
                    //Borrar fecha
                    DateTime dt = new DateTime(dd.Date.Year, dd.Date.Month, dd.Date.Day);

                    ModelFactorDataHelper.DeleteByDateModelRegion(dt, ERMTSession.Instance.CurrentModel.IDModel, ((Region)tvSelectRegionTableByRegion.SelectedNode.Tag).IDRegion);
                    //recargar grilla
                    LoadTableByRegion(tvSelectRegionTableByRegion.SelectedNode);
                    _reloadModelData = true;
                }
            }
            else CustomMessageBox.ShowError(ResourceHelper.GetResourceText("TableMustHaveData"));
        }

        private void btnFactorDeleteDate_Click(object sender, EventArgs e)
        {
            if (dgvTableByFactor.RowCount > 0)
            {
                DeleteDate dd = new DeleteDate();
                if (dd.ShowDialog() == DialogResult.OK)
                {
                    //Borrar fecha
                    DateTime dt = new DateTime(dd.Date.Year, dd.Date.Month, dd.Date.Day);

                    ModelFactorDataHelper.DeleteByDateModelFactor(dt, ((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDModelFactor);
                    //recargar grilla
                    LoadTableByFactor(tvSelectFactorTableByFactor.SelectedNode);
                    _reloadModelData = true;
                }
            }
            else CustomMessageBox.ShowError(ResourceHelper.GetResourceText("TableMustHaveData"));
        }

        //#region Add/Copy/Paste Factors
        //private void addFactorRowMenuItem_Click(object sender, EventArgs e)
        //{
        //    Model model = ERMTSession.Instance.CurrentModel;
        //    List<ModelFactor> modelFactors = ModelFactorHelper.GetByModel(model);
        //    List<ModelFactorData> modelFactorsData = new List<ModelFactorData>();
        //    foreach (var mFactor in modelFactors)
        //    {
        //        modelFactorsData = ModelFactorDataHelper.GetByModelFactor(mFactor);
        //    }

        //    List<string> dgvRow = new List<string>();
        //    dgvRow.Add(DateTime.Now.ToString("d", GeneralHelper.GetDateFormat()));

        //    for (int col = 1; col < modelFactorsData.Count; col++)
        //    {
        //        dgvRow.Add("N/D");
        //    }
        //    dgvTableByFactor.Rows.Add(dgvRow.ToArray());
        //    ModelFactorData m = ModelFactorDataHelper.GetNew();
        //    m.Date = DateTime.Now;
        //    m.IDModelFactor = ((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDModelFactor;
        //    dgvTableByFactor.Rows[dgvTableByFactor.Rows.Count - 1].Cells[0].Tag = m;
        //}

        //private void copyFactorRowMenuItem_Click(object sender, EventArgs e)
        //{
        //    String csv = "";
        //    foreach (DataGridViewRow v in dgvTableByFactor.SelectedRows)
        //    {
        //        foreach (DataGridViewCell f in v.Cells)
        //        {
        //            if (f.Visible)
        //                csv += (f.FormattedValue is string ? f.FormattedValue.ToString() : "") + "\t";
        //        }
        //        csv += Environment.NewLine;
        //    }
        //    if (csv.Length > 0)
        //        csv = csv.Substring(0, csv.Length - 3);
        //    byte[] blob = System.Text.Encoding.Unicode.GetBytes(csv);
        //    MemoryStream s = new MemoryStream(blob);
        //    DataObject data = new DataObject();
        //    data.SetData(DataFormats.Text, s);
        //    Clipboard.SetDataObject(data, true);
        //}

        //private void pasteFactorRowMenuItem_Click(object sender, EventArgs e)
        //{
        //    DateTime d = new DateTime();
        //    string s = Clipboard.GetText();
        //    string[] lines = s.Replace("\r", "").Split('\n');
        //    int iFail = 0;
        //    int iRow = dgvTableByFactor.CurrentCell.RowIndex;
        //    int iCol = dgvTableByFactor.CurrentCell.ColumnIndex;
        //    DataGridViewCell oCell;
        //    int colCount = lines[0].Split('\t').Length;
        //    if (colCount > dgvTableByFactor.Columns.Count)
        //    {
        //        MessageBox.Show(ResourceHelper.GetResourceText("ColumnCountNoMatch"));
        //        return;
        //    }
        //    if (lines.Length > 200)
        //    {
        //        MessageBox.Show(ResourceHelper.GetResourceText("PasteUpTo200Rows"));
        //        return;
        //    }
        //    foreach (string line in lines)
        //    {

        //        if (iRow < dgvTableByFactor.RowCount && line.Length > 0)
        //        {
        //            string[] sCells = line.Split('\t');
        //            for (int i = 0; i < sCells.GetLength(0); ++i)
        //            {
        //                d = new DateTime();
        //                //Chequeo que el indice de columna sea el 0 (Date) para validar fecha.
        //                if (iCol == 0)
        //                {
        //                    if (DateTime.TryParse((string)sCells[0], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out d) && d > new DateTime(1900, 1, 1) &&
        //                        d < new DateTime(2200, 1, 1))
        //                    {
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Date not valid");
        //                        return;
        //                    }
        //                }
        //                oCell = GetVisibleCellAtIndex(dgvTableByFactor.Columns, iCol, i, dgvTableByFactor.Rows[iRow]);
        //                if (oCell != null)
        //                {
        //                    //oCell = dgvTableByFactor[iCol + i, iRow];
        //                    if (!oCell.ReadOnly)
        //                    {
        //                        _leave = oCell.ColumnIndex == 0;
        //                        dgvTableByFactor.Rows[oCell.RowIndex].Cells[oCell.ColumnIndex].Value = sCells[i];
        //                    }
        //                }
        //                else
        //                { break; }
        //            }
        //            iRow++;
        //        }
        //        else
        //        {
        //            d = new DateTime();
        //            //Este caso trata una nueva fila, solo si la primer columna pegada es datetime.
        //            string[] sCells = line.Split('\t');
        //            if (DateTime.TryParse((string)sCells[0], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out d) && d > new DateTime(1900, 1, 1) &&
        //                        d < new DateTime(2200, 1, 1))
        //            {
        //                //Agrego una nueva fila.
        //                addFactorRowMenuItem_Click(sender, EventArgs.Empty);
        //                for (int i = 0; i < sCells.GetLength(0); ++i)
        //                {
        //                    //Chequeo que el indice de columna sea el 0 (Date) para validar fecha.
        //                    if (iCol == 0)
        //                    {
        //                        if (DateTime.TryParse((string)sCells[0], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out d) && d > new DateTime(1900, 1, 1) &&
        //                            d < new DateTime(2200, 1, 1))
        //                        {
        //                        }
        //                        else
        //                        {
        //                            MessageBox.Show("Date not valid");
        //                            return;
        //                        }
        //                    }
        //                    oCell = GetVisibleCellAtIndex(dgvTableByFactor.Columns, iCol, i, dgvTableByFactor.Rows[iRow]);
        //                    if (oCell != null)
        //                    {
        //                        //oCell = dgvTableByFactor[iCol + i, iRow];
        //                        if (!oCell.ReadOnly)
        //                        {
        //                            dgvTableByFactor.Rows[oCell.RowIndex].Cells[oCell.ColumnIndex].Value = sCells[i];
        //                            //dgvTableByFactor_CellEndEdit(dgvTableByFactor, new DataGridViewCellEventArgs(oCell.ColumnIndex, oCell.RowIndex));
        //                        }
        //                    }
        //                    else
        //                    { break; }
        //                }
        //            }
        //            iRow++;
        //        }

        //    }
        //}
        //#endregion

        private void dgvTableByRegion_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTableByRegion.CurrentCell != null)
            {
                if (e.RowIndex == dgvTableByRegion.CurrentCell.RowIndex && e.ColumnIndex == dgvTableByRegion.CurrentCell.ColumnIndex)
                    e.CellStyle.SelectionBackColor = Color.SteelBlue;
                else
                    e.CellStyle.SelectionBackColor = dgvTableByRegion.DefaultCellStyle.SelectionBackColor;
            }
        }

        private void dgvTableByFactor_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTableByFactor.CurrentCell != null)
            {
                if (e.RowIndex == dgvTableByFactor.CurrentCell.RowIndex && e.ColumnIndex == dgvTableByFactor.CurrentCell.ColumnIndex)
                    e.CellStyle.SelectionBackColor = Color.SteelBlue;
                else
                    e.CellStyle.SelectionBackColor = dgvTableByFactor.DefaultCellStyle.SelectionBackColor;
            }
        }

        private void dgvStaticMarkers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvStaticMarkers.CurrentCell != null)
            {
                if (e.RowIndex == dgvStaticMarkers.CurrentCell.RowIndex && e.ColumnIndex == dgvStaticMarkers.CurrentCell.ColumnIndex)
                    e.CellStyle.SelectionBackColor = Color.SteelBlue;
                else
                    e.CellStyle.SelectionBackColor = dgvStaticMarkers.DefaultCellStyle.SelectionBackColor;
            }
        }

        private DataGridViewCell GetVisibleCellAtIndex(DataGridViewColumnCollection columns, int start, int index, DataGridViewRow row)
        {
            int aux = -1;
            for (int i = start; i < columns.Count; i++)
            {
                if (columns[i].Visible)
                    aux++;
                if (aux == index)
                    return row.Cells[i];
            }
            return null;
        }

        private void dgvStaticMarkers_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            int index = ((DataGridViewTextBoxColumn)e.Column).DisplayIndex;
            switch (index)
            {
                case 0:
                case 1:
                    try
                    {
                        DateTime date1 = Convert.ToDateTime(e.CellValue1);
                        DateTime date2 = Convert.ToDateTime(e.CellValue2);

                        e.SortResult = DateTime.Compare(date1, date2);
                        e.Handled = true;
                        return;
                    }
                    catch
                    {
                        e.SortResult = 0;
                        e.Handled = true;
                        return;
                    }
                    break;
                case 6:
                case 7:
                    try
                    {
                        decimal float1 = Convert.ToDecimal(e.CellValue1);
                        decimal float2 = Convert.ToDecimal(e.CellValue2);

                        e.SortResult = decimal.Compare(float1, float2);
                        e.Handled = true;
                        return;
                    }
                    catch
                    {
                        e.SortResult = 0;
                        e.Handled = true;
                        return;
                    }
                    break;
                default:
                    break;
            }
        }


        private void tsmiChartSaveAsImage_Click(object sender, EventArgs e)
        {
            const string extension = ".jpg";
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = txtMapSettingsName.Text + extension,
                Filter =
                    "Jpg Image (*.jpg)|*.jpg|Windows Bitmap (*.bmp)|*.bmp|TIFF Image(*.tif)|*.tif|GIF Image(*.gif)|*.gif"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ChartImageFormat cif = ChartImageFormat.Jpeg;
                switch (Path.GetExtension(saveFileDialog.FileName).ToLower())
                {
                    case "jpg":
                        cif = ChartImageFormat.Jpeg;
                        break;
                    case "gif":
                        cif = ChartImageFormat.Gif;
                        break;
                    case "tif":
                        cif = ChartImageFormat.Tiff;
                        break;
                    case "bmp":
                        cif = ChartImageFormat.Bmp;
                        break;
                }
                chart1.SaveImage(saveFileDialog.FileName, cif);
            }
        }

        private void btnSaveMapAndChartSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if ((((Button)sender).Name == "btnSaveMapSettings" && txtMapSettingsName.Text != string.Empty) || (((Button)sender).Name == "btnSaveChartSettings" && txtChartSettingsName.Text != string.Empty))
                {
                    int type = int.Parse(((Button)sender).Tag.ToString());
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml("<report/>");
                    XmlAttribute at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("from"));
                    at.Value = dtpMapDateFrom.Value.ToString("yyyy-MM-dd");
                    at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("to"));
                    at.Value = dtpMapDateTo.Value.ToString("yyyy-MM-dd");
                    at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("factor" + type));
                    if (type == 1)
                    {
                        string factorIds = string.Empty;
                        foreach (CheckBox c in flpMapFactors.Controls)
                        {
                            if (c.Checked)
                                factorIds += ((ModelFactor)c.Tag).IDFactor + ";";
                        }
                        at.Value = factorIds;
                    }
                    else
                    {
                        string factorIds = string.Empty;
                        foreach (CheckBox c in flpChartFactors.Controls)
                        {
                            if (c.Checked)
                                factorIds += ((ModelFactor)c.Tag).IDFactor + ";";
                        }
                        at.Value = factorIds;
                    }
                    if (type == 1)
                    {
                        at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("colorScheme"));
                        at.Value = cbMapColorScheme.SelectedIndex.ToString();

                        at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("selectedColor"));
                        at.Value = pnlSelectedColor.BackColor.ToArgb().ToString();
                    }
                    List<int> regions = new List<int>();
                    if (type == 1)
                        GetCheckedRegionIds(tvMapRegions.Nodes, ref regions);
                    else
                        GetCheckedRegionIds(tvChartRegions.Nodes, ref regions);

                    at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("region" + type));
                    string regionIds = string.Empty;
                    foreach (int i in regions)
                    {
                        regionIds += i + ";";
                    }
                    at.Value = regionIds;
                    Report report = new Report();

                    List<Report> existingReports = (type == 1
                        ? ReportHelper.GetByNameAndType(txtMapSettingsName.Text, type)
                        : ReportHelper.GetByNameAndType(txtChartSettingsName.Text, type));

                    if (existingReports.Count > 0)
                    {
                        report = existingReports[0];
                    }

                    if (type == 1)
                    {
                        at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("markerTypes"));
                        string ids = string.Empty;
                        foreach (CheckBox c in flpMarkerType.Controls)
                        {
                            if (c.Checked)
                                ids += ((MarkerType)c.Tag).Symbol + ";";
                        }
                        at.Value = ids;
                    }
                    else
                    {
                        at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("factorCombination"));
                        foreach (RadioButton r in flpFactorCombination.Controls)
                        {
                            if (!r.Checked) continue;
                            at.Value = r.Name;
                            break;
                        }
                        at = doc.DocumentElement.Attributes.Append(doc.CreateAttribute("chartType"));
                        at.Value = _lastChartType.ToString();
                    }

                    if (type == 1)
                    {
                        report.Type = 1;
                        report.Name = txtMapSettingsName.Text;
                    }
                    else
                    {
                        report.Type = 2;
                        report.Name = txtChartSettingsName.Text;
                    }
                    report.IDModel = ERMTSession.Instance.CurrentModel.IDModel;
                    report.Parameters = doc.OuterXml;
                    //JPF 08072015: this is something from V6. 
                    report.Markers = string.Empty;
                    ReportHelper.Save(report);
                    LoadReports(ERMTSession.Instance.CurrentModel);
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("SaveReportOk"),
                        CustomMessageBoxMessageType.Information, CustomMessageBoxButtonType.OKOnly);
                }
                else
                {
                    CustomMessageBox.ShowError(ResourceHelper.GetResourceText("EmptyField"));
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("SaveReportError"), CustomMessageBoxMessageType.Error);
            }
        }

        private void btnLoadMapAndChartSettings_Click(object sender, EventArgs e)
        {
            try
            {
                tvMapRegions.AfterCheck -= tvMapRegions_AfterCheck;
                LoadingForm.ShowLoading();
                int type = int.Parse(((Button)sender).Tag.ToString());
                XmlDocument doc = new XmlDocument();
                Report report;
                if (type == 1)
                    report = (Report)lbSavedReports.SelectedItem;
                else
                    report = (Report)lbSavedReportsCharting.SelectedItem;
                doc.LoadXml(report.Parameters);
                XmlNode node = doc.DocumentElement;

                if (type == 1)
                {
                    MarkRegionsToLoad(tvMapRegions.Nodes, node, "region1");
                }
                else
                {
                    MarkRegionsToLoad(tvChartRegions.Nodes, node, "region2");
                }

                List<int> markerTypeIds = new List<int>();
                if (type == 1)
                {
                    foreach (CheckBox c in flpMapFactors.Controls)
                    {
                        if (node.Attributes["factor1"].Value.Contains(((ModelFactor)c.Tag).IDFactor.ToString() + ";"))
                            c.Checked = true;
                        else
                            c.Checked = false;
                    }
                    foreach (CheckBox c in flpMarkerType.Controls)
                    {
                        if (node.Attributes["markerTypes"].Value.Contains(((MarkerType)c.Tag).Symbol + ";"))
                        {
                            c.Checked = true;
                            markerTypeIds.Add(((MarkerType)c.Tag).IDMarkerType);
                        }
                        else
                            c.Checked = false;
                    }

                    int colorSchemeIndex = 0;
                    int.TryParse(node.Attributes["colorScheme"].Value, out colorSchemeIndex);
                    cbMapColorScheme.SelectedIndex = (colorSchemeIndex >= 0 ? colorSchemeIndex : 0);

                    int intPnlSelectedColor = Convert.ToInt32(node.Attributes["selectedColor"].Value);

                    if (intPnlSelectedColor == -986896 && cbMapColorScheme.SelectedIndex < 1)
                    {
                        cbMapColorScheme.SelectedIndex = 1;
                    }
                    pnlSelectedColor.BackColor = Color.FromArgb(intPnlSelectedColor);
                }
                else
                {
                    List<ModelFactor> mf = new List<ModelFactor>();
                    foreach (CheckBox c in flpChartFactors.Controls)
                    {
                        if (!node.Attributes["factor2"].Value.Contains(((ModelFactor)c.Tag).IDFactor.ToString() + ";"))
                            continue;
                        c.Checked = true;
                        mf.Add((ModelFactor)c.Tag);
                    }
                    foreach (RadioButton r in flpFactorCombination.Controls)
                    {
                        if (!node.Attributes["factorCombination"].Value.Contains(r.Name)) continue;
                        r.Checked = true;
                        break;
                    }

                    switch (node.Attributes["chartType"].Value.ToString())
                    {
                        case "FastLine":
                            _lastChartType = SeriesChartType.FastLine;
                            break;
                        case "Line":
                            _lastChartType = SeriesChartType.Line;
                            break;
                        case "Spline":
                            _lastChartType = SeriesChartType.Spline;
                            break;
                        case "Spline Area":
                            _lastChartType = SeriesChartType.SplineArea;
                            break;
                        default:
                            _lastChartType = SeriesChartType.Column;
                            break;
                    }
                }

                if (type == 1)
                {
                    dtpMapDateFrom.Value = DateTime.Parse(node.Attributes["from"].Value);
                    dtpMapDateTo.Value = DateTime.Parse(node.Attributes["to"].Value);
                    foreach (CheckBox c in flpMapFactors.Controls)
                    {
                        if (node.Attributes["factor1"].Value == ((ModelFactor)c.Tag).IDModelFactor.ToString())
                        {
                            c.Checked = true;
                            break;
                        }
                    }
                }
                else
                {
                    dtpChartDateFrom.Value = DateTime.Parse(node.Attributes["from"].Value);
                    dtpChartDateTo.Value = DateTime.Parse(node.Attributes["to"].Value);
                    foreach (CheckBox c in flpChartFactors.Controls)
                    {
                        if (
                            node.Attributes["factor2"].Value.Contains(
                                FactorHelper.Get(((ModelFactor)c.Tag).IDFactor).Name + ";"))
                        {
                            c.Checked = true;
                        }
                    }
                }
                int index = -1;
                if (lbSavedReportsCharting.SelectedItem != null)
                    index = lbSavedReportsCharting.SelectedIndex;
                if (index != -1)
                    lbSavedReportsCharting.SelectedIndex = index;

                if (type == 1)
                {
                    SetColorScheme();

                    ConfigureShapesVisibility();

                    LoadMarkers();

                    AddMarkersToMarkersOverlay();

                    GetCumulativeFactors();

                    SetMarkersVisibility();

                    RefreshMap();
                }
                else
                {
                    GenerateChart();
                }
                LoadingForm.Fadeout();
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("LoadedCorrectlySettings"));
            }
            catch (Exception)
            {

            }
            finally
            {
                tvMapRegions.AfterCheck += tvMapRegions_AfterCheck;
            }

        }

        private void btnDeleteMapAndChartSettings_Click(object sender, EventArgs e)
        {
            int type = int.Parse(((Button)sender).Tag.ToString());

            if (type == 1)
            {
                if (lbSavedReports.SelectedItem != null)
                {
                    if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ReportDelete"), CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo) ==
                        CustomMessageBoxReturnValue.Ok)
                    {
                        ReportHelper.Delete((Report)lbSavedReports.SelectedItem);
                    }
                }
            }
            else
            {
                if (lbSavedReportsCharting.SelectedItem != null)
                {
                    if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ReportDelete"), CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo) ==
                        CustomMessageBoxReturnValue.Ok)
                    {
                        ReportHelper.Delete((Report)lbSavedReportsCharting.SelectedItem);
                    }
                }
            }
            LoadReports(ERMTSession.Instance.CurrentModel);
        }

        private void lbSavedReportsCharting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSavedReportsCharting.SelectedItem != null)
            {
                btnLoadChartSettings.Enabled = true;
                btnDeleteChartSettings.Enabled = true;
                txtChartSettingsName.Text = ((Report)lbSavedReportsCharting.SelectedItem).Name;
            }
        }

        private void btnMapAllAvailableDates_Click(object sender, EventArgs e)
        {
            LoadMinAndMaxDates();
            LoadMarkers();
            AddMarkersToMarkersOverlay();

            ClearCumulativeFactors();
            GetCumulativeFactors();

            RefreshMarkers();
            RefreshMap();
        }

        private void cmRegions_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tvMapRegions.AfterCheck -= tvMapRegions_AfterCheck;
            tvChartRegions.AfterCheck -= tvChartRegions_AfterCheck;
            if (((ContextMenuStrip)sender).Items[0].Selected)
            {
                //select all child regions
                CheckChildsDraw(_lastTreeNode, true);
                ((ContextMenuStrip)sender).Visible = false;
            }
            else if (((ContextMenuStrip)sender).Items[1].Selected)
            {
                //select all regions on this level.
                //CheckRegionOnThisLevel(_lastTreeNode, true);
                //JPF: changed the method for a better one.
                CheckNodesInLevel(_lastTreeNode.Level,
                    tcRiskMapping.SelectedTab.Name.ToLower() == "tpmap" ? tvMapRegions.Nodes[0] : tvChartRegions.Nodes[0],
                    true);

                ((ContextMenuStrip)sender).Visible = false;
            }
            else if (((ContextMenuStrip)sender).Items[3].Selected)
            {
                //deselect all child regions
                CheckChildsDraw(_lastTreeNode, false);
                ((ContextMenuStrip)sender).Visible = false;
            }
            else
            {
                //deselect all regions on this level.
                //CheckRegionOnThisLevel(_lastTreeNode, false);
                CheckNodesInLevel(_lastTreeNode.Level,
                    tcRiskMapping.SelectedTab.Name.ToLower() == "tpmap" ? tvMapRegions.Nodes[0] : tvChartRegions.Nodes[0],
                    false);
                ((ContextMenuStrip)sender).Visible = false;
            }
            if (_lastTreeNode.TreeView == tvSelectRegionsTableByFactor)
            {
                tvSelectFactorTableByFactor_NodeMouseClick(sender, new TreeNodeMouseClickEventArgs(tvSelectFactorTableByFactor.SelectedNode, MouseButtons.Left, 1, 0, 0));
            }
            tvMapRegions.AfterCheck += tvMapRegions_AfterCheck;
            tvChartRegions.AfterCheck += tvChartRegions_AfterCheck;

            if (tcRiskMapping.SelectedTab == tpMap)
            {
                RefreshMap();
            }
            else
            {
                GenerateChart();
            }

        }

        private void cmRegions_Opening(object sender, CancelEventArgs e)
        {
            if (_lastTreeNode != null)
            {
                tsmiSelectAllChildRegions.Enabled = true;
                tsmiDeselectAllChildRegions.Enabled = true;
                //tsmiSelectAllChildRegions.Text = "Select all child regions";
                //tsmiDeselectAllChildRegions.Text = "Deselect all child regions";
            }
        }

        private void btnChartAllAvailableDates_Click(object sender, EventArgs e)
        {
            LoadMinAndMaxDates();
            GenerateChart();
        }

        private void tvChartRegions_AfterCheck(object sender, TreeViewEventArgs e)
        {
            GenerateChart();
        }

        private void tsmiChartShowGrid_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
            GenerateChart();
        }

        private void cmTableByFactorRegions_Opening(object sender, CancelEventArgs e)
        {
            if (_lastTreeNode != null)
            {
                tsmiTBFSelectAllChildRegions.Enabled = true;
                tsmiTBFDeselectAllChildRegions.Enabled = true;
                //tsmiTBFSelectAllChildRegions.Text = "Select all child regions";
                //tsmiTBFDeselectAllChildRegions.Text = "Deselect all child regions";
            }
        }

        private void cmTableByFactorRegions_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (((ContextMenuStrip)sender).Items[0].Selected)
            {
                //select all child regions
                CheckChildsDraw(_lastTreeNode, true);
                ((ContextMenuStrip)sender).Visible = false;
            }
            else if (((ContextMenuStrip)sender).Items[1].Selected)
            {
                //select all regions on this level
                CheckNodesInLevel(_lastTreeNode.Level, tvSelectRegionsTableByFactor.Nodes[0], true);
                //CheckRegionOnThisLevel(_lastTreeNode, true);
                ((ContextMenuStrip)sender).Visible = false;
            }
            else if (((ContextMenuStrip)sender).Items[3].Selected)
            {
                //deselect all child regions
                CheckChildsDraw(_lastTreeNode, false);
                ((ContextMenuStrip)sender).Visible = false;
            }
            else
            {
                //deselect all regions on this level
                CheckNodesInLevel(_lastTreeNode.Level, tvSelectRegionsTableByFactor.Nodes[0], false);
                ((ContextMenuStrip)sender).Visible = false;
            }
            if (_lastTreeNode.TreeView == tvSelectRegionsTableByFactor)
            {
                tvSelectFactorTableByFactor_NodeMouseClick(sender, new TreeNodeMouseClickEventArgs(tvSelectFactorTableByFactor.SelectedNode, MouseButtons.Left, 1, 0, 0));
            }

            if (tcRiskMapping.SelectedTab == tpMap)
            {
                RefreshMap();
                if (tsbMarkers.Checked)
                {
                    RefreshMarkers();
                }
            }
            else
            {
                GenerateChart();
            }
        }

        private void lblMapSettingsSection_Click(object sender, EventArgs e)
        {
            List<int> rowSizes = new List<int> { 41, 115, 180, 250, 120, 180, 200 };
            int rowIndex = Convert.ToInt32(((Label)sender).Tag);

            int newHeight = tlpScroll.RowStyles[rowIndex].Height == 30 ? rowSizes[rowIndex] : 30;
            tlpScroll.RowStyles[rowIndex].Height = newHeight;

            int auxCalc = 0;
            for (int i = 0; i < rowSizes.Count; i++)
            {
                auxCalc += Convert.ToInt32(tlpScroll.RowStyles[i].Height);
            }

            tlpScroll.Height = auxCalc + 30;
        }

        private void lblChartSettingsSection_Click(object sender, EventArgs e)
        {
            List<int> rowSizes = new List<int> { 41, 115, 115, 180, 250, 155, 200 };
            int rowIndex = Convert.ToInt32(((Label)sender).Tag);

            int newHeight = tlpChartScroll.RowStyles[rowIndex].Height == 30 ? rowSizes[rowIndex] : 30;
            tlpChartScroll.RowStyles[rowIndex].Height = newHeight;

            int auxCalc = 0;
            for (int i = 0; i < rowSizes.Count; i++)
            {
                auxCalc += Convert.ToInt32(tlpChartScroll.RowStyles[i].Height);
            }

            tlpChartScroll.Height = auxCalc + 30;
        }

        private void tsmiChartShowLegends_Click(object sender, EventArgs e)
        {
            tsmiChartShowLegends.Checked = !tsmiChartShowLegends.Checked;
            _chartLegend.Visible = tsmiChartShowLegends.Checked;
        }

        private void dtpChartDate_ValueChanged(object sender, EventArgs e)
        {
            if (!_riskMappingControlFirstCharge)
            {
                GenerateChart();
            }
        }

        #region Print

        public override void Print()
        {
            switch (tcRiskMapping.SelectedIndex)
            {
                case 0:
                    {
                        //map
                        PrintDocument printDocument = new PrintDocument();
                        printDocument.PrintPage += (sender, args) =>
                        {
                            Bitmap auxBitmap = winformsMap1.GetBitmap(winformsMap1.Width, winformsMap1.Height);
                            Image i = auxBitmap;
                            Rectangle m = args.MarginBounds;

                            if ((double)i.Width / (double)i.Height > (double)m.Width / (double)m.Height) // image is wider
                            {
                                m.Height = (int)((double)i.Height / (double)i.Width * (double)m.Width);
                            }
                            else
                            {
                                m.Width = (int)((double)i.Width / (double)i.Height * (double)m.Height);
                            }
                            args.Graphics.DrawImage(i, m);
                        };

                        PageSetupDialog psd = new PageSetupDialog
                        {
                            PageSettings = new PageSettings(),
                            PrinterSettings = new PrinterSettings()
                        };

                        if (psd.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }

                        printDocument.DefaultPageSettings = psd.PageSettings;

                        PrintDialog printDialog = new PrintDialog();
                        printDialog.Document = printDocument;

                        if (printDialog.ShowDialog() == DialogResult.OK)
                        {
                            printDocument.Print();
                        }
                        break;
                    }
                case 1:
                    {
                        //chart
                        chart1.Printing.PageSetup();
                        chart1.Printing.Print(true);
                        break;
                    }
            }
        }
        #endregion

        #region dgvTableByFactor Context Menu
        private void tsmiTableByFactor_Delete_Click(object sender, EventArgs e)
        {
            if (dgvTableByFactor.SelectedRows == null)
            {
                ((ToolStripMenuItem)sender).Visible = false;
            }
            else
            {
                if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("DeleteTableRowConfirm"),
                    CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo)
                    == CustomMessageBoxReturnValue.Ok)
                {
                    foreach (DataGridViewRow r in dgvTableByFactor.SelectedRows)
                    {
                        foreach (DataGridViewCell c in r.Cells)
                        {
                            if (c.ColumnIndex == 0) continue;
                            if ((ModelFactorData)c.Tag == null) continue;
                            ModelFactorDataHelper.Delete(((ModelFactorData)c.Tag));
                        }
                        r.Visible = false;
                    }
                }
            }
        }

        private void tsmiTableByFactor_Add_Click(object sender, EventArgs e)
        {
            Model model = ERMTSession.Instance.CurrentModel;
            List<ModelFactor> modelFactors = ModelFactorHelper.GetByModel(model);
            List<ModelFactorData> modelFactorsData = new List<ModelFactorData>();
            foreach (var mFactor in modelFactors)
            {
                modelFactorsData = ModelFactorDataHelper.GetByModelFactor(mFactor);
            }

            List<string> dgvRow = new List<string>();
            DateTime date =
                ModelFactorDataHelper.GetAvailableDateForPastedDataByModelFactor(
                    ((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDModelFactor);
            dgvRow.Add(date.ToShortDateString());

            for (int col = 1; col < modelFactorsData.Count; col++)
            {
                dgvRow.Add("N/D");
            }
            dgvTableByFactor.Rows.Add(dgvRow.ToArray());
            ModelFactorData m = ModelFactorDataHelper.GetNew();
            m.Date = DateTime.Now;
            m.IDModelFactor = ((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDModelFactor;
            dgvTableByFactor.Rows[dgvTableByFactor.Rows.Count - 1].Cells[0].Tag = m;
        }

        private void tsmiTableByFactor_Copy_Click(object sender, EventArgs e)
        {
            String csv = "";
            foreach (DataGridViewRow v in dgvTableByFactor.SelectedRows)
            {
                foreach (DataGridViewCell f in v.Cells)
                {
                    if (f.Visible)
                        csv += (f.FormattedValue is string ? f.FormattedValue.ToString() : "") + "\t";
                }

                if (csv.Length > 0)
                {
                    //remove the last TAB character.
                    csv = csv.Substring(0, csv.Length - 1);
                }
                csv += Environment.NewLine;
            }

            if (csv.Length > 0)
            {
                csv = csv.Substring(0, csv.Length - 1);
            }

            byte[] blob = System.Text.Encoding.Unicode.GetBytes(csv);
            MemoryStream s = new MemoryStream(blob);
            DataObject data = new DataObject();
            data.SetData(DataFormats.Text, s);
            Clipboard.SetDataObject(data, true);
        }

        private void tsmiTableByFactor_Paste_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("TableByRegionPasteOverwriteWarning"),
                    CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo) !=
                CustomMessageBoxReturnValue.Ok)
            {
                return;
            }

            LoadingForm.ShowLoading();
            DateTime date = new DateTime();
            string s = Clipboard.GetText();
            string[] lines = s.Replace("\r", "").Split('\n');
            if (dgvTableByFactor.CurrentCell == null)
            {
                tsmiTableByFactor_Add_Click(new object(), new EventArgs());
                //CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("PasteOnEmptyTable"));
                //return;
            }

            int iRow = dgvTableByFactor.CurrentCell.RowIndex;
            int iCol = dgvTableByFactor.CurrentCell.ColumnIndex;
            int colCount = lines[0].Split('\t').Length;
            //this variable is used to verify if the first column is just an empty tab result of realeasing CONTROL key slightly before C.
            Boolean removeFirstColumn = false;

            if (colCount > dgvTableByFactor.Columns.Count)
            {
                if (colCount == dgvTableByFactor.Columns.Count + 1)
                {
                    //might be that the CONTROL key was released slightly before the C when copying.
                    if (DateTime.TryParse(lines[0].Split('\t')[1], GeneralHelper.GetDateFormat(), DateTimeStyles.None,
                        out date))
                    {
                        removeFirstColumn = true;
                    }
                    else
                    {
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ColumnCountNoMatch"));
                        LoadingForm.Fadeout();
                        return;
                    }
                }
                else
                {
                    CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ColumnCountNoMatch"));
                    LoadingForm.Fadeout();
                    return;
                }

            }
            if (lines.Length > 200)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("PasteUpTo200Rows"));
                LoadingForm.Fadeout();
                return;
            }

            foreach (string line in lines)
            {
                DataGridViewCell oCell;

                //Boolean useDateFromPastedData = true;
                //Boolean insertedRow = false;

                //if (iRow < dgvTableByFactor.RowCount && line.Length > 0)
                //{
                //if (iRow >= dgvTableByFactor.RowCount && line.Length > 0)
                //{
                //    tsmiTableByFactor_Add_Click(sender, EventArgs.Empty);
                //    //insertedRow = true;
                //}
                iRow = -1;
                string[] sCells = line.Split('\t');
                if (removeFirstColumn)
                {
                    sCells = sCells.Skip(1).ToArray();
                }

                for (int i = 0; i < sCells.GetLength(0); ++i)
                {
                    date = new DateTime();
                    //Chequeo que el indice de columna sea el 0 (Date) para validar fecha.
                    if (iCol == 0)
                    {
                        if (!DateTime.TryParse(sCells[0], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out date) || date < new DateTime(1900, 1, 1) ||
                            date > new DateTime(2200, 1, 1))
                        {
                            continue;
                        }

                        //We're going to check if the pasted date is already in the grid. If it is, then we'll need to get a new date to use.
                        for (int j = 0; j < dgvTableByFactor.Rows.Count; j++)
                        {
                            DateTime aux;
                            try
                            {
                                if (DateTime.TryParse(dgvTableByFactor.Rows[j].Cells[0].Value.ToString(), GeneralHelper.GetDateFormat(), DateTimeStyles.None, out aux))
                                {
                                    if (aux.Date == date.Date)
                                    {
                                        //if (date.Date == DateTime.Now.Date && insertedRow)
                                        //{
                                        //    useDateFromPastedData = true;
                                        //}
                                        //else
                                        //{
                                        //    useDateFromPastedData = false;
                                        //}
                                        iRow = j;

                                        break;
                                    }
                                }
                            }
                            catch (Exception) { }
                        }

                        if (iRow == -1)
                        {
                            //means the date does NOT exist in the current data. We need to create a new row for this info.
                            tsmiTableByFactor_Add_Click(sender, EventArgs.Empty);
                            iRow = dgvTableByFactor.Rows.Count - 1;
                        }
                    }

                    oCell = GetVisibleCellAtIndex(dgvTableByFactor.Columns, iCol, i, dgvTableByFactor.Rows[iRow]);

                    if (oCell != null)
                    {
                        //oCell = dgvTableByFactor[iCol + i, iRow];
                        if (!oCell.ReadOnly)
                        {
                            _leave = oCell.ColumnIndex == 0;
                            //if (oCell.ColumnIndex == 0)
                            //{
                            //    //DateTime auxDate = ModelFactorDataHelper.GetAvailableDateForPastedData(
                            //    //    ((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDModelFactor);
                            //    //dgvTableByFactor.Rows[oCell.RowIndex].Cells[oCell.ColumnIndex].Value = (useDateFromPastedData ? sCells[i] :
                            //    //    String.Format("{0:dd/MM/yyyy}", auxDate));
                            //}
                            //else
                            //{
                            dgvTableByFactor.Rows[oCell.RowIndex].Cells[oCell.ColumnIndex].Value = sCells[i];
                            //}

                        }
                    }
                    else
                    { break; }
                }
                //iRow++;
                //}
                //else
                //{
                //    date = new DateTime();
                //    //Este caso trata una nueva fila, solo si la primer columna pegada es datetime.
                //    string[] sCells = line.Split('\t');
                //    if (DateTime.TryParse(sCells[0], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out date) && date > new DateTime(1900, 1, 1) &&
                //                date < new DateTime(2200, 1, 1))
                //    {
                //        //Agrego una nueva fila.
                //        tsmiTableByFactor_Add_Click(sender, EventArgs.Empty);
                //        for (int i = 0; i < sCells.GetLength(0); ++i)
                //        {
                //            //Chequeo que el indice de columna sea el 0 (Date) para validar fecha.
                //            if (iCol == 0)
                //            {
                //                if (DateTime.TryParse((string)sCells[0], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out date) && date > new DateTime(1900, 1, 1) &&
                //                    date < new DateTime(2200, 1, 1))
                //                {
                //                }
                //                else
                //                {
                //                    CustomMessageBox.ShowMessage("Date not valid");
                //                    return;
                //                }
                //            }
                //            oCell = GetVisibleCellAtIndex(dgvTableByFactor.Columns, iCol, i, dgvTableByFactor.Rows[iRow]);
                //            if (oCell != null)
                //            {
                //                //oCell = dgvTableByFactor[iCol + i, iRow];
                //                if (!oCell.ReadOnly)
                //                {
                //                    dgvTableByFactor.Rows[oCell.RowIndex].Cells[oCell.ColumnIndex].Value = sCells[i];
                //                }
                //            }
                //            else
                //            {
                //                break;
                //            }
                //        }
                //    }
                //    iRow++;
                //}
            }
            LoadingForm.Fadeout();
            LoadTableByFactor();
        }

        #endregion

        private void tsmiTableByRegion_Add_Click(object sender, EventArgs e)
        {
            Model model = ERMTSession.Instance.CurrentModel;
            List<ModelFactor> modelFactors = ModelFactorHelper.GetByModel(model);
            var date = DateTime.Today;
            List<string> dgvRow = new List<string> { date.ToString("d", GeneralHelper.GetDateFormat()) };
            for (int col = 1; col <= modelFactors.Count; col++)
            {
                dgvRow.Add("N/D");
            }
            dgvTableByRegion.Rows.Add(dgvRow.ToArray());
        }

        private void tsmiTableByRegion_Delete_Click(object sender, EventArgs e)
        {
            if (dgvTableByRegion.SelectedRows.Count == 0)
            {
                return;
            }

            if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("DeleteTableRowConfirm")) == CustomMessageBoxReturnValue.Ok)
            {
                foreach (DataGridViewRow r in dgvTableByRegion.SelectedRows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.ColumnIndex == 0) continue;
                        if ((ModelFactorData)c.Tag == null) continue;
                        ModelFactorDataHelper.Delete(((ModelFactorData)c.Tag));
                    }
                    r.Visible = false;
                }
            }
        }

        private void tsmiTableByRegion_Copy_Click(object sender, EventArgs e)
        {
            String csv = "";
            foreach (DataGridViewRow v in dgvTableByRegion.SelectedRows)
            {
                foreach (DataGridViewCell f in v.Cells)
                {
                    csv += (f.FormattedValue is string ? f.FormattedValue.ToString() : "") + "\t";
                }

                if (csv.Length > 0)
                {
                    csv = csv.Substring(0, csv.Length - 1);
                }

                csv += Environment.NewLine;
            }
            //if (csv.Length > 0)
            //    csv = csv.Substring(0, csv.Length - 3);
            byte[] blob = System.Text.Encoding.Unicode.GetBytes(csv);
            MemoryStream s = new MemoryStream(blob);
            DataObject data = new DataObject();
            data.SetData(DataFormats.Text, s);
            Clipboard.SetDataObject(data, true);
        }

        private void tsmiTableByRegion_Paste_Click(object sender, EventArgs e)
        {
            if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("TableByRegionPasteOverwriteWarning"),
                   CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo) !=
               CustomMessageBoxReturnValue.Ok)
            {
                return;
            }

            if (dgvTableByRegion.Rows.Count == 0)
            {
                tsmiTableByRegion_Add_Click(new object(), new EventArgs());
            }

            DateTime date;
            string s = Clipboard.GetText();
            string[] lines = s.Replace("\r", "").Split('\n');
            int iFail = 0;
            int iRow = dgvTableByRegion.CurrentCell.RowIndex;
            int iCol = dgvTableByRegion.CurrentCell.ColumnIndex;

            int colCount = lines[0].Split('\t').Length;
            if (colCount > dgvTableByRegion.Columns.Count)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("ColumnCountNoMatch"));
                return;
            }
            if (lines.Length > 200)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("PasteUpTo200Rows"));
                return;
            }

            foreach (string line in lines)
            {
                DataGridViewCell oCell;
                iRow = -1;
                //if (iRow < dgvTableByRegion.RowCount && line.Length > 0)
                //{
                string[] sCells = line.Split('\t');
                for (int i = 0; i < sCells.GetLength(0); ++i)
                {
                    date = new DateTime();
                    //Chequeo que el indice de columna sea el 0 (Date) para validar fecha.
                    if (iCol == 0)
                    {
                        if (!DateTime.TryParse(sCells[0], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out date) || date < new DateTime(1900, 1, 1) ||
                            date > new DateTime(2200, 1, 1))
                        {
                            continue;
                        }

                        //We're going to check if the pasted date is already in the grid. If it is, then we'll need to get a new date to use.
                        for (int j = 0; j < dgvTableByRegion.Rows.Count; j++)
                        {
                            DateTime aux;
                            try
                            {
                                if (DateTime.TryParse(dgvTableByRegion.Rows[j].Cells[0].Value.ToString(), GeneralHelper.GetDateFormat(), DateTimeStyles.None, out aux))
                                {
                                    if (aux.Date == date.Date)
                                    {
                                        //if (date.Date == DateTime.Now.Date && insertedRow)
                                        //{
                                        //    useDateFromPastedData = true;
                                        //}
                                        //else
                                        //{
                                        //    useDateFromPastedData = false;
                                        //}
                                        iRow = j;

                                        break;
                                    }
                                }
                            }
                            catch (Exception) { }
                        }

                        if (iRow == -1)
                        {
                            //means the date does NOT exist in the current data. We need to create a new row for this info.
                            tsmiTableByRegion_Add_Click(new object(), new EventArgs());
                            iRow = dgvTableByRegion.Rows.Count - 1;
                        }
                    }

                    oCell = GetVisibleCellAtIndex(dgvTableByRegion.Columns, iCol, i, dgvTableByRegion.Rows[iRow]);

                    if (oCell != null)
                    {
                        //oCell = dgvTableByFactor[iCol + i, iRow];
                        if (!oCell.ReadOnly)
                        {
                            _leave = oCell.ColumnIndex == 0;
                            //if (oCell.ColumnIndex == 0)
                            //{
                            //    //DateTime auxDate = ModelFactorDataHelper.GetAvailableDateForPastedData(
                            //    //    ((ModelFactor)tvSelectFactorTableByFactor.SelectedNode.Tag).IDModelFactor);
                            //    //dgvTableByFactor.Rows[oCell.RowIndex].Cells[oCell.ColumnIndex].Value = (useDateFromPastedData ? sCells[i] :
                            //    //    String.Format("{0:dd/MM/yyyy}", auxDate));
                            //}
                            //else
                            //{
                            dgvTableByRegion.Rows[oCell.RowIndex].Cells[oCell.ColumnIndex].Value = sCells[i];
                            //}

                        }
                    }
                    else
                    { break; }


                    //if (iCol + i < this.dgvTableByRegion.ColumnCount)
                    //{
                    //    oCell = dgvTableByRegion[iCol + i, iRow];
                    //    if (!oCell.ReadOnly)
                    //    {
                    //        oCell.Value = sCells[i];
                    //    }
                    //}
                    //else
                    //{ break; }
                }
                //iRow++;
                //}
                //else
                //{
                //    date = new DateTime();
                //    //Este caso trata una nueva fila, solo si la primer columna pegada es datetime.
                //    string[] sCells = line.Split('\t');
                //    if (DateTime.TryParse((string)sCells[0], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out date) && date > new DateTime(1900, 1, 1) &&
                //                date < new DateTime(2200, 1, 1))
                //    {
                //        //Agrego una nueva fila.
                //        tsmiTableByRegion_Add_Click(sender, EventArgs.Empty);
                //        for (int i = 0; i < sCells.GetLength(0); ++i)
                //        {
                //            //Chequeo que el indice de columna sea el 0 (Date) para validar fecha.
                //            if (iCol == 0)
                //            {
                //                if (DateTime.TryParse((string)sCells[0], GeneralHelper.GetDateFormat(), DateTimeStyles.None, out date) && date > new DateTime(1900, 1, 1) &&
                //                    date < new DateTime(2200, 1, 1))
                //                {
                //                }
                //                else
                //                {
                //                    MessageBox.Show("Date not valid");
                //                    return;
                //                }
                //            }
                //            oCell = GetVisibleCellAtIndex(dgvTableByFactor.Columns, iCol, i, dgvTableByFactor.Rows[iRow]);
                //            if (oCell != null)
                //            {
                //                //oCell = dgvTableByFactor[iCol + i, iRow];
                //                if (!oCell.ReadOnly)
                //                {
                //                    dgvTableByFactor.Rows[oCell.RowIndex].Cells[oCell.ColumnIndex].Value = sCells[i];
                //                    dgvTableByFactor_CellEndEdit(dgvTableByFactor, new DataGridViewCellEventArgs(oCell.ColumnIndex, oCell.RowIndex));
                //                }
                //            }
                //            else
                //            { break; }
                //            //if (iCol + i < this.dgvTableByRegion.ColumnCount)
                //            //{
                //            //    oCell = dgvTableByRegion[iCol + i, iRow];
                //            //    if (!oCell.ReadOnly)
                //            //    {
                //            //        oCell.Value = sCells[i];
                //            //    }
                //            //}
                //            //else
                //            //{ break; }
                //        }
                //    }
                //    iRow++;
                //}

            }
            LoadTableByRegion();
        }

        private void tsmiStaticMarker_Add_Click(object sender, EventArgs e)
        {
            btnAddMarker_Click(new object(), new EventArgs());
        }

        private void dgvTableByFactor_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTableByFactor_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}