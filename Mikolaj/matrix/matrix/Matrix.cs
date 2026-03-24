namespace matrix
{
    public class Matrix<T>:IMatrix<T>
    {
        private int Dimension { get; set; }
        public T[,] MatrixArray { set; get; }

        public static Matrix<T> operator +(Matrix<T> m1, Matrix<T> m2)
        {
            int dimension = m1.Dimension;
            Matrix<T> m3 = new Matrix<T>(dimension);

            for (int x = 0; x < dimension; x++)
            {
                for (int y = 0; y < dimension; y++)
                {
                    m3[x, y] = (T)((dynamic)m1[x, y] + (dynamic)m2[x, y]);
                }
            }

            m3.displayMatrix();
            return m3;
        }


        public static Matrix<T> operator *(Matrix<T> m1, Matrix<T> m2)
        {
            int dimension = m1.Dimension;
            Matrix<T> m3 = new Matrix<T>(dimension);

            for (int x = 0; x < dimension; x++)
            {
                for (int y = 0; y < dimension; y++)
                {
                    dynamic sum = null;

                    for (int k = 0; k < dimension; k++)
                    {
                        dynamic product = (dynamic)m1[x, k] * (dynamic)m2[k, y];
                        sum = sum == null ? product : sum + product;
                    }

                    m3[x, y] = (T)sum;
                }
            }

            m3.displayMatrix();
            return m3;
        }


        public T this[int x, int y]
        {
            get
            {
                return MatrixArray[x, y];
            }

            set
            {
                MatrixArray[x, y] = value;
            }
        }

        public Matrix(int dimension) {

            Dimension = dimension;
            MatrixArray = new T[Dimension, Dimension];
        
        }
        public void displayMatrix()
        {
            for(int x = 0; x < Dimension; x++)
            {
                for(int y = 0; y < Dimension; y++)
                {
                    Console.Write($"{MatrixArray[x, y]} ");
                }

                Console.Write("\n");
            }
        }

        public override string ToString()
        {
            return "";
        }
    }
}
