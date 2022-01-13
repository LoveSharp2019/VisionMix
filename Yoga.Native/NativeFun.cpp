#include "stdafx.h"
#include "NativeFun.h"
#include"Util.h"
#include"qstring.h"
#include "MyMD5.h"
using namespace HalconCpp;

NativeFun::NativeFun()
{
}

void NativeFun::MyFindBarCode(const HImage & image, const HRegion & searchRegion, HRegion* symbolRegions, const HTuple & toolHandle, const HTuple & codeType, std::string * useCodeType, std::string * decodedDataStrings)
{

	if (image.IsInitialized() == false)
	{
		return;
	}
	//判断是否存在搜索框
	bool reduceFlag = false;
	if (searchRegion.IsInitialized())
	{
		HTuple r, c;
		HTuple area = searchRegion.AreaCenter(&r, &c);
		if (area.D() > 0)
		{
			reduceFlag = true;
		}
	}
	HImage findImg;
	if (reduceFlag)
	{
		findImg = image.ReduceDomain(searchRegion);
	}
	else
	{
		findImg = image;
	}
	HTuple dataStr, codeTypeUse;
	HalconCpp::FindBarCode(findImg, symbolRegions, toolHandle, codeType, &dataStr);
	GetBarCodeResult(toolHandle, "all", "decoded_types", &codeTypeUse);
	if (dataStr.Length()>0)
	{
		*decodedDataStrings = dataStr.S().Text();
	}
	if (codeTypeUse.Length()>0)
	{
		*useCodeType = codeTypeUse.S().Text();
	}
}


void NativeFun::MyCreateCode2D(const HalconCpp::HImage& image, const HalconCpp::HRegion &searchRegion, const HalconCpp::HTuple& wandCodeTpye, const HalconCpp::HTuple& findMode, HalconCpp::HObject * symbolXld, HalconCpp::HTuple *toolHandle,
	std::string* useCodeType, std::string* decodedDataStrings)
{

	if (image.IsInitialized()==false)
	{
		return;
	}
	HTuple CodsTypes;
	//编码类型初始化
	CodsTypes.Clear();
	if (wandCodeTpye == "auto")
	{
		/*	Util::Invoke("自动模式");*/
		CodsTypes[0] = "Data Matrix ECC 200";
		CodsTypes[1] = "QR Code";
		CodsTypes[2] = "Micro QR Code";
		CodsTypes[3] = "PDF417";
		CodsTypes[4] = "GS1 DataMatrix";
		CodsTypes[5] = "GS1 QR Code";
		CodsTypes[6] = "GS1 Aztec Code";
		CodsTypes[7] = "Aztec Code";
	}
	else
	{
		CodsTypes = wandCodeTpye;
	}

	//判断是否存在搜索框
	bool reduceFlag = false;
	if (searchRegion.IsInitialized())
	{
		HTuple r, c;
		HTuple area = searchRegion.AreaCenter(&r, &c);
		if (area.D() > 0)
		{
			reduceFlag = true;
		}
	}

	HImage findImg;
	/*Util::Invoke("声明图像变量");
	return;*/
	if (reduceFlag)
	{
		findImg = image.ReduceDomain(searchRegion);
	}
	else
	{
		findImg = image;
		//Notify(GBK("复制图像"));
	}

	HTuple resultHandles;
	HTuple handleTmp;
	/*Util::Invoke("实际尝试创建二维码模型");
	return;*/
	for (int i = 0; i < CodsTypes.Length(); i++)
	{
		HTuple CodeType = CodsTypes[i];
		CreateDataCode2dModel(CodeType, HTuple("default_parameters"), findMode, &handleTmp);
		SetDataCode2dParam(handleTmp, "timeout", 200);
		//Notify( CodeType.S().Text());
		HTuple dataStr;
		try
		{
			FindDataCode2d(findImg, symbolXld, handleTmp, "train", "all",
				&resultHandles, &dataStr);
		}
		catch (const HalconCpp::HException& )
		{
			;
		}

		//读出条码
		if (dataStr.Length() > 0)
		{
			*decodedDataStrings = dataStr.S().Text();
			*useCodeType = CodsTypes[i].S().Text();
			*toolHandle = handleTmp;
			return;
		}
		HalconCpp::ClearDataCode2dModel(handleTmp);
	}
	
}

