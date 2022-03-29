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
    ///  Service Class For Repo Layer  Interface
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.IOrderRL" />
    public class OrderRL : IOrderRL
    {    
        /// <summary>
           /// The SQL connection
           /// </summary>
        private SqlConnection sqlConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public OrderRL(IConfiguration configuration)
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
        /// Adds the order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Order Added in the System
        /// </returns>
        public OrderModel AddOrder(OrderModel order, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("AddOrders", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookQuantity", order.Quantity);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@BookId", order.BookId);
                cmd.Parameters.AddWithValue("@AddressId", order.AddressId);
                this.sqlConnection.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                this.sqlConnection.Close();
                if (i == 3)
                {
                    return null;
                }

                if (i == 2)
                {
                    return null;
                }
                else
                {
                    return order;
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
        /// Gets all order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Get All Order from Orders
        /// </returns>
        public List<OrderModel> GetAllOrder(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("GetOrders", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<OrderModel> orderModels = new List<OrderModel>();
                    while (reader.Read())
                    {
                        OrderModel orderModel = new OrderModel();
                        BookModel bookModel = new BookModel();
                        orderModel.OrderId = Convert.ToInt32(reader["OrdersId"]);
                        orderModel.UserId = Convert.ToInt32(reader["UserId"]);
                        orderModel.BookId = Convert.ToInt32(reader["bookId"]);
                        orderModel.AddressId = Convert.ToInt32(reader["AddressId"]);
                        orderModel.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                        orderModel.Quantity = Convert.ToInt32(reader["BookQuantity"]);
                        orderModel.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                        bookModel.BookName = reader["bookName"].ToString();
                        bookModel.AuthorName = reader["authorName"].ToString();
                        bookModel.BookImage = reader["bookImage"].ToString();
                        orderModel.BookModel = bookModel;
                        orderModels.Add(orderModel);
                    }

                    this.sqlConnection.Close();
                    return orderModels;
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
