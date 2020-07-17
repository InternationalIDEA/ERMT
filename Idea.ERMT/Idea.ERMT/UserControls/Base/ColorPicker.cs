using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Idea.ERMT.UserControls
{
    /// <summary>
    /// Possible modes for ColorPicker control.
    /// </summary>
    public enum PickerModes
    {
        /// <summary>
        /// Functions only as a clickable button.
        /// </summary>
        ButtonOnly,

        /// <summary>
        /// Displays a drop-down color palette when clicked.
        /// </summary>
        DropDown,

        /// <summary>
        /// Splits the control into one section that displays a color
        /// that can be clicked, and another section that drops down
        /// a color palette.
        /// </summary>
        Split
    }

    /// <summary>
    /// Button control that allows the user to click to select the current color
    /// and/or display a palette of colors.
    /// </summary>
    [DefaultProperty("Mode")]
    public class ColorPicker : Control
    {
        // Events
        public delegate void ColorPaletteEventHandler(object sender, ColorPickerEventArgs e);
        public new event ColorPaletteEventHandler Click;
        public event ColorPaletteEventHandler SelectionChanged;

        // Properties
        private PickerModes _mode = PickerModes.Split;
        private Color _value = Color.White;

        /// <summary>
        /// Gets or sets the appearance and behavior mode of this control.
        /// </summary>
        [Description("Determines the appearance and behavior of this control.")]
        [DefaultValue(PickerModes.Split)]
        public PickerModes Mode
        {
            get { return _mode; }
            set { _mode = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the current color value of this instance.
        /// </summary>
        [Description("Specifies the current color value.")]
        [DefaultValue(KnownColor.White)]
        public Color Value
        {
            get { return _value; }
            set { _value = value; Invalidate(); }
        }

        #region ColorPalette Properties

        /// <summary>
        /// Gets or sets the height of each palette item, in pixels.
        /// </summary>
        [Description("Indicates the height of each palette item in pixels.")]
        [DefaultValue(16)]
        public int BoxHeight { get { return _palette.BoxHeight; } set { _palette.BoxHeight = value; } }

        /// <summary>
        /// Gets or sets the width of each palette item, in pixels.
        /// </summary>
        [Description("Indicates the width of each palette item in pixels.")]
        [DefaultValue(16)]
        public int BoxWidth { get { return _palette.BoxWidth; } set { _palette.BoxWidth = value; } }

        /// <summary>
        /// Gets or sets the number of palette items per row.
        /// </summary>
        [Description("Determines the number of palette items per row.")]
        [DefaultValue(8)]
        public int Columns { get { return _palette.Columns; } set { _palette.Columns = value; } }

        /// <summary>
        /// Gets or sets the initial color palette.
        /// </summary>
        [Description("Specifies the control's initial palette.")]
        [DefaultValue(Palette.Basic)]
        public Palette DefaultPalette { get { return _palette.DefaultPalette; } set { _palette.DefaultPalette = value; } }

        /// <summary>
        /// Gets or sets the distance between palette items, in pixels.
        /// </summary>
        [Description("Determines the distance between palette items in pixels.")]
        [DefaultValue(2)]
        public int Margins { get { return _palette.Margins; } set { _palette.Margins = value; } }

        /// <summary>
        /// Gets or sets the maximum number of visible rows. Scrolling is enabled if this
        /// number is less than the total number of rows. Use 0 to display all rows.
        /// </summary>
        [Description("Indicates the maximum number of visible rows. Use 0 to display all rows.")]
        [DefaultValue(0)]
        public int VisibleRows { get { return _palette.VisibleRows; } set { _palette.VisibleRows = value; } }

        #endregion

        #region ColorDropDown Properties

        /// <summary>
        /// Gets or sets whether or not the drop-down palette includes a button that
        /// allows the user to select a color from the color dialog box.
        /// </summary>
        [Description("Determines if the palette displays a button to select additional colors.")]
        [DefaultValue(true)]
        public bool MoreColorsButton { get { return _dropDown.MoreColorsButton; } set { _dropDown.MoreColorsButton = value; } }

        #endregion

        // Private data
        private ColorDropDown _dropDown = new ColorDropDown();
        private ColorPalette _palette;
        private int _margins;
        private int _splitPos;
        private bool _mousePress;

        // Construction
        public ColorPicker()
        {
            _dropDown = new ColorDropDown();
            _palette = _dropDown.GetColorPaletteControl();
            _mousePress = false;

            _palette.Click += _palette_Click;
            _palette.SelectionChanged += _palette_SelectionChanged;
            _dropDown.Closed += _dropDown_Closed;
        }

        // Propagate Click event
        void _palette_Click(object sender, ColorPickerEventArgs e)
        {
            Value = e.Value;
            _dropDown.Close(ToolStripDropDownCloseReason.ItemClicked);
            RaiseClickEvent();
        }

        // Propagate SelectionChanged event
        void _palette_SelectionChanged(object sender, ColorPickerEventArgs e)
        {
            if (SelectionChanged != null)
                SelectionChanged(this, e);
        }

        // Dropdown has closed
        void _dropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Returns the dropdown ColorPalette control associated with
        /// this instance.
        /// </summary>
        public ColorPalette GetColorPaletteControl()
        {
            return _dropDown.GetColorPaletteControl();
        }

        // Handle resized control
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            _margins = Math.Min(Math.Min(ClientSize.Width, ClientSize.Height) / 4, 8);
            _splitPos = Width - (Math.Max(_margins, 3) * 3);
        }

        // Render control
        protected override void OnPaint(PaintEventArgs e)
        {
            PushButtonState state;
            if (_dropDown.Visible)
                state = PushButtonState.Pressed;
            else
            {
                Point point = Parent.PointToClient(Cursor.Position);
                if (Bounds.Contains(point))
                    state = _mousePress ? PushButtonState.Pressed : PushButtonState.Hot;
                else
                    state = PushButtonState.Default;
            }
            ButtonRenderer.DrawButton(e.Graphics, ClientRectangle, state);

            // Implement custom drawing
            Rectangle rect = ClientRectangle;
            if (Mode == PickerModes.DropDown)
                rect.Width = _splitPos + _margins;
            else if (Mode == PickerModes.Split)
                rect.Width = _splitPos;
            rect.Inflate(-_margins, -_margins);
            rect.Width--;
            rect.Height--;

            // Draw color box
            e.Graphics.FillRectangle(new SolidBrush(Value), rect);
            e.Graphics.DrawRectangle(SystemPens.GrayText, rect);
            if (Mode == PickerModes.DropDown)
            {
                // Draw arrow
                rect.X = _splitPos;
                rect.Width = ClientRectangle.Width - rect.X - _margins;
                DrawArrow(e.Graphics, new SolidBrush(SystemColors.ControlText), rect);
            }
            else if (Mode == PickerModes.Split)
            {
                // Draw divider line
                e.Graphics.DrawLine(SystemPens.ControlDark,
                    _splitPos - 1, rect.Top, _splitPos - 1, rect.Bottom);
                e.Graphics.DrawLine(SystemPens.ControlLightLight,
                    _splitPos, rect.Top, _splitPos, rect.Bottom);

                // Draw arrow
                rect.X = _splitPos;
                rect.Width = ClientRectangle.Width - rect.X - _margins;
                DrawArrow(e.Graphics, new SolidBrush(SystemColors.ControlText), rect);
            }
            base.OnPaint(e);
        }

        // Draws a small down arrow
        protected void DrawArrow(Graphics g, Brush brush, Rectangle rect)
        {
            int x = rect.Left + (rect.Width / 2);
            int y = rect.Top + (rect.Height / 2);
            using (Pen pen = new Pen(brush))
            {
                g.DrawLine(pen, x - 2, y - 1, x + 2, y - 1);
                g.DrawLine(pen, x - 1, y, x + 1, y);
            }
            g.FillRectangle(brush, x, y + 1, 1, 1);
        }

        // Implement hot tracking
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }

        // Implement hot tracking
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }

        // Handle mouse down
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Response to left button down
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // Determine action (drop down or click)
                bool dropDown;
                if (Mode == PickerModes.ButtonOnly)
                    dropDown = false;
                else if (Mode == PickerModes.DropDown)
                    dropDown = true;
                else
                    dropDown = (e.X >= _splitPos);

                // Take action
                if (dropDown)
                    _dropDown.Show(this, 0, Height);
                else
                    _mousePress = true;

                // Force button repaint
                Invalidate();
            }
        }

        // Handle mouse up
        protected override void OnMouseUp(MouseEventArgs e)
        {
            // Test for mouse click
            if (_mousePress &&
                Bounds.Contains(Parent.PointToClient(Cursor.Position)))
                RaiseClickEvent();

            _mousePress = false;
            Invalidate();
        }

        #region Helper Methods

        // Raise Click event
        protected void RaiseClickEvent()
        {
            if (Click != null)
            {
                ColorPickerEventArgs args = new ColorPickerEventArgs();
                args.Value = Value;
                Click(this, args);
            }
        }

        #endregion

    }
}
