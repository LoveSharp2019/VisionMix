#pragma once
#include "Yoga.Native.h"
typedef struct _PlaneInfo
{
	double A;
	double B;
	double C;
	double D;
} nPlaneInfo;
extern "C" __declspec(dllexport)  bool FitPlane(int nPntSum, const double px[], const double   py[], const double pz[], nPlaneInfo *planeInfo);

extern "C" __declspec(dllexport) double DisPntToPlane(double dx, double dy, double dz, const nPlaneInfo& plane);

extern "C" __declspec(dllexport)  double Flatness(int nPntSum, const double px[], const double   py[], const double pz[], const nPlaneInfo& plane);

