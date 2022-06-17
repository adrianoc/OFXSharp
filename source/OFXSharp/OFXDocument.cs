namespace OFXSharp
{
    public class OFXDocument
    {
        private readonly List<AccountInfo> accountInfos = new List<AccountInfo>();

        public DateTime StatementStart { get; set; }

        public DateTime StatementEnd { get; set; }

        public AccountType AccType { get; set; }

        public string Currency { get; set; }

        public SignOn SignOn { get; set; }

        public Account Account { get; set; }

        public AccountBalance Balance { get; set; }

        public List<AccountInfo> AccountInfos { get { return accountInfos; } }
    }
}


