using System;
using System.Collections.Generic;
using System.Text;

namespace matrix
{
    public interface IComplexNumber
    {
        int Real { get; set; }
        int Imaginary { get; set; }
    }
}
