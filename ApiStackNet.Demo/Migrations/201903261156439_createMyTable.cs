namespace ApiStackNet.Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createMyTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "DBO.MYTABLE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MY_CUSTOM_NAME = c.String(),
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
            DropTable("DBO.MYTABLE");
        }
    }
}
