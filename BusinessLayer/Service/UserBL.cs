namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using CommonLayer.Token;
    using RepoLayer.Interface;

    /// <summary>
    /// Service Class
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IUserBL" />
    public class UserBL : IUserBL
    {
        /// <summary>
        /// The user RL interface
        /// </summary>
        private readonly IUserRL userRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBL"/> class.
        /// </summary>
        /// <param name="userRL">The user RL.</param>
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns> User Details </returns>
        public UserModel Register(UserModel user)
        {
            try
            {
                return this.userRL.Register(user);
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
        /// <returns> Login Token </returns>
        public UserAccount UserLogin(string email, string password)
        {
            try
            {
                return this.userRL.UserLogin(email, password);
            }
            catch (Exception)
            {
                throw;
            }
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
                return this.userRL.ForgotPassword(email);
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
        /// <returns>
        /// True or False
        /// </returns>
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, newPassword, confirmPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
