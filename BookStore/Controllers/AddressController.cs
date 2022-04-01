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
    ///  Address Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [Authorize(Roles = Role.User)]
    [ApiController]
    public class AddressController : ControllerBase
    {
        /// <summary>
        /// The address BL
        /// </summary>
        private readonly IAddressBL addressBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressController"/> class.
        /// </summary>
        /// <param name="addressBL">The address BL</param>
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns> Add new Address For User</returns>
        [HttpPost("Add")]
        public IActionResult AddAddress(AddressModel address)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var addData = this.addressBL.AddAddress(address, userId);
                if (addData.Equals(" Address Added Successfully"))
                {
                    return this.Ok(new { Status = true, Response = addData });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Response = addData });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { status = false, Response = ex.Message });
            }
        }

        /// <summary>
        /// Updates the address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="addressId">The address identifier.</param>
        /// <returns> Updated Address Detail </returns>
        [HttpPut("Update")]
        public IActionResult UpdateAddress(AddressModel address, int addressId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var addData = this.addressBL.UpdateAddress(address, addressId, userId);
                if (addData != null)
                {
                    return this.Ok(new { Status = true, Message = "Address Updated Successfully", Response = addData });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Enter Correct AddressId or TypeId ", Response = addData });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { status = false, Response = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the address.
        /// </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <returns>True Or False</returns>
        [HttpDelete("Delete")]
        public IActionResult DeleteAddress(int addressId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (this.addressBL.DeleteAddress(addressId, userId))
                {
                    return this.Ok(new { Status = true, Message = "Address Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Enter Correct Address Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { status = false, Response = ex.Message });
            }
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <returns>All Address For given user Id</returns>
        [HttpGet("{UserId}/Get")]
        public IActionResult GetAddress()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var data = this.addressBL.GetAllAddress(userId);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "All Address Fetched Successfully", Response = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Some Error Occured With User Detail" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { status = false, Response = ex.Message });
            }
        }
    }
}
