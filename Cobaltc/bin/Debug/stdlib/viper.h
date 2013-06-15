
typedef unsigned int DWORD;
typedef unsigned char BYTE;
typedef int* IntPtr;
typedef char* string;

void WriteString(DWORD fd, IntPtr str)
{
	asm("push dword 1");
	asm("push ptr %str");
	asm("dload");
	asm("sysf 1");
}