void NativeFun::MyFindCode2D(const HalconCpp::HImage & image, const HalconCpp::HRegion & searchRegion, HalconCpp::HObject * symbolXld, const HalconCpp::HTuple & toolHandle, std::string * decodedDataStrings)
{
	if (image.IsInitialized() == false)
	{
		return;
	}
	//判断是否存在搜索框
	bool reduceFlag = false;
	if (searchRegion.IsInitialized())
	{
		HTuple r, c;
		HTuple area = searchRegion.AreaCenter(&r, &c);
		if (area.D() > 0)
		{
			reduceFlag = true;
		}
	}
	HImage findImg;
	if (reduceFlag)
	{
		findImg = image.ReduceDomain(searchRegion);
	}
	else
	{
		findImg = image;
	}
	HTuple resultHandles;
	HTuple dataStr;
	FindDataCode2d(findImg, symbolXld, toolHandle, HTuple(), HTuple(),
		&resultHandles, &dataStr);
	if (dataStr.Length() > 0)
	{
		*decodedDataStrings = dataStr.S().Text();
	}
	else
	{
		*decodedDataStrings = std::string();
	}
}


void NativeFun::MyFindDirty(const HalconCpp::HImage & image, const HalconCpp::HRegion & searchRegion, HalconCpp::HRegion * errRegion, const DynThresholdParamN & param)
{
	if (image.IsInitialized() == false)
	{
		return;
	}
	//Notify("fint dirty");
	int max = max(param.maskWidth, param.maskHeight);
	if (max < 1)
	{
		max = 1;
	}
	HImage imgReduce = image.ReduceDomain(searchRegion);
	//将roi边界区域扩充至边界外,防止干扰
	HImage expandImg = imgReduce.ExpandDomainGray(max);
	HImage meanImgMax = expandImg.MeanImage(param.maskWidth, param.maskHeight);
	//再次reducedomain到平均值图
	HImage meanImg = meanImgMax.ReduceDomain(searchRegion);

	HRegion errRegiontmp = imgReduce.DynThreshold(meanImg, (double)param.offset, param.lightDark.data());

	if (errRegiontmp.IsInitialized())
	{
		//闭运算来膨胀边缘
		errRegiontmp = errRegiontmp.ClosingCircle(param.closingRadius);
		//开运算  腐蚀求区域
		errRegiontmp = errRegiontmp.OpeningCircle(param.openingRadius);

		//面积
		errRegiontmp = errRegiontmp.Connection();
		errRegiontmp = errRegiontmp.SelectShape("area", "and", param.leastArea, 99999.0);
	}
	*errRegion = errRegiontmp;
}

