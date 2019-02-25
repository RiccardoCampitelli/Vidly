namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBirthdateCustomer : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET BirthDate=CAST('1997-03-28' AS DATETIME) WHERE Id=4");
        }
        
        public override void Down()
        {
        }
    }
}
