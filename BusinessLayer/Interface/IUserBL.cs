namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;

    /// <summary>
    ///  Interface class 
    /// </summary>
    public interface IUserBL
    {
        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Registered User Details</returns>
        public UserModel Register(UserModel user);

        /// <summary>
        /// Users the login.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>Login Token</returns>
        public string UserLogin(string email, string password);

        /// <summary>
        /// Forgot the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Forgot Password Token</returns>
        public string ForgotPassword(string email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>True or False</returns>
        public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
