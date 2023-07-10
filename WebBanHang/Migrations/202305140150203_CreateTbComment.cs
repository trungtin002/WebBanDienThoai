namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTbComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Description = c.String(),
                        Email = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_OrderDetail", "Comment_Id", c => c.Int());
            AddColumn("dbo.tb_Product", "Comment_Id", c => c.Int());
            AddColumn("dbo.tb_ProductImage", "Comment_Id", c => c.Int());
            CreateIndex("dbo.tb_OrderDetail", "Comment_Id");
            CreateIndex("dbo.tb_Product", "Comment_Id");
            CreateIndex("dbo.tb_ProductImage", "Comment_Id");
            AddForeignKey("dbo.tb_OrderDetail", "Comment_Id", "dbo.tb_Comment", "Id");
            AddForeignKey("dbo.tb_Product", "Comment_Id", "dbo.tb_Comment", "Id");
            AddForeignKey("dbo.tb_ProductImage", "Comment_Id", "dbo.tb_Comment", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_ProductImage", "Comment_Id", "dbo.tb_Comment");
            DropForeignKey("dbo.tb_Product", "Comment_Id", "dbo.tb_Comment");
            DropForeignKey("dbo.tb_OrderDetail", "Comment_Id", "dbo.tb_Comment");
            DropIndex("dbo.tb_ProductImage", new[] { "Comment_Id" });
            DropIndex("dbo.tb_Product", new[] { "Comment_Id" });
            DropIndex("dbo.tb_OrderDetail", new[] { "Comment_Id" });
            DropColumn("dbo.tb_ProductImage", "Comment_Id");
            DropColumn("dbo.tb_Product", "Comment_Id");
            DropColumn("dbo.tb_OrderDetail", "Comment_Id");
            DropTable("dbo.tb_Comment");
        }
    }
}
