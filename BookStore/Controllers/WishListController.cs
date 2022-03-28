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
    /// Wish list Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WishListController : ControllerBase
    {
        /// <summary>
        /// The wish list BL
        /// </summary>
        private readonly IWishListBL wishlistBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="WishListController"/> class.
        /// </summary>
        /// <param name="wishlistBL">The wish list BL</param>
        public WishListController(IWishListBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }

        /// <summary>
        /// Adds the in wish list.
        /// </summary>
        /// <param name="bookId">The wish.</param>
        /// <returns> Added Record in Wish List </returns>
        [HttpPost("Add")]
        public IActionResult AddInWishlist(int bookId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.wishlistBL.AddInWishlist(bookId, userId);
                if (result.Equals("Book added in Wishlist"))
                {
                    return this.Ok(new { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes from wish list.
        /// </summary>
        /// <param name="wishlistId">The wish list identifier.</param>
        /// <returns> True or False </returns>
        [HttpDelete("Delete")]
        public IActionResult DeleteFromWishlist(int wishlistId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.wishlistBL.DeleteFromWishlist(userId, wishlistId))
                {
                    return this.Ok(new { Status = true, Message = "Deleted From Wishlist" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Some Error Occured" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all from wish list.
        /// </summary>
        /// <returns> All Records From Wish List and Matching Records from Book </returns>
        [HttpGet("{UserId}/ Get")]
        public IActionResult GetCart()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var wishData = this.wishlistBL.GetAllFromWishlist(userId);
                if (wishData != null)
                {
                    return this.Ok(new { success = true, message = "All Wishlist Data Fetched Successfully ", response = wishData });
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
