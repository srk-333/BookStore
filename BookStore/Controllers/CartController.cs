namespace BookStore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///  Cart Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        /// <summary>
        /// The cart BL
        /// </summary>
        private readonly ICartBL cartBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="cartBL">The cart BL</param>
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        /// <summary>
        /// Adds the cart.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <returns> Book Added in the Cart</returns>
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddCart(Cart cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.AddCart(cart,  userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Added in Cart ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "cart Add failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        /// <summary>
        /// Updates the cart.
        /// </summary>
        /// <param name="cart">The cart.</param>
        /// <returns>Update cart Detail</returns>
        [Authorize]
        [HttpPut("Update")]
        public IActionResult UpdateCart(Cart cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.UpdateCart(cart, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Updated in Cart ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "cart Update failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the cart.
        /// </summary>
        /// <param name="cartId">The cart identifier.</param>
        /// <returns>True or False </returns>
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.cartBL.DeleteCart(cartId, userId))
                {
                    return this.Ok(new { success = true, message = "Book Deleted from Cart " });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "cart Delete  failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        /// <summary>
        /// Gets the cart.
        /// </summary>
        /// <returns> List of Records from Cart and Book Table</returns>
        [Authorize]
        [HttpGet("{UserId}/ Get")]
        public IActionResult GetCart()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var cartData = this.cartBL.GetCartDetails(userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Cat Data Fetched Successfully ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User Id is Wrong" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
    }
}
