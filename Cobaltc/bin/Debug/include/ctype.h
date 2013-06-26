#ifndef CTYPE_H
#define CTYPE_H

extern int isalnum (char c);
extern int isalpha (char c);
extern int islower (char c);
extern int isupper (char c);
extern int isdigit (char c);
extern int ispunct (char c);
extern int isxdigit (char c);
extern int isblack(char c);
extern int isspace(char c);
extern int isgraph(char c);
extern int isprint(char c);
extern char tolower(char c);
extern char toupper(char c);

#endif