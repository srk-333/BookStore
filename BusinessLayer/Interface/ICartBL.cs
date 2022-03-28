namespace BusinessLayer.Interface
{ 
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;

    /// <summary>
    /// Interface Class
    /// </summary>
    public interface ICartBL
    {
        /// <summary>
        /// Adds the cart.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> Book Added in cart</returns>
        public Cart AddCart(Cart cart, int userId);

        /// <summary>
        /// Updates the cart.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Book Updated in cart</returns>
        public Cart UpdateCart(Cart cart, int userId);

        /// <summary>
        /// Deletes the cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>True or false</returns>
        public bool DeleteCart(int cartId, int userId);

        /// <summary>
        /// Gets the cart details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of Records from Cart and Book Table</returns>
        public List<CartModel> GetCartDetails(int userId);
    }
}
