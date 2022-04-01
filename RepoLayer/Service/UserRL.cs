namespace RepoLayer.Service
{  
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using CommonLayer.Models;
    using CommonLayer.Token;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service Class
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.IUserRL" />
    public class UserRL : IUserRL
    {
        /// <summary>
        /// The SQL connection
        /// </summary>
        private SqlConnection sqlConnection;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public UserRL(IConfiguration configuration)
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
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns> Registered user Details </returns>
        public UserModel Register(UserModel user)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand com = new SqlCommand("UserRegister", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Fullname", user.Fullname);
                com.Parameters.AddWithValue("@Email", user.Email);
                com.Parameters.AddWithValue("@Password", user.Password);
                com.Parameters.AddWithValue("@MobileNumber", user.Mobile);
                this.sqlConnection.Open();
                int i = com.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return user;
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
        /// Users the login.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns> Login Token String </returns>
        public UserAccount UserLogin(string email, string password)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand com = new SqlCommand("UserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", email);
                com.Parameters.AddWithValue("@Password", password);
                this.sqlConnection.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    UserAccount user = new UserAccount();
                    while (rd.Read())
                    {
                        user.Email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                        user.UserId = Convert.ToInt32(rd["UserId"] == DBNull.Value ? default : rd["UserId"]);
                        user.FullName = Convert.ToString(rd["FullName"] == DBNull.Value ? default : rd["FullName"]);
                    }

                    this.sqlConnection.Close();
                    user.Token = this.GenerateJWTToken(user);
                    return user;
                }
                else
                {
                    this.sqlConnection.Close();
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
        /// Generates the JWT token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Jwt Token</returns>
        public string GenerateJWTToken(UserAccount user)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "User"),
                new Claim("Email", user.Email),
                new Claim("Id", user.UserId.ToString()),
            };

            // signature
            var token = new JwtSecurityToken(
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Forgot the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns> Forgot Password Token </returns>
        public string ForgotPassword(string email)
        {
            try
            {
                this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                SqlCommand com = new SqlCommand("UserForgotPassword", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", email);
                this.sqlConnection.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    int userId = 0;
                    while (rd.Read())
                    {
                        email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                        userId = Convert.ToInt32(rd["UserId"] == DBNull.Value ? default : rd["UserId"]);
                    }

                    this.sqlConnection.Close();
                    var token = this.GenerateJWTTokenForPassword(email, userId);
                    new MSMQ().Sender(token);
                    return token;
                }
                else
                {
                    this.sqlConnection.Close();
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

        public string GenerateJWTTokenForPassword(string email, int userId)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                new Claim("Email", email),
                new Claim("Id", userId.ToString()),
            };

            // signature
            var token = new JwtSecurityToken(
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>True Or False </returns>
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword == confirmPassword)
                {
                    this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStore"]);
                    SqlCommand com = new SqlCommand("UserResetPassword", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.AddWithValue("@Email", email);
                    com.Parameters.AddWithValue("@Password", confirmPassword);
                    this.sqlConnection.Open();
                    int i = com.ExecuteNonQuery();
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
    }
}
