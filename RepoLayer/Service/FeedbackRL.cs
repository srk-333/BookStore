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
    /// Service class for Interface  of Repo Layer
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.IFeedbackRL" />
    public class FeedbackRL : IFeedbackRL
    {
        /// <summary>
        /// The SQL connection
        /// </summary>
        private SqlConnection sqlConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public FeedbackRL(IConfiguration configuration)
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
        /// Adds the feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Added feedback Message
        /// </returns>
        public FeedbackModel AddFeedback(FeedbackModel feedback, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("AddFeedback", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Comment", feedback.Comment);
                cmd.Parameters.AddWithValue("@Rating", feedback.Rating);
                cmd.Parameters.AddWithValue("@BookId", feedback.BookId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                this.sqlConnection.Open();
                cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                return feedback;
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
        /// Updates the feedback.
        /// </summary>
        /// <param name="feedback">The feedback.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="feedbackId">The feedback identifier.</param>
        /// <returns>
        /// Updated feedback Message
        /// </returns>
        public string UpdateFeedback(FeedbackModel feedback, int userId, int feedbackId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("UpdateFeedback", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Comment", feedback.Comment);
                cmd.Parameters.AddWithValue("@Rating", feedback.Rating);
                cmd.Parameters.AddWithValue("@BookId", feedback.BookId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@FeedbackId", feedbackId);
                this.sqlConnection.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                this.sqlConnection.Close();
                if (i == 2)
                {
                    return "Enter Correct Book Id";
                }

                if (i == 1)
                {
                    return "Feedback Already Given for this book";
                }
                else
                {
                    return "Feedback Updated For this Book Successfully";
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
        /// Deletes the feedback.
        /// </summary>
        /// <param name="feedbackId">The feedback identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// True or False
        /// </returns>
        public bool DeleteFeedback(int feedbackId, int userId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("DeleteFeedback", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@FeedbackId", feedbackId);
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
        /// Gets the records by book identifier.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <returns>
        /// All Records From Feedback Table
        /// </returns>
        public List<GetFeedModel> GetRecordsByBookId(int bookId)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand cmd = new SqlCommand("GetAllFeedback", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
               
                cmd.Parameters.AddWithValue("@BookId", bookId);
                this.sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<GetFeedModel> feedbackModel = new List<GetFeedModel>();
                    while (reader.Read())
                    {
                        GetFeedModel feedback = new GetFeedModel();
                        UserModel user = new UserModel
                        {
                            Fullname = reader["FullName"].ToString()
                        };

                        feedback.FeedbackId = Convert.ToInt32(reader["FeedbackId"]);
                        feedback.Comment = reader["Comment"].ToString();
                        feedback.Rating = Convert.ToInt32(reader["Rating"]);
                        feedback.BookId = Convert.ToInt32(reader["BookId"]);
                        feedback.User = user;
                        feedbackModel.Add(feedback);
                    }

                    this.sqlConnection.Close();
                    return feedbackModel;
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
