#include <iostream>
#include "Analyser/Scanner.h"
#include "Parser/Parser.h"
#include "NaturalDeduction/NaturalDeduction.h"

using namespace std;
int main()
{
	string text = 

	// 	"P, Q, (P & Q) -> (R & S) |- S";
	// 	"HCl & NaOH -> NaCl & H2O , C &O2 -> CO2,CO2 & H2O -> H2CO3, HCl, NaOH,O2,C |- H2CO3";
	//	"A , B|- (F -> (A&B)) & (G -> (A&B)&(A&B)) ";
	// 	"F |- G -> F";
	// 	"|- F -> F";
	// 	"|- ((P->Q)->P)->P";
	// 	"|- F -> (G -> F)";
	 //	"F -> G , F -> !G |- !F";
	// 	"!A | !B |- !(A&B)";
	// 	"!(A & B) |- !A | !B";
	// 	" A , B |- A & B ";
	// 	"G->H|- F|G -> F|H";
	// 	"F->G |- !F |G";
	// 	"!F | G |- F -> G";
	// 	"F|-!!F";
	// 	"F->G , !G |- !F";
	// 	"F->(G->H), F, !H |- !G";
	// 	"F->G |- !G -> !F";
	// 	"|- F | !F";
	// 	"!F | !G |- !(F & G)";
	// 	"F -> !F |- !F";
	// 	"(F & !G) -> H, !H,F |- G ";
	// 	"A |- A | B";
	// 	"F |- !!F";
	// 	"!G->!F |- F ->!!G ";
	// 	"|- (G->H)->((!G->!F)->(F->H))";
	// 	"(F&G)->H |- F->(G->H)";
	//	"F->(G->H)|- (F&G)->H";
	//	"F->G |-(F&H)->(G&H)";
	//	"( F | G ) | H |- F | ( G | H )";  
	//	"F & ( G | H ) |- (F & G ) | ( F & H )";
	//	"F -> (G->H) , F, !H |- !G";
	//"F | H, !F |- H";
	//"((A&B)&C)&D , B, C->A, B -> A |- A";
	"(P -> Q) -> (P & Q) |- (Q -> P)&(!P -> Q) ";
	//	"|- ((F->G) & F) -> G";
	//	"P & Q & (!P | !Q) |- !P & !Q & ( P | Q )";
	//	"(P -> Q) & (P -> R) |- P -> (R & Q)";
	//	"P & (P -> (P & Q)) |- !P | !Q | (P & Q)";
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
		cout<<text<<endl<<endl;
		cout<<nd.Result()<<endl<<endl<<endl;	
	}	
	return 0;
	
}
