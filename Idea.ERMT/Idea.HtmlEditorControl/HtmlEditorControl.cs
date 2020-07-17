using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using Idea.Facade;
using Microsoft.ConsultingServices.HtmlEditor;
using HtmlDocument = mshtml.HTMLDocument;
using HtmlBody = mshtml.HTMLBody;
using HtmlStyleSheet = mshtml.IHTMLStyleSheet;
using HtmlStyle = mshtml.IHTMLStyle;
using HtmlDomNode = mshtml.IHTMLDOMNode;
using HtmlDomTextNode = mshtml.IHTMLDOMTextNode;
using HtmlTextRange = mshtml.IHTMLTxtRange;
using HtmlSelection = mshtml.IHTMLSelectionObject;
using HtmlControlRange = mshtml.IHTMLControlRange;

using HtmlElement = mshtml.IHTMLElement;
using HtmlElementCollection = mshtml.IHTMLElementCollection;
using HtmlControlElement = mshtml.IHTMLControlElement;
using HtmlAnchorElement = mshtml.IHTMLAnchorElement;
using HtmlImageElement = mshtml.IHTMLImgElement;
using HtmlFontElement = mshtml.IHTMLFontElement;
using HtmlLineElement = mshtml.IHTMLHRElement;
using HtmlSpanElement = mshtml.IHTMLSpanFlow;
using HtmlScriptElement = mshtml.IHTMLScriptElement;

using HtmlTable = mshtml.IHTMLTable;
using HtmlTableCaption = mshtml.IHTMLTableCaption;
using HtmlTableRow = mshtml.IHTMLTableRow;
using HtmlTableCell = mshtml.IHTMLTableCell;
using HtmlTableRowMetrics = mshtml.IHTMLTableRowMetrics;
using HtmlTableColumn = mshtml.IHTMLTableCol;

using HtmlEventObject = mshtml.IHTMLEventObj;

using Microsoft.ConsultingServices.COM;
using Microsoft.ConsultingServices.COM.IOleCommandTarget;


namespace Microsoft.ConsultingServices
{
    /// <summary>
    /// This is the main UserControl class that defines the Html Editor
    /// BodyHtml sets the complet Body including the body tag
    /// InnerText and InnerHtml sets the contents of the Body
    /// Property ReadOnly defines whether the content is editable
    /// </summary>
    [DefaultProperty("InnerText")]
    public sealed class HtmlEditorControl : UserControl
    {
        #region Public Events

        // public event that is raised if an internal processing exception is found
        [Category("Exception"), Description("An Internal Processing Exception was encountered")]
        public event HtmlExceptionEventHandler HtmlException;

        // public event that is raised if navigation event is captured
        [Category("Navigation"), Description("A Navigation Event was encountered")]
        public event HtmlNavigationEventHandler HtmlNavigation;

        #endregion

        #region Constant Defintions

        // general constants
        private const int HTML_BUFFER_SIZE = 256;

        // define the tags being used by the application
        private const string BODY_TAG = "BODY";
        private const string SCRIPT_TAG = "SCRIPT";
        private const string ANCHOR_TAG = "A";
        private const string FONT_TAG = "FONT";
        private const string BOLD_TAG = "STRONG";
        private const string UNDERLINE_TAG = "U";
        private const string ITALIC_TAG = "EM";
        private const string STRIKE_TAG = "STRIKE";
        private const string SUBSCRIPT_TAG = "SUB";
        private const string SUPERSCRIPT_TAG = "SUP";
        private const string HEAD_TAG = "HEAD";
        private const string IMAGE_TAG = "IMG";
        private const string TABLE_TAG = "TABLE";
        private const string TABLE_ROW_TAG = "TR";
        private const string TABLE_CELL_TAG = "TD";
        private const string TABLE_HEAD_TAG = "TH";
        private const string SPAN_TAG = "SPAN";
        private const string OPEN_TAG = "<";
        private const string CLOSE_TAG = ">";
        private const string SELECT_TYPE_TEXT = "text";
        private const string SELECT_TYPE_CONTROL = "control";
        private const string SELECT_TYPE_NONE = "none";
        private const string FORMATTED_PRE = "Formatted";
        private const string FORMATTED_NORMAL = "Normal";
        private const string FORMATTED_HEADING = "Heading";
        private const string EVENT_CONTEXT_MENU = "contextmenu";

        // define commands for mshtml execution execution
        private const string HTML_COMMAND_OVERWRITE = "OverWrite";
        private const string HTML_COMMAND_BOLD = "Bold";
        private const string HTML_COMMAND_UNDERLINE = "Underline";
        private const string HTML_COMMAND_ITALIC = "Italic";
        private const string HTML_COMMAND_SUBSCRIPT = "Subscript";
        private const string HTML_COMMAND_SUPERSCRIPT = "Superscript";
        private const string HTML_COMMAND_STRIKE_THROUGH = "StrikeThrough";
        private const string HTML_COMMAND_FONT_NAME = "FontName";
        private const string HTML_COMMAND_FONT_SIZE = "FontSize";
        private const string HTML_COMMAND_FORE_COLOR = "ForeColor";
        private const string HTML_COMMAND_INSERT_FORMAT_BLOCK = "FormatBlock";
        private const string HTML_COMMAND_REMOVE_FORMAT = "RemoveFormat";
        private const string HTML_COMMAND_JUSTIFY_LEFT = "JustifyLeft";
        private const string HTML_COMMAND_JUSTIFY_CENTER = "JustifyCenter";
        private const string HTML_COMMAND_JUSTIFY_RIGHT = "JustifyRight";
        private const string HTML_COMMAND_JUSTIFY_FULL = "JustifyFull";
        private const string HTML_COMMAND_INDENT = "Indent";
        private const string HTML_COMMAND_OUTDENT = "Outdent";
        private const string HTML_COMMAND_INSERT_LINE = "InsertHorizontalRule";
        private const string HTML_COMMAND_INSERT_LIST = "Insert{0}List"; // replace with (Un)Ordered
        private const string HTML_COMMAND_INSERT_IMAGE = "InsertImage";
        private const string HTML_COMMAND_INSERT_LINK = "CreateLink";
        private const string HTML_COMMAND_REMOVE_LINK = "Unlink";
        private const string HTML_COMMAND_TEXT_CUT = "Cut";
        private const string HTML_COMMAND_TEXT_COPY = "Copy";
        private const string HTML_COMMAND_TEXT_PASTE = "Paste";
        private const string HTML_COMMAND_TEXT_DELETE = "Delete";
        private const string HTML_COMMAND_TEXT_UNDO = "Undo";
        private const string HTML_COMMAND_TEXT_REDO = "Redo";
        private const string HTML_COMMAND_TEXT_SELECT_ALL = "SelectAll";
        private const string HTML_COMMAND_TEXT_UNSELECT = "Unselect";
        private const string HTML_COMMAND_TEXT_PRINT = "Print";

        // internal command constants
        private const string INTERNAL_COMMAND_TEXTCUT = "TextCut";
        private const string INTERNAL_COMMAND_TEXTCOPY = "TextCopy";
        private const string INTERNAL_COMMAND_TEXTPASTE = "TextPaste";
        private const string INTERNAL_COMMAND_TEXTDELETE = "TextDelete";
        private const string INTERNAL_COMMAND_CLEARSELECT = "ClearSelect";
        private const string INTERNAL_COMMAND_SELECTALL = "SelectAll";
        private const string INTERNAL_COMMAND_EDITUNDO = "EditUndo";
        private const string INTERNAL_COMMAND_EDITREDO = "EditRedo";
        private const string INTERNAL_COMMAND_FORMATBOLD = "FormatBold";
        private const string INTERNAL_COMMAND_FORMATUNDERLINE = "FormatUnderline";
        private const string INTERNAL_COMMAND_FORMATITALIC = "FormatItalic";
        private const string INTERNAL_COMMAND_FORMATSUPERSCRIPT = "FormatSuperscript";
        private const string INTERNAL_COMMAND_FORMATSUBSCRIPT = "FormatSubscript";
        private const string INTERNAL_COMMAND_FORMATSTRIKEOUT = "FormatStrikeout";
        private const string INTERNAL_COMMAND_FONTDIALOG = "FontDialog";
        private const string INTERNAL_COMMAND_FONTNORMAL = "FontNormal";
        private const string INTERNAL_COMMAND_COLORDIALOG = "ColorDialog";
        private const string INTERNAL_COMMAND_FONTINCREASE = "FontIncrease";
        private const string INTERNAL_COMMAND_FONTDECREASE = "FontDecrease";
        private const string INTERNAL_COMMAND_JUSTIFYLEFT = "JustifyLeft";
        private const string INTERNAL_COMMAND_JUSTIFYCENTER = "JustifyCenter";
        private const string INTERNAL_COMMAND_JUSTIFYRIGHT = "JustifyRight";
        private const string INTERNAL_COMMAND_JUSTIFY_FULL = "JustifyFull";
        private const string INTERNAL_COMMAND_FOOTNOTE = "Footnote";
        private const string INTERNAL_COMMAND_FONTINDENT = "FontIndent";
        private const string INTERNAL_COMMAND_FONTOUTDENT = "FontOutdent";
        private const string INTERNAL_COMMAND_LISTORDERED = "ListOrdered";
        private const string INTERNAL_COMMAND_LISTUNORDERED = "ListUnordered";
        private const string INTERNAL_COMMAND_INSERTLINE = "InsertLine";
        private const string INTERNAL_COMMAND_INSERTTABLE = "InsertTable";
        private const string INTERNAL_COMMAND_TABLEPROPERTIES = "TableModify";
        private const string INTERNAL_COMMAND_TABLEINSERTROW = "TableInsertRow";
        private const string INTERNAL_COMMAND_TABLEDELETEROW = "TableDeleteRow";
        private const string INTERNAL_COMMAND_INSERTIMAGE = "InsertImage";
        private const string INTERNAL_COMMAND_INSERTLINK = "InsertLink";
        private const string INTERNAL_COMMAND_INSERTTEXT = "InsertText";
        private const string INTERNAL_COMMAND_INSERTHTML = "InsertHtml";
        private const string INTERNAL_COMMAND_FINDREPLACE = "FindReplace";
        private const string INTERNAL_COMMAND_DOCUMENTPRINT = "DocumentPrint";
        private const string INTERNAL_COMMAND_OPENFILE = "OpenFile";
        private const string INTERNAL_COMMAND_SAVEFILE = "SaveFile";
        private const string INTERNAL_TOGGLE_OVERWRITE = "ToggleOverwrite";
        private const string INTERNAL_TOGGLE_TOOLBAR = "ToggleToolbar";
        private const string INTERNAL_TOGGLE_SCROLLBAR = "ToggleScrollbar";
        private const string INTERNAL_TOGGLE_WORDWRAP = "ToggleWordwrap";

        // browser html constan expressions
        private const string EMPTY_SPACE = @"&nbsp;";
        private const string BLANK_HTML_PAGE = "about:blank";
        private const string TARGET_WINDOW_NEW = "_BLANK";
        private const string TARGET_WINDOW_SAME = "_SELF";

        // constants for displaying the HTML dialog
        private const string HTML_TITLE_EDIT = "Edit Html";
        private const string HTML_TITLE_VIEW = "View Html";
        private const string PASTE_TITLE_HTML = "Enter Html";
        private const string PASTE_TITLE_TEXT = "Enter Text";
        private const string HTML_TITLE_OPENFILE = "Open Html File";
        private const string HTML_TITLE_SAVEFILE = "Save Html File";
        private const string HTML_FILTER = "Html files (*.html,*.htm)|*.html;*htm|All files (*.*)|*.*";
        private const string HTML_EXTENSION = "html";
        private const string CONTENT_EDITABLE_INHERIT = "inherit";
        private const string DEFAULT_HTML_TEXT = "";

        // constants for regular expression work
        // BODY_INNER_TEXT_PARSE = @"(<)/*\w*/*(>)";
        // HREF_TEST_EXPRESSION = @"(http|ftp|https):\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?";
        // BODY_PARSE_EXPRESSION = @"(?<preBody>.*)(?<bodyOpen><body.*?>)(?<innerBody>.*)(?<bodyClose></body>)(?<afterBody>.*)";
        private const string HREF_TEST_EXPRESSION = @"mailto\:|(news|(ht|f)tp(s?))\:\/\/[\w]+(.[\w]+)([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?";
        private const string BODY_PARSE_PRE_EXPRESSION = @"(<body).*?(</body)";
        private const string BODY_PARSE_EXPRESSION = @"(?<bodyOpen>(<body).*?>)(?<innerBody>.*?)(?<bodyClose>(</body\s*>))";
        private const string BODY_DEFAULT_TAG = @"<Body></Body>";
        private const string BODY_TAG_PARSE_MATCH = @"${bodyOpen}${bodyClose}";
        private const string BODY_INNER_PARSE_MATCH = @"${innerBody}";
        private const string CONTENTTYPE_PARSE_EXPRESSION = @"^(?<mainType>\w+)(\/?)(?<subType>\w*)((\s*;\s*charset=)?)(?<charSet>.*)";
        private const string CONTENTTYPE_PARSE_MAINTYPE = @"${mainType}";
        private const string CONTENTTYPE_PARSE_SUBTYPE = @"${subType}";
        private const string CONTENTTYPE_PARSE_CHARSET = @"${charSet}";

        #endregion

        # region Initialization and Dispose Code

        // browser constants and commands
        private object EMPTY_PARAMETER;

        // acceptable formatting commands
        // in case order to enable binary search
        private readonly string[] formatCommands = new String[] { "Formatted", "Heading 1", "Heading 2", "Heading 3", "Heading 4", "Heading 5", "Normal" };

        // document and body elements
        private HtmlDocument document;
        private HtmlBody body;
        private HtmlStyleSheet stylesheet;
        private HtmlScriptElement script;
        private volatile bool loading = false;
        private volatile bool codeNavigate = false;
        private volatile bool rebaseUrlsNeeded = false;

        // default values used to reset values
        private Color _defaultBackColor;
        private Color _defaultForeColor;
        private HtmlFontProperty _defaultFont;

        // internal property values
        private bool _readOnly;
        private bool _toolbarVisible;
        private bool _enableVisualStyles;
        private DockStyle _toolbarDock;
        private string _bodyText;
        private string _bodyHtml;
        private string _bodyUrl;

        // internal body property values
        private Color _bodyBackColor;
        private Color _bodyForeColor;
        private HtmlFontProperty _bodyFont;
        private int[] _customColors;
        private string _imageDirectory;
        private string _htmlDirectory;
        private NavigateActionOption _navigateWindow;
        private DisplayScrollBarOption _scrollBars;
        private string _baseHref;
        private bool _autoWordWrap;

        // find and replace internal text range
        private HtmlTextRange _findRange;

        // winform generated code
        private AxSHDocVw.AxWebBrowser editorWebBrowser;
        private System.Windows.Forms.ImageList toolbarImageList;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.MenuItem menuTextUndo;
        private System.Windows.Forms.MenuItem menuTextRedo;
        private System.Windows.Forms.MenuItem menuTextSep1;
        private System.Windows.Forms.MenuItem menuTextCut;
        private System.Windows.Forms.MenuItem menuTextCopy;
        private System.Windows.Forms.MenuItem menuTextPaste;
        private System.Windows.Forms.MenuItem menuTextDelete;
        private System.Windows.Forms.MenuItem menuTextFont;
        private System.Windows.Forms.MenuItem menuTextFontIncrease;
        private System.Windows.Forms.MenuItem menuTextFontDecrease;
        private System.Windows.Forms.MenuItem menuTextFontBold;
        private System.Windows.Forms.MenuItem menuTextFontItalic;
        private System.Windows.Forms.MenuItem menuTextFontUnderline;
        private System.Windows.Forms.MenuItem menuTextFontIndent;
        private System.Windows.Forms.MenuItem menuTextFontOutdent;
        private System.Windows.Forms.MenuItem menuTextFontDialog;
        private System.Windows.Forms.MenuItem menuTextFontColor;
        private System.Windows.Forms.MenuItem menuTextSelectAll;
        private System.Windows.Forms.MenuItem menuJustify;
        private System.Windows.Forms.MenuItem menuJustifyLeft;
        private System.Windows.Forms.MenuItem menuJustifyCenter;
        private System.Windows.Forms.MenuItem menuJustifyRight;
        private System.Windows.Forms.MenuItem menuTextSelectNone;
        private System.Windows.Forms.MenuItem menuFormatting;
        private System.Windows.Forms.MenuItem menuTextFontSep1;
        private System.Windows.Forms.MenuItem menuTextFontSep2;
        private System.Windows.Forms.MenuItem menuTextFontSep3;
        private System.Windows.Forms.MenuItem menuTextFontSuperscript;
        private System.Windows.Forms.MenuItem menuTextFontSubscript;
        private System.Windows.Forms.MenuItem menuInsert;
        private System.Windows.Forms.MenuItem menuInsertLine;
        private System.Windows.Forms.MenuItem menuInsertLink;
        private System.Windows.Forms.MenuItem menuInsertImage;
        private System.Windows.Forms.MenuItem menuInsertText;
        private System.Windows.Forms.MenuItem menuInsertHtml;
        private System.Windows.Forms.MenuItem menuInsertTable;
        private System.Windows.Forms.MenuItem menuMainSep1;
        private System.Windows.Forms.MenuItem menuMainSep2;
        private System.Windows.Forms.MenuItem menuText;
        private System.Windows.Forms.MenuItem menuTableModify;
        private System.Windows.Forms.ContextMenu contextMenuMain;
        private System.Windows.Forms.MenuItem menuTextSep3;
        private System.Windows.Forms.MenuItem menuTextSep2;
        private System.Windows.Forms.MenuItem menuTextFindReplace;
        private System.Windows.Forms.MenuItem menuDocument;
        private System.Windows.Forms.MenuItem menuDocumentOpen;
        private System.Windows.Forms.MenuItem menuDocumentSave;
        private System.Windows.Forms.MenuItem menuDocumentSep1;
        private System.Windows.Forms.MenuItem menuDocumentPrint;
        private System.Windows.Forms.MenuItem menuTextFontNormal;
        private System.Windows.Forms.MenuItem menuTextFontStrikeout;
        private System.Windows.Forms.MenuItem menuTextFontListOrdered;
        private System.Windows.Forms.MenuItem menuTextFontListUnordered;
        private System.Windows.Forms.MenuItem menuDocumentSep2;
        private System.Windows.Forms.MenuItem menuDocumentToolbar;
        private System.Windows.Forms.MenuItem menuDocumentScrollbar;
        private System.Windows.Forms.MenuItem menuDocumentWordwrap;
        private System.Windows.Forms.MenuItem menuDocumentOverwrite;
        private System.Windows.Forms.MenuItem menuTableProperties;
        private System.Windows.Forms.MenuItem menuTableInsertRow;
        private ToolBarButton toolBarFindReplace;
        private ToolBarButton toolBarEditSep6;
        private ToolBarButton toolBarInsertLink;
        private ToolBarButton toolBarInsertImage;
        private ToolBarButton toolBarInsertTable;
        private ToolBarButton toolBarInsertLine;
        private ToolBarButton toolBarEditSep5;
        private ToolBarButton toolBarListUnordered;
        private ToolBarButton toolBarListOrdered;
        private ToolBarButton toolBarSuperscript;
        private ToolBarButton toolBarFootnote;
        private ToolBarButton toolBarEditSep4;
        private ToolBarButton toolBarButton1;
        private ToolBarButton toolBarJustRight;
        private ToolBarButton toolBarJustCenter;
        private ToolBarButton toolBarJustLeft;
        private ToolBarButton toolBarEditSep3;
        private ToolBarButton toolBarItalic;
        private ToolBarButton toolBarUnderline;
        private ToolBarButton toolBarBold;
        private ToolBarButton toolBarEditSep2;
        private ToolBarButton toolBarRedo;
        private ToolBarButton toolBarUndo;
        private ToolBarButton toolBarEditSep1;
        private ToolBarButton toolBarPaste;
        private ToolBarButton toolBarCopy;
        private ToolBarButton toolBarCut;
        private ToolBar editorToolbar;
        private System.Windows.Forms.MenuItem menuTableDeleteRow;


