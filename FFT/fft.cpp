#include "fft.h"
#include "Complex.h"

fft::fft(int SamplesNumber)
{
}


fft::~fft()
{
}

void fft::complex_mul(ShortComplex *z, const ShortComplex *z1, const Complex *z2)
{
	z->re = (double)(z1->re * z2->re - z1->im * z2->im);
	z->im = (double)(z1->re * z2->im + z1->im * z2->re);
}

void fft::createWstore(unsigned int Nmax)
{
	//unsigned int N, Skew, Skew2;
	//ShortComplex *Warray, *WstoreEnd;
	//Complex WN, *pWN;

	//Skew2 = Nmax >> 1;
	//Wstore = new ShortComplex[Skew2];
	//WstoreEnd = Wstore + Skew2;
	//Wstore[0].re = 1.0;
	//Wstore[0].im = 0.0;

	//for (N = 4, pWN = W2n + 1, Skew = Skew2 >> 1; N <= Nmax; N += N, pWN++, Skew2 = Skew, Skew >>= 1)
	//{
	//	//WN = W(1, N) = exp(-2*pi*j/N)
	//	WN = *pWN;
	//	for (Warray = Wstore; Warray < WstoreEnd; Warray += Skew2)
	//		complex_mul(Warray + Skew, Warray, &WN);
	//}
	//
}


inline void operator+=(ShortComplex &x, const Complex &y)       { x.re += (double)y.re; x.im += (double)y.im; }
inline void operator-=(ShortComplex &x, const Complex &y)       { x.re -= (double)y.re; x.im -= (double)y.im; }
inline void operator*=(Complex &x, const Complex &y)			{ temp = x.re; x.re = temp * y.re - x.im * y.im; x.im = temp * y.im + x.im * y.re; }
inline void operator*=(Complex &x, const ShortComplex &y)		{ temp = x.re; x.re = temp * y.re - x.im * y.im; x.im = temp * y.im + x.im * y.re; }
inline void operator/=(ShortComplex &x, double div)			    { x.re /= div; x.im /= div; }
inline void operator/=(Complex &x, double div)					{ x.re /= div; x.im /= div; }
inline void operator*=(ShortComplex&x, const ShortComplex &y)	{ double temp = x.re; x.re = temp * y.re - x.im * y.im; x.im = temp * y.im + x.im * y.re; }