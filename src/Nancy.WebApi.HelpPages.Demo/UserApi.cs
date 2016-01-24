using System.Collections.Generic;
using Nancy.WebApi.AttributeRouting;

namespace Nancy.WebApi.Demo
{
    /// <summary>
    /// Web API for User.
    /// </summary>
    [RoutePrefix("api/userApi")]
    public class UserApi : ApiController
    {       
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/")]
        public IEnumerable<User> GetAll()
        {
            return new List<User> { new User(), new User() };
        }

        /// <summary>
        /// Get User by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/id/{id}")]
        public User GetById(int id)
        {
            return new User();
        }

        /// <summary>
        /// Get User by Name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("/name")]
        public IEnumerable<User> GetByName(Name name)
        {
            return new List<User> { new User(), new User(), new User() };
        }
      
        /// <summary>
        /// Modified User.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("/{id}/{firstname}/{lastname}")]
        public bool ChangeUser(int id, string firstname, string lastname)
        {
            return true;
        }

        /// <summary>
        /// Add User.
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpPut("/user")]
        public int AddUser(User User)
        {
            return 1;
        }
    }
}