        public HtmlEditorControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            
            // define the context menu for format commands
            DefineFormatBlockMenu();

            // define the default values
            // browser constants and commands
            EMPTY_PARAMETER = System.Reflection.Missing.Value;

            // default values used to reset values
            _defaultBackColor = Color.White;
            _defaultForeColor = Color.Black;
            _defaultFont = new HtmlFontProperty(this.Font);

            // set browser default values to hide IE items
            this.editorWebBrowser.AddressBar = false;
            this.editorWebBrowser.MenuBar = false;
            this.editorWebBrowser.StatusBar = false;

            // obtain the underlying web browser and set options
            // SHDocVw.WebBrowser webBrowser = (SHDocVw.WebBrowser)this.editorWebBrowser.GetOcx();

            // define the default values of the properties
            _readOnly = false;
            _toolbarVisible = false;
            _enableVisualStyles = false;
            _toolbarDock = DockStyle.Bottom;
            _bodyText = DEFAULT_HTML_TEXT;
            _bodyHtml = DEFAULT_HTML_TEXT;
            _bodyBackColor = _defaultBackColor;
            _bodyForeColor = _defaultForeColor;
            _bodyFont = _defaultFont;
            _scrollBars = DisplayScrollBarOption.Auto;
            _imageDirectory = string.Empty;
            _htmlDirectory = string.Empty;
            _navigateWindow = NavigateActionOption.Default;
            _baseHref = string.Empty;
            _autoWordWrap = true;
            stylesheet = null;
            script = null;

            // define context menu state
            this.menuDocumentToolbar.Checked = true;
            this.menuDocumentScrollbar.Checked = true;
            this.menuDocumentWordwrap.Checked = true;

            // load the blank Html page to load the MsHtml object model
            BrowserCodeNavigate(BLANK_HTML_PAGE);

            // after load ensure document marked as editable
            this.ReadOnly = _readOnly;

        } //HTMLEditorControl


