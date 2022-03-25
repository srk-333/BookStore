namespace CommonLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Book Model Class for Adding Book
    /// </summary>
    public class AddBookModel
    {
        /// <summary>
        /// Gets or sets the name of the book.
        /// </summary>
        /// <value>
        /// The name of the book.
        /// </value>
        public string BookName { get; set; }

        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        /// <value>
        /// The name of the author.
        /// </value>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the total rating.
        /// </summary>
        /// <value>
        /// The total rating.
        /// </value>
        public int TotalRating { get; set; }

        /// <summary>
        /// Gets or sets the discount price.
        /// </summary>
        /// <value>
        /// The discount price.
        /// </value>
        public decimal DiscountPrice { get; set; }

        /// <summary>
        /// Gets or sets the original price.
        /// </summary>
        /// <value>
        /// The original price.
        /// </value>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the book image.
        /// </summary>
        /// <value>
        /// The book image.
        /// </value>
        public string BookImage { get; set; }

        /// <summary>
        /// Gets or sets the book count.
        /// </summary>
        /// <value>
        /// The book count.
        /// </value>
        public int BookCount { get; set; }
    }
}
