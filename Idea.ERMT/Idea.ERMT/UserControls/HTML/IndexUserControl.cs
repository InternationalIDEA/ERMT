using System;
using System.Windows.Forms;
using Idea.Facade;
using Idea.Utils;

namespace Idea.ERMT.UserControls
{
    public enum IndexContentType
    {
        ElectoralCycle,
        KnowledgeResources,
        KnowledgeResourcesModel
    }

    public partial class IndexUserControl : ERMTUserControl
    {


        bool _printOnOpen;
        bool _showBackForward = true;

        public IndexContentType IndexContentType { get; set; }

        public IndexUserControl()
        {
            LoadControls();
            wbIndex.DocumentCompleted += PrintDocument;
        }

        private void IndexUserControl_Load(object sender, EventArgs e)
        {
            ShowTitle();
        }

        private void LoadControls()
        {
            btnBack.Image = ResourceHelper.GetResourceImage("ERMTleft40x40");
            btnForward.Image = ResourceHelper.GetResourceImage("ERMTright40x40");
            btnPrint.Image = ResourceHelper.GetResourceImage("ERMTprint40x40");
            ttIUC.SetToolTip(btnPrint,ResourceHelper.GetResourceText("Print"));
            ttIUC.SetToolTip(btnBack, ResourceHelper.GetResourceText("Back"));
            ttIUC.SetToolTip(btnForward, ResourceHelper.GetResourceText("Forward"));
        }

        public bool ShowBackForward
        {
            get
            {
                return _showBackForward;
            }
            set
            {
                _showBackForward = value;
                btnBack.Visible = _showBackForward;
                btnForward.Visible = _showBackForward;
            }
        }

        public void ShowHtml()
        {
            Uri uri = new Uri("file:///" + DirectoryAndFileHelper.ClientHTMLFolder + "PMM\\FirstScreen.htm");
            switch (IndexContentType)
            {
                case IndexContentType.ElectoralCycle:
                {
                    uri = new Uri("file:///" + DirectoryAndFileHelper.ClientHTMLFolder + "PMM\\FirstScreen.htm");
                    break;
                }
                case IndexContentType.KnowledgeResources:
                {
                    DocumentHelper.DownloadFiles();
                    uri = new Uri("file:///" + DirectoryAndFileHelper.ClientHTMLFolder + "Index-base.htm");
                    break;
                }
                case IndexContentType.KnowledgeResourcesModel:
                {
                    DocumentHelper.DownloadFilesCurrentModel();
                    uri = new Uri("file:///" + DirectoryAndFileHelper.ClientHTMLFolder + "IndexModel.htm");
                    break;
                }
            }
            
            _printOnOpen = false;
            wbIndex.Navigate(uri);
        }

        public void ShowHtml(string htmlUrl)
        {
            _printOnOpen = false;
            wbIndex.Navigate(htmlUrl);
        }

        public void ShowHtmlAndPrint(string htmlUrl)
        {
            _printOnOpen = true;
            wbIndex.Navigate(htmlUrl);
        }

        private void HistoryBackWebBrowser()
        {
            wbIndex.GoBack();
        }

        private void HistoryForwardWebBrowser()
        {
            wbIndex.GoForward();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            HistoryBackWebBrowser();
        }
        private void btnForward_Click(object sender, EventArgs e)
        {
            HistoryForwardWebBrowser();
        }
        
        public override void ShowTitle()
        {
            switch (IndexContentType)
            {
                case IndexContentType.ElectoralCycle:
                {
                    ViewManager.ShowTitle(ResourceHelper.GetResourceText("ElectoralCycle"));
                    break;
                }
                case IndexContentType.KnowledgeResources:
                {
                    ViewManager.ShowTitle(ResourceHelper.GetResourceText("KnowledgeResources"));
                    break;
                }
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public override void Print()
        {
            wbIndex.ShowPrintDialog();
        }

        private void PrintDocument(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (_printOnOpen)
            {
                ((WebBrowser)sender).ShowPrintDialog();
            }
        }

        
    }
}
