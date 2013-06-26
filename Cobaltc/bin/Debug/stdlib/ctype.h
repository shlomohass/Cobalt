
int isalnum (char c)
{
	int tmp = (int)c;
	if(tmp > 47 && tmp < 58) {
		return 1;
	} else if (tmp > 64 && tmp < 91) {
		return 1;
	} else if (tmp > 96 && tmp < 123) {
		return 1;
	}
	return 0;
}

int isalpha (char c)
{
	int tmp = (int)c;
	if (tmp > 64 && tmp < 91) {
		return 1;
	} else if (tmp > 96 && tmp < 123) {
		return 1;
	}
	return 0;
}

int islower (char c)
{
	int tmp = (int)c;
	if (tmp > 96 && tmp < 123) {
		return 1;
	}
	return 0;
}

int isupper (char c)
{
	int tmp = (int)c;
	if (tmp > 64 && tmp < 91) {
		return 1;
	}
	return 0;
}

int isdigit (char c)
{
	int tmp = (int)c;
	if(tmp > 47 && tmp < 58) {
		return 1;
	}
	return 0;
}
int ispunct (char c)
{
	int tmp = (int)c;
	if(tmp > 0x20 && tmp < 0x30) {
		return 1;
	} else if(tmp > 0x39 && tmp < 0x41) {
		return 1;
	} else if(tmp > 0x5A && tmp < 0x61) {
		return 1;
	} else if(tmp > 0x7a && tmp < 0x7F) {
		return 1;
	}
	return 0;
}

int isxdigit (char c)
{
	int tmp = (int)c;
	if(tmp > 47 && tmp < 58) {
		return 1;
	} else if(tmp > 0x40 && tmp < 0x47) {
		return 1;
	}
	return 0;
}

int isblack(char c)
{
	int tmp = (int)c;
	if(tmp == 0x09) {
		return 1;
	} else if(tmp == 0x20) {
		return 1;
	}
	return 0;
}

int isspace(char c)
{
	int tmp = (int)c;
	if(tmp > 0x09 && tmp < 0x0F) {
		return 1;
	} else if(tmp == 0x20) {
		return 1;
	}
	return 0;
}

int isgraph(char c)
{
	int tmp = (int)c;
	if(tmp > 0x20 && tmp < 0x7F) {
		return 1;
	}
	return 0;
}

int isprint(char c)
{
	int tmp = (int)c;
	if(tmp > 0x19 && tmp < 0x7F) {
		return 1;
	}
	return 0;
}

char tolower(char c)
{
	int tmp = (int)c;
	if(isupper(c))
		return (char)((tmp - 65) + 97);
	return c;
}

char toupper(char c)
{
	if(islower(c))
		return (char)((c - 97) + 65);
	return c;
}
