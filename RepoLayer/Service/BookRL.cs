namespace RepoLayer.Service
{ 
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service class for Interface 
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.IBookRL" />
    public class BookRL : IBookRL
    {
        /// <summary>
        /// The SQL connection
        /// </summary>
        private SqlConnection sqlConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        private IConfiguration Configuration { get; }

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
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Bookstore"]);
                SqlCommand cmd = new SqlCommand("AddBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@bookName", book.BookName);
                cmd.Parameters.AddWithValue("@authorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@rating", book.Rating);
                cmd.Parameters.AddWithValue("@totalRating", book.TotalRating);
                cmd.Parameters.AddWithValue("@discountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@originalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("@description", book.Description);
                cmd.Parameters.AddWithValue("@bookImage", book.BookImage);
                cmd.Parameters.AddWithValue("@BookCount", book.BookCount);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
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
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Bookstore"]);
                SqlCommand cmd = new SqlCommand("UpdateBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@bookId", book.BookId);
                cmd.Parameters.AddWithValue("@bookName", book.BookName);
                cmd.Parameters.AddWithValue("@authorName", book.AuthorName);
                cmd.Parameters.AddWithValue("@rating", book.Rating);
                cmd.Parameters.AddWithValue("@totalRating", book.TotalRating);
                cmd.Parameters.AddWithValue("@discountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@originalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("@description", book.Description);
                cmd.Parameters.AddWithValue("@bookImage", book.BookImage);
                cmd.Parameters.AddWithValue("@BookCount", book.BookCount);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
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
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Bookstore"]);
                SqlCommand cmd = new SqlCommand("DeleteBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@bookId", bookId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
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
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Bookstore"]);
                SqlCommand cmd = new SqlCommand("GetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@bookId", bookId);
                this.sqlConnection.Open();
                BookModel bookModel = new BookModel();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bookModel.BookId = Convert.ToInt32(reader["bookId"]);
                        bookModel.BookName = reader["bookName"].ToString();
                        bookModel.AuthorName = reader["authorName"].ToString();
                        bookModel.Rating = Convert.ToInt32(reader["rating"]);
                        bookModel.TotalRating = Convert.ToInt32(reader["totalRating"]);
                        bookModel.DiscountPrice = Convert.ToDecimal(reader["discountPrice"]);
                        bookModel.OriginalPrice = Convert.ToDecimal(reader["originalPrice"]);
                        bookModel.Description = reader["description"].ToString();
                        bookModel.BookImage = reader["bookImage"].ToString();
                        bookModel.BookCount = Convert.ToInt32(reader["BookCount"]);
                    }

                    this.sqlConnection.Close();
                    return bookModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
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
                List<BookModel> book = new List<BookModel>();
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:Bookstore"]);
                SqlCommand cmd = new SqlCommand("GetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new BookModel
                        {
                            BookId = Convert.ToInt32(reader["bookId"]),
                            BookName = reader["bookName"].ToString(),
                            AuthorName = reader["authorName"].ToString(),
                            Rating = Convert.ToInt32(reader["rating"]),
                            TotalRating = Convert.ToInt32(reader["totalRating"]),
                            DiscountPrice = Convert.ToDecimal(reader["discountPrice"]),
                            OriginalPrice = Convert.ToDecimal(reader["originalPrice"]),
                            Description = reader["description"].ToString(),
                            BookImage = reader["bookImage"].ToString(),
                            BookCount = Convert.ToInt32(reader["BookCount"])
                        });
                    }
                    this.sqlConnection.Close();
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}
