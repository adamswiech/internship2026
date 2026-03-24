using System;
using Matrix;
using System.Collections.Generic;
using System.Numerics;

Console.WriteLine("Hello, World!");
var matrix = new Matrix.Matrix();
matrix.Write1();
matrix.Write2();
matrix.Printn(1);
matrix.Printn(2);
Console.WriteLine();
//Console.WriteLine(matrix[0, 0]);


//Matrix.Matrix a = new Matrix.Matrix();
//a.Write1();
//Matrix.Matrix b = new Matrix.Matrix();
//b.Write2();
//Matrix.Matrix ymatrix =  * b;

matrix.Mult();
matrix.Printn(3);