#include <iostream>
#include "Analyser/Scanner.h"
#include "Parser/Parser.h"
#include "NaturalDeduction/NaturalDeduction.h"

using namespace std;
int main()
{



	//string text = "P, Q, (P & Q) -> (R & S) |- S";
	//string text = "HCl & NaOH -> NaCl & H2O , C &O2 -> CO2,CO2 & H2O -> H2CO3, HCl, NaOH,O2,C |- H2CO3";
	//string text = "A, B |- F -> A & B";
	//string text = "F |- G -> F";
	string text = "|- F -> F";
	//string text = "|- ((P->Q)->P)->P";
	Scanner* scanner = new Scanner(text);
	Parser* p = new Parser(scanner);
	p->parse();
	if ( p->s != "")
	{
		cout<< p->s <<endl;
	}
	else
	{
		
		NaturalDeduction nd(p->data);
		//p->data.print();
		nd.ProveIt();
		cout<<nd.Result()<<endl<<endl<<endl;	
	}	
	return 0;
	
}
