
typedef unsigned int DWORD;
typedef unsigned char BYTE;
typedef int* IntPtr;
typedef char* string;

void WriteString(DWORD fd, IntPtr str)
{
	asm("push ptr %fd");
	asm("dload");
	asm("push ptr %str");
	asm("dload");
	asm("sysf 1");
}

char* toString(int data)
{
	asm("push ptr %data");
	asm("dload");
	asm("sysf 5");
	asm("ret");
}
