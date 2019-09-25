#include "IjwLibrary.h"
#include <iostream>

using namespace System;

void Interop::IjwLibrary::IjwClass::Greet()
{
	printf("Hello from an IJW assembly!\n");
	Console::WriteLine("CLR version: " + Environment::Version->ToString());
}