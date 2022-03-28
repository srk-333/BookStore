namespace BusinessLayer.Service
{ 
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service Class for Business Layer
    /// </summary>
    public class WishListBL : IWishListBL
    {
        /// <summary>
        /// The wish list RL
        /// </summary>
        private readonly IWishListRL wishlistRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="WishListBL"/> class.
        /// </summary>
        /// <param name="wishlistRL">The wish list RL.</param>
        public WishListBL(IWishListRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }

        /// <summary>
        /// Adds the in wish list.
        /// </summary>
        /// <param name="bookId">The book identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Added Record in Wish List
        /// </returns>
        public string AddInWishlist(int bookId, int userId)
        {
            try
            {
                return this.wishlistRL.AddInWishlist(bookId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes from wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="wishlistId">The wish list identifier.</param>
        /// <returns>
        /// True or False
        /// </returns>
        public bool DeleteFromWishlist(int userId, int wishlistId)
        {
            try
            {
                return this.wishlistRL.DeleteFromWishlist(userId, wishlistId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all from wish list.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// All Records From Wish List and Matching Records from Book
        /// </returns>
        public List<WishModel> GetAllFromWishlist(int userId)
        {
            try
            {
                return this.wishlistRL.GetAllFromWishlist(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
