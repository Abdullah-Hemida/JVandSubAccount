namespace JVandSubAccount.ViewModel
{
    public class JVDetailViewModel
    {
        public int JVDetailID { get; set; }
        public int AccountID { get; set; }
        public string AccountNameEn { get; set; } = string.Empty;
        public string AccountNameAr { get; set; } = string.Empty;
        public int SubAccountID { get; set; }
        public string SubAccountNameEn { get; set; } = string.Empty;
        public string SubAccountNameAr { get; set; } = string.Empty;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public bool IsDoc { get; set; }
        public string? Notes { get; set; }
    }
}
