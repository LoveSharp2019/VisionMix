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
		/// 图像处理方法类集合
		/// </summary>
		// --------------------------------------------------------------------------------
		public ref class Fun
		{
			// TODO:  在此处添加此类的方法。

		public:
			/// ********************************************************************************
			/// <summary>
			/// 查找一维码
			/// </summary>
			/// <param name="image">需要查找的图像</param>
			/// <param name="searchRegion">搜索区域,如果为空就为全图搜索</param>
			/// <param name="symbolRegions">搜索到的结果图形区域</param>
			/// <param name="toolHandle">一维码模型handle</param>
			/// <param name="codeType">查找的编码类型,如果未知可以设置为"auto"</param>
			/// <param name="useCodeType">找到条码所用到的编码类型</param>
			/// <param name="decodedDataStrings">找到的结果文字,多个条码为数组形式</param>
			/// <created>linyugang,2018/3/3</created>
			/// <changed>linyugang,2018/3/15</changed>
			/// ********************************************************************************
			static void MyFindBarCode(HImage^ image, HRegion^ searchRegion, [Out] HRegion^% symbolRegions, HTuple^ toolHandle,
				HTuple^ codeType, [Out] System::String^% useCodeType, [Out] System::String^% decodedDataStrings);

			/// ********************************************************************************
			/// <summary>
			/// 创建并训练二维码模型--时间较长
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
			/// 查找二维码
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
			/// 使用动态阈值查找图像上的异色点
			/// </summary>
			/// <param name="image">要检查的图像</param>
			/// <param name="searchRegion">检查区域</param>
			/// <param name="errRegion">找到的异常</param>
			/// <param name="param">查找参数结构体</param>
			/// <created>linyugang,2018/6/12</created>
			/// <changed>linyugang,2018/6/12</changed>
			// ********************************************************************************
			static	void MyFindDirty(HImage^ image, HRegion^searchRegion,
				[Out]HRegion^% errRegion, DynThresholdParam^ param);


			// ********************************************************************************
			/// <summary>
			/// 使用手动分割方式做ocr
			/// </summary>
			/// <param name="image">原始图像</param>
			/// <param name="searchRegion">文字区域</param>
			/// <param name="txtRegion">结果文字轮廓</param>
			/// <param name="ocrHandle">查找的句柄</param>
			/// <param name="param">文字分割等参数结构体</param>
			/// <param name="score">结果分数</param>
			/// <param name="resultText">结果文字</param>
			/// <created>linyugang,2018/6/12</created>
			/// <changed>linyugang,2018/6/12</changed>
			// ********************************************************************************
			static void MyFindText(HImage^ image, HRegion^searchRegion,
				[Out]HRegion^% txtRegion, HTuple^ ocrHandle, OcrParam^ param,
				[Out]double% score, [Out]System::String^% resultText
			);
			// ********************************************************************************
			/// <summary>
			/// 二值化并筛选最大面积
			/// </summary>
			/// <param name="image">输入图像</param>
			/// <param name="thresholdMin">二值化最小值</param>
			/// <param name="thresholdMax">二值化最大值</param>
			/// <param name="resultRegion">筛选结果region</param>
			/// <created>linyugang,2018/5/15</created>
			/// <changed>linyugang,2018/5/15</changed>
			// ********************************************************************************
			static void MySelectStd(HImage^ image, int thresholdMin, int  thresholdMax, [Out]HRegion^% resultRegion);
			// ********************************************************************************
			/// <summary>
			/// 运行环境初始化
			/// </summary>
			/// <created>linyugang,2018/4/10</created>
			/// <changed>linyugang,2018/4/10</changed>
			// ********************************************************************************
			static void InitSystem();

			// ********************************************************************************
			/// <summary>
			/// 字符串加密转换,单相转换,不可逆
			/// </summary>
			/// <param name="dat">要加密的字符串</param>
			/// <returns>加密结果的字符串</returns>
			/// <created>linyugang,2018/4/21</created>
			/// <changed>linyugang,2018/4/21</changed>
			// ********************************************************************************
			static System::String^ EncryptString(System::String^ dat);
		};

	}
}