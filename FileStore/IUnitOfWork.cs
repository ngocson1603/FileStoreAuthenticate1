using Microsoft.EntityFrameworkCore;
using FileStore.Services;

namespace FileStore
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }       
        IManageImage ManageImage { get; }
        IUserRepository UserRepository { get; }
        void Save();
    }
}
