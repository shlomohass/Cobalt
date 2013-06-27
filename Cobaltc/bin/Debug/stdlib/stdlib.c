
typedef unsigned int size_t; 

#define NULL 0

#include "viper.h"
#include "stdio.h"
#include "string.h"
#include "ctype.h"

void abort()
{
	asm("push ptr 0");
	asm("end");
}
void* malloc(size_t size)
{
	asm("ldloc_d %size");
	asm("heapalloc");
	asm("ret");
	return 'a';
}
void* calloc(size_t elem, size_t elemsize)
{
	return malloc(elem * elemsize);
}
void free(void* size)
{
	asm("ldloc_d %size");
	asm("heapfree");
	asm("ret");
	return NULL;
}

int atou(char* s) 
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

int atoi(char* s) 
{
	int sign = 0;
	
	while(isspace(s[0]))
		s++;
	if(s[0] == '-')
	{
		sign = 1;
		s++;
	}	
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
	if(sign) {
		final = ~final + 1;	
	}
	return final;
}

int abs(int x)
{
	if((x & 0x80000000) != 0) 
		return ~x + 1;
	else {
		return x;
	}
}
void itoa(char* s,int i) 
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
