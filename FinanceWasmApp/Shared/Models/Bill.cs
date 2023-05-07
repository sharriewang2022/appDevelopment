using System.ComponentModel.DataAnnotations;

namespace FinanceWasmApp.Shared.Models
{
    public class Bill
    {
        // PrimaryKey is typically numeric 
        public int Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Bill Type length can't be more than 30.")]
        public string BillType { get; set; }

        [MaxLength(50, ErrorMessage = "Bill number length can't be more than 30.")]
        public string BillNo { get; set; } = String.Empty;

        [MaxLength(200, ErrorMessage = "Bill name length can't be more than 30.")]
        public string BillName { get; set; } = String.Empty;

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

        [MaxLength(200, ErrorMessage = "Bill Abstract length can't be more than 200.")]
        public string BillAbstract { get; set; } = String.Empty;

        [MaxLength(200, ErrorMessage = "Remark length can't be more than 200.")]
        public string Remark { get; set; } = String.Empty;

        // 0: add; 1: update
        public int IsAdd { get; set; } = 0;

    }
}
