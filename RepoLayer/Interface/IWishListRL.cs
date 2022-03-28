namespace RepoLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;

    /// <summary>
    /// WishList Interface Class for Repo Layer
    /// </summary>
    public interface IWishListRL
    {
        /// <summary>
        /// Adds the in Wish list.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Added Record in Wish List</returns>
        public string AddInWishlist(int bookId, int userId);

        /// <summary>
        /// Deletes from Wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="wishlistId">The wish list identifier.</param>
        /// <returns> True or False </returns>
        public bool DeleteFromWishlist(int userId, int wishlistId);

        /// <summary>
        /// Gets all from Wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> All Records From Wish List and Matching Records from Book </returns>
        public List<WishModel> GetAllFromWishlist(int userId);
    }
}
