namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// User Register Model Class
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the FullName.
        /// </summary>
        /// <value>
        /// The Full Name.
        /// </value>
        public string Fullname { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>
        /// The mobile.
        /// </value>
        public string Mobile { get; set; }
    }
}
