namespace BusinessLayer.Service
{  
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using RepoLayer.Interface;

    /// <summary>
    /// Service Class Of Business Layer
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.ICartBL" />
    public class CartBL : ICartBL
    {
        /// <summary>
        /// The user RL interface
        /// </summary>
        private readonly ICartRL cartRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartBL"/> class.
        /// </summary>
        /// <param name="cartRL">The user RL.</param>
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        /// <summary>
        /// Adds the cart.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Book Added in cart
        /// </returns>
        public Cart AddCart(Cart cart, int userId)
        {
            try
            {
                return this.cartRL.AddCart(cart, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// True or false
        /// </returns>
        public bool DeleteCart(int cartId, int userId)
        {
            try
            {
                return this.cartRL.DeleteCart(cartId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the cart details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of Records from Cart and Book Table
        /// </returns>
        public List<CartModel> GetCartDetails(int userId)
        {
            try
            {
                return this.cartRL.GetCartDetails(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the cart.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Book Updated in cart
        /// </returns>
        public Cart UpdateCart(Cart cart, int userId)
        {
            try
            {
                return this.cartRL.UpdateCart(cart, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
