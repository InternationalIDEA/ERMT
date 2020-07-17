using System;
using System.Drawing;
using System.Windows.Forms;
using Idea.Entities;

namespace Idea.ERMT.UserControls
{
    public partial class MarkerPickForm : Form
    {
        public MarkerPickForm()
        {
            InitializeComponent();
        }

        public string Title
        {
            get
            {
                return markerPick1.Title;
            }
            set
            {
                markerPick1.Title = value;
            }
        }

        public Color TitleColor
        {
            get { return markerPick1.TitleColor; }
            set { markerPick1.TitleColor = value; }
        }

        public string TextContent
        {
            get
            {
                return markerPick1.TextContent;
            }
            set
            {
                markerPick1.TextContent = value;
            }
        }

        public MarkerType MarkerType
        {
            get
            {
                return markerPick1.MarkerType;
            }
            set
            {
                markerPick1.MarkerType = value;
            }
        }

        public DateTime From
        {
            get
            {
                return markerPick1.From;
            }
            set
            {
                markerPick1.From = value;
            }
        }

        public DateTime To
        {
            get
            {
                return markerPick1.To;
            }
            set
            {
                markerPick1.To = value;
            }
        }

        public decimal Latitude
        {
            get
            {
                return markerPick1.Latitude;
            }
            set
            {
                markerPick1.Latitude = value;
            }
        }

        public decimal Longitude
        {
            get
            {
                return markerPick1.Longitude;
            }
            set
            {
                markerPick1.Longitude = value;
            }
        }
    }
}
