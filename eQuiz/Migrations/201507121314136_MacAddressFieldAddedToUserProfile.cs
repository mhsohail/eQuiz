namespace eQuiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MacAddressFieldAddedToUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MacAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MacAddress");
        }
    }
}
