using System;
using System.Collections.Generic;
using System.Text;

namespace matrix_l_zespolone
{
    public class Matrix<T>
    {
        public T[,] Data { get; set; }

        private Func<T, T, T> add;
        private Func<T, T, T> multiply;

        public Matrix(T a, T b, T c, T d)
        {
            Data = new T[2, 2] { { a, b }, { c, d } };

            if (Operator<T>.Add == null || Operator<T>.Multiply == null)
                throw new Exception($"Operators not defined for type {typeof(T)}");

            add = Operator<T>.Add;
            multiply = Operator<T>.Multiply;
        }

        public Matrix<T> Multiply(Matrix<T> m2)
        {
            T a = add(multiply(Data[0, 0], m2.Data[0, 0]),
                      multiply(Data[0, 1], m2.Data[1, 0]));

            T b = add(multiply(Data[0, 0], m2.Data[0, 1]),
                      multiply(Data[0, 1], m2.Data[1, 1]));

            T c = add(multiply(Data[1, 0], m2.Data[0, 0]),
                      multiply(Data[1, 1], m2.Data[1, 0]));

            T d = add(multiply(Data[1, 0], m2.Data[0, 1]),
                      multiply(Data[1, 1], m2.Data[1, 1]));

            return new Matrix<T>(a, b, c, d);
        }
    }
}