void NativeFun::MyFindText(const HalconCpp::HImage & image, const HalconCpp::HRegion & searchRegion, HalconCpp::HRegion * txtRegion, const HalconCpp::HTuple & ocrHandle, const OcrParamN & param, double * score, std::string * resultText)
{
	if (image.IsInitialized() == false)
	{
		return;
	}
	HImage findImage;
	//Util::Invoke("ocr查找开始");
	//Notify("1 通道判断");
	//通道判断
	if (image.CountChannels() == 3)
	{
		findImage = image.Rgb1ToGray();
	}
	else
	{
		findImage = image;
	}

	//抠图
	findImage = findImage.ReduceDomain(searchRegion);


	//动态阈值  01 求出region内的平均图像
	HImage meanImage = searchRegion.RegionToMean(findImage);
	/*Notify("3 极性判断");*/
	HTuple lightDark = "light";
	if (param.polarity != "light_on_dark")
	{
		lightDark = "dark";
		//Notify("3 极性判断--黑字");
	}
	else
	{
		//Notify("3 极性判断--白字");
	}
	HRegion regionDynThresh = findImage.DynThreshold(meanImage, param.thresholdValue, lightDark);
	//形态筛选
	HRegion regionClosing = regionDynThresh;
	if (param.closeCircleRadius>0.5)
	{
		regionClosing = regionDynThresh.ClosingCircle(param.closeCircleRadius);
	}
	HRegion regionOpening = regionClosing;
	if (param.openCircleRadius>0.5)
	{
		regionOpening = regionClosing.OpeningCircle(param.openCircleRadius);
	}
	HRegion ConnectedRegions1 = regionOpening.Connection();

	//特征筛选
	HRegion SelectShape1 = ConnectedRegions1.SelectShape("area", "and", param.minCharArea, param.maxCharArea);

	HRegion selectShape2 = SelectShape1.SelectShape("width", "and", param.minCharWidth, param.maxCharWidth);

	HRegion selectShape3 = selectShape2.SelectShape("height", "and", param.minCharHeight, param.maxCharHeight);

	HRegion shaprUnion = selectShape3.Union1();

	//获取查找区域的最小矩形来确定膨胀参数
	HTuple ho_row1, ho_column1, ho_row2, ho_column2, ho_width, ho_height;
	searchRegion.SmallestRectangle1(&ho_row1, &ho_column1, &ho_row2, &ho_column2);
	ho_width = ho_column2 - ho_column1;
	ho_height = ho_row2 - ho_row1;
	//横向及竖向膨胀忽略文字内部区域
	HRegion RegionDilation1 = shaprUnion.DilationRectangle1(ho_width / 2, 1);
	HRegion RegionDilation2 = shaprUnion.DilationRectangle1(1, ho_height / 2);
	//求交集获取每个文字的一团
	HRegion RegionIntersection1 = RegionDilation1.Intersection(RegionDilation2);

	//文字粘连分割
	HRegion ConnectedRegions = RegionIntersection1.Connection();

	HRegion Partitioned = ConnectedRegions.PartitionDynamic(param.partitionCharWidth, 20);

	HRegion RegionIntersection2 = Partitioned.Intersection(shaprUnion);

	//判断查找到的文字的行数来判断是否找到文字

	//文字排序
	HRegion textHObject = RegionIntersection2.SortRegion("upper_left", "true", "column");


	HTuple confidence;
	//查找到的字符串
	HTuple resultClass;
	HTuple word, scoreTmp;

	HImage ocrImg;
	if (param.polarity == "light_on_dark")
	{
		ocrImg = findImage.InvertImage();
	}
	else
	{
		ocrImg = findImage;
	}
	/*if (param.ocrType == "mpl")
	{*/
		HalconCpp::DoOcrWordMlp(textHObject, ocrImg, ocrHandle, param.expression.data(),
			3, 5, &resultClass, &confidence, &word, &scoreTmp);
	/*}
	else
	{
		HalconCpp::DoOcrWordCnn(textHObject, ocrImg, ocrHandle, param.expression.data(),
			3, 5, &resultClass, &confidence, &word, &scoreTmp);
	}*/

	//取信心与正则表达式的最小值来作为最后识别结果的分值
	HTuple scoreTmp1;
	scoreTmp1.Clear();
	scoreTmp1.Append(confidence);
	scoreTmp1.Append(scoreTmp);
	*txtRegion = textHObject;
	//结果分数
	*score = scoreTmp1.TupleMin().D();
	*resultText = word.S().Text();
}

void NativeFun::MySelectStd(const HalconCpp::HImage & image, int thresholdMin, int thresholdMax, HalconCpp::HRegion * resultRegion)
{
	HRegion regionBin = image.Threshold((double)thresholdMin, thresholdMax);
	HRegion conn = regionBin.Connection();
	*resultRegion = conn.SelectShapeStd("max_area",70);
}

void NativeFun::InitSystem()
{
	SetSystem("clip_region", "false");
#if defined(_WIN32)
	SetSystem("use_window_thread", "true");
#elif defined(__linux__)
	XInitThreads();
#endif
	//utf8用于二维码查找中文时候防止乱码-测试中
	//SetSystem("filename_encoding", "utf8");
}

std::string NativeFun::EncryptString(const std::string & str)
{
	std::string dat = str + "linyugangMd5Encrypt";
	return  MyMD5(dat).toStr();
	//return MyMD5(str).toStr();
}
