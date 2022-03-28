namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;
    
    /// <summary>
    /// Wish List Interface Class for business Layer
    /// </summary>
    public interface IWishListBL
    {
        /// <summary>
        /// Adds the in wish list.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> Added Record in Wish List </returns>
        public string AddInWishlist(int bookId, int userId);

        /// <summary>
        /// Deletes from wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="wishlistId">The wish list identifier.</param>
        /// <returns> True or False </returns>
        public bool DeleteFromWishlist(int userId, int wishlistId);

        /// <summary>
        /// Gets all from wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> All Records From Wish List and Matching Records from Book </returns>
        public List<WishModel> GetAllFromWishlist(int userId);
    }
}
