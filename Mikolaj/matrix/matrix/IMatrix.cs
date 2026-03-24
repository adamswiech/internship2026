namespace matrix
{
    public interface IMatrix<T>
    {
        T[,] MatrixArray { get; set; }
        void displayMatrix() { }
    }
}
