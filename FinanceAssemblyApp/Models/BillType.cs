using SQLite;

namespace FinanceAssemblyApp.Models
{
    [Table("gl_billType")]
    public class BillType
    {
        // PrimaryKey is typically numeric 
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [MaxLength(50), Unique]
        public string BillTypeNo { get; set; }

        [MaxLength(200)]
        public string BillTypeName { get; set; }

        // 0: add; 1: update
        public int IsAdd { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

    }
}
