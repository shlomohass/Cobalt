#include <stdio.h>

int main()
{
	int n;
	int t1=0;
	int t2=1;
	int display=0;
	printf("Enter number of terms: ");
	scanf("%u",&n);
	printf("The Fibonacci Series is: \n%u\n%u\n", t1, t2); 
	for(int i = 2; i < n; i++)
	{
		display=t1+t2;
		t1=t2;
		t2=display;
		printf("%u\n",display);
	}
	getline();
	return 0;
}
