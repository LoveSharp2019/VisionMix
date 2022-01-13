#pragma once

#include "Conversion.h"
using namespace HalconDotNet;
using namespace System::Runtime::InteropServices;
using namespace System::Runtime::Serialization;
using namespace System;
namespace Yoga {
	namespace Wrapper {
		public ref class ShowUnit
		{
		public:

			/// ********************************************************************************
			/// <summary>
			/// ���ͼ���������ʾ����,��Ҫˢ�²�����ʾ
			/// </summary>
			/// <param name="winID">��ʾ�ؼ�id</param>
			/// <param name="img">ͼ�����</param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void AddIconicVar(HTuple^ winID, HImage^ img);
			/// ********************************************************************************
			/// <summary>
			/// ���ͼ�α�������ʾ����,��Ҫˢ�²�����ʾ
			/// </summary>
			/// <param name="winID"></param>
			/// <param name="obj"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void AddIconicVar(HTuple^ winID, HObject^ obj);

			/// ********************************************************************************
			/// <summary>
			/// ������ʾ������Ϣ
			/// </summary>
			/// <param name="winID">����id</param>
			/// <param name="message"></param>
			/// <param name="row"></param>
			/// <param name="colunm"></param>
			/// <param name="size"></param>
			/// <param name="color"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void ShowText(HTuple^ winID, HTuple^ message, int row, int colunm, int size, HTuple^ color, HTuple^ coordSystem);
			/// ********************************************************************************
			/// <summary>
			/// ������ֶ�����ʾ����
			/// </summary>
			/// <param name="winID"></param>
			/// <param name="message"></param>
			/// <param name="row"></param>
			/// <param name="colunm"></param>
			/// <param name="size"></param>
			/// <param name="color"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void AddText(HTuple^ winID, HTuple^ message, int row, int colunm, int size, HTuple^ color);
			// ********************************************************************************
			/// <summary>
			/// ������ֶ�����ʾ����
			/// </summary>
			/// <param name="winID"></param>
			/// <param name="message"></param>
			/// <param name="row"></param>
			/// <param name="colunm"></param>
			/// <created>linyugang,2018/4/10</created>
			/// <changed>linyugang,2018/4/10</changed>
			// ********************************************************************************
			static void AddText(HTuple^ winID, HTuple^ message, int row, int colunm);			
			/// ********************************************************************************
			/// <summary>
			/// �����ʾ��������Ӧ����������
			/// </summary>
			/// <param name="winID"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void ClearWindowData(HTuple^ winID);
			/// ********************************************************************************
			/// <summary>
			/// �����ʾ�����ڵ���ʾ���� ������roi 
			/// </summary>
			/// <param name="winID"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void ClearEntryList(HTuple^ winID);
			/// ********************************************************************************
			/// <summary>
			/// ˢ����ʾ����-������Ӷ����ʾ��������,���������Զ������÷���
			/// </summary>
			/// <param name="winID"></param>
			/// <param name="showImageOnly"></param>
			/// <param name="isShowText"></param>
			/// <param name="scale"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void Refresh(HTuple^ winID, bool showImageOnly, bool isShowText, double scale);
			/// ********************************************************************************
			/// <summary>
			/// ���洰���ڵ�ԭʼͼ�����
			/// </summary>
			/// <param name="winID"></param>
			/// <param name="path"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void SaveImage(HTuple^  winID, HTuple^  path);
			/// ********************************************************************************
			/// <summary>
			/// �����ڵ�ͼ���Ƿ�Ϊ��
			/// </summary>
			/// <param name="winID"></param>
			/// <returns></returns>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static bool IsEmpty(HTuple^  winID);

			/// ********************************************************************************
			/// <summary>
			/// �޸Ĵ����Ӧ����ʾЧ��
			/// </summary>
			/// <param name="winID"></param>
			/// <param name="mode"></param>
			/// <param name="val"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void ChangeGraphicSettings(HTuple^ winID, HTuple^ mode, HTuple^ val);

			/// ********************************************************************************
			/// <summary>
			/// ������ʾ�ؼ��ڵ�ͼ��ߴ�
			/// </summary>
			/// <param name="winID"></param>
			/// <param name="imageWidth"></param>
			/// <param name="imageHeight"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void SetImageSize(HTuple^ winID, double imageWidth, double imageHeight);
			/// ********************************************************************************
			/// <summary>
			/// ���ñ�����ɫ
			/// </summary>
			/// <param name="winID"></param>
			/// <param name="color"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void SetBackgroundColor(HTuple^ winID, HTuple^ color);
			/// ********************************************************************************
			/// <summary>
			/// ��ʾ����ʮ�ּܵ�
			/// </summary>
			/// <param name="winID"></param>
			/// <param name="isShowCross"></param>
			/// <created>linyugang,2018/3/4</created>
			/// <changed>linyugang,2018/3/4</changed>
			/// ********************************************************************************
			static void ShowHat(HTuple^ winID, bool isShowCross);

			// ********************************************************************************
			/// <summary>
			/// ��ȡ��ǰ���λ����Ϣ
			/// </summary>
			/// <param name="winID">ͼ�񴰿�</param>
			/// <param name="currX">��ǰx����</param>
			/// <param name="currY">��ǰy����</param>
			/// <returns>�������꼰�Ҷ���Ϣ�ȵ��ı�</returns>
			/// <created>linyugang,2018/4/10</created>
			/// <changed>linyugang,2018/4/10</changed>
			// ********************************************************************************
			
			static System::String^ GetPixMessage(HTuple^ winID, [Out]HTuple^%   currX, [Out]HTuple^% currY);
			// ********************************************************************************
			/// <summary>
			/// ��ȡ������ͼ��ָ�����������ڵ�ֱ��ͼ����
			/// </summary>
			/// <param name="winID">����id</param>
			/// <param name="rectangle1">�������� rectangle1</param>
			/// <returns>�Ҷ�ֱ��ͼ ����ֱ��ͼ��Ϣ��Ϊ��htuple</returns>
			/// <created>linyugang,2018/4/11</created>
			/// <changed>linyugang,2018/4/11</changed>
			// ********************************************************************************
			static HTuple^  GetGrayHisto(HTuple^ winID, HTuple^ rectangle1);
		};
	}
}
