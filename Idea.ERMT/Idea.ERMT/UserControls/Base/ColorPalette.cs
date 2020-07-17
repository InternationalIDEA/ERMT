using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Idea.ERMT.UserControls
{

    /// <summary>
    /// Default palettes for ColorPalette control.
    /// </summary>
    public enum Palette
    {
        None,
        Basic,
        Extended,
        Extended2,
        Extended3,
    }

    /// <summary>
    /// Control that allows the user to select a color from a palette of colors.
    /// </summary>
    public class ColorPalette : ScrollableControl
    {
        // Events
        public delegate void ColorPaletteEventHandler(object sender, ColorPickerEventArgs e);
        public new event ColorPaletteEventHandler Click;
        public event ColorPaletteEventHandler SelectionChanged;

        // Properties
        protected int _boxHeight = 16;
        protected int _boxWidth = 16;
        protected int _columns = 8;
        protected Palette _colorSet = Palette.Basic;
        protected int _margins = 2;
        protected int _visibleRows = 0;

        /// <summary>
        /// Gets or sets the height of each palette item, in pixels.
        /// </summary>
        [Description("Indicates the height of each palette item in pixels.")]
        [DefaultValue(16)]
        public int BoxHeight
        {
            get { return _boxHeight; }
            set { _boxHeight = value; UpdateLayout(); }
        }

        /// <summary>
        /// Gets or sets the width of each palette item, in pixels.
        /// </summary>
        [Description("Indicates the width of each palette item in pixels.")]
        [DefaultValue(16)]
        public int BoxWidth
        {
            get { return _boxWidth; }
            set { _boxWidth = value; UpdateLayout(); }
        }

        /// <summary>
        /// Gets or sets the number of palette items per row.
        /// </summary>
        [Description("Determines the number of palette items per row.")]
        [DefaultValue(8)]
        public int Columns
        {
            get { return _columns; }
            set { _columns = value; UpdateLayout(); }
        }

        /// <summary>
        /// Gets or sets the initial color palette.
        /// </summary>
        [Description("Specifies the control's initial palette.")]
        [DefaultValue(Palette.Basic)]
        public Palette DefaultPalette
        {
            get { return _colorSet; }
            set
            {
                _colorSet = value;
                if (IsHandleCreated)
                    SetDefaultPalette();
                UpdateLayout();
            }
        }

        /// <summary>
        /// Gets or sets the distance between palette items, in pixels.
        /// </summary>
        [Description("Determines the distance between palette items in pixels.")]
        [DefaultValue(2)]
        public int Margins
        {
            get { return _margins; }
            set { _margins = value; UpdateLayout(); }
        }

        /// <summary>
        /// Gets or sets the maximum number of visible rows. Scrolling is enabled if this
        /// number is less than the total number of rows. Use 0 to display all rows.
        /// </summary>
        [Description("Indicates the maximum number of visible rows. Use 0 to display all rows.")]
        [DefaultValue(0)]
        public int VisibleRows
        {
            get { return _visibleRows; }
            set { _visibleRows = value; UpdateLayout(); }
        }

        /// <summary>
        /// Gets or sets the currently selected color.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color SelectedColor
        {
            get
            {
                if (_selected >= 0 && _selected < ColorItems.Count)
                    return ColorItems[_selected];
                return Color.Black;
            }
            set
            {
                for (int i = 0; i < ColorItems.Count; i++)
                {
                    if (ColorItems[i] == value)
                    {
                        SetSelected(i);
                        return;
                    }
                }
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Color> ColorItems = new List<Color>();

        // Private data
        private int _selected;
        private int _rows;
        private bool _autoScroll = false;

        // Construction
        public ColorPalette()
        {
            _selected = -1;

            // Enable AutoScroll. Set up in UpdateLayout()
            AutoScroll = true;
            // Necessary for ToolStripDropDown
            AutoSize = true;
            // Provides slightly smoother updates at the
            // cost of using more resources
            DoubleBuffered = true;
        }

        // Handle control creation
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Populate default palette
            SetDefaultPalette();

            // Update layout
            UpdateLayout();
        }

        /// <summary>
        /// Initializes AutoScroll according to current settings and palette.
        /// Sizes control if AutoSize is true.
        /// </summary>
        public void UpdateLayout()
        {
            // No point doing this before control is created
            if (!IsHandleCreated)
                return;

            // Calculate width
            Size size = new Size();
            size.Width = (Columns * (BoxWidth + Margins)) + Margins;

            // Calculate height
            _rows = ColorItems.Count / Columns;
            if ((ColorItems.Count % Columns) != 0)
                _rows++;	// Partial row
            size.Height = (_rows * (BoxHeight + Margins)) + Margins;

            // Set up AutoScroll
            if (VisibleRows > 0 && VisibleRows < _rows)
            {
                // Enable scrolling
                _autoScroll = true;
                AutoScrollMinSize = new Size(size.Width, size.Height);
                AutoScrollPosition = new Point(0, 0);
                VerticalScroll.SmallChange = (BoxHeight + Margins);

                // Calculate control height
                size.Height -= ((_rows - VisibleRows) * (BoxHeight + Margins));
                size.Width += SystemInformation.VerticalScrollBarWidth;
            }
            else
            {
                // No scrolling
                _autoScroll = false;
                AutoScrollMinSize = size;
            }

            // Size control to fit content
            if (AutoSize)
                SetClientSizeCore(size.Width, size.Height);

            Invalidate();
        }

        /// <summary>
        /// Populates the current palette with the current default palette
        /// </summary>
        protected void SetDefaultPalette()
        {
            // Clear any existing items
            ColorItems.Clear();

            // Set current default palette
            switch (DefaultPalette)
            {
                case Palette.Basic:
                    ColorItems.Add(Color.Black);
                    ColorItems.Add(Color.Navy);
                    ColorItems.Add(Color.Green);
                    ColorItems.Add(Color.Teal);
                    ColorItems.Add(Color.Maroon);
                    ColorItems.Add(Color.Purple);
                    ColorItems.Add(Color.Olive);
                    ColorItems.Add(Color.Gray);
                    ColorItems.Add(Color.Silver);
                    ColorItems.Add(Color.Blue);
                    ColorItems.Add(Color.Lime);
                    ColorItems.Add(Color.Aqua);
                    ColorItems.Add(Color.Red);
                    ColorItems.Add(Color.Fuchsia);
                    ColorItems.Add(Color.Yellow);
                    ColorItems.Add(Color.White);
                    break;

                case Palette.Extended:
                    SetExtendedPalette(64);
                    break;

                case Palette.Extended2:
                    SetExtendedPalette(32);
                    break;

                case Palette.Extended3:
                    SetExtendedPalette(16);
                    break;
            }
        }

        // Helper for SetDefaultPalette
        protected void SetExtendedPalette(int step)
        {
            int red, green, blue;

            for (red = 0; red <= 256; red += step)
            {
                if (red == 256) red--;
                for (green = 0; green <= 256; green += step)
                {
                    if (green == 256) green--;
                    for (blue = 0; blue <= 256; blue += step)
                    {
                        if (blue == 256) blue--;
                        ColorItems.Add(Color.FromArgb(red, green, blue));
                    }
                }
            }
        }

        /// <summary>
        /// Displays a color dialog box to allow the user to select a
        /// custom color. Raises the Click event if a color is selected.
        /// </summary>
        public void ShowColorDialog()
        {
            // Create color dialog
            ColorDialog dlg = new ColorDialog();
            dlg.Color = SelectedColor;
            dlg.FullOpen = true;

            // Show dialog
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Set selected color
                RaiseClickEvent(dlg.Color);
            }
        }

        // Highlight color under mouse
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int i = PointToIndex(Cursor.Position);
            if (i >= 0)
                SetSelected(i);
            base.OnMouseMove(e);
        }

        // Choose color under mouse
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int index = PointToIndex(Cursor.Position);
            if (index >= 0)
                RaiseClickEvent(ColorItems[index]);
        }

        // Process keystrokes
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int index = _selected;
            switch (keyData)
            {
                case Keys.Enter:
                    if (_selected >= 0)
                        RaiseClickEvent();
                    break;
                case Keys.Down:
                    if (index == -1)
                        index = 0;
                    else if (index + Columns < ColorItems.Count)
                        index += Columns;
                    break;
                case Keys.Up:
                    if (index == -1)
                        index = ColorItems.Count - 1;
                    else if (index - Columns >= 0)
                        index -= Columns;
                    break;
                case Keys.Right:
                    if (index == -1)
                        index = 0;
                    else if (index < (ColorItems.Count - 1))
                        index++;
                    break;
                case Keys.Left:
                    if (index == -1)
                        index = ColorItems.Count - 1;
                    else if (index > 0)
                        index--;
                    break;
                case Keys.PageUp:
                    if (index == -1)
                        index = ColorItems.Count - 1;
                    else
                    {
                        int rows = (_visibleRows != 0) ? _visibleRows : _rows;
                        index = Math.Max(0, index - (Columns * rows));
                    }
                    break;
                case Keys.PageDown:
                    if (index == -1)
                        index = 0;
                    else
                    {
                        int rows = (_visibleRows != 0) ? _visibleRows : _rows;
                        index = Math.Min(ColorItems.Count - 1, index + (Columns * rows));
                    }
                    break;
                case Keys.Home:
                    index = 0;
                    break;
                case Keys.End:
                    index = ColorItems.Count - 1;
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            // Update selection if it's changed
            SetSelected(index, true);
            return true;
        }

        // Paint palette
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = new Rectangle();

            for (int i = 0; i < ColorItems.Count; i++)
            {
                // Get rectangle for this item
                IndexToRectangle(i, ref rect);

                // Paint item if within clipping rectangle
                if (e.ClipRectangle.IntersectsWith(rect))
                {
                    // Tweak needed to keep rectangle expected size
                    rect.Width--;
                    rect.Height--;

                    // Fill with this color
                    e.Graphics.FillRectangle(new SolidBrush(ColorItems[i]), rect);
                    if (i == _selected)
                    {
                        // Draw selected border around box
                        e.Graphics.DrawRectangle(Pens.Red, rect);
                        rect.Inflate(-1, -1);
                        e.Graphics.DrawRectangle(Pens.White, rect);
                    }
                    else
                    {
                        // Draw border around box
                        e.Graphics.DrawRectangle(Pens.Gray, rect);
                    }
                }
            }
            base.OnPaint(e);
        }

        #region Helper Methods

        /// <summary>
        /// Raises the Click event with the currently selected color
        /// </summary>
        protected void RaiseClickEvent()
        {
            RaiseClickEvent(SelectedColor);
        }

        /// <summary>
        /// Raises the Click event with the specified color
        /// </summary>
        /// <param name="color">Color to pass to Click event handlers</param>
        protected void RaiseClickEvent(Color color)
        {
            if (Click != null)
            {
                ColorPickerEventArgs args = new ColorPickerEventArgs();
                args.Value = color;
                Click(this, args);
            }
        }

        /// <summary>
        /// Updates the selected palette item.
        /// </summary>
        /// <param name="index">New palette index</param>
        protected void SetSelected(int index)
        {
            SetSelected(index, false);
        }

        /// <summary>
        /// Updates the selected palette item.
        /// </summary>
        /// <param name="index">New palette index</param>
        /// <param name="scrollIntoView">Control will be scrolled so
        /// that new item is fully visible when true</param>
        protected void SetSelected(int index, bool scrollIntoView)
        {
            // Something to do only if item has changed
            if (index != _selected)
            {
                // Get update region
                if (_selected >= 0)
                    Invalidate(IndexToRectangle(_selected));
                if (index >= 0)
                {
                    Rectangle rect = IndexToRectangle(index);
                    Invalidate(rect);

                    // Scroll if needed
                    if (_autoScroll && scrollIntoView)
                    {
                        if (rect.Top < 0)
                        {
                            // Scroll up
                            Point point = new Point(0, -AutoScrollPosition.Y);
                            point.Y += (rect.Top - Margins);
                            AutoScrollPosition = point;
                        }
                        else if (rect.Bottom >= ClientRectangle.Bottom)
                        {
                            // Scroll down
                            Point point = new Point(0, -AutoScrollPosition.Y);
                            point.Y += ((rect.Bottom - ClientRectangle.Bottom) + Margins);
                            AutoScrollPosition = point;
                        }
                    }
                }

                // Update selected index
                _selected = index;

                // Raise SelectionChanged event
                if (SelectionChanged != null && _selected >= 0)
                {
                    ColorPickerEventArgs args = new ColorPickerEventArgs();
                    args.Value = ColorItems[_selected];
                    SelectionChanged(this, args);
                }
            }
        }

        /// <summary>
        /// Returns the rectangle that corresponds to the
        /// given palette item
        /// </summary>
        /// <param name="index">Palette index</param>
        protected Rectangle IndexToRectangle(int index)
        {
            Rectangle rect = new Rectangle();
            IndexToRectangle(index, ref rect);
            return rect;
        }

        /// <summary>
        /// Calculates the rectangle that corresponds to the
        /// given palette item.
        /// </summary>
        /// <param name="index">Palette index</param>
        /// <param name="rect">Returns the calculated rectangle</param>
        protected void IndexToRectangle(int index, ref Rectangle rect)
        {
            // Calculate rectangle for index
            int y = index / Columns;
            int x = index % Columns;
            rect.X = Margins + (x * (BoxWidth + Margins));
            rect.Y = Margins + (y * (BoxHeight + Margins));
            rect.Width = BoxWidth;
            rect.Height = BoxHeight;

            // Adjust for scrolling
            rect.Offset(0, AutoScrollPosition.Y);
        }

        /// <summary>
        /// Returns the index of the palette item that corresponds
        /// to the given point. Returns -1 if the point is not
        /// over an item.
        /// </summary>
        /// <param name="point">Point in screen coordinates</param>
        /// <returns></returns>
        protected int PointToIndex(Point point)
        {
            point = PointToClient(point);
            if (ClientRectangle.Contains(point))
            {
                // Adjust for scrolling
                point.Offset(0, -AutoScrollPosition.Y);

                // Calculate index under point
                int x = point.X - Margins; int cx = BoxWidth + Margins;
                int y = point.Y - Margins; int cy = BoxHeight + Margins;
                if (x >= 0 && (x % cx) < BoxWidth &&
                    y >= 0 && (y % cy) < BoxHeight)
                {
                    x /= cx;
                    if (x < Columns)
                    {
                        int i = ((y / cy) * Columns) + x;
                        if (i >= 0 && i < ColorItems.Count)
                            return i;
                    }
                }
            }
            return -1;
        }

        #endregion

    }

    public class ColorPickerEventArgs : EventArgs
    {
        public Color Value;
    }
}
