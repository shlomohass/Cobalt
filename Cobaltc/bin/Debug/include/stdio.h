#ifndef NULL
#define NULL 0
#endif

#ifndef SIZE_T
#define SIZE_T
typedef unsigned int size_t; 
#endif
extern void* malloc(size_t size);
extern free(void* size);
extern puts(const char* str);
extern printf(const char* format);
extern sprintf(char* buff, char* format);
