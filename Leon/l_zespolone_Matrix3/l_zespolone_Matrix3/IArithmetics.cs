using System;
using System.Collections.Generic;
using System.Text;

namespace l_zespolone_Matrix3
{
    public interface IArithmetic<T>
    {
        T Add(T other);
        T Multiply(T other);
    }

    public struct ArWrapper<T> : IArithmetic<ArWrapper<T>> where T : struct
    {
        public T Value;
        
        public ArWrapper(T value) => Value = value;

        public ArWrapper<T> Add(ArWrapper<T> other)
        {
            return new ArWrapper<T>((dynamic)Value + other.Value);
        }

        public ArWrapper<T> Multiply(ArWrapper<T> other)
        {
            return new ArWrapper<T>((dynamic)Value * other.Value);
        }

        public override string ToString() => Value.ToString(); 

        public static implicit operator ArWrapper<T>(T value) => new ArWrapper<T>(value);
    }
}
