namespace BusinessLayer.Interface
{ 
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Models;

    /// <summary>
    ///  Interface Class For Business Layer
    /// </summary>
    public interface IAddressBL
    {
        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="add">The add.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> Add new Address For User</returns>
        public string AddAddress(AddressModel add, int userId);

        /// <summary>
        /// Updates the address.
        /// </summary>
        /// <param name="add">The add.</param>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns> Updated Address Detail </returns>
        public AddressModel UpdateAddress(AddressModel add, int addressId, int userId);

        /// <summary>
        /// Deletes the address.
        /// </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>True Or False</returns>
        public bool DeleteAddress(int addressId, int userId);

        /// <summary>
        /// Gets all address.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>All Address For given user Id</returns>
        public List<AddressModel> GetAllAddress(int userId);
    }
}
