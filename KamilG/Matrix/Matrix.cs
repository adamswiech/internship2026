using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix
{
    public class Matrix
    {
        int[,] matrix1 = new int[2, 2];
        int[,] matrix2 = new int[2, 2];
        int[,] ymatrix = new int[2, 2];

        public void Write1()
        {
            int i = 1;
            matrix1[0, 0] = i;
            matrix1[0, 1] = i + 1;
            matrix1[1, 1] = i + 2;
            matrix1[1, 0] = i + 3;
        }

        public void Write2()
        {
            int i = 5;
            matrix2[0, 0] = i;
            matrix2[0, 1] = i + 1;
            matrix2[1, 1] = i + 2;
            matrix2[1, 0] = i + 3;
        }

        //indeksator?
        //public int this[int row, int col]
        //{
        //    get { return matrix1[row, col]; }
        //    set { matrix1[row, col] = value; }
        //}



        // Wyświetlanie macierzy
        public void Printn(int matrixNumber)
        {
            // Wybór matrycy na podstawie przekazanego numeru
            int[,] targetMatrix = matrixNumber switch
            {
                1 => matrix1,
                2 => matrix2,
                3 => ymatrix,
                _ => throw new ArgumentException("Nieznany numer macierzy")
            };
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write(targetMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        //public void Print()
        //{
        //    //Macierz 1
        //    for (int i = 0; i < 2; i++)
        //    {
        //        for (int j = 0; j < 2; j++)
        //        {
        //            Console.Write(matrix1[i, j] + " ");
        //        }
        //        Console.WriteLine();
        //    }

        //    //Macierz 2
        //    for (int i = 0; i < 2; i++)
        //    {
        //        for (int j = 0; j < 2; j++)
        //        {
        //            Console.Write(matrix2[i, j] + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //}

        // Mnożenie macierzy
        public void Mult()
        {
            ymatrix = new int[,]
            {
                { matrix1[0, 0] * matrix2[0, 0] + matrix1[0, 1] * matrix2[1, 0], matrix1[0, 0] * matrix2[0, 1] + matrix1[0, 1] * matrix2[1, 1] },
                { matrix1[1, 0] * matrix2[0, 0] + matrix1[1, 1] * matrix2[1, 0], matrix1[1, 0] * matrix2[0, 1] + matrix1[1, 1] * matrix2[1, 1] }

            };
            
        }
    }
   
}