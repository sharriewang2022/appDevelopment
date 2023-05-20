using SQLite;
using System.ComponentModel.DataAnnotations;

namespace WeiFin.Models
{
    [Table("gl_billType")]
    public class BillType
    {
        // PrimaryKey is typically numeric 
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [StringLength(50), Unique]
        public string BillTypeNo { get; set; }

        [StringLength(200, ErrorMessage = "Bill type name length can't be more than 200.")]
        public string BillTypeName { get; set; }

        // 0: add; 1: update
        public int IsAdd { get; set; }

        [StringLength(200, ErrorMessage = "Remark length can't be more than 200.")]
        public string Remark { get; set; }

    }
}
