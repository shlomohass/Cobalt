#include "viper.h"

#ifndef NULL
#define NULL 0
#endif

#ifndef SIZE_T
#define SIZE_T
typedef unsigned int size_t; 
#endif

void abort()
{
	asm("push ptr 0");
	asm("end");
}
void* malloc(size_t size)
{
	asm("push ptr %size");
	asm("dload");
	asm("heapalloc");
	asm("ret");
	return NULL;
}
void free(void* size)
{
	asm("push ptr %size");
	asm("dload");
	asm("heapfree");
	asm("ret");
	return NULL;
}

void puts(const char* str)
{
	char* nb = (char*)malloc(strlen(str) + 2);
	strcat(nb, str);
	strcat(nb, "\n");
	strcat(nb, "\0");
	WriteString(1, nb);
	free(nb);
}

void gets()
{	
	asm("push dword 0");
	asm("sysf 2");
	asm("ret");
	return 0;
}
void putchar(char c)
{
	char* tmpBuffer = "\0\0";
	tmpBuffer[0] = c;
	puts(tmpBuffer);
}

// This is not how printf is implemented in C, well 
// atleast not how its supposed to be implmeneted...
// This code is VERY dangerous and misuse could and will
// lead to major stack corruption.... 

void printf(const char* format)
{
	fprintf(1, format);
}

void fprintf(int fd, const char* format)
{
	int len = strlen(format);
	char* tmpBuffer = " ";
	for(int i = 0; i < len; i++)
	{
		if(format[i] == '%')
		{
			// Lets pray that what we are looking for is on the stack.... 2147483647. 
			i = i + 1;
			if(format[i] == 's')
			{
				WriteString(fd); 
			}
			else if (format[i] == 'u')
			{
				WriteString(fd, toString());
			}
			else
			{
				puts("*** INVALID FORMAT!");
			}
		}
		else
		{
			tmpBuffer[0] = format[i];
			WriteString(fd, tmpBuffer);
		}
	}
}

void mcpy2(char* src, int len, char* dest)
{
	memcpy(dest, src, len);
}
void scanf(const char* format)
{
	
	int len = strlen(format);
	char* tmpBuffer = " ";
	for(int i = 0; i < len; i++)
	{
		if(format[i] == '%')
		{
			// Lets pray that what we are looking for is on the stack.... 2147483647. 
			i = i + 1;
			if(format[i] == 's')
			{
				mcpy2("Hello World!", strlen("Hello World!"));
			}
			else
			{
				puts("*** INVALID FORMAT!");
			}
		}
		else
		{
			tmpBuffer[0] = format[i];
			WriteString(fd, tmpBuffer);
		}
	}
}


void sprintf(char* buff, char* format)
{
	int len = strlen(format);
	char* tmpBuffer = " ";
	for(int i = 0; i < len; i = i + 1)
	{
		if(format[i] == '%')
		{
			// Lets pray that what we are looking for is on the stack.... 
			i = i + 1;
			if(format[i] == 's')
			{
				strcat(buff);
			}
			else if (format[i] == 'i')
			{
				
				strcat(buff,toString());
			}
			else
			{
				puts("*** INVALID FORMAT!");
			}
		}
		else
		{
			tmpBuffer[0] = format[i];
			strcat(buff, tmpBuffer);
		}
	}
}


int atoi(char* s) 
{
	int final = 0;
	int mul = 1;
	int len = strlen(s);
	reverse(s);
	for(int i = 0; i < len; i++)
	{
		char b = s[i];
		int RealDigit = (int)b - 48;
		final = (RealDigit * mul) + final;
		mul = mul * 10;
	}
	return final;
}

void itoa(char* s, const int i) 
{
	int d;
	int p = 0;
	do
	{
		d = i % 10;
		i = (i -d) / 10;
		s[p] = (char)d + 48;
		p = p + 1;
	} while(i > 0)
	s[p] = 0;
	reverse(s);
}

void reverse(char* rv)
{
	char* tmpbuff = malloc(strlen(rv));
	memcpy(rv, tmpbuff, strlen(rv));
	int len = strlen(rv);
	for(int i = 0; i < len; i++) {
		rv[i] = tmpbuff[(len - 1) - i];
	}
	
}
