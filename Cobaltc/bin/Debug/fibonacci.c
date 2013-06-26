#include <stdio.h>

int main()
{
	int n = 10;
	int t1=0;
	int t2=1;
	int display=0;
	printf("The Fibonacci Series is: \n%u\n%u\n", t1, t2); 
	for(int i = 2; i < n; i++)
	{
		display=t1+t2;
		t1=t2;
		t2=display;
		printf("%u\n",display);
	}
	return 0;
}
