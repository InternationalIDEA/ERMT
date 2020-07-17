using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Idea.Entities;
using ThinkGeo.MapSuite.Core;

namespace Idea.Facade
{
    class KmlGeoCanvas : GeoCanvas
    {
        StringBuilder _kmlBuilder;

        private const float VirtualMapWidth = 100f;
        private const float VirtualMapHeight = 100f;
        private RectangleShape _virtualWorldExtent = new RectangleShape();

        private Dictionary<int, GeoBrush> _geoBrushDictionary = new Dictionary<int, GeoBrush>();

        private readonly Dictionary<int, string> _styleUrlDictionary = new Dictionary<int, string>();

        private string _extrudeString = string.Empty;
        private string _tessellateString = string.Empty;
        private string _altitudeModeString = string.Empty;

        private readonly StringBuilder _contentStringBuildLevel1 = new StringBuilder();
        private readonly StringBuilder _contentStringBuildLevel2 = new StringBuilder();
        private readonly StringBuilder _contentStringBuildLevel3 = new StringBuilder();
        private readonly StringBuilder _contentStringBuildLevel4 = new StringBuilder();
        private readonly StringBuilder _contentStringBuildLabel = new StringBuilder();


        public bool Extrude
        {
            get;
            set;
        }

        public bool Tessellate
        {
            get;
            set;
        }

        public AltitudeMode AltitudeMode
        {
            get;
            set;
        }

        protected override void BeginDrawingCore(object nativeImage, RectangleShape worldExtent, GeographyUnit drawingMapUnit)
        {
            _kmlBuilder = (StringBuilder)nativeImage;

            _virtualWorldExtent = worldExtent;

            _extrudeString = Extrude ? @"<extrude>1</extrude>" : "";
            _tessellateString = Tessellate ? @"<tessellate>1</tessellate>" : "";
            switch (AltitudeMode)
            {
                case AltitudeMode.Absolute:
                    _altitudeModeString = "<altitudeMode>absolute</altitudeMode>";
                    break;
                case AltitudeMode.RelativeToGround:
                    _altitudeModeString = "<altitudeMode>relativeToGround</altitudeMode>";
                    break;
            }

            if (!_kmlBuilder.ToString().Contains(@"<?xml version=""1.0"" encoding=""UTF-8""?>"))
            {
                //need to check if the header is not there already.
                _kmlBuilder.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
                _kmlBuilder.AppendLine(@"<kml xmlns=""http://www.opengis.net/kml/2.2"">");
                _kmlBuilder.AppendLine(@"<Document>");
            }
        }

        

        protected override void DrawEllipseCore(ScreenPointF screenPoint, float width, float height, GeoPen outlinePen, GeoBrush fillBrush, DrawingLevel drawingLevel, float xOffset, float yOffset, PenBrushDrawingOrder penBrushDrawingOrder)
        {
            throw new NotImplementedException();
        }

        protected override void DrawLineCore(IEnumerable<ScreenPointF> screenPoints, GeoPen linePen, DrawingLevel drawingLevel, float xOffset, float yOffset)
        {
            int id = linePen.GetHashCode();
            if (!_styleUrlDictionary.ContainsKey(id))
            {
                string kmlStyle = GetLineStyleKml(id, linePen);
                _kmlBuilder.Append(kmlStyle);
                _styleUrlDictionary.Add(id, string.Format("<styleUrl>#{0}</styleUrl>", id));
            }

            StringBuilder contentStringBuilder = GetStringBuilder(drawingLevel);

            contentStringBuilder.AppendLine();
            contentStringBuilder.AppendLine(@"<Placemark>");
            contentStringBuilder.AppendLine(_styleUrlDictionary[id]);
            contentStringBuilder.AppendLine(@"<LineString>");

            contentStringBuilder.AppendLine(_extrudeString);
            contentStringBuilder.AppendLine(_tessellateString);
            contentStringBuilder.AppendLine(_altitudeModeString);

            AppendCoordinates(screenPoints, xOffset, yOffset, contentStringBuilder);
            contentStringBuilder.AppendLine(@"</LineString>");
            contentStringBuilder.AppendLine(@"</Placemark>");
        }

