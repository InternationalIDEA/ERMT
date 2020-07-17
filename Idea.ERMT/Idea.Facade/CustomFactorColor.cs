using System.Collections.Generic;
using System.Drawing;

namespace Idea.Facade
{
    public class CustomFactorColor
    {
        #region Atributes
        private List<Color> _colors;
        #endregion

        #region Properties

        public string Name { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int TotalValues { get; set; }
        public List<Color> ListColors
        {
            get { return _colors ?? (_colors = new List<Color>()); }
            set { _colors = value; }
        }
        public int IdFactor { get; set; }
        #endregion

        #region Constructors
        public CustomFactorColor() { }

        public CustomFactorColor(string name, int minValue, int maxValue, int totalValues, int idFactor, List<Color> colors)
        {
            Name = name;
            MinValue = minValue;
            MaxValue = maxValue;
            TotalValues = totalValues;
            IdFactor = idFactor;

            if (_colors == null)
                _colors = new List<Color>();

            _colors = colors;
        }
        #endregion
    }
}
