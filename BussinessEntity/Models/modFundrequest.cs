namespace BussinessEntity.Models
{
    public class modFundrequest
    {
        public string Regno { get; set; }
        public decimal amount { get; set; } = 0;
        public decimal usd { get; set; } = 0;
        public string Utrno { get; set; }
        public string Remarks { get; set; }
        public string RequestType { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountHName { get; set; }
        public string AccountNo { get; set; }
        public string UpiId { get; set; }
    }
}