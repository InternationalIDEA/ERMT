using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Idea.Facade;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using ThinkGeoMarker = ThinkGeo.MapSuite.DesktopEdition.Marker;

namespace Idea.ERMT.UserControls
{
    public partial class RiskMappingFullScreen : Form
    {
        private List<ShapeFileFeatureLayer> _modelLayers = new List<ShapeFileFeatureLayer>(); 
        public LayerOverlay _layerOverlay;
        private SimpleMarkerOverlay _markersOverlay;
        private SimpleMarkerOverlay _cumulativeFactorsOverlay;
        private LayerOverlay _pathAndPOILayerOverlay;

        public event EventHandler OnClose;

        public WinformsMap Map
        {
            set { winformsMap1 = value; }
            get { return winformsMap1;}
        }

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        public RiskMappingFullScreen()
        {
            InitializeComponent();
            ConfigureMap();
        }

        public void SetupMap(List<ShapeFileFeatureLayer> modelLayers, SimpleMarkerOverlay markersOverlay, SimpleMarkerOverlay cumulativeFactorsOverlay,
            LayerOverlay pathAndPOILayerOverlay)
        {
            _modelLayers = modelLayers;
            foreach (ShapeFileFeatureLayer shapeFileFeatureLayer in _modelLayers)
            {
                _layerOverlay.Layers.Add(shapeFileFeatureLayer);
            }

            foreach (ThinkGeoMarker marker in markersOverlay.Markers)
            {
                _markersOverlay.Markers.Add(MapHelper.CopyMarker(marker));
            }

            foreach (ThinkGeoMarker marker in cumulativeFactorsOverlay.Markers)
            {
                _cumulativeFactorsOverlay.Markers.Add(MapHelper.CopyMarker(marker));
            }

            foreach (Layer layer in pathAndPOILayerOverlay.Layers)
            {
                _pathAndPOILayerOverlay.Layers.Add(layer);
            }

            SetCurrentExtent();

            winformsMap1.Refresh();
        }

        private void winformsMap1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F11:
                case Keys.Escape:
                {
                    int hWnd = FindWindow("Shell_TrayWnd", "");
                    ShowWindow(hWnd, SW_SHOW);
                    if (OnClose != null)
                    {
                        OnClose(sender, EventArgs.Empty);
                    }
                    Close();
                    break;
                }
            }
        }
        
        public void SetCurrentExtent()
        {
            try
            {
                ShapeFileFeatureLayer aux = MapHelper.GetRegionFeatureLayer(ERMTSession.Instance.CurrentModelMainRegion, _modelLayers);

                if (aux != null)
                {
                    aux.Open();
                    winformsMap1.CurrentExtent = aux.GetBoundingBox();
                    aux.Close();
                }
            }
            catch
            { }
        }

        public void RefreshMap()
        {
            winformsMap1.Refresh();
        }


        private void ConfigureMap()
        {
            winformsMap1.Overlays.Clear();
            _layerOverlay = new LayerOverlay {Name = "main", IsBase = true};
            _markersOverlay = MapHelper.GetMarkersOverlay(winformsMap1);
            _pathAndPOILayerOverlay = new LayerOverlay { Name = "pathandpoi" };
            _cumulativeFactorsOverlay = MapHelper.GetCumulativeFactorsOverlay(winformsMap1);

            winformsMap1.Overlays.Add(_layerOverlay);
            winformsMap1.Overlays.Add(_markersOverlay);
            winformsMap1.Overlays.Add(_cumulativeFactorsOverlay);
            winformsMap1.Overlays.Add(_pathAndPOILayerOverlay);
        }
    }
}
