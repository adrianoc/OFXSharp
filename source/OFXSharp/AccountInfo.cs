using System.Xml;

namespace OFXSharp
{
    public class AccountInfo
    {
        public string BankID { get; set; }
        public string BranchID { get; set; }
        public string AccountID { get; set; }

        public string AccountType { get; set; }

        public bool SupportsDownload { get; set; }
        public bool IsTransferSource { get; set; }
        public bool IsTransferDestination { get; set; }

        public string ServiceStatus { get; set; }

        public string Description { get; set; }

        protected static bool GetYNBoolean(XmlNode node, string valueName)
        {
            string str = node.GetValue(valueName);

            if (str == null)
                return false;

            if (str == "Y")
                return true;

            return false;
        }

        public AccountInfo(XmlNode node)
        {
            AccountID = node.GetValue(".//ACCTID");

            BankID = node.GetValue(".//BANKID");
            BranchID = node.GetValue(".//BRANCHID");
            AccountType = node.GetValue(".//ACCTTYPE");

            SupportsDownload = GetYNBoolean(node, ".//SUPTXDL");
            IsTransferSource = GetYNBoolean(node, ".//XFERSRC");
            IsTransferDestination = GetYNBoolean(node, ".//XFERDEST");

            ServiceStatus = node.GetValue(".//SVCSTATUS");

            Description = node.GetValue(".//DESC");
        }
    }

    public class InvestmentAccountInfo : AccountInfo
    {
        public string BrokerID { get; set; }

        public bool IsChecking { get; set; }
        public string USProductType { get; set; }

        public string InvestmentAccountType { get; set; }

        public string OptionLevel { get; set; }

        public InvestmentAccountInfo(XmlNode info, XmlNode investmentAccountNode)
            : base(info)
        {
            USProductType = investmentAccountNode.GetValue(".//USPRODUCTTYPE");
            BrokerID = investmentAccountNode.GetValue(".//BROKERID");
            IsChecking = GetYNBoolean(investmentAccountNode, ".//CHECKING");
            AccountType = "INVESTMENT";

            InvestmentAccountType = investmentAccountNode.GetValue("..//INVACCTTYPE");
            OptionLevel = investmentAccountNode.GetValue(".//OPTIONLEVEL");

        }
    }


    public class CCAccountInfo : AccountInfo
    {
        public CCAccountInfo(XmlNode info, XmlNode ccAccountNode)
            : base(info)
        {
            AccountType = "CC";
        }
    }

}



