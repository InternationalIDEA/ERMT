using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Facade;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;

namespace Idea.ERMT.UserControls
{
    public partial class EditRegion : ERMTUserControl
    {
        private LayerOverlay _layerOverlay;
        private Region _regionData;
        private int? _idParent = null;
        private int _currentLayerCount = 0;
        private List<Region> _allRegions;
        private List<Region> _currentRegions;
        private TreeNode _selectedNode;
        private Boolean _screenFirstLoad = true;
        private Boolean _setInitialExtent = true;


        private string _columnName;

        public Region RegionData
        {
            get
            {
                if (_regionData == null)
                {
                    _regionData = RegionHelper.GetNew();
                    _regionData.IDRegion = 1;
                }

                return _regionData;
            }
            set
            {
                _regionData = value;
                //txtParentName.Text = _regionData.RegionName;
            }
        }

        private Region NewRegionData
        {
            get
            {
                Region aux = RegionHelper.GetNew();
                //aux.RegionName = txtRegionName.Text;
                aux.IDParent = _regionData.IDRegion;
                return aux;
            }
        }

        public EditRegion()
        {
            InitializeComponent();
            LoadRegions();
            //SizeChanged+=EditRegion_SizeChanged;
            winformsMap1.SizeChanged += EditRegion_SizeChanged;
            _screenFirstLoad = false;
        }

        public override void ShowTitle()
        {
            ViewManager.ShowTitle(ResourceHelper.GetResourceText("EditRegionTitle"));
        }

        void EditRegion_SizeChanged(object sender, EventArgs e)
        {
            if (_setInitialExtent)
            {
                SetCurrentExtent(_allRegions[0]);
                _setInitialExtent = false;
            }
        }

        public void LoadRegions()
        {
            //EL primer nodo que se muestra es el seleccionado para el modelo.
            tvRegions.Nodes.Clear();
            _allRegions = RegionHelper.GetAll().OrderBy(r => r.RegionLevel).ToList();
            TreeNode node = new TreeNode { Text = _allRegions[0].RegionName, Name = _allRegions[0].IDRegion.ToString(), Tag = _allRegions[0] };
            tvRegions.Nodes.Add(node);
            node.Expand();
            AddChildRegions(tvRegions.Nodes[0], _allRegions);
            if (tvRegions.SelectedNode != null)
            {
                tvRegions.SelectedNode.EnsureVisible();
                tvRegions.SelectedNode.Expand();
            }

            tvRegions_NodeMouseClick(tvRegions, new TreeNodeMouseClickEventArgs(node, MouseButtons.Left, 1, 0, 0));

        }

        private void AddChildRegions(TreeNode treeNode, List<Region> regions)
        {
            foreach (Region r in regions)
            {
                if (r.IDParent.ToString() == treeNode.Name)
                {
                    TreeNode t = new TreeNode { Text = r.RegionName, Name = r.IDRegion.ToString(), Tag = r };
                    treeNode.Nodes.Add(t);
                    if (_regionData != null && _regionData.IDRegion == r.IDRegion)
                    {
                        tvRegions.SelectedNode = t;
                    }
                    AddChildRegions(t, regions);
                }
            }
        }

