#include <stdio.h> 
#include <string.h> 
#include <ctype.h>

static unsigned int sentences;
static unsigned int words;
static unsigned int letters;

int main()
{
	words = 1;
	char* paragraph = (char*)malloc(150);
	printf("Enter a paragraph: ");
	scanf("%s", paragraph );
	letters = strlen(paragraph );
	for(int i = 0; i < letters; i++) {
		if(ispunct(paragraph[i]) == 1) {
			sentences++;
		} else if (isspace(paragraph[i]) == 1) {
			words++;
			while(isspace(paragraph[i + 1]) == 1) {
				i++; // Eat whitespaces!
			}
		}
	}
	printf("The paragraph %s has:\n\n", paragraph);
	printf("%u Words\n", words);
	printf("%u Letters\n", letters);
	printf("%u Sentences\n", sentences);
	return 0;
}

