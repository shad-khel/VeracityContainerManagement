namespace VeracityContainerManagementAPI.DB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addkeytemplate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContainerGroups", "KeyTemplateId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContainerGroups", "KeyTemplateId");
        }
    }
}
