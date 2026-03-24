using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace l_zespolone_Matrix2
{
    public class Matrix<T>
    {
        public T[,] Data { get; private set; }

        public Matrix(T a, T b, T c, T d)
        {
            Data = new T[2, 2] { { a, b }, { c, d } };
        }

        public Matrix<T> Multiply(Matrix<T> other)
        {
            dynamic A = Data[0, 0], B = Data[0, 1], C = Data[1, 0], D = Data[1, 1];
            dynamic E = other.Data[0, 0], F = other.Data[0, 1], G = other.Data[1, 0], H = other.Data[1, 1];

            T a = A * E + B * G;
            T b = A * F + B * H;
            T c = C * E + D * G;
            T d = C * F + D * H;

            return new Matrix<T>(a, b, c, d);
        }
    }
}
