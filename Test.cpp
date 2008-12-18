#include <iostream>
#include "Analyser/Scanner.h"
#include "Parser/Parser.h"
#include "NaturalDeduction/NaturalDeduction.h"



using namespace std;
int main()
{
	string dummy = "";
	string text = 
				"all x  all y all z ((le(x, y) &le(y, z)) -> (!gt(x, z))),\n" +	dummy	+
				"all x all y (le(x, y) -> (gt(y, x) | eq(x, y))),\n"		+	dummy	+
				"all x all y ((gt(y, x) | eq(x, y)) ->le(x, y)),\n"			+	dummy	+
				"all x exists y le(x, y) |- all x le(x,x)";

	//	" all y all x f(x,y) |- f(A,B)";
	//	"! -] x (b(x) & g(x)), all x ( d(x) -> b(x)) |- ! -]x (d(x) & g(x))";
	//	"all x all y all z ((le(x, y) & le(y, z)) ->!gt(x, z)),all x all y (le(x, y) -> (gt(y, x) | eq(x, y))),all x all y ( (gt(y, x) | eq(x, y))->le(x, y)), all x exists y le(x, y), all x all y ( eq(x,y) -> eq(y,x))|- all x le(x, x)";
	//	"all x ( an (x) & ngu(x) -> heo(x)) , all x (nguoi(x) -> an (x) & lam (x) & ngu(x)) , nguoi (A) |- heo(A)";
	//	"A,B, A&B -> all x(f(x)) |- all t f(t) & all y f(y)";
	//  "all x (p(x) ->!q(x)) |- !(exists x (p(x) & q(x)))";
	//	"nguoi(Cesar), all x ( nguoi (x) -> chet(x)) |- chet(Cesar)";
	//	"all x (p(x) ->!q(x)) |- !(exists x (p(x) & q(x)))";
	//	"all x (p(x) ->q(x)) |- (all x !q(x)) ->(all x !p(x))";
	//	"all x p(x) |- all y p(y)";
	//	"exists x exists y f(x,y)|- exists y exists x f(x,y)";
	//	"all x all y f(x,y)|- all y all x f(x,y)";
	//	"all x f(x) & all x g(x) |- all x (f(x) & g(x))";
	//	"! (all x f(x) ) |- exists x !f(x)";
	//	"all x (f(x) & F) |- all x (F & f(x))";
	//	"exists x p(x), all x all y (p(x) -> q(y)) |-  all y q(y)";
	//	"all x (q(x) -> r(x)), exists x (p(x) & q(x)) |-  exists x (p(x) & r(x))";
// 		"all x (p(x) ->q(x)), exists x p(x) |- exists x q(x)";
// 		"all x f(x) |- exists x f(x)";
// 		"all x (p(x) -> q(x)), all x p(x) |- all x q(x)";
// 		"p(t), all x (p(x) -> !q(x)) |- !q(t)";
// 		"all x( p(x) & q(x)) |- all x p(x) & all x q(x) ";


	//	"p(A,B) -> h(A,B) |- p(A,B)|K -> h(A,B)|K";
	//	"A,B , A&B -> F |- F ->A";
	//	"A&B&C|- A";
	//	"A1&A2->A3,A3|A1,A2|A3 |- A3 | H |F |  A | B | A";
	//	"F|F |- F";
	//	"A,B |- A & B";
	//	"A,B,A&B->C |- C";
	//	"(F|G)|H |- F|(G|H)";
	//	"P, Q, (P & Q) -> (R & S) |- S";
	//	"HCl & NaOH -> NaCl & H2O , C &O2 -> CO2,CO2 & H2O -> H2CO3, HCl, NaOH,O2,C |- H2CO3";
	//	"A , B |- (F -> (A&B)) & (G -> (A&B)&(A&B)) ";
	//	"F |- G -> F";
	//	"|- F -> F";
	//	"|- ((P->Q)->P)->P";
	//	"|- F -> (G -> F)";
	//	"F -> G , F -> !G |- !F";
	// 	"!A | !B |- !(A&B)";
	//	"!(A & B) |- !A | !B";
	// 	" A , B |- A & B ";
	//	"G->H  |- F | G -> F|H";
	//	"G|!G,F | !F , !F->G |- !F | G";
	//	"F|!F,G|!G,  F->G |- !F | G";
	//	"!F |F , F -> G |- !F | G ";
	// 	"F |- !!F";
	//	"F->G , !G |- !F";
	// 	"F->(G->H), F, !H |- !G";
	// 	"F->G |- !G -> !F";
	// 	"|- F | !F";
	// 	"!F | !G |- !(F & G)";
	// 	"F -> !F |- !F";
 	// "(F & !G) -> H, !H,F |- G ";
	// 	"A |- A | B";
	// 	"F |- !!F";
	//	"!G->!F |- F ->!!G ";
	// 	"|- (G->H)->((!G->!F)->(F->H))";
	// 	"(F&G)->H |- F->(G->H)";
	//	"F->(G->H)|- (F&G)->H";
	//	"F->G |-(F&H)->(G&H)";
	//	"( F | G ) | H |- F | ( G | H )";  
	//	"F & ( G | H ) |- (F & G ) | ( F & H )";
	//	"F | H, !F |- H";
	//	"((A&B)&C)&D , B, C->A, B -> A |- A";
	//	"(P -> Q) -> (P & Q) |- (!P -> Q) & (Q -> P) ";
	//	"|- ((F->G) & F) -> G";
	//	"P & Q & (!P | !Q) |- !P & !Q & ( P | Q )";
	//	"(P -> Q) & (P -> R) |- P -> (R & Q)";
	//	"P -> (R & Q) |-(P -> Q) & (P -> R) ";
	//	"P & (P -> (P & Q)) |- !P | !Q | (P & Q)";
	//	"!P | !Q | (P & Q) |- P & (P -> (P & Q)) ";
	//	"P -> (R & Q) |- (P -> Q) & (P -> R)";
	//	"!(P | (!P & Q)) |- !P & !Q";
	//	"!P & !Q|-!(P | (!P & Q))  ";
	//	"( P & Q) -> R |-  (!P | ! Q) | R";
	//	"!P | ! Q | R |-( P & Q) -> R  ";
	//	"A,B , A&B -> F  |- G | F ";
	//	"F -> G |- F|H -> G|H";
		"P | !P , Q  | !Q |- (P&Q)|(P&!Q)|(!P&Q)|(!P&!Q)";

	Scanner* scanner = new Scanner(text);
	Parser* p = new Parser(scanner);
	p->parse();
	if ( p->s != "")
	{
		cout<< p->s <<endl;
		
	}
	else
	{
		//p->data.print();
		NaturalDeduction nd(p->data);
		//nd.insertLEMs();
		if(nd.ProveIt())
		{
			cout<<text<<endl<<endl;
			cout<<nd.Result()<<endl<<endl<<endl;
		}
		else 
		{
			
			cout<<"Chiu chit! Giai? khong ra dau"<<endl<<endl;

		}
	}	
	return 0;
	
}
