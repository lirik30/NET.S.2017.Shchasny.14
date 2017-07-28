namespace MatrixLogic
{
    /// <summary>
    /// Class provides method for work with square matrix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SquareMatrix<T> : Matrix<T>
    {
        /// <summary>
        /// Order of square matrix
        /// </summary>
        public int Order { get; }
        
        /// <summary>
        /// Create square matrix with known order full with default values
        /// </summary>
        /// <param name="n">Order of matrix</param>
        public SquareMatrix(int n) : base(n, n) => Order = n;
    }
}
