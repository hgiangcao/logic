
#ifndef NDUtils
#define NDUtils

#include <sstream>
#include <string>
using namespace std;

template <class T>
inline std::string ToString (const T& t)
{
	std::stringstream ss;
	ss << t;
	return ss.str();
}

template <class T>
inline std::string ToStringX (const T& t,int width)
{
	std::stringstream ss;
	ss.setf(ios_base::left,ios_base::adjustfield);
	ss.width(width);
	ss << t;
	return ss.str();
}

struct pLine 
{
	pLine(int index ,int line, int indent, string assump, string content, string rule = "", int first = -1, int second = -1 , int third = -1)
	{
		m_index = index;
		m_line = line;
		m_indent = indent;
		m_assumption = assump;
		m_content = content;
		m_rule = rule;
		m_first=  first;
		m_second = second;
		m_third = third;
		m_isFixed = false;
		m_isPrefix = false;
		m_extra = "";
	}
	string ToString(int max = 0)
	{
		string s = "";
		//s +=  ToStringX(ToString(m_line)+".",4) ;
		//s += ToStringX("",m_indent * 4) ;
		//s += m_assumption ;
		//s += ToStringX(m_content,max) ;
		//s += m_rule;
		if (m_first > -1)
		{
			s += " " + ToString(m_first);
		}
		if (m_second > -1)
		{
			s += "," + ToString(m_second);
		}
		return s;
	}
	int Assumption() const
	{
		return m_assumption == "" ? 0 : 1 ;
	}
	int m_line;
	int m_indent;
	string m_assumption;
	string m_content;
	string m_rule;
	string m_extra;
	int m_first;
	int m_second;
	int m_third;
	int m_index;
	bool m_isFixed;
	bool m_isPrefix;

};
inline std::string pLine2Str(const pLine& p)
{
	string s = "";
#if _DEBUG
	s += ToStringX(ToString(p.m_line)+".",4) ;
#else
	s += ToStringX(" " +ToString(p.m_line)+".\t",0) ;
#endif
	s += ToStringX("",p.m_indent * 5) ;
	s += p.m_assumption + p.m_content;
	return s;
}

inline std::string pLine2Str(const pLine& p, int max)
{
	string s = "";	

#if _DEBUG
	s = ToStringX(pLine2Str(p),max) + " ";
#else
	s = ToString(pLine2Str(p)) + "#";
#endif
	s += p.m_rule ;
	if (p.m_third > -1)
	{
		s += " " + ToString(p.m_third) + ","+ ToString(p.m_second) + "," + ToString(p.m_first);
	}
	else if (p.m_second > -1)
	{
		if (p.m_second < p.m_first)
		{
			s += " " + ToString(p.m_second) + "," + ToString(p.m_first);	
		}
		else
		{
			s += " " + ToString(p.m_first) + "," + ToString(p.m_second);	
		}
		
	}

	else if (p.m_first > -1)
	{
		s += " " + ToString(p.m_first);
	}
	
	return s + p.m_extra;
}



#endif