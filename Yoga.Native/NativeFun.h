#pragma once

#include "HalconCpp.h"
#include"DynThresholdParamN.h"
#include"OcrParamN.h"
class __declspec(dllexport)  NativeFun
{
public:
	NativeFun();
	static	void MyFindBarCode(const HalconCpp::HImage& image, const HalconCpp::HRegion &searchRegion, HalconCpp::HRegion* symbolRegions, const HalconCpp::HTuple &toolHandle,
		const HalconCpp::HTuple& codeType, std::string* useCodeType, std::string* decodedDataStrings);

	// ********************************************************************************
	/// <summary>
	/// 创建二维码图像处理工具
	/// </summary>
	/// <param name="image"></param>
	/// <param name="searchRegion"></param>
	/// <param name="wandCodeTpye">目标编码格式,若未知需要设置为"auto"</param>
	/// <param name="symbolXld"></param>
	/// <param name="toolHandle">二维码处理模型handle 若未找到二维码则为空句柄</param>
	/// <param name="useCodeType"></param>
	/// <param name="decodedDataStrings"></param>
	/// <created>linyugang,2018/4/21</created>
	/// <changed>linyugang,2018/4/21</changed>
	// ********************************************************************************
	static	void MyCreateCode2D(const HalconCpp::HImage& image, const HalconCpp::HRegion &searchRegion,const HalconCpp::HTuple& wandCodeTpye, const HalconCpp::HTuple& findMode, HalconCpp::HObject * symbolXld, HalconCpp::HTuple *toolHandle,
		std::string* useCodeType, std::string* decodedDataStrings);
	static	void MyFindCode2D(const HalconCpp::HImage& image, const HalconCpp::HRegion &searchRegion, HalconCpp::HObject * symbolXld, const HalconCpp::HTuple &toolHandle,
		std::string* decodedDataStrings);

	static	void MyFindDirty(const HalconCpp::HImage& image, const HalconCpp::HRegion &searchRegion,
		HalconCpp::HRegion * errRegion,const DynThresholdParamN&param);

	static void MyFindText(const HalconCpp::HImage& image, const HalconCpp::HRegion &searchRegion,
		HalconCpp::HRegion * txtRegion,	const HalconCpp::HTuple& ocrHandle,const OcrParamN& paran,
		double* score, std::string* resultText
	);

	static void MySelectStd(const HalconCpp::HImage& image,int thresholdMin,int  thresholdMax, HalconCpp::HRegion * resultRegion);
	static void InitSystem();
	static std::string EncryptString(const std::string& str);

};

