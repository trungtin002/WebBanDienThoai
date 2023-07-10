namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateComment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_OrderDetail", "Comment_Id", "dbo.tb_Comment");
            DropForeignKey("dbo.tb_Product", "Comment_Id", "dbo.tb_Comment");
            DropForeignKey("dbo.tb_ProductImage", "Comment_Id", "dbo.tb_Comment");
            DropIndex("dbo.tb_OrderDetail", new[] { "Comment_Id" });
            DropIndex("dbo.tb_Product", new[] { "Comment_Id" });
            DropIndex("dbo.tb_ProductImage", new[] { "Comment_Id" });
            AddColumn("dbo.tb_Comment", "FullName", c => c.String());
            AddColumn("dbo.tb_Comment", "Rate", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Comment", "UserId");
            DropColumn("dbo.tb_Comment", "Email");
            DropColumn("dbo.tb_OrderDetail", "Comment_Id");
            DropColumn("dbo.tb_Product", "Comment_Id");
            DropColumn("dbo.tb_ProductImage", "Comment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_ProductImage", "Comment_Id", c => c.Int());
            AddColumn("dbo.tb_Product", "Comment_Id", c => c.Int());
            AddColumn("dbo.tb_OrderDetail", "Comment_Id", c => c.Int());
            AddColumn("dbo.tb_Comment", "Email", c => c.String());
            AddColumn("dbo.tb_Comment", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Comment", "Rate");
            DropColumn("dbo.tb_Comment", "FullName");
            CreateIndex("dbo.tb_ProductImage", "Comment_Id");
            CreateIndex("dbo.tb_Product", "Comment_Id");
            CreateIndex("dbo.tb_OrderDetail", "Comment_Id");
            AddForeignKey("dbo.tb_ProductImage", "Comment_Id", "dbo.tb_Comment", "Id");
            AddForeignKey("dbo.tb_Product", "Comment_Id", "dbo.tb_Comment", "Id");
            AddForeignKey("dbo.tb_OrderDetail", "Comment_Id", "dbo.tb_Comment", "Id");
        }
    }
}
