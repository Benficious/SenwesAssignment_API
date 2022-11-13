using SenwesAssignment_API.Models;

namespace SenwesAssignment_API
{
    public interface IUserRepository
    {
        UserDTO GetUser(UserModel userModel);
    }
}