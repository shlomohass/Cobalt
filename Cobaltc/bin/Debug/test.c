

// This is a comment!

#include <stdio.h>
#include <string.h>
#include <ctype.h>
#include <viper.h>

char* lol;
void main()
{
	printf("Hello %s", "World!");
	puts("\nw00t\n");
	char* buff = malloc(10); // This will get freed (0x0A bytes)
	lol = malloc(20); // This will stay, because LOL is global. (0x
	lol = 0;
}
