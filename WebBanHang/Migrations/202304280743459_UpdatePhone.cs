namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "Phone", c => c.String(nullable: false));
            AddColumn("dbo.tb_Order", "Email", c => c.String());
            AddColumn("dbo.tb_Order", "TypePayment", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Order", "TypePaymet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Order", "TypePaymet", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Order", "TypePayment");
            DropColumn("dbo.tb_Order", "Email");
            DropColumn("dbo.tb_Order", "Phone");
        }
    }
}
