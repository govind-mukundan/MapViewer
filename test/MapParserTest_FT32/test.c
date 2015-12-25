#include <stdio.h>

// Some sybols with the same name as in main.c
static unsigned int StaticDataGlobal = 0xACEDC0DE; // A static global .data member
static unsigned int StaticBSSGlobal; // A static BSS member


static void StaticFunc(void){
	static unsigned int FuncScopeStatic = 0xACEDC0DA;
	printf("I'm a static Func %8X",FuncScopeStatic );
	printf("StaticDataGlobal=%8X, StaticBSSGlobal=%8X",StaticDataGlobal,StaticBSSGlobal);
}

void Test(void){
	StaticFunc();
}
