#define OUT_OF_MEMORY 		0x01
#define ILL_MEMORY_ACCESS 	0x02
#define ILL_MEMORY_WRITE  	0x03
#define ILL_MEMORY_FREE   	0x04
#define ILL_INSTRUCTION		0x05
typedef unsigned int Exception;
extern char* decodeException(Exception ex);

