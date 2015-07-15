namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MacAddressFieldUniqued : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "MacAddress", c => c.String(maxLength: 450));
            CreateIndex("dbo.AspNetUsers", "MacAddress", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "MacAddress" });
            AlterColumn("dbo.AspNetUsers", "MacAddress", c => c.String());
        }
    }
}
