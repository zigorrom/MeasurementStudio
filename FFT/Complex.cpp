#include "Complex.h"

inline void ShortComplex::operator=(const Complex &y) { re = (double)y.re; im = (double)y.im; }
inline void Complex::operator=(const Complex &y) { re = y.re; im = y.im; }
inline void Complex::operator=(const ShortComplex &y) { re = y.re; im = y.im; }