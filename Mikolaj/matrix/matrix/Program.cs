using matrix;

int dimension = 2;

Matrix<int> m1 = new(dimension);
Matrix<int> m2 = new(dimension);

Matrix<ComplexNumber> m3 = new Matrix<ComplexNumber>(dimension);
Matrix<ComplexNumber> m4 = new Matrix<ComplexNumber>(dimension);

Matrix<float> m5 = new(dimension);
Matrix<float> m6 = new(dimension);

m1[0, 0] = 1;
m1[0, 1] = 2;
m1[1, 0] = 3;
m1[1, 1] = 4;

m2[0, 0] = 5;
m2[0, 1] = 6;
m2[1, 0] = 7;
m2[1, 1] = 8;

m3[0, 0] = new ComplexNumber(2, 3);
m3[0, 1] = new ComplexNumber(-1, 5);
m3[1, 0] = new ComplexNumber(4, 3);
m3[1, 1] = new ComplexNumber(6, -8);

m4[0, 0] = new ComplexNumber(1, -2);
m4[0, 1] = new ComplexNumber(3, 4);
m4[1, 0] = new ComplexNumber(-5, 7);
m4[1, 1] = new ComplexNumber(0, 6);

m5[0, 0] = 1.5f;
m5[0, 1] = -2.3f;
m5[1, 0] = 4.7f;
m5[1, 1] = 0.8f;

m6[0, 0] = 3.2f;
m6[0, 1] = -1.1f;
m6[1, 0] = 2.6f;
m6[1, 1] = -4.9f;

ComplexNumber z1 = new ComplexNumber(3, -4);
ComplexNumber z2 = new ComplexNumber(6, 10);

Console.WriteLine($"\nResult - add two complex numbers {z1} + {z2}: ");
Console.WriteLine(z1 + z2);

Console.WriteLine($"\nResult - multiply two complex numbers {z1} * {z2}: ");
Console.WriteLine(z1 * z2);

Console.WriteLine("\nDisplay m1:");
m1.displayMatrix();

Console.WriteLine("\nDisplay m2:");
m2.displayMatrix();

Console.WriteLine("\nm1 + m2 (int)");
var m1m2Add = m1 + m2;
Console.Write(m1m2Add);

Console.WriteLine("\nm1 * m2 (int)");
var m1m2Mul = m1 * m2;
Console.Write(m1m2Mul);

Console.WriteLine("\nm3 + m4 (ComplexNumber)");
var m3m4Add = m3 + m4;
Console.Write(m3m4Add);

Console.WriteLine("\nm3 * m4 (ComplexNumber)");
var m3m4Mul = m3 * m4;
Console.Write(m3m4Mul);

Console.WriteLine("\nm5 + m6 (float)");
var m5m6Add = m5 + m6;
Console.Write(m5m6Add);

Console.WriteLine("\nm5 * m6 (float)");
var m5m6Mul = m5 * m6;
Console.Write(m5m6Mul);

Console.WriteLine();
Console.ReadKey();