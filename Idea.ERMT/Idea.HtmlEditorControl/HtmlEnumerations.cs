using System;

namespace Microsoft.ConsultingServices.HtmlEditor
{

	// Enum used to insert a list
	public enum HtmlListType
	{
		Ordered,
		Unordered

	} //HtmlListType


	// Enum used to insert a heading
	public enum HtmlHeadingType
	{
		H1 = 1,
		H2 = 2,
		H3 = 3,
		H4 = 4,
		H5 = 5

	} //HtmlHeadingType


	// Enum used to define the navigate action on a user clicking a href
	public enum NavigateActionOption
	{
		Default,
		NewWindow,
		SameWindow

	} //NavigateActionOption


	// Enum used to define the image align property
	public enum ImageAlignOption
	{
		AbsBottom,
		AbsMiddle,
		Baseline,
		Bottom,
		Left,
		Middle,
		Right,
		TextTop,
		Top

	} //ImageAlignOption


	// Enum used to define the text alignment property
	public enum HorizontalAlignOption
	{
		Default,
		Left,
		Center,
		Right

	} //HorizontalAlignOption


	// Enum used to define the vertical alignment property
	public enum VerticalAlignOption
	{
		Default,
		Top,
		Bottom

	} //VerticalAlignOption


	// Enum used to define the visibility of the scroll bars
	public enum DisplayScrollBarOption
	{
		Yes,
		No,
		Auto

	} //DisplayScrollBarOption


	// Enum used to define the unit of measure for a value
	public enum MeasurementOption
	{
		Pixel,
		Percent

	} //MeasurementOption

}