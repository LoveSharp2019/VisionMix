#pragma once
using namespace std;
#include<iostream>
typedef struct _DynThresholdParamN
{
	std::string lightDark;
	double offset;
	int maskWidth;
	int maskHeight;
	double closingRadius;
	double openingRadius;
	double leastArea;
}DynThresholdParamN;
