# Fibonacci numbers
Fibonacci numbers generator allows to get some count of the Fibonacci numbers.

# Set
A generalized collection class Set that allows you to work only with reference variables with semantics of comparison by value, which implements basic operations on sets and provides the ability to enumerate the elements of a collection.

### Class provides next functionality: 
 - void Add(T): add element in the ending of the set;
 - void AddAfter(T, T): add element after any other element(if it exists) in the set;
 - void AddBefore(T, T): add element before any other element(if it exists) in the set;
 - void AddInTheBeginning(T): add element in the beginning of the set;
 - void Remove(T): remove element in the set, if it exists;

Class realize IEnumerable<T> interface. This allows you to iterate through the collection and use LINQ-queries.