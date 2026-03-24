using System;
using System.Collections.Generic;
using System.Text;

namespace matrix_l_zespolone
{
    public static class Operator<T>
    {
        public static Func<T, T, T> Add { get; private set; }
        public static Func<T, T, T> Multiply { get; private set; }

        static Operator()
        {
            if (typeof(T) == typeof(int))
            {
                Add = (x, y) => (T)(object)((int)(object)x + (int)(object)y);
                Multiply = (x, y) => (T)(object)((int)(object)x * (int)(object)y);
            }
            else if (typeof(T) == typeof(double))
            {
                Add = (x, y) => (T)(object)((double)(object)x + (double)(object)y);
                Multiply = (x, y) => (T)(object)((double)(object)x * (double)(object)y);
            }
            else if (typeof(T) == typeof(decimal))
            {
                Add = (x, y) => (T)(object)((decimal)(object)x + (decimal)(object)y);
                Multiply = (x, y) => (T)(object)((decimal)(object)x * (decimal)(object)y);
            }
            else if (typeof(T) == typeof(float))
            {
                Add = (x, y) => (T)(object)((float)(object)x + (float)(object)y);
                Multiply = (x, y) => (T)(object)((float)(object)x * (float)(object)y);
            }
        }

        public static void SetOperators(Func<T, T, T> add, Func<T, T, T> multiply)
        {
            Add = add;
            Multiply = multiply;
        }
    }
}