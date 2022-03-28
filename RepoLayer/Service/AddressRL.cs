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
    ///  Service Class For Repo Layer
    /// </summary>
    public class AddressRL : IAddressRL
    {
        /// <summary>
        /// The SQL connection
        /// </summary>
        private SqlConnection sqlConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public AddressRL(IConfiguration configuration)
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
        /// Adds the address.
        /// </summary>
        /// <param name="add">The add.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Add new Address For User
        /// </returns>
        public string AddAddress(AddressModel add, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("AddAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Address", add.Address);
                cmd.Parameters.AddWithValue("@City", add.City);
                cmd.Parameters.AddWithValue("@State", add.State);
                cmd.Parameters.AddWithValue("@TypeId", add.TypeId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                this.sqlConnection.Close();
                if (i == 2)
                {
                    return "Enter Correct TypeId For Adding Address";
                }
                else
                {
                    return " Address Added Successfully";
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
        /// Updates the address.
        /// </summary>
        /// <param name="add">The add.</param>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Updated Address Detail
        /// </returns>
        public AddressModel UpdateAddress(AddressModel add, int addressId, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("UpdateAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Address", add.Address);
                cmd.Parameters.AddWithValue("@City", add.City);
                cmd.Parameters.AddWithValue("@State", add.State);
                cmd.Parameters.AddWithValue("@TypeId", add.TypeId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@AddressId", addressId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return add;
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
        /// Deletes the address.
        /// </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>True Or False</returns>
        public bool DeleteAddress(int addressId, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("DeleteAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AddressId", addressId);
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
        /// Gets all address.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// All Address For given user Id
        /// </returns>
        public List<AddressModel> GetAllAddress(int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("GetAllAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<AddressModel> addressModel = new List<AddressModel>();
                    while (reader.Read())
                    {
                        addressModel.Add(new AddressModel
                        {
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            TypeId = Convert.ToInt32(reader["TypeId"]),
                            UserId = Convert.ToInt32(reader["UserId"])
                        });
                    }

                    this.sqlConnection.Close();
                    return addressModel;
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
