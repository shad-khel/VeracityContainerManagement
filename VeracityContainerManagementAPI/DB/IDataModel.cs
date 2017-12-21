using System.Data.Entity;

namespace VeracityContainerManagementAPI.DB
{
    public interface IDataModel
    {
        DbSet<ContainerGroups> ContainerGroups { get; set; }
        DbSet<Containers> Containers { get; set; }
        DbSet<UserGroups> UserGroups { get; set; }
        DbSet<Users> Users { get; set; }
        DbSet<ContainerManagementUsers> ContainerManagementUser { get; set; }

        int SaveChanges();
    }
}