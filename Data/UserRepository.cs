using SenwesAssignment_API.Models;
using System.Collections.Generic;
using System.Linq;

namespace SenwesAssignment_API
{
    public class UserRepository : IUserRepository
    {
        private readonly List<UserDTO> users = new List<UserDTO>();
        public UserRepository()
        {
            users.Add(new UserDTO { UserName = "Ben@gmail.com", Password = "Ben123"});
            users.Add(new UserDTO { UserName = "michaelsanders", Password = "michael321" });
            users.Add(new UserDTO { UserName = "stephensmith", Password = "stephen123" });
            users.Add(new UserDTO { UserName = "rodpaddock", Password = "rod123" });
            users.Add(new UserDTO { UserName = "rexwills", Password = "rex321" });
        }
        public UserDTO GetUser(UserModel userModel)
        {
            return users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower()
                && x.Password == userModel.Password).FirstOrDefault();
        }
    }
}
