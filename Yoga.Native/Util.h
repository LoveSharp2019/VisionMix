#pragma once
#include "stdafx.h"  
//#include <assert.h>
#include<QTextCodec>
#define   GBK(s)   QTextCodec::codecForName("GBK")->toUnicode(s)
extern "C" __declspec(dllexport) typedef void(__stdcall* EventCallback)(char*);
class __declspec(dllexport) Util
{
public:
	Util();
	~Util();
	/// ע��ص�����  
	static void RegisterCallback(EventCallback callback);
	
	/// ����ע��Ļص�����  
	static void Invoke(char* msg);
private:
	static EventCallback ms_callback;
};

