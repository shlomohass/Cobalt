#define OUT_OF_MEMORY 0x01
#define INVALID_MEMORY_ACCESS 0x02


typedef unsigned int DWORD;
typedef unsigned char BYTE;
typedef int* IntPtr;
extern void WriteString(DWORD fd, IntPtr str);
extern char* toString(int data);
