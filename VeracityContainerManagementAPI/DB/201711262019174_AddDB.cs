namespace VeracityContainerManagementAPI.DB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContainerGroups",
                c => new
                    {
                        ContainerGroupId = c.Guid(nullable: false),
                        ContainerGroupName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ContainerGroupId);
            
            CreateTable(
                "dbo.Containers",
                c => new
                    {
                        ContainerId = c.Guid(nullable: false),
                        ContainerName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ContainerId);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        UserGroupId = c.Guid(nullable: false),
                        UserGroupName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserGroupId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ContainerGroupsContainers",
                c => new
                    {
                        ContainerGroupId = c.Guid(nullable: false),
                        ContainerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContainerGroupId, t.ContainerId })
                .ForeignKey("dbo.ContainerGroups", t => t.ContainerGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Containers", t => t.ContainerId, cascadeDelete: true)
                .Index(t => t.ContainerGroupId)
                .Index(t => t.ContainerId);
            
            CreateTable(
                "dbo.UserGroupsUsers",
                c => new
                    {
                        UserGroupId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserGroupId, t.UserId })
                .ForeignKey("dbo.UserGroups", t => t.UserGroupId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserGroupId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserGroupsContainerGroups",
                c => new
                    {
                        ContainerGroupId = c.Guid(nullable: false),
                        UserGroupId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContainerGroupId, t.UserGroupId })
                .ForeignKey("dbo.ContainerGroups", t => t.ContainerGroupId, cascadeDelete: true)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupId, cascadeDelete: true)
                .Index(t => t.ContainerGroupId)
                .Index(t => t.UserGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGroupsContainerGroups", "UserGroupId", "dbo.UserGroups");
            DropForeignKey("dbo.UserGroupsContainerGroups", "ContainerGroupId", "dbo.ContainerGroups");
            DropForeignKey("dbo.UserGroupsUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserGroupsUsers", "UserGroupId", "dbo.UserGroups");
            DropForeignKey("dbo.ContainerGroupsContainers", "ContainerId", "dbo.Containers");
            DropForeignKey("dbo.ContainerGroupsContainers", "ContainerGroupId", "dbo.ContainerGroups");
            DropIndex("dbo.UserGroupsContainerGroups", new[] { "UserGroupId" });
            DropIndex("dbo.UserGroupsContainerGroups", new[] { "ContainerGroupId" });
            DropIndex("dbo.UserGroupsUsers", new[] { "UserId" });
            DropIndex("dbo.UserGroupsUsers", new[] { "UserGroupId" });
            DropIndex("dbo.ContainerGroupsContainers", new[] { "ContainerId" });
            DropIndex("dbo.ContainerGroupsContainers", new[] { "ContainerGroupId" });
            DropTable("dbo.UserGroupsContainerGroups");
            DropTable("dbo.UserGroupsUsers");
            DropTable("dbo.ContainerGroupsContainers");
            DropTable("dbo.Users");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Containers");
            DropTable("dbo.ContainerGroups");
        }
    }
}
