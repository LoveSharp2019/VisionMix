// Wrapper.h

#pragma once

#include "stdafx.h"
#include "Conversion.h"

#include"DynThresholdParamN.h"
#include"DynThresholdParam.h"	

#include"OcrParam.h"
#include"OcrParamN.h"

using namespace HalconDotNet;
using namespace System::Runtime::InteropServices;
using namespace System::Runtime::Serialization;
using namespace System;
namespace Yoga
{
	namespace Wrapper {
		// --------------------------------------------------------------------------------
		/// <summary>
		/// ͼ�������༯��
		/// </summary>
		// --------------------------------------------------------------------------------
		public ref class Fun
		{
			// TODO:  �ڴ˴���Ӵ���ķ�����

		public:
			/// ********************************************************************************
			/// <summary>
			/// ����һά��
			/// </summary>
			/// <param name="image">��Ҫ���ҵ�ͼ��</param>
			/// <param name="searchRegion">��������,���Ϊ�վ�Ϊȫͼ����</param>
			/// <param name="symbolRegions">�������Ľ��ͼ������</param>
			/// <param name="toolHandle">һά��ģ��handle</param>
			/// <param name="codeType">���ҵı�������,���δ֪��������Ϊ"auto"</param>
			/// <param name="useCodeType">�ҵ��������õ��ı�������</param>
			/// <param name="decodedDataStrings">�ҵ��Ľ������,�������Ϊ������ʽ</param>
			/// <created>linyugang,2018/3/3</created>
			/// <changed>linyugang,2018/3/15</changed>
			/// ********************************************************************************
			static void MyFindBarCode(HImage^ image, HRegion^ searchRegion, [Out] HRegion^% symbolRegions, HTuple^ toolHandle,
				HTuple^ codeType, [Out] System::String^% useCodeType, [Out] System::String^% decodedDataStrings);

			/// ********************************************************************************
			/// <summary>
			/// ������ѵ����ά��ģ��--ʱ��ϳ�
			/// </summary>
			/// <param name="image"></param>
			/// <param name="searchRegion"></param>
			/// <param name="symbolXld"></param>
			/// <param name="toolHandle"></param>
			/// <param name="useCodeType"></param>
			/// <param name="decodedDataStrings"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static	void MyCreateCode2D(HImage^ image, HRegion^ searchRegion, HTuple^ wandCodeTpye, HTuple^ findMode, [Out]HObject^% symbolXld, [Out]HTuple^% toolHandle,
				[Out]System::String^% useCodeType, [Out]System::String^% decodedDataStrings);
			/// ********************************************************************************
			/// <summary>
			/// ���Ҷ�ά��
			/// </summary>
			/// <param name="image"></param>
			/// <param name="searchRegion"></param>
			/// <param name="symbolXld"></param>
			/// <param name="toolHandle"></param>
			/// <param name="decodedDataStrings"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static	void MyFindCode2D(HImage^  image, HRegion^ searchRegion, [Out]HObject^% symbolXld, HTuple^ toolHandle,
				[Out]System::String^% decodedDataStrings);


			// ********************************************************************************
			/// <summary>
			/// ʹ�ö�̬��ֵ����ͼ���ϵ���ɫ��
			/// </summary>
			/// <param name="image">Ҫ����ͼ��</param>
			/// <param name="searchRegion">�������</param>
			/// <param name="errRegion">�ҵ����쳣</param>
			/// <param name="param">���Ҳ����ṹ��</param>
			/// <created>linyugang,2018/6/12</created>
			/// <changed>linyugang,2018/6/12</changed>
			// ********************************************************************************
			static	void MyFindDirty(HImage^ image, HRegion^searchRegion,
				[Out]HRegion^% errRegion, DynThresholdParam^ param);


			// ********************************************************************************
			/// <summary>
			/// ʹ���ֶ��ָʽ��ocr
			/// </summary>
			/// <param name="image">ԭʼͼ��</param>
			/// <param name="searchRegion">��������</param>
			/// <param name="txtRegion">�����������</param>
			/// <param name="ocrHandle">���ҵľ��</param>
			/// <param name="param">���ַָ�Ȳ����ṹ��</param>
			/// <param name="score">�������</param>
			/// <param name="resultText">�������</param>
			/// <created>linyugang,2018/6/12</created>
			/// <changed>linyugang,2018/6/12</changed>
			// ********************************************************************************
			static void MyFindText(HImage^ image, HRegion^searchRegion,
				[Out]HRegion^% txtRegion, HTuple^ ocrHandle, OcrParam^ param,
				[Out]double% score, [Out]System::String^% resultText
			);
			// ********************************************************************************
			/// <summary>
			/// ��ֵ����ɸѡ������
			/// </summary>
			/// <param name="image">����ͼ��</param>
			/// <param name="thresholdMin">��ֵ����Сֵ</param>
			/// <param name="thresholdMax">��ֵ�����ֵ</param>
			/// <param name="resultRegion">ɸѡ���region</param>
			/// <created>linyugang,2018/5/15</created>
			/// <changed>linyugang,2018/5/15</changed>
			// ********************************************************************************
			static void MySelectStd(HImage^ image, int thresholdMin, int  thresholdMax, [Out]HRegion^% resultRegion);
			// ********************************************************************************
			/// <summary>
			/// ���л�����ʼ��
			/// </summary>
			/// <created>linyugang,2018/4/10</created>
			/// <changed>linyugang,2018/4/10</changed>
			// ********************************************************************************
			static void InitSystem();

			// ********************************************************************************
			/// <summary>
			/// �ַ�������ת��,����ת��,������
			/// </summary>
			/// <param name="dat">Ҫ���ܵ��ַ���</param>
			/// <returns>���ܽ�����ַ���</returns>
			/// <created>linyugang,2018/4/21</created>
			/// <changed>linyugang,2018/4/21</changed>
			// ********************************************************************************
			static System::String^ EncryptString(System::String^ dat);
		};

	}
}