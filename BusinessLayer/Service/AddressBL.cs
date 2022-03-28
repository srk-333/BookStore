namespace BusinessLayer.Service
{   
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Models;
    using RepoLayer.Interface;

    /// <summary>
    /// Service Class For Business Layer
    /// </summary>
    public class AddressBL : IAddressBL
    {
        /// <summary>
        /// The address RL
        /// </summary>
        private readonly IAddressRL addressRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressBL"/> class.
        /// </summary>
        /// <param name="addressRL">The address RL</param>
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="add">The add.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Add new Address For User
        /// </returns>
        public string AddAddress(AddressModel add, int userId)
        {
            try
            {
                return this.addressRL.AddAddress(add, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the address.
        /// </summary>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// True Or False
        /// </returns>
        public bool DeleteAddress(int addressId, int userId)
        {
            try
            {
                return this.addressRL.DeleteAddress(addressId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all address.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// All Address For given user Id
        /// </returns>
        public List<AddressModel> GetAllAddress(int userId)
        {
            try
            {
                return this.addressRL.GetAllAddress(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the address.
        /// </summary>
        /// <param name="add">The add.</param>
        /// <param name="addressId">The address identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Updated Address Detail
        /// </returns>
        public AddressModel UpdateAddress(AddressModel add, int addressId, int userId)
        {
            try
            {
                return this.addressRL.UpdateAddress(add, addressId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
