using Microsoft.EntityFrameworkCore;
using FileStore.Services;

namespace FileStore
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }   
        public IManageImage ManageImage { get; set; }
        public IUserRepository UserRepository { get; set; }
        public UnitOfWork(Context context,
            IManageImage manageImage,
            IUserRepository userRepository)
        {
            Context = context;
            ManageImage = manageImage;
            UserRepository = userRepository;
            }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
