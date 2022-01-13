#include "stdafx.h"
#include "HWndMessage.h"
#  include "HalconCpp.h"
using namespace HalconCpp;
#include"Util.h"
HWndMessage::HWndMessage()
{
	isInitialized = false;
}

HWndMessage::HWndMessage(HTuple message, int row, int colunm, int size, HTuple color)
{
	this->message = message;
	this->size = size;
	this->row = row;
	this->colunm = colunm;
	this->color = color;
	isInitialized = true;
}

HWndMessage::HWndMessage(HTuple message, int row, int colunm)
{
	this->message = message;
	this->row = row;
	this->colunm = colunm;
	isInitialized = true;
}



HWndMessage::~HWndMessage()
{
}
double HWndMessage::dispMessage(HTuple winID, QString coordSystem, double zoom, double currentSize)
{
	double sizeTmp = size* zoom;
	if (sizeTmp != currentSize)
	{
		try
		{
			setDisplayFont(winID, sizeTmp, "serif", "true", "false");
		}
		catch (const std::exception&)
		{

		}
		catch (const HException& /*hex*/)
		{
			/*QString dat = GBK("图像刷新异常出现:")+QString("%1").arg(hex.ErrorMessage().Text());;
			Util::Invoke((dat.toLocal8Bit().data()));*/
		}
	}

	dispMessage(winID, message, coordSystem.toLocal8Bit().data(), row, colunm, color, "false");
	return sizeTmp;
}

void HWndMessage::dispMessage(HalconCpp::HTuple winID, QString coordSystem, double zoom)
{
	double sizeTmp = size* zoom;

	setDisplayFont(winID, sizeTmp, "serif", "true", "false");

	dispMessage(winID, message, coordSystem.toLocal8Bit().data(), row, colunm, color, "false");
}

bool HWndMessage::IsInitialized() const
{
	return isInitialized;
}

HWndMessage HWndMessage::clone() const
{
	HWndMessage tmp = HWndMessage(this->message, this->row, this->colunm,
		this->size, this->color);
	return tmp;
}

