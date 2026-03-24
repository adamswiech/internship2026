using matrix_l_zespolone;

var m1 = new Matrix<ComplexNumber>(
        new ComplexNumber(1, 2),
        new ComplexNumber(3, 1),
        new ComplexNumber(5, 2),
        new ComplexNumber(7, 4)
    );
var m2 = new Matrix<ComplexNumber>(
        new ComplexNumber(6, 1),
        new ComplexNumber(-3, 7),
        new ComplexNumber(4, -2),
        new ComplexNumber(4, 2)
    );

PrintM(m1);

var m3 = m1.Multiply(m2);
PrintM(m3);

var w1 = new Matrix<float>(1, 3, 2, 7);
var w2 = new Matrix<float>(1, -3, 4, 4);

PrintM(w1);

var w3 = w1.Multiply(w2);
PrintM(w3);


static void PrintM<T>(Matrix<T> matrix)
{
    for (int i = 0; i < 2; i++)
    {
        for (int j = 0; j < 2; j++)
        {
            Console.Write(matrix.Data[i, j] + "\t");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}