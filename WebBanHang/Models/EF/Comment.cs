using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHang.Models.EF
{
    [Table("tb_Comment")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}