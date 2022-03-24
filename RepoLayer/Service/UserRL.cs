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
        /// The connection string
        /// </summary>
        private readonly string connectionString = "Data Source=SAURAVSHARMA;Initial Catalog=BookStore;Persist Security Info=True;User ID=saurav;Password=Saurav78#$;";
     
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRL"/> class.
        /// </summary>
        /// <param name="Iconfiguration">The configuration.</param>
        public UserRL(IConfiguration Iconfiguration)
        {
            this.Iconfiguration = Iconfiguration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        private IConfiguration Iconfiguration { get; }
      
        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns> Registered user Details </returns>
        public UserModel Register(UserModel user)
        {
            try
            {
                    SqlConnection conn = new SqlConnection(this.connectionString);                  
                    SqlCommand com = new SqlCommand("UserRegister", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.AddWithValue("@Fullname", user.Fullname);
                    com.Parameters.AddWithValue("@Email", user.Email);
                    com.Parameters.AddWithValue("@Password", user.Password);
                    com.Parameters.AddWithValue("@MobileNumber", user.Mobile);
                    conn.Open();
                    int i = com.ExecuteNonQuery();
                    conn.Close();
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
        }

        /// <summary>
        /// Users the login.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns> Login Token String </returns>
        public string UserLogin(string email, string password)
        {
            try
            {
                SqlConnection conn = new SqlConnection(this.connectionString);
                SqlCommand com = new SqlCommand("UserLogin", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", email);
                com.Parameters.AddWithValue("@Password", password);
                conn.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                    }

                    conn.Close();
                    var token = this.GenerateJWTToken(email);
                    return token;
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns> Token string </returns>
        public string GenerateJWTToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.Iconfiguration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Email", email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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
                SqlConnection conn = new SqlConnection(this.connectionString);
                SqlCommand com = new SqlCommand("UserForgotPassword", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("@Email", email);
                conn.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                    }

                    conn.Close();
                    var token = this.GenerateJWTToken(email);
                    new MSMQ().Sender(token);
                    return token;
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
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
                    SqlConnection conn = new SqlConnection(this.connectionString);
                    SqlCommand com = new SqlCommand("UserResetPassword", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.AddWithValue("@Email", email);
                    com.Parameters.AddWithValue("@Password", confirmPassword);
                    conn.Open();
                    int i = com.ExecuteNonQuery();
                    conn.Close();
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
        }
    }
}
