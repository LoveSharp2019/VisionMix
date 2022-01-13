// 这是主 DLL 文件。

#include "stdafx.h"
#include <msclr\marshal_cppstd.h>  
#include "Fun.h"
//#include"NativeFun.h"
#include"NativeShowUnit.h"
#include"NativeFun.h"
#include"NativeTest.h"
using namespace msclr::interop;

void Yoga::Wrapper::Fun::MyFindBarCode(HImage^ image, HRegion^ searchRegion, [Out] HRegion^% symbolRegions,
	HTuple^ toolHandle,
	HTuple^ codeType, [Out] System::String^% useCodeType, [Out] System::String^% decodedDataStrings)
{
	HalconCpp::HImage n_image = Conversion::ImageToNative(image);
	HalconCpp::HRegion n_region = Conversion::RegionToNative(searchRegion);
	HalconCpp::HRegion n_SymbolRegions;
	HalconCpp::HTuple n_toolHandle = Conversion::TupleToNative(toolHandle);
	HalconCpp::HTuple n_CodeType = Conversion::TupleToNative(codeType);
	std::string n_useCodeType, n_DecodedDataStrings;
	NativeFun::MyFindBarCode(n_image, n_region, &n_SymbolRegions, n_toolHandle, n_CodeType, &n_useCodeType, &n_DecodedDataStrings);

	symbolRegions = Conversion::RegionToManaged(n_SymbolRegions);
	useCodeType = Conversion::StringToManged(n_useCodeType);
	decodedDataStrings = Conversion::StringToManged(n_DecodedDataStrings);
}

void Yoga::Wrapper::Fun::MyCreateCode2D(HImage^ image, HRegion^ searchRegion, HTuple^ wandCodeTpye, HTuple^ findMode, [Out]HObject^% symbolXld, [Out]HTuple^% toolHandle,
	[Out]System::String^% useCodeType, [Out]System::String^% decodedDataStrings)
{
	HalconCpp::HImage n_image = Conversion::ImageToNative(image);
	HalconCpp::HRegion n_region = Conversion::RegionToNative(searchRegion);
	HalconCpp::HTuple n_wandCodeTpye = Conversion::TupleToNative(wandCodeTpye);
	HalconCpp::HTuple n_findMode = Conversion::TupleToNative(findMode);
	HalconCpp::HXLDCont n_symbolXld;
	HalconCpp::HTuple n_toolhandle;
	std::string n_DecodedDataStrings, n_useCodeType;
	NativeFun::MyCreateCode2D(n_image, n_region, n_wandCodeTpye, n_findMode, &n_symbolXld, &n_toolhandle, &n_useCodeType, &n_DecodedDataStrings);

	toolHandle = Conversion::TupleToManaged(n_toolhandle);
	symbolXld = Conversion::ObjectToManaged(n_symbolXld);
	useCodeType = Conversion::StringToManged(n_useCodeType);
	decodedDataStrings = Conversion::StringToManged(n_DecodedDataStrings);
}

void Yoga::Wrapper::Fun::MyFindCode2D(HImage^  image, HRegion^ searchRegion, [Out]HObject^% symbolXld, HTuple^ toolHandle,
	[Out]System::String^% decodedDataStrings)
{
	HalconCpp::HImage n_image = Conversion::ImageToNative(image);
	HalconCpp::HRegion n_region = Conversion::RegionToNative(searchRegion);
	HalconCpp::HTuple n_toolhandle = Conversion::TupleToNative(toolHandle);
	HalconCpp::HXLDCont n_symbolXld;
	HalconCpp::HTuple  n_useCodeType;
	std::string n_DecodedDataStrings;
	NativeFun::MyFindCode2D(n_image, n_region, &n_symbolXld, n_toolhandle, &n_DecodedDataStrings);
	symbolXld = Conversion::ObjectToManaged(n_symbolXld);
	decodedDataStrings = Conversion::StringToManged(n_DecodedDataStrings);
}


void Yoga::Wrapper::Fun::MyFindDirty(HImage ^ image, HRegion ^ searchRegion, HRegion ^% errRegion, DynThresholdParam ^ param)
{
	HalconCpp::HImage n_image = Conversion::ImageToNative(image);
	HalconCpp::HRegion n_region = Conversion::RegionToNative(searchRegion);
	DynThresholdParamN n_param = Conversion::DynThresholdParamToNative(param);

	HalconCpp::HRegion n_errRegion;
	NativeFun::MyFindDirty(n_image, n_region, &n_errRegion, n_param);

	errRegion = Conversion::RegionToManaged(n_errRegion);
}

void Yoga::Wrapper::Fun::MyFindText(HImage ^ image, HRegion ^ searchRegion, [Out]HRegion ^% txtRegion,
	HTuple ^ ocrHandle, OcrParam ^ param,
	[Out]double % score, [Out] System::String ^% resultText)
{
	HalconCpp::HImage n_image = Conversion::ImageToNative(image);
	HalconCpp::HRegion n_searchRegion = Conversion::RegionToNative(searchRegion);

	HalconCpp::HTuple n_ocrHandle = Conversion::TupleToNative(ocrHandle);
	OcrParamN n_ocrParam = Conversion::OcrPramToNative(param);
	HalconCpp::HRegion n_txtRegion;
	double n_score;
	std::string n_resultText;
	NativeFun::MyFindText(n_image, n_searchRegion, &n_txtRegion, n_ocrHandle, n_ocrParam,
		&n_score, &n_resultText);
	txtRegion = Conversion::RegionToManaged(n_txtRegion);
	score = n_score;
	resultText = Conversion::StringToManged(n_resultText);
}

void Yoga::Wrapper::Fun::MySelectStd(HImage ^ image, int thresholdMin, int thresholdMax, [Out]HRegion^% resultRegion)
{
	HalconCpp::HImage n_image = Conversion::ImageToNative(image);
	HalconCpp::HRegion n_Region;
	NativeFun::MySelectStd(n_image, thresholdMin, thresholdMax, &n_Region);
	resultRegion = Conversion::RegionToManaged(n_Region);
}

void Yoga::Wrapper::Fun::InitSystem()
{
	NativeFun::InitSystem();
}

System::String^ Yoga::Wrapper::Fun::EncryptString(System::String^ dat)
{
	std::string cliToStd = marshal_as<std::string>(dat);
	std::string StdResult = NativeFun::EncryptString(cliToStd);
	System::String^ result = marshal_as<String^>(StdResult);
	return result;
	//return NativeFun::EncryptString(str);
}
