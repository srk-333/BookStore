namespace RepoLayer.Interface
{   
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    ///  Interface class
    /// </summary>
    public interface IBookRL
    {
        /// <summary>
        /// Adds the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns> Added Book Detail </returns>
        public AddBookModel AddBook(AddBookModel book);

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>Updated Book Detail </returns>
        public BookModel UpdateBook(BookModel book);

        /// <summary>
        /// Deletes the book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>True or False </returns>
        public bool DeleteBook(int bookId);

        /// <summary>
        /// Gets the book by book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns> Get a Book By Book Id </returns>
        public BookModel GetBookByBookId(int bookId);

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns> Get All Book </returns>
        public List<BookModel> GetAllBooks();
    }
}
