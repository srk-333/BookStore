namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using RepoLayer.Interface;

    /// <summary>
    /// Service Class For Business Layer  Interface
    /// </summary>
    /// <seealso cref="BusinessLayer.Interface.IOrderBL" />
    public class OrderBL : IOrderBL
    {
        /// <summary>
        /// The order RL
        /// </summary>
        private readonly IOrderRL orderRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBL"/> class.
        /// </summary>
        /// <param name="orderRL">The order RL.</param>
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        /// <summary>
        /// Adds the order.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Order Added in the System
        /// </returns>
        public OrderModel AddOrder(OrderModel order, int userId)
        {
            try
            {
                return this.orderRL.AddOrder(order, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all order.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Get All Order from Orders
        /// </returns>
        public List<OrderModel> GetAllOrder(int userId)
        {
            try
            {
                return this.orderRL.GetAllOrder(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
