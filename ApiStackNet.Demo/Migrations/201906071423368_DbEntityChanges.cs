namespace ApiStackNet.Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbEntityChanges : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "DBO.ORDER", name: "UserId", newName: "USER_ID");
            CreateTable(
                "DBO.USER",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        USER_ID = c.Int(nullable: false),
                        NAME = c.String(),
                        ADDRESS = c.String(),
                        EMAIL = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        DeletedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DeletedBy = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "DBO.ORDER_DETAIL",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ORDER_ID = c.Int(nullable: false),
                        PRODUCT_ID = c.Int(nullable: false),
                        QUANTITY = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("DBO.ORDER", t => t.ORDER_ID, cascadeDelete: true)
                .ForeignKey("DBO.PRODUCT", t => t.PRODUCT_ID, cascadeDelete: true)
                .Index(t => t.ORDER_ID)
                .Index(t => t.PRODUCT_ID);
            
            CreateTable(
                "DBO.PRODUCT",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PRODUCT_ID = c.Int(nullable: false),
                        DESCRIPTION = c.String(),
                        PRICE = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        DeletedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        DeletedBy = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("DBO.ORDER", "ORDER_ID", c => c.Int(nullable: false));
            AddColumn("DBO.ORDER", "DATA", c => c.DateTime(nullable: false));
            AddColumn("DBO.ORDER", "AMOUNT", c => c.Double(nullable: false));
            CreateIndex("DBO.ORDER", "USER_ID");
            AddForeignKey("DBO.ORDER", "USER_ID", "DBO.USER", "Id", cascadeDelete: true);
            DropColumn("DBO.ORDER", "UserName");
            DropColumn("DBO.ORDER", "Email");
            DropColumn("DBO.ORDER", "OrderDesc");
            DropColumn("DBO.ORDER", "Price");
            DropColumn("DBO.ORDER", "Address");
            DropTable("DBO.MYTABLE");
        }
        
        public override void Down()
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
            
            AddColumn("DBO.ORDER", "Address", c => c.String());
            AddColumn("DBO.ORDER", "Price", c => c.Double(nullable: false));
            AddColumn("DBO.ORDER", "OrderDesc", c => c.String());
            AddColumn("DBO.ORDER", "Email", c => c.String());
            AddColumn("DBO.ORDER", "UserName", c => c.String());
            DropForeignKey("DBO.ORDER_DETAIL", "PRODUCT_ID", "DBO.PRODUCT");
            DropForeignKey("DBO.ORDER_DETAIL", "ORDER_ID", "DBO.ORDER");
            DropForeignKey("DBO.ORDER", "USER_ID", "DBO.USER");
            DropIndex("DBO.ORDER_DETAIL", new[] { "PRODUCT_ID" });
            DropIndex("DBO.ORDER_DETAIL", new[] { "ORDER_ID" });
            DropIndex("DBO.ORDER", new[] { "USER_ID" });
            DropColumn("DBO.ORDER", "AMOUNT");
            DropColumn("DBO.ORDER", "DATA");
            DropColumn("DBO.ORDER", "ORDER_ID");
            DropTable("DBO.PRODUCT");
            DropTable("DBO.ORDER_DETAIL");
            DropTable("DBO.USER");
            RenameColumn(table: "DBO.ORDER", name: "USER_ID", newName: "UserId");
        }
    }
}
