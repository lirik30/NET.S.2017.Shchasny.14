using System;
using System.Collections.Generic;

namespace BookLogic
{
    [Serializable]
    public sealed class Book : IComparable, IEquatable<Book>, IComparable<Book>
    {
        private string _author;
        private string _name;
        private string _genre;
        private int _pages;

        /// <summary>
        /// Book author
        /// </summary>
        public string Author {
            get => _author;
            set => _author = value ?? "NoAuthor";
        }

        /// <summary>
        /// Book title
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value ?? "NoName";
        }

        /// <summary>
        /// Book genre
        /// </summary>
        public string Genre
        {
            get => _genre;
            set => _genre = value??"NoGenre";
        }

        /// <summary>
        /// Number of pages in a book
        /// </summary>
        public int Pages
        {
            get => _pages;
            set => _pages = value <= 0 ? throw new ArgumentOutOfRangeException($"{value} is to low for the pages number") : value;
        }

        public Book(string author, string name, string genre, int pages)
        {
            Author = author;
            Name = name;
            Genre = genre;
            Pages = pages;
        }
        
        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Name == other.Name && this.Pages == other.Pages &&
                this.Genre == other.Genre && this.Author == other.Author; 
        }

        
        /// <summary>
        /// Compare 2 books by title
        /// </summary>
        /// <param name="other">Book to compare</param>
        /// <returns></returns>
        public int CompareTo(Book other) => CompareTo(other, null);

        /// <summary>
        /// Method compares 2 books by comparer. Compare by title is default
        /// </summary>
        /// <param name="other">Book to compare</param>
        /// <param name="comparer">Custom comparer</param>
        /// <returns></returns>
        public int CompareTo(Book other, IComparer<Book> comparer)
        {
            if (ReferenceEquals(comparer, null))
                return ReferenceEquals(other, null) ? 1 : String.Compare(Name, other.Name, StringComparison.Ordinal);
            return comparer.Compare(this, other);
        }
            

        public override string ToString() => $"{Name}. Author: {Author}. Genre: {Genre}. Number of pages: {Pages}";

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            
            var book = obj as Book;
            return !ReferenceEquals(book, null) && Equals(book);
        }

        public override int GetHashCode() => Pages ^ Author.GetHashCode() ^ Name.GetHashCode() ^ Genre.GetHashCode();

        public int CompareTo(object obj) => CompareTo(obj as Book, null);
    }
}
