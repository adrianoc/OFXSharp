using System;
using System.Collections.Generic;
using System.Xml;

namespace OFXSharp
{

    public class SECInfo
    {
        public string UniqueID { get; set; }
        public string UniqueIDType { get; set; }
        public string SECName { get; set; }
        public string Ticker { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime AsOfDate { get; set; }
    }

    public class StockInfo : SECInfo
    {
        private readonly bool hasUnitPrice;


        public bool HasUnitPrice { get { return hasUnitPrice; }  }
        public string StockType { get; set; }
        public string AssetClass { get; set; }
        public string FIAssetClass { get; set; }

        public StockInfo(XmlNode node)
        {
            UniqueID = node.GetValue(".//SECID//UNIQUEID");
            UniqueIDType = node.GetValue(".//SECID//UNIQUEIDTYPE");
            SECName = node.GetValue(".//SECNAME");
            Ticker = node.GetValue(".//TICKER");

            string unitPrice = node.GetValue(".//UNITPRICE").Trim();

            if (unitPrice != null && unitPrice != "")
            {
                UnitPrice = Convert.ToDecimal(unitPrice);
                hasUnitPrice = true;
            }
            else
            {
                hasUnitPrice = false;
            }
            AsOfDate = node.GetValue(".//DTASOF").ToDate();
            StockType = node.GetValue("//STOCKTYPE");
            AssetClass = node.GetValue(("//ASSETCLASS"));
            FIAssetClass = node.GetValue(("//FIASSETCLASS"));
        }
    }

    public class OptionInfo : SECInfo
    {

        public string OptionType { get; set; }
        public string AssetClass { get; set; }
        public string FIAssetClass { get; set; }
        public decimal StrikePrice { get; set; }
        public DateTime ExpirationDt { get; set; }

        public OptionInfo(XmlNode node)
        {
            UniqueID = node.GetValue(".//SECID//UNIQUEID");
            UniqueIDType = node.GetValue(".//SECID//UNIQUEIDTYPE");
            SECName = node.GetValue(".//SECNAME");
            Ticker = node.GetValue(".//TICKER");
            UnitPrice = Convert.ToDecimal(node.GetValue(".//UNITPRICE").Trim());
            AsOfDate = node.GetValue(".//DTASOF").ToDate();
            OptionType = node.GetValue("//OPTTYPE");
            StrikePrice = Convert.ToDecimal(node.GetValue(".//STRIKEPRICE").Trim());
            ExpirationDt = node.GetValue(".//DTEXPIRE").ToDate();
            AssetClass = node.GetValue(("//ASSETCLASS"));
            FIAssetClass = node.GetValue(("//FIASSETCLASS"));
        }
    }

    public class MutualFundInfo : SECInfo
    {
        private readonly bool hasUnitPrice;
        public bool HasUnitPrice { get { return hasUnitPrice; } }
        public string Memo { get; set; }
        public string MutualFundType { get; set; }

        public MutualFundInfo(XmlNode node)
        {
            UniqueID = node.GetValue(".//SECID//UNIQUEID");
            UniqueIDType = node.GetValue(".//SECID//UNIQUEIDTYPE");
            SECName = node.GetValue(".//SECNAME");
            Ticker = node.GetValue(".//TICKER");

            string unitPrice = node.GetValue(".//UNITPRICE").Trim();
            if (unitPrice == null || unitPrice == "")
                hasUnitPrice = false;
            else
            {
                UnitPrice = Convert.ToDecimal(unitPrice);
                hasUnitPrice = true;
            }

            AsOfDate = node.GetValue(".//DTASOF").ToDate();
            Memo = node.GetValue(".//MEMO");
            MutualFundType = node.GetValue("//MFTYPE");
        }
    }

    public class OtherInfo : SECInfo
    {
        public string TypeDescrption { get; set; }
        public string AssetClass { get; set; }
        public string FIAssetClass { get; set; }

        public OtherInfo(XmlNode node)
        {
            UniqueID = node.GetValue(".//SECID//UNIQUEID");
            UniqueIDType = node.GetValue(".//SECID//UNIQUEIDTYPE");
            SECName = node.GetValue(".//SECNAME");
            Ticker = node.GetValue(".//TICKER");
            UnitPrice = Convert.ToDecimal(node.GetValue(".//UNITPRICE").Trim());
            AsOfDate = node.GetValue(".//DTASOF").ToDate();
            TypeDescrption = node.GetValue("//TYPEDESC");
            AssetClass = node.GetValue(".//ASSETCLASS");
            FIAssetClass = node.GetValue(".//FIASSETCLASS");

        }
    }
}


