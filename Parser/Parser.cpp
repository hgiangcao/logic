// Parser.cpp : Defines the entry point for the console application.
//

#include "Parser.h"

Parser::Parser(Scanner* s)
{
	scanner = s;
	lookAheadToken = Token(LGC_NIL);
}

void Parser::match(TokenKind tokenKind)
{
	if((TokenKind)lookAheadToken == (TokenKind)tokenKind)
		lookAheadToken = scanner->nextToken();
	else
		cout<<"Unexpected Token"<<endl;
}

bool Parser::check(TokenKind tokenKind)
{
	return (TokenKind)lookAheadToken == (TokenKind)tokenKind;
}

Token Parser::getLookAheadToken()
{
	return lookAheadToken;	
}

//<tail>			::= 	',' <source>  	|  <binary-operator><source>  	|
void parseFormula()
{
	if(check(LGC_COMMA)){
		match(LGC_COMMA);
		parseSource();
	}
	else if (check(LGC_AND) || check(LGC_OR) || check(LGC_MODUS)){
		parseBOperator();
		parseSource();
	}
	else 
		parseEmptyTail();
}

//<source>		::= 	<formula> <tail>  
void parseSource()
{
	parseFormula();
	parseTail();
}

void Parser::parse()
{
	parseSource();
}






//<formula>		::=   con	|  'not' <formula>  |  <quantifier> <formula> 	| con <argument-list>	| var <argument-list>	|'(' <source> ')'
void parseFormula()
{
	
}
