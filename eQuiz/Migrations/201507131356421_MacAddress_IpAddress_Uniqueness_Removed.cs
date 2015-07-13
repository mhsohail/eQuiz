namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MacAddress_IpAddress_Uniqueness_Removed : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "MacAddress" });
            DropIndex("dbo.AspNetUsers", new[] { "IpAddress" });
            AlterColumn("dbo.AspNetUsers", "MacAddress", c => c.String());
            AlterColumn("dbo.AspNetUsers", "IpAddress", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "IpAddress", c => c.String(maxLength: 450));
            AlterColumn("dbo.AspNetUsers", "MacAddress", c => c.String(maxLength: 450));
            CreateIndex("dbo.AspNetUsers", "IpAddress", unique: true);
            CreateIndex("dbo.AspNetUsers", "MacAddress", unique: true);
        }
    }
}