        private void AppendCoordinates(IEnumerable<ScreenPointF> screenPoints, float xOffset, float yOffset, StringBuilder contentStringBuilder)
        {
            contentStringBuilder.AppendLine(@"<coordinates>");

            List<ScreenPointF> points = screenPoints.ToList();
            //in order to create a KML file that's NOT too big, we simplify and add less points.
            int simplificationLevel = points.Count/1000;
            simplificationLevel = (simplificationLevel == 0 ? 1 : simplificationLevel);
            int i = 0;
            while (i < points.Count())
            {
                PointShape pointShape = ExtentHelper.ToWorldCoordinate(_virtualWorldExtent, points[i].X + xOffset, points[i].Y + yOffset, VirtualMapWidth, VirtualMapHeight);
                contentStringBuilder.AppendFormat(" {0},{1},{2} ", pointShape.X, pointShape.Y, 500);
                i += simplificationLevel;
            }
            contentStringBuilder.AppendLine(@"</coordinates>");
        }

        private void AppendLinearRing(IEnumerable<ScreenPointF> screenPoints, float xOffset, float yOffset, StringBuilder contentStringBuilder)
        {
            contentStringBuilder.AppendLine(@"<LinearRing>");
            AppendCoordinates(screenPoints, xOffset, yOffset, contentStringBuilder);
            contentStringBuilder.AppendLine(@"</LinearRing>");
        }

        private StringBuilder GetStringBuilder(DrawingLevel drawingLevel)
        {
            StringBuilder result = new StringBuilder();
            switch (drawingLevel)
            {
                case DrawingLevel.LevelOne:
                    result = _contentStringBuildLevel1;
                    break;
                case DrawingLevel.LevelTwo:
                    result = _contentStringBuildLevel2;
                    break;
                case DrawingLevel.LevelThree:
                    result = _contentStringBuildLevel3;
                    break;
                case DrawingLevel.LevelFour:
                    result = _contentStringBuildLevel4;
                    break;
                case DrawingLevel.LabelLevel:
                    result = _contentStringBuildLabel;
                    break;
            }
            return result;
        }

        private string GetLineStyleKml(int id, GeoPen linePen)
        {
            StringBuilder styleBuilder = new StringBuilder();
            styleBuilder.AppendLine();
            styleBuilder.AppendFormat(@"<Style id=""{0}"">", id);
            styleBuilder.AppendLine();
            styleBuilder.AppendLine(@"<LineStyle>");
            styleBuilder.AppendFormat(@"<color>{0}</color>", GetGoogleHTMLColor(linePen.Color));
            styleBuilder.AppendLine();
            styleBuilder.AppendFormat(@"<width>{0}</width>", linePen.Width);
            styleBuilder.AppendLine();
            styleBuilder.AppendLine(@"</LineStyle>");
            styleBuilder.AppendLine(@"</Style>");

            return styleBuilder.ToString();
        }

        private string GetPolygonStyleKml(int id, GeoPen outlinePen, GeoColor fillColor)
        {
            StringBuilder styleBuilder = new StringBuilder();
            styleBuilder.AppendLine();
            styleBuilder.AppendFormat(@"<Style id=""{0}"">", id);
            styleBuilder.AppendLine();
            styleBuilder.AppendLine(@"<LineStyle>");
            if (outlinePen != null)
            {
                styleBuilder.AppendFormat(@"<color>{0}</color>", GetGoogleHTMLColor(outlinePen.Color));
                styleBuilder.AppendLine();
                styleBuilder.AppendFormat(@"<width>{0}</width>", outlinePen.Width);
            }
            styleBuilder.AppendLine();
            styleBuilder.AppendLine(@"</LineStyle>");
            styleBuilder.AppendLine(@"<PolyStyle>");
            styleBuilder.AppendFormat(@"<color>{0}</color>", GetGoogleHTMLColor(fillColor));
            styleBuilder.AppendLine();
            styleBuilder.AppendLine(@"</PolyStyle>");

            styleBuilder.AppendLine(@"</Style>");

            return styleBuilder.ToString();
        }


        private string GetGoogleHTMLColor(GeoColor geoColor)
        {
            StringBuilder googleHtmlColor = new StringBuilder();
            googleHtmlColor.Append(GetColorComponentInHex(geoColor.AlphaComponent));
            googleHtmlColor.Append(GetColorComponentInHex(geoColor.BlueComponent));
            googleHtmlColor.Append(GetColorComponentInHex(geoColor.GreenComponent));
            googleHtmlColor.Append(GetColorComponentInHex(geoColor.RedComponent));
            return googleHtmlColor.ToString();
        }

        private string GetColorComponentInHex(byte component)
        {
            return Convert.ToInt32(component).ToString("X").PadLeft(2, '0');
        }

