using System.Collections.Generic;
using System.Drawing;
using Nancy.WebApi.AttributeRouting;

namespace Nancy.WebApi.Demo
{
    /// <summary>
    /// Web API for Address.
    /// </summary>
    [RoutePrefix("api/address")]
    public class AddressApi : ApiController
    {
        /// <summary>
        /// Gets all addresss.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/")]
        public IEnumerable<Address> GetAll()
        {
            return new List<Address> { new Address(), new Address() };
        }

        /// <summary>
        /// Get Address by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/id/{id}")]
        public Address GetById(int id)
        {
            return new Address();
        }


        /// <summary>
        /// Modified Address.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost("/id/{id}")]
        public bool ChangeAddress(int id, Address address)
        {
            return true;
        }

        /// <summary>
        /// Add Address.
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        [HttpPut("/")]
        public int AddAddress(Address Address)
        {
            return 1;
        }

        /// <summary>
        /// Add Address.
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        [HttpPost("/{id}")]
        public int AddImage(int id, Image image)
        {
            return 1;
        }
    }
}