void HWndMessage::dispMessage(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
	HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
{

	// Local iconic variables

	// Local control variables
	HTuple  hv_Red, hv_Green, hv_Blue, hv_Row1Part;
	HTuple  hv_Column1Part, hv_Row2Part, hv_Column2Part, hv_RowWin;
	HTuple  hv_ColumnWin, hv_WidthWin, hv_HeightWin, hv_MaxAscent;
	HTuple  hv_MaxDescent, hv_MaxWidth, hv_MaxHeight, hv_R1;
	HTuple  hv_C1, hv_FactorRow, hv_FactorColumn, hv_UseShadow;
	HTuple  hv_ShadowColor, hv_Exception, hv_Width, hv_Index;
	HTuple  hv_Ascent, hv_Descent, hv_W, hv_H, hv_FrameHeight;
	HTuple  hv_FrameWidth, hv_R2, hv_C2, hv_DrawMode, hv_CurrentColor;

	//This procedure displays text in a graphics window.
	//
	//Input parameters:
	//WindowHandle: The WindowHandle of the graphics window, where
	//   the message should be displayed
	//String: A tuple of strings containing the text message to be displayed
	//CoordSystem: If set to 'window', the text position is given
	//   with respect to the window coordinate system.
	//   If set to 'image', image coordinates are used.
	//   (This may be useful in zoomed images.)
	//Row: The row coordinate of the desired text position
	//   If set to -1, a default value of 12 is used.
	//Column: The column coordinate of the desired text position
	//   If set to -1, a default value of 12 is used.
	//Color: defines the color of the text as string.
	//   If set to [], '' or 'auto' the currently set color is used.
	//   If a tuple of strings is passed, the colors are used cyclically
	//   for each new textline.
	//Box: If Box[0] is set to 'true', the text is written within an orange box.
	//     If set to' false', no box is displayed.
	//     If set to a color string (e.g. 'white', '#FF00CC', etc.),
	//       the text is written in a box of that color.
	//     An optional second value for Box (Box[1]) controls if a shadow is displayed:
	//       'true' -> display a shadow in a default color
	//       'false' -> display no shadow (same as if no second value is given)
	//       otherwise -> use given string as color string for the shadow color
	//
	//Prepare window
	GetRgb(hv_WindowHandle, &hv_Red, &hv_Green, &hv_Blue);
	GetPart(hv_WindowHandle, &hv_Row1Part, &hv_Column1Part, &hv_Row2Part, &hv_Column2Part);
	GetWindowExtents(hv_WindowHandle, &hv_RowWin, &hv_ColumnWin, &hv_WidthWin, &hv_HeightWin);
	SetPart(hv_WindowHandle, 0, 0, hv_HeightWin - 1, hv_WidthWin - 1);
	//
	//default settings
	if (0 != (hv_Row == -1))
	{
		hv_Row = 12;
	}
	if (0 != (hv_Column == -1))
	{
		hv_Column = 12;
	}
	if (0 != (hv_Color == HTuple()))
	{
		hv_Color = "";
	}
	//
	hv_String = (("" + hv_String) + "").TupleSplit("\n");
	//
	//Estimate extentions of text depending on font size.
	GetFontExtents(hv_WindowHandle, &hv_MaxAscent, &hv_MaxDescent, &hv_MaxWidth, &hv_MaxHeight);
	if (0 != (hv_CoordSystem == HTuple("window")))
	{
		hv_R1 = hv_Row;
		hv_C1 = hv_Column;
	}
	else
	{
		//Transform image to window coordinates
		hv_FactorRow = (1.*hv_HeightWin) / ((hv_Row2Part - hv_Row1Part) + 1);
		hv_FactorColumn = (1.*hv_WidthWin) / ((hv_Column2Part - hv_Column1Part) + 1);
		hv_R1 = ((hv_Row - hv_Row1Part) + 0.5)*hv_FactorRow;
		hv_C1 = ((hv_Column - hv_Column1Part) + 0.5)*hv_FactorColumn;
	}
	//
	//Display text box depending on text size
	hv_UseShadow = 1;
	hv_ShadowColor = "gray";
	if (0 != (HTuple(hv_Box[0]) == HTuple("true")))
	{
		hv_Box[0] = "#fce9d4";
		hv_ShadowColor = "#f28d26";
	}
	if (0 != ((hv_Box.TupleLength()) > 1))
	{
		if (0 != (HTuple(hv_Box[1]) == HTuple("true")))
		{
			//Use default ShadowColor set above
		}
		else if (0 != (HTuple(hv_Box[1]) == HTuple("false")))
		{
			hv_UseShadow = 0;
		}
		else
		{
			hv_ShadowColor = ((const HTuple&)hv_Box)[1];
			//Valid color?
			try
			{
				SetColor(hv_WindowHandle, HTuple(hv_Box[1]));
			}
			// catch (Exception) 
			catch (HalconCpp::HException &HDevExpDefaultException)
			{
				HDevExpDefaultException.ToHTuple(&hv_Exception);
				hv_Exception = "Wrong value of control parameter Box[1] (must be a 'true', 'false', or a valid color string)";
				throw HalconCpp::HException(hv_Exception);
			}
		}
	}
	if (0 != (HTuple(hv_Box[0]) != HTuple("false")))
	{
		//Valid color?
		try
		{
			SetColor(hv_WindowHandle, HTuple(hv_Box[0]));
		}
		// catch (Exception) 
		catch (HalconCpp::HException &HDevExpDefaultException)
		{
			HDevExpDefaultException.ToHTuple(&hv_Exception);
			hv_Exception = "Wrong value of control parameter Box[0] (must be a 'true', 'false', or a valid color string)";
			throw HalconCpp::HException(hv_Exception);
		}
		//Calculate box extents
		hv_String = (" " + hv_String) + " ";
		hv_Width = HTuple();
		{
			HTuple end_val93 = (hv_String.TupleLength()) - 1;
			HTuple step_val93 = 1;
			for (hv_Index = 0; hv_Index.Continue(end_val93, step_val93); hv_Index += step_val93)
			{
				GetStringExtents(hv_WindowHandle, HTuple(hv_String[hv_Index]), &hv_Ascent,
					&hv_Descent, &hv_W, &hv_H);
				hv_Width = hv_Width.TupleConcat(hv_W);
			}
		}
		hv_FrameHeight = hv_MaxHeight*(hv_String.TupleLength());
		hv_FrameWidth = (HTuple(0).TupleConcat(hv_Width)).TupleMax();
		hv_R2 = hv_R1 + hv_FrameHeight;
		hv_C2 = hv_C1 + hv_FrameWidth;
		//Display rectangles
		GetDraw(hv_WindowHandle, &hv_DrawMode);
		SetDraw(hv_WindowHandle, "fill");
		//Set shadow color
		SetColor(hv_WindowHandle, hv_ShadowColor);
		if (0 != hv_UseShadow)
		{
			DispRectangle1(hv_WindowHandle, hv_R1 + 1, hv_C1 + 1, hv_R2 + 1, hv_C2 + 1);
		}
		//Set box color
		SetColor(hv_WindowHandle, HTuple(hv_Box[0]));
		DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
		SetDraw(hv_WindowHandle, hv_DrawMode);
	}
	//Write text.
	{
		HTuple end_val115 = (hv_String.TupleLength()) - 1;
		HTuple step_val115 = 1;
		for (hv_Index = 0; hv_Index.Continue(end_val115, step_val115); hv_Index += step_val115)
		{
			hv_CurrentColor = ((const HTuple&)hv_Color)[hv_Index % (hv_Color.TupleLength())];
			if (0 != (HTuple(hv_CurrentColor != HTuple("")).TupleAnd(hv_CurrentColor != HTuple("auto"))))
			{
				SetColor(hv_WindowHandle, hv_CurrentColor);
			}
			else
			{
				SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
			}
			hv_Row = hv_R1 + (hv_MaxHeight*hv_Index);
			SetTposition(hv_WindowHandle, hv_Row, hv_C1);
			WriteString(hv_WindowHandle, HTuple(hv_String[hv_Index]));
		}
	}
}

void HWndMessage::setDisplayFont(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font, HTuple hv_Bold,
	HTuple hv_Slant)
{

	// Local iconic variables

	// Local control variables
	HTuple  hv_OS, hv_Fonts, hv_Style, hv_Exception;
	HTuple  hv_AvailableFonts, hv_Fdx, hv_Indices;

	//This procedure sets the text font of the current window with
	//the specified attributes.
	//
	//Input parameters:
	//WindowHandle: The graphics window for which the font will be set
	//Size: The font size. If Size=-1, the default of 16 is used.
	//Bold: If set to 'true', a bold font is used
	//Slant: If set to 'true', a slanted font is used
	//
	GetSystem("operating_system", &hv_OS);
	// dev_get_preferences(...); only in hdevelop
	// dev_set_preferences(...); only in hdevelop
	if (0 != (HTuple(hv_Size == HTuple()).TupleOr(hv_Size == -1)))
	{
		hv_Size = 16;
	}
	if (0 != ((hv_OS.TupleSubstr(0, 2)) == HTuple("Win")))
	{
		//Restore previous behaviour
		hv_Size = (1.13677*hv_Size).TupleInt();
	}
	if (0 != (hv_Font == HTuple("Courier")))
	{
		hv_Fonts.Clear();
		hv_Fonts[0] = "Courier";
		hv_Fonts[1] = "Courier 10 Pitch";
		hv_Fonts[2] = "Courier New";
		hv_Fonts[3] = "CourierNew";
	}
	else if (0 != (hv_Font == HTuple("mono")))
	{
		hv_Fonts.Clear();
		hv_Fonts[0] = "Consolas";
		hv_Fonts[1] = "Menlo";
		hv_Fonts[2] = "Courier";
		hv_Fonts[3] = "Courier 10 Pitch";
		hv_Fonts[4] = "FreeMono";
	}
	else if (0 != (hv_Font == HTuple("sans")))
	{
		hv_Fonts.Clear();
		hv_Fonts[0] = "Luxi Sans";
		hv_Fonts[1] = "DejaVu Sans";
		hv_Fonts[2] = "FreeSans";
		hv_Fonts[3] = "Arial";
	}
	else if (0 != (hv_Font == HTuple("serif")))
	{
		hv_Fonts.Clear();
		hv_Fonts[0] = "Times New Roman";
		hv_Fonts[1] = "Luxi Serif";
		hv_Fonts[2] = "DejaVu Serif";
		hv_Fonts[3] = "FreeSerif";
		hv_Fonts[4] = "Utopia";
	}
	else
	{
		hv_Fonts = hv_Font;
	}
	hv_Style = "";
	if (0 != (hv_Bold == HTuple("true")))
	{
		hv_Style += HTuple("Bold");
	}
	else if (0 != (hv_Bold != HTuple("false")))
	{
		hv_Exception = "Wrong value of control parameter Bold";
		throw HalconCpp::HException(hv_Exception);
	}
	if (0 != (hv_Slant == HTuple("true")))
	{
		hv_Style += HTuple("Italic");
	}
	else if (0 != (hv_Slant != HTuple("false")))
	{
		hv_Exception = "Wrong value of control parameter Slant";
		throw HalconCpp::HException(hv_Exception);
	}
	if (0 != (hv_Style == HTuple("")))
	{
		hv_Style = "Normal";
	}
	QueryFont(hv_WindowHandle, &hv_AvailableFonts);
	hv_Font = "";
	{
		HTuple end_val48 = (hv_Fonts.TupleLength()) - 1;
		HTuple step_val48 = 1;
		for (hv_Fdx = 0; hv_Fdx.Continue(end_val48, step_val48); hv_Fdx += step_val48)
		{
			hv_Indices = hv_AvailableFonts.TupleFind(HTuple(hv_Fonts[hv_Fdx]));
			if (0 != ((hv_Indices.TupleLength()) > 0))
			{
				if (0 != (HTuple(hv_Indices[0]) >= 0))
				{
					hv_Font = HTuple(hv_Fonts[hv_Fdx]);
					break;
				}
			}
		}
	}
	if (0 != (hv_Font == HTuple("")))
	{
		throw HalconCpp::HException("Wrong value of control parameter Font");
	}
	hv_Font = (((hv_Font + "-") + hv_Style) + "-") + hv_Size;
	SetFont(hv_WindowHandle, hv_Font);
	// dev_set_preferences(...); only in hdevelop
	return;
}
