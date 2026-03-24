using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace LiczbyZespoloneIMatrix
{
    public class Matrix<T> : IEnumerable<T> where T : INumber<T>
    {
        T[,] M;
        public Matrix()
        {
            M = new T[2, 2];
        }
        public Matrix(T[,] m)
        {
            M = m;
        }

        public T this[int column, int row] {
            get => M[column, row];
            set => M[column, row] = value;
        }

        public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
        {
            T[,] res = new T[2, 2];

            res[0, 0] = (a[0, 0] * b[0, 0]) + (a[1, 0] * b[0, 1]);
            res[0, 1] = (a[0, 0] * b[0, 1]) + (a[0, 1] * b[1, 1]);
            res[1, 0] = (a[1, 0] * b[0, 0]) + (a[1, 1] * b[1, 0]);
            res[1, 1] = (a[1, 0] * b[0, 1]) + (a[1, 1] * b[1, 1]);

            Matrix<T> resMatrix = new Matrix<T>(res);
            return resMatrix;
        }
        public void Print()
        {
            Console.WriteLine($"| {M[0, 0].ToString()}   {M[0, 1].ToString()} |");
            Console.WriteLine($"| {M[1, 0].ToString()}   {M[1, 1].ToString()} |");
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                    yield return this[i, j];
            }

        }
       IEnumerator IEnumerable.GetEnumerator () => GetEnumerator();
    }

}
