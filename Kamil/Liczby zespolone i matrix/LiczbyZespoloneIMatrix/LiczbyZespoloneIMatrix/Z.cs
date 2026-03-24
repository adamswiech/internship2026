using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace LiczbyZespoloneIMatrix
{
    public class Z : INumber<Z>
    {
        decimal Re { get; set; }
        decimal Im { get; set; }


        public Z(decimal re, decimal im)
        {
            Re = re;
            Im = im;
        }

        public static Z operator +(Z z1, Z z2)
        {
            return new Z(z1.Re + z2.Re, z1.Im + z2.Im);
        }
        public static Z operator *(Z z1, Z z2)
        {
            decimal re = (z1.Re * z2.Re) - (z1.Im * z2.Im);
            decimal im = (z1.Re * z2.Im) + (z1.Im * z2.Re);
            return new Z(re, im);
        }
        #region implementationOfINumber

        public static Z One => throw new NotImplementedException();

        public static int Radix => throw new NotImplementedException();

        public static Z Zero => throw new NotImplementedException();

        public static Z AdditiveIdentity => throw new NotImplementedException();

        public static Z MultiplicativeIdentity => throw new NotImplementedException();



        public static bool operator >(Z left, Z right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(Z left, Z right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <(Z left, Z right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(Z left, Z right)
        {
            throw new NotImplementedException();
        }

        public static Z operator %(Z left, Z right)
        {
            throw new NotImplementedException();
        }

        public static Z operator --(Z value)
        {
            throw new NotImplementedException();
        }

        public static Z operator /(Z left, Z right)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Z? left, Z? right)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Z? left, Z? right)
        {
            throw new NotImplementedException();
        }

        public static Z operator ++(Z value)
        {
            throw new NotImplementedException();
        }

        public static Z operator -(Z left, Z right)
        {
            throw new NotImplementedException();
        }

        public static Z operator -(Z value)
        {
            throw new NotImplementedException();
        }

        public static Z operator +(Z value)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{this.Re} + {this.Im}i";
        }
        public void Print()
        {
            Console.WriteLine(this.ToString());
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Z? other)
        {
            throw new NotImplementedException();
        }

        public static Z Abs(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsCanonical(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsComplexNumber(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsEvenInteger(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsFinite(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsImaginaryNumber(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInfinity(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsInteger(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNaN(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegative(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNegativeInfinity(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsNormal(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsOddInteger(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositive(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsPositiveInfinity(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsRealNumber(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsSubnormal(Z value)
        {
            throw new NotImplementedException();
        }

        public static bool IsZero(Z value)
        {
            throw new NotImplementedException();
        }

        public static Z MaxMagnitude(Z x, Z y)
        {
            throw new NotImplementedException();
        }

        public static Z MaxMagnitudeNumber(Z x, Z y)
        {
            throw new NotImplementedException();
        }

        public static Z MinMagnitude(Z x, Z y)
        {
            throw new NotImplementedException();
        }

        public static Z MinMagnitudeNumber(Z x, Z y)
        {
            throw new NotImplementedException();
        }

        public static Z Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static Z Parse(string s, NumberStyles style, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromChecked<TOther>(TOther value, [MaybeNullWhen(false)] out Z result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromSaturating<TOther>(TOther value, [MaybeNullWhen(false)] out Z result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertFromTruncating<TOther>(TOther value, [MaybeNullWhen(false)] out Z result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToChecked<TOther>(Z value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToSaturating<TOther>(Z value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryConvertToTruncating<TOther>(Z value, [MaybeNullWhen(false)] out TOther result) where TOther : INumberBase<TOther>
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Z result)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out Z result)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Z? other)
        {
            throw new NotImplementedException();
        }

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        public static Z Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Z result)
        {
            throw new NotImplementedException();
        }

        public static Z Parse(string s, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Z result)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
