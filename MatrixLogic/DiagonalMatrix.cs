using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLogic
{
    /// <summary>
    /// Class provides methods for work with diagonal matrix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DiagonalMatrix<T> : SquareMatrix<T>
    {
        private T[] _matrix;

        /// <summary>
        /// Create diagonal matrix with known order full with default values
        /// </summary>
        /// <param name="n">Order of matrix</param>
        public DiagonalMatrix(int n) : base(n)
        {
            _matrix = new T[n];
        }

        /// <summary>
        /// Get element by indices
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Element from matrix</returns>
        public override T GetElement(int row, int column)
        {
            if (row >= Order || column >= Order || row < 0 || column < 0)
                throw new ArgumentOutOfRangeException();

            return row == column ? _matrix[row] : default(T);
        }

        /// <summary>
        /// Set element by indices. You can't change indices outside the diagonal
        /// </summary>
        /// <param name="value">Value to set</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        public override void SetElement(T value, int row, int column)
        {
            if (row >= Order || column >= Order || row < 0 || column < 0)
                throw new ArgumentOutOfRangeException();

            if(row != column)
                throw new InvalidOperationException();

            T oldValue = GetElement(row, column);
            _matrix[row] = value;
            OnIndexChanged(new ElementChangedEventArgs(value, oldValue, row, column));
        }
    }
}
