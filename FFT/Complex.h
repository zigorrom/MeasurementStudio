#pragma once
struct Complex;
struct ShortComplex;

struct Complex
{
	long double re, im;
	inline void operator= (const Complex &y);
	inline void operator= (const ShortComplex &y);
};

struct ShortComplex
{
	double re, im;
	inline void operator=(const Complex &y);
};