        private void tvRegions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvRegions.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Left)
            {
                int idRegion = Convert.ToInt32(e.Node.Name);
                _regionData = RegionHelper.Get(idRegion);
                ShowShapeChilds(_regionData);
            }
            else
            {
                _selectedNode = e.Node;
                tvRegions.ContextMenuStrip = cmEditRegions;
                cmEditRegions.Show(tvRegions, e.X, e.Y);
            }
        }

        private void tsmiEditRegionAddChildRegion_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Shape Files (*.shp)|*.shp" };
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileInfo shapeFileInfo = new FileInfo(openFileDialog.FileName);
                try
                {
                    int count = (LayerOverlay)winformsMap1.Overlays[0] != null ? ((LayerOverlay)winformsMap1.Overlays[0]).Layers.Count : 0;
                    if (!File.Exists(shapeFileInfo.ToString().Replace(".shp", ".dbf")) || !File.Exists(shapeFileInfo.ToString().Replace(".shp", ".shx")))
                    {
                        //if there's no DBF / SHX file, can't use this .shp.
                        //http://thinkgeo.com/forums/MapSuite/tabid/143/aft/2947/Default.aspx
                        //1) In real life, how those files (.shp .shx .dbf) are generated ? 
                        //1. These files are automatically created upon creation of a shapefile, using the CreateShapeFile method of Map Suite or another shapefile creation application. These three files are required for a shapefile to function as dictated by ESRI shapefile standard.
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("EditRegionShapeFileMissingFiles"));
                    }
                    else
                    {
                        ShapeFileFeatureLayer newRegion = MapHelper.GetRegionFeatureLayer(shapeFileInfo);
                        DataTable table = GetDataTable(newRegion);


                        PickColumnForm pck = new PickColumnForm(table);
                        if (pck.ShowDialog() == DialogResult.OK)
                        {
                            LoadingForm.ShowLoading();
                            _columnName = pck.SelectedColumn;
                            string parentColumnName = pck.SelectedParentColumn;
                            try
                            {
                                int regionCount = 0;
                                List<Region> siblingRegions = null;
                                String newRegionFileName = string.Empty;
                                Region parent = null;
                                foreach (DataRow row in table.Rows)
                                {
                                    Boolean saveRegion = false;
                                    Region region = NewRegionData;
                                    region.RegionName = row[_columnName].ToString();
                                    if (parentColumnName != "No parent column")
                                    //If la segunda columna esta seleccionada parentNameColumnIndex!=-1
                                    {
                                        //Quien es el pais?
                                        int idCountry = GetCountry(RegionHelper.Get(region.IDParent.Value));
                                        if (region.IDParent != null)
                                        {
                                            Region parentRegion = RegionHelper.Get(region.IDParent.Value);
                                            if (parentRegion.IDParent != null)
                                            {
                                                if (idCountry == _regionData.IDRegion)
                                                {
                                                    region.IDParent = _regionData.IDRegion;
                                                    saveRegion = true;
                                                }
                                                else
                                                {
                                                    if (siblingRegions == null)
                                                    {
                                                        siblingRegions = RegionHelper.GetChildsAtLevel(idCountry,
                                                            _regionData.RegionLevel);
                                                    }

                                                    foreach (Region r in siblingRegions) //Hermanos
                                                    {
                                                        if (r.RegionName != (string)row[parentColumnName]) continue;
                                                        region.IDParent = r.IDRegion;
                                                        saveRegion = true;
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                region.IDParent = parentRegion.IDRegion;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        saveRegion = true;
                                    }

                                    parent = RegionHelper.Get(region.IDParent.Value);
                                    if (saveRegion)
                                    {

                                        region.RegionLevel = parent.RegionLevel + 1;
                                        if (newRegionFileName == string.Empty)
                                        {
                                            newRegionFileName = SaveShapefiles(shapeFileInfo, region);
                                        }
                                        region.ShapeFileName = newRegionFileName;
                                        if (table.Rows.Count == 1)
                                        {
                                            //it's the only region in the shapefile. So, no need to specify an index
                                            region.ShapeFileIndex = null;
                                        }
                                        else
                                        {
                                            //it's a shapefile that contains more than 1 regions. Index is needed.
                                            region.ShapeFileIndex = regionCount;
                                        }

                                        RegionHelper.Save(region);
                                        regionCount++;
                                    }
                                }
                                //Reload  map and tree
                                LoadRegions();
                                if (parent != null)
                                {
                                    ShowShapeChilds(parent);
                                }
                                LoadingForm.Fadeout();
                                CustomMessageBox.ShowMessage(regionCount + " " + ResourceHelper.GetResourceText("NewRegionsHaveBeenImported"));
                            }
                            catch (Exception ex)
                            {
                                LogHelper.LogError(ex);
                                throw;
                            }
                            finally
                            {
                                LoadingForm.Fadeout();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Shape was not valid
                }
                finally
                {

                }
            }
        }

        /// <summary>
        /// Clears map and loads all region first level child regions. 
        /// </summary>
        public void ShowShapeChilds(Region selectedRegion)
        {
            _currentRegions = new List<Region> { selectedRegion };

            // Create a new Layer Overlay to hold the layer we just created
            _layerOverlay = new LayerOverlay();

            //Get the child regions of the selected region.
            List<Region> childRegions = RegionHelper.GetChilds(selectedRegion.IDRegion);

            _currentRegions.AddRange(childRegions);

            List<ShapeFileFeatureLayer> layers = MapHelper.GetFeatureLayers(selectedRegion);

            foreach (ShapeFileFeatureLayer shapeFileFeatureLayer in layers)
            {
                shapeFileFeatureLayer.FeatureIdsToExclude.Clear();
                shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
                shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                _layerOverlay.Layers.Add(shapeFileFeatureLayer);
            }

            winformsMap1.Overlays.Clear();

            // We need to add the layer overlay to Map.
            winformsMap1.Overlays.Add(_layerOverlay);

            if (!_screenFirstLoad)
            {
                SetCurrentExtent(selectedRegion);
            }


            // We now need to call the Refresh() method of the Map control so that the Map can redraw based on the data that has been provided.
            winformsMap1.Refresh();
        }

        private void SetCurrentExtent(Region selectedRegion)
        {
            if (_layerOverlay == null)
            {
                return;
            }

            Layer aux = _layerOverlay.Layers.FirstOrDefault(l => l.Name == selectedRegion.IDRegion.ToString());

            if (aux != null)
            {
                aux.Open();
                winformsMap1.CurrentExtent = aux.GetBoundingBox();
                aux.Close();
                winformsMap1.Refresh();
            }
        }

        /// <summary>
        /// Saves the 3 basic files: .shp, .dbf and .idx
        /// </summary>
        /// <param name="shapefileInfo"></param>
        /// <param name="region"></param>
        /// <param name="shapefileERMTType"></param>
        private String SaveShapefiles(FileInfo shapefileInfo, Region region, ShapeFileERMTType shapefileERMTType = ShapeFileERMTType.Map)
        {
            try
            {
                //check if we need to create the index file for the .shp
                MapHelper.CheckIndexFile(shapefileInfo);

                FileInfo indexFileInfo = new FileInfo(shapefileInfo.FullName.Replace(".shp", ".idx"));
                FileInfo dbfFileInfo = new FileInfo(shapefileInfo.FullName.Replace(".shp", ".dbf"));
                FileInfo shxFileInfo = new FileInfo(shapefileInfo.FullName.Replace(".shp", ".shx"));

                //now, copy files to the server.
                string shapefileName = DocumentHelper.SaveShapefile(shapefileInfo, region, shapefileERMTType);

                DocumentHelper.SaveShapefile(dbfFileInfo, region, shapefileERMTType);

                //DocumentHelper.SaveShapefile(indexFileInfo, newRegion);

                DocumentHelper.SaveShapefile(shxFileInfo, region, shapefileERMTType);

                return region.RegionLevel + "\\" + shapefileName;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                throw;
            }

        }

        private int GetCountry(Region region)
        {
            while (region.RegionLevel > 2)
            {
                region = RegionHelper.Get(region.IDParent.Value);
            }
            return region.IDRegion;
        }

        private DataTable GetDataTable(ShapeFileFeatureLayer newRegion)
        {
            newRegion.Open();
            Collection<Feature> allFeatures = newRegion.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns, 0);

            List<String> allColumnNames = new List<string>();

            if (allFeatures.Count > 0)
            {
                allColumnNames.AddRange(allFeatures[0].ColumnValues.Select(keyValuePair => keyValuePair.Key));
            }

            newRegion.Close();

            return MapEngine.LoadDataTable(allFeatures, allColumnNames);
        }

        private void tsmiEditRegionAddRoadsAndPOI_Click(object sender, EventArgs e)
        {
            ShapeFileERMTType shapeFileERMTType = ((ToolStripMenuItem)sender).Name == "tsmiEditRegionAddRoads"
                ? ShapeFileERMTType.Path
                : ShapeFileERMTType.POI;
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Shape Files (*.shp)|*.shp" };
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileInfo shapeFileInfo = new FileInfo(openFileDialog.FileName);
                try
                {
                    if (!File.Exists(shapeFileInfo.ToString().Replace(".shp", ".dbf")) || !File.Exists(shapeFileInfo.ToString().Replace(".shp", ".shx")))
                    {
                        //if there's no DBF / SHX file, can't use this .shp.
                        //http://thinkgeo.com/forums/MapSuite/tabid/143/aft/2947/Default.aspx
                        //1) In real life, how those files (.shp .shx .dbf) are generated ? 
                        //1. These files are automatically created upon creation of a shapefile, using the CreateShapeFile method of Map Suite or another shapefile creation application. These three files are required for a shapefile to function as dictated by ESRI shapefile standard.
                        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("EditRegionShapeFileMissingFiles"));
                        return;
                    }
                    else
                    {
                        Region selectedRegion = (Region)_selectedNode.Tag;
                        if (selectedRegion != null)
                        {

                            String newRegionFileName = SaveShapefiles(shapeFileInfo, selectedRegion, shapeFileERMTType);
                            switch (shapeFileERMTType)
                            {
                                case ShapeFileERMTType.Path:
                                    {
                                        selectedRegion.PathFileName = newRegionFileName;
                                        break;
                                    }
                                case ShapeFileERMTType.POI:
                                    {
                                        selectedRegion.POIFileName = newRegionFileName;
                                        break;
                                    }
                            }

                            RegionHelper.Save(selectedRegion);
                            CustomMessageBox.ShowMessage(shapeFileERMTType == ShapeFileERMTType.Path
                                ? ResourceHelper.GetResourceText("PathSuccessfullySaved")
                                : ResourceHelper.GetResourceText("POISuccessfullySaved"));

                        }
                    }
                }
                catch (Exception ex)
                {
                    //Shape was not valid
                }
                finally
                {

                }
            }
        }

        private void tsmiEditRegionDeleteRegion_Click(object sender, EventArgs e)
        {
            Region region = (Region)_selectedNode.Tag;

            List<Region> childRegions = RegionHelper.GetAllRelated(region.IDRegion);

            List<Model> modelsUsingSelectedRegionOrChilds = ModelHelper.GetByRegions(childRegions);

            modelsUsingSelectedRegionOrChilds.AddRange(ModelHelper.GetByRegion(region.IDRegion));

            if (modelsUsingSelectedRegionOrChilds.Count > 0)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RegionWithModels"));
                return;
            }

            if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RegionDeleteConfirm"), CustomMessageBoxMessageType.Warning,
                CustomMessageBoxButtonType.YesNo, new[] { region.RegionName }) == CustomMessageBoxReturnValue.Ok)
            {
                RegionHelper.Delete(region);
                LoadRegions();
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RegionDeleted"));
            }
        }

        //private void tsmiEditRegionDeleteAllChildRegions_Click(object sender, EventArgs e)
        //{
        //    Region region = (Region)_selectedNode.Tag;
        //    if (region.RegionLevel < 1)
        //    {
        //        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("WorldContinentNotDelete"));
        //        return;
        //    }

        //    List<Region> childRegions = RegionHelper.GetChilds(region.IDRegion);

        //    if (childRegions.Any(r => r.IDRegion == region.IDRegion))
        //    {
        //        Region regionRemove = childRegions.First(r => r.IDRegion == region.IDRegion);
        //        childRegions.Remove(regionRemove);
        //    }

        //    if (childRegions == null || childRegions.Count == 0)
        //    {
        //        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RegionHasNoChilds"));
        //        return;
        //    }

        //    if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RegionChildsDeleteConfirm"), CustomMessageBoxMessageType.Warning,
        //        CustomMessageBoxButtonType.YesNo, new[] { region.RegionName }) == CustomMessageBoxReturnValue.Ok)
        //    {
        //        LoadingForm.ShowLoading();
        //        foreach (Region childRegion in childRegions)
        //        {
        //            RegionHelper.Delete(childRegion);
        //        }
        //        LoadRegions();
        //        CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RegionChildsDeleted"));
        //        LoadingForm.Fadeout();
        //    }
        //}

        private void tsmiEditRegionChangeName_Click(object sender, EventArgs e)
        {
            Region region = (Region)_selectedNode.Tag;
            EditRegionNameForm editRegionNameForm = new EditRegionNameForm { RegionToChange = region };
            editRegionNameForm.OnRegionNameChanged += editRegionNameForm_OnRegionNameChanged;

            editRegionNameForm.ShowDialog();
        }

        void editRegionNameForm_OnRegionNameChanged(object sender, EventArgs e)
        {
            LoadRegions();
        }

        private void tsmiEditRegionDeleteAllChildRegions_Click(object sender, EventArgs e)
        {
            Region region = (Region)_selectedNode.Tag;
            if (region.RegionLevel < 1)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("WorldContinentNotDelete"));
                return;
            }

            List<Region> childRegions = RegionHelper.GetAllChilds(region.IDRegion);

            if (childRegions.Any(r => r.IDRegion == region.IDRegion))
            {
                Region regionRemove = childRegions.First(r => r.IDRegion == region.IDRegion);
                childRegions.Remove(regionRemove);
            }

            if (childRegions == null || childRegions.Count == 0)
            {
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RegionHasNoChilds"));
                return;
            }

            List<Model> childRegionModels = ModelHelper.GetByRegions(childRegions);

            if (CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RegionChildsDeleteConfirm"), CustomMessageBoxMessageType.Warning,
                CustomMessageBoxButtonType.YesNo, new[] { region.RegionName }) == CustomMessageBoxReturnValue.Ok)
            {
                
                if (childRegionModels.Count > 0)
                {
                    if (
                        CustomMessageBox.ShowMessage(
                            ResourceHelper.GetResourceText("RegionChildsWithModelsDeleteConfirm"),
                            CustomMessageBoxMessageType.Warning, CustomMessageBoxButtonType.YesNo) !=
                        CustomMessageBoxReturnValue.Ok)
                    {
                        return;
                    }
                    LoadingForm.ShowLoading();
                    foreach (Model model in childRegionModels)
                    {
                        ModelHelper.Delete(model);
                    }
                    ViewManager.LoadModelsMenu();
                }
                LoadingForm.ShowLoading();
                foreach (Region childRegion in childRegions)
                {
                    RegionHelper.Delete(childRegion);
                }
                LoadRegions();
                CustomMessageBox.ShowMessage(ResourceHelper.GetResourceText("RegionChildsDeleted"));
                LoadingForm.Fadeout();
            }
        }
    }
}

