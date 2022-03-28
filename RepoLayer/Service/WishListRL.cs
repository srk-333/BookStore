namespace RepoLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using CommonLayer.Models;
    using Microsoft.Extensions.Configuration;
    using RepoLayer.Interface;
   
    /// <summary>
    ///  Service Class for Repo Layer
    /// </summary>
    public class WishListRL : IWishListRL
    {
        /// <summary>
        /// The SQL connection
        /// </summary>
        private SqlConnection sqlConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="WishListRL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public WishListRL(IConfiguration configuration)
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
        /// Adds the in wish list.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Added Record in Wish List
        /// </returns>
        public string AddInWishlist(int bookId, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("AddInWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@BookId", bookId);
                this.sqlConnection.Open();
                 int i = Convert.ToInt32(cmd.ExecuteScalar());
                this.sqlConnection.Close();
                if (i == 2)
                {
                    return "Book is Already in Wishlist";
                }

                if (i == 1)
                {
                    return "Choose Correct BookID";
                }
                else
                {
                    return "Book added in Wishlist";               
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
        /// Deletes from Wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="wishlistId">The wish list identifier.</param>
        /// <returns>
        /// True or False
        /// </returns>
        public bool DeleteFromWishlist(int userId, int wishlistId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("DeleteFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@WishlistId", wishlistId);
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
        /// Gets all from Wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// All Records From WishList and Matching Records from Book
        /// </returns>
        public List<WishModel> GetAllFromWishlist(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("GetAllRecordsFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<WishModel> wishModel = new List<WishModel>();                  
                    while (reader.Read())
                    {
                        BookModel bookModel = new BookModel();
                        WishModel wish = new WishModel();
                        bookModel.BookName = reader["bookName"].ToString();
                        bookModel.AuthorName = reader["authorName"].ToString();
                        bookModel.OriginalPrice = Convert.ToDecimal(reader["originalPrice"]);
                        bookModel.DiscountPrice = Convert.ToDecimal(reader["discountPrice"]);
                        bookModel.BookImage = reader["bookImage"].ToString();
                        wish.WishlistId = Convert.ToInt32(reader["WishlistId"]);
                        wish.UserId = Convert.ToInt32(reader["UserId"]);
                        wish.BookId = Convert.ToInt32(reader["BookId"]);
                        wish.Bookmodel = bookModel;
                        wishModel.Add(wish);
                    }

                    this.sqlConnection.Close();
                    return wishModel;
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
