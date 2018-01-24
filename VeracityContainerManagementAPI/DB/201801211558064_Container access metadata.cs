namespace VeracityContainerManagementAPI.DB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Containeraccessmetadata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserGroupsContainerGroups", "ContainerGroupId", "dbo.ContainerGroups");
            DropForeignKey("dbo.UserGroupsContainerGroups", "UserGroupId", "dbo.UserGroups");
            DropIndex("dbo.UserGroupsContainerGroups", new[] { "ContainerGroupId" });
            DropIndex("dbo.UserGroupsContainerGroups", new[] { "UserGroupId" });
            CreateTable(
                "dbo.ContainerAccesses",
                c => new
                    {
                        ContainerAccessId = c.Guid(nullable: false),
                        UserGroupId = c.Guid(nullable: false),
                        ContainerGroupId = c.Guid(nullable: false),
                        AccessKeyId = c.Guid(nullable: false),
                        DateTimeAdded = c.DateTime(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ContainerAccessId)
                .ForeignKey("dbo.ContainerGroups", t => t.ContainerGroupId)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .ForeignKey("dbo.UserGroups", t => t.UserGroupId)
                .Index(t => t.UserGroupId)
                .Index(t => t.ContainerGroupId)
                .Index(t => t.OwnerId);
            
            DropTable("dbo.UserGroupsContainerGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserGroupsContainerGroups",
                c => new
                    {
                        ContainerGroupId = c.Guid(nullable: false),
                        UserGroupId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContainerGroupId, t.UserGroupId });
            
            DropForeignKey("dbo.ContainerAccesses", "UserGroupId", "dbo.UserGroups");
            DropForeignKey("dbo.ContainerAccesses", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.ContainerAccesses", "ContainerGroupId", "dbo.ContainerGroups");
            DropIndex("dbo.ContainerAccesses", new[] { "OwnerId" });
            DropIndex("dbo.ContainerAccesses", new[] { "ContainerGroupId" });
            DropIndex("dbo.ContainerAccesses", new[] { "UserGroupId" });
            DropTable("dbo.ContainerAccesses");
            CreateIndex("dbo.UserGroupsContainerGroups", "UserGroupId");
            CreateIndex("dbo.UserGroupsContainerGroups", "ContainerGroupId");
            AddForeignKey("dbo.UserGroupsContainerGroups", "UserGroupId", "dbo.UserGroups", "UserGroupId", cascadeDelete: true);
            AddForeignKey("dbo.UserGroupsContainerGroups", "ContainerGroupId", "dbo.ContainerGroups", "ContainerGroupId", cascadeDelete: true);
        }
    }
}
