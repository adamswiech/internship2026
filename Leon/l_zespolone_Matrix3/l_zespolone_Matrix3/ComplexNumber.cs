using System;
using System.Collections.Generic;
using System.Text;

namespace l_zespolone_Matrix3
{
    public class ComplexNumber : IArithmetic<ComplexNumber>
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
            => new ComplexNumber(a.Real * b.Real - a.Imag * b.Imag,
                                 a.Real * b.Imag + a.Imag * b.Real);

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.Real + b.Real, a.Imag + b.Imag);
        
        public ComplexNumber Add(ComplexNumber other) => this + other;
        public ComplexNumber Multiply(ComplexNumber other) => this * other;
    }
}
