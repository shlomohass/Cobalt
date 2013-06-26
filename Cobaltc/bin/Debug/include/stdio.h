#ifndef STDIO_H
#define STDIO_H

#define NULL 0

#include <sizet.h>

extern void abort();
extern void* malloc(size_t size);
extern void free(void* size);
extern void puts(const char* str);
extern void gets(char* str);
extern char* getline();
extern void putchar(char c);
extern void printf(const char* format);
extern void fprintf(int fd, const char* format);
extern void scanf(const char* format);
extern void sprintf(char* buff, char* format);
extern int atoi(char* s);
extern void itoa(char* s,int i);
extern void reverse(char* rv);
extern int abs(int x);

#endif
