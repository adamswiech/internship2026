using System;
using System.Collections.Generic;
using System.Text;

namespace matrix_l_zespolone
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imag { get; set; }

        public ComplexNumber(double real, double imag)
        {
            Real = real;
            Imag = imag;
        }

        static ComplexNumber()
        {
            Operator<ComplexNumber>.SetOperators(
                (x, y) => x + y,
                (x, y) => x * y
            );
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.Real + b.Real, a.Imag + b.Imag);

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            double real = (a.Real * b.Real) - (a.Imag * b.Imag);
            double imag = (a.Real * b.Imag) + (a.Imag * b.Real);
            return new ComplexNumber(real, imag);
        }

        public override string ToString()
        {
            return $"{Real}{(Imag >= 0 ? "+" : "")}{Imag}i";
        }
    }
}