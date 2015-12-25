
#include <stdio.h>

unsigned int DataGlobal = 0xDEAFFADE; 	// A member in the .data segment
unsigned int BSSGlobal; // A BSS member
static unsigned int StaticDataGlobal = 0xDEADBEEF; // A static global .data member
static unsigned int StaticBSSGlobal; // A static BSS member


static void StaticFunc(void){
	static unsigned int FuncScopeStatic = 0xDEADFACE;
	printf("I'm a static Func %8X",FuncScopeStatic );
}

void GlobalFunc(void){
	static unsigned int FuncScopeStatic = 0xCAFEC0DE;
	printf("I'm a global Func %8X",FuncScopeStatic);
}

extern void Test(void);
int main(void){

	StaticFunc();
	GlobalFunc();
	Test();
	printf("DataGlobal=%8X, StaticDataGlobal=%8X, BSSGlobal=%8X, StaticBSSGlobal=%8X",DataGlobal,StaticDataGlobal,BSSGlobal,StaticBSSGlobal);
}
