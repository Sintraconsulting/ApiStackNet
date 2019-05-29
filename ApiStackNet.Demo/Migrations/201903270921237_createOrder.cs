namespace ApiStackNet.Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DBO.ORDER",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(),
                        Email = c.String(),
                        OrderDesc = c.String(),
                        Price = c.Double(nullable: false),
                        Address = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        DeletedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DeletedBy = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("DBO.ORDER");
        }
    }
}
