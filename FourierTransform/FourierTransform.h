/*
Fast Fourier Transformation
====================================================
Coded by Miroslav Voinarovsky, 2002
This source is freeware.
*/

#ifndef FFT_H_
#define FFT_H_

struct Complex;
struct ShortComplex;

/*
Fast Fourier Transformation
x: x - array of items
N: N - number of items in array
complement: false - normal (direct) transformation, true - reverse transformation
*/
extern void universal_fft(ShortComplex *x, int N, bool complement);

struct ShortComplex
{
	double re, im;
	inline void operator=(const Complex &y);
};

struct Complex
{
	long double re, im;
	inline void operator= (const Complex &y);
	inline void operator= (const ShortComplex &y);
};


inline void ShortComplex::operator=(const Complex &y)    { re = (double)y.re; im = (double)y.im; }
inline void Complex::operator= (const Complex &y)       { re = y.re; im = y.im; }
inline void Complex::operator= (const ShortComplex &y)  { re = y.re; im = y.im; }

#endif
