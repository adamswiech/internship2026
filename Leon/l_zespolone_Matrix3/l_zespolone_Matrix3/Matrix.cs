using System;
using System.Collections.Generic;
using System.Text;

namespace l_zespolone_Matrix3
{
    public class Matrix<T> where T : IArithmetic<T>
    {
        private T[,] _data;

        public T[,] Data => _data;

        public Matrix(T a, T b, T c, T d)
        {
            _data = new T[2, 2] { { a, b }, { c, d } };
        }

        public Matrix<T> Multiply(Matrix<T> other)
        {
            T a = _data[0, 0].Multiply(other._data[0, 0]).Add(_data[0, 1].Multiply(other._data[1, 0]));
            T b = _data[0, 0].Multiply(other._data[0, 1]).Add(_data[0, 1].Multiply(other._data[1, 1]));
            T c = _data[1, 0].Multiply(other._data[0, 0]).Add(_data[1, 1].Multiply(other._data[1, 0]));
            T d = _data[1, 0].Multiply(other._data[0, 1]).Add(_data[1, 1].Multiply(other._data[1, 1]));

            return new Matrix<T>(a, b, c, d);
        }
    }
}
