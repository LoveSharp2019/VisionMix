// ���� ifdef ���Ǵ���ʹ�� DLL �������򵥵�
// ��ı�׼�������� DLL �е������ļ��������������϶���� YOGANATIVE_EXPORTS
// ���ű���ġ���ʹ�ô� DLL ��
// �κ�������Ŀ�ϲ�Ӧ����˷��š�������Դ�ļ��а������ļ����κ�������Ŀ���Ὣ
// YOGANATIVE_API ������Ϊ�Ǵ� DLL ����ģ����� DLL ���ô˺궨���
// ������Ϊ�Ǳ������ġ�
#ifdef YOGANATIVE_EXPORTS
#define YOGANATIVE_API __declspec(dllexport)
#else
#define YOGANATIVE_API __declspec(dllimport)
#endif

// �����Ǵ� Yoga.Native.dll ������
class YOGANATIVE_API CYogaNative {
public:
	CYogaNative(void);
	// TODO:  �ڴ�������ķ�����
};

extern YOGANATIVE_API int nYogaNative;

YOGANATIVE_API int fnYogaNative(void);
