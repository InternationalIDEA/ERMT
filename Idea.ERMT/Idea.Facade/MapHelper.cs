using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Idea.Entities;
using Idea.Utils;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using Region = Idea.Entities.Region;
using ThinkGeoMarker = ThinkGeo.MapSuite.DesktopEdition.Marker;

namespace Idea.Facade
{
    public static class MapHelper
    {
        /// <summary>
        /// Returns a ShapeFileFeatureLayer with the world's shape file.
        /// </summary>
        /// <returns></returns>
        public static ShapeFileFeatureLayer GetWorldFeatureLayer()
        {
            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(DirectoryAndFileHelper.ClientShapefilesFolder + "\\1\\" + "world.shp")
                {
                    Name = "1"
                };
            //worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country2;
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level09;

            RebuildIndexFile(worldLayer);

            return worldLayer;
        }


        /// <summary>
        /// Returns a ShapeFileFeatureLayer with the region's shape file.
        /// </summary>
        /// <returns></returns>
        public static ShapeFileFeatureLayer GetRegionFeatureLayer(Region region)
        {
            if (String.IsNullOrEmpty(region.ShapeFileName))
            {
                return null;
            }

            string featureIDsToExclude = RegionHelper.GetFeatureIDsToExclude(region.IDRegion);

            ShapeFileFeatureLayer layer = new ShapeFileFeatureLayer(GetShapeFileFullPath(region))
            {
                Name = region.IDRegion.ToString()
            };

            if (featureIDsToExclude != null)
            {
                string[] ids = featureIDsToExclude.Split('|');
                foreach (string id in ids)
                {
                    if (id.Trim() == string.Empty)
                    {
                        continue;
                    }
                    int index = Convert.ToInt32(id);
                    //seems the featureID is actually index + 1.
                    layer.FeatureIdsToExclude.Add((index + 1).ToString());
                }
            }

            RebuildIndexFile(layer);

            return layer;
        }

        /// <summary>
        /// Returns a ShapeFileFeatureLayer with the region's path shape file.
        /// </summary>
        /// <returns></returns>
        public static ShapeFileFeatureLayer GetRegionPathLayer(Region region)
        {
            if (String.IsNullOrEmpty(region.PathFileName))
            {
                return null;
            }

            ShapeFileFeatureLayer layer = new ShapeFileFeatureLayer(GetPathShapeFileFullPath(region))
            {
                Name = region.IDRegion + "PATH"
            };

            layer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColors.GreenYellow, 1, GeoColors.GreenYellow, 0, GeoColors.GreenYellow, 0, false);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            RebuildIndexFile(layer);

            return layer;
        }

        /// <summary>
        /// Returns a ShapeFileFeatureLayer with the region's path shape file.
        /// </summary>
        /// <returns></returns>
        public static ShapeFileFeatureLayer GetRegionPOILayer(Region region)
        {
            if (String.IsNullOrEmpty(region.POIFileName))
            {
                return null;
            }

            ShapeFileFeatureLayer layer = new ShapeFileFeatureLayer(GetPOIShapeFileFullPath(region))
            {
                Name = region.IDRegion + "POI"
            };

            layer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.City2;
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            RebuildIndexFile(layer);

            return layer;
        }

        public static Overlay GetCumulativeFactorOverlay()
        {
            InMemoryFeatureLayer pointLayer = new InMemoryFeatureLayer();

            PointStyle pointStyle = new PointStyle();
            pointStyle.SymbolType = PointSymbolType.Triangle;
            pointStyle.SymbolSolidBrush = new GeoSolidBrush(GeoColor.SimpleColors.Blue);
            pointStyle.SymbolSize = 18;
            pointStyle.PointType = PointType.Symbol;


            pointLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = pointStyle;
            pointLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay pointOverlay = new LayerOverlay();
            pointOverlay.Layers.Add("PointLayer", pointLayer);

            Feature f = new Feature(25.76513565, -13.65728759, "cholo");
            Feature f2 = new Feature(28.94567764, -14.99212646, "cholo");

            pointLayer.InternalFeatures.Add(f);
            pointLayer.InternalFeatures.Add(f2);
            //pointLayer.
            return pointOverlay;
        }

        /// <summary>
        /// Returns the full path of the region's shapefile.
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string GetShapeFileFullPath(Region region)
        {
            return DirectoryAndFileHelper.ClientShapefilesFolder
                   + region.ShapeFileName;
        }

        public static string GetPathShapeFileFullPath(Region region)
        {
            return DirectoryAndFileHelper.ClientShapefilesFolder
                   + region.PathFileName;
        }

        public static string GetPOIShapeFileFullPath(Region region)
        {
            return DirectoryAndFileHelper.ClientShapefilesFolder
                   + region.POIFileName;
        }

