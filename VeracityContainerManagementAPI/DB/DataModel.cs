namespace VeracityContainerManagementAPI.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataModel : DbContext, IDataModel
    {
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<ContainerGroups> ContainerGroups { get; set; }
        public virtual DbSet<Containers> Containers { get; set; }
       
        public virtual DbSet<UserGroups> UserGroups { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<ContainerManagementUsers> ContainerManagementUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContainerGroups>()
                .HasMany(e => e.Containers)
                .WithMany(e => e.ContainerGroups)
                .Map(m => m.ToTable("ContainerGroupsContainers").MapLeftKey("ContainerGroupId").MapRightKey("ContainerId"));

            modelBuilder.Entity<ContainerGroups>()
                .HasMany(e => e.UserGroups)
                .WithMany(e => e.ContainerGroups)
                .Map(m => m.ToTable("UserGroupsContainerGroups").MapLeftKey("ContainerGroupId").MapRightKey("UserGroupId"));

            modelBuilder.Entity<UserGroups>()
                .HasMany(e => e.Users)
                .WithMany(e => e.UserGroups)
                .Map(m => m.ToTable("UserGroupsUsers").MapLeftKey("UserGroupId").MapRightKey("UserId"));


            modelBuilder.Entity<UserGroups>().HasRequired<Users>(j => j.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<Containers>().HasRequired<Users>(j => j.User).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<ContainerGroups>().HasRequired<Users>(j => j.User).WithMany().WillCascadeOnDelete(false);
            


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

       
    }
}
