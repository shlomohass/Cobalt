
#ifndef STRING_H
#define STRING_H

#include <sizet.h>
#define NULL 0

extern int strlen(const char* str);
extern char* strrchr (const char* str, char ch);
extern char* strcpy (char * dest, const char* src);
extern char* strncpy (char* dest, const char* src, size_t len);
extern int strncmp(const char* str1, const char* str2, size_t len);
extern int strcmp(const char* str1, const char* str2);
extern char* strcat(char* dest, const char* dat);
extern char* strncat(char* dest, const char* dat, size_t dlen);
extern void* memcpy(void* src, void* dest, size_t len);
extern int memcmp(void* op1, void* op2, size_t len);
extern void memset(void* dest, char dat, size_t len);
extern void memmove(void* dest, void* src, size_t len);

#endif
