using FileStore.Services;
using FileStore.Models;
using FileStore;

namespace FileStore.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {

        }
    }
}
