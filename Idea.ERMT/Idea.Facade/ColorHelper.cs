using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using ThinkGeo.MapSuite.Core;

namespace Idea.Facade
{
    public class ColorHelper
    {
        private const int RGBMax = 255;
        private const float HLSMax = 360.0F;

        private static int HueToRGB(float n1, float n2, float hue)
        {
            /* range check: note values passed add/subtract thirds of range */
            if (hue < 0)
                hue += HLSMax;

            if (hue > HLSMax)
                hue -= HLSMax;

            /* return r,g, or b value from this tridrant */
            if (hue < (HLSMax / 6))
                return (int)(n1 + (((n2 - n1) * hue + (HLSMax / 12)) / (HLSMax / 6)));
            if (hue < (HLSMax / 2))
                return (int)(n2);
            if (hue < ((HLSMax * 2) / 3))
                return (int)(n1 + (((n2 - n1) * (((HLSMax * 2) / 3) - hue) + (HLSMax / 12)) / (HLSMax / 6)));
                
            return (int)(n1);
        }

        public static Color HBStoRGB(float hue, float bri, float sat)
        {
            float R, G, B;                /* RGB component values */
            bri = bri * 360F;
            sat = sat * 360F;

            if (sat == 0)
            {            /* achromatic case */
                R = G = B = (bri * RGBMax) / HLSMax;
                if (hue < 0 || hue > HLSMax)
                {
                    /* ERROR */
                    return Color.White;
                }
            }
            else
            {   /* chromatic case */
                /* set up magic numbers */
                float magic2;       /* calculated magic numbers (really!) */
                if (bri <= (HLSMax / 2))
                    magic2 = (bri * (HLSMax + sat) + (HLSMax / 2)) / HLSMax;
                else
                    magic2 = bri + sat - ((bri * sat) + (HLSMax / 2)) / HLSMax;
                float magic1 = 2 * bri - magic2;       /* calculated magic numbers (really!) */

                /* get RGB, change units from HLSMAX to RGBMAX */
                R = (HueToRGB(magic1, magic2, hue + (HLSMax / 3)) * RGBMax + (HLSMax / 2)) / HLSMax;
                G = (HueToRGB(magic1, magic2, hue) * RGBMax + (HLSMax / 2)) / HLSMax;
                B = (HueToRGB(magic1, magic2, hue - (HLSMax / 3)) * RGBMax + (HLSMax / 2)) / HLSMax;
            }
            return Color.FromArgb(Math.Min(255, (int)R), Math.Min(255, (int)G), Math.Min(255, (int)B));
        }

        /// <summary>
        /// Devuelve un gradiente de oscuro a claro
        /// </summary>
        /// <param name="c"></param>
        /// <param name="levels">cantidad de niveles a devolver</param>
        /// <returns></returns>
        public static Color[] GetColorGradient(Color c, int levels)
        {
            List<Color> colors = new List<Color>();
            const float brightness = 1.0F;
            float step = (brightness - 0.25F) / levels;
            for (float value = 0.20F; value < (brightness - 0.05F); value = value + step)
            {
                if (colors.Count == levels)
                    break;
                colors.Add(HBStoRGB(c.GetHue(), value, c.GetSaturation()));
            }
            return colors.ToArray();
        }

        /// <summary>
        /// Devuelve un gradiente de oscuro a claro
        /// </summary>
        /// <param name="c"></param>
        /// <param name="levels">cantidad de niveles a devolver</param>
        /// <returns></returns>
        public static ClassBreakStyle GetColorGradientClassBreakStyle(Color c, int levels)
        {
            Color[] colors = GetColorGradient(c, levels);
            return GetClassBreakStyleFromColors(colors);
        }

        /// <summary>
        /// Devuelve un gradiente de oscuro moderado a claro moderado
        /// </summary>
        /// <param name="c"></param>
        /// <param name="levels">cantidad de niveles a devolver</param>
        /// <returns></returns>
        public static Color[] GetModerateColorGradient(Color c, int levels)
        {
            List<Color> colors = new List<Color>();
            const float brightness = 1.0F;
            float step = (brightness - 0.55F) / levels;
            for (float value = 0.50F; value < (brightness - 0.05F); value = value + step)
            {
                if (colors.Count == levels)
                    break;
                colors.Add(HBStoRGB(c.GetHue(), value, c.GetSaturation()));
            }
            return colors.ToArray();
        }

