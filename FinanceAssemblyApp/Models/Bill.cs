using SQLite;
using System.ComponentModel.DataAnnotations;

namespace FinanceAssemblyApp.Models
{
    [Table("gl_bill")]
    public class Bill
    {
        // PrimaryKey is typically numeric 
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "BillType length can't be more than 30.")]
        public string BillType { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(50), Unique]
        public string BillNo { get; set; }

        [System.ComponentModel.DataAnnotations.MaxLength(200)]
        public string BillName { get; set; }

        [Required]
        public DateTime BillDate { get; set; }

        [Range(0,12,ErrorMessage = "Bill month must be between 1 and 12 ")]
        public int BillMonth { get; set; }

        public int BillYear { get; set; }

        // 1:income; 0:payment
        [Required]
        public int BillDirection { get; set; }

        public DateTime InputDate { get; set; }

        [Required]
        public decimal BillAmount { get; set; }

        [StringLength(200, ErrorMessage = "Bill Abstract length can't be more than 200.")]
        public string BillAbstract { get; set; }

        [StringLength(200, ErrorMessage = "Remark length can't be more than 200.")]
        public string Remark { get; set; }

        // 0: add; 1: update
        public int IsAdd { get; set; }

    }
}
