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
		/// ocr������ȡ����
		/// </summary>
		// --------------------------------------------------------------------------------
		[Serializable]
		public ref struct OcrParam
		{
			/// <summary>���ּ��� "light_on_dark"���� "dark_on_light";</summary>
			String^ polarity;
			/// <summary>ocrģ�͵�����  "mpl"����"cnn";</summary>
			String^ ocrType;
			/// <summary>�����������ʽ</summary>
			String^ expression;
			/// <summary>��ȡ�����뱳���ĻҶȲ�ֵ</summary>
			Int32 thresholdValue;
			/// <summary>���������������뾶 ���С��</summary>
			Double closeCircleRadius;
			/// <summary>��������������뾶 ��ʴС��</summary>
			Double openCircleRadius;
			/// <summary>���ֵ���С���</summary>
			Int32 minCharArea;
			/// <summary>����������</summary>
			Int32 maxCharArea;
			/// <summary>������С���</summary>
			Int32 minCharWidth;
			/// <summary>���������</summary>
			Int32 maxCharWidth;
			/// <summary>��С�ַ��߶�</summary>
			Int32 minCharHeight;
			/// <summary>����ַ��߶�</summary>
			Int32 maxCharHeight;
			/// <summary>��̬�ָ�ճ������ƽ�����</summary>
			Int32 partitionCharWidth;
		};

	}
}