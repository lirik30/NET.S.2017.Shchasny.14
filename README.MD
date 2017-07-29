# Fibonacci numbers
Fibonacci numbers generator allows to get some count of the Fibonacci numbers.

# Set<T>
A generalized collection class Set that allows you to work only with reference variables with semantics of comparison by value, which implements basic operations on sets and provides the ability to enumerate the elements of a collection.

### Class provides next functionality: 
 - void Add(T):               add element in the ending of the set;
 - void AddAfter(T, T):       add element after any other element(if it exists) in the set;
 - void AddBefore(T, T):      add element before any other element(if it exists) in the set;
 - void AddInTheBeginning(T): add element in the beginning of the set;
 - void Remove(T):            remove element in the set, if it exists.

Class realize IEnumerable<T> interface. This allows you to iterate through the collection and use LINQ-queries.

# Matrix<T>
Class provides methods for representation of matrix:
 - T GetElement(int, int):       get element by indexes of row and column;
 - void SetElement(T, int, int): set element by indexes of row and column;
 - string ToString():            get string representation of the matrix;
 - Matrix<T> operator+(Matrix<T>, Matrix<T>) : possibility to get the sum of two matrices.

Properties:
 - int RowsCount:    rows count in the matrix
 - int ColumnsCount: columns count in the matrix

This class provides with event that will happened when element in matrix is changed.

### Square, symmetric and diagonal matrix
This classes are derived from Matrix class. They have the same functionality, which differs only in the implementation and situations that are specific only for a particular type of matrix. For example, you cannot change element outside the diagonal in the diagonal matrix.
