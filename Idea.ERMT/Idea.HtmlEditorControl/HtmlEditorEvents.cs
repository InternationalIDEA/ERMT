using System;

namespace Microsoft.ConsultingServices.HtmlEditor
{
	# region Application delegate definitions

	// Define delegate for raising an editor exception
	public delegate void HtmlExceptionEventHandler(object sender, HtmlExceptionEventArgs e);

	// Define delegate for handling navigation events
	public delegate void HtmlNavigationEventHandler(object sender, HtmlNavigationEventArgs e);

	// delegate declarations required for the find and replace dialog
	internal delegate void FindReplaceResetDelegate();
	internal delegate bool FindFirstDelegate(string findText, bool matchWhole, bool matchCase);
	internal delegate bool FindNextDelegate(string findText, bool matchWhole, bool matchCase);
	internal delegate bool FindReplaceOneDelegate(string findText, string replaceText, bool matchWhole, bool matchCase);
	internal delegate int  FindReplaceAllDelegate(string findText, string replaceText, bool matchWhole, bool matchCase);

	#endregion

	#region Navigation Event Arguments

	// on a user initiated navigation create an event with the following EventArgs
	// user can set the cancel property to cancel the navigation
	public class HtmlNavigationEventArgs : EventArgs
	{
		//private variables
		private string _url = string.Empty;
		private bool _cancel = false;

		// constructor for event args
		public HtmlNavigationEventArgs(string url) : base()
		{
			_url = url;

		} //HtmlNavigationEventArgs

		// define url property get
		public string Url
		{
			get
			{
				return _url;
			}

		} //Url

		// define the cancel property
		// also allows a set operation
		public bool Cancel
		{
			get
			{
				return _cancel;
			}
			set
			{
				_cancel = value;
			}
		}

	} //HtmlNavigationEventArgs

	#endregion

	#region HtmlException defintion and Event Arguments

	//Exception class for HtmlEditor
	public class HtmlEditorException : ApplicationException
	{
		private string _operationName;

		// property for the operation name
		public string Operation
		{
			get
			{
				return _operationName;
			}
			set
			{
				_operationName = value;
			}

		} //OperationName


		// Default constructor
		public HtmlEditorException () : base()
		{
			_operationName = string.Empty;
		}
   
		// Constructor accepting a single string message
		public HtmlEditorException (string message) : base(message)
		{
			_operationName = string.Empty;
		}
   
		// Constructor accepting a string message and an inner exception
		public HtmlEditorException(string message, Exception inner) : base(message, inner)
		{
			_operationName = string.Empty;
		}

		// Constructor accepting a single string message and an operation name
		public HtmlEditorException(string message, string operation) : base(message)
		{
			_operationName = operation;
		}

		// Constructor accepting a string message an operation and an inner exception
		public HtmlEditorException(string message, string operation, Exception inner) : base(message, inner)
		{
			_operationName = operation;
		}

	} //HtmlEditorException


	// if capturing an exception internally throw an event with the following EventArgs
	public class HtmlExceptionEventArgs : EventArgs
	{
		//private variables
		private string _operation;
		private Exception _exception;

		// constructor for event args
		public HtmlExceptionEventArgs(string operation, Exception exception) : base()
		{
			_operation = operation;
			_exception = exception;

		} //HtmlEditorExceptionEventArgs

		// define operation name property get
		public string Operation
		{
			get
			{
				return _operation;
			}

		} //Operation

		// define the exception property get
		public Exception ExceptionObject
		{
			get
			{
				return _exception;
			}

		} //ExceptionObject

	} //HtmlExceptionEventArgs

	#endregion
}
