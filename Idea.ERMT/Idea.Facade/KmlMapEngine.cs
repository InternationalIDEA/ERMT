using Idea.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using ThinkGeo.MapSuite.Core;

namespace Idea.Facade
{
    public class KmlMapEngine : MapEngine
    {
        readonly KmlGeoCanvas _kmlGeoCanvas;

        public KmlMapEngine()
        {
            _kmlGeoCanvas = new KmlGeoCanvas();
        }

        public void DrawStaticLayers(StringBuilder kmlStringBuilder, GeographyUnit geographyUnit)
        {
            LayersDrawingEventArgs layersDrawingEventArgs = new LayersDrawingEventArgs(StaticLayers, CurrentExtent, kmlStringBuilder);
            OnLayersDrawing(layersDrawingEventArgs);
            if (!layersDrawingEventArgs.Cancel)
            {
                foreach (Layer layer in StaticLayers)
                {
                    
                    LayerDrawingEventArgs layerDrawingEventArgs = new LayerDrawingEventArgs(layer, CurrentExtent, kmlStringBuilder);
                    OnLayerDrawing(layerDrawingEventArgs);
                    if (!layerDrawingEventArgs.Cancel)
                    {
                        _kmlGeoCanvas.BeginDrawing(kmlStringBuilder, CurrentExtent, geographyUnit);
                        layer.Draw(_kmlGeoCanvas, new Collection<SimpleCandidate>());
                    }
                    OnLayerDrawn(new LayerDrawnEventArgs(layer, CurrentExtent, kmlStringBuilder));
                }
            }
            OnLayersDrawn(new LayersDrawnEventArgs(StaticLayers, CurrentExtent, kmlStringBuilder));
            _kmlGeoCanvas.EndDrawing();
        }

        public StringBuilder DrawRegionNames(List<Region> regions,List<ShapeFileFeatureLayer> modelLayers)
        {
            StringBuilder aux = new StringBuilder();
            foreach (Region region in regions)
            {
                ShapeFileFeatureLayer shapeFileFeatureLayer = MapHelper.GetRegionFeatureLayer(region, modelLayers);
                PointShape pointShape = MapHelper.GetFeatureCenterPoint(shapeFileFeatureLayer, (region.ShapeFileIndex + 1 ?? 1));
                aux.Append(KmlGeoCanvas.GetRegionNameXML(region.RegionName, pointShape));
            }
            return aux;
        }

        public StringBuilder GetMarkerXML(ThinkGeo.MapSuite.DesktopEdition.Marker marker)
        {
            return KmlGeoCanvas.GetMarkerXML(marker);
        }

        public StringBuilder GetCumulativeFactorXML(ThinkGeo.MapSuite.DesktopEdition.Marker marker)
        {
            return KmlGeoCanvas.GetCumulativeFactorXML(marker);
        }
    }
}
