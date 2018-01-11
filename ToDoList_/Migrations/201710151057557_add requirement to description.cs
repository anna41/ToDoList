namespace ToDoList_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequirementtodescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Plans", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Plans", "Description", c => c.String());
        }
    }
}
