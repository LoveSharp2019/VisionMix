#pragma once
using namespace std;
#include<iostream>
typedef struct _OcrParamN
{
	std::string polarity;
	std::string ocrType;
	std::string expression;
	int thresholdValue;
	double closeCircleRadius;
	double openCircleRadius;
	int minCharArea;
	int maxCharArea;
	int minCharWidth;
	int maxCharWidth;
	int minCharHeight;
	int maxCharHeight;
	int partitionCharWidth;

}OcrParamN;