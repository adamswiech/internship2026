using System;
using System.Collections.Generic;
using System.Text;

namespace l_zespolone_Matrix2
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

        public override string ToString()
        {
            string sign = Imag >= 0 ? "+" : "-";
            double imagAbs = Math.Abs(Imag);
            return $"{Real} {sign} {imagAbs}i";
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            double realPart = a.Real * b.Real - a.Imag * b.Imag;
            double imagPart = a.Real * b.Imag + a.Imag * b.Real;
            return new ComplexNumber(realPart, imagPart);
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.Real + b.Real, a.Imag + b.Imag);
        }
    }
}
