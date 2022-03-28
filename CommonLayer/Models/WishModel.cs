namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    ///  WishList model Class
    /// </summary>
    public class WishModel
    {
        /// <summary>
        /// Gets or sets the wish list identifier.
        /// </summary>
        /// <value>
        /// The wish list identifier.
        /// </value>
        public int WishlistId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the book identifier.
        /// </summary>
        /// <value>
        /// The book identifier.
        /// </value>
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets the book model.
        /// </summary>
        /// <value>
        /// The book model.
        /// </value>
        public BookModel Bookmodel { get; set; }
    }
}
