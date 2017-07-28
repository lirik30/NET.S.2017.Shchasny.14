using System;

namespace MatrixLogic
{
    /// <summary>
    /// Class provides method for work with symmetric matrix
    /// </summary>
    /// <typeparam name="T">Type of element</typeparam>
    public sealed class SymmetricMatrix<T> : SquareMatrix<T>
    {
        private T[] _matrix;

        /// <summary>
        /// Create symmatric matrix with known order full with default values
        /// </summary>
        /// <param name="n">Order of matrix</param>
        public SymmetricMatrix(int n) : base(n)
        {
            _matrix = new T[(n * n + n) / 2];
        }

        /// <summary>
        /// Get element by indexes
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Element from matrix</returns>
        public override T GetElement(int row, int column)
        {
            if (row > Order || column > Order || row < 0 || column < 0)
                throw new ArgumentOutOfRangeException();
            if (column > row) return GetElement(column, row);
            return _matrix[(row * row + row) / 2 + column];
        }

        /// <summary>
        /// Set element by indexes
        /// </summary>
        /// <param name="value">Value to set</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        public override void SetElement(T value, int row, int column)
        {
            if (row > Order || column > Order || row < 0 || column < 0)
                throw new ArgumentOutOfRangeException();

            T oldValue = GetElement(row, column);
            _matrix[(row * row + row) / 2 + column] = value;
            OnIndexChanged(new ElementChangedEventArgs(value, oldValue, row, column));
            if(row != column)
                OnIndexChanged(new ElementChangedEventArgs(value, oldValue, column, row));
        }
    }
}
