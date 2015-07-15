namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IpAddressFieldUniqued : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "IpAddress", c => c.String(maxLength: 450));
            CreateIndex("dbo.AspNetUsers", "IpAddress", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "IpAddress" });
            AlterColumn("dbo.AspNetUsers", "IpAddress", c => c.String());
        }
    }
}
