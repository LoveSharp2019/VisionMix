// Yoga.Native.cpp : ���� DLL Ӧ�ó���ĵ���������
//

#include "stdafx.h"
#include "Yoga.Native.h"


// ���ǵ���������һ��ʾ��
YOGANATIVE_API int nYogaNative=0;

// ���ǵ���������һ��ʾ����
YOGANATIVE_API int fnYogaNative(void)
{
    return 42;
}

// �����ѵ�����Ĺ��캯����
// �й��ඨ�����Ϣ������� Yoga.Native.h
CYogaNative::CYogaNative()
{
    return;
}
