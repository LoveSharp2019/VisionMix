#pragma once

using namespace System::Runtime::InteropServices;
using namespace System::Runtime::Serialization;
using namespace System;
namespace Yoga
{
	namespace Wrapper 
	{
		[Serializable]
		public ref struct DynThresholdParam
		{
			String^ lightDark;
			Double offset;
			Int32 maskWidth;
			Int32 maskHeight;
			Double closingRadius;
			Double openingRadius;
			Double leastArea;
		};

	}
}