        protected override void DrawScreenImageCore(GeoImage image, float centerXInScreen, float centerYInScreen, float widthInScreen, float heightInScreen, DrawingLevel drawingLevel, float xOffset, float yOffset, float rotateAngle)
        {
            throw new NotImplementedException();
        }

        protected override void DrawScreenImageWithoutScalingCore(GeoImage image, float centerXInScreen, float centerYInScreen, DrawingLevel drawingLevel, float xOffset, float yOffset, float rotateAngle)
        {
            throw new NotImplementedException();
        }

        protected override void DrawAreaCore(IEnumerable<ScreenPointF[]> screenPoints, GeoPen outlinePen, GeoBrush fillBrush, DrawingLevel drawingLevel, float xOffset, float yOffset, PenBrushDrawingOrder penBrushDrawingOrder)
        {
            //if (fillBrush == null)
            //{
            //    fillBrush = new GeoSolidBrush(GeoColor.SimpleColors.Transparent);
            //}
            int id = 0;
            if (fillBrush != null)
            {
                id = fillBrush.GetHashCode();
            }
            else
            {
                id = outlinePen.GetHashCode();
                fillBrush = new GeoSolidBrush(GeoColor.SimpleColors.Transparent);
            }

            if (!_styleUrlDictionary.ContainsKey(id))
            {
                string kmlStyle = GetPolygonStyleKml(id, outlinePen, ((GeoSolidBrush)fillBrush).Color);
                _kmlBuilder.Append(kmlStyle);
                _styleUrlDictionary.Add(id, string.Format("<styleUrl>#{0}</styleUrl>", id));
            }

            StringBuilder contentStringBuilder = GetStringBuilder(drawingLevel);

            contentStringBuilder.AppendLine();
            contentStringBuilder.AppendLine(@"<Placemark>");
            contentStringBuilder.AppendLine(_styleUrlDictionary[id]);
            contentStringBuilder.AppendLine(@"<Polygon>");

            contentStringBuilder.AppendLine(_extrudeString);
            contentStringBuilder.AppendLine(_tessellateString);
            contentStringBuilder.AppendLine(_altitudeModeString);

            bool firstCoordinates = true;
            foreach (ScreenPointF[] screenPoint in screenPoints)
            {
                if (firstCoordinates)
                {
                    contentStringBuilder.AppendLine(@"<outerBoundaryIs>");
                    AppendLinearRing(screenPoint, xOffset, yOffset, contentStringBuilder);
                    contentStringBuilder.AppendLine(@"</outerBoundaryIs>");
                    firstCoordinates = false;
                }
                else
                {
                    contentStringBuilder.AppendLine(@"<innerBoundaryIs>");
                    AppendLinearRing(screenPoint, xOffset, yOffset, contentStringBuilder);
                    contentStringBuilder.AppendLine(@"</innerBoundaryIs>");
                }
            }
            contentStringBuilder.AppendLine(@"</Polygon>");
            contentStringBuilder.AppendLine(@"</Placemark>");
        }

        protected override void DrawTextCore(string text, GeoFont font, GeoBrush fillBrush, GeoPen haloPen, IEnumerable<ScreenPointF> textPathInScreen, DrawingLevel drawingLevel, float xOffset, float yOffset, float rotateAngle)
        {
            //StringBuilder rnSB = new StringBuilder();
            //rnSB.AppendLine("<Placemark>");
            //rnSB.AppendFormat("<description>{0}</description>", text);
            //rnSB.AppendFormat("<name>{0}</name>", text);
            //rnSB.AppendLine("<visibility>1</visibility>");
            //rnSB.AppendLine("<Point>");
            //AppendCoordinates(textPathInScreen,xOffset,yOffset,rnSB);
            ////rnSB.AppendFormat("<coordinates>{0},{1},20000</coordinates>",
            ////    ((ScreenPointF[])(textPathInScreen))[0].X,
            ////    ((ScreenPointF[])(textPathInScreen))[0].Y);
            //rnSB.AppendLine("</Point>");
            //rnSB.AppendLine("<Style><IconStyle><Icon><href></href></Icon></IconStyle></Style>");
            //rnSB.AppendLine("</Placemark>");
            //StringBuilder contentStringBuilder = GetStringBuilder(drawingLevel);
            //contentStringBuilder.Append(rnSB.ToString());
        }

