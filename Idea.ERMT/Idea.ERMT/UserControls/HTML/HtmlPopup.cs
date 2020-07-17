using System;
using System.Windows.Forms;

namespace Idea.ERMT.UserControls
{
    public partial class HtmlPopup : Form
    {
        readonly string _url = string.Empty;
        readonly bool _printOnOpen;
        bool _showBackForward = true;

        //public string Url
        //{
        //    get
        //    {
        //        return _url;
        //    }
        //    set
        //    {
        //        _url = value;
        //        if (_url != string.Empty)
        //        {
        //            indexUserControl1.ShowHtml(_url);
        //        }
        //    }
        //}

        public bool ShowBackForward
        {
            get
            {
                return _showBackForward;
            }
            set
            {
                indexUserControl1.ShowBackForward = value;
                _showBackForward = value;
            }
        }

        public HtmlPopup()
        {
            InitializeComponent();
        }

        public HtmlPopup(string url)
        {
            InitializeComponent();
            _url = url;
            _printOnOpen = false;
        }

        public HtmlPopup(string url, bool printOnOpen)
        {
            InitializeComponent();
            _url = url;
            _printOnOpen = printOnOpen;
        }

        private void HtmlPopup_Load(object sender, EventArgs e)
        {
            if (_url != string.Empty)
            {
                if (_printOnOpen)
                {
                    indexUserControl1.ShowHtmlAndPrint(_url);
                }
                else
                {
                    indexUserControl1.ShowHtml(_url);
                }
            }
        }
    }
}
