#include "stdafx.h"
#include "UtilManaged.h"
#include"Util.h"

Yoga::Wrapper::UtilManaged::UtilManaged()
{

	// �ӳ�Ա��������һ��ί��  
	this->nativeCallback = gcnew EventDelegate(this, &UtilManaged::Callback);

	// ��֤ί�в��ᱻ�ڴ��ƶ����������յ�  
	this->delegateHandle = GCHandle::Alloc(this->nativeCallback);

	// ת��Ϊ����ָ��ע��  
	IntPtr ptr = Marshal::GetFunctionPointerForDelegate(this->nativeCallback);
	Util::RegisterCallback(static_cast<EventCallback>(ptr.ToPointer()));
}

Yoga::Wrapper::UtilManaged::~UtilManaged()
{
	// �ͷ�ί�о��  
	if (this->delegateHandle.IsAllocated)
		this->delegateHandle.Free();
}

void Yoga::Wrapper::UtilManaged::Callback(String^ msg)
{
	if (NetCallback!= nullptr)
	{
		NetCallback(msg);
	}
	//Console::WriteLine("�й����������: {0}", i);
	//throw gcnew System::NotImplementedException();
}
