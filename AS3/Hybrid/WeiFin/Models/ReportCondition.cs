namespace WeiFin.Models
{
    //search condition when generating the financial report
    public class ReportCondition
    {
  
        public string BillType { get; set; }
  
        public string BillNo { get; set; }

        public string BillName { get; set; }

        public DateTime BillDate { get; set; }

        public int StartMonth { get; set; }

        public int EndMonth { get; set; }

        public int Year { get; set; }

        // income; payment
        public String BillDirection { get; set; }

        public decimal BillAmount { get; set; }

        public string BillAbstract { get; set; }

        public string Remark { get; set; }

    }
}
