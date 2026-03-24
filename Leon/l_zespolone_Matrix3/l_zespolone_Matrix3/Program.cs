using l_zespolone_Matrix3;

var m1 = new Matrix<ArWrapper<int>>(1, 2, 3, 4);
var m2 = new Matrix<ArWrapper<int>>(5, 6, 7, 8);
var m3 = m1.Multiply(m2);
PrintM(m3);

var f1 = new Matrix<ArWrapper<float>>(1.5f, 2.5f, 3f, 4f);
var f2 = new Matrix<ArWrapper<float>>(0.5f, 1f, -1.5f, 2f);
var f3 = f1.Multiply(f2);
PrintM(f3);

var c1 = new Matrix<ComplexNumber>(
        new ComplexNumber(1, 2),
        new ComplexNumber(3, 1),
        new ComplexNumber(5, 2),
        new ComplexNumber(7, 4)
    );

var c2 = new Matrix<ComplexNumber>(
        new ComplexNumber(6, 1),
        new ComplexNumber(-3, 7),
        new ComplexNumber(4, -2),
        new ComplexNumber(4, 2)
    );

var c3 = c1.Multiply(c2);
PrintM(c3);

static void PrintM<T>(Matrix<T> matrix) where T : IArithmetic<T>
{
    for (int i = 0; i < 2; i++)
    {
        for (int j = 0; j < 2; j++)
            Console.Write(matrix.Data[i, j] + "\t");
        Console.WriteLine();
    }
    Console.WriteLine();
}