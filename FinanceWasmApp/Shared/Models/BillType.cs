using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace FinanceWasmApp.Shared.Models
{
    public class BillType
    {
        // PrimaryKey is typically numeric 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Bill type name length can't be more than 200.")]
        public string BillTypeNo { get; set; } 

        [MaxLength(200, ErrorMessage = "Bill type name length can't be more than 200.")]
        public string BillTypeName { get; set; } 

        // 0: add; 1: update
        public int IsAdd { get; set; } = 0;

        [MaxLength(200, ErrorMessage = "Remark length can't be more than 200.")]
        public string Remark { get; set; } = String.Empty;

    }
}
