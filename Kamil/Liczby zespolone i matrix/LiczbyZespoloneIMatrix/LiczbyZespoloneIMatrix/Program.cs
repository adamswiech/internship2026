using LiczbyZespoloneIMatrix;


Z z1 = new Z(1, 2);
Z z2 = new Z(3, -2);
z1 = z1 * z2;
//z1.Print();

//Matrix<int> m1 = new Matrix<int>(new int[2, 2] { { 1, 2 }, { 3, 4 } });
//Matrix<int> m2= new Matrix<int>(new int[2, 2] { { 5, 6 }, { 7, 8 } });
//m1 = m1 * m2;
//m1.Print();

Matrix<Z> m1 = new Matrix<Z>(new Z[2, 2] { { z1, z2 }, { z1, z2 } });
Matrix<Z> m2 = new Matrix<Z>(new Z[2, 2] { { z1, z2 }, { z2, z1 } });

m1 = m1 * m2;
m1.Print();

foreach(var m in m1)
    Console.WriteLine(m.ToString());