

#include <stdio.h>
 
int main()
{
	int n;
	int fact = 1;
	printf("Number: ");
	scanf("%u", &n);

	for (int i = 1; i < n + 1; i++)
	{
		fact = fact * i;
	}

	printf("The factorial of %u is %u\n", n, fact);

	return 0;
}
