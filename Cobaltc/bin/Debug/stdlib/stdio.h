#ifndef NULL
#define NULL 0
#endif

#ifndef SIZE_T
#define SIZE_T
typedef unsigned int size_t; 
#endif


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
	strcat(str, "\n");
	WriteString(str);
}


// This is not how printf is implemented in C, well 
// atleast not how its supposed to be implmeneted...
// This code is VERY dangerous and misuse could and will
// lead to major stack corruption.... 

void printf(const char* format)
{
	int len = strlen(format);
	char* tmpBuffer = " ";
	for(int i = 0; i < len; i++)
	{
		if(format[i] == '%')
		{
			// Lets pray that what we are looking for is on the stack.... 
			i = i + 1;
			if(format[i] == 's')
			{
				WriteString();
			}
			else if (format[i] == 'i')
			{
				WriteString(toString());
			}
			else
			{
				puts("*** INVALID FORMAT!");
			}
		}
		else
		{
			tmpBuffer[0] = format[i];
			WriteString(tmpBuffer);
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
