using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeFin.Models
{
    public class LedgerReportEntity
    {
        public string AccountNo { get; set; }

        public int BillYear { get; set; }

        [Range(0, 12, ErrorMessage = "Bill month must be between 1 and 12 ")]
        public int BillMonth { get; set; }

        [StringLength(30, ErrorMessage = "BillType length can't be more than 30.")]
        public string BillType { get; set; }

        public string Abstract { get; set; }

        public string BalanceDirection { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TotalPayment { get; set; }

        public decimal RowSumAmount { get; set; }
    }
}