        /// <summary>
        /// Returns a ShapeFileFeatureLayer with the region's shape file.
        /// </summary>
        /// <returns></returns>
        public static ShapeFileFeatureLayer GetRegionFeatureLayer(FileInfo shapeFileInfo)
        {
            ShapeFileFeatureLayer layer = new ShapeFileFeatureLayer(shapeFileInfo.FullName)
            {
                Name = shapeFileInfo.Name.ToLower().Replace(".shp", string.Empty)
            };

            RebuildIndexFile(layer);

            return layer;
        }

        /// <summary>
        /// If the IDX file does not exists, we create it.
        /// </summary>
        /// <param name="layer"></param>
        public static void RebuildIndexFile(ShapeFileFeatureLayer layer)
        {
            try
            {
                ShapeFileFeatureSource.BuildIndexFile(layer.ShapePathFileName, BuildIndexMode.Rebuild);
            }
            catch (IOException ioException)
            {
            }

        }

        public static void CheckIndexFile(FileInfo shapefileInfo)
        {
            try
            {
                string indexFileName = shapefileInfo.FullName.Replace(".shp", ".idx");
                if (!File.Exists(indexFileName))
                {
                    ShapeFileFeatureSource.BuildIndexFile(shapefileInfo.FullName, BuildIndexMode.Rebuild);
                }
            }
            catch (Exception)
            {
            }
            
        }

        public static SimpleMarkerOverlay GetMarkersOverlay(WinformsMap map)
        {
            SimpleMarkerOverlay markerOverlay = new SimpleMarkerOverlay { MapControl = map, Name = "markers" };

            return markerOverlay;
        }

        public static SimpleMarkerOverlay GetCumulativeFactorsOverlay(WinformsMap map)
        {
            SimpleMarkerOverlay cumulativeFactorsOverlay = new SimpleMarkerOverlay { MapControl = map, Name = "cumulativeFactors" };

            return cumulativeFactorsOverlay;
        }

        /// <summary>
        /// Returns a ThinkGEO style for the region names.
        /// </summary>
        /// <returns></returns>
        public static Style GetRegionNameStyle()
        {
            TextStyle aux = TextStyles.CreateSimpleTextStyle("ermtregionname", "Arial", 9, DrawingFontStyles.Bold,
                GeoColor.StandardColors.Black, 0, -12);

            aux.DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;
            aux.YOffsetInPixel = 3;
            return aux;
        }

        /// <summary>
        /// Returns the grid.
        /// </summary>
        /// <returns></returns>
        public static Layer GetGraticuleAdornmentLayer()
        {
            GraticuleAdornmentLayer graticuleLayer = new GraticuleAdornmentLayer();
            return graticuleLayer;
        }

        /// <summary>
        /// Returns two feature layers: one for the selected region and one for the childs.
        /// </summary>
        /// <param name="selectedRegion"></param>
        /// <returns></returns>
        public static List<ShapeFileFeatureLayer> GetFeatureLayers(Region selectedRegion)
        {
            List<ShapeFileFeatureLayer> aux = new List<ShapeFileFeatureLayer> { GetRegionFeatureLayer(selectedRegion) };

            List<Region> childRegions = RegionHelper.GetChilds(selectedRegion.IDRegion).GroupBy(r => r.ShapeFileName).Select(g => g.First()).ToList();

            aux.AddRange(from Region region in childRegions select GetRegionFeatureLayer(region));

            return aux;
        }


        public static List<ShapeFileFeatureLayer> GetModelFeatureLayers(Model model, ref List<String> errors)
        {
            List<Region> modelRegions = RegionHelper.GetAllRelated(model.IDRegion);
            List<ShapeFileFeatureLayer> featureLayers = new List<ShapeFileFeatureLayer>();


            for (int i = 0; i < 8; i++)
            {
                List<Region> aux = modelRegions.Where(r => r.RegionLevel == i).GroupBy(r => r.ShapeFileName).Select(g => g.First()).ToList();

                foreach (Region region in aux)
                {
                    string shapefilePath = GetShapeFileFullPath(region);

                    if (!File.Exists(shapefilePath))
                    {
                        //the shapefile couldn't be found in the client. Try to get it from the server.
                        if (!DocumentHelper.GetRegionShapefilesFromServer(region))
                        {
                            errors.Add(region.RegionName + "||");
                        }
                    }

                    if (File.Exists(shapefilePath))
                    {
                        ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(shapefilePath)
                        {
                            Name = (aux.Count == 1 ? i.ToString() : i + "_" + region.RegionName)
                        };
                        RebuildIndexFile(shapeFileFeatureLayer);
                        featureLayers.Add(shapeFileFeatureLayer);
                    }


                }
            }
            return featureLayers;
        }

