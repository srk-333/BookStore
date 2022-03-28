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
    ///  Service Class of Repo Layer
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.ICartRL" />
    public class CartRL : ICartRL
    {
        /// <summary>
        /// The SQL connection
        /// </summary>
        private SqlConnection sqlConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartRL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public CartRL(IConfiguration configuration)
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
        /// Adds the cart.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Book Added in cart
        /// </returns>
        public Cart AddCart(Cart cart, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("AddCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@BookId", cart.BookId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return cart;
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
        /// Deletes the cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// True or false
        /// </returns>
        public bool DeleteCart(int cartId, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("DeleteCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CartId", cartId);
                cmd.Parameters.AddWithValue("@UserId", userId);
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
        /// Updates the cart.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Book Updated in cart
        /// </returns>
        public Cart UpdateCart(Cart cart, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("UpdateCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@BookId", cart.BookId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CartId", cart.CartId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return cart;
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
        /// Gets the cart details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of Records from Cart and Book Table
        /// </returns>
        public List<CartModel> GetCartDetails(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("GetCartbyUser", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<CartModel> cartmodel = new List<CartModel>();
                    while (reader.Read())
                    {
                        BookModel booksModel = new BookModel();
                        CartModel cart = new CartModel();
                    
                        booksModel.BookName = reader["bookName"].ToString();
                        booksModel.AuthorName = reader["authorName"].ToString();                 
                        booksModel.OriginalPrice = Convert.ToDecimal(reader["originalPrice"]);
                        booksModel.DiscountPrice = Convert.ToDecimal(reader["discountPrice"]);
                        booksModel.BookImage = reader["bookImage"].ToString();
                        cart.UserId = Convert.ToInt32(reader["UserId"]);
                        cart.UserId = Convert.ToInt32(reader["BookId"]);
                        cart.CartId = Convert.ToInt32(reader["CartId"]);
                        cart.Quantity = Convert.ToInt32(reader["Quantity"]);
                        cart.Bookmodel = booksModel;
                        cartmodel.Add(cart);
                    }

                    this.sqlConnection.Close();
                    return cartmodel;
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
