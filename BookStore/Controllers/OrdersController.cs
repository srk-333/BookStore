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
    /// Orders Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// The order BL
        /// </summary>
        private readonly IOrderBL orderBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="orderBL">The order BL</param>
        public OrdersController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        /// <summary>
        /// Adds the order.
        /// </summary>
        /// <param name="ordersModel">The order.</param>
        /// <returns>
        /// Order Added in the System
        /// </returns>
        [HttpPost("Order")]
        public IActionResult AddOrders(OrderModel ordersModel)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "Id").Value);
                var orderData = this.orderBL.AddOrder(ordersModel, userId);
                if (orderData != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Placed Successfully", Response = orderData });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Enter Correct BookId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns> Get All Order from Orders </returns>
        [HttpGet("Get")]
        public IActionResult GetAllOrders()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "Id").Value);
                var orderData = this.orderBL.GetAllOrder(userId);
                if (orderData != null)
                {
                    return this.Ok(new { Status = true, Message = "Order Fetched Successfully", Response = orderData });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Please Login First" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, ex.Message });
            }
        }
    }
}
