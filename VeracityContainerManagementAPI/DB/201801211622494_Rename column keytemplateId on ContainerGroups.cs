namespace VeracityContainerManagementAPI.DB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamecolumnkeytemplateIdonContainerGroups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContainerGroups", "DefaultKeyTemplateId", c => c.Guid());
            DropColumn("dbo.ContainerGroups", "KeyTemplateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContainerGroups", "KeyTemplateId", c => c.Guid(nullable: false));
            DropColumn("dbo.ContainerGroups", "DefaultKeyTemplateId");
        }
    }
}
