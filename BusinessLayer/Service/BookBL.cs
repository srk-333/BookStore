namespace BusinessLayer.Service
{  
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service class for Interface 
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IBookBL" />
    public class BookBL : IBookBL
    {
        /// <summary>
        /// The user RL interface
        /// </summary>
        private readonly IBookRL bookRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookBL"/> class.
        /// </summary>
        /// <param name="bookRL">The user RL.</param>
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        /// <summary>
        /// Adds the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>
        /// Added Book Detail
        /// </returns>
        public AddBookModel AddBook(AddBookModel book)
        {
            try
            {
                return this.bookRL.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the book.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>
        /// True or False
        /// </returns>
        public bool DeleteBook(int bookId)
        {
            try
            {
                return this.bookRL.DeleteBook(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <returns>
        /// Get All Book
        /// </returns>
        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the book by book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>
        /// Get a Book By Book Id
        /// </returns>
        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                return this.bookRL.GetBookByBookId(bookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>
        /// Updated Book Detail
        /// </returns>
        public BookModel UpdateBook(BookModel book)
        {
            try
            {
                return this.bookRL.UpdateBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
