
typedef unsigned int DWORD;
typedef unsigned char BYTE;
typedef int* IntPtr;
typedef char* string;

void WriteString(DWORD fd, IntPtr str)
{
	asm("ldloc_d %fd");
	asm("ldloc_d %str");
	asm("sysf 1");
}

char* toString(int data)
{
	asm("ldloc_d %data");
	asm("sysf 5");
	asm("ret");
}