        public static StringBuilder GetRegionNameXML(String regionName, PointShape position)
        {
            StringBuilder aux = new StringBuilder();
            aux.AppendLine("<Placemark>");
            aux.AppendFormat("<description>{0}</description>", regionName);
            aux.AppendFormat("<name>{0}</name>", regionName);
            aux.AppendLine("<visibility>1</visibility>");
            aux.AppendLine("<Point>");
            //AppendCoordinates(textPathInScreen, xOffset, yOffset, rnSB);
            aux.AppendFormat("<coordinates>{0},{1},20000</coordinates>",
                position.X,
                position.Y);
            aux.AppendLine("</Point>");
            aux.AppendLine("<Style><IconStyle><Icon><href></href></Icon></IconStyle></Style>");
            aux.AppendLine("</Placemark>");
            return aux;
        }

        public static StringBuilder GetMarkerXML(ThinkGeo.MapSuite.DesktopEdition.Marker thinkGeoMarker)
        {
            Marker m = (Marker) thinkGeoMarker.Tag;
            MarkerType mt = MarkerTypeHelper.Get(m.IDMarkerType);
            StringBuilder aux = new StringBuilder();
            aux.AppendLine("<Placemark>");
            aux.AppendFormat("<description>{0}</description>", thinkGeoMarker.Text);
            aux.AppendFormat("<name>{0}</name>", thinkGeoMarker.Text);
            aux.AppendLine("<visibility>1</visibility>");
            aux.AppendLine("<Point>");
            //AppendCoordinates(textPathInScreen, xOffset, yOffset, rnSB);
            aux.AppendFormat("<coordinates>{0},{1},20000</coordinates>",
                thinkGeoMarker.Position.X,
                thinkGeoMarker.Position.Y);
            aux.AppendLine("</Point>");
            aux.AppendLine(String.Format("<Style><IconStyle><Icon><href>./Images/{0}</href></Icon></IconStyle></Style>",mt.Symbol));
            aux.AppendLine("</Placemark>");
            return aux;
        }

        public static StringBuilder GetCumulativeFactorXML(ThinkGeo.MapSuite.DesktopEdition.Marker thinkGeoMarker)
        {
            StringBuilder aux = new StringBuilder();
            aux.AppendLine("<Placemark>");
            aux.AppendFormat("<description>{0}</description>", string.Empty);
            aux.AppendFormat("<name>{0}</name>", string.Empty);
            aux.AppendLine("<visibility>1</visibility>");
            aux.AppendLine("<Point>");
            //AppendCoordinates(textPathInScreen, xOffset, yOffset, rnSB);
            aux.AppendFormat("<coordinates>{0},{1},20000</coordinates>",
                thinkGeoMarker.Position.X,
                thinkGeoMarker.Position.Y);
            aux.AppendLine("</Point>");
            aux.AppendLine(String.Format("<Style><IconStyle><Icon><href>./Images/{0}</href></Icon></IconStyle></Style>", thinkGeoMarker.Name + ".jpg"));
            aux.AppendLine("</Placemark>");
            return aux;
        }

        protected override float GetCanvasHeightCore(object nativeImage)
        {
            return VirtualMapHeight;
        }

        protected override float GetCanvasWidthCore(object nativeImage)
        {
            return VirtualMapWidth;
        }

        public override Stream GetStreamFromGeoImage(GeoImage image)
        {
            throw new NotImplementedException();
        }

        protected override DrawingRectangleF MeasureTextCore(string text, GeoFont font)
        {
            Font f = new Font(new FontFamily("Arial"), font.Size);
            Size size = TextRenderer.MeasureText(text, f);
            DrawingRectangleF drf = new DrawingRectangleF(0, 0, size.Width, size.Height);
            return drf;

        }

        protected override GeoImage ToGeoImageCore(object nativeImage)
        {
            throw new NotImplementedException();
        }

        protected override object ToNativeImageCore(GeoImage image)
        {
            throw new NotImplementedException();
        }

        protected override void EndDrawingCore()
        {
            _kmlBuilder.Append(_contentStringBuildLevel4);
            _kmlBuilder.Append(_contentStringBuildLevel3);
            _kmlBuilder.Append(_contentStringBuildLevel2);
            _kmlBuilder.Append(_contentStringBuildLevel1);
            _kmlBuilder.Append(_contentStringBuildLabel);
            _kmlBuilder.AppendFormat(@"</Document>");
            _kmlBuilder.AppendFormat(@"</kml>");
        }

        protected override void FlushCore()
        {
            throw new NotImplementedException();
        }
    }
}
