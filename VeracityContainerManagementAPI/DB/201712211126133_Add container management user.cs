namespace VeracityContainerManagementAPI.DB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcontainermanagementuser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContainerManagementUsers",
                c => new
                    {
                        CMUserId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CMUserId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContainerManagementUsers", "UserId", "dbo.Users");
            DropIndex("dbo.ContainerManagementUsers", new[] { "UserId" });
            DropTable("dbo.ContainerManagementUsers");
        }
    }
}
