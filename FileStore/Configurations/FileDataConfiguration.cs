using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FileStore.Models;

namespace FileStore.Configurations
{
    //public class FileDataConfiguration : IEntityTypeConfiguration<FileData>
    //{
    //    public void Configure(EntityTypeBuilder<FileData> builder)
    //    {
    //        builder.ToTable(nameof(FileData));
    //        builder.HasKey(x => x.Id);
    //        builder.HasAlternateKey(x => x.Protect);     
    //        builder.HasOne(x => x.User).WithMany(t=>t.FileDatas).HasForeignKey(x => x.UploadBy).OnDelete(DeleteBehavior.Restrict);
    //    }
    //}
}
