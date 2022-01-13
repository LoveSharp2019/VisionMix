#include "stdafx.h"
#include "NativeFitPlane.h"
#include"Util.h"

#include <math.h>
#include"stdio.h"


#include<omp.h> 
using namespace std;
double SDet(double a[], int n)
{
	int num = 0;
	int num2 = 0;
	int num3 = 0;
	double num4 = 1.0;
	double num5 = 1.0;
	for (int i = 0; i <= n - 2; i++)
	{
		double num6 = 0.0;
		for (int j = i; j <= n - 1; j++)
		{
			for (int k = i; k <= n - 1; k++)
			{
				num3 = j * n + k;
				double num7 = fabs(a[num3]);
				if (num7 > num6)
				{
					num6 = num7;
					num = j;
					num2 = k;
				}
			}
			if (num6 + 1.0 == 1.0)
			{
				return 0.0;
			}
			if (num != i)
			{
				num4 = -num4;
				for (int k = i; k <= n - 1; k++)
				{
					int num8 = i * n + k;
					int num9 = num * n + k;
					double num7 = a[num8];
					a[num8] = a[num9];
					a[num9] = num7;
				}
			}
			if (num2 != i)
			{
				num4 = -num4;
				for (j = i; j <= n - 1; j++)
				{
					int num8 = j * n + num2;
					int num9 = j * n + i;
					double num7 = a[num8];
					a[num8] = a[num9];
					a[num9] = num7;
				}
			}
			num3 = i * n + i;
		}
		num5 *= a[num3];
		for (int j = i + 1; j <= n - 1; j++)
		{
			double num7 = a[j * n + i] / a[num3];
			for (int k = i + 1; k <= n - 1; k++)
			{
				int num8 = j * n + k;
				a[num8] -= num7 * a[i * n + k];
			}
		}
	}
	return num4 * num5 * a[n * n - 1];
}
bool FitPlane(int nPntSum, const double px[], const double   py[], const double pz[], nPlaneInfo *planeInfo)
{
	//Util::Invoke("开始");
	if (NULL == planeInfo)
	{
		return false;
	}

	if (nPntSum < 3)
	{
		Util::Invoke("数据个数小于3,无法拟合");
		return false;
	}

	//double array1[3][3] = { 0 };
	double array2[9] = { 0 };
	double num = 0.0;
	double num2 = 0.0;
	double num3 = 0.0;
	double num4 = 0.0;
	double num5 = 0.0;
	double num6 = 0.0;
	double num7 = 0.0;
	double num8 = 0.0;
	double num9 = 0.0;
	double num10 = 0.0;
	double num11 = 0.0;
	planeInfo->A = 0.0;
	planeInfo->B = 0.0;
	planeInfo->C = 0.0;
	planeInfo->D = 0.0;
	for (int i = 0L; i < nPntSum; i += 1L)
	{
		num += px[i] * px[i];
		num5 += px[i] * py[i];
		num9 += px[i];
		num2 = num5;
		num6 += py[i] * py[i];
		num10 += py[i];
		num3 = num9;
		num7 = num10;
		num4 += px[i] * pz[i];
		num8 += py[i] * pz[i];
		num11 += pz[i];
	}
	double num13 = (double)nPntSum;
	/*array1[0][ 0] = num;
	array1[0][1] = num2;
	array1[0][2] = num3;
	array1[1][0] = num5;
	array1[1][1] = num6;
	array1[1][2] = num7;
	array1[2][0] = num9;
	array1[2][1] = num10;
	array1[2][2] = num13;*/
	array2[0] = num;
	array2[1] = num2;
	array2[2] = num3;
	array2[3] = num5;
	array2[4] = num6;
	array2[5] = num7;
	array2[6] = num9;
	array2[7] = num10;
	array2[8] = num13;
	double num14 = SDet(array2, 3);
	if (num14 != 0.0)
	{
		/*array1[0][0] = num4;
		array1[0][1] = num2;
		array1[0][2] = num3;
		array1[1][0] = num8;
		array1[1][1] = num6;
		array1[1][2] = num7;
		array1[2][0] = num11;
		array1[2][1] = num10;
		array1[2][2] = num13;*/
		array2[0] = num4;
		array2[1] = num2;
		array2[2] = num3;
		array2[3] = num8;
		array2[4] = num6;
		array2[5] = num7;
		array2[6] = num11;
		array2[7] = num10;
		array2[8] = num13;
		double num15 = SDet(array2, 3);
		/*array1[0][0] = num;
		array1[0][1] = num4;
		array1[0][2] = num3;
		array1[1][0] = num5;
		array1[1][1] = num8;
		array1[1][2] = num7;
		array1[2][0] = num9;
		array1[2][1] = num11;
		array1[2][2] = num13;*/
		array2[0] = num;
		array2[1] = num4;
		array2[2] = num3;
		array2[3] = num5;
		array2[4] = num8;
		array2[5] = num7;
		array2[6] = num9;
		array2[7] = num11;
		array2[8] = num13;
		double num16 = SDet(array2, 3);
		/*	array1[0][0] = num;
			array1[0][1] = num2;
			array1[0][2] = num4;
			array1[1][0] = num5;
			array1[1][1] = num6;
			array1[1][2] = num8;
			array1[2][0] = num9;
			array1[2][1] = num10;
			array1[2][2] = num11;*/
		array2[0] = num;
		array2[1] = num2;
		array2[2] = num4;
		array2[3] = num5;
		array2[4] = num6;
		array2[5] = num8;
		array2[6] = num9;
		array2[7] = num10;
		array2[8] = num11;
		double num17 = SDet(array2, 3);
		planeInfo->A = num15 / num14;
		planeInfo->B = num16 / num14;
		planeInfo->C = -1.0;
		planeInfo->D = num17 / num14;
		return true;
	}
	return false;

	//planeInfo->A = 119;
	//Util::Invoke("结束");
	//return true;
	//wprintf(L"Struct_Change \n");
}

double DisPntToPlane(double dx, double dy, double dz, const nPlaneInfo & plane)
{
	double num = sqrt(plane.A * plane.A + plane.B * plane.B + plane.C * plane.C);

	/*const int sizeA = sizeof(double);
	char s[50] = { 0 };
	sprintf(s, "%lf", num);*/

	//Util::Invoke(s);
	double value = dx * plane.A + dy * plane.B + dz * plane.C + plane.D;
	/*sprintf(s, "%lf", value);

	Util::Invoke(s);*/
	return fabs(value) / num;
}
double ArrayyMax(double a[], int n)
{
	double max = a[0];
	for (int i = 1; i < n; i++)
	{
		if (a[i] > max)
			max = a[i];
	}
	return max;
}
double Flatness(int nPntSum, const double px[], const double py[], const double pz[], const nPlaneInfo & plane)
{
	double *arr = new double[nPntSum];
#pragma omp parallel for   //特别注意点。
	for (int i = 0L; i < nPntSum; i += 1L)
	{
		arr[i] = DisPntToPlane(px[i], py[i], pz[i], plane);
	}
	double max = ArrayyMax(arr, nPntSum);
	delete[] arr;//用完后别忘了delete
	//OutputDebugString(L"平面度计算完成");
	return max;
}
