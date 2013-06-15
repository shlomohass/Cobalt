
#ifndef string_h__
	#define string_h__

#ifndef NULL
	#define NULL 0
#endif

#ifndef SIZE_T
	#define SIZE_T
	typedef unsigned int size_t; 
#endif

int strlen(const char* str)
{
	int i = 0; 
	while(str[i] != 0)
	{
		i = i + 1;
	}
	return i;
}

char* strrchr (const char* str, char ch)
{
	int dlen = strlen(str);
	for(int i = 0; i < dlen; i++)
	{
		if((char*)str[i] == ch)
		{
			return (char*)(str + i);
		}
	}
	return NULL;
}

char* strcpy (char * dest, const char* src)
{
	size_t len = strlen(src);
	for(int i = 0; i < len; i = i + 1)
	{
		dest[i] = src[i];
	}
	return dest;
}

char* strncpy (char* dest, const char* src, size_t len)
{
	for(int i = 0; i < len && dest[i] != NULL; i++)
	{
		dest[i] = src[i];
	}
	return dest;
}

int strncmp(const char* str1, const char* str2, size_t len)
{
	for(int i = 0; i < len; i++)
	{
		if(str1[i] != str2[i]) {
			return -1;
		}
	}
	return 0;
}

int strcmp(const char* str1, const char* str2)
{
	if(strlen(str1) > strlen(str2))
	{
		return strncmp(str1, str2, strlen(str1));
	}
	else
	{
		return strncmp(str1, str2, strlen(str2));
	}
}

char* strcat(char* dest, const char* dat)
{
	int dlen = strlen(dat);
	strncat(dest, dat, dlen);
	return dest;
}

char* strncat(char* dest, const char* dat, size_t dlen)
{
	int pos = 0;
	while(dest[pos] != 0)
	{
		pos = pos + 1;
	}
	char* new_str = (char*)(dest + pos);
	for(int i = 0; i < dlen; i = i + 1)
	{
		new_str[i] = dat[i];
	}
	return dest;
}

void* memcpy(void* src, void* dest, size_t len)
{
	char* mem1 = (char*)src;
	char* mem2 = (char*)dest;
	for(int i = 0; i < len; i = i + 1)
	{
		mem2[i] = mem1[i];
	}
} 

int memcmp(void* op1, void* op2, size_t len)
{
	char* mem1 = (char*)op1;
	char* mem2 = (char*)op2;
	for(int i = 0; i < len; i = i + 1)
	{
		if(mem1[i] != mem2[i])
		{
			return 0;
		}
	}
	return 1;
}

void memset(void* dest, char dat, size_t len)
{
	char* mem = (char*)dest;
	for(int i = 0; i < len; i = i + 1)
	{
		mem[i] = dat;
	}
}

void memmove(void* dest, void* src, size_t len)
{
	char* tmp_buff = (char*)malloc(len);
	memcpy(tmp_buff, src, len);
	memcpy(dest, tmp_buff, len);
	free(tmp_buff);
}


#endif
