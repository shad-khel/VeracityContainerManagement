namespace VeracityContainerManagementAPI.DB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOwnerIdonContainersContainerGroupsUserGroups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContainerGroups", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Containers", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.UserGroups", "OwnerId", c => c.Guid(nullable: false));
            CreateIndex("dbo.ContainerGroups", "OwnerId");
            CreateIndex("dbo.Containers", "OwnerId");
            CreateIndex("dbo.UserGroups", "OwnerId");
            AddForeignKey("dbo.UserGroups", "OwnerId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Containers", "OwnerId", "dbo.Users", "UserId");
            AddForeignKey("dbo.ContainerGroups", "OwnerId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContainerGroups", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.Containers", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.UserGroups", "OwnerId", "dbo.Users");
            DropIndex("dbo.UserGroups", new[] { "OwnerId" });
            DropIndex("dbo.Containers", new[] { "OwnerId" });
            DropIndex("dbo.ContainerGroups", new[] { "OwnerId" });
            DropColumn("dbo.UserGroups", "OwnerId");
            DropColumn("dbo.Containers", "OwnerId");
            DropColumn("dbo.ContainerGroups", "OwnerId");
        }
    }
}