        /// <summary>
        /// Returns a gradient of Red Yellow Green
        /// </summary>
        /// <param name="levels">Amount of levels</param>
        /// <returns></returns>
        public static Color[] GetTrafficLightColorGradient(int levels)
        {
            int resto = levels % 3;
            List<Color> colors = new List<Color>();
            colors.AddRange(GetModerateColorGradient(Color.Red, (levels / 3) + (resto == 2 ? 1 : 0)));
            colors.AddRange(GetModerateColorGradient(Color.Yellow, levels / 3));
            colors.AddRange(GetModerateColorGradient(Color.Green, (levels / 3) + (resto > 0 ? 1 : 0)).Reverse());
            return colors.ToArray(); ;
        }

        /// <summary>
        /// Returns a gradient of Red Yellow Green
        /// </summary>
        /// <param name="levels">Amount of levels</param>
        /// <returns></returns>
        public static ClassBreakStyle GetTrafficLightClassBreakStyle(int levels)
        {
            Color[] colors = GetTrafficLightColorGradient(levels);

            return GetClassBreakStyleFromColors(colors);
        }

        /// <summary>
        /// Returns a gradient of Blue - yellow - red
        /// </summary>
        /// <param name="levels">Amount of levels</param>
        /// <returns></returns>
        public static Color[] GetTemperatureColorGradient(int levels)
        {
            int resto = levels % 3;
            List<Color> colors = new List<Color>();
            colors.AddRange(GetModerateColorGradient(Color.Red, (levels / 3) + (resto == 2 ? 1 : 0)));
            colors.AddRange(GetModerateColorGradient(Color.Yellow, levels / 3));
            colors.AddRange(GetModerateColorGradient(Color.Blue, (levels / 3) + (resto > 0 ? 1 : 0)).Reverse());
            return colors.ToArray();
        }

        /// <summary>
        /// Returns a gradient of Blue - yellow - red
        /// </summary>
        /// <param name="levels">Amount of levels</param>
        /// <returns></returns>
        public static ClassBreakStyle GetTemperatureClassBreakStyle(int levels)
        {
            Color[] colors = GetTemperatureColorGradient(levels);

            return GetClassBreakStyleFromColors(colors);
        }

        public static ClassBreakStyle GetCustomClassBreakStyle(int idModel, int levels)
        {
            Color[] colors = GetCustomColorGradient(idModel, levels);

            if (colors.Length == 0)
            {
                return new ClassBreakStyle("empty");
            }

            return GetClassBreakStyleFromColors(colors);
        }

        public static Color[] GetCustomColorGradient(int idModel, int levels)
        {
            var listaElement = CustomFactorColorHelper.GetFactorColorsFromXML(idModel);
            List<Color> colors = new List<Color>();

            if (listaElement != null)
            {
                var factorsColors =
                       from c in listaElement
                       from cColors in c.Descendants("Colors")
                       where (int)c.Element("TotalValues") == levels
                       select cColors;

                colors.AddRange(from factorsColor in factorsColors from colorXML in factorsColor.Elements() select ColorTranslator.FromHtml(colorXML.Value));
            }
            colors.Reverse();
            return colors.ToArray();
        }

        private static ClassBreakStyle GetClassBreakStyleFromColors(Color[] colors)
        {
            //seems the old app handled color scales differently. We need to invert the order in the colors array.
            colors = colors.Reverse().ToArray();

            ClassBreakStyle cbs = new ClassBreakStyle();
            
            AreaStyle defaultAreaStyle = new AreaStyle(new GeoSolidBrush(GeoColor.SimpleColors.Transparent));
            
            GeoBrush gb = new GeoSolidBrush(GeoColor.StandardColors.LightGray);
            GeoPen gp = new GeoPen(gb) {DashStyle = LineDashStyle.Solid};
            defaultAreaStyle.OutlinePen = gp;
            cbs.ClassBreaks.Add(new ClassBreak(0, defaultAreaStyle));
            


            int i = 1;
            
            foreach (Color color in colors)
            {
                GeoSolidBrush gsb = new GeoSolidBrush(new GeoColor(color.A,color.R, color.G, color.B));
                AreaStyle ss = new AreaStyle(gsb) {OutlinePen = gp};
                cbs.ClassBreaks.Add(new ClassBreak(i, ss));
                i++;
            }

            return cbs;
        }

    }
}
