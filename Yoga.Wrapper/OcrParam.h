#pragma once
using namespace System::Runtime::InteropServices;
using namespace System::Runtime::Serialization;
using namespace System;
namespace Yoga
{
	namespace Wrapper
	{
		// --------------------------------------------------------------------------------
		/// <summary>
		/// ocr文字提取参数
		/// </summary>
		// --------------------------------------------------------------------------------
		[Serializable]
		public ref struct OcrParam
		{
			/// <summary>文字极性 "light_on_dark"或者 "dark_on_light";</summary>
			String^ polarity;
			/// <summary>ocr模型的类型  "mpl"或者"cnn";</summary>
			String^ ocrType;
			/// <summary>结果的正则表达式</summary>
			String^ expression;
			/// <summary>提取文字与背景的灰度差值</summary>
			Int32 thresholdValue;
			/// <summary>文字区域处理闭运算半径 填充小孔</summary>
			Double closeCircleRadius;
			/// <summary>文字区域处理开运算半径 腐蚀小点</summary>
			Double openCircleRadius;
			/// <summary>文字的最小面积</summary>
			Int32 minCharArea;
			/// <summary>文字最大面积</summary>
			Int32 maxCharArea;
			/// <summary>文字最小宽度</summary>
			Int32 minCharWidth;
			/// <summary>文字最大宽度</summary>
			Int32 maxCharWidth;
			/// <summary>最小字符高度</summary>
			Int32 minCharHeight;
			/// <summary>最大字符高度</summary>
			Int32 maxCharHeight;
			/// <summary>动态分割粘连文字平均宽度</summary>
			Int32 partitionCharWidth;
		};

	}
}