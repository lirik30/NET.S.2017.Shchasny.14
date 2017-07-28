using System;
using System.Text;

namespace MatrixLogic
{
    /// <summary>
    /// Class provides method for work with matrix
    /// </summary>
    /// <typeparam name="T">Type of element</typeparam>
    public class Matrix<T>
    {
        private T[,] _matrix;
        private int _nRows;
        private int _nCols;

        /// <summary>
        /// Rows count in the matrix
        /// </summary>
        public int RowsCount => _nRows;

        /// <summary>
        /// Columns count in the matrix
        /// </summary>
        public int ColumnsCount => _nCols;

        /// <summary>
        /// Contains events that will happen when index will change
        /// </summary>
        public event EventHandler<ElementChangedEventArgs> IndexChanged = delegate { };

        /// <summary>
        /// Create matrix with a known number of rows and columns full with default values
        /// </summary>
        /// <param name="nRows">Number of rows</param>
        /// <param name="nCols">Number of columns</param>
        public Matrix(int nRows, int nCols)
        {
            if(nRows <= 0 || nCols <= 0)
                throw new ArgumentOutOfRangeException($"Both of {nameof(nRows)}, {nameof(nCols)} must be greater than 0");
            
            _matrix = new T[nRows, nCols];
            _nRows = nRows;
            _nCols = nCols;
        }


        /// <summary>
        /// Get element by indexes
        /// </summary>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        /// <returns>Element from matrix</returns>
        public virtual T GetElement(int row, int column)
        {
            if(row > _nRows || column > _nCols || row < 0 || column < 0)
                throw new ArgumentOutOfRangeException();

            return _matrix[row, column];
        }

        /// <summary>
        /// Set element by indexes
        /// </summary>
        /// <param name="value">Value to set</param>
        /// <param name="row">Row index</param>
        /// <param name="column">Column index</param>
        public virtual void SetElement(T value, int row, int column)
        {
            if (row > _nRows || column > _nCols || row < 0 || column < 0)
                throw new ArgumentOutOfRangeException();
            T oldValue = GetElement(row, column);
            _matrix[row, column] = value;
            OnIndexChanged(new ElementChangedEventArgs(value, oldValue, row, column));
        }


        protected virtual void OnIndexChanged(ElementChangedEventArgs eventArgs)
        {
            var temp = IndexChanged;//?
            temp?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// String representation of matrix
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var result  = new StringBuilder(String.Empty);
            for (int i = 0; i < _nRows; i++)
            {
                for (int j = 0; j < _nCols; j++)
                    result.Append(GetElement(i,j) + " ");
                result.Append(Environment.NewLine);
            }
            return result.ToString();
        }
    }

    /// <summary>
    /// Keep information about the TimePassed event
    /// </summary>
    public class ElementChangedEventArgs : EventArgs
    {
        public object Element { get; }
        public object OldElement { get; }
        public int Row { get; }
        public int Column { get; }

        public ElementChangedEventArgs(object element, object oldElement, int row, int column)
        {
            Element = element;
            OldElement = oldElement;
            Row = row;
            Column = column;
        }
    }
}
