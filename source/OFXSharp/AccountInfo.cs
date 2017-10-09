using System;
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

        private static bool GetYNBoolean(XmlNode node, string valueName)
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
            BankID = node.GetValue(".//BANKID");
            BranchID = node.GetValue(".//BRANCHID");
            AccountID = node.GetValue(".//ACCTID");
            AccountType = node.GetValue(".//ACCTTYPE");

            SupportsDownload = GetYNBoolean(node, ".//SUPTXDL");
            IsTransferSource = GetYNBoolean(node, ".//XFERSRC");
            IsTransferDestination = GetYNBoolean(node, ".//XFERDEST");

            ServiceStatus = node.GetValue(".//SVCSTATUS");

            Description = node.GetValue(".//DESC");
        }
    }
}


