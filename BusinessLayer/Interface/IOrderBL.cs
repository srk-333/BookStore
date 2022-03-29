namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;

    /// <summary>
    ///  Interface Class of Business Layer
    /// </summary>
    public interface IOrderBL
    {
        /// <summary>
        /// Adds the order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> Order Added in the System </returns>
        public OrderModel AddOrder(OrderModel order, int userId);

        /// <summary>
        /// Gets all order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns> Get All Order from Orders </returns>
        public List<OrderModel> GetAllOrder(int userId);
    }
}