        // once an html docuemnt has been loaded define the internal values
        private void DefineBodyAttributes()
        {
            // define the body colors based on the new body html
            if (body.bgColor == null)
            {
                _bodyBackColor = _defaultBackColor;
            }
            else
            {
                _bodyBackColor = ColorTranslator.FromHtml((string)body.bgColor);
            }
            if (body.text == null)
            {
                _bodyForeColor = _defaultForeColor;
            }
            else
            {
                _bodyForeColor = ColorTranslator.FromHtml((string)body.text);
            }

            // define the font object based on current font of new document
            // deafult used unless a style on the body modifies the value
            HtmlStyle bodyStyle = body.style;
            if (bodyStyle != null)
            {
                string fontName = _bodyFont.Name;
                HtmlFontSize fontSize = _bodyFont.Size;
                bool fontBold = _bodyFont.Bold;
                bool fontItalic = _bodyFont.Italic;
                bool fontUnderline = _bodyFont.Underline;
                // define the font name if defined in the style
                if (bodyStyle.fontFamily != null) fontName = bodyStyle.fontFamily;
                if (bodyStyle.fontSize != null) fontSize = HtmlFontConversion.StyleSizeToHtml(bodyStyle.fontSize.ToString());
                if (bodyStyle.fontWeight != null) fontBold = HtmlFontConversion.IsStyleBold(bodyStyle.fontWeight);
                if (bodyStyle.fontStyle != null) fontItalic = HtmlFontConversion.IsStyleItalic(bodyStyle.fontStyle);
                fontUnderline = bodyStyle.textDecorationUnderline;
                // define the new font object and set the property
                _bodyFont = new HtmlFontProperty(fontName, fontSize, fontBold, fontItalic, fontUnderline);
                this.BodyFont = _bodyFont;
            }

            // define the content based on the current value
            this.ReadOnly = _readOnly;
            this.ScrollBars = _scrollBars;
            this.AutoWordWrap = _autoWordWrap;

        } //DefineBodyAttributes


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);

        } //Dispose

        #endregion

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlEditorControl));
            this.editorWebBrowser = new AxSHDocVw.AxWebBrowser();
            this.toolbarImageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuMain = new System.Windows.Forms.ContextMenu();
            this.menuTableModify = new System.Windows.Forms.MenuItem();
            this.menuTableProperties = new System.Windows.Forms.MenuItem();
            this.menuTableInsertRow = new System.Windows.Forms.MenuItem();
            this.menuTableDeleteRow = new System.Windows.Forms.MenuItem();
            this.menuText = new System.Windows.Forms.MenuItem();
            this.menuTextUndo = new System.Windows.Forms.MenuItem();
            this.menuTextRedo = new System.Windows.Forms.MenuItem();
            this.menuTextSep1 = new System.Windows.Forms.MenuItem();
            this.menuTextCut = new System.Windows.Forms.MenuItem();
            this.menuTextCopy = new System.Windows.Forms.MenuItem();
            this.menuTextPaste = new System.Windows.Forms.MenuItem();
            this.menuTextSep2 = new System.Windows.Forms.MenuItem();
            this.menuTextFindReplace = new System.Windows.Forms.MenuItem();
            this.menuTextSep3 = new System.Windows.Forms.MenuItem();
            this.menuTextSelectNone = new System.Windows.Forms.MenuItem();
            this.menuTextSelectAll = new System.Windows.Forms.MenuItem();
            this.menuTextDelete = new System.Windows.Forms.MenuItem();
            this.menuDocument = new System.Windows.Forms.MenuItem();
            this.menuDocumentOpen = new System.Windows.Forms.MenuItem();
            this.menuDocumentSave = new System.Windows.Forms.MenuItem();
            this.menuDocumentSep1 = new System.Windows.Forms.MenuItem();
            this.menuDocumentPrint = new System.Windows.Forms.MenuItem();
            this.menuDocumentSep2 = new System.Windows.Forms.MenuItem();
            this.menuDocumentToolbar = new System.Windows.Forms.MenuItem();
            this.menuDocumentScrollbar = new System.Windows.Forms.MenuItem();
            this.menuDocumentWordwrap = new System.Windows.Forms.MenuItem();
            this.menuDocumentOverwrite = new System.Windows.Forms.MenuItem();
            this.menuMainSep1 = new System.Windows.Forms.MenuItem();
            this.menuTextFont = new System.Windows.Forms.MenuItem();
            this.menuTextFontDialog = new System.Windows.Forms.MenuItem();
            this.menuTextFontColor = new System.Windows.Forms.MenuItem();
            this.menuTextFontSep1 = new System.Windows.Forms.MenuItem();
            this.menuTextFontNormal = new System.Windows.Forms.MenuItem();
            this.menuTextFontBold = new System.Windows.Forms.MenuItem();
            this.menuTextFontItalic = new System.Windows.Forms.MenuItem();
            this.menuTextFontUnderline = new System.Windows.Forms.MenuItem();
            this.menuTextFontSuperscript = new System.Windows.Forms.MenuItem();
            this.menuTextFontSubscript = new System.Windows.Forms.MenuItem();
            this.menuTextFontStrikeout = new System.Windows.Forms.MenuItem();
            this.menuTextFontSep2 = new System.Windows.Forms.MenuItem();
            this.menuTextFontIncrease = new System.Windows.Forms.MenuItem();
            this.menuTextFontDecrease = new System.Windows.Forms.MenuItem();
            this.menuTextFontIndent = new System.Windows.Forms.MenuItem();
            this.menuTextFontOutdent = new System.Windows.Forms.MenuItem();
            this.menuTextFontSep3 = new System.Windows.Forms.MenuItem();
            this.menuTextFontListOrdered = new System.Windows.Forms.MenuItem();
            this.menuTextFontListUnordered = new System.Windows.Forms.MenuItem();
            this.menuJustify = new System.Windows.Forms.MenuItem();
            this.menuJustifyLeft = new System.Windows.Forms.MenuItem();
            this.menuJustifyCenter = new System.Windows.Forms.MenuItem();
            this.menuJustifyRight = new System.Windows.Forms.MenuItem();
            this.menuFormatting = new System.Windows.Forms.MenuItem();
            this.menuMainSep2 = new System.Windows.Forms.MenuItem();
            this.menuInsert = new System.Windows.Forms.MenuItem();
            this.menuInsertLine = new System.Windows.Forms.MenuItem();
            this.menuInsertLink = new System.Windows.Forms.MenuItem();
            this.menuInsertImage = new System.Windows.Forms.MenuItem();
            this.menuInsertText = new System.Windows.Forms.MenuItem();
            this.menuInsertHtml = new System.Windows.Forms.MenuItem();
            this.menuInsertTable = new System.Windows.Forms.MenuItem();
            this.toolBarFindReplace = new System.Windows.Forms.ToolBarButton();
            this.toolBarEditSep6 = new System.Windows.Forms.ToolBarButton();
            this.toolBarInsertLink = new System.Windows.Forms.ToolBarButton();
            this.toolBarInsertImage = new System.Windows.Forms.ToolBarButton();
            this.toolBarInsertTable = new System.Windows.Forms.ToolBarButton();
            this.toolBarInsertLine = new System.Windows.Forms.ToolBarButton();
            this.toolBarEditSep5 = new System.Windows.Forms.ToolBarButton();
            this.toolBarListUnordered = new System.Windows.Forms.ToolBarButton();
            this.toolBarListOrdered = new System.Windows.Forms.ToolBarButton();
            this.toolBarSuperscript = new System.Windows.Forms.ToolBarButton();
            this.toolBarFootnote = new System.Windows.Forms.ToolBarButton();
            this.toolBarEditSep4 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarJustRight = new System.Windows.Forms.ToolBarButton();
            this.toolBarJustCenter = new System.Windows.Forms.ToolBarButton();
            this.toolBarJustLeft = new System.Windows.Forms.ToolBarButton();
            this.toolBarEditSep3 = new System.Windows.Forms.ToolBarButton();
            this.toolBarItalic = new System.Windows.Forms.ToolBarButton();
            this.toolBarUnderline = new System.Windows.Forms.ToolBarButton();
            this.toolBarBold = new System.Windows.Forms.ToolBarButton();
            this.toolBarEditSep2 = new System.Windows.Forms.ToolBarButton();
            this.toolBarRedo = new System.Windows.Forms.ToolBarButton();
            this.toolBarUndo = new System.Windows.Forms.ToolBarButton();
            this.toolBarEditSep1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarPaste = new System.Windows.Forms.ToolBarButton();
            this.toolBarCopy = new System.Windows.Forms.ToolBarButton();
            this.toolBarCut = new System.Windows.Forms.ToolBarButton();
            this.editorToolbar = new System.Windows.Forms.ToolBar();
            ((System.ComponentModel.ISupportInitialize)(this.editorWebBrowser)).BeginInit();
            this.SuspendLayout();
            // 
            // editorWebBrowser
            // 
            resources.ApplyResources(this.editorWebBrowser, "editorWebBrowser");
            this.editorWebBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("editorWebBrowser.OcxState")));
            this.editorWebBrowser.BeforeNavigate2 += new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(this.BrowserBeforeNavigate);
            this.editorWebBrowser.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(this.BrowserDocumentComplete);
            // 
            // toolbarImageList
            // 
            this.toolbarImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolbarImageList.ImageStream")));
            this.toolbarImageList.TransparentColor = System.Drawing.Color.Silver;
            this.toolbarImageList.Images.SetKeyName(0, "");
            this.toolbarImageList.Images.SetKeyName(1, "");
            this.toolbarImageList.Images.SetKeyName(2, "");
            this.toolbarImageList.Images.SetKeyName(3, "");
            this.toolbarImageList.Images.SetKeyName(4, "");
            this.toolbarImageList.Images.SetKeyName(5, "");
            this.toolbarImageList.Images.SetKeyName(6, "");
            this.toolbarImageList.Images.SetKeyName(7, "");
            this.toolbarImageList.Images.SetKeyName(8, "");
            this.toolbarImageList.Images.SetKeyName(9, "");
            this.toolbarImageList.Images.SetKeyName(10, "");
            this.toolbarImageList.Images.SetKeyName(11, "");
            this.toolbarImageList.Images.SetKeyName(12, "");
            this.toolbarImageList.Images.SetKeyName(13, "");
            this.toolbarImageList.Images.SetKeyName(14, "");
            this.toolbarImageList.Images.SetKeyName(15, "");
            this.toolbarImageList.Images.SetKeyName(16, "");
            this.toolbarImageList.Images.SetKeyName(17, "");
            this.toolbarImageList.Images.SetKeyName(18, "");
            this.toolbarImageList.Images.SetKeyName(19, "");
            this.toolbarImageList.Images.SetKeyName(20, "");
            this.toolbarImageList.Images.SetKeyName(21, "");
            this.toolbarImageList.Images.SetKeyName(22, "");
            this.toolbarImageList.Images.SetKeyName(23, "");
            this.toolbarImageList.Images.SetKeyName(24, "");
            this.toolbarImageList.Images.SetKeyName(25, "");
            this.toolbarImageList.Images.SetKeyName(26, "");
            this.toolbarImageList.Images.SetKeyName(27, "");
            this.toolbarImageList.Images.SetKeyName(28, "");
            this.toolbarImageList.Images.SetKeyName(29, "justify.png");
            this.toolbarImageList.Images.SetKeyName(30, "footnote_edit.png");
            // 
            // contextMenuMain
            // 
            this.contextMenuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuTableModify,
            this.menuText,
            this.menuDocument,
            this.menuMainSep1,
            this.menuTextFont,
            this.menuJustify,
            this.menuFormatting,
            this.menuMainSep2,
            this.menuInsert});
            // 
            // menuTableModify
            // 
            this.menuTableModify.Index = 0;
            this.menuTableModify.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuTableProperties,
            this.menuTableInsertRow,
            this.menuTableDeleteRow});
            resources.ApplyResources(this.menuTableModify, "menuTableModify");
            // 
            // menuTableProperties
            // 
            this.menuTableProperties.Index = 0;
            resources.ApplyResources(this.menuTableProperties, "menuTableProperties");
            this.menuTableProperties.Click += new System.EventHandler(this.menuTableProperties_Click);
            // 
            // menuTableInsertRow
            // 
            this.menuTableInsertRow.Index = 1;
            resources.ApplyResources(this.menuTableInsertRow, "menuTableInsertRow");
            this.menuTableInsertRow.Click += new System.EventHandler(this.menuTableInsertRow_Click);
            // 
            // menuTableDeleteRow
            // 
            this.menuTableDeleteRow.Index = 2;
            resources.ApplyResources(this.menuTableDeleteRow, "menuTableDeleteRow");
            this.menuTableDeleteRow.Click += new System.EventHandler(this.menuTableDeleteRow_Click);
            // 
            // menuText
            // 
            this.menuText.Index = 1;
            this.menuText.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuTextUndo,
            this.menuTextRedo,
            this.menuTextSep1,
            this.menuTextCut,
            this.menuTextCopy,
            this.menuTextPaste,
            this.menuTextSep2,
            this.menuTextFindReplace,
            this.menuTextSep3,
            this.menuTextSelectNone,
            this.menuTextSelectAll,
            this.menuTextDelete});
            resources.ApplyResources(this.menuText, "menuText");
            // 
            // menuTextUndo
            // 
            this.menuTextUndo.Index = 0;
            resources.ApplyResources(this.menuTextUndo, "menuTextUndo");
            this.menuTextUndo.Click += new System.EventHandler(this.menuTextUndo_Click);
            // 
            // menuTextRedo
            // 
            this.menuTextRedo.Index = 1;
            resources.ApplyResources(this.menuTextRedo, "menuTextRedo");
            this.menuTextRedo.Click += new System.EventHandler(this.menuTextRedo_Click);
            // 
            // menuTextSep1
            // 
            this.menuTextSep1.Index = 2;
            resources.ApplyResources(this.menuTextSep1, "menuTextSep1");
            // 
            // menuTextCut
            // 
            this.menuTextCut.Index = 3;
            resources.ApplyResources(this.menuTextCut, "menuTextCut");
            this.menuTextCut.Click += new System.EventHandler(this.menuTextCut_Click);
            // 
            // menuTextCopy
            // 
            this.menuTextCopy.Index = 4;
            resources.ApplyResources(this.menuTextCopy, "menuTextCopy");
            this.menuTextCopy.Click += new System.EventHandler(this.menuTextCopy_Click);
            // 
            // menuTextPaste
            // 
            this.menuTextPaste.Index = 5;
            resources.ApplyResources(this.menuTextPaste, "menuTextPaste");
            this.menuTextPaste.Click += new System.EventHandler(this.menuTextPaste_Click);
            // 
            // menuTextSep2
            // 
            this.menuTextSep2.Index = 6;
            resources.ApplyResources(this.menuTextSep2, "menuTextSep2");
            // 
            // menuTextFindReplace
            // 
            this.menuTextFindReplace.Index = 7;
            resources.ApplyResources(this.menuTextFindReplace, "menuTextFindReplace");
            this.menuTextFindReplace.Click += new System.EventHandler(this.menuTextFindReplace_Click);
            // 
            // menuTextSep3
            // 
            this.menuTextSep3.Index = 8;
            resources.ApplyResources(this.menuTextSep3, "menuTextSep3");
            // 
            // menuTextSelectNone
            // 
            this.menuTextSelectNone.Index = 9;
            resources.ApplyResources(this.menuTextSelectNone, "menuTextSelectNone");
            this.menuTextSelectNone.Click += new System.EventHandler(this.menuTextSelectNone_Click);
            // 
            // menuTextSelectAll
            // 
            this.menuTextSelectAll.Index = 10;
            resources.ApplyResources(this.menuTextSelectAll, "menuTextSelectAll");
            this.menuTextSelectAll.Click += new System.EventHandler(this.menuTextSelectAll_Click);
            // 
            // menuTextDelete
            // 
            this.menuTextDelete.Index = 11;
            resources.ApplyResources(this.menuTextDelete, "menuTextDelete");
            this.menuTextDelete.Click += new System.EventHandler(this.menuTextDelete_Click);
            // 
            // menuDocument
            // 
            this.menuDocument.Index = 2;
            this.menuDocument.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuDocumentOpen,
            this.menuDocumentSave,
            this.menuDocumentSep1,
            this.menuDocumentPrint,
            this.menuDocumentSep2,
            this.menuDocumentToolbar,
            this.menuDocumentScrollbar,
            this.menuDocumentWordwrap,
            this.menuDocumentOverwrite});
            resources.ApplyResources(this.menuDocument, "menuDocument");
            // 
            // menuDocumentOpen
            // 
            this.menuDocumentOpen.Index = 0;
            resources.ApplyResources(this.menuDocumentOpen, "menuDocumentOpen");
            this.menuDocumentOpen.Click += new System.EventHandler(this.menuDocumentOpen_Click);
            // 
            // menuDocumentSave
            // 
            this.menuDocumentSave.Index = 1;
            resources.ApplyResources(this.menuDocumentSave, "menuDocumentSave");
            this.menuDocumentSave.Click += new System.EventHandler(this.menuDocumentSave_Click);
            // 
            // menuDocumentSep1
            // 
            this.menuDocumentSep1.Index = 2;
            resources.ApplyResources(this.menuDocumentSep1, "menuDocumentSep1");
            // 
            // menuDocumentPrint
            // 
            this.menuDocumentPrint.Index = 3;
            resources.ApplyResources(this.menuDocumentPrint, "menuDocumentPrint");
            this.menuDocumentPrint.Click += new System.EventHandler(this.menuDocumentPrint_Click);
            // 
            // menuDocumentSep2
            // 
            this.menuDocumentSep2.Index = 4;
            resources.ApplyResources(this.menuDocumentSep2, "menuDocumentSep2");
            // 
            // menuDocumentToolbar
            // 
            this.menuDocumentToolbar.Checked = true;
            this.menuDocumentToolbar.Index = 5;
            resources.ApplyResources(this.menuDocumentToolbar, "menuDocumentToolbar");
            this.menuDocumentToolbar.Click += new System.EventHandler(this.menuDocumentToolbar_Click);
            // 
            // menuDocumentScrollbar
            // 
            this.menuDocumentScrollbar.Index = 6;
            resources.ApplyResources(this.menuDocumentScrollbar, "menuDocumentScrollbar");
            this.menuDocumentScrollbar.Click += new System.EventHandler(this.menuDocumentScrollbar_Click);
            // 
            // menuDocumentWordwrap
            // 
            this.menuDocumentWordwrap.Index = 7;
            resources.ApplyResources(this.menuDocumentWordwrap, "menuDocumentWordwrap");
            this.menuDocumentWordwrap.Click += new System.EventHandler(this.menuDocumentWordwrap_Click);
            // 
            // menuDocumentOverwrite
            // 
            this.menuDocumentOverwrite.Index = 8;
            resources.ApplyResources(this.menuDocumentOverwrite, "menuDocumentOverwrite");
            this.menuDocumentOverwrite.Click += new System.EventHandler(this.menuDocumentOverwrite_Click);
            // 
            // menuMainSep1
            // 
            this.menuMainSep1.Index = 3;
            resources.ApplyResources(this.menuMainSep1, "menuMainSep1");
            // 
            // menuTextFont
            // 
            this.menuTextFont.Index = 4;
            this.menuTextFont.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuTextFontDialog,
            this.menuTextFontColor,
            this.menuTextFontSep1,
            this.menuTextFontNormal,
            this.menuTextFontBold,
            this.menuTextFontItalic,
            this.menuTextFontUnderline,
            this.menuTextFontSuperscript,
            this.menuTextFontSubscript,
            this.menuTextFontStrikeout,
            this.menuTextFontSep2,
            this.menuTextFontIncrease,
            this.menuTextFontDecrease,
            this.menuTextFontIndent,
            this.menuTextFontOutdent,
            this.menuTextFontSep3,
            this.menuTextFontListOrdered,
            this.menuTextFontListUnordered});
            resources.ApplyResources(this.menuTextFont, "menuTextFont");
            // 
            // menuTextFontDialog
            // 
            this.menuTextFontDialog.Index = 0;
            resources.ApplyResources(this.menuTextFontDialog, "menuTextFontDialog");
            this.menuTextFontDialog.Click += new System.EventHandler(this.menuTextFontDialog_Click);
            // 
            // menuTextFontColor
            // 
            this.menuTextFontColor.Index = 1;
            resources.ApplyResources(this.menuTextFontColor, "menuTextFontColor");
            this.menuTextFontColor.Click += new System.EventHandler(this.menuTextFontColor_Click);
            // 
            // menuTextFontSep1
            // 
            this.menuTextFontSep1.Index = 2;
            resources.ApplyResources(this.menuTextFontSep1, "menuTextFontSep1");
            // 
            // menuTextFontNormal
            // 
            this.menuTextFontNormal.Index = 3;
            resources.ApplyResources(this.menuTextFontNormal, "menuTextFontNormal");
            this.menuTextFontNormal.Click += new System.EventHandler(this.menuTextFontNormal_Click);
            // 
            // menuTextFontBold
            // 
            this.menuTextFontBold.Index = 4;
            resources.ApplyResources(this.menuTextFontBold, "menuTextFontBold");
            this.menuTextFontBold.Click += new System.EventHandler(this.menuTextFontBold_Click);
            // 
            // menuTextFontItalic
            // 
            this.menuTextFontItalic.Index = 5;
            resources.ApplyResources(this.menuTextFontItalic, "menuTextFontItalic");
            this.menuTextFontItalic.Click += new System.EventHandler(this.menuTextFontItalic_Click);
            // 
            // menuTextFontUnderline
            // 
            this.menuTextFontUnderline.Index = 6;
            resources.ApplyResources(this.menuTextFontUnderline, "menuTextFontUnderline");
            this.menuTextFontUnderline.Click += new System.EventHandler(this.menuTextFontUnderline_Click);
            // 
            // menuTextFontSuperscript
            // 
            this.menuTextFontSuperscript.Index = 7;
            resources.ApplyResources(this.menuTextFontSuperscript, "menuTextFontSuperscript");
            this.menuTextFontSuperscript.Click += new System.EventHandler(this.menuTextFontSuperscript_Click);
            // 
            // menuTextFontSubscript
            // 
            this.menuTextFontSubscript.Index = 8;
            resources.ApplyResources(this.menuTextFontSubscript, "menuTextFontSubscript");
            this.menuTextFontSubscript.Click += new System.EventHandler(this.menuTextFontSubscript_Click);
            // 
            // menuTextFontStrikeout
            // 
            this.menuTextFontStrikeout.Index = 9;
            resources.ApplyResources(this.menuTextFontStrikeout, "menuTextFontStrikeout");
            this.menuTextFontStrikeout.Click += new System.EventHandler(this.menuTextFontStrikeout_Click);
            // 
            // menuTextFontSep2
            // 
            this.menuTextFontSep2.Index = 10;
            resources.ApplyResources(this.menuTextFontSep2, "menuTextFontSep2");
            // 
            // menuTextFontIncrease
            // 
            this.menuTextFontIncrease.Index = 11;
            resources.ApplyResources(this.menuTextFontIncrease, "menuTextFontIncrease");
            this.menuTextFontIncrease.Click += new System.EventHandler(this.menuTextFontIncrease_Click);
            // 
            // menuTextFontDecrease
            // 
            this.menuTextFontDecrease.Index = 12;
            resources.ApplyResources(this.menuTextFontDecrease, "menuTextFontDecrease");
            this.menuTextFontDecrease.Click += new System.EventHandler(this.menuTextFontDecrease_Click);
            // 
            // menuTextFontIndent
            // 
            this.menuTextFontIndent.Index = 13;
            resources.ApplyResources(this.menuTextFontIndent, "menuTextFontIndent");
            this.menuTextFontIndent.Click += new System.EventHandler(this.menuTextFontIndent_Click);
            // 
            // menuTextFontOutdent
            // 
            this.menuTextFontOutdent.Index = 14;
            resources.ApplyResources(this.menuTextFontOutdent, "menuTextFontOutdent");
            this.menuTextFontOutdent.Click += new System.EventHandler(this.menuTextFontOutdent_Click);
            // 
            // menuTextFontSep3
            // 
            this.menuTextFontSep3.Index = 15;
            resources.ApplyResources(this.menuTextFontSep3, "menuTextFontSep3");
            // 
            // menuTextFontListOrdered
            // 
            this.menuTextFontListOrdered.Index = 16;
            resources.ApplyResources(this.menuTextFontListOrdered, "menuTextFontListOrdered");
            this.menuTextFontListOrdered.Click += new System.EventHandler(this.menuTextFontListOrdered_Click);
            // 
            // menuTextFontListUnordered
            // 
            this.menuTextFontListUnordered.Index = 17;
            resources.ApplyResources(this.menuTextFontListUnordered, "menuTextFontListUnordered");
            this.menuTextFontListUnordered.Click += new System.EventHandler(this.menuTextFontListUnordered_Click);
            // 
            // menuJustify
            // 
            this.menuJustify.Index = 5;
            this.menuJustify.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuJustifyLeft,
            this.menuJustifyCenter,
            this.menuJustifyRight});
            resources.ApplyResources(this.menuJustify, "menuJustify");
            // 
            // menuJustifyLeft
            // 
            this.menuJustifyLeft.Index = 0;
            resources.ApplyResources(this.menuJustifyLeft, "menuJustifyLeft");
            this.menuJustifyLeft.Click += new System.EventHandler(this.menuJustifyLeft_Click);
            // 
            // menuJustifyCenter
            // 
            this.menuJustifyCenter.Index = 1;
            resources.ApplyResources(this.menuJustifyCenter, "menuJustifyCenter");
            this.menuJustifyCenter.Click += new System.EventHandler(this.menuJustifyCenter_Click);
            // 
            // menuJustifyRight
            // 
            this.menuJustifyRight.Index = 2;
            resources.ApplyResources(this.menuJustifyRight, "menuJustifyRight");
            this.menuJustifyRight.Click += new System.EventHandler(this.menuJustifyRight_Click);
            // 
            // menuFormatting
            // 
            this.menuFormatting.Index = 6;
            resources.ApplyResources(this.menuFormatting, "menuFormatting");
            // 
            // menuMainSep2
            // 
            this.menuMainSep2.Index = 7;
            resources.ApplyResources(this.menuMainSep2, "menuMainSep2");
            // 
            // menuInsert
            // 
            this.menuInsert.Index = 8;
            this.menuInsert.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuInsertLine,
            this.menuInsertLink,
            this.menuInsertImage,
            this.menuInsertText,
            this.menuInsertHtml,
            this.menuInsertTable});
            resources.ApplyResources(this.menuInsert, "menuInsert");
            // 
            // menuInsertLine
            // 
            this.menuInsertLine.Index = 0;
            resources.ApplyResources(this.menuInsertLine, "menuInsertLine");
            this.menuInsertLine.Click += new System.EventHandler(this.menuInsertLine_Click);
            // 
            // menuInsertLink
            // 
            this.menuInsertLink.Index = 1;
            resources.ApplyResources(this.menuInsertLink, "menuInsertLink");
            this.menuInsertLink.Click += new System.EventHandler(this.menuInsertLink_Click);
            // 
            // menuInsertImage
            // 
            this.menuInsertImage.Index = 2;
            resources.ApplyResources(this.menuInsertImage, "menuInsertImage");
            this.menuInsertImage.Click += new System.EventHandler(this.menuInsertImage_Click);
            // 
            // menuInsertText
            // 
            this.menuInsertText.Index = 3;
            resources.ApplyResources(this.menuInsertText, "menuInsertText");
            this.menuInsertText.Click += new System.EventHandler(this.menuInsertText_Click);
            // 
            // menuInsertHtml
            // 
            this.menuInsertHtml.Index = 4;
            resources.ApplyResources(this.menuInsertHtml, "menuInsertHtml");
            this.menuInsertHtml.Click += new System.EventHandler(this.menuInsertHtml_Click);
            // 
            // menuInsertTable
            // 
            this.menuInsertTable.Index = 5;
            resources.ApplyResources(this.menuInsertTable, "menuInsertTable");
            this.menuInsertTable.Click += new System.EventHandler(this.menuInsertTable_Click);
            // 
            // toolBarFindReplace
            // 
            resources.ApplyResources(this.toolBarFindReplace, "toolBarFindReplace");
            this.toolBarFindReplace.Name = "toolBarFindReplace";
            this.toolBarFindReplace.Tag = "FindReplace";
            // 
            // toolBarEditSep6
            // 
            this.toolBarEditSep6.Name = "toolBarEditSep6";
            this.toolBarEditSep6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarInsertLink
            // 
            resources.ApplyResources(this.toolBarInsertLink, "toolBarInsertLink");
            this.toolBarInsertLink.Name = "toolBarInsertLink";
            this.toolBarInsertLink.Tag = "InsertLink";
            // 
            // toolBarInsertImage
            // 
            resources.ApplyResources(this.toolBarInsertImage, "toolBarInsertImage");
            this.toolBarInsertImage.Name = "toolBarInsertImage";
            this.toolBarInsertImage.Tag = "InsertImage";
            // 
            // toolBarInsertTable
            // 
            resources.ApplyResources(this.toolBarInsertTable, "toolBarInsertTable");
            this.toolBarInsertTable.Name = "toolBarInsertTable";
            this.toolBarInsertTable.Tag = "InsertTable";
            // 
            // toolBarInsertLine
            // 
            resources.ApplyResources(this.toolBarInsertLine, "toolBarInsertLine");
            this.toolBarInsertLine.Name = "toolBarInsertLine";
            this.toolBarInsertLine.Tag = "InsertLine";
            // 
            // toolBarEditSep5
            // 
            this.toolBarEditSep5.Name = "toolBarEditSep5";
            this.toolBarEditSep5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarListUnordered
            // 
            resources.ApplyResources(this.toolBarListUnordered, "toolBarListUnordered");
            this.toolBarListUnordered.Name = "toolBarListUnordered";
            this.toolBarListUnordered.Tag = "ListUnordered";
            // 
            // toolBarListOrdered
            // 
            resources.ApplyResources(this.toolBarListOrdered, "toolBarListOrdered");
            this.toolBarListOrdered.Name = "toolBarListOrdered";
            this.toolBarListOrdered.Tag = "ListOrdered";
            // 
            // toolBarSuperscript
            // 
            resources.ApplyResources(this.toolBarSuperscript, "toolBarSuperscript");
            this.toolBarSuperscript.Name = "toolBarSuperscript";
            this.toolBarSuperscript.Tag = "FormatSuperscript";
            // 
            // toolBarFootnote
            // 
            resources.ApplyResources(this.toolBarFootnote, "toolBarFootnote");
            this.toolBarFootnote.Name = "toolBarFootnote";
            this.toolBarFootnote.Tag = "Footnote";
            // 
            // toolBarEditSep4
            // 
            this.toolBarEditSep4.Name = "toolBarEditSep4";
            this.toolBarEditSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButton1
            // 
            resources.ApplyResources(this.toolBarButton1, "toolBarButton1");
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Tag = "JustifyFull";
            // 
            // toolBarJustRight
            // 
            resources.ApplyResources(this.toolBarJustRight, "toolBarJustRight");
            this.toolBarJustRight.Name = "toolBarJustRight";
            this.toolBarJustRight.Tag = "JustifyRight";
            // 
            // toolBarJustCenter
            // 
            resources.ApplyResources(this.toolBarJustCenter, "toolBarJustCenter");
            this.toolBarJustCenter.Name = "toolBarJustCenter";
            this.toolBarJustCenter.Tag = "JustifyCenter";
            // 
            // toolBarJustLeft
            // 
            resources.ApplyResources(this.toolBarJustLeft, "toolBarJustLeft");
            this.toolBarJustLeft.Name = "toolBarJustLeft";
            this.toolBarJustLeft.Tag = "JustifyLeft";
            // 
            // toolBarEditSep3
            // 
            this.toolBarEditSep3.Name = "toolBarEditSep3";
            this.toolBarEditSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarItalic
            // 
            resources.ApplyResources(this.toolBarItalic, "toolBarItalic");
            this.toolBarItalic.Name = "toolBarItalic";
            this.toolBarItalic.Tag = "FormatItalic";
            // 
            // toolBarUnderline
            // 
            resources.ApplyResources(this.toolBarUnderline, "toolBarUnderline");
            this.toolBarUnderline.Name = "toolBarUnderline";
            this.toolBarUnderline.Tag = "FormatUnderline";
            // 
            // toolBarBold
            // 
            resources.ApplyResources(this.toolBarBold, "toolBarBold");
            this.toolBarBold.Name = "toolBarBold";
            this.toolBarBold.Tag = "FormatBold";
            // 
            // toolBarEditSep2
            // 
            this.toolBarEditSep2.Name = "toolBarEditSep2";
            this.toolBarEditSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarRedo
            // 
            resources.ApplyResources(this.toolBarRedo, "toolBarRedo");
            this.toolBarRedo.Name = "toolBarRedo";
            this.toolBarRedo.Tag = "EditRedo";
            // 
            // toolBarUndo
            // 
            resources.ApplyResources(this.toolBarUndo, "toolBarUndo");
            this.toolBarUndo.Name = "toolBarUndo";
            this.toolBarUndo.Tag = "EditUndo";
            // 
            // toolBarEditSep1
            // 
            this.toolBarEditSep1.Name = "toolBarEditSep1";
            this.toolBarEditSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarPaste
            // 
            resources.ApplyResources(this.toolBarPaste, "toolBarPaste");
            this.toolBarPaste.Name = "toolBarPaste";
            this.toolBarPaste.Tag = "TextPaste";
            // 
            // toolBarCopy
            // 
            resources.ApplyResources(this.toolBarCopy, "toolBarCopy");
            this.toolBarCopy.Name = "toolBarCopy";
            this.toolBarCopy.Tag = "TextCopy";
            // 
            // toolBarCut
            // 
            resources.ApplyResources(this.toolBarCut, "toolBarCut");
            this.toolBarCut.Name = "toolBarCut";
            this.toolBarCut.Tag = "TextCut";
            // 
            // editorToolbar
            // 
            resources.ApplyResources(this.editorToolbar, "editorToolbar");
            this.editorToolbar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.editorToolbar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarCut,
            this.toolBarCopy,
            this.toolBarPaste,
            this.toolBarEditSep1,
            this.toolBarUndo,
            this.toolBarRedo,
            this.toolBarEditSep2,
            this.toolBarBold,
            this.toolBarUnderline,
            this.toolBarItalic,
            this.toolBarEditSep3,
            this.toolBarJustLeft,
            this.toolBarJustCenter,
            this.toolBarJustRight,
            this.toolBarButton1,
            this.toolBarEditSep4,
            this.toolBarFootnote,
            this.toolBarSuperscript,
            this.toolBarListOrdered,
            this.toolBarListUnordered,
            this.toolBarEditSep5,
            this.toolBarInsertLine,
            this.toolBarInsertTable,
            this.toolBarInsertImage,
            this.toolBarInsertLink,
            this.toolBarEditSep6,
            this.toolBarFindReplace});
            this.editorToolbar.Divider = false;
            this.editorToolbar.ImageList = this.toolbarImageList;
            this.editorToolbar.Name = "editorToolbar";
            this.editorToolbar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.editorToolbar_ButtonClick);
            // 
            // HtmlEditorControl
            // 
            this.Controls.Add(this.editorWebBrowser);
            this.Controls.Add(this.editorToolbar);
            this.Name = "HtmlEditorControl";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.editorWebBrowser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Control Methods and Events

        // define the context menu for the drop down of available styles
        // defined with a TAG value set to the appropriate format Tag
        private void DefineFormatBlockMenu()
        {

            // define the menu items
            MenuItem menuFormatNormal = new System.Windows.Forms.MenuItem();
            MenuItem menuFormatH1 = new System.Windows.Forms.MenuItem();
            MenuItem menuFormatH2 = new System.Windows.Forms.MenuItem();
            MenuItem menuFormatH3 = new System.Windows.Forms.MenuItem();
            MenuItem menuFormatH4 = new System.Windows.Forms.MenuItem();
            MenuItem menuFormatH5 = new System.Windows.Forms.MenuItem();
            MenuItem menuFormatPRE = new System.Windows.Forms.MenuItem();

            // define the Text for the appropriate format block command
            menuFormatNormal.Text = FORMATTED_NORMAL;
            menuFormatH1.Text = FORMATTED_HEADING + " 1";
            menuFormatH2.Text = FORMATTED_HEADING + " 2";
            menuFormatH3.Text = FORMATTED_HEADING + " 3";
            menuFormatH4.Text = FORMATTED_HEADING + " 4";
            menuFormatH5.Text = FORMATTED_HEADING + " 5";
            menuFormatPRE.Text = FORMATTED_PRE;

            // ensure all event handlers point to the same method
            menuFormatNormal.Click += new System.EventHandler(ProcessFormattingSelection);
            menuFormatH1.Click += new System.EventHandler(ProcessFormattingSelection);
            menuFormatH2.Click += new System.EventHandler(ProcessFormattingSelection);
            menuFormatH3.Click += new System.EventHandler(ProcessFormattingSelection);
            menuFormatH4.Click += new System.EventHandler(ProcessFormattingSelection);
            menuFormatH5.Click += new System.EventHandler(ProcessFormattingSelection);
            menuFormatPRE.Click += new System.EventHandler(ProcessFormattingSelection);

            // create the submenu array to be added to the sub items
            System.Windows.Forms.MenuItem[] formattingSubMenu = new System.Windows.Forms.MenuItem[]
                                                                    {
                                                                        menuFormatNormal,
                                                                        menuFormatH1,
                                                                        menuFormatH2,
                                                                        menuFormatH3,
                                                                        menuFormatH4,
                                                                        menuFormatH5,
                                                                        menuFormatPRE
                                                                    };

            // now have the formatting context menu add to the main context menu
            this.menuFormatting.MenuItems.AddRange(formattingSubMenu);

        } //DefineFormatBlockMenu


        // COM Event Handler for HTML Element Events
        [DispId(0)]
        public void DefaultMethod()
        {
            // obtain the event object and ensure a context menu has been applied to the document
            HtmlEventObject eventObject = document.parentWindow.@event;
            string eventType = eventObject.type;

            if (IsStatedTag(eventType, EVENT_CONTEXT_MENU))
            {
                // Call the custom Web Browser HTML event 
                ContextMenuShow(this, eventObject);
            }

        } //DefaultMethod


        // method to perform the process of showing the context menus
        private void ContextMenuShow(object sender, HtmlEventObject e)
        {
            // if in readonly mode display the standard context menu
            // otherwise display the editing context menu
            if (!_readOnly)
            {
                // should disable inappropriate commands
                if (IsParentTable())
                {
                    this.menuTableModify.Visible = true;
                }
                else
                {
                    this.menuTableModify.Visible = false;
                }

                // display the text processing context menu
                contextMenuMain.Show(this, new System.Drawing.Point(e.x, e.y));

                // cancel the standard menu and event bubbling
                e.returnValue = false;
                e.cancelBubble = true;
            }

        } //ContextMenuShow


        // method used to navigate to the required page
        // call made sync using a loading variable
        private void BrowserCodeNavigate(string url)
        {
            // once navigated to the href page wait until successful
            // need to do this to ensure properties are all correctly set
            codeNavigate = true;
            loading = true;

            // perform the navigation
            this.editorWebBrowser.Navigate(url, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);

            // wait for the navigate to complete using the loading variable
            // DoEvents needs to be called to enable the DocumentComplete to execute
            while (loading)
            {
                Application.DoEvents();
                Thread.Sleep(0);
            }

        } //BrowserCodeNavigate


        // this event can be used to canel the navigation and open a new window
        // if window set to same then nothing happens
        private void BrowserBeforeNavigate(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
        {
            string url = e.uRL.ToString();
            if (!codeNavigate)
            {
                // call the appropriate event processing
                HtmlNavigationEventArgs navigateArgs = new HtmlNavigationEventArgs(url);
                OnHtmlNavigation(navigateArgs);

                // process the event based on the navigation option
                if (navigateArgs.Cancel)
                {
                    // cancel the navigation
                    e.cancel = true;
                }
                else if (_navigateWindow == NavigateActionOption.NewWindow)
                {
                    // cancel the current navigation and load url into a new window
                    e.cancel = true;
                    this.NavigateToUrl(url, true);
                }
                else
                {
                    // continue with current navigation
                    e.cancel = false;
                }
            }
            else
            {
                // TODO Should ensure the following are no executed for the editor navigation
                //   Scripts
                //   Java
                //   ActiveX Controls
                //   Behaviors
                //   Dialogs

                // continue with current navigation
                e.cancel = false;
            }

        } //BrowserBeforeNavigate


        //processing for the HtmlNavigation event
        private void OnHtmlNavigation(HtmlNavigationEventArgs args)
        {
            if (HtmlNavigation != null)
            {
                HtmlNavigation(this, args);
            }

        } //OnHtmlNavigation


        // Document complete method for the web browser
        // initiated by navigating to the about:blank page (EMPTY_PARAMETER HTML document)
        private void BrowserDocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            // get access to the HTMLDocument
            document = (HtmlDocument)this.editorWebBrowser.Document;
            body = (HtmlBody)document.body;

            // COM Interop Start
            // once browsing has completed there is the need to setup some options
            // need to ensure URLs are not modified when html is pasted
            IOleCommandTarget target = null;
            int hResult = HRESULT.S_OK;
            // try to obtain the command target for the web browser document
            try
            {
                // cast the document to a command target
                target = (IOleCommandTarget)document;
                // set the appropriate no url fixups on paste
                hResult = target.Exec(ref CommandGroup.CGID_MSHTML, (int)CommandId.IDM_NOFIXUPURLSONPASTE, (int)CommandOption.OLECMDEXECOPT_DONTPROMPTUSER, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);
            }
            // catch any exception and map back to the HRESULT
            catch (Exception ex)
            {
                hResult = Marshal.GetHRForException(ex);
            }
            // test the HRESULT for a valid operation
            if (hResult == HRESULT.S_OK)
            {
                // urls will not automatically be rebased
                rebaseUrlsNeeded = false;
            }
            else
            {
                //throw new HtmlEditorException(string.Format("Error executing NOFIXUPURLSONPASTE: Result {0}", hResult));
                rebaseUrlsNeeded = true;
            }
            // COM Interop End

            // at this point the document and body has been loaded
            // so define the event handler as the same class
            //document.oncontextmenu = this;
            ((mshtml.DispHTMLDocument)document).oncontextmenu = this;

            // signalled complete
            codeNavigate = false;
            loading = false;

            // after navigation define the document Url
            string url = e.uRL.ToString();
            _bodyUrl = IsStatedTag(url, BLANK_HTML_PAGE) ? string.Empty : url;

        } //BrowserDocumentComplete


        // create a new focus method that ensure the body gets the focus
        // should be called when text processing command are called
        public new bool Focus()
        {
            // have the return value be the focus return from the user control
            bool focus = base.Focus();
            // try to set the focus to the web browser
            try
            {
                this.editorWebBrowser.Focus();
                if (body != null) body.focus();
            }
            catch (Exception)
            {
                // ignore errors
            }
            return focus;

        } //Focus

        #endregion

        #region Runtime Display Properties

        // defines the whether scroll bars should be displayed
        [Category("RuntimeDisplay"), Description("Controls the Display of Scrolls Bars")]
        [DefaultValue(DisplayScrollBarOption.Auto)]
        public DisplayScrollBarOption ScrollBars
        {
            get
            {
                return _scrollBars;
            }
            set
            {
                _scrollBars = value;
                // define the document scroll bar visibility
                body.scroll = _scrollBars.ToString();
                // define the menu bar state
                this.menuDocumentScrollbar.Checked = (value == DisplayScrollBarOption.No ? false : true);
            }

        } //ScrollBars


        // defines the whether words will be auto wrapped
        [Category("RuntimeDisplay"), Description("Controls the auto wrapping of content")]
        [DefaultValue(true)]
        public bool AutoWordWrap
        {
            get
            {
                return _autoWordWrap;
            }
            set
            {
                _autoWordWrap = value;
                // define the document word wrap property
                body.noWrap = !_autoWordWrap;
                // define the menu bar state
                this.menuDocumentWordwrap.Checked = value;
            }

        } //AutoWordWrap


        // defines the default action when a user click on a link
        [Category("RuntimeDisplay"), Description("Window to use when clicking a Href")]
        [DefaultValue(NavigateActionOption.Default)]
        public NavigateActionOption NavigateAction
        {
            get
            {
                return _navigateWindow;
            }
            set
            {
                _navigateWindow = value;
            }

        } //NavigateAction


        // Defines the editable status of the text
        [Category("RuntimeDisplay"), Description("Marks the content as ReadOnly")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
                // define the document editable property
                body.contentEditable = (!_readOnly).ToString();
                // define the menu bar state
                this.editorToolbar.Enabled = (!_readOnly);
            }

        } //ReadOnly


        // defines the visibility of the defined toolbar
        [Category("RuntimeDisplay"), Description("Marks the toolbar as Visible")]
        [DefaultValue(false)]
        public bool ToolbarVisible
        {
            get
            {
                return _toolbarVisible;
            }
            set
            {
                _toolbarVisible = value;
                this.editorToolbar.Visible = _toolbarVisible;
                this.menuDocumentToolbar.Checked = value;
            }

        } //ToolbarVisible


        // defines the flat style of controls for visual styles
        [Category("RuntimeDisplay"), Description("Indicates if the Control Flat Style is set to System or Standard for all dialogs")]
        [DefaultValue(false)]
        public bool EnableVisualStyles
        {
            get
            {
                return _enableVisualStyles;
            }
            set
            {
                _enableVisualStyles = value;
            }

        } //EnableVisualStyles


        // defines the visibility of the defined toolbar
        [Category("RuntimeDisplay"), Description("Defines the docking location of the toolbar")]
        [DefaultValue(DockStyle.Bottom)]
        public DockStyle ToolbarDock
        {
            get
            {
                return _toolbarDock;
            }
            set
            {
                if (value != DockStyle.Fill)
                {
                    _toolbarDock = value;
                    this.editorToolbar.Dock = _toolbarDock;
                    // ensure control is repainted as docking has been modified
                    this.Invalidate();
                }
            }

        } //ToolbarDock


        #endregion

        #region Body Properties (Text and HTML)

        // defines the base text for the body (design time only value)
        // HTML value can be used at runtime
        [Category("Textual"), Description("Set the initial Body Text")]
        [DefaultValue(DEFAULT_HTML_TEXT)]
        public string InnerText
        {
            get
            {
                _bodyText = body.innerText;
                _bodyHtml = body.innerHTML;
                return _bodyText;
            }
            set
            {
                try
                {
                    // clear the defined body url
                    _bodyUrl = string.Empty;
                    if (value == null) value = string.Empty;
                    // set the body property
                    body.innerText = value;
                    // set the body text and html
                    _bodyText = body.innerText;
                    _bodyHtml = body.innerHTML;
                }
                catch (Exception ex)
                {
                    throw new HtmlEditorException("Inner Text for the body cannot be set.", "SetInnerText", ex);
                }

            }

        } //InnerText


        // the HTML value for the body contents
        // it is this value that gets serialized by the designer
        [Category("Textual"), Description("The Inner HTML of the contents")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string InnerHtml
        {
            get
            {
                _bodyText = body.innerText;
                _bodyHtml = body.innerHTML;
                return _bodyHtml;
            }
            set
            {
                try
                {
                    // clear the defined body url
                    _bodyUrl = string.Empty;
                    if (value == null) value = string.Empty;
                    // set the body property
                    body.innerHTML = value;
                    // set the body text and html
                    _bodyText = body.innerText;
                    _bodyHtml = body.innerHTML;
                    // if needed rebase urls
                    RebaseAnchorUrl();
                }
                catch (Exception ex)
                {
                    throw new HtmlEditorException("Inner Html for the body cannot be set.", "SetInnerHtml", ex);
                }
            }

        } //InnerHtml


        // returns and sets the body tag of the html
        // on set the body attributes need to be defined
        [Category("Textual"), Description("Complete Document including Body Tag")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string BodyHtml
        {
            get
            {
                // set the read only property before return
                body.contentEditable = CONTENT_EDITABLE_INHERIT;
                string html = body.outerHTML.Trim();
                this.ReadOnly = _readOnly;
                return html;

            }
            set
            {
                // clear the defined body url
                _bodyUrl = string.Empty;

                // define some local working variables
                string bodyElement = string.Empty;
                string innerHtml = string.Empty;

                try
                {
                    // ensure have body open and close tags
                    if (Regex.IsMatch(value, BODY_PARSE_PRE_EXPRESSION, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline))
                    {
                        // define a regular expression for the Html Body parsing and obtain the match expression
                        Regex expression = new Regex(BODY_PARSE_EXPRESSION, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
                        Match match = expression.Match(value);
                        // see if a match was found
                        if (match.Success)
                        {
                            // extract the body tag and the inner html
                            bodyElement = match.Result(BODY_TAG_PARSE_MATCH);
                            innerHtml = match.Result(BODY_INNER_PARSE_MATCH);
                            // remove whitespaces from the body and inner html tags
                            bodyElement = bodyElement.Trim();
                            innerHtml = innerHtml.Trim();
                        }
                    }
                    // ensure body was set
                    if (bodyElement == string.Empty)
                    {
                        // assume the Html given is an inner html with no body
                        bodyElement = BODY_DEFAULT_TAG;
                        innerHtml = value.Trim();
                    }

                    // first navigate to a blank page to reset the html header
                    BrowserCodeNavigate(BLANK_HTML_PAGE);

                    // replace the body tag with the one passed in
                    HtmlDomNode oldBodyNode = (HtmlDomNode)document.body;
                    HtmlDomNode newBodyNode = (HtmlDomNode)document.createElement(bodyElement);
                    oldBodyNode.replaceNode(newBodyNode);

                    // define the new inner html and body objects
                    body = (HtmlBody)document.body;
                    body.innerHTML = innerHtml;

                    // now all successfully loaded need to review the body attributes
                    _bodyText = body.innerText;
                    _bodyHtml = body.innerHTML;

                    // set and define the appropriate properties
                    // this will set the appropriate read only property
                    DefineBodyAttributes();

                    // if needed rebase urls
                    RebaseAnchorUrl();
                }
                catch (Exception ex)
                {
                    throw new HtmlEditorException("Outer Html for the body cannot be set.", "SetBodyHtml", ex);
                }
            }

        } //BodyHtml


        // return the html tag of the document
        // should never be set as contains the HEAD tag
        [Category("Textual"), Description("Complete Document including Head and Body Tag")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string DocumentHtml
        {
            get
            {
                return document.documentElement.outerHTML.Trim();
            }

            set
            {
                document.documentElement.outerHTML = value;
            }

        } //DocumentHtml


        // returns or sets the Text selected by the user
        [Category("Textual"), Description("The Text selected by the User")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string SelectedText
        {
            get
            {
                // obtain the selected range object
                HtmlTextRange range = GetTextRange();
                // return the text of the range
                if (range.text != null) return range.text;
                else return string.Empty;
            }
            set
            {
                try
                {
                    // obtain the selected range object
                    HtmlTextRange range = GetTextRange();
                    // set the text range
                    if (range != null)
                    {
                        // encode the text to encode any html markup
                        string html = HttpUtility.HtmlEncode(value);
                        // once have a range execute the pasteHtml command
                        // this will overwrite the current selection
                        range.pasteHTML(html);
                    }
                }
                catch (Exception ex)
                {
                    throw new HtmlEditorException("Inner Text for the selection cannot be set.", "SetSelectedText", ex);
                }
            }

        } //SelectedText


        // returns or sets the Html selected by the user
        [Category("Textual"), Description("The Text selected by the User")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string SelectedHtml
        {
            get
            {
                // obtain the selected range object
                HtmlTextRange range = GetTextRange();
                // return the text of the range
                if (range.text != null) return range.htmlText;
                else return string.Empty;
            }
            set
            {
                try
                {
                    // obtain the selected range object
                    HtmlTextRange range = GetTextRange();
                    // set the text range
                    if (range != null)
                    {
                        // once have a range execute the pasteHtml command
                        // this will overwrite the current selection
                        range.pasteHTML(value);
                        // if needed rebase urls
                        RebaseAnchorUrl();
                    }
                }
                catch (Exception ex)
                {
                    throw new HtmlEditorException("Inner Html for the selection cannot be set.", "SetSelectedHtml", ex);
                }
            }

        } //SelectedHtml


        // returns any Url that was used to load the current document
        [Category("Textual"), Description("Url used to load the Document")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public string DocumentUrl
        {
            get
            {
                //return document.baseUrl;
                return _bodyUrl;
            }
        }

        #endregion

        #region Body Properties (Font and Color)

        // body background color
        // reset and serialize values defined
        [Category("Textual"), Description("Defines the Background Color of the Body")]
        public Color BodyBackColor
        {
            get
            {
                return _bodyBackColor;
            }
            set
            {
                // set the new value using a default if Empty passed in
                if (value != Color.Empty) _bodyBackColor = value;
                else _bodyBackColor = _defaultBackColor;
                // set the bgcolor attribute of the body
                if (body != null)
                {
                    if (_bodyBackColor.ToArgb() == _defaultBackColor.ToArgb())
                    {
                        document.bgColor = string.Empty;
                    }
                    else
                    {
                        document.bgColor = ColorTranslator.ToHtml(_bodyBackColor);
                    }
                }
            }

        } //BodyBackColor

        public bool ShouldSerializeBodyBackColor()
        {
            return (_bodyBackColor != _defaultBackColor);

        } //ShouldSerializeBodyBackColor

        public void ResetBodyBackColor()
        {
            this.BodyBackColor = _defaultBackColor;

        } //ResetBodyBackColor


        // body foreground color
        // reset and serialize values defined
        [Category("Textual"), Description("Defines the Foreground Color of the Body")]
        public Color BodyForeColor
        {
            get
            {
                return _bodyForeColor;
            }
            set
            {
                // set the new value using a default if Empty passed in
                if (value != Color.Empty) _bodyForeColor = value;
                else _bodyForeColor = _defaultForeColor;
                // set the text attribute of the body
                if (body != null)
                {
                    if (_bodyForeColor.ToArgb() == _defaultForeColor.ToArgb())
                    {
                        document.fgColor = string.Empty;
                    }
                    else
                    {
                        document.fgColor = ColorTranslator.ToHtml(_bodyForeColor);
                    }
                }
            }

        } //BodyForeColor

        public bool ShouldSerializeBodyForeColor()
        {
            return (_bodyForeColor != _defaultForeColor);

        } //ShouldSerializeBodyForeColor

        public void ResetBodyForeColor()
        {
            this.BodyForeColor = _defaultForeColor;

        } //ResetBodyForeColor


        // base font to use for text editing
        // can be overriden in code or through Font property
        [Category("Textual"), Description("Defines the base Font object for the Body")]
        [RefreshProperties(RefreshProperties.All)]
        public HtmlFontProperty BodyFont
        {
            get
            {
                return _bodyFont;
            }
            set
            {
                // set the new value using the default if set to null
                if (HtmlFontProperty.IsNotNull(value)) _bodyFont = value;
                else _bodyFont = _defaultFont;
                // set the font attributes based on any body styles
                HtmlStyle bodyStyle = body.style;
                if (bodyStyle != null)
                {
                    if (HtmlFontProperty.IsEqual(_bodyFont, _defaultFont))
                    {
                        // ensure no values are set in the Body style
                        if (bodyStyle.fontFamily != null) bodyStyle.fontFamily = string.Empty;
                        if (bodyStyle.fontSize != null) bodyStyle.fontSize = string.Empty;
                        if (bodyStyle.fontWeight != null) bodyStyle.fontWeight = string.Empty;
                    }
                    else
                    {
                        // set the body styles based on the defined value
                        bodyStyle.fontFamily = _bodyFont.Name;
                        bodyStyle.fontSize = HtmlFontConversion.HtmlFontSizeString(_bodyFont.Size);
                        bodyStyle.fontWeight = HtmlFontConversion.HtmlFontBoldString(_bodyFont.Bold);
                    }
                }
            }

        } //BodyFont

        public bool ShouldSerializeBodyFont()
        {
            return (HtmlFontProperty.IsNotEqual(_bodyFont, _defaultFont));

        } //ShouldSerializeBodyFont

        public void ResetBodyFont()
        {
            this.BodyFont = _defaultFont;

        } //ResetBodyFont


        #endregion

        #region Document Processing Operations

        // allow the user to load a document by navigation
        public void NavigateToUrl(string url)
        {
            // load the requested use Url
            BrowserCodeNavigate(url);

            // now all successfully laoded need to review the body attributes
            _bodyText = body.innerText;
            _bodyHtml = body.innerHTML;

            // set and define the appropriate properties
            DefineBodyAttributes();

        } //NavigateToUrl

        // allow the user to load a document into the specified window
        public void NavigateToUrl(string url, bool newWindow)
        {
            if (newWindow)
            {
                // open the Url in a new window
                object window = (object)TARGET_WINDOW_NEW;
                this.editorWebBrowser.Navigate(url, ref EMPTY_PARAMETER, ref window, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);
            }
            else
            {
                // if no new window required call the normal navigate method
                NavigateToUrl(url);
            }

        } //NavigateToUrl


        // allow the user to load a document from a Url
        // the body tag is used and loaded
        public void LoadFromUrl(string url)
        {

            HttpWebRequest webRqst = null;
            HttpWebResponse webResp = null;
            Stream stream = null;
            // load the body at the given url into the html editor
            try
            {
                // Creates an HttpWebRequest with the specified URL. 
                webRqst = (HttpWebRequest)WebRequest.Create(url);
                // setup default credentials since may be in an intranet
                webRqst.Credentials = CredentialCache.DefaultCredentials;
                // send the HttpWebRequest and waits for the response.            
                webResp = (HttpWebResponse)webRqst.GetResponse();

                // parse the content type to determine the maintype and subtype
                string contenttype = webResp.ContentType;
                string maintype = string.Empty;
                string subtype = string.Empty;
                string charset = string.Empty;
                Regex expression = new Regex(CONTENTTYPE_PARSE_EXPRESSION, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
                Match match = expression.Match(contenttype);
                // see if a match was found
                if (match.Success)
                {
                    // extract the content type elements
                    maintype = match.Result(CONTENTTYPE_PARSE_MAINTYPE);
                    subtype = match.Result(CONTENTTYPE_PARSE_SUBTYPE);
                    charset = match.Result(CONTENTTYPE_PARSE_CHARSET);
                }

                // retrieves the text if the content type is of text/html.
                if (IsStatedTag(maintype, "text") && IsStatedTag(subtype, "html"))
                {
                    // define the encoder to use
                    Encoding encoder;
                    if (charset == string.Empty) charset = @"utf-8";
                    try
                    {
                        // base the encoder from the charset calculated
                        encoder = Encoding.GetEncoding(charset);
                    }
                    catch (Exception)
                    {
                        // use a default UTF8 encoder
                        encoder = Encoding.UTF8;
                    }
                    // read the response stream
                    StringBuilder html = new StringBuilder();
                    // read the response buffer and return the string representation
                    stream = webResp.GetResponseStream();
                    byte[] outputBuffer = new byte[HTML_BUFFER_SIZE];
                    // read the stream in buffer size blocks
                    int bytesRead = 0;
                    do
                    {
                        bytesRead = stream.Read(outputBuffer, 0, HTML_BUFFER_SIZE);
                        if (bytesRead > 0)
                        {
                            html.Append(encoder.GetString(outputBuffer, 0, bytesRead));
                        }
                    } while (bytesRead > 0);

                    // populate the string value from the text
                    this.BodyHtml = html.ToString();
                }
                else
                {
                    // navigated to a none Html document so throw exception
                    throw new HtmlEditorException(string.Format("Not a Html Document: {0}", url), "LoadFromUrl");
                }
            }
            catch (HtmlEditorException ex)
            {
                // cannot load so throw an exception
                throw ex;
            }
            catch (WebException ex)
            {
                // cannot load so throw an exception
                throw new HtmlEditorException(string.Format("Cannot load Url: {0}", url), "LoadFromUrl", ex);
            }
            catch (Exception ex)
            {
                // cannot load so throw an exception
                throw new HtmlEditorException(string.Format("Cannot load Document: {0}", url), "LoadFromUrl", ex);
            }
            finally
            {
                // close the stream reader
                if (stream != null) stream.Close();
                // close the web response
                if (webResp != null) webResp.Close();
            }

        } //LoadFromUrl


        // allow a user to load a file given a file name
        public void LoadFromFile(string filename)
        {
            // init the container for the Html
            string contents = string.Empty;

            // check to see if the file exists
            if (!File.Exists(filename))
            {
                throw new HtmlEditorException("Not a valid file name.", "LoadFromFile");
            }

            // read the file contents
            //using (StreamReader reader = File.OpenText(filename))
            using (StreamReader reader = new StreamReader(filename, Encoding.UTF8))
            {
                contents = reader.ReadToEnd();
            }
            // if the contents where successfully read write to document
            if (contents != String.Empty)
            {
                this.BodyHtml = contents;
            }

        } //LoadFromFile


        // allow the user to select a file and read the contents into the Html stream
        public void OpenFilePrompt()
        {
            // init the container for the Html
            string contents = string.Empty;

            // create an open file dialog
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                // init the input stream
                Stream stream = null;
                // define the dialog structure
                dialog.DefaultExt = HTML_EXTENSION;
                dialog.Title = HTML_TITLE_OPENFILE;
                dialog.AddExtension = true;
                dialog.Filter = HTML_FILTER;
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;
                if (_htmlDirectory != String.Empty) dialog.InitialDirectory = _htmlDirectory;
                // show the dialog and see if the users enters OK
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // look to see if the file contains any contents and stream
                    if ((stream = dialog.OpenFile()) != null)
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            // get the contents as text
                            contents = reader.ReadToEnd();
                            // persist the path navigation
                            _htmlDirectory = Path.GetDirectoryName(dialog.FileName);
                        }
                    }
                    // close the input stream
                    if (stream != null) stream.Close();
                }
            }
            // if the contents where successfully read write to document
            if (contents != String.Empty)
            {
                this.BodyHtml = contents;
            }

        } //OpenFilePrompt


        // allow the user to persist the Html stream to a file
        public void SaveFilePrompt()
        {
            // obtain the html contents
            string contents = this.BodyHtml;
            // create a file save dialog
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                // init the outpu stream
                Stream stream = null;
                // define the dialog structure
                dialog.DefaultExt = HTML_EXTENSION;
                dialog.Title = HTML_TITLE_SAVEFILE;
                dialog.AddExtension = true;
                dialog.Filter = HTML_FILTER;
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;
                if (_htmlDirectory != String.Empty) dialog.InitialDirectory = _htmlDirectory;
                // show the dialog and see if the users enters OK
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // look to see if the stream can be open for output
                    if ((stream = dialog.OpenFile()) != null)
                    {
                        // create the stream writer for the html
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            // write out the body contents to the stream
                            writer.Write(contents);
                            writer.Flush();
                            // persist the path navigation
                            _htmlDirectory = Path.GetDirectoryName(dialog.FileName);
                        }
                    }
                    // close the input stream
                    if (stream != null) stream.Close();
                }
            }

        } //SaveFilePrompt


        // define the style sheet to be used for editing
        // can be used for standard templates
        public void LinkStyleSheet(string stylesheetHref)
        {
            if (stylesheet == null)
            {                
                stylesheet = (HtmlStyleSheet)document.createStyleSheet(stylesheetHref, 0);
            }

        } //LinkStyleSheet

        // return to the user the style sheet href being used
        public string GetStyleSheetHref()
        {
            if (stylesheet != null) return stylesheet.href;
            else return string.Empty;

        } //GetStyleSheetHref


        // define a script element that is to be used by all documents
        // can be sued for document processing
        public void LinkScriptSource(string scriptSource)
        {
            if (script != null)
            {
                script.src = scriptSource;
            }
            else
            {
                // create the script element
                script = (HtmlScriptElement)document.createElement(SCRIPT_TAG);
                script.src = scriptSource;
                script.defer = true;
                // insert into the document
                HtmlDomNode node = (HtmlDomNode)document.documentElement;
                node.appendChild((HtmlDomNode)script);
            }

        } //LinkScriptSource

        // return to the user the script block source being used
        public string GetScriptBlockSource()
        {
            if (script != null) return script.src;
            else return string.Empty;

        } //GetScriptBlockSource


        // allow the user to edit the raw HTML
        // dialog presented and the body contents set
        public void HtmlContentsEdit()
        {
            using (EditHtmlForm dialog = new EditHtmlForm())
            {
                dialog.HTML = this.InnerHtml;
                dialog.ReadOnly = false;
                dialog.SetCaption(HTML_TITLE_EDIT);
                DefineDialogProperties(dialog);
                if (dialog.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    this.InnerHtml = dialog.HTML;
                }
            }

        } //HtmlContentsEdit

        // allow the user to view the html contents
        // the complete Html markup is presented
        public void HtmlContentsView()
        {
            using (EditHtmlForm dialog = new EditHtmlForm())
            {
                dialog.HTML = this.DocumentHtml;
                dialog.ReadOnly = true;
                dialog.SetCaption(HTML_TITLE_VIEW);
                DefineDialogProperties(dialog);
                dialog.ShowDialog(this.ParentForm);
            }

        } //HtmlContentsView


        // print the html text using the document print command
        // print preview is not supported
        public void DocumentPrint()
        {
            ExecuteCommandDocument(HTML_COMMAND_TEXT_PRINT);

        } //TextPrint


        // toggle the overwrite mode
        public void ToggleOverWrite()
        {
            ExecuteCommandDocument(HTML_COMMAND_OVERWRITE);

        } //ToggleOverWrite

        #endregion

        #region Document Text Operations

        // cut the currently selected text to the clipboard
        public void TextCut()
        {
            //this.editorWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_CUT, PROMPT_USER_NO, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);
            ExecuteCommandDocument(HTML_COMMAND_TEXT_CUT);

        } //TextCut

        // copy the currently selected text to the clipboard
        public void TextCopy()
        {
            //this.editorWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_COPY, PROMPT_USER_NO, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);
            ExecuteCommandDocument(HTML_COMMAND_TEXT_COPY);

        } //TextCopy

        // paste the currently selected text from the clipboard
        public void TextPaste()
        {
            //this.editorWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_PASTE, PROMPT_USER_NO, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);
            ExecuteCommandDocument(HTML_COMMAND_TEXT_PASTE);

        } //TextPaste

        // delete the currently selected text from the screen
        public void TextDelete()
        {
            //this.editorWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_DELETE, PROMPT_USER_NO, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);
            ExecuteCommandDocument(HTML_COMMAND_TEXT_DELETE);

        } //TextDelete

        // select the entire document contents
        public void TextSelectAll()
        {
            //this.editorWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_SELECTALL, PROMPT_USER_NO, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);
            ExecuteCommandDocument(HTML_COMMAND_TEXT_SELECT_ALL);

        } //TextSelectAll

        // clear the document selection
        public void TextClearSelect()
        {
            //this.editorWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_CLEARSELECTION, PROMPT_USER_NO, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);
            ExecuteCommandDocument(HTML_COMMAND_TEXT_UNSELECT);

        } //TextClearSelect


        // undo former commands
        public void EditUndo()
        {
            //this.editorWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_UNDO, PROMPT_USER_NO, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);
            ExecuteCommandDocument(HTML_COMMAND_TEXT_UNDO);

        } //EditUndo

        // redo former undo
        public void EditRedo()
        {
            //this.editorWebBrowser.ExecWB(SHDocVw.OLECMDID.OLECMDID_REDO, PROMPT_USER_NO, ref EMPTY_PARAMETER, ref EMPTY_PARAMETER);
            ExecuteCommandDocument(HTML_COMMAND_TEXT_REDO);

        } //EditRedo

        #endregion

        #region Selected Text Formatting Operations

        // using the document set the font name
        public void FormatFontName(string name)
        {
            ExecuteCommandRange(HTML_COMMAND_FONT_NAME, name);

        } //FormatFontName

        // using the document set the Html font size
        public void FormatFontSize(HtmlFontSize size)
        {
            ExecuteCommandRange(HTML_COMMAND_FONT_SIZE, (int)size);

        } //FormatFontSize


        // using the document toggles the selection with a bold tag
        public void FormatBold()
        {
            ExecuteCommandRange(HTML_COMMAND_BOLD, null);

        } //FormatBold

        // using the document toggles the selection with a underline tag
        public void FormatUnderline()
        {
            ExecuteCommandRange(HTML_COMMAND_UNDERLINE, null);

        } //FormatUnderline

        // using the document toggles the selection with a italic tag
        public void FormatItalic()
        {
            ExecuteCommandRange(HTML_COMMAND_ITALIC, null);

        } //FormatItalic

        // using the document toggles the selection with a Subscript tag
        public void FormatSubscript()
        {
            ExecuteCommandRange(HTML_COMMAND_SUBSCRIPT, null);

        } //FormatSubscript

        // using the document toggles the selection with a Superscript tag
        public void FormatSuperscript()
        {
            ExecuteCommandRange(HTML_COMMAND_SUPERSCRIPT, null);

        } //FormatSuperscript

        // using the document toggles the selection with a Strikeout tag
        public void FormatStrikeout()
        {
            ExecuteCommandRange(HTML_COMMAND_STRIKE_THROUGH, null);

        } //FormatStrikeout


        // increase the size of the font by 1 point
        public void FormatFontIncrease()
        {
            FormatFontChange(1);

        } //FormatFontIncrease

        // decrease the size of the font by 1 point
        public void FormatFontDecrease()
        {
            FormatFontChange(-1);

        } //FormatFontDecrease

        // given a int value increase the font size by that amount
        private void FormatFontChange(int change)
        {
            // ensure the command is acceptable
            if (Math.Abs(change) > 6)
            {
                throw new HtmlEditorException("Value can only be in the range [1,6].", "FontIncreaseDecrease");
            }
            else
            {
                // obtain the selected range object
                HtmlTextRange range = GetTextRange();

                // obtain the original font value
                object fontSize = QueryCommandRange(range, HTML_COMMAND_FONT_SIZE);
                int oldFontSize = (fontSize is System.Int32) ? (int)fontSize : (int)_bodyFont.Size;
                // calc the new font size and modify
                int newFontSize = oldFontSize + change;
                ExecuteCommandRange(range, HTML_COMMAND_FONT_SIZE, newFontSize);
            }

        } //FormatFontChange


        // remove any formatting tags
        public void FormatRemove()
        {
            ExecuteCommandRange(HTML_COMMAND_REMOVE_FORMAT, null);

        } //FormatRemove


        // Tab the current line to the right
        public void FormatTabRight()
        {
            ExecuteCommandRange(HTML_COMMAND_INDENT, null);

        } //FormatTabRight

        // Tab the current line to the left
        public void FormatTabLeft()
        {
            ExecuteCommandRange(HTML_COMMAND_OUTDENT, null);

        } //FormatTabLeft


        // insert a ordered or unordered list
        public void FormatList(HtmlListType listtype)
        {
            string command = string.Format(HTML_COMMAND_INSERT_LIST, listtype.ToString());
            ExecuteCommandRange(command, null);

        } //FormatList
        // define the font justification as FULL
        public void JustifyFull()
        {
            ExecuteCommandRange(HTML_COMMAND_JUSTIFY_FULL, null);

        }

        // define the font justification as LEFT
        public void JustifyLeft()
        {
            ExecuteCommandRange(HTML_COMMAND_JUSTIFY_LEFT, null);

        } //JustifyLeft

        // define the font justification as CENTER
        public void JustifyCenter()
        {
            ExecuteCommandRange(HTML_COMMAND_JUSTIFY_CENTER, null);

        } //JustifyCenter

        // define the font justification as Right
        public void JustifyRight()
        {
            ExecuteCommandRange(HTML_COMMAND_JUSTIFY_RIGHT, null);

        } //JustifyRight

        #endregion

        #region Object Insert Operations

        // insert a horizontal line in the body
        // if have a control range rather than text range one could overwrite the controls with the line
        public void InsertLine()
        {
            HtmlTextRange range = GetTextRange();
            if (range != null)
            {
                ExecuteCommandRange(range, HTML_COMMAND_INSERT_LINE, null);
            }
            else
            {
                throw new HtmlEditorException("Invalid Selection for Line insertion.", "InsertLine");
            }

        } //InsertLine


        // insert an image tag at the selected location
        public void InsertImage(string imageLocation)
        {
            ExecuteCommandRange(HTML_COMMAND_INSERT_IMAGE, imageLocation);

        } //InsertImage

        // public function to insert a image and prompt a user for the link
        // calls the public InsertImage method
        public void InsertImagePrompt()
        {
            // set default image and text tags
            string imageText = string.Empty;
            string imageHref = string.Empty;
            ImageAlignOption imageAlign = ImageAlignOption.Left;
            HtmlElement control = null;

            // look to see if an image has been selected
            control = GetFirstControl();
            if (control != null)
            {
                if (IsStatedTag(control.tagName, IMAGE_TAG))
                {
                    HtmlImageElement image = (HtmlImageElement)control;
                    imageHref = image.href;
                    imageText = image.alt;
                    if (image.align != null) imageAlign = (ImageAlignOption)TryParseEnum(typeof(ImageAlignOption), image.align, ImageAlignOption.Left);
                }
                else
                {
                    throw new HtmlEditorException("Can only Insert an Image over a previous Image.", "InsertImage");
                }
            }

            // Obtain the image file name
            // prompt the user for the new href
            using (EnterImageForm dialog = new EnterImageForm())
            {
                // set the dialog properties
                dialog.ImageLink = imageHref;
                dialog.ImageText = imageText;
                dialog.ImageAlign = imageAlign;
                DefineDialogProperties(dialog);
                // based on the user interaction perform the neccessary action
                // after one has a valid image href
                if (dialog.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    imageHref = dialog.ImageLink;
                    imageText = dialog.ImageText;
                    imageAlign = dialog.ImageAlign;
                    if (imageHref != string.Empty)
                    {
                        ExecuteCommandRange(HTML_COMMAND_INSERT_IMAGE, imageHref);
                        control = GetFirstControl();
                        if (control != null)
                        {
                            if (imageText == string.Empty) imageText = imageHref;
                            if (IsStatedTag(control.tagName, IMAGE_TAG))
                            {
                                HtmlImageElement image = (HtmlImageElement)control;
                                image.alt = imageText;
                                if (imageAlign != ImageAlignOption.Left) image.align = imageAlign.ToString().ToLower();
                            }
                        }
                    }
                }
            }

        } //InsertImagePrompt


        // create a web link from the users selected text
        public void InsertLink(string href)
        {
            ExecuteCommandRange(HTML_COMMAND_INSERT_LINK, href);

        } //InsertLink

        // public function to insert a link and prompt a user for the href
        // calls the public InsertLink method
        public void InsertLinkPrompt()
        {
            // get the text range working with
            HtmlTextRange range = GetTextRange();
            string hrefText = (range == null) ? null : range.text;
            string hrefLink = string.Empty;
            NavigateActionOption target;

            // ensure have text in the range otherwise nothing works
            if (hrefText != null)
            {
                // calculate the items working with
                HtmlAnchorElement anchor = null;
                HtmlElement element = (HtmlElement)range.parentElement();
                // parse up the tree until the anchor element is found
                while (element != null && !(element is HtmlAnchorElement))
                {
                    element = (HtmlElement)element.parentElement;
                }
                // extract the HREF properties
                if (element is HtmlAnchorElement)
                {
                    anchor = (HtmlAnchorElement)element;
                    if (anchor.href != null) hrefLink = anchor.href;
                }
                // if text is a valid href then set the link
                if (hrefLink == string.Empty && IsValidHref(hrefText))
                {
                    hrefLink = hrefText;
                }

                // prompt the user for the new href
                using (EnterHrefForm dialog = new EnterHrefForm())
                {
                    dialog.HrefText = hrefText;
                    dialog.HrefLink = hrefLink;
                    DefineDialogProperties(dialog);
                    DialogResult result = dialog.ShowDialog(this.ParentForm);
                    // based on the user interaction perform the neccessary action
                    // after one has a valid href
                    if (result == DialogResult.Yes)
                    {
                        hrefLink = dialog.HrefLink;
                        target = dialog.HrefTarget;
                        if (IsValidHref(hrefLink))
                        {
                            // insert or update the current link
                            if (anchor == null)
                            {
                                ExecuteCommandRange(range, HTML_COMMAND_INSERT_LINK, hrefLink);
                                element = (HtmlElement)range.parentElement();
                                // parse up the tree until the anchor element is found
                                while (element != null && !(element is HtmlAnchorElement))
                                {
                                    element = (HtmlElement)element.parentElement;
                                }
                                if (element != null) anchor = (HtmlAnchorElement)element;
                            }
                            else
                            {
                                anchor.href = hrefLink;
                            }
                            if (target != NavigateActionOption.Default)
                            {
                                anchor.target = (target == NavigateActionOption.NewWindow) ? TARGET_WINDOW_NEW : TARGET_WINDOW_SAME;
                            }
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        // remove the current link assuming present
                        if (anchor != null) ExecuteCommandRange(range, HTML_COMMAND_REMOVE_LINK, null); ;
                    }
                }
            }
            else
            {
                throw new HtmlEditorException(ResourceHelper.GetResourceText("HTMLWarningText"), ResourceHelper.GetResourceText("HTMLWarningTitle"));
            }

        } //InsertLinkPrompt

        // public function to insert a link to a document and prompt a user for the href
        // calls the public InsertLink method
        public void InsertLinkToDocumentPrompt()
        {
            // get the text range working with
            HtmlTextRange range = GetTextRange();
            string hrefText = (range == null) ? null : range.text;
            string hrefLink = string.Empty;
            NavigateActionOption target = NavigateActionOption.Default;

            // ensure have text in the range otherwise nothing works
            if (hrefText != null)
            {
                // calculate the items working with
                HtmlAnchorElement anchor = null;
                HtmlElement element = (HtmlElement)range.parentElement();
                // parse up the tree until the anchor element is found
                while (element != null && !(element is HtmlAnchorElement))
                {
                    element = (HtmlElement)element.parentElement;
                }
                // extract the HREF properties
                if (element is HtmlAnchorElement)
                {
                    anchor = (HtmlAnchorElement)element;
                    if (anchor.href != null) hrefLink = anchor.href;
                }
                // if text is a valid href then set the link
                if (hrefLink == string.Empty && IsValidHref(hrefText))
                {
                    hrefLink = hrefText;
                }

                // prompt the user for the new href
                using (LinkDocumentForm dialog = new LinkDocumentForm())
                {
                    dialog.HrefText = hrefText;
                    dialog.HrefLink = hrefLink;
                    DefineDialogProperties(dialog);
                    DialogResult result = dialog.ShowDialog(this.ParentForm);
                    // based on the user interaction perform the neccessary action
                    // after one has a valid href

                    hrefLink = dialog.HrefLink;                    
                    if (!string.IsNullOrEmpty(hrefLink))
                    {
                        hrefLink = "./Documents/" + hrefLink;
                        // insert or update the current link
                        if (anchor == null)
                        {
                            ExecuteCommandRange(range, HTML_COMMAND_INSERT_LINK, hrefLink);
                            element = (HtmlElement)range.parentElement();
                            // parse up the tree until the anchor element is found
                            while (element != null && !(element is HtmlAnchorElement))
                            {
                                element = (HtmlElement)element.parentElement;
                            }
                            if (element != null) anchor = (HtmlAnchorElement)element;
                        }
                        else
                        {
                            anchor.href = hrefLink;
                        }
                        if (target != NavigateActionOption.Default)
                        {
                            anchor.target = (target == NavigateActionOption.NewWindow)
                                                ? TARGET_WINDOW_NEW
                                                : TARGET_WINDOW_SAME;
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show(ResourceHelper.GetResourceText("HTMLWarningText"),
                    ResourceHelper.GetResourceText("HTMLWarningTitle"), 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        } //InsertLinkPrompt

        // remove a web link from the users selected text
        public void RemoveLink()
        {
            ExecuteCommandRange(HTML_COMMAND_REMOVE_LINK, null);

        } //InsertLink

        #endregion

        #region Text Insert Operations

        // insert the given HTML into the selected range
        public void InsertHtmlPrompt()
        {
            // display the dialog to obtain the Html to enter
            using (EditHtmlForm dialog = new EditHtmlForm())
            {
                dialog.HTML = "";
                dialog.ReadOnly = false;
                dialog.SetCaption(PASTE_TITLE_HTML);
                DefineDialogProperties(dialog);
                if (dialog.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    this.SelectedHtml = dialog.HTML;
                }
            }

        } //InsertHtmlPrompt


        // insert the given Text into the selected range
        public void InsertTextPrompt()
        {
            // display the dialog to obtain the Html to enter
            using (EditHtmlForm dialog = new EditHtmlForm())
            {
                dialog.HTML = "";
                dialog.ReadOnly = false;
                dialog.SetCaption(PASTE_TITLE_TEXT);
                DefineDialogProperties(dialog);
                if (dialog.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    this.SelectedText = dialog.HTML;
                }
            }

        } //InsertTextPrompt


        // returns the acceptable values for the format block
        public string[] GetFormatBlockCommands()
        {
            return formatCommands;

        } //GetFormatBlockCommands

        // formats the selected text wrapping items in the given format tag
        public void InsertFormatBlock(string command)
        {
            // ensure the command is acceptable
            if (Array.BinarySearch(formatCommands, command) < 0)
            {
                throw new HtmlEditorException("Invalid Format Block selection.", "InsertFormatBlock");
            }
            else
            {
                ExecuteCommandRange(HTML_COMMAND_INSERT_FORMAT_BLOCK, command);
            }

        } //InsertFormatBlock


        // formats the selected text wrapping items in a PRE tag for direct editing
        public void InsertFormattedBlock()
        {
            ExecuteCommandRange(HTML_COMMAND_INSERT_FORMAT_BLOCK, FORMATTED_PRE);

        } //InsertFormattedBlock

        // unformats the selected text removing heading and pre tags
        public void InsertNormalBlock()
        {
            ExecuteCommandRange(HTML_COMMAND_INSERT_FORMAT_BLOCK, FORMATTED_NORMAL);

        } //InsertNormalBlock

        // inserts a heading tag values Heading 1,2,3,4,5
        public void InsertHeading(HtmlHeadingType headingType)
        {
            // obtain the selected range object
            HtmlTextRange range = GetTextRange();

            // determine the appropriate heading tag and insert the heading
            string command = string.Format("{0} {1}", FORMATTED_HEADING, (int)headingType);
            InsertFormatBlock(command);

        } //InsertHeading

        #endregion

        #region System Prompt Dialog Functions

        // allows the insertion of an image using the system dialogs
        public void SystemInsertImage()
        {
            ExecuteCommandDocumentPrompt(HTML_COMMAND_INSERT_IMAGE);

        } //InsertImageSystem


        // allows the insertion of an href using the system dialogs
        public void SystemInsertLink()
        {
            ExecuteCommandDocumentPrompt(HTML_COMMAND_INSERT_LINK);

        } //InsertImageSystem

        #endregion

        #region Font and Color Processing Operations

        // using the exec command define font properties for the selected text
        public void FormatFontAttributes(HtmlFontProperty font)
        {
            // obtain the selected range object
            HtmlTextRange range = GetTextRange();

            if (range != null && HtmlFontProperty.IsNotNull(font))
            {
                // Use the FONT object to set the properties
                // FontName, FontSize, Bold, Italic, Underline, Strikeout
                ExecuteCommandRange(range, HTML_COMMAND_FONT_NAME, font.Name);
                // set the font size provide set to a value
                if (font.Size != HtmlFontSize.Default) ExecuteCommandRange(range, HTML_COMMAND_FONT_SIZE, (int)font.Size);
                // determine the BOLD property and correct as neccessary
                object currentBold = QueryCommandRange(range, HTML_COMMAND_BOLD);
                bool fontBold = (currentBold is System.Boolean) ? fontBold = (bool)currentBold : false;
                if (font.Bold != fontBold) ExecuteCommandRange(HTML_COMMAND_BOLD, null);
                // determine the UNDERLINE property and correct as neccessary
                object currentUnderline = QueryCommandRange(range, HTML_COMMAND_UNDERLINE);
                bool fontUnderline = (currentUnderline is System.Boolean) ? fontUnderline = (bool)currentUnderline : false;
                if (font.Underline != fontUnderline) ExecuteCommandRange(HTML_COMMAND_UNDERLINE, null);
                // determine the ITALIC property and correct as neccessary
                object currentItalic = QueryCommandRange(range, HTML_COMMAND_ITALIC);
                bool fontItalic = (currentItalic is System.Boolean) ? fontItalic = (bool)currentItalic : false;
                if (font.Italic != fontItalic) ExecuteCommandRange(HTML_COMMAND_ITALIC, null);
                // determine the STRIKEOUT property and correct as neccessary
                object currentStrikeout = QueryCommandRange(range, HTML_COMMAND_STRIKE_THROUGH);
                bool fontStrikeout = (currentStrikeout is System.Boolean) ? fontStrikeout = (bool)currentStrikeout : false;
                if (font.Strikeout != fontStrikeout) ExecuteCommandRange(HTML_COMMAND_STRIKE_THROUGH, null);
                // determine the SUPERSCRIPT property and correct as neccessary
                object currentSuperscript = QueryCommandRange(range, HTML_COMMAND_SUPERSCRIPT);
                bool fontSuperscript = (currentSuperscript is System.Boolean) ? fontSuperscript = (bool)currentSuperscript : false;
                if (font.Superscript != fontSuperscript) ExecuteCommandRange(HTML_COMMAND_SUPERSCRIPT, null);
                // determine the SUBSCRIPT property and correct as neccessary
                object currentSubscript = QueryCommandRange(range, HTML_COMMAND_SUBSCRIPT);
                bool fontSubscript = (currentSubscript is System.Boolean) ? fontSubscript = (bool)currentSubscript : false;
                if (font.Subscript != fontSubscript) ExecuteCommandRange(HTML_COMMAND_SUBSCRIPT, null);
            }
            else
            {
                // do not have text selected or a valid font class
                throw new HtmlEditorException("Invalid Text selection made or Invalid Font selection.", "FormatFont");
            }

        } //FormatFontAttributes

        private void Footnote()
        {
            mshtml.IHTMLTxtRange range = GetTextRange();
            range.pasteHTML("<span class=\"MsoFootnoteText\">" + range.htmlText + "</span>");
        }

        // using the exec command define color properties for the selected tag
        public void FormatFontColor(Color color)
        {
            // Use the COLOR object to set the property ForeColor
            string colorHtml;
            if (color != Color.Empty) colorHtml = ColorTranslator.ToHtml(color);
            else colorHtml = null;
            ExecuteCommandRange(HTML_COMMAND_FORE_COLOR, colorHtml);

        } //FormatFontColor


        // display the defined font dialog use to set the selected text FONT
        public void FormatFontAttributesPrompt()
        {
            using (FontAttributeForm dialog = new FontAttributeForm())
            {
                DefineDialogProperties(dialog);
                dialog.HtmlFont = GetFontAttributes();
                if (dialog.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    HtmlFontProperty font = dialog.HtmlFont;
                    FormatFontAttributes(new HtmlFontProperty(font.Name, font.Size, font.Bold, font.Italic, font.Underline, font.Strikeout, font.Subscript, font.Superscript));
                }
            }

        } //FormatFontAttributesPrompt


        // display the system color dialog and use to set the selected text
        public void FormatFontColorPrompt()
        {
            // display the Color dialog and use the selected color to modify text
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.AnyColor = true;
                colorDialog.SolidColorOnly = true;
                colorDialog.AllowFullOpen = true;
                colorDialog.Color = GetFontColor();
                colorDialog.CustomColors = _customColors;
                if (colorDialog.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    _customColors = colorDialog.CustomColors;
                    FormatFontColor(colorDialog.Color);
                }
            }

        } //FormatFontColorPrompt


        // determine the Font of the selected text range
        // set to the default value of not defined
        public HtmlFontProperty GetFontAttributes()
        {
            // get the text range working with
            HtmlTextRange range = GetTextRange();

            if (range != null)
            {
                // get font name to show
                object currentName = QueryCommandRange(range, HTML_COMMAND_FONT_NAME);
                string fontName = (currentName is System.String) ? (string)currentName : _bodyFont.Name;
                // determine font size to show
                object currentSize = QueryCommandRange(range, HTML_COMMAND_FONT_SIZE);
                HtmlFontSize fontSize = (currentSize is System.Int32) ? (HtmlFontSize)currentSize : _bodyFont.Size;
                // determine the BOLD property
                bool fontBold = IsFontBold(range);
                // determine the UNDERLINE property
                bool fontUnderline = IsFontUnderline(range);
                // determine the ITALIC property
                bool fontItalic = IsFontItalic(range);
                // determine the STRIKEOUT property
                bool fontStrikeout = IsFontStrikeout(range);
                // determine the SUPERSCRIPT property
                bool fontSuperscript = IsFontSuperscript(range);
                // determine the SUBSCRIPT property
                bool fontSubscript = IsFontSubscript(range);
                // calculate the Font and return
                return new HtmlFontProperty(fontName, fontSize, fontBold, fontUnderline, fontItalic, fontStrikeout, fontSubscript, fontSuperscript);
            }
            else
            {
                // no rnage selected so return null
                return _defaultFont;
            }

        } //GetFontAttributes


        // determine if the current font selected is bold given a range
        private bool IsFontBold(HtmlTextRange range)
        {
            // determine the BOLD property
            object currentBold = QueryCommandRange(range, HTML_COMMAND_BOLD);
            return (currentBold is System.Boolean) ? (bool)currentBold : _bodyFont.Bold;

        } //IsFontBold

        // determine if the current font selected is bold given a range
        public bool IsFontBold()
        {
            // get the text range working with
            HtmlTextRange range = GetTextRange();
            // return the font property
            return IsFontBold(range);

        } //IsFontBold


        // determine if the current font selected is Underline given a range
        private bool IsFontUnderline(HtmlTextRange range)
        {
            // determine the UNDERLINE property
            object currentUnderline = QueryCommandRange(range, HTML_COMMAND_UNDERLINE);
            return (currentUnderline is System.Boolean) ? (bool)currentUnderline : _bodyFont.Underline;

        } //IsFontUnderline

        // determine if the current font selected is Underline
        public bool IsFontUnderline()
        {
            // get the text range working with
            HtmlTextRange range = GetTextRange();
            // return the font property
            return IsFontUnderline(range);

        } //IsFontUnderline


        // determine if the current font selected is Italic given a range
        private bool IsFontItalic(HtmlTextRange range)
        {
            // determine the ITALIC property
            object currentItalic = QueryCommandRange(range, HTML_COMMAND_ITALIC);
            return (currentItalic is System.Boolean) ? (bool)currentItalic : _bodyFont.Italic;

        } //IsFontItalic

        // determine if the current font selected is Italic
        public bool IsFontItalic()
        {
            // get the text range working with
            HtmlTextRange range = GetTextRange();
            // return the font property
            return IsFontItalic(range);

        } //IsFontItalic


        // determine if the current font selected is Strikeout given a range
        private bool IsFontStrikeout(HtmlTextRange range)
        {
            // determine the STRIKEOUT property
            object currentStrikeout = QueryCommandRange(range, HTML_COMMAND_STRIKE_THROUGH);
            return (currentStrikeout is System.Boolean) ? (bool)currentStrikeout : _bodyFont.Strikeout;

        } //IsFontStrikeout

        // determine if the current font selected is Strikeout
        public bool IsFontStrikeout()
        {
            // get the text range working with
            HtmlTextRange range = GetTextRange();
            // return the font property
            return IsFontStrikeout(range);

        } //IsFontStrikeout

        // determine if the current font selected is Superscript given a range
        private bool IsFontSuperscript(HtmlTextRange range)
        {
            // determine the SUPERSCRIPT property
            object currentSuperscript = QueryCommandRange(range, HTML_COMMAND_SUPERSCRIPT);
            return (currentSuperscript is System.Boolean) ? (bool)currentSuperscript : false;

        } //IsFontSuperscript

        // determine if the current font selected is Superscript
        public bool IsFontSuperscript()
        {
            // get the text range working with
            HtmlTextRange range = GetTextRange();
            // return the font property
            return IsFontSuperscript(range);

        } //IsFontSuperscript

        // determine if the current font selected is Subscript given a range
        private bool IsFontSubscript(HtmlTextRange range)
        {
            // determine the SUBSCRIPT property
            object currentSubscript = QueryCommandRange(range, HTML_COMMAND_SUBSCRIPT);
            return (currentSubscript is System.Boolean) ? (bool)currentSubscript : false;

        } //IsFontSubscript

        // determine if the current font selected is Subscript
        public bool IsFontSubscript()
        {
            // get the text range working with
            HtmlTextRange range = GetTextRange();
            // return the font property
            return IsFontSubscript(range);

        } //IsFontSubscript


        // determine the color of the selected text range
        // set to the default value of not defined
        private Color GetFontColor()
        {
            object fontColor = QueryCommandRange(HTML_COMMAND_FORE_COLOR);
            return (fontColor is System.Int32) ? ColorTranslator.FromWin32((int)fontColor) : _bodyForeColor;

        } //GetFontColor

        #endregion

        #region Find and Replace Operations

        // dialog to allow the user to perform a find and replace
        public void FindReplacePrompt()
        {

            // define a default value for the text to find
            HtmlTextRange range = GetTextRange();
            string initText = string.Empty;
            if (range != null)
            {
                string findText = range.text;
                if (findText != null) initText = findText.Trim();
            }

            // prompt the user for the new href
            using (FindReplaceForm dialog = new FindReplaceForm(initText,
                                                                new FindReplaceResetDelegate(this.FindReplaceReset),
                                                                new FindFirstDelegate(this.FindFirst),
                                                                new FindNextDelegate(this.FindNext),
                                                                new FindReplaceOneDelegate(this.FindReplaceOne),
                                                                new FindReplaceAllDelegate(this.FindReplaceAll)))
            {
                DefineDialogProperties(dialog);
                DialogResult result = dialog.ShowDialog(this.ParentForm);
            }

        } //FindReplacePrompt


        // reset the find and replace options to initialize a new search
        public void FindReplaceReset()
        {
            // reset the range being worked with
            _findRange = (HtmlTextRange)body.createTextRange();
            ((HtmlSelection)document.selection).empty();

        } //FindReplaceReset


        // finds the first occurrence of the given text string
        // uses false for the search options
        public bool FindFirst(string findText)
        {
            return FindFirst(findText, false, false);

        } //FindFirst

        // finds the first occurrence of the given text string
        public bool FindFirst(string findText, bool matchWhole, bool matchCase)
        {
            // reset the text search range prior to making any calls
            FindReplaceReset();

            // calls the Find Next once search has been initialized
            return FindNext(findText, matchWhole, matchCase);

        } //FindFirst


        // finds the next occurrence of a given text string
        // assumes a previous search was made
        // uses false for the search options
        public bool FindNext(string findText)
        {
            return FindNext(findText, false, false);

        } //FindNext

        // finds the next occurrence of a given text string
        // assumes a previous search was made
        public bool FindNext(string findText, bool matchWhole, bool matchCase)
        {
            return (FindText(findText, matchWhole, matchCase) != null ? true : false);

        } //FindNext


        // replace the first occurrence of the given string with the other
        // uses false for the search options
        public bool FindReplaceOne(string findText, string replaceText)
        {
            return FindReplaceOne(findText, replaceText);

        } //FindReplaceOne

        // replace the first occurrence of the given string with the other
        public bool FindReplaceOne(string findText, string replaceText, bool matchWhole, bool matchCase)
        {
            // find the given text within the find range
            HtmlTextRange replaceRange = FindText(findText, matchWhole, matchCase);
            if (replaceRange != null)
            {
                // if text found perform a replace
                replaceRange.text = replaceText;
                replaceRange.select();
                // replace made to return success
                return true;
            }
            else
            {
                // no replace was made so return false
                return false;
            }

        } //FindReplaceOne


        // replaces all the occurrence of the given string with the other
        // uses false for the search options
        public int FindReplaceAll(string findText, string replaceText)
        {
            return FindReplaceAll(findText, replaceText, false, false);

        } //FindReplaceAll

        // replaces all the occurrences of the given string with the other
        public int FindReplaceAll(string findText, string replaceText, bool matchWhole, bool matchCase)
        {
            int found = 0;
            HtmlTextRange replaceRange = null;

            do
            {
                // find the given text within the find range
                replaceRange = FindText(findText, matchWhole, matchCase);
                // if found perform a replace
                if (replaceRange != null)
                {
                    replaceRange.text = replaceText;
                    found++;
                }
            } while (replaceRange != null);

            // return count of items repalced
            return found;

        } //FindReplaceAll


        // function to perform the actual find of the given text
        private HtmlTextRange FindText(string findText, bool matchWhole, bool matchCase)
        {
            // define the search options
            int searchOptions = 0;
            if (matchWhole) searchOptions = searchOptions + 2;
            if (matchCase) searchOptions = searchOptions + 4;

            // perform the search operation
            if (_findRange.findText(findText, _findRange.text.Length, searchOptions))
            {
                // select the found text within the document
                _findRange.select();
                // limit the new find range to be from the newly found text
                HtmlTextRange foundRange = (HtmlTextRange)document.selection.createRange();
                _findRange = (HtmlTextRange)body.createTextRange();
                _findRange.setEndPoint("StartToEnd", foundRange);
                // text found so return this selection
                return foundRange;
            }
            else
            {
                // reset the find ranges
                FindReplaceReset();
                // no text found so return null range
                return null;
            }

        } //FindText 

        #endregion

        #region Table Processing Operations

        // public function to create a table class
        // insert method then works on this table
        public void TableInsert(HtmlTableProperty tableProperties)
        {
            // call the private insert table method with a null table entry
            ProcessTable(null, tableProperties);

        } //TableInsert

        // public function to modify a tables properties
        // ensure a table is currently selected or insertion point is within a table
        public bool TableModify(HtmlTableProperty tableProperties)
        {
            // define the Html Table element
            HtmlTable table = GetTableElement();

            // if a table has been selected then process
            if (table != null)
            {
                ProcessTable(table, tableProperties);
                return true;
            }
            else
            {
                return false;
            }

        } //TableModify

        // present to the user the table properties dialog
        // using all the default properties for the table based on an insert operation
        public void TableInsertPrompt()
        {
            // if user has selected a table create a reference
            HtmlTable table = GetFirstControl() as HtmlTable;
            ProcessTablePrompt(table);

        } //TableInsertPrompt


        // present to the user the table properties dialog
        // ensure a table is currently selected or insertion point is within a table
        public bool TableModifyPrompt()
        {
            // define the Html Table element
            HtmlTable table = GetTableElement();

            // if a table has been selected then process
            if (table != null)
            {
                ProcessTablePrompt(table);
                return true;
            }
            else
            {
                return false;
            }

        } //TableModifyPrompt


        // will insert a new row into the table
        // based on the current user row and insertion after
        public void TableInsertRow()
        {
            // see if a table selected or insertion point inside a table
            HtmlTable table = null;
            HtmlTableRow row = null;
            GetTableElement(out table, out row);

            // process according to table being defined
            if (table != null && row != null)
            {
                try
                {
                    // find the existing row the user is on and perform the insertion
                    int index = row.rowIndex + 1;
                    HtmlTableRow insertedRow = (HtmlTableRow)table.insertRow(index);
                    // add the new columns to the end of each row
                    int numberCols = row.cells.length;
                    for (int idxCol = 0; idxCol < numberCols; idxCol++)
                    {
                        insertedRow.insertCell(-1);
                    }
                }
                catch (Exception ex)
                {
                    throw new HtmlEditorException("Unable to insert a new Row", "TableinsertRow", ex);
                }
            }
            else
            {
                throw new HtmlEditorException("Row not currently selected within the table", "TableInsertRow");
            }

        } //TableInsertRow


        // will delete the currently selected row
        // based on the current user row location
        public void TableDeleteRow()
        {
            // see if a table selected or insertion point inside a table
            HtmlTable table = null;
            HtmlTableRow row = null;
            GetTableElement(out table, out row);

            // process according to table being defined
            if (table != null && row != null)
            {
                try
                {
                    // find the existing row the user is on and perform the deletion
                    int index = row.rowIndex;
                    table.deleteRow(index);
                }
                catch (Exception ex)
                {
                    throw new HtmlEditorException("Unable to delete the selected Row", "TableDeleteRow", ex);
                }
            }
            else
            {
                throw new HtmlEditorException("Row not currently selected within the table", "TableDeleteRow");
            }

        } //TableDeleteRow


        // present to the user the table properties dialog
        // using all the default properties for the table based on an insert operation
        private void ProcessTablePrompt(HtmlTable table)
        {
            using (TablePropertyForm dialog = new TablePropertyForm())
            {
                // define the base set of table properties
                HtmlTableProperty tableProperties = GetTableProperties(table);

                // set the dialog properties
                dialog.TableProperties = tableProperties;
                DefineDialogProperties(dialog);
                // based on the user interaction perform the neccessary action
                if (dialog.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    tableProperties = dialog.TableProperties;
                    if (table == null) TableInsert(tableProperties);
                    else ProcessTable(table, tableProperties);
                }
            }

        } // ProcessTablePrompt


        // function to insert a basic table
        // will honour the existing table if passed in
        private void ProcessTable(HtmlTable table, HtmlTableProperty tableProperties)
        {
            try
            {
                // obtain a reference to the body node and indicate table present
                HtmlDomNode bodyNode = (HtmlDomNode)document.body;
                bool tableCreated = false;

                // ensure a table node has been defined to work with
                if (table == null)
                {
                    // create the table and indicate it was created
                    table = (HtmlTable)document.createElement(TABLE_TAG);
                    tableCreated = true;
                }

                // define the table border, width, cell padding and spacing
                table.border = tableProperties.BorderSize;
                if (tableProperties.TableWidth > 0) table.width = (tableProperties.TableWidthMeasurement == MeasurementOption.Pixel) ? string.Format("{0}", tableProperties.TableWidth) : string.Format("{0}%", tableProperties.TableWidth);
                else table.width = string.Empty;
                if (tableProperties.TableAlignment != HorizontalAlignOption.Default) table.align = tableProperties.TableAlignment.ToString().ToLower();
                else table.align = string.Empty;
                table.cellPadding = tableProperties.CellPadding.ToString();
                table.cellSpacing = tableProperties.CellSpacing.ToString();

                // define the given table caption and alignment
                string caption = tableProperties.CaptionText;
                HtmlTableCaption tableCaption = table.caption;
                if (caption != null && caption != string.Empty)
                {
                    // ensure table caption correctly defined
                    if (tableCaption == null) tableCaption = table.createCaption();
                    ((HtmlElement)tableCaption).innerText = caption;
                    if (tableProperties.CaptionAlignment != HorizontalAlignOption.Default) tableCaption.align = tableProperties.CaptionAlignment.ToString().ToLower();
                    if (tableProperties.CaptionLocation != VerticalAlignOption.Default) tableCaption.vAlign = tableProperties.CaptionLocation.ToString().ToLower();
                }
                else
                {
                    // if no caption specified remove the existing one
                    if (tableCaption != null)
                    {
                        // prior to deleting the caption the contents must be cleared
                        ((HtmlElement)tableCaption).innerText = null;
                        table.deleteCaption();
                    }
                }

                // determine the number of rows one has to insert
                int numberRows, numberCols;
                if (tableCreated)
                {
                    numberRows = Math.Max((int)tableProperties.TableRows, 1);
                }
                else
                {
                    numberRows = Math.Max((int)tableProperties.TableRows, 1) - (int)table.rows.length;
                }

                // layout the table structure in terms of rows and columns
                table.cols = (int)tableProperties.TableColumns;
                if (tableCreated)
                {
                    // this section is an optimization based on creating a new table
                    // the section below works but not as efficiently
                    numberCols = Math.Max((int)tableProperties.TableColumns, 1);
                    // insert the appropriate number of rows
                    HtmlTableRow tableRow;
                    for (int idxRow = 0; idxRow < numberRows; idxRow++)
                    {
                        tableRow = (HtmlTableRow)table.insertRow(-1);
                        // add the new columns to the end of each row
                        for (int idxCol = 0; idxCol < numberCols; idxCol++)
                        {
                            tableRow.insertCell(-1);
                        }
                    }
                }
                else
                {
                    // if the number of rows is increasing insert the decrepency
                    if (numberRows > 0)
                    {
                        // insert the appropriate number of rows
                        for (int idxRow = 0; idxRow < numberRows; idxRow++)
                        {
                            table.insertRow(-1);
                        }
                    }
                    else
                    {
                        // remove the extra rows from the table
                        for (int idxRow = numberRows; idxRow < 0; idxRow++)
                        {
                            table.deleteRow(table.rows.length - 1);
                        }
                    }
                    // have the rows constructed
                    // now ensure the columns are correctly defined for each row
                    HtmlElementCollection rows = table.rows;
                    foreach (HtmlTableRow tableRow in rows)
                    {
                        numberCols = Math.Max((int)tableProperties.TableColumns, 1) - (int)tableRow.cells.length;
                        if (numberCols > 0)
                        {
                            // add the new column to the end of each row
                            for (int idxCol = 0; idxCol < numberCols; idxCol++)
                            {
                                tableRow.insertCell(-1);
                            }
                        }
                        else
                        {
                            // reduce the number of cells in the given row
                            // remove the extra rows from the table
                            for (int idxCol = numberCols; idxCol < 0; idxCol++)
                            {
                                tableRow.deleteCell(tableRow.cells.length - 1);
                            }
                        }
                    }
                }

                // if the table was created then it requires insertion into the DOM
                // otherwise property changes are sufficient
                if (tableCreated)
                {
                    // table processing all complete so insert into the DOM
                    HtmlDomNode tableNode = (HtmlDomNode)table;
                    HtmlElement tableElement = (HtmlElement)table;
                    HtmlSelection selection = document.selection;
                    HtmlTextRange textRange = GetTextRange();
                    // final insert dependant on what user has selected
                    if (textRange != null)
                    {
                        // text range selected so overwrite with a table
                        try
                        {
                            string selectedText = textRange.text;
                            if (selectedText != null)
                            {
                                // place selected text into first cell
                                HtmlTableRow tableRow = (HtmlTableRow)table.rows.item(0, null);
                                ((HtmlElement)tableRow.cells.item(0, null)).innerText = selectedText;
                            }
                            textRange.pasteHTML(tableElement.outerHTML);
                        }
                        catch (Exception ex)
                        {
                            throw new HtmlEditorException("Invalid Text selection for the Insertion of a Table.", "ProcessTable", ex);
                        }
                    }
                    else
                    {
                        HtmlControlRange controlRange = GetAllControls();
                        if (controlRange != null)
                        {
                            // overwrite any controls the user has selected
                            try
                            {
                                // clear the selection and insert the table
                                // only valid if multiple selection is enabled
                                for (int idx = 1; idx < controlRange.length; idx++)
                                {
                                    controlRange.remove(idx);
                                }
                                controlRange.item(0).outerHTML = tableElement.outerHTML;
                                // this should work with initial count set to zero
                                // controlRange.add((HtmlControlElement)table);
                            }
                            catch (Exception ex)
                            {
                                throw new HtmlEditorException("Cannot Delete all previously Controls selected.", "ProcessTable", ex);
                            }
                        }
                        else
                        {
                            // insert the table at the end of the HTML
                            bodyNode.appendChild(tableNode);
                        }
                    }
                }
                else
                {
                    // table has been correctly defined as being the first selected item
                    // need to remove other selected items
                    HtmlControlRange controlRange = GetAllControls();
                    if (controlRange != null)
                    {
                        // clear the controls selected other than than the first table
                        // only valid if multiple selection is enabled
                        for (int idx = 1; idx < controlRange.length; idx++)
                        {
                            controlRange.remove(idx);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // throw an exception indicating table structure change error
                throw new HtmlEditorException("Unable to modify Html Table properties.", "ProcessTable", ex);
            }

        } //ProcessTable


        // determine if the current selection is a table
        // return the table element
        private void GetTableElement(out HtmlTable table, out HtmlTableRow row)
        {
            table = null;
            row = null;
            HtmlTextRange range = GetTextRange();

            try
            {
                // first see if the table element is selected
                table = GetFirstControl() as HtmlTable;
                // if table not selected then parse up the selection tree
                if (table == null && range != null)
                {
                    HtmlElement element = (HtmlElement)range.parentElement();
                    // parse up the tree until the table element is found
                    while (element != null && table == null)
                    {
                        element = (HtmlElement)element.parentElement;
                        // extract the Table properties
                        if (element is HtmlTable)
                        {
                            table = (HtmlTable)element;
                        }
                        // extract the Row  properties
                        if (element is HtmlTableRow)
                        {
                            row = (HtmlTableRow)element;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // have unknown error so set return to null
                table = null;
                row = null;
            }

        } //GetTableElement

        private HtmlTable GetTableElement()
        {
            // define the table and row elements and obtain there values
            HtmlTable table = null;
            HtmlTableRow row = null;
            GetTableElement(out table, out row);

            // return the defined table element
            return table;

        }


        // given an HtmlTable determine the table properties
        private HtmlTableProperty GetTableProperties(HtmlTable table)
        {
            // define a set of base table properties
            HtmlTableProperty tableProperties = new HtmlTableProperty(true);

            // if user has selected a table extract those properties
            if (table != null)
            {
                try
                {
                    // have a table so extract the properties
                    HtmlTableCaption caption = table.caption;
                    // if have a caption persist the values
                    if (caption != null)
                    {
                        tableProperties.CaptionText = ((HtmlElement)table.caption).innerText;
                        if (caption.align != null) tableProperties.CaptionAlignment = (HorizontalAlignOption)TryParseEnum(typeof(HorizontalAlignOption), caption.align, HorizontalAlignOption.Default);
                        if (caption.vAlign != null) tableProperties.CaptionLocation = (VerticalAlignOption)TryParseEnum(typeof(VerticalAlignOption), caption.vAlign, VerticalAlignOption.Default);
                    }
                    // look at the table properties
                    if (table.border != null) tableProperties.BorderSize = TryParseByte(table.border.ToString(), tableProperties.BorderSize);
                    if (table.align != null) tableProperties.TableAlignment = (HorizontalAlignOption)TryParseEnum(typeof(HorizontalAlignOption), table.align, HorizontalAlignOption.Default);
                    // define the table rows and columns
                    int rows = Math.Min(table.rows.length, Byte.MaxValue);
                    int cols = Math.Min(table.cols, Byte.MaxValue);
                    if (cols == 0 && rows > 0)
                    {
                        // cols value not set to get the maxiumn number of cells in the rows
                        foreach (HtmlTableRow tableRow in table.rows)
                        {
                            cols = Math.Max(cols, (int)tableRow.cells.length);
                        }
                    }
                    tableProperties.TableRows = (byte)Math.Min(rows, byte.MaxValue);
                    tableProperties.TableColumns = (byte)Math.Min(cols, byte.MaxValue);
                    // define the remaining table properties
                    if (table.cellPadding != null) tableProperties.CellPadding = TryParseByte(table.cellPadding.ToString(), tableProperties.CellPadding);
                    if (table.cellSpacing != null) tableProperties.CellSpacing = TryParseByte(table.cellSpacing.ToString(), tableProperties.CellSpacing);
                    if (table.width != null)
                    {
                        string tableWidth = table.width.ToString();
                        if (tableWidth.TrimEnd(null).EndsWith("%"))
                        {
                            tableProperties.TableWidth = TryParseUshort(tableWidth.Remove(tableWidth.LastIndexOf("%"), 1), tableProperties.TableWidth);
                            tableProperties.TableWidthMeasurement = MeasurementOption.Percent;
                        }
                        else
                        {
                            tableProperties.TableWidth = TryParseUshort(tableWidth, tableProperties.TableWidth);
                            tableProperties.TableWidthMeasurement = MeasurementOption.Pixel;
                        }
                    }
                    else
                    {
                        tableProperties.TableWidth = 0;
                        tableProperties.TableWidthMeasurement = MeasurementOption.Pixel;
                    }
                }
                catch (Exception ex)
                {
                    // throw an exception indicating table structure change be determined
                    throw new HtmlEditorException("Unable to determine Html Table properties.", "GetTableProperties", ex);
                }
            }

            // return the table properties
            return tableProperties;

        } //GetTableProperties


        // based on the user selection return a table definition
        // if table selected (or insertion point within table) return these values
        public void GetTableDefinition(out HtmlTableProperty table, out bool tableFound)
        {
            // see if a table selected or insertion point inside a table
            HtmlTable htmlTable = GetTableElement();

            // process according to table being defined
            if (htmlTable == null)
            {
                table = new HtmlTableProperty(true);
                tableFound = false;
            }
            else
            {
                table = GetTableProperties(htmlTable);
                tableFound = true;
            }

        } //GetTableDefinition


        // Determine if the insertion point or selection is a table
        private bool IsParentTable()
        {
            // see if a table selected or insertion point inside a table
            HtmlTable htmlTable = GetTableElement();

            // process according to table being defined
            if (htmlTable == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        } //IsParentTable


        #endregion

        #region MsHtml Command Processing

        // executes the execCommand on the selected range
        private void ExecuteCommandRange(string command, object data)
        {
            // obtain the selected range object and execute command
            HtmlTextRange range = GetTextRange();
            ExecuteCommandRange(range, command, data);

        } // ExecuteCommandRange

        // executes the execCommand on the selected range (given the range)
        private void ExecuteCommandRange(HtmlTextRange range, string command, object data)
        {
            try
            {
                if (range != null)
                {
                   if (range.queryCommandSupported(command))
                    {
                        if (range.queryCommandEnabled(command))
                        {
                            // mark the selection with the appropriate tag
                            range.execCommand(command, false, data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Unknown error so inform user
                throw new HtmlEditorException("Unknown MSHTML Error.", command, ex);
            }

        } // ExecuteCommandRange


        // executes the execCommand on the document
        private void ExecuteCommandDocument(string command)
        {
            ExecuteCommandDocument(command, false);

        } // ExecuteCommandDocument

        // executes the execCommand on the document with a system prompt
        private void ExecuteCommandDocumentPrompt(string command)
        {
            ExecuteCommandDocument(command, true);

        } // ExecuteCommandDocumentPrompt

        // executes the execCommand on the document with a system prompt
        private void ExecuteCommandDocument(string command, bool prompt)
        {
            try
            {
                // ensure command is a valid command and then enabled for the selection
                if (document.queryCommandSupported(command))
                {
                    // if (document.queryCommandEnabled(command)) {}
                    // Test fails with a COM exception if command is Print

                    // execute the given command
                    document.execCommand(command, prompt, null);
                }
            }
            catch (Exception ex)
            {
                // Unknown error so inform user
                throw new HtmlEditorException("Unknown MSHTML Error.", command, ex);
            }

        } // ExecuteCommandDocumentPrompt


        // determines the value of the command
        private object QueryCommandRange(string command)
        {
            // obtain the selected range object and execute command
            HtmlTextRange range = GetTextRange();
            return QueryCommandRange(range, command);

        } // QueryCommandRange

        // determines the value of the command
        private object QueryCommandRange(HtmlTextRange range, string command)
        {
            object retValue = null;
            if (range != null)
            {
                try
                {
                    // ensure command is a valid command and then enabled for the selection
                    if (range.queryCommandSupported(command))
                    {
                        if (range.queryCommandEnabled(command))
                        {
                            retValue = range.queryCommandValue(command);
                        }
                    }
                }
                catch (Exception)
                {
                    // have unknown error so set return to null
                    retValue = null;
                }
            }

            // return the obtained value
            return retValue;

        } //QueryCommandRange


        // get the selected range object
        private HtmlTextRange GetTextRange()
        {
            // define the selected range object
            HtmlSelection selection;
            HtmlTextRange range = null;

            try
            {
                // calculate the text range based on user selection
                selection = document.selection;
                if (IsStatedTag(selection.type, SELECT_TYPE_TEXT) || IsStatedTag(selection.type, SELECT_TYPE_NONE))
                {
                    range = (HtmlTextRange)selection.createRange();
                }
            }
            catch (Exception)
            {
                // have unknown error so set return to null
                range = null;
            }

            return range;

        } // GetTextRange


        // get the selected range object
        private HtmlElement GetFirstControl()
        {
            // define the selected range object
            HtmlSelection selection;
            HtmlControlRange range;
            HtmlElement control = null;

            try
            {
                // calculate the first control based on the user selection
                selection = document.selection;
                if (IsStatedTag(selection.type, SELECT_TYPE_CONTROL))
                {
                    range = (HtmlControlRange)selection.createRange();
                    if (range.length > 0) control = range.item(0);
                }
            }
            catch (Exception)
            {
                // have unknown error so set return to null
                control = null;
            }

            return control;

        } // GetFirstControl

        // obtains a control range to be worked with
        private HtmlControlRange GetAllControls()
        {
            // define the selected range object
            HtmlSelection selection;
            HtmlControlRange range = null;

            try
            {
                // calculate the first control based on the user selection
                selection = document.selection;
                if (IsStatedTag(selection.type, SELECT_TYPE_CONTROL))
                {
                    range = (HtmlControlRange)selection.createRange();
                }
            }
            catch (Exception)
            {
                // have unknow error so set return to null
                range = null;
            }

            return range;

        } //GetAllControls

        #endregion

        #region Utility Functions

        // performs a parse of the string into an enum
        private object TryParseEnum(Type enumType, string stringValue, object defaultValue)
        {
            // try the enum parse and return the default 
            object result = defaultValue;
            try
            {
                // try the enum parse operation
                result = Enum.Parse(enumType, stringValue, true);
            }
            catch (Exception)
            {
                // default value will be returned
                result = defaultValue;
            }

            // return the enum value
            return result;

        } //TryParseEnum


        // perform of a string into a byte number
        private byte TryParseByte(string stringValue, byte defaultValue)
        {
            byte result = defaultValue;
            double doubleValue;
            // try the conversion to a double number
            if (Double.TryParse(stringValue, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out doubleValue))
            {
                try
                {
                    // try a cast to a byte
                    result = (byte)doubleValue;
                }
                catch (Exception)
                {
                    // default value will be returned
                    result = defaultValue;
                }
            }

            // return the byte value
            return result;

        } //TryParseByte


        // perform of a string into a byte number
        private ushort TryParseUshort(string stringValue, ushort defaultValue)
        {
            ushort result = defaultValue;
            double doubleValue;
            // try the conversion to a double number
            if (Double.TryParse(stringValue, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out doubleValue))
            {
                try
                {
                    // try a cast to a byte
                    result = (ushort)doubleValue;
                }
                catch (Exception)
                {
                    // default value will be returned
                    result = defaultValue;
                }
            }

            // return the byte value
            return result;

        } //TryParseUshort


        // ensure dialog resembles the user form characteristics
        private void DefineDialogProperties(Form dialog)
        {
            // set ambient control properties
            dialog.Font = this.ParentForm.Font;
            dialog.ForeColor = this.ParentForm.ForeColor;
            dialog.BackColor = this.ParentForm.BackColor;
            dialog.Cursor = this.ParentForm.Cursor;
            dialog.RightToLeft = this.ParentForm.RightToLeft;

            // define location and control style as system
            dialog.StartPosition = FormStartPosition.CenterParent;
            SetFlatStyleSystem(dialog);

        } //DefineDialogProperties


        // set the flat style of the dialog based on the user setting
        private void SetFlatStyleSystem(Control parent)
        {
            // define the flat style to use
            FlatStyle flatstyle = _enableVisualStyles ? FlatStyle.System : FlatStyle.Standard;

            // iterate through all controls setting the flat style
            foreach (Control control in parent.Controls)
            {
                // Only these controls have a FlatStyle property
                ButtonBase button = control as ButtonBase;
                GroupBox group = control as GroupBox;
                Label label = control as Label;
                TextBox textBox = control as TextBox;
                if (button != null) button.FlatStyle = flatstyle;
                else if (group != null) group.FlatStyle = flatstyle;
                else if (label != null) label.FlatStyle = flatstyle;

                // Set contained controls FlatStyle, too
                SetFlatStyleSystem(control);
            }

        } //SetFlatStyleSystem

        // determine if a string url is valid
        private bool IsValidHref(string href)
        {
            return Regex.IsMatch(href, HREF_TEST_EXPRESSION, RegexOptions.IgnoreCase);

        } //IsValidHref


        // determine if the tage name is of the correct type
        private bool IsStatedTag(string tagText, string tagType)
        {
            return (string.Compare(tagText, tagType, true) == 0) ? true : false;

        } //IsStatedTag


        // removes references to about:blank from the anchors
        private void RebaseAnchorUrl()
        {
            if (rebaseUrlsNeeded)
            {
                // review the anchors and remove any references to about:blank
                HtmlElementCollection anchors = body.getElementsByTagName(ANCHOR_TAG);
                foreach (HtmlElement element in anchors)
                {
                    try
                    {
                        HtmlAnchorElement anchor = (HtmlAnchorElement)element;
                        string href = anchor.href;
                        // see if href need updating
                        if (href != null && Regex.IsMatch(href, BLANK_HTML_PAGE, RegexOptions.IgnoreCase))
                        {
                            anchor.href = href.Replace(BLANK_HTML_PAGE, string.Empty);
                        }
                    }
                    catch (Exception)
                    {
                        // ignore any errors
                    }
                }
            }

        } //RebaseAnchorUrl

        #endregion

        
        #region Internal Events and Error Processing

        // method to process the command and handle error exception
        private void ProcessCommand(string command)
        {
            try
            {
                // Evaluate the Button property to determine which button was clicked.
                switch (command)
                {
                    case INTERNAL_COMMAND_TEXTCUT:
                        // Browser CUT command
                        TextCut();
                        break;
                    case INTERNAL_COMMAND_TEXTCOPY:
                        // Browser COPY command
                        TextCopy();
                        break;
                    case INTERNAL_COMMAND_TEXTPASTE:
                        // Browser PASTE command
                        TextPaste();
                        break;
                    case INTERNAL_COMMAND_TEXTDELETE:
                        // Browser DELETE command
                        TextDelete();
                        break;
                    case INTERNAL_COMMAND_CLEARSELECT:
                        // Clears user selection
                        TextClearSelect();
                        break;
                    case INTERNAL_COMMAND_SELECTALL:
                        // Selects all document content
                        TextSelectAll();
                        break;
                    case INTERNAL_COMMAND_EDITUNDO:
                        // Undo the previous editing
                        EditUndo();
                        break;
                    case INTERNAL_COMMAND_EDITREDO:
                        // Redo the previous undo
                        EditRedo();
                        break;
                    case INTERNAL_COMMAND_FORMATBOLD:
                        // Selection BOLD command
                        FormatBold();
                        break;
                    case INTERNAL_COMMAND_FORMATUNDERLINE:
                        // Selection UNDERLINE command
                        FormatUnderline();
                        break;
                    case INTERNAL_COMMAND_FORMATITALIC:
                        // Selection ITALIC command
                        FormatItalic();
                        break;
                    case INTERNAL_COMMAND_FORMATSUPERSCRIPT:
                        // Selection SUPERSCRIPT command
                        FormatSuperscript();
                        break;
                    case INTERNAL_COMMAND_FORMATSUBSCRIPT:
                        // Selection SUBSCRIPT command
                        FormatSubscript();
                        break;
                    case INTERNAL_COMMAND_FORMATSTRIKEOUT:
                        // Selection STRIKEOUT command
                        FormatStrikeout();
                        break;
                    case INTERNAL_COMMAND_FONTDIALOG:
                        // FONT style creation
                        FormatFontAttributesPrompt();
                        break;
                    case INTERNAL_COMMAND_FONTNORMAL:
                        // FONT style remove
                        FormatRemove();
                        break;
                    case INTERNAL_COMMAND_COLORDIALOG:
                        // COLOR style creation
                        FormatFontColorPrompt();
                        break;
                    case INTERNAL_COMMAND_FONTINCREASE:
                        // FONTSIZE increase
                        FormatFontIncrease();
                        break;
                    case INTERNAL_COMMAND_FONTDECREASE:
                        // FONTSIZE decrease
                        FormatFontDecrease();
                        break;
                    case INTERNAL_COMMAND_JUSTIFY_FULL:
                        JustifyFull();
                        break;
                    case INTERNAL_COMMAND_FOOTNOTE:
                        Footnote();
                        break;
                    case INTERNAL_COMMAND_JUSTIFYLEFT:
                        // Justify Left
                        JustifyLeft();
                        break;
                    case INTERNAL_COMMAND_JUSTIFYCENTER:
                        // Justify Center
                        JustifyCenter();
                        break;
                    case INTERNAL_COMMAND_JUSTIFYRIGHT:
                        // Justify Right
                        JustifyRight();
                        break;
                    case INTERNAL_COMMAND_FONTINDENT:
                        // Tab Right
                        FormatTabRight();
                        break;
                    case INTERNAL_COMMAND_FONTOUTDENT:
                        // Tab Left
                        FormatTabLeft();
                        break;
                    case INTERNAL_COMMAND_LISTORDERED:
                        // Ordered List
                        FormatList(HtmlListType.Ordered);
                        break;
                    case INTERNAL_COMMAND_LISTUNORDERED:
                        // Unordered List
                        FormatList(HtmlListType.Unordered);
                        break;
                    case INTERNAL_COMMAND_INSERTLINE:
                        // Horizontal Line
                        InsertLine();
                        break;
                    case INTERNAL_COMMAND_INSERTTABLE:
                        // Display a dialog to enable the user to insert a table
                        TableInsertPrompt();
                        break;
                    case INTERNAL_COMMAND_TABLEPROPERTIES:
                        // Display a dialog to enable the user to modify a table
                        TableModifyPrompt();
                        break;
                    case INTERNAL_COMMAND_TABLEINSERTROW:
                        // Display a dialog to enable the user to modify a table
                        TableInsertRow();
                        break;
                    case INTERNAL_COMMAND_TABLEDELETEROW:
                        // Display a dialog to enable the user to modify a table
                        TableDeleteRow();
                        break;
                    case INTERNAL_COMMAND_INSERTIMAGE:
                        // Display a dialog to enable the user to insert a image
                        InsertImagePrompt();
                        break;
                    case INTERNAL_COMMAND_INSERTLINK:
                        // Display a dialog to enable user to insert the href
                        InsertLinkPrompt();
                        break;
                    case INTERNAL_COMMAND_INSERTTEXT:
                        // Display a dialog to enable user to insert the text
                        InsertTextPrompt();
                        break;
                    case INTERNAL_COMMAND_INSERTHTML:
                        // Display a dialog to enable user to insert the html
                        InsertHtmlPrompt();
                        break;
                    case INTERNAL_COMMAND_FINDREPLACE:
                        // Display a dialog to enable user to perform find and replace operations
                        FindReplacePrompt();
                        break;
                    case INTERNAL_COMMAND_DOCUMENTPRINT:
                        // Print the current document
                        DocumentPrint();
                        break;
                    case INTERNAL_COMMAND_OPENFILE:
                        // Open a selected file
                        OpenFilePrompt();
                        break;
                    case INTERNAL_COMMAND_SAVEFILE:
                        // Saves the current document
                        SaveFilePrompt();
                        break;
                    case INTERNAL_TOGGLE_OVERWRITE:
                        // toggles the document overwrite method
                        ToggleOverWrite();
                        break;
                    case INTERNAL_TOGGLE_TOOLBAR:
                        // toggles the toolbar visibility
                        this.ToolbarVisible = !_toolbarVisible;
                        break;
                    case INTERNAL_TOGGLE_SCROLLBAR:
                        // toggles the scrollbar visibility
                        this.ScrollBars = (_scrollBars == DisplayScrollBarOption.No ? DisplayScrollBarOption.Auto : DisplayScrollBarOption.No);
                        break;
                    case INTERNAL_TOGGLE_WORDWRAP:
                        // toggles the document word wrapping
                        this.AutoWordWrap = !_autoWordWrap;
                        break;
                    default:
                        throw new HtmlEditorException("Unknown Operation Being Performed.", command);
                }
            }
            catch (HtmlEditorException ex)
            {
                // process the html exception
                OnHtmlException(new HtmlExceptionEventArgs(ex.Operation, ex));
            }
            catch (Exception ex)
            {
                // process the exception
                OnHtmlException(new HtmlExceptionEventArgs(null, ex));
            }

            // ensure web browser has the focus after command execution
            Focus();

        } //ProcessCommand


        // function to perform the format block insertion
        private void ProcessFormatBlock(string command)
        {
            try
            {
                // execute the insertion command
                InsertFormatBlock(command);
            }
            catch (HtmlEditorException ex)
            {
                // process the html exception
                OnHtmlException(new HtmlExceptionEventArgs(ex.Operation, ex));
            }
            catch (Exception ex)
            {
                // process the standard exception
                OnHtmlException(new HtmlExceptionEventArgs(null, ex));
            }

            // ensure web browser has the focus after command execution
            Focus();
        }


        // method to raise an event if a delegeate is assigned
        private void OnHtmlException(HtmlExceptionEventArgs args)
        {
            if (HtmlException == null)
            {
                // obtain the message and operation
                // concatenate the message with any inner message
                string operation = args.Operation;
                Exception ex = args.ExceptionObject;
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message != null)
                    {
                        message = string.Format("{0}\n{1}", message, ex.InnerException.Message);
                    }
                }
                // define the title for the internal message box
                string title;
                if (operation == null || operation == string.Empty)
                {
                    title = "Unknown Error";
                }
                else
                {
                    title = operation + " Error";
                }
                // display the error message box
                MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                HtmlException(this, args);
            }
        }


        // function to perform the menu bar context menu
        // used to display formatting types
        private void ProcessFormattingSelection(object sender, System.EventArgs e)
        {
            // obtain the menu item that was clicked
            MenuItem menuItem = (MenuItem)sender;
            string command = menuItem.Text;

            // process the format block
            ProcessFormatBlock(command);

        } //ProcessFormattingSelection


        // toolbar processing
        // calls the processcommand with the selected command
        private void editorToolbar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            string command = e.Button.Tag.ToString();
            ProcessCommand(command);

        } //ToolbarButtonClick


        // series of function based on the context menu
        // each should call the corresponding command processor

        // Text Commands
        private void menuTextUndo_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_EDITUNDO);
        }
        private void menuTextRedo_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_EDITREDO);
        }
        private void menuTextCut_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_TEXTCUT);
        }
        private void menuTextCopy_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_TEXTCOPY);
        }
        private void menuTextPaste_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_TEXTPASTE);
        }
        private void menuTextDelete_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_TEXTDELETE);
        }
        private void menuTextFindReplace_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FINDREPLACE);
        }
        private void menuTextSelectNone_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_CLEARSELECT);
        }
        private void menuTextSelectAll_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_SELECTALL);
        }
        private void menuTextFontIncrease_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FONTINCREASE);
        }
        private void menuTextFontDecrease_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FONTDECREASE);
        }
        private void menuTextFontNormal_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FONTNORMAL);
        }
        private void menuTextFontBold_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FORMATBOLD);
        }
        private void menuTextFontItalic_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FORMATITALIC);
        }
        private void menuTextFontUnderline_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FORMATUNDERLINE);
        }
        private void menuTextFontSuperscript_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FORMATSUPERSCRIPT);
        }
        private void menuTextFontSubscript_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FORMATSUBSCRIPT);
        }
        private void menuTextFontStrikeout_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FORMATSTRIKEOUT);
        }
        private void menuTextFontIndent_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FONTINDENT);
        }
        private void menuTextFontOutdent_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FONTOUTDENT);
        }
        private void menuTextFontDialog_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_FONTDIALOG);
        }
        private void menuTextFontColor_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_COLORDIALOG);
        }
        private void menuTextFontListOrdered_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_LISTORDERED);
        }
        private void menuTextFontListUnordered_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_LISTUNORDERED);
        }

        // Insert Commands
        private void menuInsertLine_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_INSERTLINE);
        }
        private void menuInsertLink_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_INSERTLINK);
        }
        private void menuInsertImage_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_INSERTIMAGE);
        }
        private void menuInsertText_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_INSERTTEXT);
        }
        private void menuInsertHtml_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_INSERTHTML);
        }

        // Table Commands
        private void menuInsertTable_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_INSERTTABLE);
        }
        private void menuTableProperties_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_TABLEPROPERTIES);
        }
        private void menuTableInsertRow_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_TABLEINSERTROW);
        }
        private void menuTableDeleteRow_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_TABLEDELETEROW);
        }

        // Justify Commands
        private void menuJustifyLeft_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_JUSTIFYLEFT);
        }
        private void menuJustifyCenter_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_JUSTIFYCENTER);
        }
        private void menuJustifyRight_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_JUSTIFYRIGHT);
        }

        // Document Commands
        private void menuDocumentOpen_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_OPENFILE);
        }
        private void menuDocumentSave_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_SAVEFILE);
        }
        private void menuDocumentPrint_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_COMMAND_DOCUMENTPRINT);
        }
        private void menuDocumentOverwrite_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_TOGGLE_OVERWRITE);
        }
        private void menuDocumentToolbar_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_TOGGLE_TOOLBAR);
        }
        private void menuDocumentScrollbar_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_TOGGLE_SCROLLBAR);
        }
        private void menuDocumentWordwrap_Click(object sender, System.EventArgs e)
        {
            ProcessCommand(INTERNAL_TOGGLE_WORDWRAP);
        }

        #endregion

    }
}