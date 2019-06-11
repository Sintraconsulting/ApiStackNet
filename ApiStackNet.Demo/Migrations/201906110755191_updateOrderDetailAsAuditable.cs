namespace ApiStackNet.Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrderDetailAsAuditable : DbMigration
    {
        public override void Up()
        {
            AddColumn("DBO.ORDER_DETAIL", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("DBO.ORDER_DETAIL", "ModifiedOn", c => c.DateTime(nullable: false));
            AddColumn("DBO.ORDER_DETAIL", "DeletedOn", c => c.DateTime(nullable: false));
            AddColumn("DBO.ORDER_DETAIL", "CreatedBy", c => c.String());
            AddColumn("DBO.ORDER_DETAIL", "ModifiedBy", c => c.String());
            AddColumn("DBO.ORDER_DETAIL", "DeletedBy", c => c.String());
            AddColumn("DBO.ORDER_DETAIL", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("DBO.ORDER_DETAIL", "Active");
            DropColumn("DBO.ORDER_DETAIL", "DeletedBy");
            DropColumn("DBO.ORDER_DETAIL", "ModifiedBy");
            DropColumn("DBO.ORDER_DETAIL", "CreatedBy");
            DropColumn("DBO.ORDER_DETAIL", "DeletedOn");
            DropColumn("DBO.ORDER_DETAIL", "ModifiedOn");
            DropColumn("DBO.ORDER_DETAIL", "CreatedOn");
        }
    }
}