        /// <summary>
        /// Gets the ShapeFileFeatureLayer for the region, if it's included in the provided list
        /// </summary>
        /// <param name="region"></param>
        /// <param name="modelLayers"></param>
        /// <returns></returns>
        public static ShapeFileFeatureLayer GetRegionFeatureLayer(Region region, List<ShapeFileFeatureLayer> modelLayers)
        {
            if (modelLayers == null || modelLayers.Count == 0)
            {
                return null;
            }
            return modelLayers.Where(ml => ml.Name.Substring(0, 1) == region.RegionLevel.ToString()).FirstOrDefault(shapeFileFeatureLayer => shapeFileFeatureLayer.ShapePathFileName.Contains(region.ShapeFileName));
        }

        /// <summary>
        /// Returns the center of the feature.
        /// </summary>
        /// <param name="shapeFileFeatureLayer"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static PointShape GetFeatureCenterPoint(ShapeFileFeatureLayer shapeFileFeatureLayer, int index)
        {
            PointShape returnValue = new PointShape();
            try
            {
                List<string> featuresToExclude = shapeFileFeatureLayer.FeatureIdsToExclude.ToList();
                shapeFileFeatureLayer.FeatureIdsToExclude.Clear();

                if (!shapeFileFeatureLayer.IsOpen)
                {
                    shapeFileFeatureLayer.Open();
                }

                Feature feature = shapeFileFeatureLayer.QueryTools.GetFeatureById(index.ToString(),
                    ReturningColumnsType.NoColumns);

                foreach (string s in featuresToExclude)
                {
                    shapeFileFeatureLayer.FeatureIdsToExclude.Add(s);
                }

                if (feature != null)
                {
                    MultipolygonShape polygonShape = new MultipolygonShape(feature.GetWellKnownBinary());
                    returnValue = polygonShape.GetCenterPoint();
                    return returnValue;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (shapeFileFeatureLayer.IsOpen)
                {
                    shapeFileFeatureLayer.Close();
                }
            }

        }


        /// <summary>
        /// Creates the rectangle to be used as base for the cumulative factor
        /// </summary>
        /// <param name="cumulativeFactor"></param>
        /// <returns></returns>
        public static void CreateCumulativeFactorImage(Factor cumulativeFactor)
        {
            const int height = 30;
            const int width = 30;
            Bitmap bmp = new Bitmap(width, height);
            Graphics graphicsObj = Graphics.FromImage(bmp);


            Color color = SystemColors.Control;
            if (cumulativeFactor.Color.Split(',').Length > 1)
            {
                color = ColorTranslator.FromHtml(cumulativeFactor.Color.Split(',')[0]);
            }

            Pen myPen = new Pen(color, 3);
            for (int i = 0; i < 40; i = i + 3)
            {
                graphicsObj.DrawLine(myPen, new Point(i, 0), new Point(i, height));
            }

            try
            {
                bmp.Save(DirectoryAndFileHelper.ClientCumulativeFactorImageFolder + "\\" + cumulativeFactor.IdFactor + ".jpg", ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
            }

        }

        public static Image GetCumulativeFactorImage(int idFactor)
        {
            return Image.FromFile(DirectoryAndFileHelper.ClientCumulativeFactorImageFolder + "\\" + idFactor + ".jpg");
        }

        public static Image GetCumulativeGenericImage()
        {
            return Image.FromFile(DirectoryAndFileHelper.ClientIconsFolder + "transp.png");
        }

        public static String GetAsKML(List<ShapeFileFeatureLayer> shapeFileFeatureLayers)
        {
            KmlGeoCanvas kmlGeo = new KmlGeoCanvas();
            GeoBrush gb = new GeoSolidBrush(GeoColor.StandardColors.LightGray);
            GeoPen gp = new GeoPen(gb) { DashStyle = LineDashStyle.Solid };
            foreach (ShapeFileFeatureLayer shapeFileFeatureLayer in shapeFileFeatureLayers)
            {
                foreach (Feature feature in shapeFileFeatureLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns))
                {
                    kmlGeo.DrawArea(feature, gp, DrawingLevel.LevelOne);
                }

            }
            //kmlGeo.

            return string.Empty;
        }

        #region Copy

        public static ThinkGeoMarker CopyMarker(ThinkGeoMarker marker)
        {
            ThinkGeoMarker m = new ThinkGeoMarker();
            foreach (Control control in marker.Controls)
            {
                m.Controls.Add(control);
            }
            m.Name = marker.Name;
            m.Tag = marker.Tag;
            m.Image = marker.Image;
            m.Text = marker.Text;
            m.ToolTipText = marker.ToolTipText;
            m.Location = marker.Location;
            m.Position = marker.Position;
            m.Visible = marker.Visible;
            m.Height = marker.Height;
            m.Width = marker.Width;
            return m;
        }

        #endregion
    }
}
