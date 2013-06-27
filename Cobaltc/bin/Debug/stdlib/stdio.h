#include "viper.h"



void puts(const char* str)
{
	char* nb = (char*)malloc(strlen(str) + 2);
	strcat(nb, str);
	strcat(nb, "\n");
	strcat(nb, "\0");
	WriteString(1, nb);
	free(nb);
}

char* getline()
{	
	asm("push dword 0");
	asm("sysf 2");
	asm("ret");
	return 0;
}

void gets(char* buff)
{
	char* dat = getline();
	memcpy(dat, buff, strlen(dat));
	free(dat);
}

void putchar(char c)
{
	char* tmpBuffer = "\0\0";
	tmpBuffer[0] = c;
	WriteString(1, tmpBuffer);
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
			putchar(format[i]);
		}
	}
}


void scanf(const char* format, void* buff)
{
	
	int len = strlen(format);
	char* tmpBuffer = " ";
	for(int i = 0; i < len; i++)
	{
		if(format[i] == '%')
		{
			if(format[i + 1] == 's')
			{
				gets(buff);
			}
			else if (format[i + 1] == 'u') {
				int* ptr = (int*)buff;
				*ptr = atou(getline());
			}
			else if (format[i + 1] == 'd') {
				int* ptr = (int*)buff;
				*ptr = atoi(getline());
			}
			else
			{
				puts("*** INVALID FORMAT!");
			}
			i++;
		}
		else
		{
			putchar(format[i]);
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



