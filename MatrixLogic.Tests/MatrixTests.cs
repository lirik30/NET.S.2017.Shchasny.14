using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MatrixLogic.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        #region TestCaseData

        private static IEnumerable<TestCaseData> MatrixGetSet_PositiveData
        {
            get
            {
                yield return new TestCaseData(
                        new Matrix<int>(4, 3),
                        4).
                    Returns(4);

                yield return new TestCaseData(
                        new SquareMatrix<char>(3),
                        'a').
                    Returns('a');

                yield return new TestCaseData(
                        new SymmetricMatrix<string>(3),
                        "ab").
                    Returns("ab");

                yield return new TestCaseData(
                        new DiagonalMatrix<float>(2),
                        0.3f).
                    Returns(0.3f);
            }
        }

        private static IEnumerable<TestCaseData> MatrixGetSet_ThrowsOutOfRangeExceptionData
        {
            get
            {
                yield return new TestCaseData(
                    new Matrix<int>(4, 3),
                    4,
                    4, 3);

                yield return new TestCaseData(
                    new SquareMatrix<char>(3),
                    'a',
                    3, 5);

                yield return new TestCaseData(
                    new SymmetricMatrix<string>(3),
                    "ab",
                    -1, 2);

                yield return new TestCaseData(
                    new DiagonalMatrix<float>(2),
                    0.3f,
                    1, -5);
            }
        }

        private static IEnumerable<TestCaseData> MatrixAddition_PositiveData
        {
            get
            {
                yield return new TestCaseData(
                    new Matrix<int>(4, 3),
                    new Matrix<int>(4, 3),
                    5,                   //element value
                    1, 2).              //indices where element will change
                    Returns(5);

                yield return new TestCaseData(
                    new DiagonalMatrix<float>(2),
                    new SquareMatrix<float>(2),
                    0.3f,
                    1, 1).
                    Returns(0.3f);
            }
        }


        #endregion

        [Test, TestCaseSource(nameof(MatrixAddition_PositiveData))]
        public T MatrixAddition_PositiveTests<T>(Matrix<T> lhs, Matrix<T> rhs, T element, int row, int column)
        {
            lhs.SetElement(element, row, column);
            return (lhs + rhs).GetElement(row, column);
        }


        [Test, TestCaseSource(nameof(MatrixGetSet_PositiveData))]
        public T MatrixGetSet_PositiveTests<T>(Matrix<T> matrix, T element)
        {
            matrix.SetElement(element, 1, 1);
            return matrix.GetElement(1, 1);
        }

        [Test, TestCaseSource(nameof(MatrixGetSet_ThrowsOutOfRangeExceptionData))]
        public void MatrixSet_ThrowsArgumentOutOfRangeException<T>(Matrix<T> matrix, T element, int row, int column)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.SetElement(element, row, column));
        }

        [Test, TestCaseSource(nameof(MatrixGetSet_ThrowsOutOfRangeExceptionData))]
        public void MatrixGet_ThrowsArgumentOutOfRangeException<T>(Matrix<T> matrix, T element, int row, int column)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => matrix.GetElement(row, column));
        }

    }
}
